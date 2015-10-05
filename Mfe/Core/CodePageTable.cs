using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mfe
{
    public class CodePageTable
    {
        public char[] CodePoints = new char[Font.MaximumCodePoints];
        public string[] Names = new string[Font.MaximumCodePoints];
        public string Name = "";
        public char this[int i]
        {
            get
            {
                return CodePoints[i];
            }
            set
            {
                CodePoints[i] = value;
            }
        }

        public void ApplyToFont(Mfe.Font font)
        {
            for (int i = 0; i < Font.MaximumCodePoints; i++)
            {
                font[i].Codepoint = CodePoints[i];
                font[i].Name = Names[i];
            }
            font["Code Page"] = Name;
        }

        public static CodePageTable CopyFromFont(Mfe.Font font)
        {
            CodePageTable ret = new CodePageTable();
            for (int i = 0; i < Font.MaximumCodePoints; i++)
                ret.CodePoints[i] = font[i].Codepoint;
            ret.Name = font["Code Page"] ?? "<unspecified>";
            return ret;
        }

        public static CodePageTable FromFile(string path)
        {
            CodePageTable ret = new CodePageTable();
            string[] lines = System.IO.File.ReadAllLines(path);
            ret.Name = lines[0];
            for (int i = 1; i <= 256; i++)
            {
                if (lines[i].Length > 0)
                {
                    ret.CodePoints[i - 1] = lines[i][0];
                    if (lines[i].Length > 1)
                        ret.Names[i - 1] = lines[i].Substring(1);
                    else
                        ret.Names[i - 1] = ret.CodePoints[i - 1].ToString();
                }
                else
                {
                    ret.CodePoints[i - 1] = '�';
                    ret.Names[i - 1] = "";
                }
            }

            return ret;
        }

        public void WriteToFile(string path)
        {
            List<string> l = new List<string>(260);
            l.Add(Name);
            for (int i = 0; i < Font.MaximumCodePoints; i++)
                l.Add(CodePoints[i].ToString() + Names[i]);
            System.IO.File.WriteAllLines(path, l.ToArray());
        }

    }
}
