using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace CM3D2DataViewer
{
    public class ModelSummary : BaseFile
    {
        public static ModelSummary FromFile(string file)
        {
            var data= new ModelSummary() { FileName= file };

            using(var s= File.OpenRead(file))
            using(var r = new BinaryReader(s))
            {
                data.Magic      = ReadString(r);
                data.Version    = r.ReadInt32();
                data.Descriptions= ReadList(r, 2, ReadString);
            }

            return data;
        }
    }

    public partial class ModelFile : BaseFile
    {
        public int                      DataSize                { get; set; }
        public int                      NumBones                { get; set; }
        public List<ModelBone>          Bones                   { get; set; }
        public ModelMesh                Mesh                    { get; set; }
        public int                      NumMaterials            { get; set; }
        public List<ModelMaterial>      Materials               { get; set; }
        public List<Param>              Params                  { get; set; }
    }

    public class ModelBone
    {
        public string                   Name                    { get; set; }
        public byte                     Unknown                 { get; set; }
        public int                      ParentID                { get; set; }
        public float[]                  Params                  { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }

    public class ModelMesh
    {
        public int                      NumVerts                { get; set; }
        public int                      NumPrims                { get; set; }   // 1
        public int                      NumRefBones             { get; set; }
        public List<ModelRefBone>       RefBones                { get; set; }
        public List<ModelVertex>        Vertices                { get; set; }
        public int                      NumTangents             { get; set; }
        public List<Vector4>            Tangents                { get; set; }
        public List<ModelSkin>          Skins                   { get; set; }
        public List<ModelPrimitive>     Primitives              { get; set; }
    }

    public class ModelRefBone
    {
        public string                   Name                    { get; set; }
        public float[]                  Matrix                  { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }

    public struct ModelVertex
    {
        public Vector3                  P;
        public Vector3                  N;
        public Vector2                  T;

        public ModelVertex(Vector3 p, Vector3 n, Vector3 t)
        {
            P   = p;
            N   = n;
            T   = new Vector2(t.X, t.Y);
        }

        public ModelVertex(Vector3 p, Vector3 n, Vector2 t)
        {
            P   = p;
            N   = n;
            T   = t;
        }

        public bool Equals(ModelVertex obj)
        {
            return P == obj.P
                && N == obj.N
                && T == obj.T;
        }

        public override bool Equals(object obj)
        {
            return obj is ModelVertex
                && Equals((ModelVertex)obj);
        }

        public override int GetHashCode()
        {
            return P.GetHashCode()
                 ^ N.GetHashCode()
                 ^ T.GetHashCode();
        }
    }

    public class ModelSkin
    {
        public short                    B1, B2, B3, B4;
        public float                    W1, W2, W3, W4;
    }

    public class ModelMaterial
    {
        public List<string>             Descriptions            { get; set; }
        public List<Param>              Params                  { get; set; }

        public T GetAs<T>(string name) where T : Param
        {
            return (T)Params.FirstOrDefault(i => i.Name == name);
        }
    }

    public class ModelPrimitive
    {
        public int                      NumIndices              { get; set; }
        public List<ushort>             Indices                 { get; set; }
    }

    #if true
    public struct Vector4
    {
        public static Vector4           Zero    = new Vector4(0, 0, 0, 0);

        public float                    X, Y, Z, W;

        public Vector4(float x, float y, float z, float w)
        {
            X   = x;
            Y   = y;
            Z   = z;
            W   = w;
        }

        public bool Equals(Vector4 obj)
        {
            return X == obj.X
                && Y == obj.Y
                && Z == obj.Z
                && W == obj.W;
        }

        public override bool Equals(object obj)
        {
            return obj is Vector4 && Equals((Vector4)obj);
        }

        public override int GetHashCode()
        {
            return (X+Y+Z+W).GetHashCode()
                 ^ X.GetHashCode()
                 ^ Y.GetHashCode()
                 ^ Z.GetHashCode()
                 ^ W.GetHashCode();
        }

        public static bool operator==(Vector4 a, Vector4 b)
        {
            return a.Equals(b);
        }

        public static bool operator!=(Vector4 a, Vector4 b)
        {
            return !a.Equals(b);
        }
    }

    public struct Vector3
    {
        public static Vector3           Zero    = new Vector3(0, 0, 0);

        public float                    X, Y, Z;

        public Vector3(float x, float y, float z)
        {
            X   = x;
            Y   = y;
            Z   = z;
        }

        public bool Equals(Vector3 obj)
        {
            return X == obj.X
                && Y == obj.Y
                && Z == obj.Z;
        }

        public override bool Equals(object obj)
        {
            return obj is Vector3 && Equals((Vector3)obj);
        }

        public override int GetHashCode()
        {
            return (X+Y+Z).GetHashCode()
                 ^ X.GetHashCode()
                 ^ Y.GetHashCode()
                 ^ Z.GetHashCode();
        }

        public static bool operator==(Vector3 a, Vector3 b)
        {
            return a.Equals(b);
        }

        public static bool operator!=(Vector3 a, Vector3 b)
        {
            return !a.Equals(b);
        }
    }

    public struct Vector2
    {
        public static Vector2           Zero    = new Vector2(0, 0);

        public float                    X, Y;

        public Vector2(float x, float y)
        {
            X   = x;
            Y   = y;
        }

        public bool Equals(Vector2 obj)
        {
            return X == obj.X
                && Y == obj.Y;
        }

        public override bool Equals(object obj)
        {
            return obj is Vector2 && Equals((Vector2)obj);
        }

        public override int GetHashCode()
        {
            return (X+Y).GetHashCode()
                 ^ X.GetHashCode()
                 ^ Y.GetHashCode();
        }

        public static bool operator==(Vector2 a, Vector2 b)
        {
            return a.Equals(b);
        }

        public static bool operator!=(Vector2 a, Vector2 b)
        {
            return !a.Equals(b);
        }
    }
    #endif
}
