using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Models.Collada;
using Models.Intermediate;

namespace CM3D2DataViewer
{
    // OpenCollada用
    public class DaeImporter : Importer
    {
        public string                   FileName                { get; private set; }
        public string                   Directory               { get; private set; }

        public Root                     Root                    { get; private set; }
        public Dictionary<ModelVertex, int> Vertices            { get; private set; }
        public List<int>                Indices                 { get; private set; }
      //public Dictionary<int, ModelSkin> Skins                 { get; private set; }

        public DaeImporter(ImportSettings settings)
            : base(settings)
        {
        }

        public override void Import(string filename, ModelFile model)
        {
          //MorphRange      = MorphMax - MorphMin;

            var importer    = new DaeIMImpoter();
            Root            = importer.Import(filename);
            var geoms       = Root.Instances.OfType<Geometry>          ().ToArray();
            Vertices        = new Dictionary<ModelVertex, int>();
            Indices         = new List<int>();
            var geom        = geoms.First();
            var mesh        = geom.Get<Mesh>("Data");
            var posch       = mesh.Channels.FirstOrDefault(i => i.Semantic == GeometrySemantic.Position);
            var nrmch       = mesh.Channels.FirstOrDefault(i => i.Semantic == GeometrySemantic.Normal);
            var texch       = mesh.Channels.FirstOrDefault(i => i.Semantic == GeometrySemantic.TexCoord);
            var pos         = posch.GetDataAsList<SlimDX.Vector3>();
            var nrm         = nrmch.GetDataAsList<SlimDX.Vector3>();
            var tex         = texch.GetDataAsList<SlimDX.Vector2>();
            var posface     = posch.GetIndicesAsList(mesh);
            var nrmface     = nrmch.GetIndicesAsList(mesh);
            var texface     = texch.GetIndicesAsList(mesh);
            var numidx      = posface.Count;

            for(int i= 0; i < numidx; ++i)
            {
                var p       = pos[posface[i]];
                var n       = nrm[nrmface[i]];
                var t       = tex[texface[i]];
                var v       = new ModelVertex()
                {
                    P       = new Vector3(p.X, p.Y, p.Z),
                    N       = new Vector3(n.X, n.Y, n.Z),
                    T       = new Vector2(t.X, t.Y),
                };

                int index;

                if(!Vertices.TryGetValue(v, out index))
                    Vertices.Add(v, index= Vertices.Count);

                Indices.Add(index);
            }

            var prims   = new List<ModelPrimitive>();
            var start   = 0;

            foreach(var i in mesh.Primitives)
            {
                var prim        = new ModelPrimitive();
                prim.Indices    = Indices.Skip(start).Take(i.Count).Select(j => (ushort)j).ToList();
                prim.NumIndices = Indices.Count;
                start           +=Indices.Count;

                prims.Add(prim);
            }

            model.Mesh.Skins        = Vertices.Keys
                .Select(i => model.Mesh.Skins[FindClosest(model.Mesh.Vertices, i.P)])
                .ToList();
            model.Mesh.Vertices     = Vertices.Keys.ToList();
            model.Mesh.NumVerts     = model.Mesh.Vertices.Count;
            model.Mesh.Primitives   = prims;
            model.Mesh.NumPrims     = prims.Count;

            // モーフィングをボディから反映
            var morphs      = model.Params.OfType<ParamMorph>().ToList();
            var body001     = DataManager.Instance.Body001;

            foreach(var i in morphs)
            {
                var index       = (ushort)0;
                var bodymorph   = body001.Params.FirstOrDefault(j => j.Name == i.Name) as ParamMorph;
                var dic         = bodymorph.Vertices.ToDictionary(j => j.Index);
                var dic2        = new Dictionary<int, MorphVertex>();
                var rr          = Settings.MorphMax - Settings.MorphMin;
                MorphVertex mv;

                foreach(var j in model.Mesh.Vertices)
                {
                    ++index;

                    var near    = FindClosest(body001.Mesh.Vertices, j.P);

                    if(!dic.TryGetValue((ushort)near, out mv))
                        continue;

                    var v       = body001.Mesh.Vertices[near];
                    var x       = v.P.X - j.P.X;
                    var y       = v.P.Y - j.P.Y;
                    var z       = v.P.Z - j.P.Z;
                    var d       = (float)Math.Sqrt(x*x + y*y + z*z);

                    if(d >= Settings.MorphMax)
                        continue;

                    var r       = d <= Settings.MorphMin ? 1.0f : 1.0f - (d-Settings.MorphMin) / rr;
                  //r           = r * MorphScale;
                    var mv2     = new MorphVertex(index-1, mv.X*r, mv.Y*r, mv.Z*r,  mv.NX*r, mv.NY*r, mv.NZ*r);

                    dic2.Add((ushort)(index-1), mv2);
                }

                i.Vertices      = dic2.OrderBy(j => j.Key).Select(j => j.Value).ToList();
                i.NumVertices   = i.Vertices.Count;
            }

            ModelFile.ToFile(model.FileName, model);

            System.Diagnostics.Debug.Print("終了");
        }

        public void ImportOld(string filename, ModelFile model)
        {
            var importer    = new DaeIMImpoter();
            Root            = importer.Import(filename);

            var skins       = Root.Instances.OfType<SkinDeclaraion>    ().ToArray();
            var morphs      = Root.Instances.OfType<MorphingDeclaraion>().ToArray();
            var geoms       = Root.Instances.OfType<Geometry>          ().ToArray();
            Vertices        = new Dictionary<ModelVertex, int>();
            Indices         = new List<int>();
          //Skins           = new Dictionary<int, ModelSkin>();

            if(morphs.Length > 0)
            {
                var geom    = morphs.First().Source as Geometry;
                var mesh    = geom.Get<Mesh>("Data");
                var posch   = mesh.Channels.FirstOrDefault(i => i.Semantic == GeometrySemantic.Position);
                var nrmch   = mesh.Channels.FirstOrDefault(i => i.Semantic == GeometrySemantic.Normal);
                var texch   = mesh.Channels.FirstOrDefault(i => i.Semantic == GeometrySemantic.TexCoord);
                var pos     = posch.GetDataAsList<SlimDX.Vector3>();
                var nrm     = nrmch.GetDataAsList<SlimDX.Vector3>();
                var tex     = texch.GetDataAsList<SlimDX.Vector2>();
                var posface = posch.GetIndicesAsList(mesh);
                var nrmface = nrmch.GetIndicesAsList(mesh);
                var texface = texch.GetIndicesAsList(mesh);
                var numidx  = posface.Count;

                for(int i= 0; i < numidx; ++i)
                {
                    var p   = pos[posface[i]];
                    var n   = nrm[nrmface[i]];
                    var t   = tex[texface[i]];
                    var v   = new ModelVertex()
                    {
                        P   = new Vector3(p.X, p.Y, p.Z),
                        N   = new Vector3(n.X, n.Y, n.Z),
                        T   = new Vector2(t.X, t.Y),
                    };

                    int index;

                    if(!Vertices.TryGetValue(v, out index))
                        Vertices.Add(v, index= Vertices.Count);

                    Indices.Add(index);
                }

                var prims   = new List<ModelPrimitive>();
                var start   = 0;

                foreach(var i in mesh.Primitives)
                {
                    var prim        = new ModelPrimitive();
                    prim.Indices    = Indices.Skip(start).Take(i.Count).Select(j => (ushort)j).ToList();
                    prim.NumIndices = Indices.Count;
                    start           +=Indices.Count;

                    prims.Add(prim);
                }

                model.Mesh.Skins        = Vertices.Keys
                    .Select(i => model.Mesh.Skins[FindClosest(model.Mesh.Vertices, i.P)])
                    .ToList();
                model.Mesh.Vertices     = Vertices.Keys.ToList();
                model.Mesh.NumVerts     = model.Mesh.Vertices.Count;
                model.Mesh.Primitives   = prims;
                model.Mesh.NumPrims     = prims.Count;

                // モーフィング
                model.Params    = model.Params.Where(i => !(i is ParamMorph)).ToList();

                foreach(var i in morphs.First().Channels)
                {
                    var mmesh   = i.Geometry.Get<Mesh>("Data");
                    var mposch  = mmesh.Channels.FirstOrDefault(j => j.Semantic == GeometrySemantic.Position);
                    var mpos    = mposch.GetDataAsList<SlimDX.Vector3>();
                    var mposidx = mposch.GetIndicesAsList(mmesh);
                    var dic     = new Dictionary<int, MorphVertex>();
                    var mnrm    = new SlimDX.Vector3[mpos.Count];

                    // 法線計算
                    for(int j= 0; j < mposidx.Count; j+=3)
                    {
                        var a   = mpos[mposidx[j+0]];
                        var b   = mpos[mposidx[j+1]];
                        var c   = mpos[mposidx[j+2]];
                        var ab  = SlimDX.Vector3.Normalize(SlimDX.Vector3.Subtract(b, a));
                        var ac  = SlimDX.Vector3.Normalize(SlimDX.Vector3.Subtract(c, a));
                        var n   = SlimDX.Vector3.Normalize(SlimDX.Vector3.Cross(ab, ac));

                        mnrm[mposidx[j+0]]  = SlimDX.Vector3.Add(mnrm[mposidx[j+0]], n);
                        mnrm[mposidx[j+1]]  = SlimDX.Vector3.Add(mnrm[mposidx[j+1]], n);
                        mnrm[mposidx[j+2]]  = SlimDX.Vector3.Add(mnrm[mposidx[j+2]], n);
                    }

                    for(int j= 0; j < mnrm.Length; ++j)
                        mnrm[j] = SlimDX.Vector3.Normalize(mnrm[j]);

                    for(int j= 0; j < mposidx.Count; ++j)
                    {
                        var ii  = (ushort)Indices[j];
                        var v   = model.Mesh.Vertices[ii];
                        var p   = mpos[mposidx[j]];
                        var n   = mnrm[mposidx[j]];
                        p.X     -=v.P.X;
                        p.Y     -=v.P.Y;
                        p.Z     -=v.P.Z;
                        n.X     -=v.N.X;
                        n.Y     -=v.N.Y;
                        n.Z     -=v.N.Z;
                        dic[ii] = new MorphVertex(ii, p.X, p.Y, p.Z, n.X, n.Y, n.Z);
                    }

                    var morph   = new ParamMorph()
                    {
                        Name        = i.Geometry.Name,
                        Vertices    = dic.OrderBy(j => j.Key).Select(j => j.Value).ToList(),
                        NumVertices = dic.Count,
                    };

                    model.Params.Insert(0, morph);
                }
            } else
            {
                throw new Exception();
            }

            ModelFile.ToFile(model.FileName, model);

            System.Diagnostics.Debug.Print("終了");
        }

        private int FindClosest(List<ModelVertex> verts, Vector3 pos)
        {
            var d       = float.MaxValue;
            int index   = 0;
            int closest = 0;

            foreach(var i in verts)
            {
                var x   = i.P.X-pos.X;
                var y   = i.P.Y-pos.Y;
                var z   = i.P.Z-pos.Z;
                var dd  = x*x + y*y + z*z;

                if(dd < d)
                {
                    closest = index;
                    d   = dd;
                }

                ++index;
            }

            //System.Diagnostics.Debug.Print(d.ToString());

            return closest;
        }
    }
}
