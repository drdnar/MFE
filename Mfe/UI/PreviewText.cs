using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Mfe
{
    public partial class PreviewText : Form
    {

        protected MasterWindow Master;
        public PreviewText(MasterWindow master)
        {
            Master = master;
            InitializeComponent();
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true);
            fontTextPanel.CurrentFont = CurrentFont;
            backColorButton.BackColor = PreviewBackColor;
            foreColorButton.BackColor = PreviewForeColor;
            this.MdiParent = master;
            Master.CharEditorWindow.charEditor.PixelChanged += charEditor_PixelChanged;
            fontTextPanel.ScaleFactor = (int)sizeUpDown.Value;
            fontTextPanel.ScreenWidth = (int)widthUpDown.Value;
            fontTextPanel.ScreenHeight = (int)heightUpDown.Value;
            Show();
        }

        public void RefreshText()
        {
            fontTextPanel.CurrentFont = CurrentFont;
            fontTextPanel.Invalidate();
        }

        private void charEditor_PixelChanged(object sender, EventArgs e)
        {
            fontTextPanel.Invalidate();
        }

        protected Font CurrentFont
        {
            get
            {
                return Master.CurrentFont;
            }
        }

        public Color PreviewForeColor = Color.Black;

        public Color PreviewBackColor = Color.White;

        private void sizeUpDown_ValueChanged(object sender, EventArgs e)
        {
            fontTextPanel.ScaleFactor = (int)sizeUpDown.Value;
        }

        private void widthUpDown_ValueChanged(object sender, EventArgs e)
        {
            fontTextPanel.ScreenWidth = (int)widthUpDown.Value;
        }

        private void heightUpDown_ValueChanged(object sender, EventArgs e)
        {
            fontTextPanel.ScreenHeight = (int)heightUpDown.Value;
        }

        private void foreColorButton_Click(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                fontTextPanel.ForeColor = foreColorButton.BackColor = PreviewForeColor = colorDialog.Color;
                fontTextPanel.Invalidate();
            }
        }

        private void backColorButton_Click(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                fontTextPanel.VirtualBackColor = backColorButton.BackColor = PreviewBackColor = colorDialog.Color;
                fontTextPanel.Invalidate();
            }
        }

        private void textBox_TextChanged(object sender, EventArgs e)
        {
            fontTextPanel.Text = textBox.Text;
            fontTextPanel.CurrentFont = CurrentFont;
            fontTextPanel.Invalidate();
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            fontTextPanel.CurrentFont = CurrentFont;
            fontTextPanel.Invalidate();
        }

        private void PreviewText_FormClosing(object sender, FormClosingEventArgs e)
        {
            Master.CharEditorWindow.charEditor.PixelChanged -= charEditor_PixelChanged;
        }
    }
}
