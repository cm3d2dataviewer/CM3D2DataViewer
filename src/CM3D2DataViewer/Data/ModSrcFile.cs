using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CM3D2DataViewer
{
    /*
    public class ModSrcSummary
    {
        public string                   FileName                { get; private set; }
    }
    */
    public class ModSrcFile
    {
        public string                   FileName                { get; private set; }
        public Dictionary<string, string> Descriptions          { get; private set; }
        public List<ModSlot>            Slots                   { get; private set; }
        public string                   TempDescriptions        { get; set; }
        public ModSlot                  CurrentSlot             { get { return Slots.Last(); } }
        public ModMaterial              CurrentMaterial         { get { return CurrentSlot.Materials.Last(); } }

        public bool                     Valid
        {
            get
            {
                return Descriptions.ContainsKey("出力バージョン")
                    && Descriptions.ContainsKey("基本アイテム")
                    && Descriptions.ContainsKey("アイテム名")
                    && Descriptions.ContainsKey("カテゴリ名")
                    && Descriptions.ContainsKey("説明")
                    && Descriptions.ContainsKey("アイコン");
            }
        }

        public ModSrcFile()
        {
            Descriptions= new Dictionary<string, string>();
            Slots       = new List<ModSlot>();
        }

        public static void ToFile(string file, ModSrcFile data)
        {
            var lines   = new List<string>();

            foreach(var i in data.Descriptions)
                lines.Add(string.Format("{0}\t{1}", i.Key, i.Value));

            lines.Add("アイテム変更");

            foreach(var i in data.Slots)
            {
                lines.Add(string.Format("\tスロット名\t{0}", i.Name));

                foreach(var j in i.Materials)
                {
                    lines.Add(string.Format("\t\tマテリアル番号\t{0}", j.No));

                    foreach(var k in j.Textures)
                    {
                        lines.Add(string.Format(
                            k.Enabled ? "\t\t\t{0}\t{1}\t{2}"
                                    : "//\t\t\t{0}\t{1}\t{2}",
                            k.Type, k.Name, k.Texture));
                    }

                    foreach(var k in j.Colors)
                    {
                        lines.Add(string.Format(
                            k.Enabled ? "\t\t\t{0}\t{1}\t{2}\t{3}\t{4}\t{5}"
                                    : "//\t\t\t{0}\t{1}\t{2}\t{3}\t{4}\t{5}",
                            k.Type, k.Name, k.R, k.G, k.B, k.A));
                    }

                    foreach(var k in j.Values)
                    {
                        lines.Add(string.Format(
                            k.Enabled ? "\t\t\t{0}\t{1}\t{2}"
                                    : "//\t\t\t{0}\t{1}\t{2}",
                            k.Type, k.Name, k.Value));
                    }
                }
            }

            File.WriteAllLines(data.FileName, lines.ToArray());
        }

        public static ModSrcFile FromFile(string file)
        {
            var data    = new ModSrcFile() { FileName= file };
            var lines   = File.ReadAllLines(file, Encoding.UTF8);
            var delim   = " \t".ToArray();

            foreach(var i in lines)
            {
                var line    = i.Trim();
                var enabled = true;

                if(line.StartsWith("//"))
                {
                    enabled = false;
                    line    = line.Substring(2).Trim();
                }

                var t       = line.Trim().Split(delim, StringSplitOptions.RemoveEmptyEntries);

                if(t.Length == 0)
                    continue;

                switch(t[0])
                {
                case "アイテム変更":
                    break;

                case "スロット名":
                    var slot= new ModSlot(data) { Name= t[1] };
                    data.Slots.Add(slot);
                    break;

                case "マテリアル番号":
                    var mtrl= new ModMaterial(data.CurrentSlot) { No= int.Parse(t[1]) };
                    data.CurrentSlot.Materials.Add(mtrl);
                    break;

                case "テクスチャ設定":
                    var tex = new ModTex()
                    {
                        Enabled = enabled, 
                        Name    = t[1],
                        Texture = t[2]
                    };
                    data.CurrentMaterial.Textures.Add(tex);
                    break;

                case "色設定":
                    var col = new ModCol()
                    {
                        Enabled = enabled,
                        Name    = t[1],
                        R       = int.Parse(t[2]),
                        G       = int.Parse(t[3]),
                        B       = int.Parse(t[4]),
                        A       = int.Parse(t[5])
                    };
                    data.CurrentMaterial.Colors.Add(col);
                    break;

                case "数値設定":
                    var val = new ModValue()
                    {
                        Enabled = enabled,
                        Name    = t[1],
                        Value   = decimal.Parse(t[2])
                    };
                    data.CurrentMaterial.Values.Add(val);
                    break;

                default:
                    if(t.Length >= 2)
                        data.Descriptions.Add(t[0], string.Join(" ", t.Skip(1).ToArray()));

                    break;
                }
            }

            return data;
        }
    }

    public class ModSlot
    {
        public ModSrcFile               Owner                   { get; private set; }
        public string                   Name                    { get; set; }
        public List<ModMaterial>        Materials               { get; private set; }

        public ModSlot(ModSrcFile owner)
        {
            Owner       = owner;
            Materials   = new List<ModMaterial>();
        }

        public override string ToString()
        {
            return Name;
        }
    }
    
    public class ModMaterial
    {
        public ModSlot                  Owner                   { get; private set; }
        public int                      No                      { get; set; }
        public List<ModTex>             Textures                { get; private set; }
        public List<ModCol>             Colors                  { get; private set; }
        public List<ModValue>           Values                  { get; private set; }

        public ModMaterial(ModSlot owner)
        {
            Owner       = owner;
            Textures    = new List<ModTex>();
            Colors      = new List<ModCol>();
            Values      = new List<ModValue>();
        }
    }

    public class ModParam
    {
        public string                   Type                    { get; set; }
        public string                   Name                    { get; set; }
        public bool                     Enabled                 { get; set; }
    }
    
    public class ModTex : ModParam
    {
        public string                   Texture                 { get; set; }

        public ModTex()
        {
            Type    = "テクスチャ設定";
        }

        public override string ToString()
        {
            return string.Format("{0}\t{1}\t{2}", Type, Name, Texture);
        }
    }
    
    public class ModCol : ModParam
    {
        public int                      R                       { get; set; }
        public int                      G                       { get; set; }
        public int                      B                       { get; set; }
        public int                      A                       { get; set; }

        public ModCol()
        {
            Type    = "色設定";
        }

        public override string ToString()
        {
            return string.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}", Type, Name, R, G, B, A);
        }
    }
    
    public class ModValue : ModParam
    {
        public decimal                  Value                   { get; set; }

        public ModValue()
        {
            Type    = "数値設定";
        }

        public override string ToString()
        {
            return string.Format("{0}\t{1}\t{2:F}", Type, Name, Value);
        }
    }
}
