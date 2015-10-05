using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mfe.Exports
{
    [FontExporter]
    public class XedaFormat3Exporter
    {
        [FontExporter(FileTypeExtension = "asm", FileTypeDescription = "Xeda's Format #3",
            HelpText = "Variable width font format. Single height byte for whole font, then single width byte for whole font, followed by 256 characters.")]
        public static void ExportData(Mfe.Font font, string path)
        {
            // Check to make sure that it is an exportable font.
            if (font.VariableWidth)
                throw new ArgumentException("Font must fixed-width.");
            StringBuilder data = new StringBuilder(65536);
            // I dunno, maybe you want to invert the font?
            bool inverted = font["Inverted"] == "true";
            // Font header
            data.AppendLine("; FONT METADATA");
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
            data.AppendLine("; Width byte");
            data.Append("\x9.db\x9");
            data.Append(font.Width);
            data.AppendLine();
            data.AppendLine("; Height byte");
            data.Append("\x9.db\x9");
            data.Append(font.Height);
            data.AppendLine();
            Mfe.Char ch;
            int bytesPerLine = (font.Width + 7) / 8;
            int temp;
            // Iterate over codepoints
            for (int i = 0; i < 256; i++)
            {
                // Header for each character
                ch = font[i];
                data.Append("; Char ");
                data.Append(i.ToString("X2"));
                data.Append(" ");
                data.Append(ch.Codepoint.ToString());
                data.Append(" ");
                data.Append(ch.Name);
                data.Append(" ");
                data.AppendLine();
                // Produce bytes
                if (bytesPerLine != 0)
                    for (int row = 0; row < ch.Height; row++)
                    {
                        data.Append("\x9.db\x9");
                        for (int bite = 0; bite < bytesPerLine; bite++)
                        {
                            temp = 0;
                            for (int bit = 0; bit < (bite != bytesPerLine - 1 ? 8 : ch.Width - bite * 8); bit++)
                            {
                                temp |= (ch[row, bite * 8 + bit] ^ inverted ? 1 : 0) << (7 - bit);
                            }
                            data.Append("$");
                            data.Append(temp.ToString("X2"));
                            if (bite != bytesPerLine - 1)
                                data.Append(", ");
                        }
                        data.AppendLine();
                    }
            }

            System.IO.File.WriteAllText(path, data.ToString());
        }
    }
}
