using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CM3D2DataViewer
{
    public class MateFile : BaseFile
    {
        public List<Param>              Params                  { get; set; }

        public static MateFile FromFile(string file)
        {
            var data= new MateFile() { FileName= file };

            using(var s= File.OpenRead(file))
            using(var r = new BinaryReader(s))
            {
                data.Magic      = ReadString(r);
                data.Version    = r.ReadInt32();
                data.Descriptions= ReadList(r, 4, ReadString);
                data.Params     = ReadParamList(r);
            }

            return data;
        }

        public static void ToFile(string file, MateFile data)
        {
            using(var s= File.OpenWrite(file))
            using(var w = new BinaryWriter(s))
            {
                WriteString(w, data.Magic);
                w.Write(data.Version);
                Write(w, WriteString, data.Descriptions);
                WriteParamList(w, data.Params);
            }
        }
    }
}
