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
    public partial class TextureCopyDialog : Form
    {
        private MenuFile                data;

        public ModSrcFile                  Mod                     { get; set; }
        public MenuFile                 Data                    { get { return data; } set { SetData(value); } }
        public Dictionary<string, string> ReplaceMap            { get; set; }

        public TextureCopyDialog()
        {
            InitializeComponent();
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
            if(null == Data)
                return;

            try
            {
                foreach(var i in Data.Scripts)
                {
                    if(i.Count < 2)
                        continue;

                    switch(i[0])
                    {
                    case "icons":
                        AddIcon(i);
                        break;

                    case "マテリアル変更":
                        AddMaterial(i);
                        break;
                    }
                }
            } catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void AddMaterial(List<string> script)
        {
            var mate    = DataManager.Instance.FindItem(script[3]) as MateFile;

            if(null == mate)
                return;

            foreach(var i in mate.Params.OfType<ParamTex>())
            {
                var texname = Path.GetFileName(i.TexAsset);

                AddTexture(i.Name, texname);
            }
        }

        private void AddIcon(List<string> script)
        {
            var tex = DataManager.Instance.FindItem(script[1]) as TexSummary;

            if(null == tex)
                return;

            AddTexture("アイコン", Path.GetFileName(tex.AssetPath));
        }

        private void AddTexture(string type, string filename)
        {
            var item    = listView1.Items.Add(type);
            item.SubItems.Add(filename);
            item.SubItems.Add(filename);
        }

        private string StringReplace(string str, string from, string to)
        {
            if(from.Length == 0 || to.Length == 0)
                return str;

            var index   = str.ToUpper().IndexOf(from);

            if(index < 0)
                return str;

            return str.Substring(0, index) + to + str.Substring(index+from.Length);
        }

        private void UpdateReplaceList()
        {
            var a1  = tbMatchString1.Text.ToUpper();
            var b1  = tbReplaceString1.Text;
            var a2  = tbMatchString2.Text.ToUpper();
            var b2  = tbReplaceString2.Text;

            foreach(ListViewItem i in listView1.Items)
            {
                var t   = i.SubItems[1].Text;

                if(cbReplace1.Enabled)
                    t   = StringReplace(t, a1, b1);

                if(cbReplace2.Enabled)
                    t   = StringReplace(t, a2, b2);

                i.SubItems[2].Text  = t;
            }
        }

        private void tbMatchString_TextChanged(object sender, EventArgs e)
        {
            UpdateReplaceList();
        }

        private void tbReplaceString_TextChanged(object sender, EventArgs e)
        {
            UpdateReplaceList();
        }

        private void tbMatchString2_TextChanged(object sender, EventArgs e)
        {
            UpdateReplaceList();
        }

        private void tbReplaceString2_TextChanged(object sender, EventArgs e)
        {
            UpdateReplaceList();
        }


        private void bCopy_Click(object sender, EventArgs e)
        {
            ReplaceMap  = new Dictionary<string, string>();

            foreach(ListViewItem i in listView1.Items)
                ReplaceMap[i.SubItems[1].Text.ToUpper()]= i.SubItems[2].Text;

            foreach(var i in Data.Scripts)
            {
                if(i.Count < 2)
                    continue;

                switch(i[0])
                {
                case "icons":
                    CopyIconTex(i);
                    break;

                case "マテリアル変更":
                    CopyMateTex(i);
                    break;
                }
            }

            DialogResult    = DialogResult.OK;

            Close();
        }

        private void bCancel_Click(object sender, EventArgs e)
        {

        }

        private void CopyIconTex(List<string> script)
        {
            var icon    = DataManager.Instance.FindItem(script[1]) as TexSummary;

            if(icon == null)
                return;

            var icondata= TexFile.FromFile(icon.FileName);
            var dir     = Path.GetDirectoryName(Mod.FileName);
            var filename= Path.Combine(dir, Path.GetFileName(icondata.AssetPath));
            filename    = ReplaceFileName(filename);

            File.WriteAllBytes(filename, icondata.ImageData);

            Mod.Descriptions["アイコン"]   = Path.GetFileName(filename);
        }

        private string ReplaceFileName(string file)
        {
            var name    = Path.GetFileName(file).ToUpper();
            var newname = "";

            if(ReplaceMap.TryGetValue(name, out newname))
                return Path.Combine(Path.GetDirectoryName(file), newname);

            return file;
        }
                    
        private void CopyMateTex(List<string> script)
        {
            var slotname= script[1];
            var no      = int.Parse(script[2]);
            var matefile= script[3];
            var basemate= DataManager.Instance.FindItem(matefile) as MateFile;

            if(null == basemate)
                return;

            var slot    = Mod.Slots.FirstOrDefault(i => i.Name == slotname);

            if(null == slot)
                return;

            var mate    = slot.Materials.FirstOrDefault(i => i.No == no);

            if(null == mate)
                return;

            var dir = Path.GetDirectoryName(Mod.FileName);

            foreach(var i in basemate.Params.OfType<ParamTex>())
            {
                var texparam= mate.Textures.FirstOrDefault(j => j.Name == i.Name);

                if(texparam == null)
                    return;

                var texname = Path.GetFileName(i.TexAsset);
                texname     = Path.ChangeExtension(texname, ".tex");
                var tex     = DataManager.Instance.FindItem(texname) as TexSummary;

                if(tex == null)
                    continue;

                var texdata = TexFile.FromFile(tex.FileName);
                var texfile = Path.Combine(dir, Path.GetFileName(i.TexAsset));
                var texfile2= ReplaceFileName(texfile);

                File.WriteAllBytes(texfile2, texdata.ImageData);

                texparam.Texture    = Path.GetFileName(texfile2);
            }
        }

        private ListViewItem.ListViewSubItem    EditSubItem = null;

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            var pos = listView1.PointToClient(Control.MousePosition);
            var hti = listView1.HitTest(pos);

            if(hti.Item == null || hti.SubItem == null)
                return;

            var index   = hti.Item.SubItems.IndexOf(hti.SubItem);

            if(index == 2)
            {
                var bounds  = hti.SubItem.Bounds;
                bounds      = listView1.RectangleToScreen(bounds);
                bounds      = RectangleToClient(bounds);

                EditSubItem             = hti.SubItem;
                tbLabelEditor.Bounds    = bounds;
                tbLabelEditor.Visible   = true;
                tbLabelEditor.Text      = hti.SubItem.Text;
            }
        }

        private void tbLabelEditor_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyData == Keys.Enter)
            {
                EditSubItem.Text        = tbLabelEditor.Text;
                tbLabelEditor.Visible   = false;
            }
        }

        private void tbLabelEditor_Leave(object sender, EventArgs e)
        {
            EditSubItem.Text    = tbLabelEditor.Text;
        }
    }
}
