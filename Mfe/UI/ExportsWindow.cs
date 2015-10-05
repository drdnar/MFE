using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace Mfe
{
    public partial class ExportsWindow : Form
    {
        private Font CurrentFont
        {
            get
            {
                return Master.CurrentFont;
            }
        }
        
        MasterWindow Master;
        public ExportsWindow(MasterWindow master)
        {
            InitializeComponent();
            Master = master;
            this.MdiParent = master;
            this.Show();
            foreach (Exports.FontExporter.Export e in Exports.FontExporter.Exports)
                exporterListBox.Items.Add(e.FileTypeDescription);
        }

        private bool haveNotSelectedExport()
        {
            return (exporterListBox.SelectedIndex < 0);
        }

        private void browseButton_Click(object sender, EventArgs e)
        {
            if (haveNotSelectedExport())
                return;
            Exports.FontExporter.Export export = Exports.FontExporter.Exports[exporterListBox.SelectedIndex];
            exportFileDialog.Filter = export.FileTypeDescription + "|*." + export.FileTypeExtension + "|All files|*.*";
            if (exportFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                pathTextBox.Text = exportFileDialog.FileName;
        }

        private void exporterListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (haveNotSelectedExport())
                return;
            exporterDescriptionTextBox.Text = Exports.FontExporter.Exports[exporterListBox.SelectedIndex].HelpText;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (haveNotSelectedExport())
                return;
            try
            {
                Exports.FontExporter.Exports[exporterListBox.SelectedIndex].DoExport(CurrentFont, pathTextBox.Text);
                MessageBox.Show("File export successfully.", "Export Succeeded", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception x)
            {
                MessageBox.Show("Error on export: " + x.Message, "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ExportsWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            Master.ExportsWindow = null;
        }
    }
}
