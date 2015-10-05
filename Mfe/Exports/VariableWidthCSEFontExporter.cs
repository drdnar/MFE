using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mfe.Exports
{
    [FontExporter]
    public class VariableWidthCSEFontExporter
    {
        [FontExporter(FileTypeExtension = "asm", FileTypeDescription = "Variable Width CSE Font", HelpText = "Exports to an assembly source code file for #inclusion.")]
        public static void ExportData(Mfe.Font font, string path)
        {
            StringBuilder data = new StringBuilder(65536);
            int row, col, remainder;
            bool Inverted = font["Inverted"] == "true";
            string baseName = font["AsmName"] ?? "font";
            data.Append(baseName);
            data.AppendLine(":");
            data.AppendLine("; FONT METADATA");
            foreach (KeyValuePair<string, string> s in font.AboutData)
            {
                data.Append("; ");
                data.Append(s.Key);
                data.Append(" = ");
                data.Append(s.Value);
                data.AppendLine();
            }
            data.Append("\x9.db\x9");
            data.Append(font.Height);
            data.Append("\x9; font height");
            data.AppendLine();
            data.AppendLine("; ");
            data.AppendLine("; GLYPH WIDTH TABLE");
            data.Append(baseName);
            data.Append("WidthTable:");
            data.AppendLine();
            for (int i = 0; i < 256; i++)
            {
                data.Append("\x9.db\x9");
                data.Append(font[i].Width);
                data.Append("\x9; ");
                data.Append(font[i].Codepoint);
                data.Append(" ");
                data.Append(font[i].Name);
                data.AppendLine();
            }
            data.AppendLine("; ");
            data.AppendLine("; GLYPH DATA TABLE");
            data.Append(baseName);
            data.Append("DataTable:");
            data.AppendLine();
            for (int i = 0; i < 256; i++)
            {
                data.Append("\x9.dw\x9");
                data.Append(baseName);
                data.Append("Char");
                data.Append(i.ToString("X2"));
                data.Append("\x9; ");
                data.Append(font[i].Codepoint);
                data.Append(" ");
                data.Append(font[i].Name);
                data.AppendLine();
            }
            data.AppendLine("; ");
            data.AppendLine("; GLYPH DATA");
            for (int i = 0; i < 256; i++)
            {
                // Header
                data.Append(baseName);
                data.Append("Char");
                data.Append(i.ToString("X2"));
                data.Append(": ; ");
                data.Append(font[i].Codepoint);
                data.Append(" ");
                data.Append(font[i].Name);
                data.AppendLine();
                col = 0;
                row = 0;
                remainder = font[i].Width * font[i].Height;
                // Number of bytes in glyph body
                data.Append("\x9.db\x9");
                if (remainder % 8 == 0)
                    data.Append(remainder / 8 - 1);
                else
                    data.Append(remainder / 8);
                data.Append(" ; body byte count");
                data.AppendLine(); 
                // Producy body bytes
                while (remainder > 8)
                {
                    data.Append("\x9.db\x9");
                    data.Append(ToBinaryByte(getNextByte(font[i], Inverted, ref row, ref col, out remainder)));
                    data.Append("b");
                    data.AppendLine();
                }
                // Number of bits in final byte
                data.Append("\x9.db\x9");
                data.Append(remainder);
                data.Append(" ; remaining bits in final byte");
                data.AppendLine();
                // Final byte
                data.Append("\x9.db\x9");
                data.Append(ToBinaryByte(getNextByte(font[i], Inverted, ref row, ref col, out remainder)));
                data.Append("b");
                data.AppendLine();
                if (remainder != 0)
                {
                    //System.IO.File.WriteAllText(path, data.ToString());
                    throw new Exception("Internal consistency error: expected remaining bits should have been zero, was " + remainder + " on char #" + i.ToString()
                        + " with col=" + col.ToString() + " and row=" + row.ToString());
                }
            }
            System.IO.File.WriteAllText(path, data.ToString());
        }

        private static byte getNextByte(Mfe.Char ch, bool Inverted, ref int row, ref int col, out int remainingBits)
        {
            int b = 0;
            int shift = 0;
            remainingBits = ch.Width * ch.Height - col * ch.Height - row;
            int i;
            if (remainingBits > 8)
                i = 8;
            else
                i = remainingBits;
            while (i --> 0)
            {
                if (ch[row, col] ^ Inverted)
                    b |= 1 << shift++;
                else
                    shift++;
                remainingBits--;
                row++;
                if (row >= ch.Height)
                {
                    col++;
                    row = 0;
                }
            }
            return (byte)b;
        }

        /// <summary>
        /// Very stupid zero-padding routine
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        private static string ToBinaryByte(int b)
        {
            string s = Convert.ToString(b, 2);
            while (s.Length < 8)
                s = "0" + s;
            return s;
        }
    }
    
}
