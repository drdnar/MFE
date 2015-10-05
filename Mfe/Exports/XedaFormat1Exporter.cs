using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mfe.Exports
{
    [FontExporter]
    public static class XedaFormat1Exporter
    {
        [FontExporter(FileTypeExtension = "asm", FileTypeDescription = "Xeda's Format #1", HelpText = "For 4x6 fonts. Generates a .asm file with packed nibbles.")]
        public static void ExportData(Mfe.Font font, string path)
        {
            // Check to make sure that it is an exportable font.
            if (font.VariableWidth)
                throw new ArgumentException("Font must fixed-width.");
            if (font.Height != 6 || font.Width != 4)
                throw new ArgumentException("Font size must be 4x6.");
            // Strings in C# are immutable, so doing "xxx" + "yyy" + "zzz" will end up creating five strings.
            // StringBuilder solves this problem of excessive string allocation.
            // For best performance, this number should be a little larger than the estimated font size for.
            StringBuilder data = new StringBuilder(65536);
            // I dunno, maybe you want to invert the font?
            bool inverted = font["Inverted"] == "true";
            // Font header
            data.Append("; FONT METADATA");
            foreach (KeyValuePair<string, string> s in font.AboutData)
            {
                data.Append("; ");
                data.Append(s.Key);
                data.Append(" = ");
                data.Append(s.Value);
                data.AppendLine();
            }
            data.AppendLine("; ");
            data.AppendLine("; FONT DATA");
            // Iterate over codepoints
            for (int i = 0; i < 256; i++)
            {
                // Header for each character
                data.Append("; Char ");
                data.Append(i.ToString("X2"));
                data.Append(" ");
                data.Append(font[i].Codepoint.ToString());
                data.Append(" ");
                data.Append(font[i].Name);
                data.Append(" ");
                data.AppendLine();
                // Produce bytes
                for (int row = 0; row < font[i].Height; row += 2)
                {
                    Char ch = font[i];
                    // \x9 creates a tab character
                    data.Append("\x9.db\x9$");
                    // I could have used a loop, but this required less thinking.
                    data.Append((
                        (
                              (ch[row, 0] ? 1 << 7 : 0)
                            | (ch[row, 1] ? 1 << 6 : 0)
                            | (ch[row, 2] ? 1 << 5 : 0)
                            | (ch[row, 3] ? 1 << 4 : 0)
                            | (ch[row + 1, 0] ? 1 << 3 : 0)
                            | (ch[row + 1, 1] ? 1 << 2 : 0)
                            | (ch[row + 1, 2] ? 1 << 1 : 0)
                            | (ch[row + 1, 3] ? 1 : 0)
                        )
                        ^
                        (
                            inverted ? 0xFF : 0
                        )
                    ).ToString("X2")
                    );
                    data.AppendLine();
                }
            }

            System.IO.File.WriteAllText(path, data.ToString());
        }
    }
}
