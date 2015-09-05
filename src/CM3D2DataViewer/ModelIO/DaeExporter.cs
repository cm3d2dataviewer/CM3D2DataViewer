using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using Models.Collada;
using Models.Intermediate;

namespace CM3D2DataViewer
{
    // OpenCollada用
    public class DaeExporter : Exporter
    {
        public string                   FileName                { get; private set; }
        public string                   Directory               { get; private set; }

        public Root                     Root                    { get; private set; }
        public VisualScene              Scene                   { get; private set; }
        public Dictionary<string, Material> Materials           { get; private set; }
        public Dictionary<string, BitmapTexture> Bitmaps        { get; private set; }
        public List<VisualNode>         Bones                   { get; private set; }
        public MorphingDeclaraion       Morph                   { get; private set; }
        public SkinDeclaraion           Skin                    { get; private set; }

        public DaeExporter(ExportSettings settings)
            : base(settings)
        {
        }

        public override void Export(string filename, ModelFile model)
        {
            FileName    = filename;
            Directory   = Path.GetDirectoryName(filename);

            CreateRoot(model);

            // DAE作成
            var xws     = new XmlWriterSettings()
            {
                Indent          = true,
                IndentChars     = "\t",
                NewLineChars    = Environment.NewLine,
                CloseOutput     = true,
                NewLineHandling = NewLineHandling.None,
            };

            using(var w= XmlWriter.Create(filename, xws))
            {
                var settings    = new DaeIMExportSettings() { BoneRefType= RefType.SID };
                var dae         = new DaeIMWriter(w) { Settings= settings };

                dae.Write(Root);
            }
        }

        private void CreateRoot(ModelFile model)
        {
            Root        = new Root();
            Scene       = new VisualScene() { ID= "VisualScene-1", Name= "VisualScene-1" };
            Materials   = new Dictionary<string, Material>();
            Bitmaps     = new Dictionary<string, BitmapTexture>();
            Bones       = new List<VisualNode>();

            var no      = 0;

            // ボーン
            foreach(var i in model.Bones)
                CreateBone(i, no++);

            // マテリアル
            foreach(var i in model.Materials)
                CreateMaterial(i);

            // メッシュ
            CreateMesh(model);

            Root.Scenes.Add(Scene);
        }

        private void CreateMesh(ModelFile model)
        {
            var id      = "Mesh-"+model.Descriptions[0];
            var name    = model.Descriptions[1];
            var mesh    = new Mesh(id, name);
            var geom    = new Geometry("Geom-"+id, name);
            var node    = Bones.FirstOrDefault(i => i.Name == name);
            var pos     = new GeometryChannel("Pos-"+id, name, GeometrySemantic.Position, 0);
            var nrm     = new GeometryChannel("Nrm-"+id, name, GeometrySemantic.Normal,   0);
            var tex     = new GeometryChannel("Tex-"+id, name, GeometrySemantic.TexCoord, 1);
            var indices = new List<int>();
            var mats    = Materials.Values.ToArray();
            var no      = 0;

            foreach(var i in model.Mesh.Primitives)
            {
                var prim    = new TrianglePrimitive();
                prim.Start  = indices.Count;

                indices.AddRange(i.Indices.Select(j => (int)j));

                prim.Count  = indices.Count - prim.Start;
                prim.Material= mats[no++];

                mesh.Primitives.Add(prim);
            }

            pos.SetData(ToSlimDX(model.Mesh.Vertices.Select(i => i.P)).ToList());
            nrm.SetData(ToSlimDX(model.Mesh.Vertices.Select(i => i.N)).ToList());
            tex.SetData(ToSlimDX(model.Mesh.Vertices.Select(i => i.T)).ToList());
            var array   = indices.ToArray();

            pos.SetIndices(array);
            nrm.SetIndices(array);
            tex.SetIndices(array);

            mesh.Channels.Add(pos);
            mesh.Channels.Add(nrm);
            mesh.Channels.Add(tex);

            geom.SetData(mesh);

            Root.Instances.Add(geom);

            // モーフィング
            if(Settings.Morph)
            {
                Morph       = new MorphingDeclaraion() { ID= "Morph-"+id, Name= "Morph"+name, Method= MorphingMethod.Normalized };
                var morphs  = model.Params.OfType<ParamMorph>().ToArray();

                foreach(var i in morphs)
                {
                    var v   = ToSlimDX(model.Mesh.Vertices.Select(j => j.P)).ToArray();

                    foreach(var j in i.Vertices)
                    {
                        v[j.Index].X    +=j.X;
                        v[j.Index].Y    +=j.Y;
                        v[j.Index].Z    +=j.Z;
                    }

                    var mm  = new Mesh()            { ID= "MorphMesh-"   +i.Name, Name= i.Name };
                    var mg  = new Geometry()        { ID= "MorphGeom-"   +i.Name, Name= i.Name };
                    var mc  = new MorphingChannel() { ID= "MorphChannel-"+i.Name, Name= i.Name, Weight= 0.0f, Geometry= mg };
                    var c   = new GeometryChannel() { ID= "MorphPos-"    +i.Name, Name= i.Name, Semantic= GeometrySemantic.Position, Index= 0 };

                    Morph.Channels.Add(mc);
                    Root.Instances.Add(mg);
                    mg.SetData(mm);
                    mm.Channels.Add(c);
                    mm.Primitives.AddRange(mesh.Primitives);
                    c.SetData(v.ToList());
                    c.SetIndices(mesh.Channels[0].GetIndicesAsArray(mesh));
                }

                Root.Instances.Add(Morph);
            }

            // スキン
            if(Settings.Skin)
            {
                Skin        = new SkinDeclaraion() { ID= "Skin-"+id, Name= "Skin"+name, Bind= SlimDX.Matrix.Identity };
                var skinbones= model.Mesh.RefBones.Select(i => Bones.First(j => j.Name == i.Name)).ToList();
                var bonemats= model.Mesh.RefBones.Select(i =>
                    new SlimDX.Matrix() {
                        M11 = i.Matrix[ 0], M12 = i.Matrix[ 1], M13 = i.Matrix[ 2], M14 = i.Matrix[ 3],
                        M21 = i.Matrix[ 4], M22 = i.Matrix[ 5], M23 = i.Matrix[ 6], M24 = i.Matrix[ 7],
                        M31 = i.Matrix[ 8], M32 = i.Matrix[ 9], M33 = i.Matrix[10], M34 = i.Matrix[11],
                        M41 = i.Matrix[12], M42 = i.Matrix[13], M43 = i.Matrix[14], M44 = i.Matrix[15] }).ToList();

                Skin.Skeleton   = new Skeleton() { Node= Scene.Nodes.First() };
                var verts   = new List<SkinVertices>();
                var v       = new List<SkinVertex>();

                foreach(var i in model.Mesh.Skins)
                {
                    v.Clear();
                    
                    if(0 != i.W1)   v.Add(new SkinVertex(i.B1, i.W1));
                    if(0 != i.W2)   v.Add(new SkinVertex(i.B2, i.W2));
                    if(0 != i.W3)   v.Add(new SkinVertex(i.B3, i.W3));
                    if(0 != i.W4)   v.Add(new SkinVertex(i.B4, i.W4));

                    verts.Add(new SkinVertices(v));
                }

                Skin.Vertices    .AddRange(verts);
                Skin.Bones       .AddRange(skinbones.Cast<Node>());
                Skin.BindMatrices.AddRange(bonemats);

                Root.Instances.Add(Skin);
            }

            if(null == Skin)
            {
                if(null == Morph)
                {
                    node.Instance   = geom;
                } else
                {
                    Morph.Source    = geom;
                    node.Instance   = Morph;
                }
            } else
            {
                if(null == Morph)
                {
                    Skin.Source     = geom;
                    node.Instance   = Skin;
                } else
                {
                    Morph.Source    = geom;
                    Skin.Source     = Morph;
                    node.Instance   = Skin;
                }
            }
        }

        private void CreateBone(ModelBone i, int no)
        {
            var boneid  = i.Name.Replace(" ", "_");
            var bone    = new Bone()       { ID= "Bone-"+boneid, Name= i.Name, };
            var bonenode= new VisualNode() { ID= "Node-"+boneid, Name= i.Name, Instance= bone };
            var trn     = new SlimDX.Vector3(i.Params[0], i.Params[1], i.Params[2]);
            var rot     = new SlimDX.Quaternion(i.Params[3], i.Params[4], i.Params[5], -i.Params[6]);
            var mat     = SlimDX.Matrix.RotationQuaternion(rot)
                        * SlimDX.Matrix.Translation(trn);
                
            bonenode.SID            = "joint" + no;
            bonenode.LocalTransform = mat;

            Bones.Add(bonenode);
            Root.Instances.Add(bone);

            if(i.ParentID < 0)
            {
                bonenode.GlobalTransform= mat;

                Scene.Nodes.Add(bonenode);
            } else
            {
                var parent              = Bones[i.ParentID];
                bonenode.GlobalTransform= mat * parent.GlobalTransform;
                //bonenode.GlobalTransform= parent.GlobalTransform * mat;

                parent.Nodes.Add(bonenode);
            }
        }

        private Material CreateMaterial(ModelMaterial modelmtrl)
        {
            var shininess   = modelmtrl.Params.OfType<ParamF>  ().FirstOrDefault(i => i.Name == "_Shininess");
            var maintex     = modelmtrl.Params.OfType<ParamTex>().FirstOrDefault(i => i.Name == "_MainTex");
            var color       = modelmtrl.Params.OfType<ParamCol>().FirstOrDefault(i => i.Name == "_Color");
            var mtrl        = new Material();
            var id          = modelmtrl.Descriptions[0];
            var name        = modelmtrl.Descriptions[0];
            mtrl.ID         = "Material-"+id;
            mtrl.Name       = "Material-"+name;
            mtrl.Shader     = ShaderType.Phong;
            mtrl.Shininess   = null == shininess ? 1 : shininess.Value;
            mtrl.Transparency= 1;

            if(null != maintex)
            {
                var surf    = new TextureSurface("Surface-"+id+"-Diff", name+"-Diff");
                surf.Texture= CreateBitmapTexture(maintex);
                surf.TexCoord   = 1;

                mtrl.AddSurface(SurfaceSlot.Diffuse, surf);
            } else
            if(null != color)
            {
                var surf    = new ColorSurface("Surface-"+id+"-Diff", name+"-Diff");
                surf.Color3 = new SlimDX.Color3(color.R, color.G, color.B);

                mtrl.AddSurface(SurfaceSlot.Diffuse, surf);
            } else
            {
                var surf    = new ColorSurface("Surface-"+id+"-Diff", name+"-Diff");
                surf.Color3 = new SlimDX.Color3(1, 1, 1);

                mtrl.AddSurface(SurfaceSlot.Diffuse, surf);
            }

            mtrl.AddSurface(SurfaceSlot.Ambient,     new SlimDX.Color4(1.0f, 0.5f, 0.5f, 0.5f));
            mtrl.AddSurface(SurfaceSlot.Specular,    new SlimDX.Color4(1.0f, 0.0f, 0.0f, 0.0f));
            mtrl.AddSurface(SurfaceSlot.Reflective,  new SlimDX.Color4(1.0f, 0.0f, 0.0f, 0.0f));
            mtrl.AddSurface(SurfaceSlot.Transparent, new SlimDX.Color4(1.0f, 1.0f, 1.0f, 1.0f));

            Materials.Add(mtrl.ID, mtrl);
            Root.Instances.Add(mtrl);

            return mtrl;
        }

        private IEnumerable<SlimDX.Vector3> ToSlimDX(IEnumerable<Vector3> v)
        {
            return v.Select(i => new SlimDX.Vector3(i.X, i.Y, i.Z));
        //  return v.Select(i => new SlimDX.Vector3(i.X, i.Z, i.Y));
        }

        private IEnumerable<SlimDX.Vector2> ToSlimDX(IEnumerable<Vector2> v)
        {
            return v.Select(i => new SlimDX.Vector2(i.X, 1-i.Y));
        }

        private BitmapTexture CreateBitmapTexture(ParamTex tex)
        {
            if(null == tex)
                return null;

            BitmapTexture   bmptex;

            if(Bitmaps.TryGetValue(tex.TexAsset, out bmptex))
                return bmptex;

            var name    = Path.GetFileName(tex.TexAsset);
            var texname = Path.ChangeExtension(name, ".tex");
            var texinfo = DataManager.Instance.FindItem(texname) as TexSummary;

            if(null == texinfo)
                return null;

            var texdata = TexFile.FromFile(texinfo.FileName);
            var texfile = Path.Combine(Directory, name);

            File.WriteAllBytes(texfile, texdata.ImageData);

            bmptex      = new BitmapTexture();
            bmptex.ID   = "Bitmap-"+Path.GetFileNameWithoutExtension(name);
            bmptex.Name = "Bitmap-"+Path.GetFileNameWithoutExtension(name);
            bmptex.FileName= texfile;

            Root.Instances.Add(bmptex);
            Bitmaps.Add(tex.TexAsset, bmptex);

            return bmptex;
        }
    }
}
