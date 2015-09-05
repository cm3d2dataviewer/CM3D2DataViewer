using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CM3D2DataViewer
{
    public class Param
    {
        public string                   Type                    { get; set; }
        public string                   Name                    { get; set; }
    }

    public class ParamEnd : Param
    {
        public ParamEnd() { Type= "end"; }
    }

    public class ParamTex : Param
    {
        public string                   SubType                 { get; set; }
        public string                   TexName                 { get; set; }
        public string                   TexAsset                { get; set; }
        public float                    R                       { get; set; }
        public float                    G                       { get; set; }
        public float                    B                       { get; set; }
        public float                    A                       { get; set; }
        public string                   TexFileName             { get { return Path.ChangeExtension(Path.GetFileName(TexAsset), ".tex"); } }

        public ParamTex() { Type= "tex"; }
    }

    public class ParamCol : Param
    {
        public float                    R                       { get; set; }
        public float                    G                       { get; set; }
        public float                    B                       { get; set; }
        public float                    A                       { get; set; }

        public ParamCol() { Type= "col"; }
    }

    public class ParamF : Param
    {
        public float                    Value                   { get; set; }

        public ParamF() { Type= "f"; }
    }

    public class ParamMorph : Param
    {
        public int                      NumVertices             { get; set; }
        public List<MorphVertex>        Vertices                { get; set; }

        public ParamMorph() { Type= "morph"; }
    }

    public class MorphVertex
    {
        public ushort                   Index                   { get; set; }
        public float                    X, Y, Z;
        public float                    NX, NY, NZ;

        public MorphVertex()
        {
        }

        public MorphVertex(int index, float px, float py, float pz, float nx, float ny, float nz)
        {
            Index   = (ushort)index;
            X       = px;
            Y       = py;
            Z       = pz;
            NX      = nx;
            NY      = ny;
            NZ      = nz;
        }

        public MorphVertex(int index, Vector3 p, Vector3 n)
        {
            Index   = (ushort)index;
            X       = p.X;
            Y       = p.Y;
            Z       = p.Z;
            NX      = n.X;
            NY      = n.Y;
            NZ      = n.Z;
        }

        public override string ToString()
        {
            return string.Format("{0} {1:F4} {2:F4} {3:F4} {4:F4} {5:F4} {6:F4}",
                Index, X, Y, Z, NX, NY, NZ);
        }
    }
}
