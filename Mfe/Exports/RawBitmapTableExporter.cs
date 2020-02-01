using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mfe.Exports
{
    [FontExporter]
    public class RawBitmapTableExporter
    {
        [FontExporter(FileTypeExtension = "bin", FileTypeDescription = "Raw Bitmap Table", HelpText = "Exports to a raw, unstructured binary file.")]
        public static void ExportData(Mfe.Font font, string path)
        {
            if (font.VariableWidth)
                throw new ArgumentException("Raw bitmap fonts must be fixed-width.");
            if (font.Width < 1 || (font.Width & 0x7) > 0)
                throw new ArgumentException("Font width must be multiple of 8.");
            bool Inverted = font["Inverted"] == "true";
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

            byte[] data = new byte[(((font.Width - 1) >> 3) + 1) * font.Height * (lastChar - firstChar + 1)];
            int index = 0;
            for (int i = firstChar; i <= lastChar; i++)
            {
                for (int row = 0; row < font[i].Height; row++)
                {
                    int col;
                    int b = 0;
                    for (col = 0; col < font[i].Width; col++)
                    {
                        if (font[i][row, col] ^ Inverted)
                            b |= 0x80 >> (0 + (col & 0x7));
                        if ((col & 0x7) == 7)
                        {
                            data[index++] = (byte)b;
                            b = 0;
                        }
                    }
                    if ((col & 0x7) != 0)
                        data[index++] = (byte)b;
                }
            }
            System.IO.File.WriteAllBytes(path, data);
        }
    }
}
