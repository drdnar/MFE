using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mfe.Exports
{
    public class Program
    {
        protected VariableType type;
        /// <summary>
        /// Returns the VAT entry type of the variable
        /// </summary>
        public virtual VariableType Type
        {
            get
            {
                return type;
            }
            set
            {
                if (!(value == VariableType.ProtProgObj || value == VariableType.ProgObj || value == VariableType.AppVarObj))
                    throw new ArgumentException("Unsupported type.");
            }
        }


        protected byte version = 0;
        /// <summary>
        /// The version of the file, used to prevent sending new files to old OSes
        /// </summary>
        public virtual byte Version
        {
            get
            {
                return version;
            }
            set
            {
                version = value;
            }
        }


        /// <summary>
        /// Holds the raw variable data
        /// </summary>
        public byte[] Data = new byte[] { };
        /// <summary>
        /// Mutates the raw data in the object
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public byte this[int i]
        {
            get
            {
                return Data[i];
            }
            set
            {
                Data[i] = value;
            }
        }

        /// <summary>
        /// Holds the size of the raw data in the variable.
        /// </summary>
        public int Size
        {
            get
            {
                return Data.Length;
            }
            set
            {
                int len = Data.Length < value ? Data.Length : value;
                byte[] newdata = new byte[value];
                for (int i = 0; i < len; i++)
                    newdata[i] = Data[i];
                Data = newdata;
            }
        }


        /// <summary>
        /// Holds the internally used variable name
        /// </summary>
        protected byte[] name = new byte[8] { (byte)'A', 0, 0, 0, 0, 0, 0, 0 };
        protected int nameLength = 1;
        /// <summary>
        /// Gets or sets the name of the variable as a user-friendly string. 
        /// May not be valid for some variables
        /// </summary>
        /// <returns></returns>
        /// <summary>
        /// Controls the program's name
        /// </summary>
        public string Name
        {
            get
            {
                string ret = "";
                for (int i = 0; i < nameLength; i++)
                    ret += (char)name[i];
                return ret;
            }
            set
            {
                //if (value[0] == 'p' && value[0] == 'r' && value[0] == 'g' && value[0] == 'm')
                //    value.Remove(0, 4);
                if (value.Length > 8)
                    throw new ArgumentException("Name must be less than 8 characters", "value");
                name = new byte[8];
                for (int nameLength = 0; nameLength < value.Length; nameLength++)
                    name[nameLength] = (byte)value[nameLength];
            }
        }

        /// <summary>
        /// Exports the variable to a file
        /// </summary>
        /// <param name="path"></param>
        public virtual void SaveToFile(string path, bool archived = true)
        {
            List<byte> fileData = new List<byte>(FormHeaderForExport("Exported from ROM", (byte)type, name, version, archived));
            fileData.Add((byte)(Data.Length & 0xFF)); // size for raw item
            fileData.Add((byte)(Data.Length >> 8));
            fileData.AddRange(Data);
            fileData.Add(0); // for checksum
            fileData.Add(0);
            byte[] outFileData = fileData.ToArray();
            FinalizeVariableForExport(ref outFileData);
            System.IO.File.WriteAllBytes(path, outFileData);
        }

        /// <summary>
        /// Returns a bunch of bytes suitable to be written as a fully-formed 8xp/8xv.
        /// </summary>
        /// <param name="archived"></param>
        /// <returns></returns>
        public virtual byte[] Export(bool archived = true)
        {
            List<byte> fileData = new List<byte>(FormHeaderForExport("Exported from ROM", (byte)type, name, version, archived));
            fileData.Add((byte)(Data.Length & 0xFF)); // size for raw item
            fileData.Add((byte)(Data.Length >> 8));
            fileData.AddRange(Data);
            fileData.Add(0); // for checksum
            fileData.Add(0);
            byte[] outFileData = fileData.ToArray();
            FinalizeVariableForExport(ref outFileData);
            return outFileData;
        }


        /// <summary>
        /// Returns an array of bytes that form the header before that actual
        /// variable data
        /// </summary>
        /// <param name="notes">The file comments</param>
        /// <param name="varType">The VAT entry type of the variable</param>
        /// <param name="varName">The name of the variable</param>
        /// <param name="archived">true if the variable should be archived</param>
        /// <returns></returns>
        virtual public byte[] FormHeaderForExport(string notes, byte varType, byte[] varName, byte varVersion, bool archived)
        {
            List<byte> header = new List<byte>(new byte[] {
                (byte)'*', (byte)'*', (byte)'T', (byte)'I',
                (byte)'8', (byte)'3', (byte)'F', (byte)'*',
                26, 10, 0});
            for (int i = 0; i < 42; i++)
                if (i < notes.Length)
                    header.Add((byte)notes[i]);
                else
                    header.Add(32);
            header.Add(0); // Space for data section length
            header.Add(0);
            header.Add(0xD); // use version and archived fields
            header.Add(0); // do-nothing
            header.Add(0); // Variable length
            header.Add(0);
            header.Add((byte)varType);
            for (int i = 0; i < 8; i++)
                if (i < varName.Length)
                    header.Add(varName[i]);
                else
                    header.Add(0);
            header.Add(varVersion); // version
            if (archived)
                header.Add(0x80);
            else
                header.Add(0);
            header.Add(0); // Variable length
            header.Add(0);

            return header.ToArray();
        }


        /// <summary>
        /// Adds checksum and updates length fields. Remember to add two bytes to the
        /// end for the checksum.
        /// </summary>
        /// <param name="rawData"></param>
        static public void FinalizeVariableForExport(ref byte[] rawData)
        {
            // Update length fields
            int dataSectionLength = rawData.Length - 57;
            rawData[53] = (byte)(dataSectionLength & 0xFF);
            rawData[54] = (byte)(dataSectionLength >> 8);
            int rawDataLength = rawData.Length - 74;
            rawData[57] = (byte)(rawDataLength & 0xFF);
            rawData[58] = (byte)(rawDataLength >> 8);
            rawData[70] = rawData[57];
            rawData[71] = rawData[58];
            int checksum = 0;
            for (int i = 55; i < rawData.Length - 2; i++)
                checksum = (checksum + rawData[i]) & 0xFFFF;
            rawData[rawData.Length - 2] = (byte)(checksum & 0xFF);
            rawData[rawData.Length - 1] = (byte)(checksum >> 8);
        }


    }

    public enum VariableType
    {
        RealObj = 0,
        ListObj = 1,
        MatObj = 2,
        EquObj = 3,
        StrngObj = 4,
        ProgObj = 5,
        ProtProgObj = 6,
        PictObj = 7,
        GDBObj = 8,
        /// <summary>
        /// What is this?
        /// </summary>
        UnknownObj = 9,
        /// <summary>
        /// What is this?
        /// </summary>
        UnknownEquObj = 0x0A,
        /// <summary>
        /// Supposedly used in parsing
        /// </summary>
        NewEquObj = 0x0B,
        CplxObj = 0x0C,
        CListObj = 0x0D,
        /// <summary>
        /// What is this?
        /// </summary>
        UndefObj = 0x0E,
        WindowObj = 0x0F,
        ZStoObj = 0x10,
        TblRngObj = 0x11,
        /// <summary>
        /// What is this?
        /// </summary>
        LCDObj = 0x12,
        /// <summary>
        /// Should not appear in archive.
        /// </summary>
        BackupObj = 0x13,
        /// <summary>
        /// Used only in linking.  Not valid in the archive.
        /// </summary>
        AppObj = 0x14,
        /// <summary>
        /// Much like a program object, but doesn't appear in the PRGM menu.
        /// Used exclusively for data storage.
        /// </summary>
        AppVarObj = 0x15,
        /// <summary>
        /// Should not ever be archived.  Should always be deleted 
        /// upon return of control to the system monitor.
        /// </summary>
        TempProgObj = 0x16,
        /// <summary>
        /// Should never be unarchived.
        /// </summary>
        GroupObj = 0x17,
    };
}
