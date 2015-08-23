namespace CM3D2DataViewer
{
    partial class CM3D2MaterialControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CM3D2MaterialControl));
            this.tbScript = new System.Windows.Forms.TextBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbFileOpen = new System.Windows.Forms.ToolStripButton();
            this.tsbDirectoryOpen = new System.Windows.Forms.ToolStripButton();
            this.tsbFileSave = new System.Windows.Forms.ToolStripButton();
            this.tsbLoadBackup = new System.Windows.Forms.ToolStripButton();
            this.tbDescription = new System.Windows.Forms.TextBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbScript
            // 
            this.tbScript.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbScript.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.tbScript.Location = new System.Drawing.Point(0, 99);
            this.tbScript.Multiline = true;
            this.tbScript.Name = "tbScript";
            this.tbScript.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbScript.Size = new System.Drawing.Size(715, 346);
            this.tbScript.TabIndex = 2;
            this.tbScript.WordWrap = false;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Enabled = false;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbFileOpen,
            this.tsbDirectoryOpen,
            this.tsbFileSave,
            this.tsbLoadBackup});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(715, 25);
            this.toolStrip1.TabIndex = 3;
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
            // tsbLoadBackup
            // 
            this.tsbLoadBackup.Image = ((System.Drawing.Image)(resources.GetObject("tsbLoadBackup.Image")));
            this.tsbLoadBackup.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbLoadBackup.Name = "tsbLoadBackup";
            this.tsbLoadBackup.Size = new System.Drawing.Size(148, 22);
            this.tsbLoadBackup.Text = "バックアップ読み込み";
            this.tsbLoadBackup.Visible = false;
            this.tsbLoadBackup.Click += new System.EventHandler(this.tsbLoadBackup_Click);
            // 
            // tbDescription
            // 
            this.tbDescription.Dock = System.Windows.Forms.DockStyle.Top;
            this.tbDescription.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.tbDescription.Location = new System.Drawing.Point(0, 25);
            this.tbDescription.Multiline = true;
            this.tbDescription.Name = "tbDescription";
            this.tbDescription.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbDescription.Size = new System.Drawing.Size(715, 74);
            this.tbDescription.TabIndex = 4;
            this.tbDescription.WordWrap = false;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 448);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(715, 100);
            this.flowLayoutPanel1.TabIndex = 5;
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter1.Location = new System.Drawing.Point(0, 445);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(715, 3);
            this.splitter1.TabIndex = 6;
            this.splitter1.TabStop = false;
            // 
            // CM3D2MaterialControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tbScript);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.tbDescription);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "CM3D2MaterialControl";
            this.Size = new System.Drawing.Size(715, 548);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbScript;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbFileOpen;
        private System.Windows.Forms.ToolStripButton tsbDirectoryOpen;
        private System.Windows.Forms.ToolStripButton tsbFileSave;
        private System.Windows.Forms.TextBox tbDescription;
        private System.Windows.Forms.ToolStripButton tsbLoadBackup;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Splitter splitter1;
    }
}
