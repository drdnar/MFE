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
    public partial class InputBox : Form
    {
        /// <summary>
        /// This provides an input box like the one in Visual BASIC.
        /// If C# has something similar, I can't find it.
        /// </summary>
        public InputBox()
        {
            InitializeComponent();
            this.CancelButton = cancelButton;
            this.AcceptButton = doneButton;
        }

        public DialogResult ShowDialog(string prompt, string title, string initialText, int maxlen)
        {
            promptLabel.Text = prompt;
            this.Text = title;
            textBox.Text = initialText;
            textBox.MaxLength = maxlen;
            this.DialogResult = DialogResult.None;
            return base.ShowDialog();
        }

        public DialogResult ShowDialog(string prompt, string title, string initialText)
        {
            promptLabel.Text = prompt;
            this.Text = title;
            textBox.Text = initialText;
            return base.ShowDialog();
        }

        /// <summary>
        /// This is the text the user entered.
        /// </summary>
        public string EnteredText
        {
            get
            {
                return textBox.Text;
            }
            set
            {
                textBox.Text = value;
            }
        }

        private void doneButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
