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
    public partial class CharEditorWindow : Form
    {
        int scale = 12;
        MasterWindow Master;
        public Font CurrentFont
        {
            get
            {
                return Master.CurrentFont;
            }
        }
        public CharEditorWindow(MasterWindow master)
        {
            InitializeComponent();
            Master = master;
            charEditor.CurrentChar = CurrentFont[(int)charSelectorUpDown.Value];
            RefreshData();
            charEditor.FillOnDrag = true;
        }

        public void RefreshData()
        {
            characterWidthUpDown.Value = charEditor.CurrentChar.Width;
            logicalWidthUpDown.Value = charEditor.CurrentChar.LogicalWidth;
            characterWidthUpDown.Enabled = CurrentFont.VariableWidth && CurrentFont.WidthMustBeMultipleOfEight;
            /*if (CurrentFont.WidthMustBeMultipleOfEight)
                characterWidthUpDown.Increment = 8;
            else
                characterWidthUpDown.Increment = 1;*/
            logicalWidthUpDown.Enabled = CurrentFont.VariableWidth;
            if (codepointBox.Lines == null)
                codepointBox.Lines = new string[] { " " };
            if (codepointBox.Lines.Length == 0)
                codepointBox.Lines = new string[] { " " };
            if (codepointBox.Lines[0] == null)
                codepointBox.Lines[0] = " ";
            codepointBox.Text = charEditor.CurrentChar.Codepoint.ToString();
            characterNameBox.Text = charEditor.CurrentChar.Name;
            charLabel.Text = charEditor.CurrentChar.Codepoint.ToString();
            charEditor.CurrentChar = CurrentFont[(int)charSelectorUpDown.Value];
            charEditor.CharScale = scale;
            charEditor.Invalidate();
        }

        public void SelectChar(int ch)
        {
            charSelectorUpDown.Value = ch;
        }

        private void charSelectorUpDown_ValueChanged(object sender, EventArgs e)
        {
            charEditor.CurrentChar = CurrentFont[(int)charSelectorUpDown.Value];
            RefreshData();
        }

        private void characterWidthUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (CurrentFont.WidthMustBeMultipleOfEight)
            {
                if (characterWidthUpDown.Value % 8 == 0)
                    charEditor.CurrentChar.Width = (byte)(characterWidthUpDown.Value);
            }
            else
                charEditor.CurrentChar.Width = (byte)(characterWidthUpDown.Value);
            charEditor.OnPixelChanged();
            charEditor.Invalidate();
        }

        private void logicalWidthUpDown_ValueChanged(object sender, EventArgs e)
        {
            charEditor.CurrentChar.LogicalWidth = (byte)(logicalWidthUpDown.Value);
            if (!CurrentFont.WidthMustBeMultipleOfEight && CurrentFont.VariableWidth)
                characterWidthUpDown.Value = logicalWidthUpDown.Value;
            charEditor.OnPixelChanged();
            charEditor.Invalidate();
        }

        private void codepointBox_TextChanged(object sender, EventArgs e)
        {
            if (codepointBox.Lines != null && codepointBox.Lines.Length == 1 && codepointBox.Lines[0].Length == 1)
            {
                charEditor.CurrentChar.Codepoint = codepointBox.Lines[0][0];
                charLabel.Text = charEditor.CurrentChar.Codepoint.ToString();
            }
        }

        private void characterNameBox_TextChanged(object sender, EventArgs e)
        {
            if (characterNameBox.Lines != null && characterNameBox.Lines.Length == 1)
            {
                charEditor.CurrentChar.Name = characterNameBox.Lines[0];
            }
        }

        private void CharEditorWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
                e.Cancel = true;
        }

        private void biggerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (scale < 256)
                charEditor.CharScale = ++scale;
            charEditor.Invalidate();
        }

        private void smallerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (scale > 2)
                charEditor.CharScale = --scale;
            charEditor.Invalidate();
        }

        private void toggleModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            charEditor.ToggleMode = toggleModeToolStripMenuItem.Checked;
        }

        private void gridToolStripMenuItem_Click(object sender, EventArgs e)
        {
            charEditor.ShowGrid = gridToolStripMenuItem.Checked;
        }

        private void dragnFillToolStripMenuItem_Click(object sender, EventArgs e)
        {
            charEditor.FillOnDrag = dragnFillToolStripMenuItem.Checked;
        }

        private void charEditor_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.N:
                    if (charSelectorUpDown.Value < charSelectorUpDown.Maximum)
                        charSelectorUpDown.Value++;
                    break;
                case Keys.P:
                    if (charSelectorUpDown.Value > charSelectorUpDown.Minimum)
                        charSelectorUpDown.Value--;
                    break;
                case Keys.Add:
                    if (e.Control)
                        dataWider();
                    else
                        wider();
                    break;
                case Keys.Subtract:
                    if (e.Control) 
                        dataThinner();
                    else
                        thinner();
                    break;
                case Keys.G:
                    charEditor.ShowGrid = gridToolStripMenuItem.Checked = !gridToolStripMenuItem.Checked;
                    break;
                
            }
        }

               
        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            charEditor.CurrentChar.Clear();
            charEditor.Invalidate();
            charEditor.OnPixelChanged();
        }

        private void invertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            charEditor.CurrentChar.Invert();
            charEditor.Invalidate();
            charEditor.OnPixelChanged();
        }

        private DataObject doCopy()
        {
            DataObject obj = new DataObject();
            obj.SetText(charEditor.CurrentChar.ToDB());
            obj.SetImage(charEditor.CurrentChar.ToBitmap());
            obj.SetData(charEditor.CurrentChar);
            return obj;
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Master.ClipboardChar = charEditor.CurrentChar.Clone();
            Clipboard.SetDataObject(doCopy());
            //Clipboard.SetText(charEditor.CurrentChar.ToDB());
            //Clipboard.S
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*if (Master.ClipboardChar == null)
                return;
            CurrentFont[(int)charSelectorUpDown.Value] = Master.ClipboardChar.Clone();
            charEditor.CurrentChar = CurrentFont[(int)charSelectorUpDown.Value];
            RefreshData();
            charEditor.OnPixelChanged();*/
            DataObject obj = Clipboard.GetDataObject() as DataObject;
            if (obj != null && obj.GetDataPresent(typeof(Char)))
            {
                Char temp = (Char)obj.GetData(typeof(Char));
                if (temp == null)
                {
                    /*string str = "";
                    foreach (string s in obj.GetFormats())
                        str += s + " | ";
                    MessageBox.Show(str);*/
                    return;
                }
                else
                {
                    CurrentFont[(int)charSelectorUpDown.Value] = temp;
                }
            }
            else if (Clipboard.ContainsImage())
            {
                if (Clipboard.ContainsImage())
                {
                    char ch = CurrentFont[(int)charSelectorUpDown.Value].Codepoint;
                    string st = CurrentFont[(int)charSelectorUpDown.Value].Name;
                    if (!loadBitmap(Clipboard.GetImage()))
                    {
                        CurrentFont[(int)charSelectorUpDown.Value].Codepoint = ch;
                        CurrentFont[(int)charSelectorUpDown.Value].Name = st;
                    }
                }
            }
            else
            {
                /*string str = "Paste: Unable to accept clipboard data.";
                foreach (string s in obj.GetFormats())
                    str += s + " | ";
                MessageBox.Show(str);*/
                return;
            }
            charEditor.CurrentChar = CurrentFont[(int)charSelectorUpDown.Value];
            RefreshData();
            charEditor.OnPixelChanged();
        }

        private bool loadBitmap(Image asdf)
        { // Process data
            try
            {
                Bitmap img = new Bitmap(asdf);
                if (img != null)
                {
                    // Figure out what size of data to accept
                    int chwidth, chheight; // Width of new character
                    int datawidth, dataheight; // Width of data to copy from source bitmap
                    chheight = CurrentFont.Height;
                    if (!CurrentFont.VariableWidth)
                    {
                        chwidth = CurrentFont.Width;
                    }
                    else if (CurrentFont.WidthMustBeMultipleOfEight)
                    {
                        chwidth = (int)(Math.Ceiling((double)img.Width / 8.0) * 8);
                    }
                    else if (img.Width < 63)
                    {
                        chwidth = img.Width;
                    }
                    else
                        chwidth = 63;
                    if (img.Width < chwidth)
                        datawidth = img.Width;
                    else
                        datawidth = chwidth;
                    if (img.Height < chheight)
                        dataheight = img.Height;
                    else
                        dataheight = chheight;
                    Char newch = new Char();
                    newch.Height = (byte)chheight;
                    newch.Width = (byte)chwidth;
                    for (int y = 0; y < dataheight; y++)
                        for (int x = 0; x < datawidth; x++)
                            newch[y, x] = img.GetPixel(x, y).G < 0x80;
                    CurrentFont[(int)charSelectorUpDown.Value] = newch;
                    return false;
                }
                return true;
            }
            catch
            {
                //MessageBox.Show("Error.");
                return true;
            }
        }

        private void exchangeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataObject old = doCopy();
            pasteToolStripMenuItem_Click(this, null);
            Clipboard.SetDataObject(old);
        }

        private void nextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (scale < 256)
                charEditor.CharScale = ++scale;
            charEditor.Invalidate();
            charEditor.OnPixelChanged();
        }

        private void shiftUpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            charEditor.CurrentChar.ShiftUp(clearBottom: true);
            charEditor.Invalidate();
            charEditor.OnPixelChanged();
        }

        private void shiftDownToolStripMenuItem_Click(object sender, EventArgs e)
        {
            charEditor.CurrentChar.ShiftDown(clearTop: true);
            charEditor.Invalidate();
            charEditor.OnPixelChanged();
        }

        private void shiftLeftToolStripMenuItem_Click(object sender, EventArgs e)
        {
            charEditor.CurrentChar.ShiftLeft(clearRight: true);
            charEditor.Invalidate();
            charEditor.OnPixelChanged();
        }

        private void shiftRightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            charEditor.CurrentChar.ShiftRight(clearLeft: true);
            charEditor.Invalidate();
            charEditor.OnPixelChanged();
        }

        private void wider()
        {
            if (CurrentFont.VariableWidth && logicalWidthUpDown.Value < logicalWidthUpDown.Maximum)
            {
                logicalWidthUpDown.Value++;
                charEditor.CurrentChar.LogicalWidth = (byte)(logicalWidthUpDown.Value);
                charEditor.Invalidate();
                charEditor.OnPixelChanged();
            }
        }

        private void dataWider()
        {
            if (CurrentFont.VariableWidth && characterWidthUpDown.Value < characterWidthUpDown.Maximum)
            {
                characterWidthUpDown.Value++;
                charEditor.CurrentChar.Width = (byte)(characterWidthUpDown.Value);
                charEditor.Invalidate();
                charEditor.OnPixelChanged();
            }
        }

        private void widerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            wider();
        }

        private void thinner()
        {
            if (CurrentFont.VariableWidth && logicalWidthUpDown.Value > logicalWidthUpDown.Minimum)
            {
                logicalWidthUpDown.Value--;
                charEditor.CurrentChar.LogicalWidth = (byte)(logicalWidthUpDown.Value);
                charEditor.Invalidate();
                charEditor.OnPixelChanged();
            }
        }

        private void dataThinner()
        {
            if (CurrentFont.VariableWidth && characterWidthUpDown.Value > characterWidthUpDown.Minimum)
            {
                characterWidthUpDown.Value--;
                charEditor.CurrentChar.Width = (byte)(characterWidthUpDown.Value);
                charEditor.Invalidate();
                charEditor.OnPixelChanged();
            }
        }

        private void thinnerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            thinner();
        }

        private void characterBitmapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            charEditor.Focus();
        }

        private void characterCodepointToolStripMenuItem_Click(object sender, EventArgs e)
        {
            codepointBox.Focus();
        }

        private void characterNameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            characterNameBox.Focus();
        }

        private void findToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InputBox input = new InputBox();
            if (input.ShowDialog("Enter character to find:", "Find", "", 1) == System.Windows.Forms.DialogResult.OK)
                if (input.EnteredText.Length > 0)
                {
                    for (int i = 0; i < Mfe.Font.MaximumCodePoints; i++)
                        if (CurrentFont[i].Codepoint == input.EnteredText[0])
                        {
                            charSelectorUpDown.Value = i;
                            return;
                        }
                }
            
        }

        private void characterSelectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            charSelectorUpDown.Focus();
        }

        private void nextCharacterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (charSelectorUpDown.Value < charSelectorUpDown.Maximum)
                charSelectorUpDown.Value++;
        }

        private void previousCharacterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (charSelectorUpDown.Value > charSelectorUpDown.Minimum)
                charSelectorUpDown.Value--;
        }

        private void characterWidthUpDown_Validating(object sender, CancelEventArgs e)
        {
            characterWidthUpDown.Value = charEditor.CurrentChar.Width;
        }

        private void chartNavModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
/*            if (chartNavModeToolStripMenuItem.Checked)
            {
                chartNavModeToolStripMenuItem.Checked = false;
            }
            else
            {
                chartNavModeToolStripMenuItem.Checked = true;
            }*/
        }

        public bool ChartNavigationMode
        {
            get
            {
                return chartNavModeToolStripMenuItem.Checked;
            }
            set
            {
                chartNavModeToolStripMenuItem.Checked = value;
            }
        }

        private void viewToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void nextButton_Click(object sender, EventArgs e)
        {
            charSelectorUpDown.UpButton();
            charEditor.CurrentChar.Invert();
            Clipboard.SetDataObject(doCopy());
            charEditor.CurrentChar.Invert();
        }

        private void guidesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            charEditor.ShowGuides = guidesToolStripMenuItem.Checked;
        }
    }
}
