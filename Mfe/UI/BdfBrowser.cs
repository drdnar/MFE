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
    public partial class BdfBrowser : Form
    {
        BdfImporter Master;
        public BdfBrowser(BdfImporter m)
        {
            Master = m;
            InitializeComponent();
            MdiParent = Master;
        }

        protected override void OnShown(EventArgs e)
        {
 	        base.OnShown(e);
            if (CurrentFont.Chars.Count == 0)
                return;
            charSelectUpDown.Maximum = CurrentFont.Chars.Count - 1;
            charSelectUpDown_ValueChanged(null, new EventArgs());
            Master.ImportedFont = CurrentFont.MakeMfeFont(CodePageTable.CopyFromFont(new Font()));
            destCharPreviewer.Char = ImportedFont[(int)destSelectUpDown.Value];
            Master.Chart.RefreshData();
        }
    
        protected BdfFont CurrentFont
        {
            get
            {
                return Master.BdfFont;
            }
        }

        protected Mfe.Font ImportedFont
        {
            get
            {
                return Master.ImportedFont;
            }
            set
            {
                Master.ImportedFont = value;
            }
        }

        private void magnificationUpDown_ValueChanged(object sender, EventArgs e)
        {
            selectedCharPreviewer.CharScale = destCharPreviewer.CharScale = (int)magnificationUpDown.Value;
        }

        private void charSelectUpDown_ValueChanged(object sender, EventArgs e)
        {
            selectedCharPreviewer.Char = CurrentFont.Chars[(int)charSelectUpDown.Value];
            selectedCharNameTextBox.Text = CurrentFont.Chars[(int)charSelectUpDown.Value].Name;
            selectedCharCodepointTextBox.Text = CurrentFont.Chars[(int)charSelectUpDown.Value].Codepoint.ToString();
        }

        private void destSelectUpDown_ValueChanged(object sender, EventArgs e)
        {
            destCharPreviewer.Char = ImportedFont[(int)destSelectUpDown.Value];
        }

        private void defaultCpButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Ugly kludge, but whatever
                Master.ImportedFont = CurrentFont.MakeMfeFont(CodePageTable.CopyFromFont(new Font()));
                destCharPreviewer.Char = ImportedFont[(int)destSelectUpDown.Value];
                Master.Chart.RefreshData();
            }
            catch (Exception f)
            {
                MessageBox.Show("Error: " + f.ToString(), "Error Loading Codepage File", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void loadCpButton_Click(object sender, EventArgs e)
        {
            if (loadCodepageDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                try
                {
                    ImportedFont = CurrentFont.MakeMfeFont(
                        CodePageTable.FromFile(loadCodepageDialog.FileName)
                        );
                    destCharPreviewer.Char = ImportedFont[(int)destSelectUpDown.Value];
                    Master.Chart.RefreshData();
                }
                catch (Exception f)
                {
                    MessageBox.Show("Error: " + f.ToString(), "Error Loading Codepage File", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
        }

        private void copyToDestButton_Click(object sender, EventArgs e)
        {
            ImportedFont[(int)destSelectUpDown.Value] = destCharPreviewer.Char = CurrentFont.Chars[(int)charSelectUpDown.Value];
            Master.Chart.RefreshData();
        }


    }
}
