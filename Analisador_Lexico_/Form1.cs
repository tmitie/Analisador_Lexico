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
        SaveFileDialog SW;
        StreamReader SR;
        string fileName = null;
        string path = null;
        string instructionLine = null;
        string line = null;

        bool first_click = true;
        bool flag_comentario_bloco = false;
        bool flag_comentario_linha = false;

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

                        textBox_Input.Text = SR.ReadToEnd().Trim();
                        instructionLine = textBox_Input.Text;
                        path = OFD.FileName;
                        fileName = Path.GetFileName(OFD.FileName);
                        SR.Close();
                    }

                }
                catch
                {
                    MessageBox.Show("Erro ao Abrir arquivo.");
                }
            }



        }

        //AINDA Não está sendo usado
        public int[] retornaVetor()
        {
            //Use o método ReadAllLines da class File, para ler o seu arquivo
            string[] arquivo = textBox_Input.Text.Split();
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


        //Transforma os operadores e simbolos de pontuação em tokens
        private string converte_caracteres()
        {
            instructionLine = Regex.Replace(instructionLine, String.Format(@"{0}", "î"), " 17 ", RegexOptions.None);
            instructionLine = Regex.Replace(instructionLine, String.Format(@"{0}", "<>"), " 32 ", RegexOptions.None);//Não colocar em ordem, dá errado
            instructionLine = Regex.Replace(instructionLine, String.Format(@"{0}", ">="), " 29 ", RegexOptions.None);//Não colocar em ordem, dá errado
            instructionLine = Regex.Replace(instructionLine, String.Format(@"{0}", "<="), " 33 ", RegexOptions.None);//Não colocar em ordem, dá errado
            instructionLine = Regex.Replace(instructionLine, String.Format(@"{0}", ">"), " 30 ", RegexOptions.None);
            instructionLine = Regex.Replace(instructionLine, String.Format(@"{0}", "="), " 31 ", RegexOptions.None);   
            instructionLine = Regex.Replace(instructionLine, String.Format(@"{0}", "<"), " 34 ", RegexOptions.None);
            instructionLine = Regex.Replace(instructionLine, String.Format(@"(?i)[+]"), " 35 ", RegexOptions.None);// + dá problema assim
            instructionLine = Regex.Replace(instructionLine, String.Format(@"(?i)[\]]"), " 39 ", RegexOptions.None);
            instructionLine = Regex.Replace(instructionLine, String.Format(@"(?i)[\[]"), " 40 ", RegexOptions.None);
            instructionLine = Regex.Replace(instructionLine, String.Format(@"{0}", ";"), " 41 ", RegexOptions.None);
            instructionLine = Regex.Replace(instructionLine, String.Format(@"{0}", ":"), " 42 ", RegexOptions.None);
            instructionLine = Regex.Replace(instructionLine, String.Format(@"{0}", "/"), " 43 ", RegexOptions.None);
            instructionLine = Regex.Replace(instructionLine, String.Format(@"{0}", ","), " 46 ", RegexOptions.None);
            instructionLine = Regex.Replace(instructionLine, String.Format(@"(?i)[*]"), " 47 ", RegexOptions.None);
            instructionLine = Regex.Replace(instructionLine, String.Format(@"(?i)[)]"), " 48 ", RegexOptions.None);
            instructionLine = Regex.Replace(instructionLine, String.Format(@"(?i)[(]"), " 49 ", RegexOptions.None);
            instructionLine = Regex.Replace(instructionLine, String.Format(@"(?i)[$]"), " 50 ", RegexOptions.None);
            instructionLine = Regex.Replace(instructionLine, String.Format(@"(?i)[-]"), " 51 ", RegexOptions.None);
            instructionLine = Regex.Replace(instructionLine, String.Format(@"(?i)/d"), " digito ", RegexOptions.None);

            return instructionLine;
        }

        //Não funciona ainda
        public void verifica_comentario_de_linha()
        {
            var caractere = textBox_Input.Text.Split();
            string[] aux = new string[caractere.Length];

            string[] lines = textBox_Input.Text.Split();
            int i = 0;
            
   
            foreach (var x in aux)
            {
                if (x.ToString() == "%%")//Se encontrar comentario de linha
                {
                    flag_comentario_linha = true;
                    aux[i] = "";
                    //lines[x] = 2;
                    MessageBox.Show("Detectou");
                    // @"^\s*$(\n|\r|\r\n)"

                }
                if (x == "\n" | x == @"^\s*$(\r)" | x == @"^\s*$(\r\n)" | x == Environment.NewLine)
                {
                    MessageBox.Show("Quebra");

                }
                System.Diagnostics.Debug.WriteLine("COMENTARIO LINHA: " + x.ToString() + Environment.NewLine);
            }
            /*
            foreach (var x in caractere)
            {
                if (x.ToString() == "%%")//Se encontrar comentario de linha
                {
                    flag_comentario_linha = true;
                    aux[i] = "";
                    MessageBox.Show("Detectou");
                    // @"^\s*$(\n|\r|\r\n)"

                }
                else if(flag_comentario_linha == true) 
                {
                    //Se encontrou quebra de linha, fecha o comentário
                    if (x.ToString() == @"^\s*$(\n)" | x.ToString() == @"^\s*$(\r)" | x.ToString() == @"^\s*$(\r\n)" | x.ToString()== Environment.NewLine)
                    {
                        flag_comentario_linha = false;

                    }
                    else
                    {   //Quando não encontrar a quebra de linha, atribui vazio
                        MessageBox.Show("e agoraaa?");
                        aux[i] = " ";
                    }
                    
                }
                textBox_Input.Text += aux[i] + " ";
                i++;
            }*/
            //return instrucao;
        }

        //Usado para verificar pontos simples e duplo e comentarios
        public string verificador_unitario(string instrucao)
        {
            var caractere = instrucao.Split();
            string[] aux = new string[caractere.Length];
            instrucao = "";
            int i = 0;

            foreach (var x in caractere)
            {
                //Verifica abertura de comentário
                if(x.ToString() == "%*")
                {
                    flag_comentario_bloco = true;
                    aux[i] = " ";
                   // MessageBox.Show("ABRIU COMENTÁRIO: " + aux[i]);
                    System.Diagnostics.Debug.WriteLine("INSTRUÇÃO " + i + " : " + aux[i].ToString() + Environment.NewLine);

                }
               

                //Usado para verificar pontos simples e duplo
                if (flag_comentario_bloco == false)
                {
                    if (x.ToString() == ".")
                    {
                        //MessageBox.Show("PONTO");
                        aux[i] = " 45 ";

                    }
                    else if (x.ToString() == "..")
                    {
                        //MessageBox.Show("DOIS PONTO");
                        aux[i] = " 44 ";
                        //instructionLine = instructionLine.Replace(x, " 44REP ");
                    }
                    else
                    {
                        aux[i] = x.ToString();
                    }

                    System.Diagnostics.Debug.WriteLine("Não Comentário " + i + " : " + aux[i].ToString() + Environment.NewLine);
                }


                //Se  comentário foi aberto, atribui vazio para as pos~ições contidas dentro de %*, até que encontre *%
                if (flag_comentario_bloco == true)
                {
                    if (x.ToString() == "*%")
                    {
                        flag_comentario_bloco = false;
                        aux[i] = "";
                        System.Diagnostics.Debug.WriteLine("INSTRUÇÃO " + i + " : " + aux[i].ToString() + Environment.NewLine);
                        //MessageBox.Show("Fechou COMENTÁRIO: " + aux[i]);
                    }
                    else
                    {
                        aux[i] = "";
                        System.Diagnostics.Debug.WriteLine("INSTRUÇÃO " + i + " : " + aux[i].ToString() + Environment.NewLine);
                        //MessageBox.Show("VAZIO: " + aux[i].ToString());
                    }
                }

                instrucao += aux[i] + " ";
                i++;
            }

            i = 0; 
            return instrucao;
        }


        //Transforma as constantes em tokens
        private string converte_palavras()
        {
            instructionLine = Regex.Replace(instructionLine, String.Format(@"\b{0}\b", "write"), " 0 ", RegexOptions.None);
            instructionLine = Regex.Replace(instructionLine, String.Format(@"\b{0}\b", "while"), " 1 ", RegexOptions.None);
            instructionLine = Regex.Replace(instructionLine, String.Format(@"\b{0}\b", "until"), " 2 ", RegexOptions.None);
            instructionLine = Regex.Replace(instructionLine, String.Format(@"\b{0}\b", "to"), " 3 ", RegexOptions.None);
            instructionLine = Regex.Replace(instructionLine, String.Format(@"\b{0}\b", "then"), " 4 ", RegexOptions.None);
            instructionLine = Regex.Replace(instructionLine, String.Format(@"\b{0}\b", "string"), " 5 ", RegexOptions.None);
            instructionLine = Regex.Replace(instructionLine, String.Format(@"\b{0}\b", "repeat"), " 6 ", RegexOptions.None);
            instructionLine = Regex.Replace(instructionLine, String.Format(@"\b{0}\b", "real"), " 7 ", RegexOptions.None);
            instructionLine = Regex.Replace(instructionLine, String.Format(@"\b{0}\b", "read"), " 8 ", RegexOptions.None);
            instructionLine = Regex.Replace(instructionLine, String.Format(@"\b{0}\b", "program"), " 9 ", RegexOptions.None);
            instructionLine = Regex.Replace(instructionLine, String.Format(@"\b{0}\b", "procedure"), " 10 ", RegexOptions.None);
            instructionLine = Regex.Replace(instructionLine, String.Format(@"\b{0}\b", "or"), " 11 ", RegexOptions.None);
            instructionLine = Regex.Replace(instructionLine, String.Format(@"\b{0}\b", "of"), " 12 ", RegexOptions.None);
            instructionLine = Regex.Replace(instructionLine, String.Format(@"\b{0}\b", "literal"), " 13 ", RegexOptions.None);
            instructionLine = Regex.Replace(instructionLine, String.Format(@"\b{0}\b", "integer"), " 14 ", RegexOptions.None);
            instructionLine = Regex.Replace(instructionLine, String.Format(@"\b{0}\b", "if"), " 15 ", RegexOptions.None);
            instructionLine = Regex.Replace(instructionLine, String.Format(@"\b{0}\b", "identificador"), " 16 ", RegexOptions.None);
            instructionLine = Regex.Replace(instructionLine, String.Format(@"\b{0}\b", "for"), " 18 ", RegexOptions.None);
            instructionLine = Regex.Replace(instructionLine, String.Format(@"\b{0}\b", "end"), " 19 ", RegexOptions.None);
            instructionLine = Regex.Replace(instructionLine, String.Format(@"\b{0}\b", "else"), " 20 ", RegexOptions.None);
            instructionLine = Regex.Replace(instructionLine, String.Format(@"\b{0}\b", "do"), " 21 ", RegexOptions.None);
            instructionLine = Regex.Replace(instructionLine, String.Format(@"\b{0}\b", "declaravariaveis"), " 22 ", RegexOptions.None);
            instructionLine = Regex.Replace(instructionLine, String.Format(@"\b{0}\b", "const"), " 23 ", RegexOptions.None);
            instructionLine = Regex.Replace(instructionLine, String.Format(@"\b{0}\b", "char"), " 24 ", RegexOptions.None);
            instructionLine = Regex.Replace(instructionLine, String.Format(@"\b{0}\b", "chamaprocedure"), " 25 ", RegexOptions.None);
            instructionLine = Regex.Replace(instructionLine, String.Format(@"\b{0}\b", "begin"), " 26 ", RegexOptions.None);
            instructionLine = Regex.Replace(instructionLine, String.Format(@"\b{0}\b", "array"), " 27 ", RegexOptions.None);
            instructionLine = Regex.Replace(instructionLine, String.Format(@"\b{0}\b", "and"), " 28 ", RegexOptions.None);
            instructionLine = Regex.Replace(instructionLine, String.Format(@"\b{0}\b", "numreal"), " 36 ", RegexOptions.None);
            instructionLine = Regex.Replace(instructionLine, String.Format(@"\b{0}\b", "numinteiro"), " 37 ", RegexOptions.None);
            instructionLine = Regex.Replace(instructionLine, String.Format(@"\b{0}\b", "nomestring"), " 38 ", RegexOptions.None);
            instructionLine = Regex.Replace(instructionLine, String.Format(@"\b{0}\b", "real"), " 7 ", RegexOptions.None);

            //instructionLine = Regex.Replace(instructionLine, @"[0-9]", "{" + instructionLine + "}", RegexOptions.Multiline);

            return instructionLine;
        }

        private void bt_Analize_Click(object sender, EventArgs e)
        {
            try
            {
                instructionLine = textBox_Input.Text;
                textBox_Input.Text = instructionLine = Regex.Replace(instructionLine, @"^\s*$(\n|\r|\r\n)", "", RegexOptions.Multiline);
                textBox_Tokens.Text = instructionLine = verificador_unitario(instructionLine);
                textBox_Tokens.Text = converte_caracteres();
                textBox_Tokens.Text = converte_palavras();

            }
            catch
            {
                MessageBox.Show("Erro ao fazer a conversão de tokens! ");
            }

            
        }

        //AINDA Não está sendo usado
        public static string RemoveAcentos_BH(string _textoNAOFormatado)
        {
            string ret;
            string pattern = @"(?i)[áéíóúàèìòùâêîôûãõç]";
            string replacement = " | ";
            Regex rgx = new Regex(pattern);
            ret = rgx.Replace(_textoNAOFormatado, replacement);
            return ret;
        }

        //AINDA Não está sendo usado
        private void bt_Format_Click(object sender, EventArgs e)
        {
 
            string arquivo = textBox_Input.Text;

            // Here we lowercase our input first.
            arquivo = arquivo.ToLower();
            //Match match = Regex.Match(arquivo, @"content/([A-Za-z0-9\-]+)\.aspx$");


            foreach (var x in arquivo)
            {
                System.Diagnostics.Debug.WriteLine("linha: "+ x.ToString() + Environment.NewLine);
            }
        }



        private void salvarSaídaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                SW = new SaveFileDialog();
                SW.Filter = "Text File|*.txt";
                SW.Title = "Salvar arquivo como....";
                SW.ShowDialog();
                System.IO.File.WriteAllText(SW.FileName, textBox_Tokens.Text);
            }
            catch
            {
                MessageBox.Show("Erro ao salvar arquivo!");
            }
              

        }
    }
}
