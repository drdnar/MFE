using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mfe
{
    public class BdfFont
    {
        public List<Char> Chars = new List<Char>();
        //public string Name;
        public List<string> Comments = new List<string>();
        public byte Height;
        public string Copyright = "";
        public string Encoding = "";
        public int FbbX;
        //public int FbbY;
        public int Xoff;
        public int Yoff;
        /// <summary>
        /// Loads and parses a BDF font file.
        /// </summary>
        /// <param name="path"></param>
        public BdfFont(string path)
        {
            // Do import
            string[] data = System.IO.File.ReadAllLines(path);
            int x = 0;
            //Char tempChar;
            string temp1, temp2;
            int i1, i2, i3, i4;
            i1 = i2 = i3 = i4 = 0;
            ulong hex;
            temp1 = getNextField(data[x++], out temp2);
            if (!temp1.Equals("STARTFONT", StringComparison.InvariantCultureIgnoreCase))
                throw new ArgumentException("Font does not start with STARTFONT.");
            bool repeat = true;
            while (repeat)
            {
                temp1 = getNextField(data[x++], out temp2);
                if (temp1 == "CHARS")
                    repeat = false;
                else if (temp1 == "FONTBOUNDINGBOX")
                {
                    // Possibly the maximum width of any single char?
                    FbbX = Int32.Parse(getNextField(temp2, out temp1)); // fbbx
                    // Font height?
                    Height = (byte)Int32.Parse(getNextField(temp1, out temp2)); // fbby
                    // I don't know
                    Xoff = Int32.Parse(getNextField(temp2, out temp1)); // xoff from zero
                    // If negative, specifies blank space between lines?
                    // If negative, specifies descender height?
                    Yoff = Int32.Parse(getNextField(temp1, out temp2)); // yoff from zero
                }
            }
            //Height += (byte)(Yoff < 0 ? -Yoff : 0);
            int number = Int32.Parse(temp2);
            for (int i = 0; i < number; i++)
            {
                temp1 = getNextField(data[x++], out temp2);
                if (temp1 != "STARTCHAR")
                    return;
                Char ch = new Char();
                ch.Width = 16;
                ch.Height = 16;
                ch.Name = temp2;
                ch.Width = (byte)FbbX;
                ch.Height = this.Height;
                while (temp1 != "BITMAP")
                {
                    temp1 = getNextField(data[x++], out temp2);
                    if (temp1 == "ENCODING")
                        ch.Codepoint = (char)(Int32.Parse(temp2));
                    else if (temp1 == "BBX")
                    {
                        i1 = Int32.Parse(getNextField(temp2, out temp1)); // Width
                        i2 = Int32.Parse(getNextField(temp1, out temp2)); // Height
                        i3 = Int32.Parse(getNextField(temp2, out temp1)); // xoff from left
                        i4 = Int32.Parse(getNextField(temp1, out temp2)); // yoff from bottom, (+) equals go up
                    }
                    else if (temp1 == "DWIDTH")
                    {
                        ch.Width = (byte)Int32.Parse(getNextField(temp2, out temp1));
                    }
                }
                int row = 0;
                while (temp1 != "ENDCHAR")
                {
                    temp1 = getNextField(data[x++], out temp2);
                    if (temp1 != "ENDCHAR")
                    {
                        hex = Convert.ToUInt64(temp1, 16);
                        if (temp1.Length != 0 && temp1.Length % 2 == 0 && temp1.Length < 32)
                            hex = hex << (64 - (temp1.Length * 4));
                        for (int bit = 0; bit < ch.Width; bit++)
                        {
                            ch[row, bit] = (hex & 0x8000000000000000) != 0;
                            hex = hex << 1;
                        }
                        row++;
                    }
                }
                // Horizontal shift
                for (; i3 > 0; i3--)
                {
                    //ch.Width++;
                    ch.ShiftRight(true);
                }
                // if i3 < 0, well, that denotes kerning and we can't handle that
                // Compute how far to shift the bitmap vertically
                for (i4 = Height - i2 + Yoff - i4 /*(i4 < 0 ? -i4 : 0)*/; i4 > 0; i4--)
                    ch.ShiftDown(true);
                ch.LogicalWidth = ch.Width;
                Chars.Add(ch);
            }
        }

        /// <summary>
        /// Copies glyphs from this font into a new MFE font, using the
        /// characters found in the code page file to specify what characters
        /// to extract.
        /// </summary>
        /// <param name="cp"></param>
        /// <returns></returns>
        public Mfe.Font MakeMfeFont(CodePageTable cp)
        {
            Font font = new Font();
            font.Height = this.Height;
            font.WidthMustBeMultipleOfEight = false;
            //font["Name"] = this.Name;
            font["Code Page"] = cp.Name;
            font.BaseLine = (byte)(Height - (Yoff < 0 ? -Yoff : Yoff));
            for (int i = 0; i < cp.CodePoints.Length; i++)
            {
                var v = Chars.Where(ch => ch.Codepoint == cp[i]);
                if (v.FirstOrDefault() != null)
                    font[i] = v.First();
                else
                {
                    Mfe.Char t = new Char();
                    t.Height = font.Height;
                    font[i] = t;
                }
            }
            return font;
        }

        /// <summary>
        /// Parses the next field from a line in a BDF file. BDF specifies that
        /// string literals can contain spaces if enclosed in quotes; this
        /// contains logic for dealing with that.
        /// (Hopefully the logic works.)
        /// </summary>
        /// <param name="input"></param>
        /// <param name="remainder"></param>
        /// <returns></returns>
        private string getNextField(string input, out string remainder)
        {
            if (input.Length < 1)
            {
                remainder = input;
                return input;
            }
            int x;
            switch (input[0])
            {
                case ' ':
                    // Screw it, I'm lazy
                    return getNextField(input.Substring(1), out remainder);
                case '"':
                    x = input.IndexOf('"');
                    //if (x == -1)
                    remainder = input.Substring(++x);
                    return input.Substring(0, x);
                default:
                    x = input.IndexOf(' ');
                    if (x < 0)
                    {
                        remainder = "";
                        return input;
                    }
                    remainder = input.Substring(x + 1);
                    return input.Substring(0, x);
            }
        }
    }
}
