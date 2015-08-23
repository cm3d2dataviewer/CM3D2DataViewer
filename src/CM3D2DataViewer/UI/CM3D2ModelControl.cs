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
    public partial class CM3D2ModelControl : UserControl
    {
        private ModelFile               data;

        public ModelFile                Data                    { get { return data; } set { SetData(value); } }

        public CM3D2ModelControl()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

          //tscbType.Items.Clear();
          //tscbType.Items.Add("OBJ");
            tscbType.SelectedIndex  = 0;
        }

        private void SetData(ModelFile value)
        {
            if(value == data)
                return;

            data    = value;

            UpdateView();
        }

        private void UpdateView()
        {
            lvBones.Items.Clear();

            if(data == null)
            {
                toolStrip1.Enabled  = false;
                textBox1.Text       =   "";
                return;
            }

                toolStrip1.Enabled  = true;
            var sb  = new StringBuilder();

            sb.AppendFormat("Magic:   \"{0}\"", Data.Magic).AppendLine();
            sb.AppendFormat("Version: \"{0}\"", Data.Version).AppendLine();

            textBox1.Text       = sb.ToString();
            textBox1.Select(0, 0);

            int index   = 0;

            foreach(var i in data.Bones)
            {
                var item= lvBones.Items.Add(index++.ToString());
                item.Tag= i;

                item.SubItems.Add(i.Name);
                item.SubItems.Add(i.ParentID.ToString());
                item.SubItems.Add(string.Join(", ", i.Params.Select(j => j.ToString()).ToArray()));
            }

            index       = 0;

            for(int i= 0, n= data.Mesh.NumVerts; i < n; ++i)
            {
                var v   = data.Mesh.Vertices[i];
                var s   = data.Mesh.Skins[i];

                var item= lvVerts.Items.Add(index++.ToString());
                item.Tag= v;
              //item.Tag= Tuple.Create(v, s);
                
                item.SubItems.Add(v.P.X.ToString("F10"));
                item.SubItems.Add(v.P.Y.ToString("F10"));
                item.SubItems.Add(v.P.Z.ToString("F10"));
                item.SubItems.Add(v.N.X.ToString("F10"));
                item.SubItems.Add(v.N.Y.ToString("F10"));
                item.SubItems.Add(v.N.Z.ToString("F10"));
                item.SubItems.Add(v.T.X.ToString("F10"));
                item.SubItems.Add(v.T.Y.ToString("F10"));
                item.SubItems.Add(string.Format("{0}/{1}", s.B1, s.W1));
                item.SubItems.Add(string.Format("{0}/{1}", s.B2, s.W2));
                item.SubItems.Add(string.Format("{0}/{1}", s.B3, s.W3));
                item.SubItems.Add(string.Format("{0}/{1}", s.B4, 1-s.W1-s.W2-s.W3));
            }

            index   = 0;

            foreach(var prim in data.Mesh.Primitives)
            {
                var lv  = new ListView() { Dock= DockStyle.Fill, View= View.Details, FullRowSelect= true };
                lv.Columns.Add("#");
                lv.Columns.Add("1");
                lv.Columns.Add("2");
                lv.Columns.Add("3");
                var tp  = new TabPage("prim-"+ index++);

                lv.KeyDown  +=lvVerts_KeyDown;

                tp.Controls.Add(lv);
                tabControl1.TabPages.Add(tp);

                index       = 0;
                var indices = prim.Indices;

                for(int i= 0, n= prim.NumIndices; i < n; i+=3)
                {
                    var item= lv.Items.Add(index++.ToString());
                  //item.Tag= Tuple.Create(indices[i+0], indices[i+1], indices[i+2]);

                    item.SubItems.Add(indices[i+0].ToString());
                    item.SubItems.Add(indices[i+1].ToString());
                    item.SubItems.Add(indices[i+2].ToString());
                }
            }

            // マテリアル
            foreach(var mat in data.Materials)
            {
                var tb  = new TextBox() { Dock= DockStyle.Fill, Multiline= true, Font= textBox1.Font };
                var tp  = new TabPage(mat.Descriptions[0]);

                tp.Controls.Add(tb);
                tabControl1.TabPages.Add(tp);

                sb.Length   = 0;

                sb.AppendFormat("Description:").AppendLine();
            
                foreach(var i in mat.Descriptions)
                    sb.AppendFormat("  \"{0}\"", i).AppendLine();

                sb.AppendFormat("Params:").AppendLine();

                foreach(var i in mat.Params)
                {
                    if(i is ParamTex)
                    {
                        var param   = (ParamTex)i;

                        sb.AppendFormat("    {0} : texture", i.Name).AppendLine();
                        sb.AppendFormat("        SubType:  {0}", param.SubType).AppendLine();
                        sb.AppendFormat("        TexName:  {0}", param.TexName).AppendLine();
                        sb.AppendFormat("        TexAsset: {0}", param.TexAsset).AppendLine();
                        sb.AppendFormat("        Color:    {0:F4}, {1:F4}, {2:F4}, {3:F4}",
                            param.R, param.G, param.B, param.A).AppendLine();
                    } else
                    if(i is ParamCol)
                    {
                        var param   = (ParamCol)i;

                        sb.AppendFormat("    {0} : color", i.Name).AppendLine();
                        sb.AppendFormat("        Color:    {0:F4}, {1:F4}, {2:F4}, {3:F4}",
                            param.R, param.G, param.B, param.A).AppendLine();
                    } else
                    if(i is ParamF)
                    {
                        var param   = (ParamF)i;

                        sb.AppendFormat("    {0} : float", i.Name).AppendLine();
                        sb.AppendFormat("        Value:    {0:F4}", param.Value).AppendLine();
                    }
                }

                tb.Text = sb.ToString();
                tb.Select(0, 0);
            }

            // モーフィング
            foreach(var i in Data.Params.OfType<ParamMorph>())
            {
                var mp  = new CM3D2MorphControl() { Dock= DockStyle.Fill, Data= i };
                var tp  = new TabPage(i.Name);

                tp.Controls.Add(mp);
                tabControl1.TabPages.Add(tp);
            }
        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void tsbMqoOpen_Click(object sender, EventArgs e)
        {
            try
            {
                tsbExport_Click(sender, e);

                var file    = Path.ChangeExtension(data.FileName, "."+tscbType.Text);

                System.Diagnostics.Process.Start(file);
            } catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void tsbExport_Click(object sender, EventArgs e)
        {
            try
            {
                switch(tscbType.Text)
                {
                case "OBJ": ExportOBJ();    break;
                case "MQO": ExportMQO();    break;
                case "DAE": ExportDAE();    break;
                }
            } catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void tsbImport_Click(object sender, EventArgs e)
        {
            try
            {
                switch(tscbType.Text)
                {
                case "OBJ": ImportOBJ();    break;
              //case "MQO": ImportMQO();    break;
              //case "DAE": ImportDAE();    break;
                }
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

        private void tsbDebug_Click(object sender, EventArgs e)
        {
            ModelFile.ToFile(Path.ChangeExtension(Data.FileName, ".@model"), Data);
        }

        private void lvVerts_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.C && e.Control)
            {
                var lv  = (ListView)sender;
                var sb  = new StringBuilder();

                foreach(ListViewItem i in lv.Items)
                {
                    var subs    = i.SubItems.Cast<ListViewItem.ListViewSubItem>();

                    sb.AppendLine(string.Join("\t", subs.Select(j => j.Text).ToArray()));
                }

                Clipboard.SetText(sb.ToString());
            }
        }
    }
}
