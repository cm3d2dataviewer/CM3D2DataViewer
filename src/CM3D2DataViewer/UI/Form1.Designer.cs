namespace CM3D2DataViewer
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if(disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbTabClose = new System.Windows.Forms.ToolStripButton();
            this.tsbDebug = new System.Windows.Forms.ToolStripButton();
            this.tsddbArchive = new System.Windows.Forms.ToolStripDropDownButton();
            this.tscbArch = new System.Windows.Forms.ToolStripComboBox();
            this.tsbRunGame = new System.Windows.Forms.ToolStripButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.dataManagerControl1 = new CM3D2DataViewer.DataManagerControl();
            this.toolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.ImageList = this.imageList1;
            this.tabControl1.Location = new System.Drawing.Point(1105, 26);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(479, 856);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tabControl1_MouseDown);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Close.png");
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(1102, 26);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 856);
            this.splitter1.TabIndex = 2;
            this.splitter1.TabStop = false;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbTabClose,
            this.tsbDebug,
            this.tsddbArchive,
            this.tscbArch,
            this.tsbRunGame});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1584, 26);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbTabClose
            // 
            this.tsbTabClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbTabClose.Image = ((System.Drawing.Image)(resources.GetObject("tsbTabClose.Image")));
            this.tsbTabClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbTabClose.Name = "tsbTabClose";
            this.tsbTabClose.Size = new System.Drawing.Size(23, 23);
            this.tsbTabClose.Text = "タブ閉じる";
            this.tsbTabClose.Visible = false;
            // 
            // tsbDebug
            // 
            this.tsbDebug.Image = ((System.Drawing.Image)(resources.GetObject("tsbDebug.Image")));
            this.tsbDebug.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDebug.Name = "tsbDebug";
            this.tsbDebug.Size = new System.Drawing.Size(83, 23);
            this.tsbDebug.Text = "デバッグ1";
            this.tsbDebug.Visible = false;
            this.tsbDebug.Click += new System.EventHandler(this.tsbDebug_Click);
            // 
            // tsddbArchive
            // 
            this.tsddbArchive.Enabled = false;
            this.tsddbArchive.Image = ((System.Drawing.Image)(resources.GetObject("tsddbArchive.Image")));
            this.tsddbArchive.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsddbArchive.Name = "tsddbArchive";
            this.tsddbArchive.Size = new System.Drawing.Size(121, 23);
            this.tsddbArchive.Text = "アーカイブ作成";
            this.tsddbArchive.Visible = false;
            // 
            // tscbArch
            // 
            this.tscbArch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tscbArch.Items.AddRange(new object[] {
            "x86",
            "x64"});
            this.tscbArch.Name = "tscbArch";
            this.tscbArch.Size = new System.Drawing.Size(121, 26);
            this.tscbArch.SelectedIndexChanged += new System.EventHandler(this.tscbArch_SelectedIndexChanged);
            // 
            // tsbRunGame
            // 
            this.tsbRunGame.Enabled = false;
            this.tsbRunGame.Image = ((System.Drawing.Image)(resources.GetObject("tsbRunGame.Image")));
            this.tsbRunGame.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRunGame.Name = "tsbRunGame";
            this.tsbRunGame.Size = new System.Drawing.Size(88, 23);
            this.tsbRunGame.Text = "ゲーム起動";
            this.tsbRunGame.Click += new System.EventHandler(this.tsbRunGame_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 26);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(302, 856);
            this.panel1.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(302, 856);
            this.label1.TabIndex = 0;
            this.label1.Text = "GameDataのディレクトリをここにドラッグ＆ドロップしてください";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBox1
            // 
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.textBox1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBox1.Location = new System.Drawing.Point(0, 886);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(1584, 36);
            this.textBox1.TabIndex = 5;
            this.textBox1.WordWrap = false;
            // 
            // splitter2
            // 
            this.splitter2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter2.Location = new System.Drawing.Point(0, 882);
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(1584, 4);
            this.splitter2.TabIndex = 6;
            this.splitter2.TabStop = false;
            // 
            // dataManagerControl1
            // 
            this.dataManagerControl1.DataManager = null;
            this.dataManagerControl1.Dock = System.Windows.Forms.DockStyle.Left;
            this.dataManagerControl1.Location = new System.Drawing.Point(302, 26);
            this.dataManagerControl1.Name = "dataManagerControl1";
            this.dataManagerControl1.Size = new System.Drawing.Size(800, 856);
            this.dataManagerControl1.TabIndex = 1;
            this.dataManagerControl1.Visible = false;
            // 
            // Form1
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1584, 922);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.dataManagerControl1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.splitter2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "Form1";
            this.Text = "CM3DDataViewer";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Form1_DragDrop);
            this.DragOver += new System.Windows.Forms.DragEventHandler(this.Form1_DragOver);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private DataManagerControl dataManagerControl1;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbTabClose;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripButton tsbDebug;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Splitter splitter2;
        private System.Windows.Forms.ToolStripDropDownButton tsddbArchive;
        private System.Windows.Forms.ToolStripComboBox tscbArch;
        private System.Windows.Forms.ToolStripButton tsbRunGame;
        private System.Windows.Forms.ImageList imageList1;
    }
}

