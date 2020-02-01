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
    public partial class MasterWindow : Form
    {
        public Char ClipboardChar = null;
        protected Font currentFont = null;
        public Font CurrentFont
        {
            get
            {
                return currentFont;
            }
            set
            {
                //if (CharEditorWindow != null)
                //    CharEditorWindow.CurrentFont = value;
                currentFont = value;
                CurrentFontOriginalPath = "";
            }
        }
        public string CurrentFontOriginalPath = "";

        public CharEditorWindow CharEditorWindow = null;
        public FontPropertiesEditor FontPropertiesEditor = null;
        public ExportsWindow ExportsWindow = null;
        public PreviewChart PreviewChart = null;
        public HelpWindow HelpWindow = null;

        public MasterWindow()
        {
            InitializeComponent();
            CurrentFont = new Font();
            CharEditorWindow = new CharEditorWindow(this);
            CharEditorWindow.MdiParent = this;
            CharEditorWindow.Show();
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    using (var file = System.IO.File.OpenRead(openFileDialog.FileName))
                    {
                        byte[] header = new byte[2];
                        file.Read(header, 0, 2);
                        if ((char)header[0] == 'M' && (char)header[1] == 'F')
                            currentFont = Mfe.Font.ReadFromFile(CurrentFontOriginalPath = openFileDialog.FileName);
                        else if ((char)header[0] == 'M' && (char)header[1] == 'Z')
                            throw new ArgumentException(".FON files are not supported; open an individual .FNT resource instead.");
                        else if (header[0] == 0 && (header[1] == 2 || header[1] == 3))
                        {
                            currentFont = Mfe.FntImporter.Import(System.IO.File.ReadAllBytes(openFileDialog.FileName));
                            CurrentFontOriginalPath = "";
                        }
                        else
                            throw new ArgumentException("Unknown font format.");
                    }
                        
                    RefreshStuff();
                }
            }
            catch (Exception x)
            {
                MessageBox.Show("Error loading file: " + x.ToString(), "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void RefreshStuff()
        {
            if (CharEditorWindow != null)
                CharEditorWindow.RefreshData();
            if (FontPropertiesEditor != null)
                FontPropertiesEditor.RefreshData();
            if (PreviewChart != null)
                PreviewChart.RefreshData();
            foreach (Form form in this.MdiChildren)
                if (form is PreviewText)
                    ((PreviewText)form).RefreshText();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (currentFont == null)
                    return;
                if (CurrentFontOriginalPath != null && !CurrentFontOriginalPath.Equals(""))
                    currentFont.WriteToFile(CurrentFontOriginalPath);
                else
                    if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        currentFont.WriteToFile(CurrentFontOriginalPath = saveFileDialog.FileName);
            }
            catch (Exception x)
            {
                MessageBox.Show("Error saving file: " + x.ToString(), "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentFont == null)
                CurrentFont = new Font();
            else
                if (MessageBox.Show("Any changes after the last save will be lost. Proceed?", "Font May be Lost", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                    CurrentFont = new Font();
                else
                    return;
            RefreshStuff();
            characterEditorToolStripMenuItem_Click(sender, e);
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void chartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentFont == null)
                return;
            if (PreviewChart == null)
                new PreviewChart(this);
            else
                PreviewChart.BringToFront();
        }

        private void characterEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentFont == null)
                return;
            if (CharEditorWindow == null)
            {
                CharEditorWindow = new CharEditorWindow(this);
                CharEditorWindow.MdiParent = this;
                CharEditorWindow.Show();
            }
            else
            {
                CharEditorWindow.BringToFront();
            }
        }

        private void fontPropertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentFont == null)
                return;
            if (FontPropertiesEditor == null)
            {
                FontPropertiesEditor = new FontPropertiesEditor(this);
                FontPropertiesEditor.MdiParent = this;
                FontPropertiesEditor.Show();
            }
            else
                FontPropertiesEditor.BringToFront();
        }

        private void textPreviewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PreviewText x = new PreviewText(this);
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try 
            {
                if (currentFont == null)
                    return;
                if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    currentFont.WriteToFile(CurrentFontOriginalPath = saveFileDialog.FileName);
            }
            catch (Exception x)
            {
                MessageBox.Show("Error saving file: " + x.ToString(), "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void exportCodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentFont == null)
                return;
            if (ExportsWindow == null)
            {
                ExportsWindow = new ExportsWindow(this);
            }
            else
                ExportsWindow.BringToFront();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Monochrome Font Editor\n31 January 2020", "About MFE");
        }

        private void readMeFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (HelpWindow == null)
            {
                HelpWindow = new HelpWindow(this);
            }
            else
                HelpWindow.BringToFront();
        }

        private void importToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Importer importer = new Importer();
            if (importer.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                if (importer.ImportedFont != null)
                    CurrentFont = importer.ImportedFont;
        }

        private void importBDFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BdfImporter importer = new BdfImporter(this);
            if (importer.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                CurrentFontOriginalPath = "";
                currentFont = importer.ImportedFont;
                RefreshStuff();
            }
            
        }

        private void importRawBitmapTableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RawBitmapTableImporter importer = new RawBitmapTableImporter();
            try
            {
                if (importer.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    currentFont = importer.ImportedFont;
                    CurrentFontOriginalPath = "";
                    RefreshStuff();
                }
            }
            catch (Exception x)
            {
                MessageBox.Show("Error loading file: " + x.ToString(), "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
