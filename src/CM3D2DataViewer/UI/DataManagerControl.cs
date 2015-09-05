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
        public TreeNode             MenuNode;
        public TreeNode             ModNode;
        public bool                 SuppressDataAdded           { get; private set; }

        public DataManagerControl()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if(!System.Diagnostics.Debugger.IsAttached)
                treeView1.ContextMenuStrip  = null;
        }

        private void SetDataManager(DataManager value)
        {
            if(value == datamanager)
                return;

            if(null != datamanager)
                datamanager.DataAdded   -=Datamanager_DataAdded;

            datamanager = value;
            if(null != datamanager)
                datamanager.DataAdded   +=Datamanager_DataAdded;

            UpdateUI();
        }

        private void Datamanager_DataAdded(object sender, DataFileEventArgs e)
        {
            if(SuppressDataAdded)
                return;

            if(e.File is MenuFile)
            {
                var menu    = (MenuFile)e.File;
                var category= menu.GetStrings("category")[1];
                var node    = MenuNode.Nodes.Cast<TreeNode>().FirstOrDefault(i => i.Text == category);

                if(node == null)
                {
                    node    = MenuNode.Nodes.Add(category);
                } else
                foreach(TreeNode j in node.Nodes)
                {
                    if(((BaseFile)j.Tag).FileName == menu.FileName)
                    {
                        j.Text  = menu.Descriptions[1];
                        j.Tag   = menu;
                        break;
                    }
                }

                var node2   = node.Nodes.Add(menu.Descriptions[1]);
                node2.Tag   = menu;
            }
        }

        public void ReloadMenus()
        {
            throw new NotImplementedException();

            //BuildMenuTree();
        }

        public void ReloadMods()
        {
            ModManager.Instance.Reload();

            BuildModTree();
        }

        private void BuildMenuTree()
        {
            SuppressDataAdded   = true;

            try
            {
                if(MenuNode != null)
                    treeView1.Nodes.Remove(MenuNode);

                var categories  = datamanager.Menus.Values.GroupBy(i => i.GetStrings("category")[1]);
                MenuNode        = treeView1.Nodes.Add("GameData");

                foreach(var i in categories.OrderBy(i => i.Key))
                {
                    var node    = MenuNode.Nodes.Add(i.Key);

                    foreach(var j in i)
                    {
                        var node2   = node.Nodes.Add(j.Descriptions[1]);
                        node2.Tag   = j;
                    }
                }
            } finally
            {
                SuppressDataAdded   = false;
            }
        }

        private void BuildModTree()
        {
#if ENABLE_MOD
            if(ModNode != null)
                treeView1.Nodes.Remove(ModNode);

            var categories  = ModManager.Instance.ModFiles.Values.GroupBy(i => i.Descriptions["カテゴリ名"]);
            ModNode         = treeView1.Nodes.Add("ModTemplate");

            foreach(var i in categories.OrderBy(i => i.Key))
            {
                var node    = ModNode.Nodes.Add(i.Key);

                foreach(var j in i)
                {
                    var node2   = node.Nodes.Add(j.Descriptions["アイテム名"]);
                    node2.Tag   = j;
                }
            }
#endif
        }

        private void UpdateUI()
        {
            treeView1.Nodes.Clear();

            if(null == datamanager)
                return;

            BuildMenuTree();    // ゲームデータ
#if ENABLE_MOD
            BuildModTree();     // MOD
#endif
            MenuNode.Expand();
#if ENABLE_MOD
            ModNode.Expand();
#endif
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if(e.Node == null)
                return;

            if(e.Node.Tag is MenuFile)
            {
                var menu    = (MenuFile)e.Node.Tag;
                cM3D2MenuControl1.Data      = menu;
                cM3D2MenuControl1.Visible   = true;
                modEditorControl1.Visible   = false;
            } else
            if(e.Node.Tag is ModSrcFile)
            {
                var mod     = (ModSrcFile)e.Node.Tag;
                modEditorControl1.Data      = mod;
                cM3D2MenuControl1.Visible   = false;
                modEditorControl1.Visible   = true;
            }
        }

        private void cM3D2MenuControl1_OpenItem(object sender, OpenItemEventArgs e)
        {
            var item= DataManager.FindItem(e.Name);

            if(item is BaseFile)
                Form1.Instance.LoadFile(((BaseFile)item).FileName);
        }

        private void modEditorControl1_OpenItem(object sender, OpenItemEventArgs e)
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
