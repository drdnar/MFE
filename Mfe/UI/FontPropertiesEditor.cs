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
    public partial class FontPropertiesEditor : Form
    {
        MasterWindow Master;
        public FontPropertiesEditor(MasterWindow master)
        {
            Master = master;
            InitializeComponent();
            //keyComboBox.BeginUpdate();
            foreach (KeyValuePair<string, string> p in CurrentFont.AboutData)
                keyComboBox.Items.Add(p.Key);
            //keyComboBox.EndInvoke(
            RefreshData();
        }

        protected Font CurrentFont
        {
            get
            {
                return Master.CurrentFont;
            }
        }

        public void RefreshData()
        {
            Master.CharEditorWindow.RefreshData();
            firstCharUpDown.Value = CurrentFont.FirstCodePoint;
            lastCharUpDown.Value = CurrentFont.LastCodePoint;
            spaceUpDown.Value = CurrentFont.SpaceCodePoint;
            tabUpDown.Value = CurrentFont.TabCodePoint;
            newlineUpDown.Value = CurrentFont.NewlineCodePoint;
            heightUpDown.Value = CurrentFont.Height;
            widthUpDown.Value = CurrentFont[(int)'M'].Width;
            widthUpDown.Enabled = !variableWidthCheckBox.Checked;
            logicalWidthUpDown.Value = CurrentFont[(int)'M'].LogicalWidth;
            logicalWidthUpDown.Enabled = !variableWidthCheckBox.Checked;
            //widthUpDown.ReadOnly = !variableWidthCheckBox.Checked;
            variableWidthCheckBox.Checked = CurrentFont.VariableWidth;
            widthMustBeMultipleOf8CheckBox.Checked = CurrentFont.WidthMustBeMultipleOfEight;
            if (CurrentFont.AspectRatioHeight == 1 && (CurrentFont.AspectRatioWidth == 1  || CurrentFont.AspectRatioWidth == 2))
            {
                doubleWidthCheckBox.Checked = CurrentFont.AspectRatioWidth == 2;
            }
            else
            {
                doubleWidthCheckBox.CheckState = CheckState.Indeterminate;
                doubleWidthCheckBox.Enabled = false;
            }
            baselineUpDown.Value = CurrentFont.BaseLine;
            xheightUpDown.Value = CurrentFont.XHeight;
            capHeightUpDown.Value = CurrentFont.CapHeight;
            baselineUpDown.Maximum = CurrentFont.Height;
            xheightUpDown.Maximum = CurrentFont.Height;
            capHeightUpDown.Maximum = CurrentFont.Height;
            keyComboBox.Items.Clear();
            if (CurrentFont["Code Page"] != null)
                codepageNameTextbox.Text = CurrentFont["Code Page"];
            else
                codepageNameTextbox.Text = "<unspecified>";
            foreach (KeyValuePair<string, string> p in CurrentFont.AboutData)
                keyComboBox.Items.Add(p.Key);
        }

        private void FontPropertiesEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            Master.FontPropertiesEditor = null;
        }

        private void firstCharUpDown_ValueChanged(object sender, EventArgs e)
        {
            CurrentFont.FirstCodePoint = (byte)(firstCharUpDown.Value);
        }

        private void lastCharUpDown_ValueChanged(object sender, EventArgs e)
        {
            CurrentFont.LastCodePoint = (byte)(lastCharUpDown.Value);
        }

        private void spaceUpDown_ValueChanged(object sender, EventArgs e)
        {
            CurrentFont.SpaceCodePoint = (byte)(spaceUpDown.Value);
        }

        private void tabUpDown_ValueChanged(object sender, EventArgs e)
        {
            CurrentFont.TabCodePoint = (byte)(tabUpDown.Value);
        }

        private void newlineUpDown_ValueChanged(object sender, EventArgs e)
        {
            CurrentFont.NewlineCodePoint = (byte)(newlineUpDown.Value);
        }

        private void heightUpDown_ValueChanged(object sender, EventArgs e)
        {
            CurrentFont.Height = (byte)heightUpDown.Value;
            baselineUpDown.Maximum = CurrentFont.Height;
            xheightUpDown.Maximum = CurrentFont.Height;
            capHeightUpDown.Maximum = CurrentFont.Height;
            Master.CharEditorWindow.RefreshData();
        }

        private void widthUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (CurrentFont.VariableWidth)
                return;
            try
            {
                CurrentFont.Width = (byte)(widthUpDown.Value);
            }
            catch (ArgumentOutOfRangeException)
            {
                // Do nothing
            }
        }

        private void variableWidthCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            widthUpDown.Enabled = !variableWidthCheckBox.Checked;
            //widthUpDown.ReadOnly = !variableWidthCheckBox.Checked;
            CurrentFont.VariableWidth = variableWidthCheckBox.Checked;
            RefreshData();
            Master.CharEditorWindow.RefreshData();
        }

        private void baselineUpDown_ValueChanged(object sender, EventArgs e)
        {
            CurrentFont.BaseLine = (byte)baselineUpDown.Value;
            Master.CharEditorWindow.RefreshData();
        }

        private void xheightUpDown_ValueChanged(object sender, EventArgs e)
        {
            CurrentFont.XHeight = (byte)xheightUpDown.Value;
            Master.CharEditorWindow.RefreshData();
        }

        private void capHeightUpDown_ValueChanged(object sender, EventArgs e)
        {
            CurrentFont.CapHeight = (byte)capHeightUpDown.Value;
            Master.CharEditorWindow.RefreshData();
        }

        private void keyComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //groupBox1.Text = (++c1).ToString();
        }

        //int c2 = 0;
        private void keyComboBox_Validating(object sender, CancelEventArgs e)
        {
            //groupBox2.Text = (++c2).ToString();
            if (CurrentFont[keyComboBox.Text] != null)
                valueBox.Text = CurrentFont[keyComboBox.Text];
            else
                valueBox.Text = "";
        }

        private void valueBox_Validating(object sender, CancelEventArgs e)
        {
            if (CurrentFont[keyComboBox.Text] == null)
                keyComboBox.Items.Add(keyComboBox.Text);
            CurrentFont[keyComboBox.Text] = valueBox.Text;
            if (CurrentFont["Code Page"] != null)
                codepageNameTextbox.Text = CurrentFont["Code Page"];
            else
                codepageNameTextbox.Text = "<unspecified>";
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            CurrentFont.AboutData.Remove(keyComboBox.Text);
            keyComboBox.Items.Remove(keyComboBox.Text);
        }

        private void codepageImportButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (importCodePageFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    CodePageTable.FromFile(importCodePageFileDialog.FileName).ApplyToFont(CurrentFont);
                    Master.RefreshStuff();
                }
            }
            catch (Exception x)
            {
                MessageBox.Show("Error in import: " + x.ToString(), "Error in Import", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void codepageExportButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (exportCodePageFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    CodePageTable.CopyFromFont(CurrentFont).WriteToFile(exportCodePageFileDialog.FileName);
                    Master.RefreshStuff();
                }
            }
            catch (Exception x)
            {
                MessageBox.Show("Error in export: " + x.ToString(), "Error in Export", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void logicalWidthUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (!CurrentFont.VariableWidth)
                CurrentFont.LogicalWidth = (byte)(logicalWidthUpDown.Value);
        }

        private void widthMustBeMultipleOf8CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            CurrentFont.WidthMustBeMultipleOfEight = widthMustBeMultipleOf8CheckBox.Checked;
            Master.CharEditorWindow.RefreshData();
        }

        private void widthUpDown_Validating(object sender, CancelEventArgs e)
        {
            widthUpDown.Value = CurrentFont.Width;
        }

        private void doubleWidthCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (CurrentFont.AspectRatioHeight == 1 && (CurrentFont.AspectRatioWidth == 1 || CurrentFont.AspectRatioWidth == 2))
            {
                if (doubleWidthCheckBox.Checked)
                    CurrentFont.AspectRatioWidth = 2;
                else
                    CurrentFont.AspectRatioWidth = 1;
            }
            else
            {
                doubleWidthCheckBox.CheckState = CheckState.Indeterminate;
                doubleWidthCheckBox.Enabled = false;
            }
            Master.CharEditorWindow.RefreshData();
        }

    }
}
