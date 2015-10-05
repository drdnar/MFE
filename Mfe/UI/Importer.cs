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
    public partial class Importer : Form
    {
        public Importer()
        {
            InitializeComponent();
        }

        protected Mfe.Font font = null;
        public Font ImportedFont
        {
            get
            {
                return font;
            }
        }

        private void pathButton_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                pathTextBox.Text = openFileDialog.FileName;
        }

        private void DoImport()
        {
            try
            {
                importFnt(pathTextBox.Text);
                MessageBox.Show("File export successfully.", "Export Succeeded", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception x)
            {
                MessageBox.Show("Error on export: " + x.Message, "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void importFnt(string path)
        {
            /*byte[] data = System.IO.File.ReadAllBytes(path);
            throw new NotImp*/
            throw new NotImplementedException();
        }
    }
}
