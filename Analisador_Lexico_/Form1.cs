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
using System.Linq;
using System.Collections;

namespace Analisador_Lexico_
{
    public partial class Form1 : Form
    {
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

                        textBox_File.Text = Regex.Replace(textBox_File.Text, @"^\s*$(\n|\r|\r\n)", "", RegexOptions.Multiline);
                    }

                }
                catch
                {
                    MessageBox.Show("Erro ao Abrir arquivo.");
                }
            }



        }


        public int[] retornaVetor()
        {
            //Use o método ReadAllLines da class File, para ler o seu arquivo
            string[] arquivo = textBox_File.Text.Split();
            /*

            foreach (var x in arquivo)
                System.Diagnostics.Debug.WriteLine(x.ToString() + Environment.NewLine);*/

            MessageBox.Show(arquivo[63].ToString());

            //Criando objeto para guarda numeros, não usei um array de inteiro
            //pois ainda não sabemos o seu tamanho
            ArrayList listaTemporaria = new ArrayList();

            //Vendo retorno do arquivo
            foreach (var linha in arquivo)
            {
                System.Diagnostics.Debug.WriteLine(linha.ToString() + Environment.NewLine);

                int valor = 19;
                if (linha == "program")
                    int.TryParse(linha, out valor);

                
                //Verificando se o conteudo é numero e atribuindo ao ArrayList
                if (int.TryParse(linha, out valor))
                    listaTemporaria.Add(valor);
            }



            //Retornando vetor de inteiro usando o método ToArray(), o resultado sera um int[]
            return listaTemporaria.Cast<int>().ToArray();
        }

        int compara_palavras = 0;
        private void converte_caracteres()
        {

            textBox_File.Text  = Regex.Replace(textBox_File.Text, String.Format(@"\b{0}\b", "program"), " 12 ", RegexOptions.None);


            textBox_File.Text = Regex.Replace(textBox_File.Text, @"\([0-9]\)", "", RegexOptions.Multiline);
            textBox_File.Text = Regex.Replace(textBox_File.Text, String.Format(@"\b\.\b"), " 13 ", RegexOptions.Multiline);
            textBox_File.Text = Regex.Replace(textBox_File.Text, String.Format(@"\b{0}\b", ">="), " 29 ", RegexOptions.None);

            textBox_File.Text = Regex.Replace(textBox_File.Text, String.Format(@"\b{0}\b", ">"), " 30 ", RegexOptions.Multiline);
            textBox_File.Text = Regex.Replace(textBox_File.Text, String.Format(@"\b{0}\b", "="), " 31 ", RegexOptions.Multiline);
            textBox_File.Text = Regex.Replace(textBox_File.Text, String.Format(@"\b{0}\b", "<>"), " 32 ", RegexOptions.Multiline);
            textBox_File.Text = Regex.Replace(textBox_File.Text, String.Format(@"\b{0}\b", "<="), " 33 ", RegexOptions.Multiline);
            textBox_File.Text = Regex.Replace(textBox_File.Text, String.Format(@"\b{0}\b", "<"), " 34 ", RegexOptions.Multiline);
            //textBox_File.Text = Regex.Replace(textBox_File.Text, String.Format(@"\b{0}\b", "+"), " 35 ", RegexOptions.Multiline);
            //MessageBox.Show("GZUZ: " );

        }

        private void converte_palavras()
        {
            textBox_File.Text = Regex.Replace(textBox_File.Text, String.Format(@"\b{0}\b", "write"), " 0 ", RegexOptions.None);
            textBox_File.Text = Regex.Replace(textBox_File.Text, String.Format(@"\b{0}\b", "while"), " 1 ", RegexOptions.None);
            textBox_File.Text = Regex.Replace(textBox_File.Text, String.Format(@"\b{0}\b", "until"), " 2 ", RegexOptions.None);
            textBox_File.Text = Regex.Replace(textBox_File.Text, String.Format(@"\b{0}\b", "to"), " 3 ", RegexOptions.None);
            textBox_File.Text = Regex.Replace(textBox_File.Text, String.Format(@"\b{0}\b", "then"), " 4 ", RegexOptions.None);
            textBox_File.Text = Regex.Replace(textBox_File.Text, String.Format(@"\b{0}\b", "string"), " 5 ", RegexOptions.None);
            textBox_File.Text = Regex.Replace(textBox_File.Text, String.Format(@"\b{0}\b", "repeat"), " 6 ", RegexOptions.None);
            textBox_File.Text = Regex.Replace(textBox_File.Text, String.Format(@"\b{0}\b", "real"), " 7 ", RegexOptions.None);
            textBox_File.Text = Regex.Replace(textBox_File.Text, String.Format(@"\b{0}\b", "read"), " 8 ", RegexOptions.None);
            textBox_File.Text = Regex.Replace(textBox_File.Text, String.Format(@"\b{0}\b", "program"), " 9 ", RegexOptions.None);
            textBox_File.Text = Regex.Replace(textBox_File.Text, String.Format(@"\b{0}\b", "procedure"), " 10 ", RegexOptions.None);
            textBox_File.Text = Regex.Replace(textBox_File.Text, String.Format(@"\b{0}\b", "or"), " 11 ", RegexOptions.None);
            textBox_File.Text = Regex.Replace(textBox_File.Text, String.Format(@"\b{0}\b", "of"), " 12 ", RegexOptions.None);
            textBox_File.Text = Regex.Replace(textBox_File.Text, String.Format(@"\b{0}\b", "literal"), " 13 ", RegexOptions.None);
            textBox_File.Text = Regex.Replace(textBox_File.Text, String.Format(@"\b{0}\b", "integer"), " 14 ", RegexOptions.None);
            textBox_File.Text = Regex.Replace(textBox_File.Text, String.Format(@"\b{0}\b", "if"), " 15 ", RegexOptions.None);
            textBox_File.Text = Regex.Replace(textBox_File.Text, String.Format(@"\b{0}\b", "identificador"), " 16 ", RegexOptions.None);
            textBox_File.Text = Regex.Replace(textBox_File.Text, String.Format(@"\b{0}\b", "for"), " 18 ", RegexOptions.None);
            textBox_File.Text = Regex.Replace(textBox_File.Text, String.Format(@"\b{0}\b", "end"), " 19 ", RegexOptions.None);
            textBox_File.Text = Regex.Replace(textBox_File.Text, String.Format(@"\b{0}\b", "else"), " 20 ", RegexOptions.None);
            textBox_File.Text = Regex.Replace(textBox_File.Text, String.Format(@"\b{0}\b", "do"), " 21 ", RegexOptions.None);
            textBox_File.Text = Regex.Replace(textBox_File.Text, String.Format(@"\b{0}\b", "declaravariaveis"), " 22 ", RegexOptions.None);
            textBox_File.Text = Regex.Replace(textBox_File.Text, String.Format(@"\b{0}\b", "const"), " 23 ", RegexOptions.None);
            textBox_File.Text = Regex.Replace(textBox_File.Text, String.Format(@"\b{0}\b", "char"), " 24 ", RegexOptions.None);
            textBox_File.Text = Regex.Replace(textBox_File.Text, String.Format(@"\b{0}\b", "chamaprocedure"), " 25 ", RegexOptions.None);
            textBox_File.Text = Regex.Replace(textBox_File.Text, String.Format(@"\b{0}\b", "begin"), " 26 ", RegexOptions.None);
            textBox_File.Text = Regex.Replace(textBox_File.Text, String.Format(@"\b{0}\b", "array"), " 27 ", RegexOptions.None);
            textBox_File.Text = Regex.Replace(textBox_File.Text, String.Format(@"\b{0}\b", "and"), " 28 ", RegexOptions.None);
            textBox_File.Text = Regex.Replace(textBox_File.Text, String.Format(@"\b{0}\b", "numreal"), " 36 ", RegexOptions.None);
            textBox_File.Text = Regex.Replace(textBox_File.Text, String.Format(@"\b{0}\b", "numinteiro"), " 37 ", RegexOptions.None);
            textBox_File.Text = Regex.Replace(textBox_File.Text, String.Format(@"\b{0}\b", "nomestring"), " 38 ", RegexOptions.None);
            textBox_File.Text = Regex.Replace(textBox_File.Text, String.Format(@"\b{0}\b", "real"), " 7 ", RegexOptions.None);




        }

        private void bt_Analize_Click(object sender, EventArgs e)
        {
            converte_caracteres();
            converte_palavras();
            richTextBox_File.Text = textBox_File.Text;
            //richTextBox_File.Text = RemoveAcentos_BH(instructionLine);
        }


        public static string RemoveAcentos_BH(string _textoNAOFormatado)
        {
            string ret;
            string pattern = @"(?i)[áéíóúàèìòùâêîôûãõç]";
            string replacement = " | ";
            Regex rgx = new Regex(pattern);
            ret = rgx.Replace(_textoNAOFormatado, replacement);
            return ret;
        }

        private void bt_Format_Click(object sender, EventArgs e)
        {
 
            string arquivo = textBox_File.Text;

            // Here we lowercase our input first.
            arquivo = arquivo.ToLower();
            //Match match = Regex.Match(arquivo, @"content/([A-Za-z0-9\-]+)\.aspx$");


            foreach (var x in arquivo)
            {


                System.Diagnostics.Debug.WriteLine("linha: "+ x.ToString() + Environment.NewLine);
            }

         

            /*
            foreach (var item in retornaVetor())
            {
                vetor[item] = Convert.ToString(retornaVetor().ToArray());
                System.Diagnostics.Debug.WriteLine("TOKEN " + " : " + vetor[item].ToString() + Environment.NewLine);
            }*/

        }
    }
}
