using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Text;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;

namespace Mfe
{
    public partial class HelpWindow : Form
    {
        public MasterWindow Master;
        protected XDocument Index;

        public HelpWindow(MasterWindow master)
        {
            Master = master;
            InitializeComponent();
            this.MdiParent = Master;
            this.Show();

            Index = XDocument.Parse(GetTextFile("index.xml"));
            TreeNode rootNode = new TreeNode();
            rootNode.Text = "MFE";
            topicsTreeView.Nodes.Add(rootNode);
            
            addNodes(Index.FirstNode, rootNode);
            
            currentItemRichTextBox.ReadOnly = true;
            currentItemRichTextBox.Rtf = GetTextFile("default.rtf");

            rootNode.ExpandAll();
        }

        protected static string GetTextFile(string name)
        {
            using (Stream s = Assembly.GetExecutingAssembly().GetManifestResourceStream("Mfe.Help." + name))
            using (StreamReader sr = new StreamReader(s))
                return sr.ReadToEnd();
        }

        private void addNodes(XNode root, TreeNode treeRoot)
        {
            TreeNode t;
            if (root.NodeType != XmlNodeType.Element)
                return;
            XElement e = root as XElement;
            switch (e.Name.LocalName)
            {
                case "topic":
                    treeRoot.Text = e.Attributes().Where(attrib => attrib.Name.LocalName == "title").First().Value;
                    if (e.Attributes().Where(attrib => attrib.Name.LocalName == "filename").Count() > 0)
                        treeRoot.Tag = GetTextFile(e.Attributes().Where(attrib => attrib.Name.LocalName == "filename").First().Value);
                    /*temp = .First().Value;
                    if (temp != null && temp != "")
                        treeRoot.Tag = temp;*/
                    foreach (XElement item in e.Nodes())
                    {
                        t = new TreeNode();
                        treeRoot.Nodes.Add(t);
                        addNodes(item, t);
                    }
                    break;
                case "section":
                    treeRoot.Text = e.Attributes().Where(attrib => attrib.Name.LocalName == "title").First().Value;
                    treeRoot.Tag = GetTextFile(e.Attributes().Where(attrib => attrib.Name.LocalName == "filename").First().Value);
                    break;
            }
            //if (root.Name.LocalName
        }


        private void HelpWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            Master.HelpWindow = null;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return;
            currentItemRichTextBox.SaveFile(saveFileDialog.FileName);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return;
            currentItemRichTextBox.LoadFile(openFileDialog.FileName);
        }

        private void topicsTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Tag != null && (string)(e.Node.Tag) != "")
                currentItemRichTextBox.Rtf = (string)e.Node.Tag;
        }

        /*private void textBox1_TextChanged(object sender, EventArgs e)
        {
            currentItemRichTextBox.Rtf = textBox1.Text;
        }*/

    }
}
