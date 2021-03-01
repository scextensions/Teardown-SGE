using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace Teardown_SGE
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void openSavegameButton_Click(object sender, EventArgs e)
        {
            var fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Teardown\\savegame.xml");
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreWhitespace = true;
            settings.IgnoreComments = true;
            XmlReader reader = XmlReader.Create(fileName, settings);
            while (reader.Read())
            {
                Trace.Write(new string(' ', reader.Depth * 2));
                Trace.WriteLine(reader.NodeType);
            }
        }
    }
}
