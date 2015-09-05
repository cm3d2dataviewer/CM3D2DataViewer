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
        #region FromFile
        public static ModelFile FromFile(string file)
        {
            var data= new ModelFile() { FileName= file };

            using(var s= File.OpenRead(file))
            using(var r = new BinaryReader(s))
            {
                data.Magic      = ReadString(r);
                data.Version    = r.ReadInt32();
                data.Descriptions= ReadList(r, 2, ReadString);
                data.NumBones   = r.ReadInt32();
                data.Bones      = ReadList(r, data.NumBones, ReadBone);

                foreach(var i in data.Bones)
                    i.ParentID  = r.ReadInt32();

                foreach(var i in data.Bones)
                    i.Params    = ReadSingleArray(r, 7);

                data.Mesh       = ReadMesh(r);
                data.NumMaterials= r.ReadInt32();

              //System.Diagnostics.Debug.Assert(data.NumMaterials == 1);

                data.Materials  = ReadList(r, data.NumMaterials, ReadMaterial);
                data.Params     = ReadParamList(r);
            }

            return data;
        }

        private static ModelBone ReadBone(BinaryReader r)
        {
            var data    = new ModelBone();
            data.Name   = ReadString(r);
            data.Unknown= r.ReadByte();

            return data;
        }

        private static ModelRefBone ReadRefBone(BinaryReader r)
        {
            var data    = new ModelRefBone();
            data.Name   = ReadString(r);

            return data;
        }

        private static ModelMesh ReadMesh(BinaryReader r)
        {
            var data    = new ModelMesh();
            data.NumVerts   = r.ReadInt32();
            data.NumPrims   = r.ReadInt32();
            data.NumRefBones= r.ReadInt32();
            data.RefBones  = ReadList(r, data.NumRefBones, ReadRefBone);

            foreach(var i in data.RefBones)
                i.Matrix    = ReadSingleArray(r, 16);

            data.Vertices   = ReadList(r, data.NumVerts, ReadVertex);
            data.NumTangents= r.ReadInt32();

            System.Diagnostics.Debug.Assert(data.NumTangents == 0);

            data.Tangents   = ReadList(r, data.NumTangents, ReadVector4);
            data.Skins      = ReadList(r, data.NumVerts, ReadSkin);

          //System.Diagnostics.Debug.Assert(data.Unknown2 == 0);

            data.Primitives = new List<ModelPrimitive>();

            for(int i= 0; i < data.NumPrims; ++i)
            {
                var prim    = new ModelPrimitive();

                prim.NumIndices = r.ReadInt32();
                prim.Indices    = ReadList(r, prim.NumIndices, r.ReadUInt16);

                data.Primitives.Add(prim);
            }

            return data;
        }

        private static ModelVertex ReadVertex(BinaryReader r)
        {
            var data    = new ModelVertex();
            data.P      = new Vector3(r.ReadSingle(), r.ReadSingle(), r.ReadSingle());
            data.N      = new Vector3(r.ReadSingle(), r.ReadSingle(), r.ReadSingle());
            data.T      = new Vector2(r.ReadSingle(), r.ReadSingle());

            return data;
        }

        private static ModelSkin ReadSkin(BinaryReader r)
        {
            var data    = new ModelSkin();
            data.B1     = r.ReadInt16();
            data.B2     = r.ReadInt16();
            data.B3     = r.ReadInt16();
            data.B4     = r.ReadInt16();
            data.W1     = r.ReadSingle();
            data.W2     = r.ReadSingle();
            data.W3     = r.ReadSingle();
            data.W4     = r.ReadSingle();

            return data;
        }

        private static ModelMaterial ReadMaterial(BinaryReader r)
        {
            var data    = new ModelMaterial();
            data.Descriptions   = ReadList(r, 3, ReadString);
            data.Params         = ReadParamList(r);

            return data;
        }
        #endregion
    }
}
