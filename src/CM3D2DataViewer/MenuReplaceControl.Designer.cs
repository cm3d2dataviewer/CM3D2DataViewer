namespace CM3D2DataViewer
{
    partial class MenuReplaceControl
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tbSrcScript = new System.Windows.Forms.TextBox();
            this.tbNewScript = new System.Windows.Forms.TextBox();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tbSrcScript);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tbNewScript);
            this.splitContainer1.Size = new System.Drawing.Size(779, 503);
            this.splitContainer1.SplitterDistance = 386;
            this.splitContainer1.TabIndex = 3;
            // 
            // tbSrcScript
            // 
            this.tbSrcScript.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbSrcScript.Location = new System.Drawing.Point(0, 0);
            this.tbSrcScript.Multiline = true;
            this.tbSrcScript.Name = "tbSrcScript";
            this.tbSrcScript.ReadOnly = true;
            this.tbSrcScript.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbSrcScript.Size = new System.Drawing.Size(386, 503);
            this.tbSrcScript.TabIndex = 3;
            // 
            // tbNewScript
            // 
            this.tbNewScript.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbNewScript.Location = new System.Drawing.Point(0, 0);
            this.tbNewScript.Multiline = true;
            this.tbNewScript.Name = "tbNewScript";
            this.tbNewScript.ReadOnly = true;
            this.tbNewScript.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbNewScript.Size = new System.Drawing.Size(389, 503);
            this.tbNewScript.TabIndex = 5;
            // 
            // MenuReplaceControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "MenuReplaceControl";
            this.Size = new System.Drawing.Size(779, 503);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TextBox tbSrcScript;
        private System.Windows.Forms.TextBox tbNewScript;
    }
}
