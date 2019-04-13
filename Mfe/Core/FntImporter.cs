using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WORD = System.Int16;
using DWORD = System.Int32;
using CHAR = System.Byte;
using BYTE = System.Byte;

namespace Mfe
{
    class FntImporter
    {
        byte[] data;
        int ptr;
        
        WORD dfVersion;
        DWORD dfSize;
        string dfCopyright;
        int dfCopyrightSize = 60;
        WORD dfType;
        bool isRaster;
        WORD dfPoints;
        WORD dfVertRes;
        WORD dfHorizRes;
        WORD dfAscent;
        WORD dfInternalLeading;
        WORD dfExternalLeading;
        BYTE dfItalic;
        BYTE dfUnderline;
        BYTE dfStrikeOut;
        WORD dfWeight;
        BYTE dfCharSet;
        WORD dfPixWidth;
        WORD dfPixHeight;
        BYTE dfPitchAndFamily;
        WORD dfAvgWidth;
        WORD dfMaxWidth;
        BYTE dfFirstChar;
        BYTE dfLastChar;
        BYTE dfDefaultChar;
        BYTE dfBreakChar;
        WORD dfWidthBytes;
        DWORD dfDevice;
        DWORD dfFace;
        //DWORD  dfReserved; 
        // more stuff
        DWORD dfBitsPointer;
        DWORD dfBitsOffset;
        BYTE dfReserved;
        DWORD dfFlags;
        WORD dfAspace;
        WORD dfBspace;
        WORD dfCspace;
        WORD dfColorPointer;
        DWORD dfReserved1;
        DWORD[] dfCharTableOffsets;
        WORD[] dfCharTableWidths;
        //  char   szDeviceName[]; 
        //  char   szFaceName[]; 
        int totalGlyphs;
        string faceName;

        Font font = new Font();

        private FntImporter(byte[] data)
        {
            this.data = data;
            ptr = 0;
            dfVersion = readWord();
            //if (dfVersion == 0x200)
            //    dfSize = readWord();
            //else if (dfVersion == 0x300)
                dfSize = readDWord();
            //else
            //    throw new ArgumentException("Not a valid FNT resource.");
            StringBuilder sb = new StringBuilder(dfCopyrightSize);
            for (int i = 0; i < dfCopyrightSize; i++)
            {
                int x = readByte();
                if (x != 0)
                    sb.Append((char)x);
            }
            dfCopyright = sb.ToString();
            dfType = readWord();
            isRaster = (dfType & 1) == 0;
            dfPoints = readWord();
            dfVertRes = readWord();
            dfHorizRes = readWord();
            dfAscent = readWord();
            dfInternalLeading = readWord();
            dfExternalLeading = readWord();
            dfItalic = readByte();
            dfUnderline = readByte();
            dfStrikeOut = readByte();
            dfWeight = readWord();
            dfCharSet = readByte();
            dfPixWidth = readWord();
            dfPixHeight = readWord();
            font.Height = (byte)dfPixHeight;
            dfPitchAndFamily = readByte();
            dfAvgWidth = readWord();
            dfMaxWidth = readWord();
            dfFirstChar = readByte();
            dfLastChar = readByte();
            dfDefaultChar = readByte();
            dfBreakChar = readByte();
            dfWidthBytes = readWord();
            dfDevice = readDWord();
            dfFace = readDWord();
            // Should be zero
            dfBitsPointer = readDWord();
            dfBitsOffset = readDWord();
            dfReserved = readByte();
            if (dfVersion == 0x300)
            {
                dfFlags = readDWord();
                if ((dfFlags & 0xC) != 0)
                    throw new ArgumentException("DFF_ABC* is not supported.");
                if ((dfFlags & 0xF0) != 0x10)
                    throw new ArgumentException("DFF_1COLOR is required. Seriously, I'm not writing a full grayscale rendering library.");
                dfAspace = readWord();
                dfBspace = readWord();
                dfCspace = readWord();
                dfColorPointer = readWord();
                dfReserved1 = readDWord();
                // I have no idea what these are about.
                readWord();
                readWord(); readDWord();
                readWord(); readDWord();
            }

            

            totalGlyphs = dfLastChar - dfFirstChar + 1;
            dfCharTableOffsets = new DWORD[totalGlyphs];
            dfCharTableWidths = new WORD[totalGlyphs];
            for (int i = 0; i < totalGlyphs; i++)
            {
                dfCharTableWidths[i] = readWord();
                if (dfVersion == 0x200)
                    dfCharTableOffsets[i] = readWord();
                else if (dfVersion == 0x300)
                    dfCharTableOffsets[i] = readDWord();
                else
                    throw new NotImplementedException("Missing else in FNT parse.");
            }

            // Collect name
            ptr = dfFace;
            sb.Clear();
            for (byte x = readByte(); x != 0; x = readByte())
            {
                if (x != 0)
                    sb.Append((char)x);
            }
            faceName = sb.ToString();

            // Digest bitmaps
            for (int i = 0; i < totalGlyphs; i++)
            {
                font[dfFirstChar + i] = digestBitmap(dfCharTableOffsets[i], dfCharTableWidths[i]);
            }
        }

        private Char digestBitmap(int offset, int width)
        {
            Char ch = new Mfe.Char();
            ch.Height = (byte)dfPixHeight;
            ch.LogicalWidth = ch.Width = (byte)width;
            ptr = offset;
            int columns = ((width - 1) >> 3) + 1;
            for (int c = 0; c < columns; c++) /* Not a secret message */
            {
                for (int y = 0; y < dfPixHeight; y++)
                {
                    int line = readByte();
                    for (int x = 0; x < 8; x++, line = line << 1)
                        ch[y, c * 8 + x] = (line & 0x80) != 0;
                }
            }
            return ch;
        }

        private byte readByte()
        {
            return data[ptr++];
        }

        private short readWord()
        {
            return (short)(data[ptr++] | (data[ptr++] << 8));
        }

        private int readDWord()
        {
            return (data[ptr++]) | (data[ptr++] << 8) | (data[ptr++] << 16) | (data[ptr++] << 24);
        }

        public static Font Import(byte[] data)
        {
            return (new FntImporter(data)).font;
        }
    }
}
