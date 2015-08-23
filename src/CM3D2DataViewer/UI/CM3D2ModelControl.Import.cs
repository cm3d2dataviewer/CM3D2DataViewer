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
        private void Verify(List<ModelVertex> v1, List<ushort> i1, List<ModelVertex> v2, List<ushort> i2)
        {

        }
        */
        private void ImportOBJ()
        {
            if(null == data)
                return;

            var file    = Path.ChangeExtension(data.FileName, ".obj");
            var obj     = ObjFile.FromFile(file);

            obj.Dump();

            var g       = obj.Groups.FirstOrDefault(i => i.Name == "base");
            var p       = obj.Positions;
            var n       = obj.Normals;
            var t       = obj.TexCoords;
            var remap   = new VertexRemapper();

            foreach(var i in g.Meshes)
            {
                remap.BeginPrimitive();

                for(int j= 0; j < i.FaceCount; ++j)
                {
                    var fp  = i.PositionFaces[j];
                    var fn  = i.NormalFaces[j];
                    var ft  = i.TexCoordFaces[j];

                    remap.AddVertex(p[fp.A], n[fn.A], t[ft.A]);
                    remap.AddVertex(p[fp.B], n[fn.B], t[ft.B]);
                    remap.AddVertex(p[fp.C], n[fn.C], t[ft.C]);
                }

                remap.EndPrimitive();
            }

            //System.Diagnostics.Debug.Assert(remap.Vertices.Count == remap.Vertices.Distinct().Count());

            // ベースメッシュ変更
            var mesh        = Data.Mesh;
            var oldverts    = mesh.Vertices;
            mesh.NumVerts   = remap.Vertices.Count;
            mesh.Vertices   = remap.Vertices;
            mesh.NumPrims   = remap.Primitives.Count;
            mesh.Primitives = remap.Primitives
                .Select(i => new ModelPrimitive() { NumIndices= i.Count, Indices= i })
                .ToList();
            var skins       = new List<ModelSkin>(mesh.NumVerts);

            foreach(var i in mesh.Vertices)
            {
                var index   = FindClosest(oldverts, new Vector3(i.P.X, i.P.Y, i.P.Z));

                skins.Add(mesh.Skins[index]);
            }

            mesh.Skins      = skins;

            // モーフィング
            var morphs  = new List<ParamMorph>();

            foreach(var i in obj.Groups.Where(i => i.Name != "base"))
            {
                var name    = i.Name.Replace("morph_", "");
                var morph   = new Dictionary<int, MorphVertex>();

                #if false
                for(int j= 0; j < mesh.Vertices.Count; ++j)
                    morph[j]    = new MorphVertex(j, Vector3.Zero, Vector3.Zero);
                #else
                var index   = 0;

                foreach(var j in i.Meshes)
                {
                    for(int k= 0; k < j.FaceCount; ++k)
                    {
                        var ni  = remap.FaceRemap[index++];
                        var v0  = mesh.Vertices[ni];
                        var v1  = obj.Positions[j.PositionFaces[k].A];
                        v1      = new Vector3(-v1.X, -v1.Z, v1.Y);

                      //if(v0.X != v1.X || v0.Y != v1.Y || v0.Z != v1.Z)
                        {
                            v1.X    -=v0.P.X;
                            v1.Y    -=v0.P.Y;
                            v1.Z    -=v0.P.Z;
                            var n1  = obj.Normals[j.NormalFaces[k].A];
                            n1      = new Vector3(-n1.X, -n1.Z, n1.Y);
                            n1.X    -=v0.N.X;
                            n1.Y    -=v0.N.Y;
                            n1.Z    -=v0.N.Z;
                            morph[ni]= new MorphVertex(ni, v1, n1);
                        }

                        ni      = remap.FaceRemap[index++];
                        v0      = mesh.Vertices[ni];
                        v1      = obj.Positions[j.PositionFaces[k].B];
                        v1      = new Vector3(-v1.X, -v1.Z, v1.Y);

                      //if(v0.X != v1.X || v0.Y != v1.Y || v0.Z != v1.Z)
                        {
                            v1.X    -=v0.P.X;
                            v1.Y    -=v0.P.Y;
                            v1.Z    -=v0.P.Z;
                            var n1  = obj.Normals[j.NormalFaces[k].B];
                            n1      = new Vector3(-n1.X, -n1.Z, n1.Y);
                            n1.X    -=v0.N.X;
                            n1.Y    -=v0.N.Y;
                            n1.Z    -=v0.N.Z;
                            morph[ni]= new MorphVertex(ni, v1, n1);
                        }

                        ni      = remap.FaceRemap[index++];
                        v0      = mesh.Vertices[ni];
                        v1      = obj.Positions[j.PositionFaces[k].C];
                        v1      = new Vector3(-v1.X, -v1.Z, v1.Y);

                      //if(v0.X != v1.X || v0.Y != v1.Y || v0.Z != v1.Z)
                        {
                            v1.X    -=v0.P.X;
                            v1.Y    -=v0.P.Y;
                            v1.Z    -=v0.P.Z;
                            var n1  = obj.Normals[j.NormalFaces[k].C];
                            n1      = new Vector3(-n1.X, -n1.Z, n1.Y);
                            n1.X    -=v0.N.X;
                            n1.Y    -=v0.N.Y;
                            n1.Z    -=v0.N.Z;
                            morph[ni]= new MorphVertex(ni, v1, n1);
                        }
                    }
                }

                System.Diagnostics.Debug.Assert(remap.FaceRemap.Count == index);
                #endif

                morphs.Add(new ParamMorph()
                {
                    Name        = name,
                    NumVertices = morph.Count,
                    Vertices    = morph.ToArray().OrderBy(j => j.Key).Select(j => j.Value).ToList(),
                });
            }

            Data.Params = Data.Params.Where(i => !(i is ParamMorph)).ToList();
            Data.Params.InsertRange(0, morphs.Cast<Param>());

            /*
            var bak     = Path.ChangeExtension(Data.FileName, "*.@model");

            if(!File.Exists(bak))
                File.Copy(Data.FileName, bak);
            */

            ModelFile.ToFile(Data.FileName, Data);
          //ModelFile.ToFile(Path.ChangeExtension(Data.FileName, ".model"), Data);
        }

        public int FindClosest(List<ModelVertex> verts, Vector3 pos)
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

        public class VertexRemapper
        {
          //public Dictionary<Tuple<Vector3, Vector3, Vector3>, int>
            public Dictionary<ModelVertex, int>
                                        VertexMap;
            public List<ModelVertex>    Vertices;
            public List<int>            FaceRemap;
            public List<ushort>         Indices;
            public List<List<ushort>>   Primitives;

            public VertexRemapper()
            {
              //VertexMap   = new Dictionary<Tuple<Vector3, Vector3, Vector3>, int>();
                VertexMap   = new Dictionary<ModelVertex, int>();
                Vertices    = new List<ModelVertex>();
                FaceRemap   = new List<int>();
                Indices     = new List<ushort>();
                Primitives  = new List<List<ushort>>();
            }

            public void BeginPrimitive()
            {
                Indices = new List<ushort>();
            }

            public void EndPrimitive()
            {
                Primitives.Add(Indices);
            }

            public void AddVertex(Vector3 p0, Vector3 n0, Vector3 t)
            {
                int index   = 0;
                var p   = new Vector3(-p0.X, -p0.Z, p0.Y);
                var n   = new Vector3(-n0.X, -n0.Z, n0.Y);
              //var v   = Tuple.Create(p, n, t);
                var v   = new ModelVertex(p, n, t);

                #if true
                if(!VertexMap.TryGetValue(v, out index))
              //if(true)
                {
                    index           = Vertices.Count;
                    VertexMap[v]    = index;

                  //VertexMap.Add(v, index);
                  //Vertices.Add(new ModelVertex(p, n, t));
                    Vertices.Add(v);
                }
                #else
              //index       = Vertices.IndexOf(v);

              //if(index < 0)
                {
                    index           = Vertices.Count;
                    Vertices.Add(v);
              /*} else
                {
                    var v2  = Vertices[index];

                    System.Diagnostics.Debug.Assert(v.Equals(v2));
              */}
                #endif

                Indices.Add((ushort)index);
                FaceRemap.Add(index);
            }
        }
    }
}
