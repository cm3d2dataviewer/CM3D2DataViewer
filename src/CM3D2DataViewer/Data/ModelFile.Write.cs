using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace CM3D2DataViewer
{
    partial class ModelFile
    {
        #region ToFile
        public static void ToFile(string file, ModelFile data)
        {
            using(var s= File.OpenWrite(file))
            using(var w= new BinaryWriter(s))
            {
                WriteString(w, data.Magic);
                w.Write(data.Version);
                Write(w, WriteString, data.Descriptions);
                w.Write(data.NumBones);
                Write(w, WriteBone, data.Bones);
                Write(w, w.Write, data.Bones.Select(i => i.ParentID));
                Write(w, (v) => Write(w, w.Write, v), data.Bones.Select(i => i.Params));
                WriteMesh(w, data.Mesh);
                w.Write(data.NumMaterials);
                Write(w, WriteMaterial, data.Materials);
                WriteParamList(w, data.Params);

                w.Flush();
                s.SetLength(s.Position);
            }
        }

        private static void WriteBone(BinaryWriter w, ModelBone data)
        {
            WriteString(w, data.Name);
            w.Write(data.Unknown);
        }

        private static void WriteMesh(BinaryWriter w, ModelMesh data)
        {
            w.Write(data.NumVerts);
            w.Write(data.NumPrims);
            w.Write(data.NumRefBones);
            Write(w, WriteString, data.RefBones.Select(i => i.Name));
            Write(w, (v) => Write(w, w.Write, v), data.RefBones.Select(i => i.Matrix));
            Write(w, WriteVertex, data.Vertices);
            Write(w, WriteSkin,   data.Skins);
            w.Write(data.Unknown2);

            foreach(var i in data.Primitives)
            {
                w.Write(i.NumIndices);
                Write(w, w.Write, i.Indices);
            }
        }

        private static void WriteVertex(BinaryWriter w, ModelVertex data)
        {
            w.Write(data.P.X);
            w.Write(data.P.Y);
            w.Write(data.P.Z);
            w.Write(data.N.X);
            w.Write(data.N.Y);
            w.Write(data.N.Z);
            w.Write(data.T.X);
            w.Write(data.T.Y);
        }

        private static void WriteSkin(BinaryWriter w, ModelSkin data)
        {
            w.Write(data.U);
            w.Write(data.B1);
            w.Write(data.B2);
            w.Write(data.B3);
            w.Write(data.B4);
            w.Write(data.W1);
            w.Write(data.W2);
            w.Write(data.W3);
        }

        private static void WriteMaterial(BinaryWriter w, ModelMaterial data)
        {
            Write(w, WriteString, data.Descriptions);
            WriteParamList(w, data.Params);
        }
        #endregion
    }
}
