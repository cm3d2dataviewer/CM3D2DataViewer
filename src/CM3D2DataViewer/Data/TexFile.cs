using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace CM3D2DataViewer
{
    public class TexSummary : BaseFile
    {
        public string                   AssetPath               { get; set; }

        public static TexSummary FromFile(string file)
        {
            var data= new TexSummary() { FileName= file };

            using(var s= File.OpenRead(file))
            using(var r = new BinaryReader(s))
            {
                data.Magic          = ReadString(r);
                data.Version        = r.ReadInt32();
                data.AssetPath      = ReadString(r);
            }

            return data;
        }
    }

    public class TexFile : BaseFile
    {
        public string                   AssetPath               { get; set; }
        public int                      DataSize                { get; set; }
        public byte[]                   ImageData               { get; set; }

        public Image GetImage()
        {
            using(var s = new MemoryStream(ImageData))
                return Image.FromStream(s);
        }

        public void SetImageData(byte[] data)
        {
            DataSize    = data.Length;
            ImageData   = data;
        }

        public static TexFile FromFile(string file)
        {
            var data= new TexFile() { FileName= file };

            using(var s= File.OpenRead(file))
            using(var r = new BinaryReader(s))
            {
                data.Magic          = ReadString(r);
                data.Version        = r.ReadInt32();
                data.AssetPath      = ReadString(r);
                data.DataSize       = r.ReadInt32();
                data.ImageData      = r.ReadBytes(data.DataSize);
            }

            return data;
        }

        public static void ToFile(string file, TexFile data)
        {
            using(var s= File.OpenWrite(file))
            using(var w= new BinaryWriter(s))
            {
                WriteString(w, data.Magic);
                w.Write(data.Version);
                WriteString(w, data.AssetPath);
                w.Write(data.DataSize);
                w.Write(data.ImageData);
            }
        }
    }
}
