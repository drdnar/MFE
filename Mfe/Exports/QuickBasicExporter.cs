using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mfe.Exports
{
    [FontExporter]
    public class QuickBasicExporter
    {
        [FontExporter(FileTypeExtension = "bas", FileTypeDescription = "Variable Width QUICKBasic DATA font", HelpText = "Exports to .BAS file for import.")]
        public static void ExportData(Mfe.Font font, string path)
        {
            StringBuilder data = new StringBuilder(65536);
            int row, col;
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
            data.AppendLine("' FONT METADATA");
            foreach (KeyValuePair<string, string> s in font.AboutData)
            {
                data.Append("' ");
                data.Append(s.Key);
                data.Append(" = ");
                data.Append(s.Value);
                data.AppendLine();
            }
            data.AppendLine("' font height");
            data.Append("DATA ");
            data.Append(font.Height);
            data.AppendLine();
            data.AppendLine("' ");
            data.AppendLine("' GLYPH DATA TABLE");
            for (int i = firstChar; i <= lastChar; i++)
            {
                // Header
                data.Append("' ");
                data.Append(i.ToString("X2"));
                if (i > 32 && i < 128)
                {
                    data.Append(" ");
                    data.Append((char)i);
                }
                // data.Append(" ");
                //data.Append(font[i].Name);
                data.AppendLine();
                data.Append("DATA ");
                data.Append(font[i].Width);
                data.Append(",");
                col = 0;
                row = 0;
                // Produce bitmap
                for (row = 0; row < font[i].Height; row++)
                {
                    //data.Append("&H");
                    short b = 0;
                    for (col = 0; col < font[i].Width; col++)
                    {
                        b |= (short)(font[i][row, col] ?  1 << ( 15 - col) : 0);
                    }
                    data.Append(b.ToString(""));
                    if (row != font[i].Height - 1)
                        data.Append(",");
                    else
                        data.AppendLine();
                }
            }
            System.IO.File.WriteAllText(path, data.ToString());
        }
    }
}
