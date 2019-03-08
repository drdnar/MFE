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
    public partial class CharEditor : UserControl
    {
        protected int BorderX = 3;
        protected int BorderY = 3;

        protected int lastX = -1;
        protected int lastY = -1;

        protected bool toggleMode = false;
        /// <summary>
        /// If set, clicking on a square will toggle it instead of being set or reset depending on whether it was a left or right click.
        /// </summary>
        public bool ToggleMode
        {
            get
            {
                return toggleMode;
            }
            set
            {
                toggleMode = value;
                Invalidate();
            }
        }

        protected bool fillOnDrag = false;
        /// <summary>
        /// If true, clicking and dragging will fill everywhere the mouse covers.
        /// </summary>
        public bool FillOnDrag
        {
            get
            {
                return fillOnDrag;
            }
            set
            {
                fillOnDrag = value;
                Invalidate();
            }
        }

        protected int scale;
        /// <summary>
        /// Sets how magnified the character appears.
        /// </summary>
        public int CharScale
        {
            get
            {
                return scale;
            }
            set
            {
                if (value > 0)
                    scale = value;
                if (autoscaleLineThickness)
                    autoscaleLines();
            }
        }

        protected Char currentChar = null;
        
        /// <summary>
        /// Sets the current character being edited.
        /// </summary>
        public Char CurrentChar
        {
            get
            {
                return currentChar;
            }
            set
            {
                currentChar = value;
                OnCurrentCharChanged();
                resetCursor();
            }
        }
        
        /// <summary>
        /// Issued whenever the current character being edited changes.
        /// </summary>
        public event EventHandler CurrentCharChanged;
        protected void OnCurrentCharChanged()
        {
            if (CurrentCharChanged != null)
                CurrentCharChanged(this, new EventArgs());
            OnPixelChanged();
        }

        public event EventHandler PixelChanged;
        public void OnPixelChanged()
        {
            if (PixelChanged != null)
                PixelChanged(this, new EventArgs());
        }
        
        public CharEditor()
        {
            InitializeComponent();
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true);
            cursorBrush = new SolidBrush(Color.FromArgb(128, System.Drawing.SystemColors.Highlight));
        }

        Brush cursorBrush;

        #region Colors
        protected Brush foreBrush = new SolidBrush(DefaultForeColor);
        protected Brush backBrush = new SolidBrush(DefaultBackColor);

        protected Color gridColor = DefaultForeColor;
        protected Pen gridPen = new Pen(DefaultForeColor);
        /// <summary>
        /// Sets the color of the grid.
        /// </summary>
        public Color GridColor
        {
            get
            {
                return gridColor;
            }
            set
            {
                gridColor = value;
                gridPen = new Pen(new SolidBrush(gridColor));
                gridPen.Width = gridThickness;
            }
        }
        
        protected Color widthLineColor = DefaultForeColor;
        protected Pen widthLinePen = new Pen(DefaultForeColor);
        /// <summary>
        /// Sets the color of the width line.
        /// </summary>
        public Color WidthLineColor
        {
            get
            {
                return widthLineColor;
            }
            set
            {
                widthLineColor = value;
                widthLinePen = new Pen(new SolidBrush(widthLineColor));
                widthLinePen.Width = widthLineThickness;
            }
        }
        
        protected Color baselineColor = Color.Red; // DefaultForeColor;
        protected Pen baselinePen = new Pen(DefaultForeColor);
        /// <summary>
        /// Sets the color of the baseline indicator.
        /// </summary>
        public Color BaselineColor
        {
            get
            {
                return baselineColor;
            }
            set
            {
                baselineColor = value;
                baselinePen = new Pen(new SolidBrush(baselineColor));
                baselinePen.Width = capLineThickness;
            }
        }
        
        protected Color xHeightColor = Color.Red; // DefaultForeColor;
        protected Pen xHeightPen = new Pen(DefaultForeColor);
        /// <summary>
        /// Sets the color of the x-height indicator.
        /// </summary>
        public Color XHeightColor
        {
            get
            {
                return xHeightColor;
            }
            set
            {
                xHeightColor = value;
                xHeightPen = new Pen(new SolidBrush(xHeightColor));
                xHeightPen.Width = capLineThickness;
            }
        }

        protected Color capHeightColor = Color.Red; // DefaultForeColor;
        protected Pen capHeightPen = new Pen(DefaultForeColor);
        /// <summary>
        /// Sets the color of the cap height indicator.
        /// </summary>
        public Color CapHeightColor
        {
            get
            {
                return capHeightColor;
            }
            set
            {
                capHeightColor = value;
                capHeightPen = new Pen(new SolidBrush(capHeightColor));
                capHeightPen.Width = capLineThickness;
            }
        }


        protected bool showGuides = true;
        /// <summary>
        /// Set to show guides
        /// </summary>
        public bool ShowGuides
        {
            get
            {
                return showGuides;
            }
            set
            {
                showGuides = value;
            }
        }


        protected bool showGrid = true;
        /// <summary>
        /// Set to true to show a grid
        /// </summary>
        public bool ShowGrid
        {
            get
            {
                return showGrid;
            }
            set
            {
                showGrid = value;
            }
        }

        protected int gridThickness = 1;
        /// <summary>
        /// Controls the thickness of the grid lines
        /// </summary>
        public int GridThickness
        {
            get
            {
                return gridThickness;
            }
            set
            {
                if (gridPen != null)
                    gridPen.Width = value;
                gridThickness = value;
            }
        }
        
        protected int widthLineThickness = 1;
        /// <summary>
        /// Controls the thickness of the line that shows the logical width of the character.
        /// </summary>
        public int WidthLineThickness
        {
            get
            {
                return widthLineThickness;
            }
            set
            {
                if (widthLinePen != null)
                    widthLinePen.Width = value;
                widthLineThickness = value;
            }
        }
        
        protected int capLineThickness = 1;
        /// <summary>
        /// Controls the thickness of the cap hight, x-height, and base lines.
        /// </summary>
        public int CapLineThickness
        {
            get
            {
                return capLineThickness;
            }
            set
            {
                if (capHeightPen != null)
                    capHeightPen.Width = value;
                if (xHeightPen != null)
                    xHeightPen.Width = value;
                if (baselinePen != null)
                    baselinePen.Width = value;
                capLineThickness = value;   
            }
        }
        
        protected bool autoscaleLineThickness = true;
        /// <summary>
        /// Set to true to make changing the scale automatically change the line thicknesses.
        /// </summary>
        public bool AutoscaleLineThickness
        {
            get
            {
                return autoscaleLineThickness;
            }
            set
            {
                autoscaleLineThickness = value;
            }
        }
        protected void autoscaleLines()
        {
            if (scale == 1)
            {
                GridThickness = 0;
                WidthLineThickness = 0;
                CapLineThickness = 0;
            }
            else if (scale < 8)
            {
                GridThickness = 1;
                WidthLineThickness = 1;
                CapLineThickness = 1;
            }
            else if (scale < 24)
            {
                GridThickness = 1;
                WidthLineThickness = 2;
                CapLineThickness = 2;
            }
            else if (scale < 48)
            {
                GridThickness = 1;
                WidthLineThickness = 3;
                CapLineThickness = 3;
            }
            else if (scale < 64)
            {
                GridThickness = 2;
                WidthLineThickness = 4;
                CapLineThickness = 4;
            }
            
        }
        #endregion
        
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (currentChar == null)
                return;
            validateCursor();
            Graphics g = e.Graphics;
            BorderX = System.Windows.Forms.SystemInformation.BorderSize.Width;
            BorderY = System.Windows.Forms.SystemInformation.BorderSize.Height;
            //public void Paint(int xBase, int yBase, float xScale, float yScale, Pen forePen, Pen backPen, Pen gridPen, Pen widthLinePen, System.Windows.Forms.PaintEventArgs e)
            Brush white = new SolidBrush(Color.White);
            Brush black = new SolidBrush(Color.Black);
            float sx = currentChar.AspectRatioWidth * scale;
            float sy = currentChar.AspectRatioHeight * scale;
            currentChar.PaintEditor(BorderX, BorderY, sx, sy, foreBrush, backBrush, showGrid ? gridPen : null, widthLinePen, e.Graphics);
            if (showGuides)
            {
                g.DrawLine(baselinePen, BorderX, BorderY + sy * currentChar.BaseLine, BorderX + sx * currentChar.Width, BorderY + sy * currentChar.BaseLine);
                g.DrawLine(xHeightPen, BorderX, BorderY + sy * currentChar.XHeight, BorderX + sx * currentChar.Width, BorderY + sy * currentChar.XHeight);
                g.DrawLine(capHeightPen, BorderX, BorderY + sy * currentChar.CapHeight, BorderX + sx * currentChar.Width, BorderY + sy * currentChar.CapHeight);
            }
            if (blink)
                g.FillRectangle(cursorBrush, BorderX + sx * CursorX, BorderY + sy * CursorY, sx, sy);
            //gridPen.Color = Color.FromArgb(64, gridPen.Color.R, gridPen.Color.G, gridPen.Color.B);
            //currentChar.Paint(BorderX, BorderY, currentChar.AspectRatioWidth * scale, currentChar.AspectRatioHeight * scale, black, white, gridPen, widthLinePen, e);
        }

        private void CharEditor_ForeColorChanged(object sender, EventArgs e)
        {
            foreBrush = new SolidBrush(ForeColor);
        }

        private void CharEditor_BackColorChanged(object sender, EventArgs e)
        {
            backBrush = new SolidBrush(BackColor);
        }

        private void CharEditor_MouseClick(object sender, MouseEventArgs e)
        {
            handleClick(e);
            lastX = lastY = -1;
        }

        private void handleClick(MouseEventArgs e, bool noDouble = false)
        {
            float scalex = scale * currentChar.AspectRatioWidth;
            float scaley = scale * currentChar.AspectRatioHeight;
            if (e.X >= BorderX && e.X < BorderX + scalex * currentChar.Width
                && e.Y >= BorderY && e.Y < BorderY + scaley * currentChar.Height)
            {
                int x = (int)Math.Floor((e.X - BorderX) / scalex);
                int y = (int)Math.Floor((e.Y - BorderY) / scaley);
                CursorX = x;
                CursorY = y;
                if (lastX == x && lastY == y)
                    return;
                lastX = x;
                lastY = y;
                if (ToggleMode)
                    currentChar[y, x] = !currentChar[y, x];
                else
                    if (e.Button == System.Windows.Forms.MouseButtons.Left)
                        currentChar[y, x] = true;
                    else if (e.Button == System.Windows.Forms.MouseButtons.Right)
                        currentChar[y, x] = false;
                resetCursor();
                OnPixelChanged();
            }
            else
            {
                lastX = lastY = -1;
            }
        }

        private void CharEditor_MouseMove(object sender, MouseEventArgs e)
        {
            //e.
            if (fillOnDrag && (e.Button == System.Windows.Forms.MouseButtons.Left || e.Button == System.Windows.Forms.MouseButtons.Right))
                handleClick(e, true);
            else
                lastX = lastY = -1;
        }


        private int CursorX = 0;
        private int CursorY = 0;

        protected override bool IsInputKey(System.Windows.Forms.Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Up:
                case Keys.Down:
                case Keys.Left:
                case Keys.Right:
                case Keys.Space:
                case Keys.Enter:
                    return true;
            }
            return base.IsInputKey(keyData);
        }

        private void CharEditor_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.Up:
                    curUp();
                    e.Handled = true;
                    break;
                case Keys.Down:
                    curDown();
                    e.Handled = true;
                    break;
                case Keys.Left:
                    curLeft();
                    e.Handled = true;
                    break;
                case Keys.Right:
                    curRight();
                    e.Handled = true;
                    break;
                case Keys.Space:
                case Keys.Enter:
                    validateCursor();
                    currentChar[CursorY, CursorX] = !currentChar[CursorY, CursorX];
                    resetCursor();
                    OnPixelChanged();
                    e.Handled = true;
                    break;
            }
        }

        bool blink = false;

        private void validateCursor()
        {
            if (CursorX < 0)
                CursorX = 0;
            if (CursorX >= currentChar.Width)
                CursorX = currentChar.Width - 1;
            if (CursorY < 0)
                CursorY = 0;
            if (CursorY >= currentChar.Height)
                CursorY = currentChar.Height - 1;
        }

        private void curUp()
        {
            if (CursorY > 0)
                CursorY--;
            resetCursor();
        }

        private void curDown()
        {
            if (CursorY < currentChar.Height - 1)
                CursorY++;
            resetCursor();
        }

        private void curLeft()
        {
            if (CursorX > 0)
                CursorX--;
            resetCursor();
        }

        private void curRight()
        {
            if (CursorX < currentChar.Width - 1)
                CursorX++;
            resetCursor();
        }

        private void resetCursor()
        {
            blink = true;
            cursorTimer.Stop();
            cursorTimer.Start();
            Invalidate();
        }

        private void cursorTimer_Tick(object sender, EventArgs e)
        {
            blink = !blink;
            Invalidate();
        }
    }
}
