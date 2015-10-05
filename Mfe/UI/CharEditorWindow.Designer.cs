namespace Mfe
{
    partial class CharEditorWindow
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
            if (disposing && (components != null))
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
            this.charLabel = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.charSelectorUpDown = new System.Windows.Forms.NumericUpDown();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.characterWidthUpDown = new System.Windows.Forms.NumericUpDown();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.logicalWidthUpDown = new System.Windows.Forms.NumericUpDown();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.codepointBox = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.characterNameBox = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exchangeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.shiftUpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.shiftDownToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.shiftLeftToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.shiftRightToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.invertToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.widerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.thinnerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dragnFillToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toggleModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.biggerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.smallerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gridToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.findToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nextCharacterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.previousCharacterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chartNavModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.characterSelectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.characterBitmapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.characterCodepointToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.characterNameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.charEditor = new Mfe.CharEditor();
            this.flowLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.charSelectorUpDown)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.characterWidthUpDown)).BeginInit();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logicalWidthUpDown)).BeginInit();
            this.groupBox6.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // charLabel
            // 
            this.charLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.charLabel.AutoSize = true;
            this.charLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.charLabel.Location = new System.Drawing.Point(3, 0);
            this.charLabel.Name = "charLabel";
            this.charLabel.Size = new System.Drawing.Size(85, 73);
            this.charLabel.TabIndex = 1;
            this.charLabel.Text = "M";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.charLabel);
            this.flowLayoutPanel1.Controls.Add(this.groupBox1);
            this.flowLayoutPanel1.Controls.Add(this.groupBox4);
            this.flowLayoutPanel1.Controls.Add(this.groupBox5);
            this.flowLayoutPanel1.Controls.Add(this.groupBox6);
            this.flowLayoutPanel1.Controls.Add(this.groupBox2);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(277, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(104, 431);
            this.flowLayoutPanel1.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.charSelectorUpDown);
            this.groupBox1.Location = new System.Drawing.Point(3, 76);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(90, 40);
            this.groupBox1.TabIndex = 100;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Character";
            // 
            // charSelectorUpDown
            // 
            this.charSelectorUpDown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.charSelectorUpDown.Location = new System.Drawing.Point(3, 16);
            this.charSelectorUpDown.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.charSelectorUpDown.Name = "charSelectorUpDown";
            this.charSelectorUpDown.Size = new System.Drawing.Size(84, 20);
            this.charSelectorUpDown.TabIndex = 100;
            this.charSelectorUpDown.Value = new decimal(new int[] {
            32,
            0,
            0,
            0});
            this.charSelectorUpDown.ValueChanged += new System.EventHandler(this.charSelectorUpDown_ValueChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.characterWidthUpDown);
            this.groupBox4.Location = new System.Drawing.Point(3, 122);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(90, 40);
            this.groupBox4.TabIndex = 110;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Data Width";
            // 
            // characterWidthUpDown
            // 
            this.characterWidthUpDown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.characterWidthUpDown.Location = new System.Drawing.Point(3, 16);
            this.characterWidthUpDown.Maximum = new decimal(new int[] {
            64,
            0,
            0,
            0});
            this.characterWidthUpDown.Name = "characterWidthUpDown";
            this.characterWidthUpDown.Size = new System.Drawing.Size(84, 20);
            this.characterWidthUpDown.TabIndex = 110;
            this.characterWidthUpDown.ValueChanged += new System.EventHandler(this.characterWidthUpDown_ValueChanged);
            this.characterWidthUpDown.Validating += new System.ComponentModel.CancelEventHandler(this.characterWidthUpDown_Validating);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.logicalWidthUpDown);
            this.groupBox5.Location = new System.Drawing.Point(3, 168);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(90, 40);
            this.groupBox5.TabIndex = 120;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Width";
            // 
            // logicalWidthUpDown
            // 
            this.logicalWidthUpDown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logicalWidthUpDown.Location = new System.Drawing.Point(3, 16);
            this.logicalWidthUpDown.Maximum = new decimal(new int[] {
            64,
            0,
            0,
            0});
            this.logicalWidthUpDown.Name = "logicalWidthUpDown";
            this.logicalWidthUpDown.Size = new System.Drawing.Size(84, 20);
            this.logicalWidthUpDown.TabIndex = 120;
            this.logicalWidthUpDown.ValueChanged += new System.EventHandler(this.logicalWidthUpDown_ValueChanged);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.codepointBox);
            this.groupBox6.Location = new System.Drawing.Point(3, 214);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(90, 40);
            this.groupBox6.TabIndex = 125;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Codepoint";
            // 
            // codepointBox
            // 
            this.codepointBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.codepointBox.Location = new System.Drawing.Point(3, 16);
            this.codepointBox.MaxLength = 1;
            this.codepointBox.Name = "codepointBox";
            this.codepointBox.Size = new System.Drawing.Size(84, 20);
            this.codepointBox.TabIndex = 125;
            this.codepointBox.TextChanged += new System.EventHandler(this.codepointBox_TextChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.characterNameBox);
            this.groupBox2.Location = new System.Drawing.Point(3, 260);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(90, 70);
            this.groupBox2.TabIndex = 130;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Name";
            // 
            // characterNameBox
            // 
            this.characterNameBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.characterNameBox.Location = new System.Drawing.Point(3, 16);
            this.characterNameBox.MaxLength = 64;
            this.characterNameBox.Multiline = true;
            this.characterNameBox.Name = "characterNameBox";
            this.characterNameBox.Size = new System.Drawing.Size(84, 51);
            this.characterNameBox.TabIndex = 130;
            this.characterNameBox.Text = "The quick brown fox jumps over the lazy dog.";
            this.characterNameBox.TextChanged += new System.EventHandler(this.characterNameBox_TextChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editToolStripMenuItem,
            this.viewToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(384, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.Visible = false;
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToolStripMenuItem,
            this.exchangeToolStripMenuItem,
            this.pasteToolStripMenuItem,
            this.toolStripSeparator1,
            this.shiftUpToolStripMenuItem,
            this.shiftDownToolStripMenuItem,
            this.shiftLeftToolStripMenuItem,
            this.shiftRightToolStripMenuItem,
            this.clearToolStripMenuItem,
            this.invertToolStripMenuItem,
            this.toolStripSeparator2,
            this.widerToolStripMenuItem,
            this.thinnerToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "&Edit";
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.copyToolStripMenuItem.Text = "Copy";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // exchangeToolStripMenuItem
            // 
            this.exchangeToolStripMenuItem.Name = "exchangeToolStripMenuItem";
            this.exchangeToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.exchangeToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.exchangeToolStripMenuItem.Text = "Exchange";
            this.exchangeToolStripMenuItem.Click += new System.EventHandler(this.exchangeToolStripMenuItem_Click);
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.pasteToolStripMenuItem.Text = "Paste";
            this.pasteToolStripMenuItem.Click += new System.EventHandler(this.pasteToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(194, 6);
            // 
            // shiftUpToolStripMenuItem
            // 
            this.shiftUpToolStripMenuItem.Name = "shiftUpToolStripMenuItem";
            this.shiftUpToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Up)));
            this.shiftUpToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.shiftUpToolStripMenuItem.Text = "Shift Up";
            this.shiftUpToolStripMenuItem.Click += new System.EventHandler(this.shiftUpToolStripMenuItem_Click);
            // 
            // shiftDownToolStripMenuItem
            // 
            this.shiftDownToolStripMenuItem.Name = "shiftDownToolStripMenuItem";
            this.shiftDownToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Down)));
            this.shiftDownToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.shiftDownToolStripMenuItem.Text = "Shift Down";
            this.shiftDownToolStripMenuItem.Click += new System.EventHandler(this.shiftDownToolStripMenuItem_Click);
            // 
            // shiftLeftToolStripMenuItem
            // 
            this.shiftLeftToolStripMenuItem.Name = "shiftLeftToolStripMenuItem";
            this.shiftLeftToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Left)));
            this.shiftLeftToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.shiftLeftToolStripMenuItem.Text = "Shift Left";
            this.shiftLeftToolStripMenuItem.Click += new System.EventHandler(this.shiftLeftToolStripMenuItem_Click);
            // 
            // shiftRightToolStripMenuItem
            // 
            this.shiftRightToolStripMenuItem.Name = "shiftRightToolStripMenuItem";
            this.shiftRightToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Right)));
            this.shiftRightToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.shiftRightToolStripMenuItem.Text = "Shift Right";
            this.shiftRightToolStripMenuItem.Click += new System.EventHandler(this.shiftRightToolStripMenuItem_Click);
            // 
            // clearToolStripMenuItem
            // 
            this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            this.clearToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.clearToolStripMenuItem.Text = "&Clear";
            this.clearToolStripMenuItem.Click += new System.EventHandler(this.clearToolStripMenuItem_Click);
            // 
            // invertToolStripMenuItem
            // 
            this.invertToolStripMenuItem.Name = "invertToolStripMenuItem";
            this.invertToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.invertToolStripMenuItem.Text = "&Invert";
            this.invertToolStripMenuItem.Click += new System.EventHandler(this.invertToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(194, 6);
            // 
            // widerToolStripMenuItem
            // 
            this.widerToolStripMenuItem.Name = "widerToolStripMenuItem";
            this.widerToolStripMenuItem.ShortcutKeyDisplayString = "+";
            this.widerToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.widerToolStripMenuItem.Text = "Increase Width";
            this.widerToolStripMenuItem.Click += new System.EventHandler(this.widerToolStripMenuItem_Click);
            // 
            // thinnerToolStripMenuItem
            // 
            this.thinnerToolStripMenuItem.Name = "thinnerToolStripMenuItem";
            this.thinnerToolStripMenuItem.ShortcutKeyDisplayString = "-";
            this.thinnerToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.thinnerToolStripMenuItem.Text = "Decrease Width";
            this.thinnerToolStripMenuItem.Click += new System.EventHandler(this.thinnerToolStripMenuItem_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dragnFillToolStripMenuItem,
            this.toggleModeToolStripMenuItem,
            this.biggerToolStripMenuItem,
            this.smallerToolStripMenuItem,
            this.gridToolStripMenuItem,
            this.toolStripSeparator3,
            this.findToolStripMenuItem,
            this.nextCharacterToolStripMenuItem,
            this.previousCharacterToolStripMenuItem,
            this.chartNavModeToolStripMenuItem,
            this.toolStripSeparator4,
            this.characterSelectToolStripMenuItem,
            this.characterBitmapToolStripMenuItem,
            this.characterCodepointToolStripMenuItem,
            this.characterNameToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "&View";
            // 
            // dragnFillToolStripMenuItem
            // 
            this.dragnFillToolStripMenuItem.Checked = true;
            this.dragnFillToolStripMenuItem.CheckOnClick = true;
            this.dragnFillToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.dragnFillToolStripMenuItem.Name = "dragnFillToolStripMenuItem";
            this.dragnFillToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F9;
            this.dragnFillToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
            this.dragnFillToolStripMenuItem.Text = "&Drag \'n\' Fill";
            this.dragnFillToolStripMenuItem.Click += new System.EventHandler(this.dragnFillToolStripMenuItem_Click);
            // 
            // toggleModeToolStripMenuItem
            // 
            this.toggleModeToolStripMenuItem.CheckOnClick = true;
            this.toggleModeToolStripMenuItem.Name = "toggleModeToolStripMenuItem";
            this.toggleModeToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F10;
            this.toggleModeToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
            this.toggleModeToolStripMenuItem.Text = "&Toggle Mode";
            this.toggleModeToolStripMenuItem.Click += new System.EventHandler(this.toggleModeToolStripMenuItem_Click);
            // 
            // biggerToolStripMenuItem
            // 
            this.biggerToolStripMenuItem.Name = "biggerToolStripMenuItem";
            this.biggerToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F11;
            this.biggerToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
            this.biggerToolStripMenuItem.Text = "Bigger";
            this.biggerToolStripMenuItem.Click += new System.EventHandler(this.biggerToolStripMenuItem_Click);
            // 
            // smallerToolStripMenuItem
            // 
            this.smallerToolStripMenuItem.Name = "smallerToolStripMenuItem";
            this.smallerToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F12;
            this.smallerToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
            this.smallerToolStripMenuItem.Text = "Smaller";
            this.smallerToolStripMenuItem.Click += new System.EventHandler(this.smallerToolStripMenuItem_Click);
            // 
            // gridToolStripMenuItem
            // 
            this.gridToolStripMenuItem.Checked = true;
            this.gridToolStripMenuItem.CheckOnClick = true;
            this.gridToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.gridToolStripMenuItem.Name = "gridToolStripMenuItem";
            this.gridToolStripMenuItem.ShortcutKeyDisplayString = "G";
            this.gridToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
            this.gridToolStripMenuItem.Text = "Show &Grid";
            this.gridToolStripMenuItem.Click += new System.EventHandler(this.gridToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(222, 6);
            // 
            // findToolStripMenuItem
            // 
            this.findToolStripMenuItem.Name = "findToolStripMenuItem";
            this.findToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.findToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
            this.findToolStripMenuItem.Text = "Find Codepoint";
            this.findToolStripMenuItem.Click += new System.EventHandler(this.findToolStripMenuItem_Click);
            // 
            // nextCharacterToolStripMenuItem
            // 
            this.nextCharacterToolStripMenuItem.Name = "nextCharacterToolStripMenuItem";
            this.nextCharacterToolStripMenuItem.ShortcutKeyDisplayString = "";
            this.nextCharacterToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.nextCharacterToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
            this.nextCharacterToolStripMenuItem.Text = "Next Character";
            this.nextCharacterToolStripMenuItem.Click += new System.EventHandler(this.nextCharacterToolStripMenuItem_Click);
            // 
            // previousCharacterToolStripMenuItem
            // 
            this.previousCharacterToolStripMenuItem.Name = "previousCharacterToolStripMenuItem";
            this.previousCharacterToolStripMenuItem.ShortcutKeyDisplayString = "";
            this.previousCharacterToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.previousCharacterToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
            this.previousCharacterToolStripMenuItem.Text = "Previous Character";
            this.previousCharacterToolStripMenuItem.Click += new System.EventHandler(this.previousCharacterToolStripMenuItem_Click);
            // 
            // chartNavModeToolStripMenuItem
            // 
            this.chartNavModeToolStripMenuItem.Name = "chartNavModeToolStripMenuItem";
            this.chartNavModeToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
            this.chartNavModeToolStripMenuItem.Text = "Chart &Navigation";
            this.chartNavModeToolStripMenuItem.Click += new System.EventHandler(this.chartNavModeToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(222, 6);
            // 
            // characterSelectToolStripMenuItem
            // 
            this.characterSelectToolStripMenuItem.Name = "characterSelectToolStripMenuItem";
            this.characterSelectToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.W)));
            this.characterSelectToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
            this.characterSelectToolStripMenuItem.Text = "Character Select";
            this.characterSelectToolStripMenuItem.Click += new System.EventHandler(this.characterSelectToolStripMenuItem_Click);
            // 
            // characterBitmapToolStripMenuItem
            // 
            this.characterBitmapToolStripMenuItem.Name = "characterBitmapToolStripMenuItem";
            this.characterBitmapToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.characterBitmapToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
            this.characterBitmapToolStripMenuItem.Text = "Character Bitmap";
            this.characterBitmapToolStripMenuItem.Click += new System.EventHandler(this.characterBitmapToolStripMenuItem_Click);
            // 
            // characterCodepointToolStripMenuItem
            // 
            this.characterCodepointToolStripMenuItem.Name = "characterCodepointToolStripMenuItem";
            this.characterCodepointToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.characterCodepointToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
            this.characterCodepointToolStripMenuItem.Text = "Character Codepoint";
            this.characterCodepointToolStripMenuItem.Click += new System.EventHandler(this.characterCodepointToolStripMenuItem_Click);
            // 
            // characterNameToolStripMenuItem
            // 
            this.characterNameToolStripMenuItem.Name = "characterNameToolStripMenuItem";
            this.characterNameToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.T)));
            this.characterNameToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
            this.characterNameToolStripMenuItem.Text = "Character Name";
            this.characterNameToolStripMenuItem.Click += new System.EventHandler(this.characterNameToolStripMenuItem_Click);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 110F));
            this.tableLayoutPanel2.Controls.Add(this.charEditor, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.flowLayoutPanel1, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 24);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 461F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(384, 437);
            this.tableLayoutPanel2.TabIndex = 4;
            // 
            // charEditor
            // 
            this.charEditor.AutoscaleLineThickness = true;
            this.charEditor.BaselineColor = System.Drawing.SystemColors.MenuHighlight;
            this.charEditor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.charEditor.CapHeightColor = System.Drawing.SystemColors.MenuHighlight;
            this.charEditor.CapLineThickness = 1;
            this.charEditor.CharScale = 0;
            this.charEditor.CurrentChar = null;
            this.charEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.charEditor.FillOnDrag = false;
            this.charEditor.GridColor = System.Drawing.SystemColors.ControlText;
            this.charEditor.GridThickness = 1;
            this.charEditor.Location = new System.Drawing.Point(3, 3);
            this.charEditor.Name = "charEditor";
            this.charEditor.ShowGrid = true;
            this.charEditor.Size = new System.Drawing.Size(268, 431);
            this.charEditor.TabIndex = 1;
            this.charEditor.ToggleMode = false;
            this.charEditor.WidthLineColor = System.Drawing.Color.Red;
            this.charEditor.WidthLineThickness = 1;
            this.charEditor.XHeightColor = System.Drawing.SystemColors.MenuHighlight;
            this.charEditor.KeyDown += new System.Windows.Forms.KeyEventHandler(this.charEditor_KeyDown);
            // 
            // CharEditorWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 461);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(300, 300);
            this.Name = "CharEditorWindow";
            this.ShowIcon = false;
            this.Text = "Character Editor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CharEditorWindow_FormClosing);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.charSelectorUpDown)).EndInit();
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.characterWidthUpDown)).EndInit();
            this.groupBox5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.logicalWidthUpDown)).EndInit();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label charLabel;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown charSelectorUpDown;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.NumericUpDown characterWidthUpDown;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.NumericUpDown logicalWidthUpDown;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox characterNameBox;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.TextBox codepointBox;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exchangeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toggleModeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem biggerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem smallerToolStripMenuItem;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.ToolStripMenuItem gridToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dragnFillToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem invertToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem shiftUpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem shiftDownToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem shiftLeftToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem shiftRightToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem widerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem thinnerToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem characterBitmapToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem characterCodepointToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem characterNameToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem findToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem characterSelectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nextCharacterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem previousCharacterToolStripMenuItem;
        public CharEditor charEditor;
        private System.Windows.Forms.ToolStripMenuItem chartNavModeToolStripMenuItem;
    }
}