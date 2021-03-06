﻿namespace CM3D2DataViewer
{
    partial class CM3D2MenuControl
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CM3D2MenuControl));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbFileOpen = new System.Windows.Forms.ToolStripButton();
            this.tsbDirectoryOpen = new System.Windows.Forms.ToolStripButton();
            this.tsbFileSave = new System.Windows.Forms.ToolStripButton();
            this.tsbLoadBackup = new System.Windows.Forms.ToolStripButton();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.tbDescription = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tbScript = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.bDeleteFiles = new System.Windows.Forms.Button();
            this.tvRelationTree = new System.Windows.Forms.TreeView();
            this.bAnalysisRelation = new System.Windows.Forms.Button();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.itemCloneControl1 = new CM3D2DataViewer.ItemCloneControl();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.toolStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Enabled = false;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbFileOpen,
            this.tsbDirectoryOpen,
            this.tsbFileSave,
            this.tsbLoadBackup});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(696, 25);
            this.toolStrip1.TabIndex = 2;
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
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 401);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(696, 100);
            this.flowLayoutPanel1.TabIndex = 3;
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter1.Location = new System.Drawing.Point(0, 398);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(696, 3);
            this.splitter1.TabIndex = 4;
            this.splitter1.TabStop = false;
            // 
            // tbDescription
            // 
            this.tbDescription.Dock = System.Windows.Forms.DockStyle.Top;
            this.tbDescription.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.tbDescription.Location = new System.Drawing.Point(0, 25);
            this.tbDescription.Multiline = true;
            this.tbDescription.Name = "tbDescription";
            this.tbDescription.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbDescription.Size = new System.Drawing.Size(696, 86);
            this.tbDescription.TabIndex = 5;
            this.tbDescription.WordWrap = false;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.ImageList = this.imageList1;
            this.tabControl1.Location = new System.Drawing.Point(0, 111);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(696, 287);
            this.tabControl1.TabIndex = 6;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.tbScript);
            this.tabPage1.ImageIndex = 0;
            this.tabPage1.Location = new System.Drawing.Point(4, 23);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(688, 260);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "スクリプト";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tbScript
            // 
            this.tbScript.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbScript.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.tbScript.Location = new System.Drawing.Point(3, 3);
            this.tbScript.Multiline = true;
            this.tbScript.Name = "tbScript";
            this.tbScript.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbScript.Size = new System.Drawing.Size(682, 254);
            this.tbScript.TabIndex = 2;
            this.tbScript.WordWrap = false;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.bDeleteFiles);
            this.tabPage2.Controls.Add(this.tvRelationTree);
            this.tabPage2.Controls.Add(this.bAnalysisRelation);
            this.tabPage2.ImageIndex = 1;
            this.tabPage2.Location = new System.Drawing.Point(4, 23);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(688, 260);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "関連リソース";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // bDeleteFiles
            // 
            this.bDeleteFiles.Enabled = false;
            this.bDeleteFiles.Location = new System.Drawing.Point(132, 6);
            this.bDeleteFiles.Name = "bDeleteFiles";
            this.bDeleteFiles.Size = new System.Drawing.Size(120, 23);
            this.bDeleteFiles.TabIndex = 2;
            this.bDeleteFiles.Text = "削除";
            this.bDeleteFiles.UseVisualStyleBackColor = true;
            this.bDeleteFiles.Click += new System.EventHandler(this.bDeleteFiles_Click);
            // 
            // tvRelationTree
            // 
            this.tvRelationTree.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tvRelationTree.CheckBoxes = true;
            this.tvRelationTree.Location = new System.Drawing.Point(6, 35);
            this.tvRelationTree.Name = "tvRelationTree";
            this.tvRelationTree.Size = new System.Drawing.Size(676, 219);
            this.tvRelationTree.TabIndex = 1;
            this.tvRelationTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvRelationTree_AfterSelect);
            this.tvRelationTree.DoubleClick += new System.EventHandler(this.tvRelationTree_DoubleClick);
            // 
            // bAnalysisRelation
            // 
            this.bAnalysisRelation.Enabled = false;
            this.bAnalysisRelation.Location = new System.Drawing.Point(6, 6);
            this.bAnalysisRelation.Name = "bAnalysisRelation";
            this.bAnalysisRelation.Size = new System.Drawing.Size(120, 23);
            this.bAnalysisRelation.TabIndex = 0;
            this.bAnalysisRelation.Text = "関連リソースの解析";
            this.bAnalysisRelation.UseVisualStyleBackColor = true;
            this.bAnalysisRelation.Click += new System.EventHandler(this.bAnalysisRelation_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.itemCloneControl1);
            this.tabPage3.ImageIndex = 2;
            this.tabPage3.Location = new System.Drawing.Point(4, 23);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(688, 260);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "コピー";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // itemCloneControl1
            // 
            this.itemCloneControl1.Data = null;
            this.itemCloneControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.itemCloneControl1.Enabled = false;
            this.itemCloneControl1.Location = new System.Drawing.Point(3, 3);
            this.itemCloneControl1.Name = "itemCloneControl1";
            this.itemCloneControl1.Size = new System.Drawing.Size(682, 254);
            this.itemCloneControl1.TabIndex = 0;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "ic_list_black_18dp.png");
            this.imageList1.Images.SetKeyName(1, "ic_share_black_18dp.png");
            this.imageList1.Images.SetKeyName(2, "ic_content_copy_black_18dp.png");
            // 
            // CM3D2MenuControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.tbDescription);
            this.Controls.Add(this.toolStrip1);
            this.Name = "CM3D2MenuControl";
            this.Size = new System.Drawing.Size(696, 501);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbFileOpen;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.ToolStripButton tsbDirectoryOpen;
        private System.Windows.Forms.ToolStripButton tsbFileSave;
        private System.Windows.Forms.TextBox tbDescription;
        private System.Windows.Forms.ToolStripButton tsbLoadBackup;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TextBox tbScript;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TreeView tvRelationTree;
        private System.Windows.Forms.Button bAnalysisRelation;
        private System.Windows.Forms.TabPage tabPage3;
        private ItemCloneControl itemCloneControl1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Button bDeleteFiles;
    }
}
