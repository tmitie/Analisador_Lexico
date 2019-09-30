using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Analisador_Lexico_
{
    public partial class Form1 : Form
    {
        private string arquivo;
        private string mensagem;
        List<string> mensagemLinha = new List<string>();
        OpenFileDialog OFD = new OpenFileDialog();
        StreamReader SR;
        string fileName = null;
        string path = null;
        string instructionLine = null;

        public Form1()
        {
            InitializeComponent();
        }


        private void bt_abrir_arquivo_Click(object sender, EventArgs e)
        {
            OFD.Filter = "Arquivo texto (*.txt)|*.txt";
            OFD.ShowDialog();

            if (string.IsNullOrEmpty(OFD.FileName) == false)
            {
                try
                {
                    using (SR = new StreamReader(OFD.FileName))
                    {

                        textBox_File.Text = SR.ReadToEnd().Trim();
                        richTextBox_File.Text = textBox_File.Text;
                        instructionLine = textBox_File.Text;
                        path = OFD.FileName;
                        fileName = Path.GetFileName(OFD.FileName);

                    }

                }
                catch
                {

                }
            }
        }
    }
}
