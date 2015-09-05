namespace CM3D2DataViewer
{
    partial class ItemCloneControl
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
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tbNewString1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbOldString1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.bCopyFiles = new System.Windows.Forms.Button();
            this.bListing = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listView1.FullRowSelect = true;
            this.listView1.Location = new System.Drawing.Point(3, 34);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(693, 308);
            this.listView1.TabIndex = 9;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "コピー元ファイル";
            this.columnHeader1.Width = 200;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "コピー先ファイル";
            this.columnHeader2.Width = 200;
            // 
            // tbNewString1
            // 
            this.tbNewString1.Location = new System.Drawing.Point(296, 7);
            this.tbNewString1.Name = "tbNewString1";
            this.tbNewString1.Size = new System.Drawing.Size(100, 19);
            this.tbNewString1.TabIndex = 8;
            this.tbNewString1.TextChanged += new System.EventHandler(this.tbNewString1_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(273, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "→";
            // 
            // tbOldString1
            // 
            this.tbOldString1.Location = new System.Drawing.Point(167, 5);
            this.tbOldString1.Name = "tbOldString1";
            this.tbOldString1.Size = new System.Drawing.Size(100, 19);
            this.tbOldString1.TabIndex = 6;
            this.tbOldString1.TextChanged += new System.EventHandler(this.tbOldString1_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(84, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "ファイル名置換:";
            // 
            // bCopyFiles
            // 
            this.bCopyFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bCopyFiles.Enabled = false;
            this.bCopyFiles.Location = new System.Drawing.Point(596, 5);
            this.bCopyFiles.Name = "bCopyFiles";
            this.bCopyFiles.Size = new System.Drawing.Size(100, 23);
            this.bCopyFiles.TabIndex = 10;
            this.bCopyFiles.Text = "ファイルコピー実行";
            this.bCopyFiles.UseVisualStyleBackColor = true;
            this.bCopyFiles.Click += new System.EventHandler(this.bCopyFiles_Click);
            // 
            // bListing
            // 
            this.bListing.Location = new System.Drawing.Point(3, 3);
            this.bListing.Name = "bListing";
            this.bListing.Size = new System.Drawing.Size(75, 23);
            this.bListing.TabIndex = 11;
            this.bListing.Text = "ファイル抽出";
            this.bListing.UseVisualStyleBackColor = true;
            this.bListing.Click += new System.EventHandler(this.bListing_Click);
            // 
            // ItemCloneControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.bListing);
            this.Controls.Add(this.bCopyFiles);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.tbNewString1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbOldString1);
            this.Controls.Add(this.label1);
            this.Name = "ItemCloneControl";
            this.Size = new System.Drawing.Size(699, 345);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.TextBox tbNewString1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbOldString1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button bCopyFiles;
        private System.Windows.Forms.Button bListing;
    }
}
