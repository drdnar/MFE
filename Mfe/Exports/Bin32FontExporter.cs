using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mfe.Exports
{
    [FontExporter]
    public class Bin32FontExporter
    {
        /* struct BinFont
         * {
         *      uint8_t magic[8];
         *      int32_t numberOfGlyphs;
         *      int32_t offsetToMetricsData;
         *      int32_t offsetToUnicodeConversionTable;
         *      int32_t offsetToWidthsTable;
         *      int32_t offsetToBitmapsTable;
         * }
         * 
         * struct MetricsData
         * {
         *      int32_t fileFormatVersion;
         *      uint32_t flags;
         *      uint8_t height;
         *      uint8_t baseLine;
         *      uint8_t xHeight;
         *      uint8_t capsHeight;
         * }
         * 
         * Fields are padded to align if needed.
         * 
         * The Unicode mapping table must be sorted in ascending order.  Consider locating the desired glyph through a binary search.
         * 
         * The bitmaps table itself is also just a table of offsets to the actual bitmaps.
         * Bitmaps are left-aligned so you can roll bits off the top and process them left-to-right.
         * The alternative was storing the bits right-aligned, but mirrored, which you probably would object to.
         * 
         */
        [FontExporter(FileTypeExtension = "binfont", FileTypeDescription = "Bin32Font", HelpText = "A custom format based around aligned 32-bit little-endian ints.")]
        public static void ExportData(Mfe.Font font, string path)
        {
            // Glyphs must be orded by code point, so sort them once now.
            List<Char> glyphs = new List<Mfe.Char>();
            for (int i = font.FirstCodePoint; i <= font.LastCodePoint; i++)
                if (!font[i].Name.ToUpper().EndsWith("#NOEXPORT"))
                    glyphs.Add(font[i]);
            glyphs.Sort((a, b) => a.Codepoint.CompareTo(b.Codepoint));

            int location;
            byte[] data = new byte[65536];
            for (location = 0; location < 7; location++)
            data[location] = (byte)"binfont"[location];
            data[location++] = 0;

            WriteDWord(data, ref location, glyphs.Count);
            int metricsOffsetLocation = location;
            location += 4;
            int unicodeConversionTableOffsetLocation = location;
            location += 4;
            int widthsOffsetLocation = location;
            location += 4;
            int bitmapsTableOffsetLocation = location;

            WriteDWord(data, ref location, 0);

            // Write metrics struct
            // Offset to metrics
            WriteDWord(data, ref metricsOffsetLocation, location);
            // Version
            WriteDWord(data, ref location, 0x10001);
            // Flags
            WriteDWord(data, ref location, font.VariableWidth ? 0 : 1);
            // Height
            data[location++] = font.Height;
            // Base line
            data[location++] = font.BaseLine;
            // x Height
            data[location++] = font.XHeight;
            // Caps height
            data[location++] = font.CapHeight;
            // Default glyph
            int missingGlyph = 0;
            //if (!Int32.TryParse((font["MissingGlyph"] ?? "0"), out missingGlyph))
            //    missingGlyph = 0;
            if (font.AboutData.ContainsKey("MissingGlyph"))
                if (font["MissingGlyph"].Length > 0)
                    missingGlyph = (int)font["MissingGlyph"][0];
            WriteDWord(data, ref location, missingGlyph);


            // Unicode conversion table
            WriteDWord(data, ref unicodeConversionTableOffsetLocation, location);
            for (int i = 0; i < glyphs.Count; i++)
                WriteDWord(data, ref location, glyphs[i].Codepoint);

            // Widths table
            WriteDWord(data, ref widthsOffsetLocation, location);
            for (int i = 0; i < glyphs.Count; i++)
                data[location++] = glyphs[i].Width;
            if ((location & 3) > 0)
                location = (location | 3) + 1; //location += 4 - location % 4; // Ensure alignment

            // Bitmaps offsets table
            WriteDWord(data, ref bitmapsTableOffsetLocation, location);
            int bitmapsTableOffset = location;
            location += 4 * (glyphs.Count);

            // Bitmaps
            for (int i = 0; i < glyphs.Count; i++)
            {
                WriteDWord(data, ref bitmapsTableOffset, location);
                for (int y = 0; y < font.Height; y++)
                {
                    for (int xx = 0; xx < glyphs[i].Width; xx += 32)
                    {
                        uint b = 0;
                        for (int x = 0; x < glyphs[i].Width; x++)
                            b = ((glyphs[i][y, x + xx] ? 0x80000000 : 0) >> x) | b;
                        WriteDWord(data, ref location, (int)b);
                    }
                }
            }

            // Write data to disk
            byte[] finalData = new byte[location];
            for (int i = 0; i < location; i++)
                finalData[i] = data[i];
            System.IO.File.WriteAllBytes(path, finalData);
        }

        private static void WriteDWord(byte[] data, ref int i, int val)
        {
            data[i++] = (byte)(val);
            data[i++] = (byte)(val >> 8);
            data[i++] = (byte)(val >> 16);
            data[i++] = (byte)(val >> 24);
        }
    }
}