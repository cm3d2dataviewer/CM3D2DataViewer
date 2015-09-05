using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CM3D2DataViewer
{
    public class ModelGenerator
    {
        public ModelFile                RefModel                { get; private set; }
        public Dictionary<ModelVertex, ushort> Vertices         { get; private set; }
        public List<ModelPrimitive>     Primitives              { get; private set; }
        public ModelPrimitive           CurrentPrimitive        { get; private set; }
        public List<int>                Closest                 { get; private set; }
        public float                    MorphMin                { get; set; }
        public float                    MorphMax                { get; set; }

        public ModelGenerator()
        {
            Vertices    = new Dictionary<ModelVertex, ushort>();
            Primitives  = new List<ModelPrimitive>();
            MorphMin    = 0.02f;
            MorphMax    = 0.10f;
        }

        public void BeginPrimitive()
        {
            CurrentPrimitive        = new ModelPrimitive();
            CurrentPrimitive.Indices= new List<ushort>();

            Primitives.Add(CurrentPrimitive);
        }

        public void EndPrimitive()
        {
            CurrentPrimitive.NumIndices = CurrentPrimitive.Indices.Count;
        }

        public void AddVertex(ModelVertex v)
        {
            ushort  index;

            if(!Vertices.TryGetValue(v, out index))
                Vertices.Add(v, index= (ushort)Vertices.Count);

            Primitives.Last().Indices.Add(index);
        }

        public void AddVertex(Vector3 p, Vector3 n, Vector2 t)
        {
            AddVertex(new ModelVertex(p, n, t));
        }

        public void AddVertex(Vector3 p, Vector3 n, Vector3 t)
        {
            AddVertex(new ModelVertex(p, n, t));
        }

        public void ReplaceModel(ModelFile model, ModelFile refmodel)
        {
            var vertices= Vertices.Keys.ToList();
            Closest     = vertices.Select(i => FindClosest(refmodel.Mesh.Vertices, i.P)).ToList();

            // スキンの生成
            var sv      = new List<ModelSkin>();

            foreach(var i in Closest)
                sv.Add(refmodel.Mesh.Skins[i]);

            sv      = Closest.Select(i => refmodel.Mesh.Skins[i]).ToList();

            // モーフィングの作成
            var rr      = MorphMax - MorphMin;
            var morphs  = new List<ParamMorph>();

            foreach(var i in refmodel.Params.OfType<ParamMorph>()) // 元のモーフィングを列挙
            {
                var dic     = i.Vertices.ToDictionary(j => j.Index);
                var newmorph= new ParamMorph() {  Vertices= new List<MorphVertex>() };

                System.Diagnostics.Debug.Print("Morph:{0}", i.Name);

                morphs.Add(newmorph);

                for(int j= 0; j < vertices.Count; ++j)
                {
                    var v1  = vertices[j];                          // 新しい頂点
                    var v0  = refmodel.Mesh.Vertices[Closest[j]];   // 元の頂点で一番近いもの
                    var x   = v1.P.X - v0.P.X;
                    var y   = v1.P.Y - v0.P.Y;
                    var z   = v1.P.Z - v0.P.Z;
                    var d   = (float)Math.Sqrt(x*x + y*y + z*z);    // 両頂点の距離

                    // 一定以上離れていたら、モーフィングを引き継がない
                    if(d > MorphMax)
                    {
                      //System.Diagnostics.Debug.Print("  V {0} too far {1:F5}",
                      //    j.ToString().PadLeft(4), d);
                        continue;
                    }

                    MorphVertex mv;

                    // 元の頂点のモーフィングを検索
                    if(!dic.TryGetValue((ushort)Closest[j], out mv))
                    {
                      //System.Diagnostics.Debug.Print("  V {0} => {1} not found",
                      //    j.ToString().PadLeft(4), Closest[j].ToString().PadLeft(4));
                        continue;
                    }

                    // 距離により影響度を算出(MorphMin:1.0 ... MorphMax:0.0)
                    var r   = d <= MorphMin ? 1.0f : 1 - (d - MorphMin) / rr;
                  //r       *=MorphScale;
                  //var n0  = new Vector3(v0.N.X + mv.NX,  v0.N.Y + mv.NY, v0.N.Z + mv.NZ); // 元の頂点のモーフィング後頂点

                  //System.Diagnostics.Debug.Print("  V {0} => {1} dist={2:F5} weight{3:F5}",
                  //    j.ToString().PadLeft(4), Closest[j].ToString().PadLeft(4), d, r);

                    // モーフィング頂点の作成
                    newmorph.Vertices.Add(new MorphVertex()
                    {
                        Index   = (ushort)j,
                        X       = mv.X * r,
                        Y       = mv.Y * r,
                        Z       = mv.Z * r,
                        NX      = mv.NX * r,
                        NY      = mv.NY * r,
                        NZ      = mv.NZ * r,
                      //NX      = (n0.X - v1.N.X) * r,
                      //NY      = (n0.Y - v1.N.Y) * r,
                      //NZ      = (n0.Z - v1.N.Z) * r,
                    });
                }

                System.Diagnostics.Debug.Print("Old={0} New={1}", i.Vertices.Count, newmorph.Vertices.Count);

                newmorph.Name       = i.Name;
                newmorph.NumVertices= newmorph.Vertices.Count;
            }

            // モデルの更新
            model.Mesh.Vertices     = vertices;
            model.Mesh.NumVerts     = vertices.Count;
            model.Mesh.Primitives   = Primitives;
            model.Mesh.NumPrims     = Primitives.Count;
            model.Mesh.Skins        = sv;
            model.Params            = model.Params.Where(i => !(i is ParamMorph)).ToList();
            model.Params.InsertRange(0, morphs.Cast<Param>());
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

            return closest;
        }
    }
}
