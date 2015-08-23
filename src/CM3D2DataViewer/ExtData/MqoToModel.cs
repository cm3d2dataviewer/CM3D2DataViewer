using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CM3D2DataViewer
{
    public class MqoMaterial
    {
        public string   Name;
        public int      Shader;
        public Color4   Col;
        public float    Dif;
        public float    Amb;
        public float    Emi;
        public float    Spc;
        public float    Power;
        public string   Tex;

        /*
        ppublic string 
  "Mizugi005_01" shader(4) col(1.0000 1.0000 1.0000 1.0000) dif(1.0000) amb(0.5000) emi(0.0000) spc(0.0000) power(0.0000) tex("mizugi005_01.png")
  "Mizugi005_02" shader(4) col(1.0000 1.0000 1.0000 1.0000) dif(1.0000) amb(0.5000) emi(0.0000) spc(0.0000) power(0.0000) tex("mizugi005_02.png")
  */
    }

    public struct Color3
    {
        public float    R, G, B, A;
    }

    public struct Color4
    {
        public float    R, G, B;
    }

    public class MqoObject
    {
        public List<Vector3>            Vertices    = new List<Vector3>();
        public List<MqoFace>            Faces       = new List<MqoFace>();
    }

    public class MqoFace
    {
        public int                      Count;
        public int                      I0, I1, I2, I3;
        public float                    U0, U1, U2, U3;
        public float                    V0, V1, V2, V3;
        public int                      Material;
    }

    public class MqoToModel
    {
        public static Regex             RE_V    = new Regex(@"V\([^)]+\)");
        public static Regex             RE_M    = new Regex(@"M\([^)]+\)");
        public static Regex             RE_UV   = new Regex(@"UV\([^)]+\)");

        public enum MQOReadMode
        {
            Root,
            Thumbnail,
            Scene,
            Object,
            Material,
            Vertex,
            Face,
            Unknown,
        }

        public MQOReadMode          CurrentMode;
        public MqoObject            CurrentObject;

        public Stack<MQOReadMode>   ModeStack   = new Stack<MQOReadMode>();
        public List<MqoObject>      Objects     = new List<MqoObject>();
        public List<MqoMaterial>    Materials   = new List<MqoMaterial>();

        private void OnObject(string line)
        {
            PushMode(MQOReadMode.Object);

            CurrentObject   = new MqoObject();

            Objects.Add(CurrentObject);
        }

        public void AddVertex(string line)
        {
        }

        public void AddFace(string line)
        {
        }

        public void Load(string mqofile)
        {
            using(var r= new StreamReader(mqofile))
            {
                CurrentMode = MQOReadMode.Root;
                var line    = "";

                while(null != (line= r.ReadLine()))
                {
                    line    = line.Trim();

                    if(line.EndsWith("{"))
                    {
                        switch(CurrentMode)
                        {
                        case MQOReadMode.Root:
                             if(line.ToUpper().StartsWith("THUMBNAIL"))
                                    PushMode(MQOReadMode.Thumbnail);
                             else   PushMode(MQOReadMode.Unknown);
                                 if(line.StartsWith("THUMBNAIL"))   PushMode(MQOReadMode.Vertex);
                            else if(line.StartsWith("SCENE"))       PushMode(MQOReadMode.Scene);
                            else if(line.StartsWith("MATERIAL"))    PushMode(MQOReadMode.Material);
                            else if(line.StartsWith("OBJECT"))      OnObject(line);
                            else                                    PushMode(MQOReadMode.Unknown);
                            break;

                        case MQOReadMode.Object:
                                 if(line.StartsWith("VERTEX"))      PushMode(MQOReadMode.Vertex);
                            else if(line.StartsWith("FACE"))        PushMode(MQOReadMode.Face);
                            else                                    PushMode(MQOReadMode.Unknown);
                            break;
                        default:
                            PushMode(MQOReadMode.Unknown);
                            break;
                        }
                    } else
                    if(line.StartsWith("}"))
                    {
                        PopMode();
                    } else
                    {
                        switch(CurrentMode)
                        {
                        case MQOReadMode.Object:
                        case MQOReadMode.Root:
                        case MQOReadMode.Material:
                        case MQOReadMode.Vertex:
                            
                        case MQOReadMode.Face:
                            break;
                        }
                    }
                }
            }
        }

        private void PushMode(MQOReadMode mode)
        {
            ModeStack.Push(CurrentMode);
            CurrentMode = mode;
        }

        private void PopMode()
        {
            CurrentMode = ModeStack.Pop();
        }
    }
}
