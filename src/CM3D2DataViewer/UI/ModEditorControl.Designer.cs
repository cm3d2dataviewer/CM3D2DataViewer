namespace CM3D2DataViewer
{
    partial class ModEditorControl
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

        #region コンポーネント デザイナーで生成されたコード

        /// <summary> 
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ModEditorControl));
            this.tbDescription = new System.Windows.Forms.TextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbFileOpen = new System.Windows.Forms.ToolStripButton();
            this.tsbDirectoryOpen = new System.Windows.Forms.ToolStripButton();
            this.tsbFileSave = new System.Windows.Forms.ToolStripButton();
            this.tsbCopyFilePath = new System.Windows.Forms.ToolStripButton();
            this.tsbCopyTextures = new System.Windows.Forms.ToolStripButton();
            this.tsbOpenBaseMenu = new System.Windows.Forms.ToolStripButton();
            this.tsbModEnable = new System.Windows.Forms.ToolStripButton();
            this.tsbModDisable = new System.Windows.Forms.ToolStripButton();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.toolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbDescription
            // 
            this.tbDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbDescription.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.tbDescription.Location = new System.Drawing.Point(3, 3);
            this.tbDescription.Multiline = true;
            this.tbDescription.Name = "tbDescription";
            this.tbDescription.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbDescription.Size = new System.Drawing.Size(785, 133);
            this.tbDescription.TabIndex = 0;
            this.tbDescription.WordWrap = false;
            this.tbDescription.TextChanged += new System.EventHandler(this.tbDescription_TextChanged);
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(47, 142);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(221, 20);
            this.comboBox1.TabIndex = 1;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 145);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "スロット";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbFileOpen,
            this.tsbDirectoryOpen,
            this.tsbFileSave,
            this.tsbCopyFilePath,
            this.tsbCopyTextures,
            this.tsbOpenBaseMenu,
            this.tsbModEnable,
            this.tsbModDisable});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(791, 50);
            this.toolStrip1.TabIndex = 9;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbFileOpen
            // 
            this.tsbFileOpen.Image = ((System.Drawing.Image)(resources.GetObject("tsbFileOpen.Image")));
            this.tsbFileOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbFileOpen.Name = "tsbFileOpen";
            this.tsbFileOpen.Size = new System.Drawing.Size(112, 22);
            this.tsbFileOpen.Text = "ファイルを開く";
            this.tsbFileOpen.Click += new System.EventHandler(this.tsbFileOpen_Click);
            // 
            // tsbDirectoryOpen
            // 
            this.tsbDirectoryOpen.Image = ((System.Drawing.Image)(resources.GetObject("tsbDirectoryOpen.Image")));
            this.tsbDirectoryOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDirectoryOpen.Name = "tsbDirectoryOpen";
            this.tsbDirectoryOpen.Size = new System.Drawing.Size(136, 22);
            this.tsbDirectoryOpen.Text = "ディレクトリを開く";
            this.tsbDirectoryOpen.Click += new System.EventHandler(this.tsbDirectoryOpen_Click);
            // 
            // tsbFileSave
            // 
            this.tsbFileSave.Image = ((System.Drawing.Image)(resources.GetObject("tsbFileSave.Image")));
            this.tsbFileSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbFileSave.Name = "tsbFileSave";
            this.tsbFileSave.Size = new System.Drawing.Size(52, 22);
            this.tsbFileSave.Text = "保存";
            this.tsbFileSave.Click += new System.EventHandler(this.tsbFileSave_Click);
            // 
            // tsbCopyFilePath
            // 
            this.tsbCopyFilePath.Image = ((System.Drawing.Image)(resources.GetObject("tsbCopyFilePath.Image")));
            this.tsbCopyFilePath.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCopyFilePath.Name = "tsbCopyFilePath";
            this.tsbCopyFilePath.Size = new System.Drawing.Size(184, 22);
            this.tsbCopyFilePath.Text = "ディレクトリのパスをコピー";
            this.tsbCopyFilePath.Click += new System.EventHandler(this.tsbCopyFilePath_Click);
            // 
            // tsbCopyTextures
            // 
            this.tsbCopyTextures.Image = ((System.Drawing.Image)(resources.GetObject("tsbCopyTextures.Image")));
            this.tsbCopyTextures.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCopyTextures.Name = "tsbCopyTextures";
            this.tsbCopyTextures.Size = new System.Drawing.Size(244, 22);
            this.tsbCopyTextures.Text = "基本アイテムのテクスチャをインポート";
            this.tsbCopyTextures.Click += new System.EventHandler(this.tsbCopyTextures_Click);
            // 
            // tsbOpenBaseMenu
            // 
            this.tsbOpenBaseMenu.Image = ((System.Drawing.Image)(resources.GetObject("tsbOpenBaseMenu.Image")));
            this.tsbOpenBaseMenu.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbOpenBaseMenu.Name = "tsbOpenBaseMenu";
            this.tsbOpenBaseMenu.Size = new System.Drawing.Size(229, 22);
            this.tsbOpenBaseMenu.Text = "基本アイテムのmenuファイルを開く";
            this.tsbOpenBaseMenu.Click += new System.EventHandler(this.tsbOpenBaseMenu_Click);
            // 
            // tsbModEnable
            // 
            this.tsbModEnable.Image = ((System.Drawing.Image)(resources.GetObject("tsbModEnable.Image")));
            this.tsbModEnable.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbModEnable.Name = "tsbModEnable";
            this.tsbModEnable.Size = new System.Drawing.Size(138, 22);
            this.tsbModEnable.Text = "modファイルを適用";
            this.tsbModEnable.Click += new System.EventHandler(this.tsbModEnable_Click);
            // 
            // tsbModDisable
            // 
            this.tsbModDisable.Image = ((System.Drawing.Image)(resources.GetObject("tsbModDisable.Image")));
            this.tsbModDisable.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbModDisable.Name = "tsbModDisable";
            this.tsbModDisable.Size = new System.Drawing.Size(138, 22);
            this.tsbModDisable.Text = "modファイルを削除";
            this.tsbModDisable.Click += new System.EventHandler(this.tsbModDisable_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Location = new System.Drawing.Point(3, 168);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(785, 382);
            this.tabControl1.TabIndex = 3;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tbDescription);
            this.panel1.Controls.Add(this.comboBox1);
            this.panel1.Controls.Add(this.tabControl1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 50);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(791, 553);
            this.panel1.TabIndex = 10;
            // 
            // ModEditorControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "ModEditorControl";
            this.Size = new System.Drawing.Size(791, 603);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbDescription;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbFileOpen;
        private System.Windows.Forms.ToolStripButton tsbDirectoryOpen;
        private System.Windows.Forms.ToolStripButton tsbFileSave;
        private System.Windows.Forms.ToolStripButton tsbCopyFilePath;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripButton tsbOpenBaseMenu;
        private System.Windows.Forms.ToolStripButton tsbCopyTextures;
        private System.Windows.Forms.ToolStripButton tsbModEnable;
        private System.Windows.Forms.ToolStripButton tsbModDisable;
    }
}
