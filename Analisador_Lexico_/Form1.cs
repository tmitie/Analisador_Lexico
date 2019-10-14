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
        string[] instrucao_final = null;

        bool flag_comentario_bloco = false;

        string[] identificadores = new string[50];
        string[] reais = new string[50];
        string[] conjunto_de_strings = new string[50];

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







        private string detecta_erros_lexicos(string instrucao)
        {
            var caractere = instrucao.Split();
            int Cont = 0;
            instrucao_final = caractere.ToArray();                

            string pattern_identificadores = @"['][_a-zA-Z][_a-zA-Z0-9]*[']";//Match de identificadores
            string pattern_numeros = @"[0-9]+"; //Identifica numeros inteiros
            string pattern_real = @"[']?[0-9]*\.?[0-9]+[']"; //Identifica numeros float OK
            string pattern_string = @"[\!][a-zA-Z0-9\s]*[\!]";//Identifica strings OK


            Regex rgx_id = new Regex(pattern_identificadores);
            Match match_identificadores = rgx_id.Match(instrucao);

            Regex rgx_num = new Regex(pattern_numeros);
            Match match_numeros = rgx_num.Match(instrucao);

            Regex rgx_real = new Regex(pattern_real);
            Match match_real = rgx_real.Match(instrucao);

            Regex rgx_string = new Regex(pattern_string, RegexOptions.IgnoreCase);
            Match match_string = rgx_string.Match(instrucao);

           
            /*
            Regex aNum = new Regex("[a-zA-Z0-9\\s]");
            Match match_strings = aNum.Match(instrucao);*/

            if (match_identificadores.Success)
            {
                instrucao = Regex.Replace(instrucao, pattern_identificadores, " ID ", RegexOptions.None);
            }

            

            /*
            while (match_real.Success)
            {
                //MessageBox.Show("ENTROU");
                System.Diagnostics.Debug.WriteLine(match_real.Value + " found REAL at position :" + match_real.Index.ToString());
                reais[Cont] = match_real.Value;
                instrucao = Regex.Replace(instrucao, match_real.Value, " REAL ", RegexOptions.None);
                //match_real.Value[match_real.Index] = "REAL";
                Cont++;
                match_real = match_real.NextMatch();
            }
            instrucao = Regex.Replace(instrucao, pattern_real, " REAL ", RegexOptions.None);*/

            Cont = 0;
            while (match_string.Success)
            {
                    //System.Diagnostics.Debug.WriteLine(match_string.Value + " found STRING at position :" + match_string.Index.ToString());
                    conjunto_de_strings[Cont] = match_string.Value;
                    Cont++;
                    match_string = match_string.NextMatch();
            }
            instrucao = Regex.Replace(instrucao, pattern_string, " STR ", RegexOptions.None);


            Cont = 0;
            foreach (var x in caractere)
            {
                if (Regex.IsMatch(x.ToString(), pattern_real))
                {
                    System.Diagnostics.Debug.WriteLine(match_real.Value + " found REAL at position :" + match_real.Index.ToString());
                    reais[Cont] = x;
                    Cont++;
                    match_real = match_real.NextMatch();
                }

            }
            instrucao = Regex.Replace(instrucao, pattern_real, " REAL ", RegexOptions.None);

            //if(!match_identificadores.Success & !match_string.Success & !match_real.Success)

            caractere = instrucao.Split();
            string[] aux = new string[caractere.Length];
            Cont = 0;
            //MessageBox.Show(caractere.Length.ToString());

            foreach (var x in caractere)
                {
                    //System.Diagnostics.Debug.WriteLine("CARACTER {0}: {1}",Cont, x.ToString() + Environment.NewLine);
                    //aux[Cont] = x.ToString();
                    
                    if (x.ToString() == "REAL")
                    {
                        aux[Cont] = x.ToString();
                        System.Diagnostics.Debug.WriteLine("REAL EM {0}: {1}", Cont, aux[Cont] + Environment.NewLine);
                    }

                    else if (x.ToString() == "ID")
                    {
                        aux[Cont] = x.ToString();
                        System.Diagnostics.Debug.WriteLine("IDNT EM {0}: {1}", Cont, aux[Cont] + Environment.NewLine);
                    }
                    else if ( x.ToString() == "STR")
                    {
                        aux[Cont] = x.ToString();
                        System.Diagnostics.Debug.WriteLine("STRI EM {0}: {1}", Cont, aux[Cont] + Environment.NewLine);
                    }
                    else if (x.ToString() == String.Empty)
                    {
                        aux[Cont] = " ";
                        System.Diagnostics.Debug.WriteLine("VAZI EM {0}: {1}", Cont, aux[Cont] + Environment.NewLine);
                    }
                    else if( Regex.IsMatch(x.ToString(), pattern_numeros))
                    {
                        aux[Cont] = x.ToString();
                        System.Diagnostics.Debug.WriteLine("NUM EM {0}: {1}, com {2}", Cont, aux[Cont],x.ToString() + Environment.NewLine);

                    }
                    else
                    {
                        aux[Cont] = " ";
                        System.Diagnostics.Debug.WriteLine("DIFERENTE EM {0}: {1}", Cont, x.ToString() + Environment.NewLine);
                    }

                Cont++;
                }

            
            instrucao = String.Concat(aux);

            /*foreach(var x in aux)
            instrucao = String.Copy(x);*/

                    return instrucao;
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
            //instructionLine = Regex.Replace(instructionLine, String.Format(@"(?i)/d"), " digito ", RegexOptions.None);

            return instructionLine;
        }

        //Não funciona ainda
        /*
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
            }
            //return instrucao;
        }*/

        //Usado para verificar pontos simples e duplo e comentarios
        public string verifica_Pontos_e_Comentarios(string instrucao)
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
                textBox_Tokens.Text = instructionLine = verifica_Pontos_e_Comentarios(instructionLine);
                textBox_Tokens.Text = converte_caracteres();
                textBox_Tokens.Text = converte_palavras();
                textBox_Tokens.Text = detecta_erros_lexicos(instructionLine);
                //textBox_Tokens.Text = Remove_Erro_Lexico(textBox_Tokens.Text);

            }
            catch
            {
                MessageBox.Show("Erro ao fazer a conversão de tokens! ");
            }

            
        }

        //Remove erros léxicos pelo método pânico
        public static string Remove_Erro_Lexico(string instrucao)
        {
            string ret;
            string pattern = @"(?i)[áéíóúàèìòùâêîôûãõç¬ºª~°§#@¨&ⁿΦ•▬|\!¹²³£¢ \""/]";
            string replacement = "";
            Regex rgx = new Regex(pattern);

            try
            {
                ret = rgx.Replace(instrucao, replacement); //Remove os caracteres indevidos

                

                return ret;
            }
            catch
            {
                return ret = "Erro ao remover erros léxicos";
            }

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
