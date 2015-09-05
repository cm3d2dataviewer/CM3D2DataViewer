using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace CM3D2DataViewer
{
    using Root = XmlRootAttribute;
    using Elem = XmlElementAttribute;

    [Root("Config")]
    public class Config
    {
        public static Config            Instance                { get; private set; }
        public static string            FileName
            { get { return Path.Combine(Path.GetDirectoryName(typeof(Config).Assembly.Location), "config.xml"); } }

        [Elem("DataDir")]       public string DataDir           { get; set; }
        [Elem("CM3D2Tool")]     public string CM3D2Tool         { get; set; }
        [Elem("ReiPatcher")]    public string ReiPatcher        { get; set; }
        [Elem("RunArch")]       public string RunArch           { get; set; }
        [Elem("ModelType")]     public string ModelType         { get; set; }
        [Elem("ExportMorph")]   public bool   ExportMorph       { get; set; }
        [Elem("ExportSkin")]    public bool   ExportSkin        { get; set; }
        [Elem("ChangeShader")]  public bool   ChangeShader      { get; set; }
        [Elem("Shader")]        public string Shader            { get; set; }
        [Elem("MorphMin")]      public float  MorphMin          { get; set; }
        [Elem("MorphMax")]      public float  MorphMax          { get; set; }
        [Elem("ImportScale")]   public float  ImportScale       { get; set; }
        [Elem("ExportScale")]   public float  ExportScale       { get; set; }

        static Config()
        {
            try
            {
                Instance    = FromFile(FileName);
            } catch(FileNotFoundException)
            {
                Instance    = new Config();
            }
        }

        private Config()
        {
            RunArch     = "x86";
            ModelType   = "OBJ";
            Shader      = "Toony_Lighted_Trans";
            MorphMin    = 0.02f;
            MorphMax    = 0.10f;
            ImportScale = 1.00f;
            ExportScale = 1.00f;
        }

        public void Save()
        {
            ToFile(FileName, Instance);
        }

        private static Config FromFile(string file)
        {
            var xs  = new XmlSerializer(typeof(Config));

            using(var s = File.OpenRead(file))
                return (Config)xs.Deserialize(s);
        }

        private static void ToFile(string file, Config config)
        {
            var xs  = new XmlSerializer(typeof(Config));

            using(var s = File.OpenWrite(file))
            {
                xs.Serialize(s, config);
                s.SetLength(s.Position);
            }
        }
    }
}
