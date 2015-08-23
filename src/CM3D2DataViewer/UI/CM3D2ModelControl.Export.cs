using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CM3D2DataViewer
{
    partial class CM3D2ModelControl
    {
        /*
        mtllib box.mtl


        vn 0.0000 -1.0000 -0.0000
        vn 0.0000 1.0000 -0.0000
        vn 0.0000 0.0000 1.0000
        vn 1.0000 0.0000 -0.0000
        vn 0.0000 0.0000 -1.0000
        vn -1.0000 0.0000 -0.0000
        # 6 vertex normals

        vt 1.0000 0.0000 0.0000
        vt 1.0000 1.0000 0.0000
        vt 0.0000 1.0000 0.0000
        vt 0.0000 0.0000 0.0000
        # 4 texture coords

        g Box001
        usemtl Material__37
        s 2
        f 1/1/1 2/2/1 3/3/1 4/4/1 
        usemtl Material__25
        s 4
        f 5/4/2 6/1/2 7/2/2 8/3/2 
        usemtl wire_154154229
        s 8
        f 1/4/3 4/1/3 6/2/3 5/3/3 
        s 16
        f 4/4/4 3/1/4 7/2/4 6/3/4 
        s 32
        f 3/4/5 2/1/5 8/2/5 7/3/5 
        s 64
        f 2/4/6 1/1/6 5/2/6 8/3/6 
        # 6 polygons


        */

        public const string FF  = "F10";

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

        private void ExportOBJMTL()
        {
            if(null == data)
                return;

            var file    = Path.ChangeExtension(data.FileName, ".mtl");
            var dir     = Path.GetDirectoryName(file);

            using(var w = new StreamWriter(file, false, Encoding.Default))
            {
                foreach(var i in data.Materials)
                {
                    var maintex = GetTexFile(i.GetAs<ParamTex>("_MainTex"));
                    var col     = i.GetAs<ParamCol>("_Color");
                    var shine   = i.GetAs<ParamF>("_Shininess");


                    if(null == col)
                        col = new ParamCol() { R= 1, G= 1, B= 1 };

                    /*
                    Description:
                      "Mizugi001"
                      "CM3D2/Toony_Lighted_Outline"
                      "CM3D2__Toony_Lighted_Outline"
                    Params:
                        _MainTex : texture
                            SubType:  tex2d
                            TexName:  Mizugi001
                            TexAsset: Assets/texture/texture/dress/mizugi/mizugi001/Mizugi001.png
                            Color:    0.0000, 0.0000, 1.0000, 1.0000
                        _ToonRamp : texture
                            SubType:  tex2d
                            TexName:  toonBlueA1
                            TexAsset: Assets/texture/texture/toon/toonBlueA1.png
                            Color:    0.0000, 0.0000, 1.0000, 1.0000
                        _ShadowTex : texture
                            SubType:  tex2d
                            TexName:  Mizugi001_shadow
                            TexAsset: Assets/texture/texture/dress/mizugi/mizugi001/Mizugi001_shadow.png
                            Color:    0.0000, 0.0000, 1.0000, 1.0000
                        _ShadowRateToon : texture
                            SubType:  tex2d
                            TexName:  toonDress_shadow
                            TexAsset: Assets/texture/texture/toon/toonDress_shadow.png
                            Color:    0.0000, 0.0000, 1.0000, 1.0000
                        _Color : color
                            Color:    1.0000, 1.0000, 1.0000, 1.0000
                        _ShadowColor : color
                            Color:    0.0574, 0.0000, 0.2868, 1.0000
                        _RimColor : color
                            Color:    0.1228, 0.1190, 0.2279, 0.0000
                        _OutlineColor : color
                            Color:    0.0256, 0.0221, 0.1250, 1.0000
                        _ShadowColor : color
                            Color:    0.0574, 0.0000, 0.2868, 1.0000
                        _Shininess : float
                            Value:    0.0000
                        _OutlineWidth : float
                            Value:    0.0016
                        _RimPower : float
                            Value:    23.0589
                        _RimShift : float
                            Value:    0.0536
                    */

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

        private void ExportOBJ()
        {
            if(null == data)
                return;

            var file    = Path.ChangeExtension(data.FileName, ".obj");
            var mtlfile = Path.ChangeExtension(data.FileName, ".mtl");

            ExportOBJMTL();

            using(var w = new StreamWriter(file, false, Encoding.Default))
            {
                int voffset = 1;

                w.WriteLine("mtllib {0}", Path.GetFileName(mtlfile));

                // ベースのモデル
                foreach(var i in data.Mesh.Vertices)
                    WriteObjV(w, -i.P.X, i.P.Z, -i.P.Y);

                foreach(var i in data.Mesh.Vertices)
                    WriteObjVN(w, -i.N.X, i.N.Z, -i.N.Y);

                foreach(var i in data.Mesh.Vertices)
                    WriteObjVT(w, i.T.X, i.T.Y);

                w.WriteLine("g {0}", "base");

                for(var mat= 0; mat < data.Mesh.NumPrims; ++mat)
                {
                    w.WriteLine("usemtl {0}", data.Materials[mat].Descriptions[0]);
                    //w.WriteLine("s {0}", mat);

                    var prim    = data.Mesh.Primitives[mat];

                    for(int i= 0, num= prim.NumIndices; i < num; i+=3)
                    {
                        var i1  = prim.Indices[i+0] + voffset;
                        var i2  = prim.Indices[i+1] + voffset;
                        var i3  = prim.Indices[i+2] + voffset;

                        w.WriteLine("f {0}/{0}/{0} {1}/{1}/{1} {2}/{2}/{2}", i1, i2, i3);
                    }
                }

                // モーフィング
                foreach(var morph in data.Params.OfType<ParamMorph>())
                {
                    voffset +=data.Mesh.Vertices.Count;
                    var v   = data.Mesh.Vertices.Select(j => new Vector3(j.P.X, j.P.Y, j.P.Z)).ToArray();
                    var n   = data.Mesh.Vertices.Select(j => new Vector3(j.N.X, j.N.Y, j.N.Z)).ToArray();

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
                        WriteObjV(w, -i.X, i.Z, -i.Y);

                    foreach(var i in n)
                        WriteObjVN(w, -i.X, i.Z, -i.Y);

                    foreach(var i in data.Mesh.Vertices)
                        WriteObjVT(w, i.T.X, i.T.Y);

                    w.WriteLine("g morph_{0}", morph.Name);

                    for(var mat= 0; mat < data.Mesh.NumPrims; ++mat)
                    {
                        w.WriteLine("usemtl {0}", data.Materials[mat].Descriptions[0]);
                        //w.WriteLine("s {0}", mat);

                        var prim    = data.Mesh.Primitives[mat];

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

        private void ExportDAE()
        {
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

        private void ExportMQO()
        {
            if(null == data)
                return;

            var file    = Path.ChangeExtension(data.FileName, ".mqo");
            var dir     = Path.GetDirectoryName(file);
            var index   = file.IndexOf("\\model\\");
            var basedir = file.Substring(0, index);
            var texdir  = Path.Combine(basedir, "texture");
            texdir      = texdir.Replace("model_", "texture_");

            using(var w = new StreamWriter(file, false, Encoding.Default))
            {
                w.WriteLine("Metasequoia Document");
                w.WriteLine("Format Text Ver 1.0");
              //w.WriteLine("");
              //w.WriteLine("IncludeXml \"test.mqx\"");
                w.WriteLine("");
                w.WriteLine("Scene {");
                w.WriteLine("	pos 0.0000 0.0000 8.0000");
                w.WriteLine("	lookat 0.0000 0.0000 0.0000");
                w.WriteLine("	head -0.5236");
                w.WriteLine("	pich 0.5236");
                w.WriteLine("	bank 0.0000");
                w.WriteLine("	ortho 0");
                w.WriteLine("	zoom2 500");
                w.WriteLine("	amb 0.250 0.250 0.250");
                w.WriteLine("	dirlights 1 {");
                w.WriteLine("		light {");
                w.WriteLine("			dir 0.408 0.408 0.816");
                w.WriteLine("			color 1.000 1.000 1.000");
                w.WriteLine("		}");
                w.WriteLine("	}");
                w.WriteLine("}");
                w.WriteLine("");
                w.WriteLine("Material {0} {{", data.Materials.Count);

                var sb  = new StringBuilder();

                foreach(var i in data.Materials)
                {
                    sb.Length   = 0;

                    var col     = i.GetAs<ParamCol>("_Color");
                    var tex     = i.GetAs<ParamTex>("_MainTex");
                    var texpath = "";
                    var pathtok = tex.TexAsset.Split('/', '\\');

                    for(int j= pathtok.Length - 2; j >= 0; --j)
                    {
                        texpath = Path.Combine(texdir, string.Join("\\", pathtok.Skip(j).ToArray()));
                        texpath = texpath.Replace(".png", ".tex");

                        if(File.Exists(texpath))
                            break;
                    } 

                    sb.AppendFormat("  \"{0}\"", i.Descriptions[0])
                      .AppendFormat(" shader({0})", 4)
                      .AppendFormat(" col({0:F4} {1:F4} {2:F4} {3:F4})", col.R, col.G, col.B, col.A)
                      .AppendFormat(" dif({0:F4})", 1.0f)
                      .AppendFormat(" amb({0:F4})", 0.5f)
                      .AppendFormat(" emi({0:F4})", 0.0f)
                      .AppendFormat(" spc({0:F4})", 0.0f)
                      .AppendFormat(" power({0:F4})", 0.0f);

                    if(File.Exists(texpath))
                    {
                        var texdata = TexFile.FromFile(texpath);
                        var image   = texdata.GetImage();
                        var name    = Path.GetFileName(texdata.AssetPath);

                        image.Save(Path.Combine(dir, name));

                      //sb.AppendFormat(" tex(\"{0}\")", texpath);
                        sb.AppendFormat(" tex(\"{0}\")", name);
                    }

                    w.WriteLine(sb.ToString());
                }

                w.WriteLine("}");
                w.WriteLine("");
                w.WriteLine("Object \"mesh\" {");
                w.WriteLine("  depth 1");
                w.WriteLine("  folding 0");
                w.WriteLine("  scale 1.00000 1.00000 -1.00000");
                w.WriteLine("  rotation 0.00000 0.00000 0.00000");
                w.WriteLine("  translation 10.00000 30.00000 -20.00000");
                w.WriteLine("  visible 15");
                w.WriteLine("  locking 1");
                w.WriteLine("  shading 1");
                w.WriteLine("  facet 45.0");
                w.WriteLine("  color 0.5 0.5 0.5");
                w.WriteLine("  color_type 0");
                w.WriteLine("  vertex {0} {{", data.Mesh.NumVerts);

                foreach(var i in data.Mesh.Vertices)
                  //w.WriteLine("    {0:F4} {1:F4} {2:F4}", i.X, i.Y, i.Z);
                    w.WriteLine("    {0:F4} {1:F4} {2:F4}", -i.P.X, i.P.Z, -i.P.Y);

                w.WriteLine("  }");

                var nfaces  = data.Mesh.Primitives.Sum(i => i.NumIndices) / 3;

                w.WriteLine("  face {0} {{", nfaces / 3);

                for(var mat= 0; mat < data.Mesh.NumPrims; ++mat)
                {
                    var prim    = data.Mesh.Primitives[mat];

                    for(int i= 0, n= prim.NumIndices; i < n; i+=3)
                    {
                        var i1  = prim.Indices[i+0];
                        var i2  = prim.Indices[i+1];
                        var i3  = prim.Indices[i+2];
                        var v1  = data.Mesh.Vertices[i1];
                        var v2  = data.Mesh.Vertices[i2];
                        var v3  = data.Mesh.Vertices[i3];

                        w.WriteLine("    3 V({0} {1} {2}) M({3}) UV({4:F4} {5:F4} {6:F4} {7:F4} {8:F4} {9:F4})",
                            i1, i2, i3, mat, v1.T.X, 1-v1.T.Y, v2.T.X, 1-v2.T.Y, v3.T.X, 1-v3.T.Y);
                    }
                }

                w.WriteLine("  }");
                w.WriteLine("}");

                foreach(var i in data.Params.OfType<ParamMorph>())
                {
                    var v   = data.Mesh.Vertices.Select(j => new Vector3(j.P.X, j.P.Y, j.P.Z)).ToArray();

                    foreach(var j in i.Vertices)
                    {
                        v[j.Index].X    +=j.X;
                        v[j.Index].Y    +=j.Y;
                        v[j.Index].Z    +=j.Z;
                    }

                    w.WriteLine("");
                    w.WriteLine("Object \"elem:{0}\" {{", i.Name);
                    w.WriteLine("  depth 1");
                    w.WriteLine("  folding 0");
                    w.WriteLine("  scale 1.00000 1.00000 -1.00000");
                    w.WriteLine("  rotation 0.00000 0.00000 0.00000");
                    w.WriteLine("  translation 10.00000 30.00000 -20.00000");
                    w.WriteLine("  visible 15");
                    w.WriteLine("  locking 1");
                    w.WriteLine("  shading 1");
                    w.WriteLine("  facet 45.0");
                    w.WriteLine("  color 0.5 0.5 0.5");
                    w.WriteLine("  color_type 0");
                    w.WriteLine("  vertex {0} {{", data.Mesh.NumVerts);

                    foreach(var j in v)
                      //w.WriteLine("    {0:F4} {1:F4} {2:F4}", i.X, i.Y, i.Z);
                        w.WriteLine("    {0:F4} {1:F4} {2:F4}", -j.X, j.Z, -j.Y);

                    w.WriteLine("  }");

                    nfaces  = data.Mesh.Primitives.Sum(j => j.NumIndices) / 3;

                    w.WriteLine("  face {0} {{", nfaces / 3);

                    for(var mat= 0; mat < data.Mesh.NumPrims; ++mat)
                    {
                        var prim    = data.Mesh.Primitives[mat];

                        for(int j= 0, n= prim.NumIndices; j < n; j+=3)
                        {
                            var i1  = prim.Indices[j+0];
                            var i2  = prim.Indices[j+1];
                            var i3  = prim.Indices[j+2];
                            var v1  = data.Mesh.Vertices[i1];
                            var v2  = data.Mesh.Vertices[i2];
                            var v3  = data.Mesh.Vertices[i3];

                            w.WriteLine("    3 V({0} {1} {2}) M({3}) UV({4:F4} {5:F4} {6:F4} {7:F4} {8:F4} {9:F4})",
                                i1, i2, i3, mat, v1.T.X, 1-v1.T.Y, v2.T.X, 1-v2.T.Y, v3.T.X, 1-v3.T.Y);
                        }
                    }

                    w.WriteLine("  }");
                    w.WriteLine("}");
                }

                w.WriteLine("Eof");
            }
        }
    }
}
