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
    public partial class PreviewChart : Form
    {
        protected Form Master;
        public PreviewChart(Form master)
        {
            MasterWindow masterWindow = master as MasterWindow;
            BdfImporter importer = master as BdfImporter;
            Master = master;
            InitializeComponent();
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true);
            if (masterWindow != null)
            {
                masterWindow.PreviewChart = this;
                masterWindow.CharEditorWindow.charEditor.PixelChanged += charEditor_PixelChanged;
            }
            else if (importer != null)
            {
                importer.Chart = this;
            }
            this.MdiParent = master;            
            Show();
        }

        void charEditor_PixelChanged(object sender, EventArgs e)
        {
            Invalidate();
        }

        protected Font CurrentFont
        {
            get
            {
                if (Master is MasterWindow)
                    return ((MasterWindow)Master).CurrentFont;
                else if (Master is BdfImporter)
                    return ((BdfImporter)Master).ImportedFont;
                else
                    return null;
            }
        }

        public void RefreshData()
        {
            codepointBox.Text = "";
            charNameBox.Text = "";
            Invalidate();
        }
        
        private int cols = 16;
        private int rows = 16;
        private int maxwidth = 1;
        private float scalex = 1;
        private float scaley = 1;
        private int basex = 1;
        private int basey = 1;
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            
            maxwidth = 1;
            int height = CurrentFont.Height;
            foreach (Char c in CurrentFont.Glyphs)
                if (c.Width > maxwidth)
                    maxwidth = c.Width;
            scalex = (float)magUpDown.Value * CurrentFont.AspectRatioWidth;
            scaley = (float)magUpDown.Value * CurrentFont.AspectRatioHeight;
            basex = magnificationGroupBox.Location.X + 3;
            basey = magnificationGroupBox.Location.Y + magnificationGroupBox.Size.Height + 3;
            float x, y;
            for (int chr = 0; chr < Mfe.Font.MaximumCodePoints; chr++)
            {
                x = (chr % cols) * (scalex * (maxwidth + 1));
                y = (chr / cols) * (scaley * (CurrentFont.Height + 1));
                CurrentFont[chr].Paint(basex + (int)x, basey + (int)y, scalex, scaley, foreBrush, backBrush, e.Graphics);
            }
        }

        protected Brush foreBrush = new SolidBrush(Color.Black);
        protected Brush backBrush = new SolidBrush(Color.White);

        public Color PreviewForeColor = Color.Black;

        public Color PreviewBackColor = Color.White;

        private void magUpDown_ValueChanged(object sender, EventArgs e)
        {
            Invalidate();
        }

        private void PreviewChart_MouseClick(object sender, MouseEventArgs e)
        {
            // I've found that off-by-one-half is a thing that happens with rounding. . . .
            int col = (int)Math.Round((e.X - basex) / (scalex * (maxwidth + 1)) - 0.5);
            int row = (int)Math.Round((e.Y - basey) / (scaley * (CurrentFont.Height + 1)) - 0.5);
            if (col >= cols)
                return;
            if (row >= rows)
                return;
            if (col < 0)
                return;
            if (row < 0)
                return;
            byte ch = (byte)(row * cols + col);
            codepointBox.Text = CurrentFont[ch].Codepoint.ToString();
            charNameBox.Text = CurrentFont[ch].Name;
            MasterWindow master = Master as MasterWindow;
            if (master != null)
                if (master.CharEditorWindow != null)
                    if (master.CharEditorWindow.ChartNavigationMode)
                        master.CharEditorWindow.SelectChar(ch);
        }

        private void PreviewChart_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Master is MasterWindow)
            {
                ((MasterWindow)Master).CharEditorWindow.charEditor.PixelChanged -= charEditor_PixelChanged;
                ((MasterWindow)Master).PreviewChart = null;
            }
            else if (Master is BdfImporter)
                e.Cancel = true;
        }

    }
}
