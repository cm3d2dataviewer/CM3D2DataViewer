namespace CM3D2DataViewer
{
    partial class NewMenuDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if(disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.newMenuControl1 = new CM3D2DataViewer.NewMenuControl();
            this.SuspendLayout();
            // 
            // newMenuControl1
            // 
            this.newMenuControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.newMenuControl1.Location = new System.Drawing.Point(0, 0);
            this.newMenuControl1.Name = "newMenuControl1";
            this.newMenuControl1.NewMenu = null;
            this.newMenuControl1.Size = new System.Drawing.Size(884, 562);
            this.newMenuControl1.SrcMenu = null;
            this.newMenuControl1.TabIndex = 0;
            // 
            // NewMenuDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 562);
            this.Controls.Add(this.newMenuControl1);
            this.Name = "NewMenuDialog";
            this.Text = "NewMenuDialog";
            this.ResumeLayout(false);

        }

        #endregion

        private NewMenuControl newMenuControl1;
    }
}