using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mfe.Exports
{
    [FontExporter]
    public static class OmnicalcFontExporter
    {
        [FontExporter(FileTypeExtension = "8xp", FileTypeDescription = "Omnicalc font", HelpText = "Exports to an Omnicalc font file.")]
        public static void ExportData(Mfe.Font font, string path)
        {
            // Check to make sure the font even looks approximately compatible
            if (font.VariableWidth)
                throw new ArgumentException("Omnicalc does not support variable width Fonts.");
            if (!(font.Width == 8 || font.Width == 6 || font.Width == 5))
                throw new ArgumentException("Invalid font width for Omnicalc.");
            if (font.Height != 7)
                throw new ArgumentException("Omnicalc fonts must be 7 pixels high.");
            // First, we're going to stuff all the data we want to export into
            // an array.  Then, we'll use the program class to package the data
            // into a valid program file and return it to the caller.
            byte[] data = new byte[256 * 7 + 8];
            // Master pointer
            int i = 0;
            // Create file header
            string headerData = "omnicalc";
            for (int a = 0; a < headerData.Length; a++)
                data[i++] = (byte)headerData[a];
            // Add font glyphs
            // Char: 01234567
            // Omni: 76543210
            int temp;
            for (int ch = 0; ch < 256; ch++)
            {
                for (int row = 0; row < font.Height; row++)
                {
                    temp = 0;
                    for (int col = 0; col < font.Width; col++)
                        if (font[ch][row, col])
                            temp = temp | (1 << (5 - col));
                    data[i++] = (byte)temp;
                }
            }
            // Now package the data into a program variable.
            Program prgm = new Program();
            prgm.Type = VariableType.ProtProgObj;
            prgm.Data = data;
            System.IO.File.WriteAllBytes(path, prgm.Export(archived: true));
        }
    }
}
