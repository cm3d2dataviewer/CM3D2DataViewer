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
    public partial class CM3D2MenuControl : UserControl
    {
        private MenuFile                data;

        public MenuFile                 Data                    { get { return data; } set { SetData(value); } }

        public event EventHandler<OpenItemEventArgs>    OpenItem;

        public CM3D2MenuControl()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        private void SetData(MenuFile value)
        {
            if(value == data)
                return;

            data    = value;

            UpdateView();
        }

        private void UpdateView()
        {
            flowLayoutPanel1.Controls.Clear();

            if(data == null)
            {
                bAnalysisRelation.Enabled   = false;
                toolStrip1.Enabled          = false;
                tbScript.Text       = "";
                return;
            }

            bAnalysisRelation.Enabled  = true;
            toolStrip1.Enabled          = true;

            var sb      = new StringBuilder();

            foreach(var i in Data.Descriptions)
                sb.AppendFormat("{0}", i).AppendLine();

            tbDescription.Text   = sb.ToString();
            tbDescription.Select(0, 0);

            sb.Length   = 0;

            foreach(var i in Data.Scripts)
                sb.AppendLine(string.Join(" ", i.ToArray()));

            tbScript.Text   = sb.ToString();
            tbScript.Select(0, 0);

            foreach(var i in Data.Scripts)
            {
                if(i.Count == 0)
                    continue;

                #if false
                Button  b;

                switch(i[0])
                {
                case "アイテム":
                case "icons":
                case "additem":
                    b       = new Button() { AutoSize= true, Text= i[1], Tag= i };
                    b.Click +=B_Click;

                    flowLayoutPanel1.Controls.Add(b);

                    break;

                case "マテリアル変更":
                    b       = new Button() { AutoSize= true, Text= i[3], Tag= i };
                    b.Click +=B_Click;

                    flowLayoutPanel1.Controls.Add(b);

                    break;
                }
                #else
                foreach(var j in i)
                {
                    if(DataManager.Instance.FindItem(j) != null)
                    {
                        var b   = new Button() { AutoSize= true, Text= j, Tag= i };
                        b.Click +=B_Click;

                        flowLayoutPanel1.Controls.Add(b);
                    }
                }
                #endif
            }
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

        private void B_Click(object sender, EventArgs e)
        {
            var c   = (Control)sender;

            Open(c.Text);
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

        private void tsbClose_Click(object sender, EventArgs e)
        {
            if(Parent is TabPage)
            {
                var tp  = (TabPage)Parent;
                ((TabControl)tp.Parent).TabPages.Remove(tp);
            }
        }

        private void tsbClone_Click(object sender, EventArgs e)
        {
            // todo implement
        }

        private void tsbFileSave_Click(object sender, EventArgs e)
        {
            try
            {
                var desc            = tbDescription.Lines.Select(i => i.Trim()).Where(i => i.Length > 0).ToList();
                var scripts         = new List<List<string>>();
                var delim           = " ".ToArray();

                while(desc.Count < Data.Descriptions.Count)   desc.Add("");
                while(desc.Count > Data.Descriptions.Count)   desc.RemoveAt(Data.Descriptions.Count);

                foreach(var i in tbScript.Lines.Select(i => i.Trim()).Where(i => i.Length > 0))
                {
                    var t   = i.Split(delim, StringSplitOptions.RemoveEmptyEntries);

                    scripts.Add(t.ToList());
                }

                Data.Descriptions   = desc;
                Data.Scripts        = scripts;

                Data.Backup();

                MenuFile.ToFile(Data.FileName, Data);
            } catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void tsbLoadBackup_Click(object sender, EventArgs e)
        {
            try
            {
                var data        = MenuFile.FromFile(Data.GetBackupFileName());
                data.FileName   = Path.ChangeExtension(data.FileName, ".menu");
                Data            = data;
            } catch {}
        }

        private void bAnalysisRelation_Click(object sender, EventArgs e)
        {
            if(Data == null)
                return;

            var cloneset    = new CloneSet(Data);

            tvRelationTree.Nodes.Clear();

            BuildResourceTree(tvRelationTree.Nodes, cloneset);

            tvRelationTree.ExpandAll();
        }

        private string MakeLabel(BaseFile file)
        {
            var filename= Path.GetFileName(file.FileName);

            if(file is ModelSummary)
                return string.Format("{0}({1})", filename, file.Descriptions[1]);

            if(file is MateFile)
                return string.Format("{0}({1})", filename, file.Descriptions[1]);

            if(file is MenuFile)
                return string.Format("{0}({1})", filename, file.Descriptions[1]);

            if(file is TexSummary)
                return filename;

            return filename;
        }

        private void BuildResourceTree(TreeNodeCollection nodes, FileComposition comp)
        {
            var text    = MakeLabel(comp.File);
            var node    = nodes.Add(text);
            node.Tag    = comp;

            if(comp is CloneSet)
            {
                var cs  = (CloneSet)comp;

                foreach(var i in cs.RelationMenuFiles)
                    BuildResourceTree(node.Nodes, i);
            }

            foreach(var i in comp.RelationFiles)
                BuildResourceTree(node.Nodes, i);
        }

        private void bOpenRelation_Click(object sender, EventArgs e)
        {
            if(tvRelationTree.SelectedNode == null)
                return;

            var data= (FileComposition)tvRelationTree.SelectedNode.Tag;

            Open(Path.GetFileName(data.File.FileName));
        }

        private void tvRelationTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            bOpenRelation.Enabled   = tvRelationTree.SelectedNode != null;
        }

        private void tvRelationTree_DoubleClick(object sender, EventArgs e)
        {
            var pos = tvRelationTree.PointToClient(Control.MousePosition);
            var hti = tvRelationTree.HitTest(pos);

            if(hti.Node == null)
                return;

            var data= (FileComposition)hti.Node.Tag;

            Open(Path.GetFileName(data.File.FileName));
        }
    }

    public class OpenItemEventArgs : EventArgs
    {
        public string                   Name                    { get; private set;  }

        public OpenItemEventArgs(string name)
        {
            Name    = name;
        }
    }
}
