using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace CM3D2DataViewer
{
    public class MenuFile : BaseFile
    {
        public int                      DataSize                { get; set; }
        public List<List<String>>       Scripts                 { get; set; }
        public IEnumerable<string>      ScriptLines             { get { return MakeScriptLines(Scripts); } }
        public string                   ScriptText              { get { return MakeScriptText(Scripts); } }

        public override string ToString()
        {
            return string.Join("/", Descriptions.ToArray());
        }

        public List<string> GetStrings(string key)
        {
            return Scripts.FirstOrDefault(i => i[0] == key);
        }

        public static MenuFile FromFile(string file)
        {
            var data= new MenuFile() { FileName= file };

            using(var s= File.OpenRead(file))
            using(var r = new BinaryReader(s))
            {
                data.Magic      = ReadString(r);
                data.Version    = r.ReadInt32();
                data.Descriptions= ReadList(r, 4, ReadString);
                data.DataSize   = r.ReadInt32();
                data.Scripts    = new List<List<string>>();
                var end         = r.BaseStream.Position + data.DataSize;

                try
                {
                    while(r.BaseStream.Position < end)
                    {
                        var count   = r.ReadByte();
                        var strings = ReadList(r, count, ReadString);

                        data.Scripts.Add(strings);
                    }
                } catch(EndOfStreamException ex)
                {
                    System.Diagnostics.Debug.Print(file);
                    System.Diagnostics.Debug.Print(ex.ToString());
                }
            }

            return data;
        }

        public static void ToFile(string file, MenuFile data)
        {
            using(var s= File.OpenWrite(file))
            using(var w= new BinaryWriter(s))
            {
                WriteString(w, data.Magic);
                w.Write(data.Version);
                Write(w, WriteString, data.Descriptions);
                w.Flush();

                var pos = w.BaseStream.Position;

                w.Write(0);

                foreach(var i in data.Scripts)
                {
                    w.Write((byte)i.Count);
                    Write(w, WriteString, i);
                }

                w.Write((byte)0);
                w.Flush();

                var len = w.BaseStream.Position;

                w.BaseStream.Position   = pos;
                w.Write((int)(len - pos - 4));

                w.BaseStream.SetLength(len);
            }
        }

        public static IEnumerable<string> MakeScriptLines(List<List<string>> script)
        {
            return script.Select(i => string.Join(" ", i.ToArray()));
        }

        public static string MakeScriptText(List<List<string>> script)
        {
            return string.Join(Environment.NewLine, MakeScriptLines(script).ToArray());
        }
    }
}
