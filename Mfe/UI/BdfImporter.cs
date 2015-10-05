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
    public partial class BdfImporter : Form
    {
        BdfBrowser Browser;
        public PreviewChart Chart;
        MasterWindow Master;
        public BdfFont BdfFont;
        public BdfImporter(MasterWindow m)
        {
            InitializeComponent();
            ImportedFont.WidthMustBeMultipleOfEight = false;
            Chart = new PreviewChart(this);
            Browser = new BdfBrowser(this);
            Master = m;
        }

        public Font ImportedFont = new Font();

        private void okButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            Close();
        }

        private void BdfImporter_Shown(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
            {
                this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                this.Close();
                return;
            }
            try
            {
                BdfFont = new BdfFont(openFileDialog.FileName);
            }
            catch (Exception f)
            {
                this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                Close();
                MessageBox.Show("Error: " + f.ToString(), "Error Importing Font", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Browser.Show();
        }

        private void BdfImporter_FormClosing(object sender, FormClosingEventArgs e)
        {
            Browser.Close();
        }
    }
}
