using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CM3D2DataViewer
{
    public class MqoExporter : Exporter
    {
        public const string FF  = "F10";

        public MqoExporter(ExportSettings settings)
            : base(settings)
        {
        }

        public override void Export(string filename, ModelFile model)
        {
            var dir     = Path.GetDirectoryName(filename);
            var index   = filename.IndexOf("\\model\\");
            var basedir = filename.Substring(0, index);
            var texdir  = Path.Combine(basedir, "texture");
            texdir      = texdir.Replace("model_", "texture_");

            using(var w = new StreamWriter(filename, false, Encoding.Default))
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
                w.WriteLine("Material {0} {{", model.Materials.Count);

                var sb  = new StringBuilder();

                foreach(var i in model.Materials)
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
                w.WriteLine("  vertex {0} {{", model.Mesh.NumVerts);

                foreach(var i in model.Mesh.Vertices)
                    //w.WriteLine("    {0:F4} {1:F4} {2:F4}", i.X, i.Y, i.Z);
                    w.WriteLine("    {0:F4} {1:F4} {2:F4}", -i.P.X, i.P.Z, -i.P.Y);

                w.WriteLine("  }");

                var nfaces  = model.Mesh.Primitives.Sum(i => i.NumIndices) / 3;

                w.WriteLine("  face {0} {{", nfaces / 3);

                for(var mat= 0; mat < model.Mesh.NumPrims; ++mat)
                {
                    var prim    = model.Mesh.Primitives[mat];

                    for(int i= 0, n= prim.NumIndices; i < n; i+=3)
                    {
                        var i1  = prim.Indices[i+0];
                        var i2  = prim.Indices[i+1];
                        var i3  = prim.Indices[i+2];
                        var v1  = model.Mesh.Vertices[i1];
                        var v2  = model.Mesh.Vertices[i2];
                        var v3  = model.Mesh.Vertices[i3];

                        w.WriteLine("    3 V({0} {1} {2}) M({3}) UV({4:F4} {5:F4} {6:F4} {7:F4} {8:F4} {9:F4})",
                            i1, i2, i3, mat, v1.T.X, 1-v1.T.Y, v2.T.X, 1-v2.T.Y, v3.T.X, 1-v3.T.Y);
                    }
                }

                w.WriteLine("  }");
                w.WriteLine("}");

                foreach(var i in model.Params.OfType<ParamMorph>())
                {
                    var v   = model.Mesh.Vertices.Select(j => new Vector3(j.P.X, j.P.Y, j.P.Z)).ToArray();

                    foreach(var j in i.Vertices)
                    {
                        v[j.Index].X    +=j.X;
                        v[j.Index].Y    +=j.Y;
                        v[j.Index].Z    +=j.Z;
                    }

                    w.WriteLine("");
                    w.WriteLine("Object \"elem:{0}-base\" {{", i.Name);
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
                    w.WriteLine("  vertex {0} {{", model.Mesh.NumVerts);

                    foreach(var j in v)
                        //w.WriteLine("    {0:F4} {1:F4} {2:F4}", i.X, i.Y, i.Z);
                        w.WriteLine("    {0:F4} {1:F4} {2:F4}", -j.X, j.Z, -j.Y);

                    w.WriteLine("  }");

                    nfaces  = model.Mesh.Primitives.Sum(j => j.NumIndices) / 3;

                    w.WriteLine("  face {0} {{", nfaces / 3);

                    for(var mat= 0; mat < model.Mesh.NumPrims; ++mat)
                    {
                        var prim    = model.Mesh.Primitives[mat];

                        for(int j= 0, n= prim.NumIndices; j < n; j+=3)
                        {
                            var i1  = prim.Indices[j+0];
                            var i2  = prim.Indices[j+1];
                            var i3  = prim.Indices[j+2];
                            var v1  = model.Mesh.Vertices[i1];
                            var v2  = model.Mesh.Vertices[i2];
                            var v3  = model.Mesh.Vertices[i3];

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
