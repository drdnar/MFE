using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Mfe
{
    public partial class RawBitmapTableImporter : Form
    {
        public RawBitmapTableImporter()
        {
            InitializeComponent();
        }

        private void browseButton_Click(object sender, EventArgs e)
        {
            if (browseFileDialog.ShowDialog() == DialogResult.OK)
                filePathTextBox.Text = browseFileDialog.FileName;
        }

        public Font ImportedFont
        {
            get;
            set;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            ImportedFont = new Mfe.Font();
            ImportedFont.VariableWidth = false;
            byte height = (byte)heightNumericUpDown.Value;
            byte width = (byte)widthNumericUpDown.Value;
            byte first = (byte)firstCharNumericUpDown.Value;
            byte last = (byte)lastCharNumericUpDown.Value;
            ImportedFont.Width = width;
            ImportedFont.Height = height;
            try
            {
                byte[] data = System.IO.File.ReadAllBytes(filePathTextBox.Text);
                int index = 0;
                int b = 0;

                for (int ch = first; ch <= last; ch++)
                    for (int y = 0; y < height; y++)
                        for (int x = 0; x < width; x++, b <<= 1)
                        {
                            if ((x & 7) == 0)
                                b = data[index++];
                            ImportedFont[(byte)ch][y, x] = (b & 0x80) != 0;
                        }
                this.DialogResult = DialogResult.OK;
            }
            catch (Exception err)
            {
                MessageBox.Show($"Error: {err.ToString()}", "Import Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }
    }
}
