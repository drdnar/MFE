using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mfe.Exports
{
    [FontExporter]
    public class MicrosFontExporter
    {
        [FontExporter(FileTypeExtension = "asm", FileTypeDescription = "MicrOS font", HelpText = "Exports to an assembly source code file for #inclusion into MicrOS.")]
        public static void ExportData(Mfe.Font font, string path)
        {
            StringBuilder data = new StringBuilder(8192);
            int shift, curByte;
            bool Inverted = font["Inverted"] == "true";
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
            for (int i = 0; i < 256; i++)
            {
                shift = 0;
                curByte = 0;
                data.Append("; Char ");
                data.Append(i.ToString("X2"));
                data.Append(" ");
                if (i > 31)
                    data.Append((char)i);
                data.AppendLine();
                // Produce bytes
                for (int col = 0; col < font[i].Width; col++)
                    for (int row = 0; row < font[i].Height; row++)
                    {
                        if (font[i][row, col] ^ Inverted)
                            curByte |= 1 << shift++;
                        else
                            shift++;
                        if (shift > 7)
                        {
                            data.Append("\x9.db\x9");
                            data.Append(ToBinaryByte(curByte));
                            data.Append("b");
                            data.AppendLine();
                            shift = 0;
                            curByte = 0;
                        }
                    }
                if (shift != 0)
                {
                    data.Append("\x9.db\x9");
                    data.Append(ToBinaryByte(curByte));
                    data.Append("b");
                    data.AppendLine();
                }
            }

            System.IO.File.WriteAllText(path, data.ToString());
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
