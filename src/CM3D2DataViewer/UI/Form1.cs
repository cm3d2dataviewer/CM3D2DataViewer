using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CM3D2DataViewer
{
    public partial class Form1 : Form
    {
        public static Form1             Instance                { get; private set; }

        public DataManager              DataManager             { get; private set; }
        public ModManager               ModManager              { get; private set; }
        public TextBox                  MessageTextBox          { get { return textBox1; } }
                    
        public Form1()
        {
            InitializeComponent();

            Instance    = this;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            DataManager = new DataManager();
            ModManager  = new ModManager();

            if(Config.Instance.DataDir != null)
            {
                #if ENABLE_MOD
                try
                {
                    if(Directory.Exists(Config.Instance.DataDir))
                        LoadMods(Config.Instance.DataDir);
                } catch(Exception ex)
                {
                    System.Diagnostics.Debug.Print(ex.ToString());
                }
                #endif

                try
                {
                    if(Directory.Exists(Config.Instance.DataDir))
                    {
                        LoadFile(Config.Instance.DataDir);
                    }

                    foreach(var i in Directory.GetFiles(Config.Instance.DataDir, "*.arc"))
                    {
                        var tsb = new ToolStripButton(Path.GetFileName(i));
                        tsb.Tag = i;
                        tsb.Click += tsbArchive_Click;

                        tsddbArchive.DropDownItems.Add(tsb);
                    }
                } catch(Exception ex)
                {
                    System.Diagnostics.Debug.Print(ex.ToString());
                }
            }

            try { tsddbArchive.Enabled= null != Config.Instance.CM3D2Tool  && File.Exists(Config.Instance.CM3D2Tool);  } catch {}
            try { tsbRunGame.Enabled  = null != Config.Instance.ReiPatcher && File.Exists(Config.Instance.ReiPatcher); } catch {} 

            tscbArch.SelectedItem   = Config.Instance.RunArch;

            if(DataManager.Empty)
            {
                panel1.Visible              = true;
                dataManagerControl1.Visible = false;
            }
        }

        private void tsbArchive_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            if(e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                var files   = (string[])e.Data.GetData(DataFormats.FileDrop);

                foreach(var i in files)
                    LoadFile(i);
            }
        }

        private void LoadDataTree(string dir)
        {
            if(!Directory.Exists(dir))
                return;

            DataManager.Load(dir);

            dataManagerControl1.DataManager = DataManager;
            Config.Instance.DataDir         = dir;
            Config.Instance.Save();

            if(DataManager.Empty)
            {
                panel1.Visible              = true;
                dataManagerControl1.Visible = false;
            } else
            {
                panel1.Visible              = false;
                dataManagerControl1.Visible = true;
            }
        }

        private void SetCM3D2Tool(string file)
        {
            Config.Instance.CM3D2Tool   = file;
            Config.Instance.Save();
            try { tsddbArchive.Enabled= null != Config.Instance.CM3D2Tool  && File.Exists(Config.Instance.CM3D2Tool);  } catch {}
        }

        private void SetReiPatcher(string file)
        {
            Config.Instance.ReiPatcher  = file;
            Config.Instance.Save();
            try { tsbRunGame.Enabled  = null != Config.Instance.ReiPatcher && File.Exists(Config.Instance.ReiPatcher); } catch {} 
        }

        public TabPage FindFileTab(string file)
        {
            foreach(TabPage i in tabControl1.TabPages)
            {
                if(i.Controls.Count != 1)
                    continue;

                var tag = i.Controls[0].Tag;

                if(tag is BaseFile)
                    if(((BaseFile)tag).FileName == file)
                        return i;
            }

            return null;
        }

        public void LoadMods(string dir)
        {
            ModManager.Instance.Load(dir);
        }

        public void LoadFile(string file)
        {
            if(Directory.Exists(file))
                LoadDataTree(file);

            var name    = Path.GetFileName(file);

            if(name.ToUpper() == "CM3D2TOOL.EXE")
            {
                SetCM3D2Tool(file);
                return;
            }

            if(name.ToUpper() == "REIPATCHER.EXE")
            {
                SetReiPatcher(file);
                return;
            }

            var tab = FindFileTab(file);

            if(tab != null)
            {
                tabControl1.SelectedTab = tab;
                return;
            }

            switch(Path.GetExtension(file).ToLower())
            {
            case ".menu":   LoadMenu (file); break;
            case ".tex":    LoadTex  (file); break;
            case ".@model":
            case ".@@model":
            case ".model":  LoadModel(file); break;
            case ".mate":   LoadMate (file); break;
            }
        }

        private void LoadMenu(string file)
        {
            var data    = MenuFile.FromFile(file);
            var control = new CM3D2MenuControl() { Data= data, Tag= data };

            AddTab(Path.GetFileName(file), control);

            control.OpenItem += Control_OpenItem;
        }

        private void LoadTex(string file)
        {
            var data    = TexFile.FromFile(file);
            var control = new CM3D2TextureControl() { Data= data, Tag= data };

            AddTab(Path.GetFileName(file), control);
        }

        private void LoadModel(string file)
        {
            var data    = ModelFile.FromFile(file);
            var control = new CM3D2ModelControl() { Data= data, Tag= data };

            AddTab(Path.GetFileName(file), control);
        }

        private void LoadMate(string file)
        {
            var data    = MateFile.FromFile(file);
            var control = new CM3D2MaterialControl() { Data= data, Tag= data };

            AddTab(Path.GetFileName(file), control);

            control.OpenItem += Control_OpenItem;
        }

        private void Control_OpenItem(object sender, OpenItemEventArgs e)
        {
            var item= DataManager.FindItem(e.Name);

            if(item is BaseFile)
                LoadFile(((BaseFile)item).FileName);
        }

        private void AddTab(string name, Control c)
        {
            var tab     = new TabPage(name);
            c.Dock      = DockStyle.Fill;
            tab.ImageIndex  = 0;

            tab.Controls.Add(c);
            tabControl1.TabPages.Add(tab);

            tabControl1.SelectedTab = tab;
        }

        private void Form1_DragOver(object sender, DragEventArgs e)
        {
            e.Effect    = e.Data.GetDataPresent(DataFormats.FileDrop)
                ? e.AllowedEffect & DragDropEffects.Copy : DragDropEffects.None;
        }

        private void tsbDebug_Click(object sender, EventArgs e)
        {
            var sb  = new StringBuilder();

            foreach(var i in DataManager.Instance.Menus.Values)
            {
                foreach(var j in i.Scripts)
                {
                    var text    = string.Join("\t", j.ToArray());

                    System.Diagnostics.Debug.Print(text);
                    sb.AppendLine(text);
                }
            }

            Clipboard.SetText(sb.ToString());
        }

        private void tsbRunGame_Click(object sender, EventArgs e)
        {
            var psi = new System.Diagnostics.ProcessStartInfo()
            {
                FileName            = Config.Instance.ReiPatcher,
                Arguments           = string.Format("-c CM3D2{0}.ini", tscbArch.Text),
                WorkingDirectory    = Path.GetDirectoryName(Config.Instance.ReiPatcher),
                UseShellExecute     = false,
            };

            try
            {
                System.Diagnostics.Process.Start(psi);

                Globals.Message("ゲームを起動しました -- {0} {1}", psi.FileName, psi.Arguments);
            } catch(Exception ex)
            {
                Globals.Message("ゲームの起動に失敗しました -- {0} {1}", psi.FileName, psi.Arguments);
                MessageBox.Show(ex.ToString());
            }
        }

        private void tabControl1_MouseDown(object sender, MouseEventArgs e)
        {
            if(tabControl1.TabPages.Count == 0)
                return;

            var tp  = tabControl1.SelectedTab;
            var b   = tabControl1.GetTabRect(tabControl1.TabPages.IndexOf(tp));

            if(b.Contains(e.Location))
            {
                var p   = e.Location;
                p.X     -=b.Left;
                p.Y     -=b.Top;

                if(new Rectangle(4, 1, 16, 16).Contains(p))
                    tabControl1.TabPages.Remove(tabControl1.SelectedTab);
            }
        }

        private void tscbArch_SelectedIndexChanged(object sender, EventArgs e)
        {
            Config.Instance.RunArch = tscbArch.Text;
            Config.Instance.Save();
        }

        private void tsbReloadMod_Click(object sender, EventArgs e)
        {
            dataManagerControl1.ReloadMods();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            //var fbx = FbxLib.FbxFile.FromFile(@"D:\sh0\Documents\3dsMax\export\Test.FBX");

        }
    }
}
