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
    public partial class FontTextPanel : UserControl
    {
        public FontTextPanel()
        {
            InitializeComponent();
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true);
        }

        protected Font font;
        public Font CurrentFont
        {
            get
            {
                return font;
            }
            set
            {
                font = value;
                Invalidate();
            }
        }

        protected int scaleFactor = 2;
        public int ScaleFactor
        {
            get
            {
                return scaleFactor;
            }
            set
            {
                scaleFactor = value;
                Invalidate();
            }
        }

        protected int screenWidth = 96;
        public int ScreenWidth
        {
            get
            {
                return screenWidth;
            }
            set
            {
                screenWidth = value;
                Invalidate();
            }
        }

        protected int screenHeight = 64;
        public int ScreenHeight
        {
            get
            {
                return screenHeight;
            }
            set
            {
                screenHeight = value;
                Invalidate();
            }
        }
        protected string text = "The quick brown fox jumps over the lazy dog.";
        public override string Text
        {
            get
            {
                return text;
            }
            set
            {
                if (value != null && value.Length > 0)
                    text = value;
            }
        }

        protected SolidBrush foreBrush = new SolidBrush(Color.Black);
        protected override void OnForeColorChanged(EventArgs e)
        {
            base.OnForeColorChanged(e);
            foreBrush.Color = this.ForeColor;
        }

        protected SolidBrush screenBackBrush = new SolidBrush(Color.White);

        SolidBrush backBrush = new SolidBrush(Color.White);
        public Color VirtualBackColor
        {
            get
            {
                return backBrush.Color;
            }
            set
            {
                backBrush.Color = value;
            }
        }

        protected int tabWidth = 8;
        public int TabWidth
        {
            get
            {
                return tabWidth;
            }
            set
            {
                tabWidth = value;
            }
        }

        // TODO: Find a less convoluted way of doing this!
        protected override void OnPaint(PaintEventArgs e)
        {
            // Possible alternate superlong word behavior:
            // If word length is greater than the width of the screen,
            // force it to print without breaking first.
            base.OnPaint(e);
            if (font == null)
                return;
            int x = 0;
            int y = 0;
            
            float scalex = (float)this.scaleFactor * font.AspectRatioWidth;
            float scaley = (float)this.scaleFactor * font.AspectRatioHeight;
            e.Graphics.FillRectangle(backBrush, x, y, (screenWidth) * scalex, scaley * (screenHeight));

            // Handle stupid case
            if (text.Length == 0)
                return;
            // VS is anal about silly things
            int loc = 0;
            char currentChar = '\x0';
            //bool newLine = true;
            
            // I had these in separate functions, but I tired of passing all the variables
            Func<bool> IsWhiteSpace = () =>
                {
                    return text[loc] == ' ' || (int)text[loc] <= 0x0F || text[loc] == '\t';
                };
            
            Action NewLine = () =>
                {
                    y += CurrentFont.Height;
                    x = 0;
                };

            Func<int> GetWidthOfWord = () =>
                {
                    int l = 0;
                    int oldLoc = loc;
                    if (IsWhiteSpace())
                        return 0;
                    while (loc < text.Length && !IsWhiteSpace())
                        if (CurrentFont[text[loc]] != null)
                            l += CurrentFont[text[loc++]].Width;
                        else
                            loc++;
                    loc = oldLoc;
                    return l;
                };

            Func<Mfe.Char> FontChar = () =>
            {
                return CurrentFont[text[loc]] ?? CurrentFont[' '] ?? CurrentFont[32];
            };

            Func<bool> PrintWord = () =>
                {
                    while (loc < text.Length && !IsWhiteSpace())
                    {
                        if (font[text[loc]] == null)
                        {
                            if (++loc < text.Length)
                                return true;
                            continue;
                        }
                        if (x + FontChar().Width < screenWidth)
                            FontChar().PaintVirtual(x, y, screenWidth, screenHeight, scalex, scaley, foreBrush, null, e);
                        else
                            return false;
                        x += FontChar().Width;
                        loc++;
                    }
                    return true;
                };

            Func<bool> TryPrintWord = () =>
                {
                    if (GetWidthOfWord() + x < screenWidth)
                    {
                        return PrintWord();
                    }
                    return false;
                };

            while (loc < text.Length)
            {
                if (y + CurrentFont.Height > screenHeight)
                    break;
                currentChar = text[loc];
                if (currentChar == ' ')
                {
                    if (x + FontChar().Width > screenWidth)
                    {
                        NewLine();
                        //if (loc > 0 && text[loc - 1] != ' ' && loc + 1 < text.Length && text[loc + 1] == ' ')
                        if (loc > 0 && (text[loc - 1] != ' ' && text[loc - 1] != '\t') && loc + 1 < text.Length && (text[loc + 1] == ' ' || text[loc + 1] == '\t'))
                        {
                            loc++;
                        }
                        loc++;
                        continue;
                    }
                    FontChar().PaintVirtual(x, y, screenWidth, screenHeight, scalex, scaley, foreBrush, null, e);
                    x += FontChar().LogicalWidth;
                    loc++;
                }
                else if (currentChar == '\t')
                {
                    if (x + CurrentFont[' '].Width * tabWidth > screenWidth)
                    {
                        NewLine();
                        if (loc > 0 && (text[loc - 1] != ' ' && text[loc - 1] != '\t') && loc + 1 < text.Length && (text[loc + 1] == ' ' || text[loc + 1] == '\t'))
                        {
                            loc++;
                        }
                        loc++;
                        continue;
                    }
                    for (int i = 0; i < tabWidth; i++)
                    {
                        FontChar().PaintVirtual(x, y, screenWidth, screenHeight, scalex, scaley, foreBrush, null, e);
                        x += FontChar().LogicalWidth;
                    }
                    loc++;
                }
                else if (currentChar == (char)0x0D)
                {
                    if (loc + 1 < text.Length && text[loc + 1] == (char)0x0A)
                        loc++;
                    NewLine();
                    loc++;
                }
                else if (currentChar == (char)0x0A)
                {
                    NewLine();
                    if (y + CurrentFont.Height > screenHeight)
                        break;
                    loc++;
                }
                else if ((int)currentChar > 0x20)
                {
                    if (x == 0)
                    {
                        if (!PrintWord())
                            NewLine();
                    }
                    else
                        if (!TryPrintWord())
                            NewLine();
                }
            }
        }
    }
}
