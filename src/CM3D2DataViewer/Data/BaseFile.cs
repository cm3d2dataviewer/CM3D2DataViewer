using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CM3D2DataViewer
{
    public class BaseFile
    {
        public string                   FileName                { get; set; }
        public string                   Magic                   { get; set; }
        public int                      Version                 { get; set; }   // ?
        public List<string>             Descriptions            { get; set; }

        public string GetBackupFileName(string ext = null)
        {
            if(ext == null)
            {
                ext = Path.GetExtension(FileName);
                ext = ".@" + ext.Substring(1);
            }

            return Path.ChangeExtension(FileName, ext);
        }

        public string Backup(string ext = null)
        {
            var bak = GetBackupFileName(ext);

            #if false // 拡張子が違ってもゲーム中に読み込まれてしまうので、バックアップは作成しない
            if(!File.Exists(bak))
                File.Copy(FileName, bak);
            #endif

            return bak;
        }

        public static string ReadString(BinaryReader r)
        {
            int len = r.ReadByte();

            if(len >= 0x80)
                len +=(r.ReadByte() << 7) - 0x80;

            var buf = r.ReadBytes(len);

            return Encoding.UTF8.GetString(buf);
        }

        public static void WriteString(BinaryWriter w, string s)
        {
            var b   = Encoding.UTF8.GetBytes(s);

            if(b.Length > 127)
            {
              //w.Write((byte)(128 | (b.Length >> 7)));
              //w.Write((byte)(b.Length & 127));
                w.Write((byte)(128 | (b.Length & 127)));
                w.Write((byte)(b.Length >> 7));
                w.Write(b);
            } else
            {
                w.Write((byte)b.Length);
                w.Write(b);
            }
        }

        public static float[] ReadSingleArray(BinaryReader r, int n)
        {
            var a   = new float[n];

            for(int i= 0; i < n; ++i)
                a[i]    = r.ReadSingle();

            return a;
        }

        public static T[] ReadArray<T>(BinaryReader r, int n, Func<T> func)
        {
            var a   = new T[n];

            for(int i= 0; i < n; ++i)
                a[i]    = func();

            return a;
        }

        public static T[] ReadArray<T>(BinaryReader r, int n, Func<BinaryReader, T> func)
        {
            var a   = new T[n];

            for(int i= 0; i < n; ++i)
                a[i]    = func(r);

            return a;
        }

        public static List<T> ReadList<T>(BinaryReader r, int n, Func<T> func)
        {
            var l   = new List<T>(n);

            for(int i= 0; i < n; ++i)
                l.Add(func());

            return l;
        }

        public static List<T> ReadList<T>(BinaryReader r, int n, Func<BinaryReader, T> func)
        {
            var l   = new List<T>(n);

            for(int i= 0; i < n; ++i)
                l.Add(func(r));

            return l;
        }

        public static void Write<T>(BinaryWriter w, Action<T> func, IEnumerable<T> values)
        {
            foreach(var i in values)
                func(i);
        }

        public static void Write<T>(BinaryWriter w, Action<BinaryWriter, T> func, IEnumerable<T> values)
        {
            foreach(var i in values)
                func(w, i);
        }

        public static List<Param> ReadParamList(BinaryReader r)
        {
            var l   = new List<Param>();

            while(r.BaseStream.Position < r.BaseStream.Length)
            {
                var param   = ReadParam(r);

                l.Add(param);

                if(param is ParamEnd)
                    break;
            }

            return l;
        }

        public static Param ReadParam(BinaryReader r)
        {
            var type    = ReadString(r);

            try
            {
                switch(type)
                {
                case "tex":     return ReadParamTex(r);
                case "col":     return ReadParamCol(r);
                case "f":       return ReadParamF(r);
                case "morph":   return ReadParamMorph(r);
                case "end":     return new ParamEnd();
                default:    throw new FormatException();
                }
            } catch(EndOfStreamException ex)
            {
                System.Diagnostics.Debug.Print(ex.ToString());
                throw;
            }
        }

        public static ParamTex ReadParamTex(BinaryReader r)
        {
            var data        = new ParamTex();
            data.Name       = ReadString(r);
            data.SubType    = ReadString(r);

            if(data.SubType == "null")
                return data;

            data.TexName    = ReadString(r);
            data.TexAsset   = ReadString(r);
            data.R          = r.ReadSingle();
            data.G          = r.ReadSingle();
            data.B          = r.ReadSingle();
            data.A          = r.ReadSingle();

            return data;
        }

        public static ParamCol ReadParamCol(BinaryReader r)
        {
            var data        = new ParamCol();
            data.Name       = ReadString(r);
            data.R          = r.ReadSingle();
            data.G          = r.ReadSingle();
            data.B          = r.ReadSingle();
            data.A          = r.ReadSingle();

            return data;
        }

        public static ParamF ReadParamF(BinaryReader r)
        {
            var data        = new ParamF();
            data.Name       = ReadString(r);
            data.Value      = r.ReadSingle();

            return data;
        }

        public static ParamMorph ReadParamMorph(BinaryReader r)
        {
            var data        = new ParamMorph();
            data.Name       = ReadString(r);
            data.NumVertices= r.ReadInt32();
            data.Vertices   = ReadList(r, data.NumVertices, ReadMorphVertex);

            return data;
        }

        public static MorphVertex ReadMorphVertex(BinaryReader r)
        {
            var data    = new MorphVertex();
            data.Index  = r.ReadUInt16();
            data.X      = r.ReadSingle();
            data.Y      = r.ReadSingle();
            data.Z      = r.ReadSingle();
            data.NX     = r.ReadSingle();
            data.NY     = r.ReadSingle();
            data.NZ     = r.ReadSingle();

            return data;
        }

        public static void WriteParamList(BinaryWriter w, List<Param> data)
        {
            foreach(var i in data)
                WriteParam(w, i);
        }

        public static void WriteParam(BinaryWriter w, Param data)
        {
            WriteString(w, data.Type);

            try
            {
                switch(data.Type)
                {
                case "tex":     WriteParamTex(w, (ParamTex)data);       break;
                case "col":     WriteParamCol(w, (ParamCol)data);       break;
                case "f":       WriteParamF(w, (ParamF)data);           break;
                case "morph":   WriteParamMorph(w, (ParamMorph)data);   break;
                case "end":     break;
                default:    throw new FormatException();
                }
            } catch(EndOfStreamException ex)
            {
                System.Diagnostics.Debug.Print(ex.ToString());
                throw;
            }
        }

        public static void WriteParamTex(BinaryWriter w, ParamTex data)
        {
            WriteString(w, data.Name);
            WriteString(w, data.SubType);

            if(data.TexName == null)
            {
                WriteString(w, "null");
                return;
            }

            WriteString(w, data.TexName);
            WriteString(w, data.TexAsset);
            w.Write(data.R);
            w.Write(data.G);
            w.Write(data.B);
            w.Write(data.A);
        }

        public static void WriteParamCol(BinaryWriter w, ParamCol data)
        {
            WriteString(w, data.Name);
            w.Write(data.R);
            w.Write(data.G);
            w.Write(data.B);
            w.Write(data.A);
        }

        public static void WriteParamF(BinaryWriter w, ParamF data)
        {
            WriteString(w, data.Name);
            w.Write(data.Value);
        }

        public static void WriteParamMorph(BinaryWriter w, ParamMorph data)
        {
            WriteString(w, data.Name);
            w.Write(data.NumVertices);
            Write(w, WriteMorphVertex, data.Vertices);
        }

        public static void WriteMorphVertex(BinaryWriter w, MorphVertex data)
        {
            w.Write(data.Index);
            w.Write(data.X);
            w.Write(data.Y);
            w.Write(data.Z);
            w.Write(data.NX);
            w.Write(data.NY);
            w.Write(data.NZ);
        }
    }
}
