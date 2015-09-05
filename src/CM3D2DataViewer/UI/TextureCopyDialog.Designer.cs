namespace CM3D2DataViewer
{
    partial class TextureCopyDialog
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
            this.tbMatchString1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbReplaceString1 = new System.Windows.Forms.TextBox();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.bCancel = new System.Windows.Forms.Button();
            this.bCopy = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbReplace2 = new System.Windows.Forms.CheckBox();
            this.tbMatchString2 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbReplaceString2 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbReplace1 = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.tbLabelEditor = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbMatchString1
            // 
            this.tbMatchString1.Location = new System.Drawing.Point(165, 18);
            this.tbMatchString1.Name = "tbMatchString1";
            this.tbMatchString1.Size = new System.Drawing.Size(159, 19);
            this.tbMatchString1.TabIndex = 0;
            this.tbMatchString1.TextChanged += new System.EventHandler(this.tbMatchString_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(68, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "置換対象文字列:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(330, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "置換後文字列:";
            // 
            // tbReplaceString1
            // 
            this.tbReplaceString1.Location = new System.Drawing.Point(415, 18);
            this.tbReplaceString1.Name = "tbReplaceString1";
            this.tbReplaceString1.Size = new System.Drawing.Size(159, 19);
            this.tbReplaceString1.TabIndex = 3;
            this.tbReplaceString1.TextChanged += new System.EventHandler(this.tbReplaceString_TextChanged);
            // 
            // listView1
            // 
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3,
            this.columnHeader1,
            this.columnHeader2});
            this.listView1.LabelEdit = true;
            this.listView1.Location = new System.Drawing.Point(12, 106);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(828, 366);
            this.listView1.TabIndex = 4;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.DoubleClick += new System.EventHandler(this.listView1_DoubleClick);
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "種別";
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "コピー元ファイル名";
            this.columnHeader1.Width = 358;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "コピー先ファイル名";
            this.columnHeader2.Width = 389;
            // 
            // bCancel
            // 
            this.bCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bCancel.Location = new System.Drawing.Point(765, 478);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(75, 23);
            this.bCancel.TabIndex = 5;
            this.bCancel.Text = "キャンセル";
            this.bCancel.UseVisualStyleBackColor = true;
            this.bCancel.Click += new System.EventHandler(this.bCancel_Click);
            // 
            // bCopy
            // 
            this.bCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bCopy.Location = new System.Drawing.Point(684, 478);
            this.bCopy.Name = "bCopy";
            this.bCopy.Size = new System.Drawing.Size(75, 23);
            this.bCopy.TabIndex = 6;
            this.bCopy.Text = "コピー実行";
            this.bCopy.UseVisualStyleBackColor = true;
            this.bCopy.Click += new System.EventHandler(this.bCopy_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbReplace2);
            this.groupBox1.Controls.Add(this.tbMatchString2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.tbReplaceString2);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.cbReplace1);
            this.groupBox1.Controls.Add(this.tbMatchString1);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.tbReplaceString1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 30);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(852, 70);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "ファイル名置換";
            // 
            // cbReplace2
            // 
            this.cbReplace2.AutoSize = true;
            this.cbReplace2.Location = new System.Drawing.Point(6, 45);
            this.cbReplace2.Name = "cbReplace2";
            this.cbReplace2.Size = new System.Drawing.Size(56, 16);
            this.cbReplace2.TabIndex = 4;
            this.cbReplace2.Text = "置換2:";
            this.cbReplace2.UseVisualStyleBackColor = true;
            // 
            // tbMatchString2
            // 
            this.tbMatchString2.Location = new System.Drawing.Point(165, 43);
            this.tbMatchString2.Name = "tbMatchString2";
            this.tbMatchString2.Size = new System.Drawing.Size(159, 19);
            this.tbMatchString2.TabIndex = 5;
            this.tbMatchString2.TextChanged += new System.EventHandler(this.tbMatchString2_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(68, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "置換対象文字列:";
            // 
            // tbReplaceString2
            // 
            this.tbReplaceString2.Location = new System.Drawing.Point(415, 43);
            this.tbReplaceString2.Name = "tbReplaceString2";
            this.tbReplaceString2.Size = new System.Drawing.Size(159, 19);
            this.tbReplaceString2.TabIndex = 8;
            this.tbReplaceString2.TextChanged += new System.EventHandler(this.tbReplaceString2_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(330, 46);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "置換後文字列:";
            // 
            // cbReplace1
            // 
            this.cbReplace1.AutoSize = true;
            this.cbReplace1.Location = new System.Drawing.Point(6, 20);
            this.cbReplace1.Name = "cbReplace1";
            this.cbReplace1.Size = new System.Drawing.Size(56, 16);
            this.cbReplace1.TabIndex = 0;
            this.cbReplace1.Text = "置換1:";
            this.cbReplace1.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label5);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(852, 30);
            this.panel1.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(470, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "置換をチェックして自動置換を行うか、リストのコピー先ファイル名を修正してコピーを実行してください。";
            // 
            // tbLabelEditor
            // 
            this.tbLabelEditor.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbLabelEditor.Location = new System.Drawing.Point(14, 480);
            this.tbLabelEditor.Name = "tbLabelEditor";
            this.tbLabelEditor.Size = new System.Drawing.Size(159, 12);
            this.tbLabelEditor.TabIndex = 9;
            this.tbLabelEditor.Visible = false;
            this.tbLabelEditor.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbLabelEditor_KeyDown);
            this.tbLabelEditor.Leave += new System.EventHandler(this.tbLabelEditor_Leave);
            // 
            // TextureCopyDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(852, 513);
            this.Controls.Add(this.tbLabelEditor);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.bCopy);
            this.Controls.Add(this.bCancel);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.panel1);
            this.Name = "TextureCopyDialog";
            this.Text = "TextureCopyDialog";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbMatchString1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbReplaceString1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Button bCancel;
        private System.Windows.Forms.Button bCopy;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox cbReplace2;
        private System.Windows.Forms.TextBox tbMatchString2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbReplaceString2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox cbReplace1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbLabelEditor;
    }
}