using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Mfe
{
    public partial class CharPreviewer : UserControl
    {
        public CharPreviewer()
        {
            InitializeComponent();
        }

        protected Mfe.Char currentChar = null;
        public Char Char
        {
            get
            {
                return currentChar;
            }
            set
            {
                currentChar = value;
                Invalidate();
            }
        }

        protected int size = 1;
        public int CharScale
        {
            get
            {
                return size;
            }
            set
            {
                if (value < 1)
                    throw new ArgumentOutOfRangeException("Size must be a positive non-zero integer.");
                size = value;
                Invalidate();
            }
        }

        protected Brush BlackBrush = new SolidBrush(Color.Black);
        protected Brush WhiteBrush = new SolidBrush(Color.White);

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (currentChar == null)
                return;
            currentChar.Paint(this.Margin.Left, this.Margin.Top, size, size, BlackBrush, WhiteBrush, e.Graphics);
        }
    }
}
