namespace Mfe
{
    partial class BdfBrowser
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.charSelectUpDown = new System.Windows.Forms.NumericUpDown();
            this.magnificationUpDown = new System.Windows.Forms.NumericUpDown();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.destSelectUpDown = new System.Windows.Forms.NumericUpDown();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.loadCpButton = new System.Windows.Forms.Button();
            this.defaultCpButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.selectedCharNameTextBox = new System.Windows.Forms.TextBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.selectedCharCodepointTextBox = new System.Windows.Forms.TextBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.copyToDestButton = new System.Windows.Forms.Button();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.loadCodepageDialog = new System.Windows.Forms.OpenFileDialog();
            this.selectedCharPreviewer = new Mfe.CharPreviewer();
            this.destCharPreviewer = new Mfe.CharPreviewer();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.charSelectUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.magnificationUpDown)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.destSelectUpDown)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.flowLayoutPanel3.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.charSelectUpDown);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(85, 40);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Character";
            // 
            // charSelectUpDown
            // 
            this.charSelectUpDown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.charSelectUpDown.Location = new System.Drawing.Point(3, 16);
            this.charSelectUpDown.Name = "charSelectUpDown";
            this.charSelectUpDown.Size = new System.Drawing.Size(79, 20);
            this.charSelectUpDown.TabIndex = 0;
            this.charSelectUpDown.ValueChanged += new System.EventHandler(this.charSelectUpDown_ValueChanged);
            // 
            // magnificationUpDown
            // 
            this.magnificationUpDown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.magnificationUpDown.Location = new System.Drawing.Point(3, 16);
            this.magnificationUpDown.Maximum = new decimal(new int[] {
            256,
            0,
            0,
            0});
            this.magnificationUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.magnificationUpDown.Name = "magnificationUpDown";
            this.magnificationUpDown.Size = new System.Drawing.Size(79, 20);
            this.magnificationUpDown.TabIndex = 1;
            this.magnificationUpDown.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.magnificationUpDown.ValueChanged += new System.EventHandler(this.magnificationUpDown_ValueChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.destSelectUpDown);
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(85, 40);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Character";
            // 
            // destSelectUpDown
            // 
            this.destSelectUpDown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.destSelectUpDown.Location = new System.Drawing.Point(3, 16);
            this.destSelectUpDown.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.destSelectUpDown.Name = "destSelectUpDown";
            this.destSelectUpDown.Size = new System.Drawing.Size(79, 20);
            this.destSelectUpDown.TabIndex = 0;
            this.destSelectUpDown.Value = new decimal(new int[] {
            32,
            0,
            0,
            0});
            this.destSelectUpDown.ValueChanged += new System.EventHandler(this.destSelectUpDown_ValueChanged);
            // 
            // flowLayoutPanel1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.flowLayoutPanel1, 2);
            this.flowLayoutPanel1.Controls.Add(this.groupBox5);
            this.flowLayoutPanel1.Controls.Add(this.loadCpButton);
            this.flowLayoutPanel1.Controls.Add(this.defaultCpButton);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(584, 50);
            this.flowLayoutPanel1.TabIndex = 2;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.magnificationUpDown);
            this.groupBox5.Location = new System.Drawing.Point(3, 3);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(85, 40);
            this.groupBox5.TabIndex = 0;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Magnification";
            // 
            // loadCpButton
            // 
            this.loadCpButton.Location = new System.Drawing.Point(94, 3);
            this.loadCpButton.Name = "loadCpButton";
            this.loadCpButton.Size = new System.Drawing.Size(85, 40);
            this.loadCpButton.TabIndex = 1;
            this.loadCpButton.Text = "Load Codepage";
            this.loadCpButton.UseVisualStyleBackColor = true;
            this.loadCpButton.Click += new System.EventHandler(this.loadCpButton_Click);
            // 
            // defaultCpButton
            // 
            this.defaultCpButton.Location = new System.Drawing.Point(185, 3);
            this.defaultCpButton.Name = "defaultCpButton";
            this.defaultCpButton.Size = new System.Drawing.Size(85, 40);
            this.defaultCpButton.TabIndex = 2;
            this.defaultCpButton.Text = "Use ASCII Codepage";
            this.defaultCpButton.UseVisualStyleBackColor = true;
            this.defaultCpButton.Click += new System.EventHandler(this.defaultCpButton_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox3, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.groupBox4, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(584, 411);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tableLayoutPanel2);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(3, 53);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(286, 355);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Select";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.flowLayoutPanel2, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.groupBox8, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 51F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(280, 336);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.groupBox1);
            this.flowLayoutPanel2.Controls.Add(this.groupBox6);
            this.flowLayoutPanel2.Controls.Add(this.groupBox7);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(280, 51);
            this.flowLayoutPanel2.TabIndex = 3;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.selectedCharNameTextBox);
            this.groupBox6.Location = new System.Drawing.Point(94, 3);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(85, 40);
            this.groupBox6.TabIndex = 4;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Name";
            // 
            // selectedCharNameTextBox
            // 
            this.selectedCharNameTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.selectedCharNameTextBox.Location = new System.Drawing.Point(3, 16);
            this.selectedCharNameTextBox.Name = "selectedCharNameTextBox";
            this.selectedCharNameTextBox.ReadOnly = true;
            this.selectedCharNameTextBox.Size = new System.Drawing.Size(79, 20);
            this.selectedCharNameTextBox.TabIndex = 5;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.selectedCharCodepointTextBox);
            this.groupBox7.Location = new System.Drawing.Point(185, 3);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(85, 40);
            this.groupBox7.TabIndex = 5;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Codepoint";
            // 
            // selectedCharCodepointTextBox
            // 
            this.selectedCharCodepointTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.selectedCharCodepointTextBox.Location = new System.Drawing.Point(3, 16);
            this.selectedCharCodepointTextBox.Name = "selectedCharCodepointTextBox";
            this.selectedCharCodepointTextBox.ReadOnly = true;
            this.selectedCharCodepointTextBox.Size = new System.Drawing.Size(79, 20);
            this.selectedCharCodepointTextBox.TabIndex = 0;
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.selectedCharPreviewer);
            this.groupBox8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox8.Location = new System.Drawing.Point(3, 54);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(274, 279);
            this.groupBox8.TabIndex = 4;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Glyph";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.tableLayoutPanel3);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Location = new System.Drawing.Point(295, 53);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(286, 355);
            this.groupBox4.TabIndex = 6;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Destination Code Point";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.flowLayoutPanel3, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.groupBox9, 0, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 51F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(280, 336);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.Controls.Add(this.groupBox2);
            this.flowLayoutPanel3.Controls.Add(this.copyToDestButton);
            this.flowLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(280, 51);
            this.flowLayoutPanel3.TabIndex = 4;
            // 
            // copyToDestButton
            // 
            this.copyToDestButton.Location = new System.Drawing.Point(94, 3);
            this.copyToDestButton.Name = "copyToDestButton";
            this.copyToDestButton.Size = new System.Drawing.Size(85, 40);
            this.copyToDestButton.TabIndex = 2;
            this.copyToDestButton.Text = "Copy Glyph from Select";
            this.copyToDestButton.UseVisualStyleBackColor = true;
            this.copyToDestButton.Click += new System.EventHandler(this.copyToDestButton_Click);
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.destCharPreviewer);
            this.groupBox9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox9.Location = new System.Drawing.Point(3, 54);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(274, 279);
            this.groupBox9.TabIndex = 5;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "Glyph";
            // 
            // loadCodepageDialog
            // 
            this.loadCodepageDialog.Filter = "Codepage|*.codepage|All files|*.*";
            // 
            // selectedCharPreviewer
            // 
            this.selectedCharPreviewer.Char = null;
            this.selectedCharPreviewer.CharScale = 2;
            this.selectedCharPreviewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.selectedCharPreviewer.Location = new System.Drawing.Point(3, 16);
            this.selectedCharPreviewer.Name = "selectedCharPreviewer";
            this.selectedCharPreviewer.Size = new System.Drawing.Size(268, 260);
            this.selectedCharPreviewer.TabIndex = 0;
            // 
            // destCharPreviewer
            // 
            this.destCharPreviewer.Char = null;
            this.destCharPreviewer.CharScale = 2;
            this.destCharPreviewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.destCharPreviewer.Location = new System.Drawing.Point(3, 16);
            this.destCharPreviewer.Name = "destCharPreviewer";
            this.destCharPreviewer.Size = new System.Drawing.Size(268, 260);
            this.destCharPreviewer.TabIndex = 0;
            // 
            // BdfBrowser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 411);
            this.ControlBox = false;
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "BdfBrowser";
            this.Text = " Character Browser";
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.charSelectUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.magnificationUpDown)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.destSelectUpDown)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.flowLayoutPanel3.ResumeLayout(false);
            this.groupBox9.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown magnificationUpDown;
        private System.Windows.Forms.NumericUpDown charSelectUpDown;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.NumericUpDown destSelectUpDown;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Button copyToDestButton;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.TextBox selectedCharNameTextBox;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.TextBox selectedCharCodepointTextBox;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.GroupBox groupBox9;
        private CharPreviewer selectedCharPreviewer;
        private CharPreviewer destCharPreviewer;
        private System.Windows.Forms.Button loadCpButton;
        private System.Windows.Forms.Button defaultCpButton;
        private System.Windows.Forms.OpenFileDialog loadCodepageDialog;
    }
}