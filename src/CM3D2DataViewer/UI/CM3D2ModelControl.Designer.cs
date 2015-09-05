namespace CM3D2DataViewer
{
    partial class CM3D2ModelControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CM3D2ModelControl));
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.lvBones = new System.Windows.Forms.ListView();
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.lvVerts = new System.Windows.Forms.ListView();
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader12 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader13 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader14 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader15 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader16 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader17 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbFileOpen = new System.Windows.Forms.ToolStripButton();
            this.tsbDirectoryOpen = new System.Windows.Forms.ToolStripButton();
            this.tsbDebug = new System.Windows.Forms.ToolStripButton();
            this.tsbRestore = new System.Windows.Forms.ToolStripButton();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.nudExportScale = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.cbExportMorph = new System.Windows.Forms.CheckBox();
            this.cbExportSkin = new System.Windows.Forms.CheckBox();
            this.cbExportType = new System.Windows.Forms.ComboBox();
            this.bExport = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.nudImportScale = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.cbShader = new System.Windows.Forms.ComboBox();
            this.cbChangeShader = new System.Windows.Forms.CheckBox();
            this.cbImportType = new System.Windows.Forms.ComboBox();
            this.bImport = new System.Windows.Forms.Button();
            this.bBrowseRefModel = new System.Windows.Forms.Button();
            this.cbRefModel = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.nudMinDist = new System.Windows.Forms.NumericUpDown();
            this.nudMaxDist = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudExportScale)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudImportScale)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinDist)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxDist)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.textBox1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBox1.Location = new System.Drawing.Point(3, 3);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox1.Size = new System.Drawing.Size(783, 71);
            this.textBox1.TabIndex = 2;
            this.textBox1.WordWrap = false;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(3, 74);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(783, 489);
            this.tabControl1.TabIndex = 3;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.lvBones);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(775, 463);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Bone";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // lvBones
            // 
            this.lvBones.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader4,
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.lvBones.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvBones.FullRowSelect = true;
            this.lvBones.Location = new System.Drawing.Point(3, 3);
            this.lvBones.Name = "lvBones";
            this.lvBones.Size = new System.Drawing.Size(769, 457);
            this.lvBones.TabIndex = 0;
            this.lvBones.UseCompatibleStateImageBehavior = false;
            this.lvBones.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "#";
            this.columnHeader4.Width = 40;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Name";
            this.columnHeader1.Width = 136;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Parent";
            this.columnHeader2.Width = 49;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Param";
            this.columnHeader3.Width = 346;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.lvVerts);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(775, 463);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Vertex";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // lvVerts
            // 
            this.lvVerts.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7,
            this.columnHeader8,
            this.columnHeader9,
            this.columnHeader10,
            this.columnHeader11,
            this.columnHeader12,
            this.columnHeader13,
            this.columnHeader14,
            this.columnHeader15,
            this.columnHeader16,
            this.columnHeader17});
            this.lvVerts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvVerts.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lvVerts.FullRowSelect = true;
            this.lvVerts.Location = new System.Drawing.Point(3, 3);
            this.lvVerts.Name = "lvVerts";
            this.lvVerts.Size = new System.Drawing.Size(769, 457);
            this.lvVerts.TabIndex = 1;
            this.lvVerts.UseCompatibleStateImageBehavior = false;
            this.lvVerts.View = System.Windows.Forms.View.Details;
            this.lvVerts.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvVerts_KeyDown);
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "#";
            this.columnHeader5.Width = 40;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "X";
            this.columnHeader6.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeader6.Width = 100;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Y";
            this.columnHeader7.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeader7.Width = 100;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Z";
            this.columnHeader8.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeader8.Width = 100;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "NX";
            this.columnHeader9.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeader9.Width = 100;
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "NY";
            this.columnHeader10.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeader10.Width = 100;
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "NZ";
            this.columnHeader11.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeader11.Width = 100;
            // 
            // columnHeader12
            // 
            this.columnHeader12.Text = "U";
            this.columnHeader12.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeader12.Width = 100;
            // 
            // columnHeader13
            // 
            this.columnHeader13.Text = "V";
            this.columnHeader13.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeader13.Width = 100;
            // 
            // columnHeader14
            // 
            this.columnHeader14.Text = "Bone1";
            this.columnHeader14.Width = 120;
            // 
            // columnHeader15
            // 
            this.columnHeader15.Text = "Bone2";
            this.columnHeader15.Width = 120;
            // 
            // columnHeader16
            // 
            this.columnHeader16.Text = "Bone3";
            this.columnHeader16.Width = 120;
            // 
            // columnHeader17
            // 
            this.columnHeader17.Text = "Bone4";
            this.columnHeader17.Width = 120;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Enabled = false;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbFileOpen,
            this.tsbDirectoryOpen,
            this.tsbRestore,
            this.tsbDebug});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(797, 25);
            this.toolStrip1.TabIndex = 4;
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
            // tsbDebug
            // 
            this.tsbDebug.Image = ((System.Drawing.Image)(resources.GetObject("tsbDebug.Image")));
            this.tsbDebug.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDebug.Name = "tsbDebug";
            this.tsbDebug.Size = new System.Drawing.Size(76, 22);
            this.tsbDebug.Text = "デバッグ";
            this.tsbDebug.Visible = false;
            this.tsbDebug.Click += new System.EventHandler(this.tsbDebug_Click);
            // 
            // tsbRestore
            // 
            this.tsbRestore.Image = ((System.Drawing.Image)(resources.GetObject("tsbRestore.Image")));
            this.tsbRestore.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRestore.Name = "tsbRestore";
            this.tsbRestore.Size = new System.Drawing.Size(76, 22);
            this.tsbRestore.Text = "リストア";
            this.tsbRestore.Click += new System.EventHandler(this.tsbRestore_Click);
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tabPage3);
            this.tabControl2.Controls.Add(this.tabPage4);
            this.tabControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl2.Location = new System.Drawing.Point(0, 25);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(797, 592);
            this.tabControl2.TabIndex = 5;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.tabControl1);
            this.tabPage3.Controls.Add(this.textBox1);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(789, 566);
            this.tabPage3.TabIndex = 0;
            this.tabPage3.Text = "モデルデータ";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.groupBox2);
            this.tabPage4.Controls.Add(this.groupBox1);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(789, 566);
            this.tabPage4.TabIndex = 1;
            this.tabPage4.Text = "インポート・エクスポート";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.nudExportScale);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.cbExportMorph);
            this.groupBox2.Controls.Add(this.cbExportSkin);
            this.groupBox2.Controls.Add(this.cbExportType);
            this.groupBox2.Controls.Add(this.bExport);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(3, 124);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(783, 96);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "エクスポート";
            // 
            // nudExportScale
            // 
            this.nudExportScale.DecimalPlaces = 2;
            this.nudExportScale.Location = new System.Drawing.Point(72, 40);
            this.nudExportScale.Name = "nudExportScale";
            this.nudExportScale.Size = new System.Drawing.Size(69, 19);
            this.nudExportScale.TabIndex = 14;
            this.nudExportScale.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 42);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 12);
            this.label5.TabIndex = 13;
            this.label5.Text = "スケーリング:";
            // 
            // cbExportMorph
            // 
            this.cbExportMorph.AutoSize = true;
            this.cbExportMorph.Location = new System.Drawing.Point(64, 18);
            this.cbExportMorph.Name = "cbExportMorph";
            this.cbExportMorph.Size = new System.Drawing.Size(76, 16);
            this.cbExportMorph.TabIndex = 11;
            this.cbExportMorph.Text = "モーフィング";
            this.cbExportMorph.UseVisualStyleBackColor = true;
            // 
            // cbExportSkin
            // 
            this.cbExportSkin.AutoSize = true;
            this.cbExportSkin.Location = new System.Drawing.Point(6, 18);
            this.cbExportSkin.Name = "cbExportSkin";
            this.cbExportSkin.Size = new System.Drawing.Size(52, 16);
            this.cbExportSkin.TabIndex = 10;
            this.cbExportSkin.Text = "スキン";
            this.cbExportSkin.UseVisualStyleBackColor = true;
            // 
            // cbExportType
            // 
            this.cbExportType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbExportType.FormattingEnabled = true;
            this.cbExportType.Items.AddRange(new object[] {
            "OBJ",
            "DAE",
            "MQO"});
            this.cbExportType.Location = new System.Drawing.Point(6, 68);
            this.cbExportType.Name = "cbExportType";
            this.cbExportType.Size = new System.Drawing.Size(75, 20);
            this.cbExportType.TabIndex = 9;
            this.cbExportType.SelectedIndexChanged += new System.EventHandler(this.cbExportType_SelectedIndexChanged);
            // 
            // bExport
            // 
            this.bExport.Image = ((System.Drawing.Image)(resources.GetObject("bExport.Image")));
            this.bExport.Location = new System.Drawing.Point(87, 65);
            this.bExport.Name = "bExport";
            this.bExport.Size = new System.Drawing.Size(92, 25);
            this.bExport.TabIndex = 8;
            this.bExport.Text = "エクスポート";
            this.bExport.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.bExport.UseVisualStyleBackColor = true;
            this.bExport.Click += new System.EventHandler(this.bExport_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.nudImportScale);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.cbShader);
            this.groupBox1.Controls.Add(this.cbChangeShader);
            this.groupBox1.Controls.Add(this.cbImportType);
            this.groupBox1.Controls.Add(this.bImport);
            this.groupBox1.Controls.Add(this.bBrowseRefModel);
            this.groupBox1.Controls.Add(this.cbRefModel);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.nudMinDist);
            this.groupBox1.Controls.Add(this.nudMaxDist);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(783, 121);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "インポート";
            // 
            // nudImportScale
            // 
            this.nudImportScale.DecimalPlaces = 2;
            this.nudImportScale.Location = new System.Drawing.Point(72, 68);
            this.nudImportScale.Name = "nudImportScale";
            this.nudImportScale.Size = new System.Drawing.Size(69, 19);
            this.nudImportScale.TabIndex = 12;
            this.nudImportScale.Value = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 71);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 12);
            this.label4.TabIndex = 11;
            this.label4.Text = "スケーリング:";
            // 
            // cbShader
            // 
            this.cbShader.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbShader.FormattingEnabled = true;
            this.cbShader.Items.AddRange(new object[] {
            "Toony_Lighted_Trans",
            "Toony_Lighted_Outline"});
            this.cbShader.Location = new System.Drawing.Point(313, 94);
            this.cbShader.Name = "cbShader";
            this.cbShader.Size = new System.Drawing.Size(131, 20);
            this.cbShader.TabIndex = 10;
            // 
            // cbChangeShader
            // 
            this.cbChangeShader.AutoSize = true;
            this.cbChangeShader.Location = new System.Drawing.Point(185, 96);
            this.cbChangeShader.Name = "cbChangeShader";
            this.cbChangeShader.Size = new System.Drawing.Size(122, 16);
            this.cbChangeShader.TabIndex = 9;
            this.cbChangeShader.Text = "シェーダーを変更する";
            this.cbChangeShader.UseVisualStyleBackColor = true;
            // 
            // cbImportType
            // 
            this.cbImportType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbImportType.FormattingEnabled = true;
            this.cbImportType.Items.AddRange(new object[] {
            "OBJ",
            "DAE",
            "MQO"});
            this.cbImportType.Location = new System.Drawing.Point(6, 94);
            this.cbImportType.Name = "cbImportType";
            this.cbImportType.Size = new System.Drawing.Size(75, 20);
            this.cbImportType.TabIndex = 8;
            this.cbImportType.SelectedIndexChanged += new System.EventHandler(this.cbImportType_SelectedIndexChanged);
            // 
            // bImport
            // 
            this.bImport.Image = ((System.Drawing.Image)(resources.GetObject("bImport.Image")));
            this.bImport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bImport.Location = new System.Drawing.Point(87, 91);
            this.bImport.Name = "bImport";
            this.bImport.Size = new System.Drawing.Size(92, 25);
            this.bImport.TabIndex = 7;
            this.bImport.Text = "インポート";
            this.bImport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.bImport.UseVisualStyleBackColor = true;
            this.bImport.Click += new System.EventHandler(this.bImport_Click);
            // 
            // bBrowseRefModel
            // 
            this.bBrowseRefModel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bBrowseRefModel.Location = new System.Drawing.Point(702, 16);
            this.bBrowseRefModel.Name = "bBrowseRefModel";
            this.bBrowseRefModel.Size = new System.Drawing.Size(75, 23);
            this.bBrowseRefModel.TabIndex = 6;
            this.bBrowseRefModel.Text = "参照";
            this.bBrowseRefModel.UseVisualStyleBackColor = true;
            // 
            // cbRefModel
            // 
            this.cbRefModel.AllowDrop = true;
            this.cbRefModel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbRefModel.FormattingEnabled = true;
            this.cbRefModel.Location = new System.Drawing.Point(72, 18);
            this.cbRefModel.Name = "cbRefModel";
            this.cbRefModel.Size = new System.Drawing.Size(624, 20);
            this.cbRefModel.TabIndex = 5;
            this.cbRefModel.DragDrop += new System.Windows.Forms.DragEventHandler(this.cbRefModel_DragDrop);
            this.cbRefModel.DragOver += new System.Windows.Forms.DragEventHandler(this.cbRefModel_DragOver);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "参照モデル:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(188, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "影響度0%の距離:";
            // 
            // nudMinDist
            // 
            this.nudMinDist.DecimalPlaces = 2;
            this.nudMinDist.Location = new System.Drawing.Point(113, 44);
            this.nudMinDist.Name = "nudMinDist";
            this.nudMinDist.Size = new System.Drawing.Size(69, 19);
            this.nudMinDist.TabIndex = 1;
            this.nudMinDist.Value = new decimal(new int[] {
            2,
            0,
            0,
            131072});
            // 
            // nudMaxDist
            // 
            this.nudMaxDist.DecimalPlaces = 2;
            this.nudMaxDist.Location = new System.Drawing.Point(283, 44);
            this.nudMaxDist.Name = "nudMaxDist";
            this.nudMaxDist.Size = new System.Drawing.Size(69, 19);
            this.nudMaxDist.TabIndex = 2;
            this.nudMaxDist.Value = new decimal(new int[] {
            10,
            0,
            0,
            131072});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "影響度100%の距離:";
            // 
            // CM3D2ModelControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl2);
            this.Controls.Add(this.toolStrip1);
            this.Name = "CM3D2ModelControl";
            this.Size = new System.Drawing.Size(797, 617);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tabControl2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudExportScale)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudImportScale)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinDist)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxDist)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ListView lvBones;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ListView lvVerts;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.ColumnHeader columnHeader10;
        private System.Windows.Forms.ColumnHeader columnHeader11;
        private System.Windows.Forms.ColumnHeader columnHeader12;
        private System.Windows.Forms.ColumnHeader columnHeader13;
        private System.Windows.Forms.ColumnHeader columnHeader14;
        private System.Windows.Forms.ColumnHeader columnHeader15;
        private System.Windows.Forms.ColumnHeader columnHeader16;
        private System.Windows.Forms.ColumnHeader columnHeader17;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbFileOpen;
        private System.Windows.Forms.ToolStripButton tsbDirectoryOpen;
        private System.Windows.Forms.ToolStripButton tsbDebug;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cbExportType;
        private System.Windows.Forms.Button bExport;
        private System.Windows.Forms.ComboBox cbImportType;
        private System.Windows.Forms.Button bImport;
        private System.Windows.Forms.Button bBrowseRefModel;
        private System.Windows.Forms.ComboBox cbRefModel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nudMinDist;
        private System.Windows.Forms.NumericUpDown nudMaxDist;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox cbExportMorph;
        private System.Windows.Forms.CheckBox cbExportSkin;
        private System.Windows.Forms.ToolStripButton tsbRestore;
        private System.Windows.Forms.ComboBox cbShader;
        private System.Windows.Forms.CheckBox cbChangeShader;
        private System.Windows.Forms.NumericUpDown nudImportScale;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown nudExportScale;
        private System.Windows.Forms.Label label5;
    }
}
