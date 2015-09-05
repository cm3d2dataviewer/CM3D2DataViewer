using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CM3D2DataViewer
{
    public partial class ModEditorControl : UserControl
    {
        private ModSrcFile                 data;

        public ModSrcFile                  Data                { get { return data; } set { SetData(value); } }

        public event EventHandler<OpenItemEventArgs>    OpenItem;

        public ModEditorControl()
        {
            InitializeComponent();
        }

        private void SetData(ModSrcFile value)
        {
            if(value == data)
                return;

            data    = value;

            UpdateView();
        }

        private void UpdateView()
        {
            if(data == null)
                return;

            if(Data.TempDescriptions == null)
            {
                tbDescription.Text  = string.Join(
                    Environment.NewLine,
                    Data.Descriptions.Select(i => string.Format("{0}\t{1}", i.Key, i.Value)).ToArray());
            } else
            {
                tbDescription.Text  = Data.TempDescriptions;
            }

            comboBox1.Items.Clear();
            comboBox1.Items.AddRange(Data.Slots.Cast<object>().ToArray());
                
            if(comboBox1.Items.Count > 0)
                comboBox1.SelectedIndex = 0;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox1.SelectedIndex < 0
            || comboBox1.SelectedIndex >= comboBox1.Items.Count)
                return;

            tabControl1.TabPages.Clear();

            var data= (ModSlot)comboBox1.SelectedItem;

            foreach(var i in data.Materials)
            {
                var mmc = new ModMateralControl() { Dock= DockStyle.Fill, Data= i };
                var tc  = new TabPage() { Text= i.No.ToString() };
                
                tc.Controls.Add(mmc);
                tabControl1.TabPages.Add(tc);
            }
        }

        private void tsbFileOpen_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(Data.FileName);
            } catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void tsbDirectoryOpen_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(Path.GetDirectoryName(Data.FileName));
            } catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void tsbFileSave_Click(object sender, EventArgs e)
        {
            if(null == Data)
                return;

            UpdateDescription();

            ModSrcFile.ToFile(Data.FileName, Data);
        }

        private void tsbCopyFilePath_Click(object sender, EventArgs e)
        {
            if(null != Data)
                Clipboard.SetText(Path.GetDirectoryName(Data.FileName));
        }

        private void tbDescription_TextChanged(object sender, EventArgs e)
        {
            Data.TempDescriptions   = tbDescription.Text;
        }

        private void UpdateDescription()
        {
            var delim   = " \t".ToArray();

            foreach(var i in tbDescription.Lines)
            {
                var line    = i.Trim();
                var t       = line.Split(delim, StringSplitOptions.RemoveEmptyEntries);

                if(t.Length >= 2)
                    Data.Descriptions[t[0]] = string.Join(" ", t.Skip(1).ToArray());
            }

            Data.TempDescriptions   = null;
        }

        private void tsbCopyTextures_Click(object sender, EventArgs e)
        {
            try
            {
                var basename= Data.Descriptions["基本アイテム"];
                var menu    = DataManager.Instance.FindItem(basename) as MenuFile;

                if(menu == null)
                {
                    MessageBox.Show("基本アイテムがありません");
                    return;
                }

                UpdateDescription();

                var dlg = new TextureCopyDialog();
                dlg.Data= menu;
                dlg.Mod = Data;

                if(dlg.ShowDialog() != DialogResult.OK)
                    return;

                UpdateView();
            } catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void tsbOpenBaseMenu_Click(object sender, EventArgs e)
        {
            var basename= Data.Descriptions["基本アイテム"];
            var menu    = DataManager.Instance.FindItem(basename) as MenuFile;

            if(null != menu)
                Open(Path.GetFileName(menu.FileName));
        }

        private void Open(string name)
        {
            OnOpenItem(new OpenItemEventArgs(name));
        }

        protected void OnOpenItem(OpenItemEventArgs e)
        {
            if(null != OpenItem)
                OpenItem(this, e);
        }

        private void tsbModEnable_Click(object sender, EventArgs e)
        {
            try
            {
                var modfile = Path.ChangeExtension(Data.FileName, ".mod");

                if(File.Exists(modfile))
                {
                    var dir     = Path.GetDirectoryName(ModManager.Instance.DataDirectory);
                    dir         = Path.Combine(dir, "Mod");

                    if(!Directory.Exists(dir))
                        Directory.CreateDirectory(dir);

                    var dstfile = Path.Combine(dir, Path.GetFileName(modfile));

                    File.Copy(modfile, dstfile, true);

                    Globals.Message("MODをコピーしました -- {0}", dstfile);
                }
            } catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void tsbModDisable_Click(object sender, EventArgs e)
        {
            try
            {
                var modfile = Path.ChangeExtension(Data.FileName, ".mod");

                if(File.Exists(modfile))
                {
                    var dir     = Path.GetDirectoryName(ModManager.Instance.DataDirectory);
                    dir         = Path.Combine(dir, "Mod");
                    var dstfile = Path.Combine(dir, Path.GetFileName(modfile));

                    if(File.Exists(dstfile))
                    {
                        File.Delete(dstfile);
                        Globals.Message("MODを削除しました -- {0}", dstfile);
                    } else
                    {
                        Globals.Message("MODが適用されていません -- {0}", dstfile);
                    }
                }
            } catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
