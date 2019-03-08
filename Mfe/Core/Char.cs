using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace Mfe
{
    [Serializable]
    public class Char : ISerializable
    {
        public float AspectRatioWidth = 1;
        public float AspectRatioHeight = 1;
        protected byte logicalWidth;
        public byte LogicalWidth
        {
            get
            {
                return logicalWidth;
            }
            set
            {
                logicalWidth = value;
            }
        }

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
                capHeight = value;
            }
        }


        protected void enlargeTo(byte newHeight, byte newWidth)
        {
            byte[,] b = new byte[newHeight, newWidth];
            for (int y = 0; y < maxHeight; y++)
                for (int x = 0; x < maxWidth; x++)
                    b[y, x] = bitmap[y, x];
            maxHeight = newHeight;
            maxWidth = newWidth;
        }

        protected float enlargeFactor = 1.2f;

        protected byte width;
        protected byte maxWidth;
        public byte Width
        {
            get
            {
                return width;
            }
            set
            {
                if (value > maxWidth)
                    enlargeTo(maxHeight, (byte)(value * enlargeFactor));
                width = value;
            }
        }

        protected byte height;
        protected byte maxHeight;
        public byte Height
        {
            get
            {
                return height;
            }
            set
            {
                if (value > maxHeight)
                    enlargeTo((byte)(value * enlargeFactor), maxWidth);
                height = value;
            }
        }

        public char Codepoint;

        public string Name;

        protected byte[,] bitmap;

        public bool this[int row, int col]
        {
            get
            {
                return bitmap[row, col] != 0;
            }
            set
            {
                if (value)
                    bitmap[row, col] = 0xFF;
                else
                    bitmap[row, col] = 0;
            }
        }

        public Char()
        {
            Name = "";
            Codepoint = '�';
            maxWidth = 64;
            maxHeight = 64;
            bitmap = new byte[maxHeight, maxWidth];
            Width = 8;
            Height = 8;
            LogicalWidth = 6;
        }

        private Char(int ignored)
        {
            // Do nothing.
        }

        /// <summary>
        /// Produces a deep copy of this char.
        /// (Well, the Name string isn't deep copied, which is OK because strings are immutable.)
        /// </summary>
        /// <returns></returns>
        public Char Clone()
        {
            Char newChar = new Char(0);
            newChar.bitmap = new byte[maxHeight, maxWidth];
            for (int y = 0; y < maxHeight; y++)
                for (int x = 0; x < maxWidth; x++)
                    newChar.bitmap[y, x] = bitmap[y, x];
            newChar.Name = Name;
            newChar.Codepoint = Codepoint;
            newChar.width = width;
            newChar.maxWidth = maxWidth;
            newChar.height = height;
            newChar.maxHeight = maxHeight;
            newChar.logicalWidth = logicalWidth;
            newChar.AspectRatioWidth = AspectRatioWidth;
            newChar.AspectRatioHeight = AspectRatioHeight;
            return newChar;
        }

        /// <summary>
        /// Shifts the bitmap around.
        /// </summary>
        public void ShiftUp(bool clearBottom = false)
        {
            if (height <= 1)
                return;
            for (int i = 1; i < height; i++)
                for (int j = 0; j < width; j++)
                    this[i - 1, j] = this[i, j];
            if (clearBottom)
                for (int i = 0; i < width; i++)
                    this[height - 1, i] = false;
        }

        /// <summary>
        /// Shifts the bitmap around.
        /// </summary>
        public void ShiftDown(bool clearTop = false)
        {
            if (height <= 1)
                return;
            int i = width;
            while (i --> 0) // "while i goes toward zero"
            {
                int j = height;
                while (0 <-- j) // "while zero is approached by j"
                    this[j, i] = this[j - 1, i];
            }
            if (clearTop)
                for (i = 0; i < width; i++)
                    this[0, i] = false;
        }

        /// <summary>
        /// Shifts the bitmap around.
        /// </summary>
        public void ShiftLeft(bool clearRight = false)
        {
            if (width <= 1)
                return;
            for (int i = 1; i < width; i++)
                for (int j = 0; j < height; j++)
                    this[j, i - 1] = this[j, i];
            if (clearRight)
                for (int i = 0; i < height; i++)
                    this[i, width - 1] = false;
        }

        /// <summary>
        /// Shifts the bitmap around.
        /// </summary>
        public void ShiftRight(bool clearLeft = false)
        {
            if (width <= 1)
                return;
            int i = width;
            while (0 <-- i)
            {
                int j = height;
                while (j --> 0)
                    this[j, i] = this[j, i - 1];
            }
            if (clearLeft)
                for (i = 0; i < height; i++)
                    this[i, 0] = false;
        }

        /// <summary>
        /// Resets all pixels.
        /// </summary>
        public void Clear()
        {
            for (int i = 0; i < height; i++)
                for (int j = 0; j < width; j++)
                    this[i, j] = false;
        }

        /// <summary>
        /// Inverts all pixels.
        /// </summary>
        public void Invert()
        {
            for (int i = 0; i < height; i++)
                for (int j = 0; j < width; j++)
                    this[i, j] = !this[i, j];
        }

        public void PaintEditor(int xBase, int yBase, float xScale, float yScale, Brush foreBrush, Brush backBrush, Pen gridPen, Pen widthLinePen, Graphics g)
        {
            if (gridPen != null)
                g.DrawLine(gridPen, xBase, yBase - gridPen.Width, xBase + xScale * width - 1, yBase - gridPen.Width);
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    if (bitmap[y, x] != 0)
                    {
                        if (foreBrush != null)
                            g.FillRectangle(foreBrush, xBase + xScale * x, yBase + yScale * y, xScale, yScale);
                    }
                    else
                    {
                        if (backBrush != null)
                            g.FillRectangle(backBrush, xBase + xScale * x, yBase + yScale * y, xScale, yScale);
                    }
                }
                if (gridPen != null)
                    //g.DrawLine(gridPen, xBase, yBase + yScale * y + yScale - gridPen.Width, xBase + xScale * width - 1, yBase + yScale * y + yScale - gridPen.Width);
                    g.DrawLine(gridPen, xBase, yBase + yScale * y + yScale - 1, xBase + xScale * width - 1, yBase + yScale * y + yScale - 1);
                    
            }
            if (gridPen != null)
                for (int x = -1; x < Width; x++)
                {
                    //g.DrawLine(gridPen, xBase + x * xScale + xScale - gridPen.Width, yBase, xBase + x * xScale + xScale - gridPen.Width, yBase + yScale * height - 1);
                    g.DrawLine(gridPen, xBase + x * xScale + xScale, yBase, xBase + x * xScale + xScale, yBase + yScale * height - 1);
                }
            if (widthLinePen != null)
                //g.DrawLine(widthLinePen, xBase + logicalWidth * xScale - gridPen.Width, yBase, xBase + logicalWidth * xScale - gridPen.Width, yBase + yScale * height - 1);
                g.DrawLine(widthLinePen, xBase + logicalWidth * xScale, yBase, xBase + logicalWidth * xScale, yBase + yScale * height - 1);
            //g.DrawRectangle(forePen, 1, 1, 100, 100);
            
        }

        public void PaintVirtual(int xBase, int yBase, int xLimit, int yLimit, float xScale, float yScale, Brush foreBrush, Brush backBrush, Graphics g)
        {
            if (xBase >= xLimit || yBase >= yLimit)
            {
                throw new Exception();//return;
            }

            int maxx = this.Width;
            int maxy = this.Height;

            if (xLimit < xBase + this.LogicalWidth)
                maxx = xLimit - xBase;
            if (yLimit < yBase + this.Height)
                maxy = yLimit - yBase;

            xBase *= (int)xScale;
            yBase *= (int)yScale;

            if (backBrush != null)
                g.FillRectangle(backBrush, xBase, yBase, xScale * maxx, yScale * maxy);

            if (foreBrush != null)
                for (int y = 0; y < maxy; y++)
                {
                    for (int x = 0; x < maxx; x++)
                    {
                        if (bitmap[y, x] != 0)
                        {
                            g.FillRectangle(foreBrush, xBase + xScale * x, yBase + yScale * y, xScale, yScale);
                        }
                    }
                }
        }

        public void Paint(int xBase, int yBase, float xScale, float yScale, Brush foreBrush, Brush backBrush, Graphics g)
        {
            if (backBrush != null)
                g.FillRectangle(backBrush, xBase, yBase, xScale * width, yScale * height);

            if (foreBrush != null)
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        if (bitmap[y, x] != 0)
                        {
                            g.FillRectangle(foreBrush, xBase + xScale * x, yBase + yScale * y, xScale, yScale);
                        }
                    }
                }
        }

        public string ToDB()
        {
            StringBuilder s = new StringBuilder();
            s.Append("\t; Char ");
            s.Append(this.Name);
            s.AppendLine();
            s.Append("\t.db\t");
            s.Append(this.height.ToString());
            s.Append(", ");
            s.Append(this.width.ToString());
            s.AppendLine("\t; height, width");
            for (int y = 0; y < this.height; y++)
            {
                s.Append("\t.db\t");
                int bit = 0;
                for (int x = 0; x < this.width; x++)
                {
                    s.Append(this[y, x] ? "1" : "0");
                    if (bit++ >= 7)
                    {
                        bit = 0;
                        s.Append("b");
                        if (x != this.Width - 1)
                            s.Append(", ");
                    }
                }
                if (bit != 0)
                {
                    for (; bit < 8; bit++)
                        s.Append("0");
                    s.AppendLine("b");
                }
                else
                    s.AppendLine();
            }
            return s.ToString();
        }

        public Bitmap ToBitmap()
        {
            Bitmap img = new Bitmap(width, height);
            for (int y = 0; y < height; y++)
                for (int x = 0; x < width; x++)
                    img.SetPixel(x, y, this[y, x] ? Color.Black : Color.White);
            return img;
        }


        /// <summary>
        /// Returns how many bytes the object needs to serialize.
        /// </summary>
        public int SerializedLength
        {
            get
            {
                return 2 + Name.Length * 2 + 2 + 1 + 1 + 1 + width * height;
            }
        }

        /* struct Char
         * {
         *      ushort NameLength;
         *      char Codepoint;
         *      byte VirtualWidth;
         *      byte Height;
         *      byte Width;
         *      byte[][] Data;
         *  }
         */

        /// <summary>
        /// Writes the object data to the given byte array.
        /// </summary>
        /// <param name="destination">Byte array to write to</param>
        /// <param name="start">Location to start writing at</param>
        /// <returns>The number of bytes written</returns>
        public void SerializeTo(byte[] destination, ref int i)
        {
            destination[i++] = (byte)(Name.Length & 0xFF);
            destination[i++] = (byte)(Name.Length >> 8);
            for (int j = 0; j < Name.Length; j++)
            {
                destination[i++] = (byte)((int)Name[j] & 0xFF);
                destination[i++] = (byte)((int)Name[j] >> 8);
            }
            destination[i++] = (byte)((int)Codepoint & 0xFF);
            destination[i++] = (byte)((int)Codepoint >> 8);
            destination[i++] = LogicalWidth;
            destination[i++] = Height;
            destination[i++] = Width;
            for (int y = 0; y < Height; y++)
                for (int x = 0; x < Width; x++)
                    destination[i++] = bitmap[y, x];
        }

        /// <summary>
        /// Creates a new Char from the data in the given byte array.
        /// </summary>
        /// <param name="source">Source data</param>
        /// <param name="i">Starting location, on return points to byte after last byte used by serialized Char</param>
        /// <returns>A new Char object</returns>
        public static Char DeserializeFrom(byte[] source, ref int i)
        {
            Char newChar = new Char();
            int temp;
            StringBuilder str = new StringBuilder();
            temp = source[i++] | (source[i++] << 8);
            for (int j = temp; j > 0; j--)
                str.Append((char)(source[i++] | (source[i++] << 8)));
            newChar.Name = str.ToString();
            newChar.Codepoint = (char)(source[i++] | (source[i++] << 8));
            newChar.LogicalWidth = source[i++];
            newChar.Height = source[i++];
            newChar.Width = source[i++];
            for (int y = 0; y < newChar.Height; y++)
                for (int x = 0; x < newChar.Width; x++)
                    newChar.bitmap[y, x] = source[i++];
            return newChar;
        }



        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            // Use the AddValue method to specify serialized values.
            info.AddValue("AspectRatioWidth", AspectRatioWidth, typeof(float));
            info.AddValue("AspectRatioHeight", AspectRatioHeight, typeof(float));
            info.AddValue("logicalWidth", logicalWidth, typeof(byte));
            info.AddValue("baseLine", baseLine, typeof(byte));
            info.AddValue("xHeight", xHeight, typeof(byte));
            info.AddValue("capHeight", capHeight, typeof(byte));
            info.AddValue("width", width, typeof(byte));
            //info.AddValue("", , typeof(byte));
            info.AddValue("height", height, typeof(byte));
            info.AddValue("Codepoint", Codepoint, typeof(char));
            info.AddValue("Name", Name, typeof(string));
            info.AddValue("bitmap", bitmap, typeof(byte[,]));

        }

        protected Char(SerializationInfo info, StreamingContext context)
        {
            AspectRatioWidth = (float)info.GetValue("AspectRatioWidth", typeof(float));
            AspectRatioHeight = (float)info.GetValue("AspectRatioHeight", typeof(float));
            logicalWidth = (byte)info.GetValue("logicalWidth", typeof(byte));
            baseLine = (byte)info.GetValue("baseLine", typeof(byte));
            xHeight = (byte)info.GetValue("xHeight", typeof(byte));
            capHeight = (byte)info.GetValue("capHeight", typeof(byte));
            width = (byte)info.GetValue("width", typeof(byte));
            // = ()info.GetValue("", typeof());
            height = (byte)info.GetValue("height", typeof(byte));
            // = ()info.GetValue("", typeof());
            Codepoint = (char)info.GetValue("Codepoint", typeof(char));
            Name = (string)info.GetValue("Name", typeof(string));
            bitmap = (byte[,])info.GetValue("bitmap", typeof(byte[,]));

        }
    }
}
