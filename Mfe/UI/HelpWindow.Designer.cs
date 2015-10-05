namespace Mfe
{
    partial class HelpWindow
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
            this.currentItemRichTextBox = new System.Windows.Forms.RichTextBox();
            this.helpSplitContainer = new System.Windows.Forms.SplitContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.topicsTreeView = new System.Windows.Forms.TreeView();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.helpSplitContainer.Panel1.SuspendLayout();
            this.helpSplitContainer.Panel2.SuspendLayout();
            this.helpSplitContainer.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // currentItemRichTextBox
            // 
            this.currentItemRichTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.currentItemRichTextBox.Location = new System.Drawing.Point(0, 0);
            this.currentItemRichTextBox.Name = "currentItemRichTextBox";
            this.currentItemRichTextBox.Size = new System.Drawing.Size(363, 361);
            this.currentItemRichTextBox.TabIndex = 0;
            this.currentItemRichTextBox.Text = "";
            // 
            // helpSplitContainer
            // 
            this.helpSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.helpSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.helpSplitContainer.Name = "helpSplitContainer";
            // 
            // helpSplitContainer.Panel1
            // 
            this.helpSplitContainer.Panel1.Controls.Add(this.panel1);
            // 
            // helpSplitContainer.Panel2
            // 
            this.helpSplitContainer.Panel2.Controls.Add(this.currentItemRichTextBox);
            this.helpSplitContainer.Size = new System.Drawing.Size(584, 361);
            this.helpSplitContainer.SplitterDistance = 217;
            this.helpSplitContainer.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(217, 361);
            this.panel1.TabIndex = 3;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.topicsTreeView);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(217, 361);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Topics";
            // 
            // topicsTreeView
            // 
            this.topicsTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.topicsTreeView.Location = new System.Drawing.Point(3, 16);
            this.topicsTreeView.Name = "topicsTreeView";
            this.topicsTreeView.Size = new System.Drawing.Size(211, 342);
            this.topicsTreeView.TabIndex = 0;
            this.topicsTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.topicsTreeView_AfterSelect);
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.Filter = "All files|*.*";
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "All files|*.*";
            // 
            // HelpWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 361);
            this.Controls.Add(this.helpSplitContainer);
            this.Name = "HelpWindow";
            this.ShowIcon = false;
            this.Text = "Help";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.HelpWindow_FormClosing);
            this.helpSplitContainer.Panel1.ResumeLayout(false);
            this.helpSplitContainer.Panel2.ResumeLayout(false);
            this.helpSplitContainer.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox currentItemRichTextBox;
        private System.Windows.Forms.SplitContainer helpSplitContainer;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TreeView topicsTreeView;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Panel panel1;
    }
}