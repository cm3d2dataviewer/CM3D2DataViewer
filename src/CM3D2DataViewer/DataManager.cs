using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CM3D2DataViewer
{
    public class DataManager
    {
        public static DataManager       Instance                { get; private set; }

        public string                   BaseDir                 { get; private set; }
        public List<MenuFile>           Menus                   { get; private set; }
        public List<ModelSummary>       Models                  { get; private set; }
        public List<MateFile>           Materials               { get; private set; }
        public List<TexSummary>         Textures                { get; private set; }
        public Dictionary<string, ModelSummary> ModelByName     { get; private set; }
        public Dictionary<string, MateFile> MaterialByName      { get; private set; }
        public Dictionary<string, TexSummary> TextureByName     { get; private set; }
        public Dictionary<string, MenuFile> MenuByName          { get; private set; }

        public List<string>             Errors                  { get; private set; }

        public bool Empty
        {
            get
            {
                return 0 == Menus.Count
                    && 0 == Models.Count
                    && 0 == Materials.Count
                    && 0 == Textures.Count;
            }
        }

        public DataManager()
        {
            Instance    = this;
            Errors      = new List<string>();
            Menus       = new List<MenuFile>();
            Models      = new List<ModelSummary>();
            Textures    = new List<TexSummary>();
            Materials   = new List<MateFile>();
        }

        public object FindItem(string name)
        {
            name    = name.ToUpper();

            ModelSummary    model;

            if(ModelByName.TryGetValue(name, out model))
                return model;

            TexSummary      tex;

            if(TextureByName.TryGetValue(name, out tex))
                return tex;

            MateFile        mate;

            if(MaterialByName.TryGetValue(name, out mate))
                return mate;

            MenuFile        menu;

            if(MenuByName.TryGetValue(name, out menu))
                return menu;

            return null;
        }

        public void Load(string dir)
        {
            BaseDir         = dir;
            Menus           = new List<MenuFile>();
            Models          = new List<ModelSummary>();
            Materials       = new List<MateFile>();
            Textures        = new List<TexSummary>();
            ModelByName     = new Dictionary<string, ModelSummary>();
            MaterialByName  = new Dictionary<string, MateFile>();
            TextureByName   = new Dictionary<string, TexSummary>();
            MenuByName      = new Dictionary<string, MenuFile>();

            LoadDirectory(dir);

            foreach(var i in Models)
                ModelByName[Path.GetFileName(i.FileName).ToUpper()]     = i;

            foreach(var i in Materials)
                MaterialByName[Path.GetFileName(i.FileName).ToUpper()]  = i;

            foreach(var i in Textures)
                TextureByName[Path.GetFileName(i.FileName).ToUpper()]   = i;

            foreach(var i in Menus)
                MenuByName[Path.GetFileName(i.FileName).ToUpper()]      = i;
        }

        private void LoadDirectory(string dir)
        {
            foreach(var i in Directory.GetFiles(dir))
                LoadFile(i);

            foreach(var i in Directory.GetDirectories(dir))
                LoadDirectory(i);
        }

        private void LoadFile(string file)
        {
            try
            {
                switch(Path.GetExtension(file).ToLower())
                {
                case ".model":  Models   .Add(ModelSummary.FromFile(file)); break;
                case ".menu":   Menus    .Add(MenuFile    .FromFile(file)); break;
                case ".mate":   Materials.Add(MateFile    .FromFile(file)); break;
                case ".tex":    Textures .Add(TexSummary  .FromFile(file)); break;
                }
            } catch(Exception ex)
            {
                var msg = string.Format("読み込み失敗: {0}", file);

                System.Diagnostics.Debug.Print(msg);
                Errors.Add(msg);
            }
        }
    }
}
