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
    public partial class NewMenuControl : UserControl
    {
        public MenuFile                 SrcMenu                 { get; set; }
        public MenuFile                 NewMenu                 { get; set; }
        public string                   SrcName                 { get; private set; }
        public string                   NewName                 { get; private set; }
        public CloneSet                 CloneSet                { get; private set; }

        public NewMenuControl()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if(SrcMenu == null)
                return;

            bCreate.Enabled = System.Diagnostics.Debugger.IsAttached;

            CloneSet    = new CloneSet(SrcMenu);

            CloneSet.Dump();

            var additem = SrcMenu.Scripts.Where(i => i.Count > 0).FirstOrDefault(i => i[0] == "additem");

            if(additem == null)
            {
                SrcName = "";
            } else
            {
                var s   = Path.GetFileNameWithoutExtension(additem[1]);
              //var idx = s.IndexOf('_');
              //s       = idx < 0 ? s : s.Substring(0, idx);
                SrcName = s;
            }

            NewName = "New" + SrcName;

            Replace();

            tbSrcName.Text  = SrcName;
            tbNewName.Text  = NewName;
        }

        private void Replace()
        {
            var sb1 = new StringBuilder();
            var sb2 = new StringBuilder();

            listView1.Items.Clear();

            foreach(var i in SrcMenu.Scripts)
            {
                var s   = i.Select(j => j.Replace(SrcName, NewName)).ToArray();

                sb1.AppendLine(string.Join(" ", i.ToArray()));
                sb2.AppendLine(string.Join(" ", s));

                foreach(var j in i)
                {
                    var file= DataManager.Instance.FindItem(j) as BaseFile;

                    if(file == null)
                        continue;

                    var name    = Path.GetFileName(file.FileName);
                    var newname = Replace(name, SrcName, NewName);

                    if(name != newname)
                    {
                        var item= listView1.Items.Add(name);
                        item.Tag= file;

                        item.SubItems.Add(newname);

                        if(file is MateFile)
                        {
                            var mate= (MateFile)file;

                            foreach(var k in mate.Params.OfType<ParamTex>().Where(l => l.TexAsset != null))
                            {
                                var texname = Path.ChangeExtension(Path.GetFileName(k.TexAsset), ".tex");
                                var tex     = DataManager.Instance.FindItem(texname) as TexSummary;

                                if(tex != null)
                                {
                                    var name2   = Path.GetFileName(tex.FileName);
                                    var newname2= Replace(name2, SrcName, NewName);

                                    if(name2 != newname2)
                                    {
                                        var item2   = listView1.Items.Add(name2);
                                        item2.Tag   = file;
                                        item2.IndentCount   = 1;

                                        item2.SubItems.Add(newname2);
                                    }
                                }
                            }
                        } else
                        if(file is MenuFile)
                        {
                        }
                    }
                }
            }

            tbSrcScript.Text= sb1.ToString();
            tbNewScript.Text= sb2.ToString();
        }

        private string Replace(string s, string s1, string s2)
        {
            var index   = s.ToUpper().IndexOf(s1.ToUpper());

            if(index < 0)
                return s;

            return s.Substring(0, index) + s2 + s.Substring(index+s1.Length);
        }

        private void bReplace_Click(object sender, EventArgs e)
        {
            SrcName = tbSrcName.Text;
            NewName = tbNewName.Text;

            Replace();
        }

        private void bCancel_Click(object sender, EventArgs e)
        {
            if(Parent is Form)
            {
                var form            = (Form)Parent;
                form.DialogResult   = DialogResult.Cancel;

                form.Close();
            }
        }

        private void bCreate_Click(object sender, EventArgs e)
        {
            var scripts         = new List<List<string>>();
            var delim           = " ".ToArray();
            var newfiles        = new List<BaseFile>();

            foreach(var i in SrcMenu.Scripts)
            {
                var s1  = i.ToArray();
                var s2  = i.Select(j => j.Replace(SrcName, NewName)).ToArray();

                for(int j= 0; j < s1.Length; ++j)
                {
                    var file    = DataManager.Instance.FindItem(s1[j]) as BaseFile;

                    if(null == file)
                        continue;

                    var dir     = Path.GetDirectoryName(file.FileName);
                    var newname = Path.Combine(dir, s2[j]);

                    File.Copy(file.FileName, newname, true);

                    if(file is TexFile)
                    {
                        var tex     = TexFile.FromFile(newname);

                        // todo 参照など変更

                        TexFile.ToFile(tex.FileName, tex);
                        newfiles.Add(tex);
                    } else
                    if(file is MateFile)
                    {
                        var mate    = MateFile.FromFile(newname);

                        // todo 参照など変更

                        MateFile.ToFile(mate.FileName, mate);
                        newfiles.Add(mate);
                    } else
                    if(file is ModelFile)
                    {
                        var model   = ModelFile.FromFile(newname);

                        // todo 参照など変更

                        ModelFile.ToFile(model.FileName, model);
                        newfiles.Add(model);
                    } else
                    if(file is MenuFile)
                    {
                        var menu    = MenuFile.FromFile(newname);

                        // todo 参照など変更

                        MenuFile.ToFile(menu.FileName, menu);
                        newfiles.Add(menu);
                    }
                }
            }
        }
    }
}
