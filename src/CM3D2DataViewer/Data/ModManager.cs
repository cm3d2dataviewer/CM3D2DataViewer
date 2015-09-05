using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CM3D2DataViewer
{
    public class ModManager
    {
        public static ModManager        Instance                { get; private set; }

        public string                   DataDirectory           { get; private set; }
        public Dictionary<string, ModSrcFile>  ModFiles    = new Dictionary<string, ModSrcFile>();

        public ModManager()
        {
            Instance    = this;
            ModFiles    = new Dictionary<string, ModSrcFile>();
        }

        public void Load(string datadir)
        {
            DataDirectory   = datadir;

            DoLoadd();
        }

        public void Reload()
        {
            ModFiles.Clear();
            DoLoadd();
        }

        private void DoLoadd()
        {
            var moddir  = Path.GetDirectoryName(DataDirectory);
            moddir      = Path.Combine(moddir, "ModExport");

            if(Directory.Exists(moddir))
                LoadDirectory(moddir);
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
                case ".txt":    LoadModFile(file); break;
                }
            } catch(Exception ex)
            {
                var msg = string.Format("読み込み失敗: {0}", file);

                System.Diagnostics.Debug.Print(msg);
              //Errors.Add(msg);
            }
        }

        private void LoadModFile(string file)
        {
            try
            {
                var mod = ModSrcFile.FromFile(file);

                if(mod.Valid)
                    ModFiles[Path.GetFileName(file)]= mod;
            } catch(Exception ex)
            {
            }
        }
    }
}
