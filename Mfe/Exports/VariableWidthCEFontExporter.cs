using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mfe.Exports
{
    [FontExporter]
    public class VariableWidthCEFontExporter
    {
        [FontExporter(FileTypeExtension = "asm", FileTypeDescription = "Variable Width CE Font", HelpText = "Exports to an assembly source code file for #inclusion.")]
        public static void ExportData(Mfe.Font font, string path)
        {
            StringBuilder data = new StringBuilder(65536);
            int row, col, remainder;
            bool Inverted = font["Inverted"] == "true";
            string baseName = font["AsmName"] ?? "font";
            int firstChar;
            int lastChar;
            if (!Int32.TryParse((font["FirstChar"] ?? "0"), out firstChar))
                firstChar = 0;
            if (!Int32.TryParse((font["LastChar"] ?? "255"), out lastChar))
                firstChar = 255;
            if (firstChar < 0)
                firstChar = 0;
            if (lastChar > 255)
                lastChar = 255;
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
            for (int i = firstChar; i <= lastChar; i++)
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


            /*
            byte[] bytes = new byte[65536];
            int[] ptrs = new int[256];
            int ptr = (lastChar - firstChar) * 3 + 3;

            for (int i = firstChar; i <= lastChar; i++)
            {
                // Header
                bytes[(i - firstChar) * 3] = (byte)(ptr & 255);
                bytes[(i - firstChar) * 3 + 1] = (byte)((ptr >> 8) & 255);
                bytes[(i - firstChar) * 3 + 2] = (byte)((ptr >> 16) & 255);
                col = 0;
                row = 0;
                remainder = font[i].Width * font[i].Height;
                // Produce bitmap
                for (row = 0; row < font[i].Height; row++)
                {
                    for (col = 0; col < font[i].Width; col += 8)
                    {
                        int dataByte = 0;
                        for (int j = 0; j < 8; j++)
                            if (j + col < font[i].Width ^ Inverted)
                                if (font[i][row, col + j])
                                    dataByte = (dataByte << 1) | 1;
                                else
                                    dataByte <<= 1;
                        bytes[ptr++] = (byte)dataByte;
                    }
                }
            }

            int blah = 0;
            for (int i = 0; i < ptr; i++)
            {
                if (blah-- <= 1)
                {
                    data.AppendLine();
                    data.Append("\x9.db\x9");
                    blah = 8;
                }
                else
                    data.Append(", ");
                data.Append("0");
                data.Append(bytes[i].ToString("X2"));
                data.Append("h");
            }
            */


                for (int i = firstChar; i <= lastChar; i++)
                {
                    data.Append("\x9.dl\x9");
                    data.Append(baseName);
                    data.Append("Char");
                    data.Append(i.ToString("X2"));
                    data.Append(" - ");
                    data.Append(baseName);
                    data.Append("DataTable");
                    data.Append("\x9; ");
                    data.Append(font[i].Codepoint);
                    data.Append(" ");
                    data.Append(font[i].Name);
                    data.AppendLine();
                }
                data.AppendLine("; ");
                data.AppendLine("; GLYPH DATA");
                for (int i = firstChar; i <= lastChar; i++)
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
                    // Produce bitmap
                    for (row = 0; row < font[i].Height; row++)
                    {
                        data.Append("\x9.db\x9");
                        for (col = 0; col < font[i].Width; col += 8)
                        {
                            if (col != 0)
                                data.Append(", ");
                            for (int j = 0; j < 8; j++)
                                if (j + col >= font[i].Width ^ Inverted)
                                    data.Append("0");
                                else
                                    if (font[i][row, col + j])
                                        data.Append("1");
                                    else
                                        data.Append("0");
                            data.Append("b");
                        }
                        data.AppendLine();
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
            while (i-- > 0)
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
