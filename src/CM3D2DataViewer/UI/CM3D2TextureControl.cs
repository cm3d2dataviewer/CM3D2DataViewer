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
    public partial class CM3D2TextureControl : UserControl
    {
        private TexFile                 data;

        public TexFile                  Data                    { get { return data; } set { SetData(value); } }

        public CM3D2TextureControl()
        {
            InitializeComponent();
        }

        private void SetData(TexFile value)
        {
            if(value == data)
                return;

            data    = value;

            UpdateView();
        }

        private void UpdateView()
        {
            if(data == null)
            {
                toolStrip1.Enabled  = false;
                textBox1.Text       =   "";
                pictureBox1.Image   = null;
                return;
            }

            toolStrip1.Enabled  = true;
            var sb  = new StringBuilder();

            sb.AppendFormat("Magic:   \"{0}\"", Data.Magic).AppendLine();
            sb.AppendFormat("Version: \"{0}\"", Data.Version).AppendLine();
            sb.AppendFormat("Path:    \"{0}\"", Data.AssetPath).AppendLine();

            pictureBox1.Image   = Data.GetImage();
            textBox1.Text       = sb.ToString();
            textBox1.Select(0, 0);
        }

        private void tsbImport_Click(object sender, EventArgs e)
        {
            var dir     = Path.GetDirectoryName(Data.FileName);
            var orgdir  = Path.Combine(dir, "original");
            var name    = Path.GetFileName(Data.AssetPath);
            var orgfile = Path.Combine(dir, name);

            if(!Directory.Exists(orgdir))
                Directory.CreateDirectory(orgdir);

            if(!File.Exists(Path.Combine(orgdir, name)))
                pictureBox1.Image.Save(Path.Combine(orgdir, name));

            var expfile = Path.Combine(dir, name);

            Data.SetImageData(File.ReadAllBytes(expfile));

            TexFile.ToFile(Data.FileName, Data);

            UpdateView();
        }

        private void tsbExport_Click(object sender, EventArgs e)
        {
            var dir     = Path.GetDirectoryName(Data.FileName);
            var name    = Path.GetFileName(Data.AssetPath);

            pictureBox1.Image.Save(Path.Combine(dir, name));
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

        private void tsbImageOpen_Click(object sender, EventArgs e)
        {
            try
            {
                var dir     = Path.GetDirectoryName(Data.FileName);
                var name    = Path.GetFileName(Data.AssetPath);
                var file    = Path.Combine(dir, name);

                if(!File.Exists(file))
                    Data.GetImage().Save(file);

                System.Diagnostics.Process.Start(file);
            } catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public Point                DownPos;
        public int                  DragState;

        private void pictureBox1_MouseCaptureChanged(object sender, EventArgs e)
        {
            if(!pictureBox1.Capture)
                DragState   = 0;

        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                DownPos             = e.Location;
                pictureBox1.Capture = true;
                DragState           = 1;
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if(DragState == 1)
            {
                var x   = e.X - DownPos.X;
                var y   = e.Y - DownPos.Y;

                if(x*x+y*y >= 16)
                {
                    DragState   = 2;

                    var dir = Path.GetDirectoryName(Data.FileName);
                    var name= Path.GetFileName(Data.AssetPath);
                    var file= Path.Combine(dir, name);

                    if(File.Exists(file))
                    {
                        var data= new DataObject(DataFormats.FileDrop, new string[] {  file });

                        DoDragDrop(data, DragDropEffects.Copy);
                    }
                }
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                pictureBox1.Capture = false;
                DragState           = 0;
            }
        }

        private void tsbCopyToClipboard_Click(object sender, EventArgs e)
        {
            Clipboard.SetImage(pictureBox1.Image);
        }
    }
}
