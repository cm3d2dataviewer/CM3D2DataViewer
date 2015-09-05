using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CM3D2DataViewer
{
    public class DataFileEventArgs : EventArgs
    {
        public BaseFile                 File                    { get; private set; }

        public DataFileEventArgs(BaseFile file)
        {
            File    = file;
        }
    }

    public class DataManager
    {
        public static DataManager       Instance                { get; private set; }

        public string                   BaseDir                 { get; private set; }
        public string                   BackupDir               { get; private set; }
      //public List<MenuFile>           Menus                   { get; private set; }
      //public List<ModelSummary>       Models                  { get; private set; }
      //public List<MateFile>           Materials               { get; private set; }
      //public List<TexSummary>         Textures                { get; private set; }
        public Dictionary<string, ModelSummary> Models          { get; private set; }
        public Dictionary<string, MateFile> Materials           { get; private set; }
        public Dictionary<string, TexSummary> Textures          { get; private set; }
        public Dictionary<string, MenuFile> Menus               { get; private set; }

        public List<string>             Errors                  { get; private set; }
        public ModelFile                Body001                 { get; private set; }

        public event EventHandler<DataFileEventArgs> DataAdded;

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
            Menus       = new Dictionary<string, MenuFile>();
            Models      = new Dictionary<string, ModelSummary>();
            Textures    = new Dictionary<string, TexSummary>();
            Materials   = new Dictionary<string, MateFile>();
        }

        protected void OnDataAdded(DataFileEventArgs e)
        {
            if(null != DataAdded)
                DataAdded(this, e);
        }

        public object FindItem(string name)
        {
            name    = name.ToUpper();

            ModelSummary    model;

            if(Models.TryGetValue(name, out model))
                return model;

            TexSummary      tex;

            if(Textures.TryGetValue(name, out tex))
                return tex;

            MateFile        mate;

            if(Materials.TryGetValue(name, out mate))
                return mate;

            MenuFile        menu;

            if(Menus.TryGetValue(name, out menu))
                return menu;

            return null;
        }

        public void Load(string dir)
        {
            BaseDir         = dir;
            BackupDir       = BaseDir + "Backup";
            Models          = new Dictionary<string, ModelSummary>();
            Materials       = new Dictionary<string, MateFile>();
            Textures        = new Dictionary<string, TexSummary>();
            Menus           = new Dictionary<string, MenuFile>();

            LoadDirectory(dir);

            var body001 = FindItem("body001.model") as ModelSummary;

            if(null != body001)
                Body001 = ModelFile.FromFile(body001.FileName);

            if(!Directory.Exists(BackupDir))
                Directory.CreateDirectory(BackupDir);
        }

        public void LoadDirectory(string dir)
        {
            foreach(var i in Directory.GetFiles(dir))
                LoadFile(i);

            foreach(var i in Directory.GetDirectories(dir))
                LoadDirectory(i);
        }

        private void Add(MateFile data)
        {
            Materials[Path.GetFileName(data.FileName).ToUpper()]= data;

            OnDataAdded(new DataFileEventArgs(data));
        }

        private void Add(MenuFile data)
        {
            Menus[Path.GetFileName(data.FileName).ToUpper()]    = data;

            OnDataAdded(new DataFileEventArgs(data));
        }

        private void Add(ModelSummary data)
        {
            Models[Path.GetFileName(data.FileName).ToUpper()]   = data;

            OnDataAdded(new DataFileEventArgs(data));
        }

        private void Add(TexSummary data)
        {
            Textures[Path.GetFileName(data.FileName).ToUpper()] = data;

            OnDataAdded(new DataFileEventArgs(data));
        }

        public void LoadFile(string file)
        {
            try
            {
                switch(Path.GetExtension(file).ToLower())
                {
                case ".model" : Add(ModelSummary.FromFile(file)); break;
                case ".menu":   Add(MenuFile    .FromFile(file)); break;
                case ".mate":   Add(MateFile    .FromFile(file)); break;
                case ".tex":    Add(TexSummary  .FromFile(file)); break;
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
