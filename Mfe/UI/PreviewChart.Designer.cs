namespace Mfe
{
    partial class PreviewChart
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
            this.magUpDown = new System.Windows.Forms.NumericUpDown();
            this.magnificationGroupBox = new System.Windows.Forms.GroupBox();
            this.charNameBox = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.unicodeT = new System.Windows.Forms.GroupBox();
            this.codepointBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.magUpDown)).BeginInit();
            this.magnificationGroupBox.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.unicodeT.SuspendLayout();
            this.SuspendLayout();
            // 
            // magUpDown
            // 
            this.magUpDown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.magUpDown.Location = new System.Drawing.Point(3, 16);
            this.magUpDown.Maximum = new decimal(new int[] {
            256,
            0,
            0,
            0});
            this.magUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.magUpDown.Name = "magUpDown";
            this.magUpDown.Size = new System.Drawing.Size(79, 20);
            this.magUpDown.TabIndex = 0;
            this.magUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.magUpDown.ValueChanged += new System.EventHandler(this.magUpDown_ValueChanged);
            // 
            // magnificationGroupBox
            // 
            this.magnificationGroupBox.Controls.Add(this.magUpDown);
            this.magnificationGroupBox.Location = new System.Drawing.Point(12, 12);
            this.magnificationGroupBox.Name = "magnificationGroupBox";
            this.magnificationGroupBox.Size = new System.Drawing.Size(85, 40);
            this.magnificationGroupBox.TabIndex = 1;
            this.magnificationGroupBox.TabStop = false;
            this.magnificationGroupBox.Text = "Magnification";
            // 
            // charNameBox
            // 
            this.charNameBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.charNameBox.Location = new System.Drawing.Point(3, 16);
            this.charNameBox.Name = "charNameBox";
            this.charNameBox.ReadOnly = true;
            this.charNameBox.Size = new System.Drawing.Size(79, 20);
            this.charNameBox.TabIndex = 3;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.charNameBox);
            this.groupBox2.Location = new System.Drawing.Point(103, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(85, 40);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Char Name";
            // 
            // unicodeT
            // 
            this.unicodeT.Controls.Add(this.codepointBox);
            this.unicodeT.Location = new System.Drawing.Point(194, 12);
            this.unicodeT.Name = "unicodeT";
            this.unicodeT.Size = new System.Drawing.Size(85, 40);
            this.unicodeT.TabIndex = 5;
            this.unicodeT.TabStop = false;
            this.unicodeT.Text = "Unicode";
            // 
            // codepointBox
            // 
            this.codepointBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.codepointBox.Location = new System.Drawing.Point(3, 16);
            this.codepointBox.Name = "codepointBox";
            this.codepointBox.ReadOnly = true;
            this.codepointBox.Size = new System.Drawing.Size(79, 20);
            this.codepointBox.TabIndex = 6;
            // 
            // PreviewChart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(304, 311);
            this.Controls.Add(this.unicodeT);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.magnificationGroupBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MinimumSize = new System.Drawing.Size(300, 300);
            this.Name = "PreviewChart";
            this.Text = "Chart";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PreviewChart_FormClosing);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.PreviewChart_MouseClick);
            ((System.ComponentModel.ISupportInitialize)(this.magUpDown)).EndInit();
            this.magnificationGroupBox.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.unicodeT.ResumeLayout(false);
            this.unicodeT.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NumericUpDown magUpDown;
        private System.Windows.Forms.GroupBox magnificationGroupBox;
        private System.Windows.Forms.TextBox charNameBox;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox unicodeT;
        private System.Windows.Forms.TextBox codepointBox;
    }
}