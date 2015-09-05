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
    public partial class ModMateralControl : UserControl
    {
        private ModMaterial             data;

        public ModMaterial              Data                    { get { return data; } set { SetData(value); } }

        public ModMateralControl()
        {
            InitializeComponent();
        }

        private void SetData(ModMaterial value)
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

            foreach(var i in data.Textures)
            {
                switch(i.Name)
                {
                case "_MainTex":            SetTex(i, cb_MainTex,        tb_MainTex);        break;
			    case "_ToonRamp":           SetTex(i, cb_ToonRamp,       tb_ToonRamp);       break;
			    case "_ShadowTex":          SetTex(i, cb_ShadowTex,      tb_ShadowTex);      break;
			    case "_ShadowRateToon":     SetTex(i, cb_ShadowRateToon, tb_ShadowRateToon); break;
                }
            }

            foreach(var i in data.Colors)
            {
                switch(i.Name)
                {
                case "_Color":              SetCol(i, cb_Color,        pb_Color,        nud_ColorR,        nud_ColorG,        nud_ColorB,        nud_ColorA);        break;
			    case "_ShadowColor":        SetCol(i, cb_ShadowColor,  pb_ShadowColor,  nud_ShadowColorR,  nud_ShadowColorG,  nud_ShadowColorB,  nud_ShadowColorA);  break;
			    case "_RimColor":           SetCol(i, cb_RimColor,     pb_RimColor,     nud_RimColorR,     nud_RimColorG,     nud_RimColorB,     nud_RimColorA);     break;
			    case "_OutlineColor":       SetCol(i, cb_OutlineColor, pb_OutlineColor, nud_OutlineColorR, nud_OutlineColorG, nud_OutlineColorB, nud_OutlineColorA); break;
                }
            }

            foreach(var i in data.Values)
            {
                switch(i.Name)
                {
                case "_Shininess":          SetF(i, cb_Shininess, nud_Shininess); break;
                }
            }
        }

        private ModCol GetCol(string name)          { return Data.Colors.FirstOrDefault(i => i.Name == name); }
        private ModCol GetCol(CheckBox cb)          { return GetCol(cb .Name.Substring(2, cb .Name.Length-2)); }
        private ModCol GetCol(PictureBox pb)        { return GetCol(pb .Name.Substring(2, pb .Name.Length-2)); }
        private ModCol GetCol(NumericUpDown nud)    { return GetCol(nud.Name.Substring(2, nud.Name.Length-3)); }

        private ModTex GetTex(string name)          { return Data.Textures.FirstOrDefault(i => i.Name == name); }
        private ModTex GetTex(CheckBox cb)          { return GetTex(cb .Name.Substring(2, cb .Name.Length-2)); }
        private ModTex GetTex(TextBox tb)           { return GetTex(tb .Name.Substring(2, tb .Name.Length-2)); }
        private ModTex GetTex(Button b)             { return GetTex(b  .Name.Substring(1, b  .Name.Length-1)); }

        private ModValue GetF(string name)          { return Data.Values.FirstOrDefault(i => i.Name == name); }
        private ModValue GetF(CheckBox cb)          { return GetF  (cb .Name.Substring(2, cb .Name.Length-2)); }
        private ModValue GetF(NumericUpDown nud)    { return GetF  (nud.Name.Substring(2, nud.Name.Length-3)); }

        private void SetTex(ModTex tex, CheckBox cb, TextBox tb)
        {
            cb.Checked  = tex.Enabled;
            tb.Text     = tex.Texture;
        }

        private void SetCol(ModCol col, CheckBox cb, PictureBox pb, NumericUpDown r, NumericUpDown g, NumericUpDown b, NumericUpDown a)
        {
            cb.Checked  = col.Enabled;
            pb.BackColor= Color.FromArgb(col.R, col.G, col.B);
            r.Value     = col.R;
            g.Value     = col.G;
            b.Value     = col.B;
            a.Value     = col.A;
        }

        private void SetF(ModValue val, CheckBox cb, NumericUpDown nud)
        {
            cb.Checked  = val.Enabled;
            nud.Value   = val.Value;
        }

        private void UpdateTex(ModTex tex)
        {
            var tb      = Controls["tb"+tex.Name];
            tex.Texture = tb.Text;
        }

        private void UpdateCol(ModCol col)
        {
            var nudR    = (NumericUpDown)Controls["nud"+col.Name+"R"];
            var nudG    = (NumericUpDown)Controls["nud"+col.Name+"G"];
            var nudB    = (NumericUpDown)Controls["nud"+col.Name+"B"];
            var nudA    = (NumericUpDown)Controls["nud"+col.Name+"A"];
            col.R       = (int)nudR.Value;
            col.G       = (int)nudG.Value;
            col.B       = (int)nudB.Value;
            col.A       = (int)nudA.Value;
        }

        private void UpdateF(ModValue val)
        {
            var nud     = (NumericUpDown)Controls["nud"+val.Name];
            val.Value   = nud.Value;
        }

        private void tb_DragDrop(object sender, DragEventArgs e)
        {
            if(Data == null)
                return;

            if(e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                var files   = (string[])e.Data.GetData(DataFormats.FileDrop);

                ((TextBox)sender).Text  = files[0];
            }
        }

        private void tb_DragOver(object sender, DragEventArgs e)
        {
            if(Data == null)
                return;

            e.Effect    = e.Data.GetDataPresent(DataFormats.FileDrop)
                        ? e.AllowedEffect & DragDropEffects.Copy
                        : DragDropEffects.None;
        }


        private void pb_Click(object sender, EventArgs e)
        {
            if(Data == null)
                return;

            var col     = GetCol((PictureBox)sender);

            if(col == null)
                return;

            var dlg     = new ColorDialog();
            dlg.Color   = Color.FromArgb(col.R, col.G, col.B);

            if(dlg.ShowDialog() != DialogResult.OK)
                return;

            ((NumericUpDown)Controls["nud"+col.Name+"R"]).Value= dlg.Color.R;
            ((NumericUpDown)Controls["nud"+col.Name+"G"]).Value= dlg.Color.G;
            ((NumericUpDown)Controls["nud"+col.Name+"B"]).Value= dlg.Color.B;
        }

        private void nud_ValueChanged(object sender, EventArgs e)
        {
            if(Data == null)
                return;

            var nud = (NumericUpDown)sender;
            var col = GetCol(nud);

            if(col == null)
                return;

            switch(nud.Name.Last())
            {
            case 'R':   col.R= (int)nud.Value; break;
            case 'G':   col.G= (int)nud.Value; break;
            case 'B':   col.B= (int)nud.Value; break;
            case 'A':   col.A= (int)nud.Value; break;
            }
        }

        private void tb_ShadowRateToon_TextChanged(object sender, EventArgs e)
        {
            if(Data == null)
                return;

            var tb  = (TextBox)sender;
            var tex = GetTex(tb.Name);

            if(tex == null)
                return;

            tex.Texture = tb.Text;
        }

        private void cb_tex_CheckedChanged(object sender, EventArgs e)
        {
            if(Data == null)
                return;

            var cb  = (CheckBox)sender;
            var name= cb.Name.Substring(2, cb.Name.Length-2);
            var tex = Data.Textures.FirstOrDefault(i => i.Name == name);

            if(tex != null)
            {
                tex.Enabled = cb.Checked;
            } else
            if(cb.Checked)
            {
                tex = new ModTex() { Name= name, Enabled= true };
                UpdateTex(tex);
                Data.Textures.Add(tex);
            }
        }

        private void cb_col_CheckedChanged(object sender, EventArgs e)
        {
            if(Data == null)
                return;

            var cb  = (CheckBox)sender;
            var name= cb.Name.Substring(2, cb.Name.Length-2);
            var col = Data.Colors.FirstOrDefault(i => i.Name == name);

            if(col != null)
            {
                col.Enabled = cb.Checked;
            } else
            if(cb.Checked)
            {
                col = new ModCol() { Name= name, Enabled= true };
                UpdateCol(col);
                Data.Colors.Add(col);
            }
        }

        private void cb_f_CheckedChanged(object sender, EventArgs e)
        {
            if(Data == null)
                return;

            var cb  = (CheckBox)sender;
            var name= cb.Name.Substring(2, cb.Name.Length-2);
            var val = Data.Values.FirstOrDefault(i => i.Name == name);

            if(val != null)
            {
                val.Enabled = cb.Checked;
            } else
            if(cb.Checked)
            {
                val = new ModValue() { Name= name, Enabled= true };
                UpdateF(val);
                Data.Values.Add(val);
            }
        }

        private void b_Click(object sender, EventArgs e)
        {
            var tex = GetTex((Button)sender);

            if(null == tex)
                return;

            var dir     = Path.GetDirectoryName(Data.Owner.Owner.FileName);
            var dlg     = new OpenFileDialog();
            dlg.Filter  = "画像ファイル|*.png;*.dds"
                      //+"|全ての画像ファイル|*.png;*.dds;*.jpg;*.jpeg;*.bmp;*.gif"
                        +"|全てのファイル|*.*";
            dlg.FileName= Path.Combine(dir, tex.Texture);

            if(dlg.ShowDialog() != DialogResult.OK)
                return;

            var srcfile = dlg.FileName;
            var dstfile = Path.Combine(dir, Path.GetFileName(srcfile));

            try
            {
                File.Copy(srcfile, dstfile, true);
            } catch
            {
            }

            tex.Texture = Path.GetFileName(srcfile);

            ((TextBox)Controls["tb"+tex.Name]).Text = tex.Texture;
        }
    }
}
