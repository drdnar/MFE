using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mfe.Exports
{
    [FontExporter]
    public class FontLibCAsmExporter
    {
        [FontExporter(FileTypeExtension = "asm", FileTypeDescription = "FontLibC .asm file", HelpText = "Exports to an assembly source code file for inclusion in source code or building a font pack.")]
        public static void ExportData(Mfe.Font font, string path)
        {
            StringBuilder data = new StringBuilder(65536);
            int row, col;
            bool Inverted = font["Inverted"] == "true";
            //string baseName = font["AsmName"] ?? "font";
            string baseName;
            if (font.AboutData.TryGetValue("AsmName", out baseName))
                data.Append(baseName);
            else
                baseName  = "font";
            data.AppendLine(".header:");
            data.AppendLine("; FONT METADATA");
            foreach (KeyValuePair<string, string> s in font.AboutData)
            {
                data.Append("; ");
                data.Append(s.Key);
                data.Append(" = ");
                data.Append(s.Value);
                data.AppendLine();
            }
            data.AppendLine("\x0009db\x00090 ; font format version");
            data.Append("\x0009db\x0009"); data.Append(font.Height); data.AppendLine(" ; height");
            int glyphCount = (font.LastCodePoint - font.FirstCodePoint + 1) & 255;
            data.Append("\x0009db\x0009"); data.Append(glyphCount); data.AppendLine(" ; glyph count");
            //data.Append("\x0009db\x0009"); data.Append(); data.AppendLine(" ; ");
            data.Append("\x0009db\x0009"); data.Append(font.FirstCodePoint); data.AppendLine(" ; first codepoint");
            /*int locationOfWidthsTable = 18;
            data.Append("\x0009dl\x0009"); data.Append(locationOfWidthsTable); data.AppendLine(" ; offset to widths table");
            int locationOfBitmapsTable = locationOfWidthsTable + glyphCount;
            data.Append("\x0009dl\x0009"); data.Append(locationOfBitmapsTable); data.AppendLine(" ; offset to bitmaps table");
            int locationOfNextBitmap = locationOfBitmapsTable + glyphCount * 3;*/
            data.Append("\x0009dl\x0009"); /*data.Append(baseName); */data.Append(".widthsTable - .header"); /*data.Append(baseName);*/ data.AppendLine(" ; offset to widths table");
            data.Append("\x0009dl\x0009"); /*data.Append(baseName); */data.Append(".bitmapsTable - .header"); /*data.Append(baseName);*/ data.AppendLine(" ; offset to bitmaps offsets table");
            data.Append("\x0009db\x0009"); data.Append(font.ItalicSpaceAdjust); data.AppendLine(" ; italics space adjust");
            data.Append("\x0009db\x0009"); data.Append(font.SpaceAbove); data.AppendLine(" ; suggested blank space above");
            data.Append("\x0009db\x0009"); data.Append(font.SpaceBelow); data.AppendLine(" ; suggested blank space below");
            data.Append("\x0009db\x0009"); data.Append(font.Weight); data.AppendLine(" ; weight (boldness/thinness)");
            data.Append("\x0009db\x0009"); data.Append(font.Style); data.AppendLine(" ; style field");
            data.Append("\x0009db\x0009"); data.Append(font.CapHeight); data.AppendLine(" ; capital height");
            data.Append("\x0009db\x0009"); data.Append(font.XHeight); data.AppendLine(" ; lowercase x height");
            data.Append("\x0009db\x0009"); data.Append(font.BaseLine); data.AppendLine(" ; baseline height");

            /*data.Append(baseName); */data.AppendLine(".widthsTable: ; start of widths table");
            for (int i = font.FirstCodePoint; i <= font.LastCodePoint; i++)
            {
                data.Append("\x0009db\x0009"); data.Append(font[i].Width); 
                data.Append("\x0009; Code point ");
                data.Append(i.ToString("X2"));
                data.Append(" ");
                data.Append(font[i].Codepoint);
                data.Append(" ");
                data.Append(font[i].Name);
                data.AppendLine();
            }

            /*data.Append(baseName); */data.AppendLine(".bitmapsTable: ; start of table of offsets to bitmaps");
            for (int i = font.FirstCodePoint; i <= font.LastCodePoint; i++)
            {
                data.Append("\x0009dw\x0009");
                /*data.Append(baseName); */data.Append(".glyph_"); data.Append(i.ToString("X2"));
                data.Append(" - .header");// data.Append(baseName);
                if (font[i].Width <= 16)
                {
                    data.Append(" - ");
                    data.Append((byte)(2 - ((font[i].Width > 0 ? font[i].Width : 1) - 1) / 8));
                }
                data.AppendLine();
            }

            for (int i = font.FirstCodePoint; i <= font.LastCodePoint; i++)
            {
                // Header
                /*data.Append(baseName); */data.Append(".glyph_"); data.Append(i.ToString("X2"));
                data.Append(":\x0009; Code point "); data.Append(i.ToString("X2")); data.Append(" ");
                data.Append(font[i].Codepoint); data.Append(" ");
                data.AppendLine(font[i].Name);
                // Data
                Char ch = font[i];
                if (ch.Width > 24)
                    throw new ArgumentException("This format does not support glyphs wider than 24 pixels.");
                for (row = 0; row < font.Height; row++)
                {
                    data.Append("\x0009db\x0009");
                    // Output distinct bytes, but in little-endian order
                    // The format also requires omitting the least-significant byte(s) if they're unused,
                    // so we have to output the bytes from right-to-left, and the bits from left-to-right.
                    for (int bit = ((ch.Width > 1 ? ch.Width : 1) - 1) & ~7; bit >= 0; bit -= 8) // Initializer gives 0 for 0-8, 8 for 9-16, 16 for 17-24
                    {
                        for (col = 0; col < 8; col++)
                            if (bit + col >= ch.Width)
                                data.Append('0');
                            else if (ch[row, bit + col] ^ Inverted)
                                data.Append('1');
                            else
                                data.Append('0');
                        data.Append("b");
                        if (bit > 0)
                            data.Append(", ");
                    }
                    data.AppendLine();
                }
            }
            
            System.IO.File.WriteAllText(path, data.ToString());
        }
    }
}
