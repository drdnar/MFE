using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mfe.Exports
{
    public class FntExporter
    {
        [FontExporter(FileTypeExtension = "fnt", FileTypeDescription = "FNT resource", HelpText = "Exports to a Windows FNT resource (2.0)")]
        public static void ExportData(Mfe.Font font, string path)
        {
            List<byte> bytes = new List<byte>();

            // Version
            appendWord(bytes, 0x0200);
            // Location
            int dfSizeLocation = bytes.Count;
            appendDWord(bytes, 0);
            // Copyright
            for (int i = 0; i < 60; i++)
                appendByte(bytes, 0);
            // dfType
            appendWord(bytes, 0);
            // dfPoints
            appendWord(bytes, (short)(font.BaseLine - font.CapHeight));
            // dfVertRes, dfHorizRes
            appendWord(bytes, 96);
            appendWord(bytes, 96);
            // dfAscent
            appendWord(bytes, font.BaseLine);
            // dfInternalLeading
            appendWord(bytes, 0);
            // dfExternalLeading
            appendWord(bytes, 0);
            // dfItalic
            appendByte(bytes, font.StyleObliqueness != Font.Obliqueness.Upright ? (byte)1 : (byte)0);
            // dfUnderline
            appendByte(bytes, 0);
            // dfStrikeOut
            appendByte(bytes, 0);
            // dfWeight
            appendWord(bytes, 0);
            // dfCharSet
            appendByte(bytes, 0);
            // dfPixWidth
            appendWord(bytes, 0);
            // dfPixHeight
            appendWord(bytes, font.Height);
            // dfPitchAndFamily
            appendByte(bytes, 0);
            // dfAvgWidth
            appendWord(bytes, font.Width);
            // dfMaxWidth
            appendWord(bytes, font.Glyphs.Max(x => x.Width));
            // dfFirstChar
            appendByte(bytes, (byte)font.FirstCodePoint);
            // dfLastChar
            appendByte(bytes, (byte)font.LastCodePoint);
            // dfDefaultChar
            appendByte(bytes, font.FirstCodePoint <= (byte)' ' && (byte)' ' <= font.LastCodePoint ? (byte)' ' : (byte)font.FirstCodePoint);
            // dfBreakChar
            appendByte(bytes, 0);
            // dfWidthBytes, I think this is the right way to compute this. . . .
            appendWord(bytes, (short)(((font.Glyphs.Sum(x => x.Width) - 1) >> 3) + 1));
            // dfDevice
            appendDWord(bytes, 0);
            // dfFace
            int dfFaceLocation = bytes.Count;
            appendDWord(bytes, 0);
            // dfBitsPointer
            appendDWord(bytes, 0);
            // dfBitsOffset
            appendDWord(bytes, 0);
            // dfReserved
            appendByte(bytes, 0);
            // Serialize widths table
            ushort[] bitmapLocations = new ushort[font.LastCodePoint - font.FirstCodePoint + 1];
            for (int i = font.FirstCodePoint; i <= font.LastCodePoint; i++)
            {
                appendWord(bytes, font[i].Width);
                bitmapLocations[i] = (ushort)bytes.Count;
                appendWord(bytes, 0);
            }
            // Serialize bitmaps
            for (int i = font.FirstCodePoint; i <= font.LastCodePoint; i++)
            {
                Char ch = font[i];
                writeWord(bytes, bitmapLocations[i], (short)bytes.Count);
                int byteColumns = ((ch.Width - 1) >> 3) + 1;
                if (byteColumns > 0)
                    for (int col = 0; col < byteColumns; col++)
                        for (int y = 0; y < ch.Height; y++)
                        {
                            int data = 0;
                            for (int b = 0; b <= (col == byteColumns - 1 ? ((ch.Width - 1) & 7) + 1: 7); b++)
                                data |= (ch[y, col * 8 + b] ? 1 : 0) << (7 - b);
                            appendByte(bytes, (byte)data);
                        }
            }
            // Serialize name
            writeDWord(bytes, dfFaceLocation, bytes.Count);
            string name = font["Name"] ?? "MFE Font";
            for (int i = 0; i < name.Length; i++)
                appendByte(bytes, (byte)name[i]);
            appendByte(bytes, 0);
            writeDWord(bytes, dfSizeLocation, bytes.Count);
            System.IO.File.WriteAllBytes(path, bytes.ToArray());
        }

        static void appendByte(List<byte> bytes, byte b)
        {
            bytes.Add(b);
        }

        static void writeByte(List<byte> bytes, int location, byte b)
        {
            bytes[location] = b;
        }
        
        static void appendWord(List<byte> bytes, short s)
        {
            bytes.Add((byte)(s & 0xFF));
            bytes.Add((byte)(s >> 8));
        }

        static void writeWord(List<byte> bytes, int location, short s)
        {
            bytes[location++] = (byte)(s & 0xFF);
            bytes[location] = (byte)(s >> 8);
        }

        static void appendDWord(List<byte> bytes, int i)
        {
            bytes.Add((byte)(i & 0xFF));
            bytes.Add((byte)((i >> 8) & 0xFF));
            bytes.Add((byte)((i >> 16) & 0xFF));
            bytes.Add((byte)(i >> 24));
        }

        static void writeDWord(List<byte> bytes, int location, int i)
        {
            bytes[location++] = (byte)(i & 0xFF);
            bytes[location++] = (byte)((i >> 8) & 0xFF);
            bytes[location++] = (byte)((i >> 16) & 0xFF);
            bytes[location++] = (byte)(i >> 24);
        }
    }
}
