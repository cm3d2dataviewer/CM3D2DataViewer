using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CM3D2DataViewer
{
    public class ObjExporter : Exporter
    {
        public const string FF  = "F10";

        public ObjExporter(ExportSettings settings)
            : base(settings)
        {
        }

        public override void Export(string filename, ModelFile model)
        {
            var mtlfile = Path.ChangeExtension(filename, ".mtl");

            ExportMTL(mtlfile, model);
            ExportOBJ(filename, model);
        }

        private void ExportOBJ(string filename, ModelFile model)
        {
            var mtlfile = Path.ChangeExtension(filename, ".mtl");

            using(var w = new StreamWriter(filename, false, Encoding.Default))
            {
                int voffset = 1;

                w.WriteLine("mtllib {0}", Path.GetFileName(mtlfile));

                // ベースのモデル
                foreach(var i in model.Mesh.Vertices)
                    WriteObjV(w, i.P.X, i.P.Y, i.P.Z);

                foreach(var i in model.Mesh.Vertices)
                    WriteObjVN(w, i.P.X, i.P.Y, i.P.Z);

                foreach(var i in model.Mesh.Vertices)
                    WriteObjVT(w, i.T.X, i.T.Y);

                w.WriteLine("g {0}", "base");

                for(var mat= 0; mat < model.Mesh.NumPrims; ++mat)
                {
                    w.WriteLine("usemtl {0}", model.Materials[mat].Descriptions[0]);
                    //w.WriteLine("s {0}", mat);

                    var prim    = model.Mesh.Primitives[mat];

                    for(int i= 0, num= prim.NumIndices; i < num; i+=3)
                    {
                        var i1  = prim.Indices[i+0] + voffset;
                        var i2  = prim.Indices[i+1] + voffset;
                        var i3  = prim.Indices[i+2] + voffset;

                        w.WriteLine("f {0}/{0}/{0} {1}/{1}/{1} {2}/{2}/{2}", i1, i2, i3);
                    }
                }

                // モーフィング
                if(Settings.Morph)
                foreach(var morph in model.Params.OfType<ParamMorph>())
                {
                    voffset +=model.Mesh.Vertices.Count;
                    var v   = model.Mesh.Vertices.Select(j => new Vector3(j.P.X, j.P.Y, j.P.Z)).ToArray();
                    var n   = model.Mesh.Vertices.Select(j => new Vector3(j.N.X, j.N.Y, j.N.Z)).ToArray();

                    foreach(var i in morph.Vertices)
                    {
                        v[i.Index].X    +=i.X;
                        v[i.Index].Y    +=i.Y;
                        v[i.Index].Z    +=i.Z;
                    }

                    foreach(var i in morph.Vertices)
                    {
                        n[i.Index].X    +=i.NX;
                        n[i.Index].Y    +=i.NY;
                        n[i.Index].Z    +=i.NZ;
                    }

                    foreach(var i in v)
                        //WriteObjV(w, -i.X, i.Z, -i.Y);
                        WriteObjV(w, i.X, i.Y, i.Z);

                    foreach(var i in n)
                        //WriteObjVN(w, -i.X, i.Z, -i.Y);
                        WriteObjVN(w, i.X, i.Y, i.Z);

                    foreach(var i in model.Mesh.Vertices)
                        WriteObjVT(w, i.T.X, i.T.Y);

                    w.WriteLine("g morph_{0}", morph.Name);

                    for(var mat= 0; mat < model.Mesh.NumPrims; ++mat)
                    {
                        w.WriteLine("usemtl {0}", model.Materials[mat].Descriptions[0]);
                        //w.WriteLine("s {0}", mat);

                        var prim    = model.Mesh.Primitives[mat];

                        for(int i= 0, num= prim.NumIndices; i < num; i+=3)
                        {
                            var i1  = prim.Indices[i+0] + voffset;
                            var i2  = prim.Indices[i+1] + voffset;
                            var i3  = prim.Indices[i+2] + voffset;

                            w.WriteLine("f {0}/{0}/{0} {1}/{1}/{1} {2}/{2}/{2}", i1, i2, i3);
                        }
                    }
                }
            }
        }

        private void ExportMTL(string filename, ModelFile model)
        {
            var dir     = Path.GetDirectoryName(filename);

            using(var w = new StreamWriter(filename, false, Encoding.Default))
            {
                foreach(var i in model.Materials)
                {
                    var maintex = GetTexFile(i.GetAs<ParamTex>("_MainTex"));
                    var col     = i.GetAs<ParamCol>("_Color");
                    var shine   = i.GetAs<ParamF>("_Shininess");

                    if(null == col)
                        col = new ParamCol() { R= 1, G= 1, B= 1 };

                    w.WriteLine("newmtl {0}", i.Descriptions[0]);
                    w.WriteLine("	Ns {0:F8}", 10.0000f);
                    w.WriteLine("	Ni {0:F8}", 1.5000f);
                    w.WriteLine("	d {0:F8}", 1.0000f);
                    w.WriteLine("	Tr {0:F8}", 0.0000f);
                    w.WriteLine("	Tf {0:F8} {1:F8} {2:F8}", 1.0000f, 1.0000f, 1.0000f);
                    w.WriteLine("	illum {0}", 2);
                    w.WriteLine("	Ka {0:F8} {1:F8} {2:F8}", col.R, col.G, col.B);
                    w.WriteLine("	Kd {0:F8} {1:F8} {2:F8}", 0.50f, 0.50f, 0.50f);
                    w.WriteLine("	Ks {0:F8} {1:F8} {2:F8}", 0.00f, 0.00f, 0.00f);
                    w.WriteLine("	Ke {0:F8} {1:F8} {2:F8}", 0.00f, 0.00f, 0.00f);

                    if(null != maintex)
                    {
                        var texname = Path.GetFileName(maintex.AssetPath);

                        maintex.GetImage().Save(Path.Combine(dir, texname));

                      //w.WriteLine("	map_Ka {0}", texname);
                      //w.WriteLine("	map_Kd {0}", texname);
                        w.WriteLine("	map_Ka {0}", Path.Combine(dir, texname));
                        w.WriteLine("	map_Kd {0}", Path.Combine(dir, texname));
                    }
                }
            }
        }

        private TexFile GetTexFile(ParamTex tex)
        {
            if(null == tex)
                return null;

            var name    = Path.GetFileName(tex.TexAsset);
            name        = Path.ChangeExtension(name, ".tex");
            var texinfo = DataManager.Instance.FindItem(name);
            var texfile = TexFile.FromFile(((TexSummary)texinfo).FileName);

            return texfile;
        }

        private void WriteObjV(StreamWriter w, float x, float y, float z)
        {
            w.WriteLine("v {0:"+FF+"} {1:"+FF+"} {2:"+FF+"}", x, y, z);
        }

        private void WriteObjVN(StreamWriter w, float x, float y, float z)
        {
            w.WriteLine("vn {0:"+FF+"} {1:"+FF+"} {2:"+FF+"}", x, y, z);
        }

        private void WriteObjVT(StreamWriter w, float u, float v)
        {
            w.WriteLine("vt {0:"+FF+"} {1:"+FF+"} {2:"+FF+"}", u, v, 0.0f);
        }
    }
}
