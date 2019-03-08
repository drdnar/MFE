using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mfe
{
    public class Font
    {
        public const byte CurrentVersionNumber = 1;
        public const byte CurrentMinorVersionNumber = 0;
        public int FirstCodePoint;
        public int LastCodePoint;
        public int NewlineCodePoint;
        public int TabCodePoint;
        public int SpaceCodePoint;
        public byte Style;
        public byte Weight = 0x80;
        public byte ItalicSpaceAdjust;

        #region Style
        public bool StyleSerif
        {
            get
            {
                return (Style & 1) != 0;
            }
            set
            {
                Style |= (byte)(value ? 1 : 0);
            }
        }

        [Flags]
        public enum Obliqueness
        {
            Upright = 0,
            Oblique = 2,
            Italic = 4,
        }

        public Obliqueness StyleObliqueness
        {
            get
            {
                return (Obliqueness)(Style & 6);
            }
            set
            {
                Style &= unchecked((byte)(~6));
                Style |= (byte)(value);
            }
        }
        #endregion

        protected float aspectRatioWidth = 1;
        public float AspectRatioWidth
        {
            get
            {
                return aspectRatioWidth;
            }
            set
            {
                aspectRatioWidth = value;
                for (int i = 0; i < MaximumCodePoints; i++)
                    Glyphs[i].AspectRatioWidth = aspectRatioWidth;
            }
        }

        protected float aspectRatioHeight = 1;
        public float AspectRatioHeight
        {
            get
            {
                return aspectRatioHeight;
            }
            set
            {
                aspectRatioWidth = value;
                for (int i = 0; i < MaximumCodePoints; i++)
                    Glyphs[i].AspectRatioHeight = aspectRatioHeight;
            }
        }

        protected bool variableWidth;
        /// <summary>
        /// If false, then all characters are forced to be the same width.
        /// </summary>
        public bool VariableWidth
        {
            get
            {
                return variableWidth;
            }
            set
            {
                variableWidth = value;
                if (!variableWidth)
                    for (int i = 0; i < 255; i++)
                    {
                        Glyphs[i].LogicalWidth = Glyphs[(int)'M'].LogicalWidth;
                        Glyphs[i].Width = Glyphs[(int)'M'].Width;
                    }
                if (value)
                    Style |= 8;
                else
                    Style &= unchecked((byte)(~8));
            }
        }

        protected bool widthMustBeMultipleOfEight = false;
        /// <summary>
        /// If true, characters must have a width that is a multiple of eight.
        /// </summary>
        public bool WidthMustBeMultipleOfEight
        {
            get
            {
                return widthMustBeMultipleOfEight;
            }
            set
            {
                widthMustBeMultipleOfEight = value;
                if (widthMustBeMultipleOfEight)
                {
                    for (int i = 0; i < MaximumCodePoints; i++)
                    {
                        Glyphs[i].Width = (byte)((Glyphs[i].Width / 8) * 8);
                    }
                }
                else if (variableWidth)
                    for (int i = 0; i < MaximumCodePoints; i++)
                        Glyphs[i].LogicalWidth = Glyphs[i].Width;
            }
        }

        /// <summary>
        /// If the font width is fixed, this adjusts the width.
        /// If not, returns the width of 'M' and writes throw an error.
        /// </summary>
        public byte Width
        {
            get
            {
                return this[(byte)'M'].Width;
            }
            set
            {
                if (variableWidth)
                    throw new InvalidOperationException("Cannot globally set font width on variable-width font.");
                if (widthMustBeMultipleOfEight && value % 8 != 0)
                    throw new ArgumentOutOfRangeException("WidthMustBeMultipleOfEight is set and value given is not a multiple of eight.");
                for (int i = 0; i < 256; i++)
                    Glyphs[i].Width = value;
            }
        }

        /// <summary>
        /// If the font width is fixed, this adjusts the width.
        /// If not, returns the width of 'M' and writes throw an error.
        /// </summary>
        public byte LogicalWidth
        {
            get
            {
                return this[(byte)'M'].LogicalWidth;
            }
            set
            {
                if (variableWidth)
                    throw new InvalidOperationException("Cannot globally set font width on variable-width font.");
                for (int i = 0; i < 256; i++)
                    Glyphs[i].LogicalWidth = value;
            }
        }

        public Char[] Glyphs;
        public Char this[int index]
        {
            get
            {
                return Glyphs[index];
            }
            set
            {
                Glyphs[index] = value;
            }
        }
        protected byte height;
        public byte Height
        {
            get
            {
                return height;
            }
            set
            {
                height = value;
                for (int i = 0; i < Glyphs.Length; i++)
                    Glyphs[i].Height = height;
            }
        }
        public byte SpaceAbove;
        public byte SpaceBelow;
        protected byte baseLine = 7;
        /// <summary>
        /// Sets the location of the baseline indicator.
        /// </summary>
        public byte BaseLine
        {
            get
            {
                return baseLine;
            }
            set
            {
                for (int i = 0; i < Glyphs.Length; i++)
                    Glyphs[i].BaseLine = value;
                baseLine = value;
            }
        }
        protected byte xHeight = 3;
        /// <summary>
        /// Sets the lcoation of the x-height indicator.
        /// </summary>
        public byte XHeight
        {
            get
            {
                return xHeight;
            }
            set
            {
                for (int i = 0; i < Glyphs.Length; i++)
                    Glyphs[i].XHeight = value;
                xHeight = value;
            }
        }
        protected byte capHeight = 0;
        /// <summary>
        /// Sets the location of the cap height indicator
        /// </summary>
        public byte CapHeight
        {
            get
            {
                return capHeight;
            }
            set
            {
                for (int i = 0; i < Glyphs.Length; i++)
                    Glyphs[i].CapHeight = value;
                capHeight = value;
            }
        }

        /// <summary>
        /// Accesses Chars in the font according to their codepoint
        /// </summary>
        /// <param name="c">Codepoint to search for</param>
        /// <returns>Char if found, null if not</returns>
        public Char this[char c]
        {
            get
            {
                for (int i = 0; i < MaximumCodePoints; i++)
                    if (Glyphs[i].Codepoint == c)
                        return Glyphs[i];
                return null;
            }
            set
            {
                for (int i = 0; i < MaximumCodePoints; i++)
                    if (Glyphs[i].Codepoint == value.Codepoint)
                        Glyphs[i] = value;
                throw new KeyNotFoundException("Cannot assign glyph to char not in font.");
            }
        }

        public const int MaximumCodePoints = 256;

        public Dictionary<string, string> AboutData;

        /// <summary>
        /// Wraps the font metadata.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string this[string key]
        {
            get
            {
                string t;
                AboutData.TryGetValue(key, out t);
                return t;
            }
            set
            {
                AboutData[key] = value;
            }
        }

        public Font()
        {
            FirstCodePoint = 0;
            LastCodePoint = 255;
            NewlineCodePoint = 10;
            TabCodePoint = (byte)'\t';
            SpaceCodePoint = 32;
            variableWidth = true;
            height = 8;
            widthMustBeMultipleOfEight = true;
            Glyphs = new Char[MaximumCodePoints];
            for (int i = 0; i < MaximumCodePoints; i++)
            {
                Glyphs[i] = new Char();
                if (i > 31 && i < 128)
                {
                    Glyphs[i].Codepoint = (char)i;
                    Glyphs[i].Name = Glyphs[i].Codepoint.ToString();
                }
            }
            AboutData = new Dictionary<string, string>();
            AboutData["Code Page"] = "ASCII";
        }

        private Font(int ignored)
        {
            // Do nothing.
        }

        public Font Clone()
        {
            Font font = new Font(0);
            font.FirstCodePoint = FirstCodePoint;
            font.LastCodePoint = LastCodePoint;
            font.NewlineCodePoint = NewlineCodePoint;
            font.TabCodePoint = TabCodePoint;
            font.SpaceCodePoint = SpaceCodePoint;
            font.variableWidth = variableWidth;
            font.height = height;
            font.widthMustBeMultipleOfEight = widthMustBeMultipleOfEight;
            font.Glyphs = new Char[MaximumCodePoints];
            for (int i = 0; i < MaximumCodePoints; i++)
                font.Glyphs[i] = Glyphs[i].Clone();
            font.AboutData = new Dictionary<string, string>();
            foreach (KeyValuePair<string, string> s in AboutData)
                font.AboutData.Add(s.Key, s.Value);
            return font;
        }

        protected static readonly char[] defaultHeader = new char[] {'M', 'F', 'E', ' ', 'F', 'o', 'n', 't',
            ' ', 'f', 'i', 'l',  'e', '.', (char)13, (char)10};

        public void WriteToFile(string path)
        {
            byte[] serialization = new byte[defaultHeader.Length + SerializedLength];
            int i = 0;
            for (i = 0; i < defaultHeader.Length; i++)
                serialization[i] = (byte)defaultHeader[i];
            SerializeTo(serialization, ref i);
            System.IO.File.WriteAllBytes(path, serialization);
        }

        public static Font ReadFromFile(string path)
        {
            byte[] data = System.IO.File.ReadAllBytes(path);
            if (data.Length < 256)
                throw new ArgumentException("File is far too small to be a valid font.");
            int i = 0;
            for (i = 0; i < defaultHeader.Length; i++)
                if (data[i] != (byte)defaultHeader[i])
                    throw new ArgumentException("File does not have a valid header.");
            return DeserializeFrom(data, ref i);
        }

        #region Serialization
        protected void WriteSerialized(byte[] destination, ref int i, string s)
        {
            destination[i++] = (byte)(s.Length & 0xFF);
            destination[i++] = (byte)(s.Length >> 8);
            for (int j = 0; j < s.Length; j++)
            {
                destination[i++] = (byte)((int)s[j] & 0xFF);
                destination[i++] = (byte)((int)s[j] >> 8);
            }
        }

        protected void WriteSerialized(byte[] destination, ref int i, char c)
        {
            destination[i++] = (byte)(c & 0xFF);
            destination[i++] = (byte)(c >> 8);
        }

        protected void WriteSerialized(byte[] destination, ref int i, int c)
        {
            destination[i++] = (byte)(c & 0xFF);
            destination[i++] = (byte)((c >> 8) & 0xFF);
            destination[i++] = (byte)((c >> 16) & 0xFF);
            destination[i++] = (byte)((c >> 24) & 0xFF);
        }

        /// <summary>
        /// Returns how many bytes the object needs to serialize.
        /// </summary>
        public int SerializedLength
        {
            get
            {
                // Master field, Version block, then BasicInformation, then WidthMustBeMultipleOfEightFlag, then MetricsInformation
                int l = 5 + 7 + 5 + 7 + 8 + 6 + 13;
                /* FirstCodePoint LastCodePoint NewlineCodePoint TabCodePoint SpaceCodePoint variableWidth height AspectRatioWidth AspectRatioHeight */
                l += 9;
                foreach (KeyValuePair<string, string> s in AboutData)
                    l += (s.Key.Length + s.Value.Length) * 2 + 4;
                l += 9;
                for (int i = 0; i < MaximumCodePoints; i++)
                    l += Glyphs[i].SerializedLength;
                return l;
            }
        }

        protected enum SerialzationHeaderIds : byte
        {
            MasterField = 1,
            Version = 2,
            BasicInformation = 16,
            AboutInformation = 17,
            WidthMustBeMultipleOfEightFlag = 18,
            MetricsInformation = 19,
            Glyphs = 32,
        }

        public void SerializeTo(byte[] destination, ref int i)
        {
            destination[i++] = (byte)SerialzationHeaderIds.MasterField;
            int g = i; 
            i += 4;
            destination[i++] = (byte)SerialzationHeaderIds.Version;
            WriteSerialized(destination, ref i, 2);
            destination[i++] = CurrentVersionNumber;
            destination[i++] = CurrentMinorVersionNumber;

            destination[i++] = (byte)SerialzationHeaderIds.BasicInformation;
            WriteSerialized(destination, ref i, 7 + 8);
            destination[i++] = (byte)FirstCodePoint;
            destination[i++] = (byte)LastCodePoint;
            destination[i++] = (byte)NewlineCodePoint;
            destination[i++] = (byte)TabCodePoint;
            destination[i++] = (byte)SpaceCodePoint;
            destination[i++] = (byte)(variableWidth ? 255 : 0);
            destination[i++] = height;
            WriteSerialized(destination, ref i, (int)(aspectRatioWidth * 65536));
            WriteSerialized(destination, ref i, (int)(aspectRatioHeight * 65536));

            destination[i++] = (byte)SerialzationHeaderIds.WidthMustBeMultipleOfEightFlag;
            WriteSerialized(destination, ref i, 1);
            destination[i++] = WidthMustBeMultipleOfEight ? (byte)1 : (byte)0;

            destination[i++] = (byte)SerialzationHeaderIds.MetricsInformation;
            WriteSerialized(destination, ref i, 8);
            destination[i++] = Style;
            destination[i++] = Weight;
            destination[i++] = SpaceAbove;
            destination[i++] = SpaceBelow;
            destination[i++] = BaseLine;
            destination[i++] = XHeight;
            destination[i++] = CapHeight;
            destination[i++] = ItalicSpaceAdjust;


            destination[i++] = (byte)SerialzationHeaderIds.AboutInformation;
            int h = i;
            i += 4;
            WriteSerialized(destination, ref i, AboutData.Count);
            foreach (KeyValuePair<string, string> s in AboutData)
            {
                WriteSerialized(destination, ref i, s.Key);
                WriteSerialized(destination, ref i, s.Value);
            }
            WriteSerialized(destination, ref h, i - h - 4);

            destination[i++] = (byte)SerialzationHeaderIds.Glyphs;
            h = i;
            i += 4;
            WriteSerialized(destination, ref i, MaximumCodePoints);
            for (int j = 0; j < MaximumCodePoints; j++)
                Glyphs[j].SerializeTo(destination, ref i);
            WriteSerialized(destination, ref h, i - h - 4);

            WriteSerialized(destination, ref g, i - g - 4);
        }

        protected static int DeserializeInt(byte[] source, ref int i)
        {
            int j = source[i++];
            j |= source[i++] << 8;
            j |= source[i++] << 16;
            j |= source[i++] << 24;
            return j;
        }

        protected static ushort DeserializeUshort(byte[] source, ref int i)
        {
            ushort j = source[i++];
            j = (ushort)(j | (source[i++] << 8));
            return j;
        }

        protected static char DeserializeChar(byte[] source, ref int i)
        {
            return (char)DeserializeUshort(source, ref i);
        }

        protected static string DeserializeString(byte[] source, ref int i)
        {
            int length = DeserializeUshort(source, ref i);
            StringBuilder str = new StringBuilder(length + 4);
            for (int j = 0; j < length; j++)
                str.Append(DeserializeChar(source, ref i));
            return str.ToString();
        }

        public static Font DeserializeFrom(byte[] source, ref int i)
        {
            if (source[i++] != (int)SerialzationHeaderIds.MasterField)
                throw new ArgumentException("Invalid font.");
            i += 4;
            int p = -1;
            int temp;
            if (source[i++] != (int)SerialzationHeaderIds.Version)
                throw new ArgumentException("Invalid font.");
            temp = DeserializeInt(source, ref i);
            if (temp != 2)
                throw new ArgumentException("Invalid font.");
            if (source[i++] != CurrentVersionNumber)
                throw new ArgumentException("Unable to read new font version.");
            i++;
            Font font = new Font(0);
            while (i < source.Length && p != i)
            {
                p = i;
                switch ((SerialzationHeaderIds)source[i++])
                {
                    case SerialzationHeaderIds.AboutInformation:
                        i += 4; // Ignore size
                        temp = DeserializeInt(source, ref i);
                        font.AboutData = new Dictionary<string, string>(temp + 4);
                        for (int j = 0; j < temp; j++)
                            font.AboutData.Add(DeserializeString(source, ref i), DeserializeString(source, ref i));
                        break;
                    case SerialzationHeaderIds.BasicInformation:
                        if (DeserializeInt(source, ref i) != 15)
                            throw new ArgumentException("Invalid BasicInformation");
                        font.FirstCodePoint = source[i++];
                        font.LastCodePoint = source[i++];
                        font.NewlineCodePoint = source[i++];
                        font.TabCodePoint = source[i++];
                        font.SpaceCodePoint = source[i++];
                        font.variableWidth = source[i++] != 0 ? true : false;
                        font.height = source[i++];
                        font.aspectRatioWidth = (float)DeserializeInt(source, ref i) / 65536;
                        font.aspectRatioHeight = (float)DeserializeInt(source, ref i) / 65536;
                        break;
                    case SerialzationHeaderIds.WidthMustBeMultipleOfEightFlag:
                        if ((temp = DeserializeInt(source, ref i)) != 1)
                            throw new ArgumentException("Invalid WidthMustBeMultipleOfEightFlag (" + temp.ToString() + ")");
                        //i += 4;
                        temp = source[i++];
                        font.widthMustBeMultipleOfEight = (temp & 1) != 0;
                        break;
                    case SerialzationHeaderIds.Glyphs:
                        i += 4; // Ignore size
                        if (DeserializeInt(source, ref i) != MaximumCodePoints)
                            throw new ArgumentOutOfRangeException("MaximumCodePoints in font does not match internally expected value.");
                        font.Glyphs = new Char[MaximumCodePoints];
                        for (int j = 0; j < MaximumCodePoints; j++)
                        {
                            font.Glyphs[j] = Char.DeserializeFrom(source, ref i);
                            font.Glyphs[j].BaseLine = font.baseLine;
                            font.Glyphs[j].XHeight = font.xHeight;
                            font.Glyphs[j].CapHeight = font.CapHeight;
                        }
                        break;
                    case SerialzationHeaderIds.MetricsInformation:
                        int metricsLength = DeserializeInt(source, ref i);
                        if (metricsLength != 7 && metricsLength != 8)
                            throw new ArgumentException("Invalid MetricsInformation");
                        font.Style = source[i++];
                        font.Weight = source[i++];
                        font.SpaceAbove = source[i++];
                        font.SpaceBelow = source[i++];
                        font.baseLine = source[i++];
                        font.xHeight = source[i++];
                        font.capHeight = source[i++];
                        if (metricsLength == 8)
                            font.ItalicSpaceAdjust = source[i++];
                        break;
                    default:
                        throw new ArgumentException("Invalid font.");
                }
            }
            return font;
        }
        #endregion
    }
}
