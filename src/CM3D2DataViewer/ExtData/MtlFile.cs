using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CM3D2DataViewer
{
    public class MtlFile
    {
        public string                   FileName                { get; private set; }
        public List<MtlMaterial>        Materials               { get; private set; }
        public MtlMaterial              CurrentMaterial         { get; private set; }

        public MtlFile()
        {
            Materials   = new List<MtlMaterial>();
        }

        public MtlMaterial Get(string name)
        {
            return Materials.FirstOrDefault(i => i.Name == name);
        }

        public static MtlFile FromFile(string file)
        {
            using(var r= new StreamReader(file, Encoding.Default))
                return FromReader(r);
        }

        public static MtlFile FromReader(StreamReader r)
        {
            var mtl = new MtlFile();
            mtl.Load(r);
            return mtl;
        }

        private void Load(StreamReader r)
        {
            if(r.BaseStream is FileStream)
                FileName= ((FileStream)r.BaseStream).Name;

            var delim   = " \t".ToArray();

            for(;;)
            {
                var line= r.ReadLine();

                if(null == line)
                    break;

                line    = line.Trim();

                if(line.Length == 0)
                    continue;

                if(line[0] == '#')
                    continue;

                LineReaded(line.Split(delim, StringSplitOptions.RemoveEmptyEntries));
            }
        }

        private void LineReaded(string[] t)
        {
            switch(t[0].ToLower())
            {
            case "newmtl":  NewMaterial(t[1]);                                  break;  // newmtl mat0
            case "ns":      GetCurrentMaterial().Ns     = float.Parse(t[1]);    break;  // Ns 0.0000
            case "ni":      GetCurrentMaterial().Ni     = float.Parse(t[1]);    break;  // Ni 1.5000
            case "d":       GetCurrentMaterial().D      = float.Parse(t[1]);    break;  // d 1.0000
            case "tr":      GetCurrentMaterial().Tr     = float.Parse(t[1]);    break;  // Tr 0.0000
            case "tf":      GetCurrentMaterial().Tf     = ParseVector3(t);      break;  // Tf 1.0000 1.0000 1.0000 
            case "illum":   GetCurrentMaterial().Illum  = float.Parse(t[1]);    break;  // illum 2
            case "ka":      GetCurrentMaterial().Ka     = ParseVector3(t);      break;  // Ka 0.0625 0.0625 0.0625
            case "kd":      GetCurrentMaterial().Kd     = ParseVector3(t);      break;  // Kd 0.0980 0.0980 0.0980
            case "ks":      GetCurrentMaterial().Ks     = ParseVector3(t);      break;  // Ks 0.0500 0.0500 0.0500
            case "ke":      GetCurrentMaterial().Ke     = ParseVector3(t);      break;  // Ke 0.0000 0.0000 0.0000
            case "map_ka":  GetCurrentMaterial().MapKa  = t[1];                 break;  // map_Ka C:\a.dds
            case "map_kd":  GetCurrentMaterial().MapKd  = t[1];                 break;  // map_Kd C:\b.dds
            case "map_ks":  GetCurrentMaterial().MapKs  = t[1];                 break;
            case "map_ke":  GetCurrentMaterial().MapKe  = t[1];                 break;
            default:    throw new FormatException();
            }
        }

        private Vector3 ParseVector3(string[] t)
        {
            try
            {
                return new Vector3(float.Parse(t[1]), float.Parse(t[2]), float.Parse(t[3]));
            } catch
            {
                return new Vector3(0, 0, 0);
            }
        }

        private MtlMaterial GetCurrentMaterial()
        {
            return null == CurrentMaterial ? NewMaterial("noname") : CurrentMaterial;
        }

        private MtlMaterial NewMaterial(string name)
        {
            Materials.Add(CurrentMaterial = new MtlMaterial(this, Materials.Count) { Name= name });
            return CurrentMaterial;
        }
    }

    public class MtlMaterial
    {
        public MtlFile                  Owner                   { get; private set; }
        public string                   Name                    { get; set; }
        public int                      Index                   { get; set; }
        public float                    Ns                      { get; set; }   // Ns 0.0000
        public float                    Ni                      { get; set; }   // Ni 1.5000
        public float                    D                       { get; set; }   // d 1.0000
        public float                    Tr                      { get; set; }   // Tr 0.0000
        public Vector3                  Tf                      { get; set; }   // Tf 1.0000 1.0000 1.0000 
        public float                    Illum                   { get; set; }   // illum 2
        public Vector3                  Ka                      { get; set; }   // Ka 0.0625 0.0625 0.0625
        public Vector3                  Kd                      { get; set; }   // Kd 0.0980 0.0980 0.0980
        public Vector3                  Ks                      { get; set; }   // Ks 0.0500 0.0500 0.0500
        public Vector3                  Ke                      { get; set; }   // Ke 0.0000 0.0000 0.0000
        public string                   MapKa                   { get; set; }   // map_Ka C:\a.dds
        public string                   MapKd                   { get; set; }   // map_Kd C:\b.dds
        public string                   MapKs                   { get; set; }
        public string                   MapKe                   { get; set; }
         
        public MtlMaterial(MtlFile owner, int index)
        {
            Owner       = owner;
            Index       = index;
        }
    }
}
