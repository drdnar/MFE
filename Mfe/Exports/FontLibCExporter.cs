using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mfe.Exports
{
    public class FontLibCExporter
    {
        [FontExporter(FileTypeExtension = "inc", FileTypeDescription = "FontLibC C include file", HelpText = "Exports to a C source code file for inclusion in source code.")]
        public static void ExportData(Mfe.Font font, string path)
        {
            byte[] data = new byte[65536];
            int dataLength = 0;
            int row, col;
            bool Inverted = font["Inverted"] == "true";
            //string baseName = font["AsmName"] ?? "font";
            // Font format version
            data[dataLength++] = 0;
            // Height
            data[dataLength++] = font.Height;
            // Glyph count
            data[dataLength++] = (byte)(font.LastCodePoint - font.FirstCodePoint + 1);
            // First codepoint
            data[dataLength++] = (byte)font.FirstCodePoint;
            int locationOfWidthsTable = dataLength;
            dataLength += 3;
            int locationOfBitmapsTable = dataLength;
            dataLength += 3;
            data[dataLength++] = font.ItalicSpaceAdjust;
            data[dataLength++] = font.SpaceAbove;
            data[dataLength++] = font.SpaceBelow;
            data[dataLength++] = font.Weight;
            data[dataLength++] = font.Style;
            data[dataLength++] = font.CapHeight;
            data[dataLength++] = font.XHeight;
            data[dataLength++] = font.BaseLine;
            // Widths table
            data[locationOfWidthsTable++] = (byte)(dataLength & 0xFF);
            data[locationOfWidthsTable++] = 0;
            data[locationOfWidthsTable++] = 0;
            for (int i = font.FirstCodePoint; i <= font.LastCodePoint; i++)
            {
                data[dataLength++] = font[i].Width;
            }              
            // Bitmaps locations table
            data[locationOfBitmapsTable++] = (byte)(dataLength & 0xFF);
            data[locationOfBitmapsTable++] = (byte)((dataLength >> 8) & 0xFF);
            data[locationOfBitmapsTable++] = 0;
            int locationOfNextBitmapOffset = dataLength;
            dataLength += (font.LastCodePoint - font.FirstCodePoint + 1) * 2;
            // Bitmaps
            for (int i = font.FirstCodePoint; i <= font.LastCodePoint; i++)
            {
                Char ch = font[i];
                int bitmapoffset = -2 + ((ch.Width > 0 ? ch.Width : 1) - 1) / 8;
                bitmapoffset += dataLength;
                data[locationOfNextBitmapOffset++] = (byte)(bitmapoffset & 0xFF);
                data[locationOfNextBitmapOffset++] = (byte)((bitmapoffset >> 8) & 0xFF);
                //data[locationOfNextBitmapOffset++] = (byte)(bitmapoffset >> 16);
                if (ch.Width > 24)
                    throw new ArgumentException("This format does not support glyphs wider than 24 pixels.");
                for (row = 0; row < font.Height; row++)
                {
                    // Output distinct bytes, but in little-endian order
                    // The format also requires omitting the least-significant byte(s) if they're unused,
                    // so we have to output the bytes from right-to-left, and the bits from left-to-right.
                    for (int bit = ((ch.Width > 0 ? ch.Width : 1) - 1) & ~7; bit >= 0; bit -= 8) // Initializer gives 0 for 0-8, 8 for 9-16, 16 for 17-24
                    {
                        int b = 0;
                        for (col = 0; col < 8; col++)
                            if (bit + col >= ch.Width)
                                b = (b << 1) | 0;
                            else if (ch[row, bit + col] ^ Inverted)
                                b = (b << 1) | 1;
                            else
                                b = (b << 1) | 0;
                        data[dataLength++] = (byte)b;
                    }
                }
            }
            // Convert binary data into string
            StringBuilder str = new StringBuilder();
            for (int i = 0; i < dataLength; i++)
            {
                str.Append("0x");
                str.Append(data[i].ToString("X2"));
                str.Append(", ");
                if (i % 16 == 15)
                    str.AppendLine();
            }
            System.IO.File.WriteAllText(path, str.ToString());
        }
    }
}
