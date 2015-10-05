namespace Mfe
{
    partial class CharEditor
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.cursorTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // cursorTimer
            // 
            this.cursorTimer.Enabled = true;
            this.cursorTimer.Interval = 500;
            this.cursorTimer.Tick += new System.EventHandler(this.cursorTimer_Tick);
            // 
            // CharEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DoubleBuffered = true;
            this.Name = "CharEditor";
            this.Size = new System.Drawing.Size(148, 148);
            this.BackColorChanged += new System.EventHandler(this.CharEditor_BackColorChanged);
            this.ForeColorChanged += new System.EventHandler(this.CharEditor_ForeColorChanged);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CharEditor_KeyDown);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.CharEditor_MouseClick);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.CharEditor_MouseMove);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer cursorTimer;
    }
}
