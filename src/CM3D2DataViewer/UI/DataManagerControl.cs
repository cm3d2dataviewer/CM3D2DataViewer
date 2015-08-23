using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CM3D2DataViewer
{
    public partial class DataManagerControl : UserControl
    {
        private DataManager         datamanager;

        public DataManager          DataManager                 { get { return datamanager; } set { SetDataManager(value); } }

        public DataManagerControl()
        {
            InitializeComponent();
        }

        private void SetDataManager(DataManager value)
        {
            if(value == datamanager)
                return;

            datamanager = value;

            UpdateUI();
        }

        private void UpdateUI()
        {
            treeView1.Nodes.Clear();

            if(null == datamanager)
                return;

            var categories  = datamanager.Menus.GroupBy(i => i.GetStrings("category")[1]);

            foreach(var i in categories.OrderBy(i => i.Key))
            {
                var node    = treeView1.Nodes.Add(i.Key);

                foreach(var j in i)
                {
                    var node2   = node.Nodes.Add(j.Descriptions[1]);
                    node2.Tag   = j;
                }
            }

            //treeView1.ExpandAll();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if(e.Node == null)
                return;

            if(e.Node.Tag is MenuFile)
            {
                var menu    = (MenuFile)e.Node.Tag;
                cM3D2MenuControl1.Data  = menu;
            }
        }

        private void cM3D2MenuControl1_OpenItem(object sender, OpenItemEventArgs e)
        {
            var item= DataManager.FindItem(e.Name);

            if(item is BaseFile)
                Form1.Instance.LoadFile(((BaseFile)item).FileName);
        }

        private void tsmiNew_Click(object sender, EventArgs e)
        {
            try
            {
                var dlg = new NewMenuDialog();

                dlg.NewMenuControl.SrcMenu  = (MenuFile)treeView1.SelectedNode.Tag;

                if(dlg.ShowDialog() != DialogResult.OK)
                    return;
            } catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void tsmiAddColor_Click(object sender, EventArgs e)
        {

        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if(null == treeView1.SelectedNode)
            {
                tsmiNew.Enabled     = false;
                tsmiAddColor.Enabled= false;
            } else
            {
                tsmiNew.Enabled     = treeView1.SelectedNode.Tag is MenuFile;
                tsmiAddColor.Enabled= treeView1.SelectedNode.Tag is MenuFile;
            }
        }
    }
}
