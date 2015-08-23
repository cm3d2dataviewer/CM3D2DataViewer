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
    public partial class CM3D2MaterialControl : UserControl
    {
        private MateFile                data;

        public MateFile                 Data                    { get { return data; } set { SetData(value); } }

        public event EventHandler<OpenItemEventArgs>    OpenItem;

        public CM3D2MaterialControl()
        {
            InitializeComponent();
        }

        private void SetData(MateFile value)
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
                tbScript.Text       = "";
                return;
            }

            toolStrip1.Enabled  = true;
            var sb      = new StringBuilder();

            foreach(var i in Data.Descriptions)
                sb.AppendFormat("{0}", i).AppendLine();

            tbDescription.Text  = sb.ToString();
            tbDescription.Select(0, 0);

            sb.Length   = 0;
            Button  b;

            foreach(var i in Data.Params)
            {
                if(i is ParamTex)
                {
                    var param   = (ParamTex)i;

                    if(param.TexName == null)
                    {
                        sb.AppendFormat("tex {0} {1} null", i.Name, param.SubType).AppendLine();
                    } else
                    {
                        sb.AppendFormat("tex {0} {1} {2} {3} {4:F4} {5:F4} {6:F4} {7:F4}",
                            i.Name.PadRight(16),
                            param.SubType, param.TexName.PadRight(24), param.TexAsset.PadRight(80),
                            param.R, param.G, param.B, param.A).AppendLine();

                        var texname = Path.GetFileName(Path.ChangeExtension(param.TexAsset, ".tex"));

                        b       = new Button() { AutoSize= true, Text= texname, Tag= i };
                        b.Click += B_Click;

                        flowLayoutPanel1.Controls.Add(b);
                    }
                } else
                if(i is ParamCol)
                {
                    var param   = (ParamCol)i;

                    sb.AppendFormat("col {0} {1:F4} {2:F4} {3:F4} {4:F4}",
                        i.Name.PadRight(16),
                        param.R, param.G, param.B, param.A).AppendLine();
                } else
                if(i is ParamF)
                {
                    var param   = (ParamF)i;

                    sb.AppendFormat("f   {0} {1:F4}",
                        i.Name.PadRight(16),
                        param.Value).AppendLine();
                } else
                if(i is ParamEnd)
                {
                } else
                {
                    sb.AppendFormat("{0} {1}", i.Type.PadRight(3), i.Name).AppendLine();
                }
            }

            tbScript.Text   = sb.ToString();
            tbScript.Select(0, 0);
        }

        private void B_Click(object sender, EventArgs e)
        {
            var c   = (Control)sender;

            Open(c.Text);
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

        private void tsbFileSave_Click(object sender, EventArgs e)
        {
            try
            {
                var desc            = tbDescription.Lines.Select(i => i.Trim()).Where(i => i.Length > 0).ToList();
                var param           = new List<Param>();
                var delim           = " ".ToArray();
                var lineno          = 0;
                var error           = false;

                while(desc.Count < Data.Descriptions.Count)   desc.Add("");
                while(desc.Count > Data.Descriptions.Count)   desc.RemoveAt(Data.Descriptions.Count);

                Globals.Message("{0}: マテリアルデータ保存開始", DateTime.Now.ToString());

                foreach(var i in tbScript.Lines)
                {
                    ++lineno;

                    try
                    {
                        var line    = i.Trim();

                        if(line.Length == 0)
                            continue;

                        var t   = i.Split(delim, StringSplitOptions.RemoveEmptyEntries);

                        switch(t[0])
                        {
                        case "tex":
                            if(t.Length == 3)
                            {
                                param.Add(new ParamTex()
                                {
                                    Name    = t[1],
                                    SubType = t[2],
                                });
                            } else
                            if(t.Length == 9)
                            {
                                try
                                {
                                    param.Add(new ParamTex()
                                    {
                                        Name    = t[1],
                                        SubType = t[2],
                                        TexName = t[3],
                                        TexAsset= t[4],
                                        R       = float.Parse(t[5]),
                                        G       = float.Parse(t[6]),
                                        B       = float.Parse(t[7]),
                                        A       = float.Parse(t[8]),
                                    });
                                } catch(FormatException ex)
                                {
                                    Globals.Message("{0}行目: 数値の形式が異常です。", lineno);
                                    error   = true;
                                }
                            } else
                            {
                                Globals.Message("{0}行目: texのパラメータ指定が無効です。", lineno);
                                error   = true;
                            }

                            break;

                        case "col":
                            if(t.Length == 6)
                            {
                                try
                                {
                                    param.Add(new ParamCol()
                                    {
                                        Name    = t[1],
                                        R       = float.Parse(t[2]),
                                        G       = float.Parse(t[3]),
                                        B       = float.Parse(t[4]),
                                        A       = float.Parse(t[5]),
                                    });
                                } catch(FormatException ex)
                                {
                                    Globals.Message("{0}行目: 数値の形式が異常です。", lineno);
                                    error   = true;
                                }
                            } else
                            {
                                Globals.Message("{0}行目: colのパラメータ指定が無効です。", lineno);
                                error   = true;
                            }

                            break;

                        case "f":
                            if(t.Length == 3)
                            {
                                try
                                {
                                    param.Add(new ParamF()
                                    {
                                        Name    = t[1],
                                        Value   = float.Parse(t[2]),
                                    });
                                } catch(FormatException ex)
                                {
                                    Globals.Message("{0}行目: 数値の形式が異常です。", lineno);
                                    error   = true;
                                }
                            } else
                            {
                                Globals.Message("{0}行目: fのパラメータ指定が無効です。", lineno);
                                error   = true;
                            }

                            break;

                        default:
                            Globals.Message("{0}行目: 無効なパラメータタイプです。{1}", t[0]);
                            error   = true;
                            break;
                        }
                    } catch(FormatException ex)
                    {
                        Globals.Message("{0}行目: データ作成中にエラーが発生しました。{1}", ex.GetType().Name);
                    }
                }

                if(error)
                {
                    Globals.Message("{0}: マテリアルデータ保存失敗", DateTime.Now.ToString());
                    MessageBox.Show("データ作成中にエラーが発生しました。\nメッセージ出力を確認してください。");
                    return;
                }

                Data.Descriptions   = desc;
                Data.Params         = param;

                Data.Backup();

                MateFile.ToFile(Data.FileName, Data);

                Globals.Message("{0}: マテリアルデータ保存完了", DateTime.Now.ToString());
            } catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void tsbLoadBackup_Click(object sender, EventArgs e)
        {
            try
            {
                var data        = MateFile.FromFile(Data.GetBackupFileName());
                data.FileName   = Path.ChangeExtension(data.FileName, ".mate");
                Data            = data;
            } catch {}
        }
    }
}
