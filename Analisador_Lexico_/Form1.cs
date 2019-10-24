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
using System.Windows;


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

        bool[] flag_pertence_gramatica;

        string[] identificadores = new string[50];
        string[] reais = new string[50];
        string[] conjunto_de_strings = new string[50];
        string[] conjunto_de_integers = new string[50];
        string[] conjunto_erros = new string[1000];

        string[] lines = new string[100];

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
            instrucao = instrucao.Replace("\n", "º");
            instrucao = instrucao.Replace(" ", "ª");
            var caractere = instrucao.Split();
            int Cont = 0;
            instrucao_final = caractere.ToArray();                

            string pattern_identificadores = @"['][_a-zA-Z][_a-zA-Z]*[']";//Match de identificadores
            string pattern_numeros = @"['][0-9]+[']"; //Identifica numeros inteiros
            string pattern_real = @"[']?[0-9]*\.?[0-9]+[']"; //Identifica numeros float OK
 
            string pattern_string = @"[\!][_ªa-zA-Z0-9\s]*[\!]";//Identifica strings OK PAREI AQIII, ELE IDENTIFICA

            string wrong_pattern_numbers_with_quotes = @"\""([\d\.]+)\""";
            string wring_pattern_single_word_quotes = ("\"[\\d\\.]\"");
            string wrong_pattern_numbers = @"([\d\.]+)";
            string wrong_pattern_numbers_comma = @"([\d\,]+)";

            Regex rgx_id = new Regex(pattern_identificadores);
            Match match_identificadores = rgx_id.Match(instrucao);

            Regex rgx_num = new Regex(pattern_numeros);
            Match match_numeros = rgx_num.Match(instrucao);

            Regex rgx_real = new Regex(pattern_real);
            Match match_real = rgx_real.Match(instrucao);

            Regex rgx_string = new Regex(pattern_string, RegexOptions.IgnoreCase);
            Match match_string = rgx_string.Match(instrucao);

            flag_pertence_gramatica = new bool[instrucao.Length];

            /*
            Regex aNum = new Regex("[a-zA-Z0-9\\s]");
            Match match_strings = aNum.Match(instrucao);*/
            Cont = 0;


            for(int a = 0; a < identificadores.Length-1; a++)
            {
                identificadores[a] = null;
                reais[a] = null;
                conjunto_de_strings[a] = null;
                conjunto_de_integers[a] = null;
            }
            for (int a = 0; a < conjunto_erros.Length - 1; a++)
                conjunto_erros[a] = null;




            Cont = 0;//Armazena os identificadores encontrados em um vetor
            while (match_identificadores.Success)
            {
                System.Diagnostics.Debug.WriteLine(match_identificadores.Value + " found IDENTIFIER at position :" + match_identificadores.Index.ToString());
                identificadores[Cont] = match_identificadores.Value;
                Cont++;
                match_identificadores = match_identificadores.NextMatch();
            }
            

            Cont = 0;//Armazenas as string encontradas em um vetor
            while (match_string.Success)
            {
                System.Diagnostics.Debug.WriteLine(match_string.Value + " found STRING at position :" + match_string.Index.ToString());
                conjunto_de_strings[Cont] = match_string.Value;
                Cont++;
                match_string = match_string.NextMatch();
            }
            //MessageBox.Show(conjunto_de_strings[0]);


            Cont = 0;//Armazenas as integers encontrados em um vetor **NÃO MUDAR A ORDEM, SER PRIMEIRO QUE INTEGER**
            while (match_real.Success)
            {
                System.Diagnostics.Debug.WriteLine(match_real.Value + " found REAL at position :" + match_real.Index.ToString());
                reais[Cont] = match_real.Value;
                Cont++;
                match_real = match_real.NextMatch();
            }
            //MessageBox.Show(reais[0]);

            Cont = 0;//Armazenas as integers encontrados em um vetor **NÃO MUDAR A ORDEM, SER PRIMEIRO QUE REAL**
            while (match_numeros.Success)
            {
                System.Diagnostics.Debug.WriteLine(match_numeros.Value + " found INTEGER at position :" + match_numeros.Index.ToString());
                conjunto_de_integers[Cont] = match_numeros.Value;
                Cont++;
                match_numeros = match_numeros.NextMatch();
            }
            //MessageBox.Show(conjunto_de_integers[1]);

            #region detecta_identificadores
            Cont = 0;
            for (int i = 0; i < identificadores.Length - 1; i++)
            {
                if (identificadores[i] == null | identificadores[i] == String.Empty | identificadores[i] == @"\s+")
                {
                    //MessageBox.Show("vazio");
                }
                else
                {
                    instrucao = Regex.Replace(instrucao, pattern_identificadores, " { 16, identificador, aast }", RegexOptions.None);
                }   

            }

           
            #endregion

            #region detecta_strings
            Cont = 0;
            for (int i = 0; i < conjunto_de_strings.Length - 1; i++)
            {
                if (conjunto_de_strings[i] == null | conjunto_de_strings[i] == String.Empty | conjunto_de_strings[i] == @"\s+")
                {
                    //MessageBox.Show("vazio");
                }
                else
                {
                    instrucao = Regex.Replace(instrucao, pattern_string, " { 38, nomestring, str }", RegexOptions.None);
                }

            }


            #endregion

            
            #region detecta_reais
            Cont = 0;
            for (int i = 0; i < reais.Length - 1; i++)
            {
                if (reais[i] == null | reais[i] == String.Empty | reais[i] == @"\s+")
                {
                    //MessageBox.Show("vazio");
                }
                else
                {
                    instrucao = Regex.Replace(instrucao, pattern_real, " { 36, numreal, numr }", RegexOptions.None);
                }

            }

            

            #endregion
            

            #region detecta_integer
            Cont = 0;
            for (int i = 0; i < conjunto_de_integers.Length - 1; i++)
            {
                if (conjunto_de_integers[i] == null | conjunto_de_integers[i] == String.Empty | conjunto_de_integers[i] == @"\s+")
                {
                    //MessageBox.Show("vazio");
                }
                else
                {
                    instrucao = Regex.Replace(instrucao, pattern_numeros, " { 36, numinteger, intnum }", RegexOptions.None);
                }

            }

            #endregion

            var x = instrucao.Split();
            Cont = 0;
            int p, q, r, s;
            p = q = r = s = 0;
            foreach (var a in x)
            {
                if (a.ToString() == "aast")
                {
                    x[Cont] = identificadores[p];
                    p++;
                    //MessageBox.Show(x[Cont]);
                }
                else if (a.ToString() == "str")
                {
                    x[Cont] = conjunto_de_strings[q];
                    q++;
                }
                else if (a.ToString() == "numr")
                {
                    x[Cont] = reais[r];
                    r++;
                }


                else if (a.ToString() == "intnum")
                {
                    x[Cont] = conjunto_de_integers[s];
                    s++;
                }
                else
                {
                    x[Cont] = a;
                }
                System.Diagnostics.Debug.WriteLine("Primeiro print: "+ x[Cont]);
                Cont++;
            }



            string pattern_erro = @"{(.*?)\}";
            Regex rgx_erro = new Regex(pattern_erro, RegexOptions.ExplicitCapture);
            Match match_erro = rgx_erro.Match(instrucao);
            Cont = 0;

            instrucao = null;
            foreach (var myscore in x)
            {
                instrucao += myscore.ToString();
                System.Diagnostics.Debug.WriteLine("AQUI: "+myscore);
            }

            instrucao = instrucao.Replace("º", Environment.NewLine);
            instrucao = instrucao.Replace("ª", " ");

            x = instrucao.Split();

            foreach (var myscore in x)
            {
                System.Diagnostics.Debug.WriteLine("Vetor txt: "+myscore);
            }

            string pattern = @"(?i)[áéíóúàèìòùâêîôûãõç¬ºª~°§#@¨&ⁿΦ•▬\|¹²³£¢\""/]";

            Cont = 0;
            foreach (var myscore in x)
            {
                System.Diagnostics.Debug.WriteLine("Vetor Final {0}: {1}", Cont, myscore);

                textBox_Tokens.Text += myscore + Environment.NewLine;
                /* if (Regex.IsMatch(myscore.ToString(), pattern))
                 {
                     MessageBox.Show("Verifique a sentença: " + myscore.ToString(), "Erro Léxico Detectado");

                 }*/
                if (!Regex.IsMatch(myscore.ToString(), pattern_erro) && myscore.ToString() != " " && myscore.ToString() != String.Empty)
                {
                    MessageBox.Show("Verifique a sentença: " + myscore.ToString(), "Erro Léxico Detectado");
                }

                Cont++;
            }



            return instrucao;
        }


        private void armazena_linhas()
        {
           // MessageBox.Show("A");
            if(textBox_Tokens.Lines.Length > 0)
            {
                //MessageBox.Show("AB");
                for (int i = 0; i  < textBox_Tokens.Lines.Length - 1; i++)
                {
                    //MessageBox.Show("B");
                    lines[i] = textBox_Tokens.Lines[i];
                    System.Diagnostics.Debug.WriteLine("Linha {0}: {1}", i, lines[i] + Environment.NewLine);

                }
            }

        }


        //Transforma os operadores e simbolos de pontuação em tokens
        private string converte_caracteres()
        {
            instructionLine = Regex.Replace(instructionLine, String.Format(@"{0}", ","), "{46,','} ", RegexOptions.None);
            instructionLine = Regex.Replace(instructionLine, @"\s\.\.\s", " {44,ponto duplo} ", RegexOptions.ExplicitCapture);
            instructionLine = Regex.Replace(instructionLine, @"\s\.", " {45,ponto} ", RegexOptions.ExplicitCapture);
            instructionLine = Regex.Replace(instructionLine, String.Format(@"{0}", "î"), "  {17,î} ", RegexOptions.None);
            instructionLine = Regex.Replace(instructionLine, String.Format(@"{0}", "<>"), " {32,<>} ", RegexOptions.None);//Não colocar em ordem, dá errado
            instructionLine = Regex.Replace(instructionLine, String.Format(@"{0}", ">="), " {29,>=} ", RegexOptions.None);//Não colocar em ordem, dá errado
            instructionLine = Regex.Replace(instructionLine, String.Format(@"{0}", "{="), " {33,{=} ", RegexOptions.None);//Não colocar em ordem, dá errado
            instructionLine = Regex.Replace(instructionLine, String.Format(@"{0}", ">"), " {30,>} ", RegexOptions.None);
            instructionLine = Regex.Replace(instructionLine, String.Format(@"{0}", "="), " {31,=} ", RegexOptions.None);
            instructionLine = Regex.Replace(instructionLine, String.Format(@"{0}", "<"), " {34,<} ", RegexOptions.None);
            instructionLine = Regex.Replace(instructionLine, String.Format(@"(?i)[+]"), " {35,+} ", RegexOptions.None);// + dá problema assim
            instructionLine = Regex.Replace(instructionLine, String.Format(@"(?i)[\]]"), " {39,]} ", RegexOptions.None);
            instructionLine = Regex.Replace(instructionLine, String.Format(@"(?i)[\[]"), " {40,[} ", RegexOptions.None);
            instructionLine = Regex.Replace(instructionLine, String.Format(@"{0}", ";"), " {41,;} ", RegexOptions.None);
            instructionLine = Regex.Replace(instructionLine, String.Format(@"{0}", ":"), " {42,:} ", RegexOptions.None);
            instructionLine = Regex.Replace(instructionLine, String.Format(@"{0}", "/"), " {43,/} ", RegexOptions.None);
            instructionLine = Regex.Replace(instructionLine, String.Format(@"(?i)[*]"), " {47,*} ", RegexOptions.None);
            instructionLine = Regex.Replace(instructionLine, String.Format(@"(?i)[)]"), " {48,)} ", RegexOptions.None);
            instructionLine = Regex.Replace(instructionLine, String.Format(@"(?i)[(]"), " {49,(} ", RegexOptions.None);
            instructionLine = Regex.Replace(instructionLine, String.Format(@"(?i)[$]"), " {50,$} ", RegexOptions.None);
            instructionLine = Regex.Replace(instructionLine, String.Format(@"(?i)[-]"), " {51,-} ", RegexOptions.None);

            //instructionLine = Regex.Replace(instructionLine, String.Format(@"(?i)/d"), " digito ", RegexOptions.None);

            return instructionLine;
        }
        
        //NÂO SENDO USADO. Usado para verificar pontos simples e duplo e comentarios
        public string verifica_Pontos(string instrucao)
        {
            var caractere = textBox_Tokens.Text;
            string[] aux = new string[caractere.Length];
            instrucao = "";
            int i = 0;

            foreach (var x in caractere)
            {
                    if (x.ToString() == ".")
                    {
                        //MessageBox.Show("PONTO");
                        aux[i] = " {45, .} ";

                    }
                    else if (x.ToString() == "..")
                    {
                        //MessageBox.Show("DOIS PONTO");
                        aux[i] = " {44,..} ";
                        //instructionLine = instructionLine.Replace(x, " 44REP ");
                    }
                    else
                    {
                        aux[i] = x.ToString();
                    }

                instrucao += aux[i] + " ";
                i++;
            }

          
            i = 0;
            return instrucao;
        }

        //Remove comentarios de linha e em bloco
        private void remove_comentarios()
        {
            var blockComments = @"/\*(.*?)\*/";
            var lineComments = @"//(.*?)\r?\n";
            var strings = @"""((\\[^\n]|[^""\n])*)""";
            var verbatimStrings = @"@(""[^""]*"")+";

            string noComments = Regex.Replace(textBox_Input.Text,
                                blockComments + "|" + lineComments + "|" + strings + "|" + verbatimStrings,
                             me => {
                        if (me.Value.StartsWith("/*") || me.Value.StartsWith("//"))
                            return me.Value.StartsWith("//") ? Environment.NewLine : "";
        // Keep the literal strings
        return me.Value;
        },
            RegexOptions.Singleline);

            textBox_Tokens.Text = noComments;
            armazena_linhas();

        }


        //Transforma as constantes em tokens
        private string converte_palavras()
        {
            instructionLine = Regex.Replace(instructionLine, String.Format(@"\b{0}\b", "write"), " {0,write}" , RegexOptions.None);
            instructionLine = Regex.Replace(instructionLine, String.Format(@"\b{0}\b", "while"), " {1,while} ", RegexOptions.None);
            instructionLine = Regex.Replace(instructionLine, String.Format(@"\b{0}\b", "until"), " {2,until} ", RegexOptions.None);
            instructionLine = Regex.Replace(instructionLine, String.Format(@"\b{0}\b", "to"), " {3,to} ", RegexOptions.None);
            instructionLine = Regex.Replace(instructionLine, String.Format(@"\b{0}\b", "then"), " {4,then} ", RegexOptions.None);
            instructionLine = Regex.Replace(instructionLine, String.Format(@"\b{0}\b", "string"), " {5,string} ", RegexOptions.None);
            instructionLine = Regex.Replace(instructionLine, String.Format(@"\b{0}\b", "repeat"), " {6,repeat} ", RegexOptions.None);
            instructionLine = Regex.Replace(instructionLine, String.Format(@"\b{0}\b", "real"), " {7,real} ", RegexOptions.None);
            instructionLine = Regex.Replace(instructionLine, String.Format(@"\b{0}\b", "read"), " {8,read} ", RegexOptions.None);
            instructionLine = Regex.Replace(instructionLine, String.Format(@"\b{0}\b", "program"), " {9,program} ", RegexOptions.None);
            instructionLine = Regex.Replace(instructionLine, String.Format(@"\b{0}\b", "procedure"), " {10,procedure} ", RegexOptions.None);
            instructionLine = Regex.Replace(instructionLine, String.Format(@"\b{0}\b", "or"), " {11,or} ", RegexOptions.None);
            instructionLine = Regex.Replace(instructionLine, String.Format(@"\b{0}\b", "of"), " {12,of} ", RegexOptions.None);
            instructionLine = Regex.Replace(instructionLine, String.Format(@"\b{0}\b", "literal"), " {13,literal} ", RegexOptions.None);
            instructionLine = Regex.Replace(instructionLine, String.Format(@"\b{0}\b", "integer"), " {14,integer} ", RegexOptions.None);
            instructionLine = Regex.Replace(instructionLine, String.Format(@"\b{0}\b", "if"), " {15,if} ", RegexOptions.None);
            instructionLine = Regex.Replace(instructionLine, String.Format(@"\b{0}\b", "identificador"), " {16,identificador} ", RegexOptions.None);
            instructionLine = Regex.Replace(instructionLine, String.Format(@"\b{0}\b", "for"), " {18,for} ", RegexOptions.None);
            instructionLine = Regex.Replace(instructionLine, String.Format(@"\b{0}\b", "end"), " {19,end} ", RegexOptions.None);
            instructionLine = Regex.Replace(instructionLine, String.Format(@"\b{0}\b", "else"), "{20,else} ", RegexOptions.None);
            instructionLine = Regex.Replace(instructionLine, String.Format(@"\b{0}\b", "do"), " {21,do} ", RegexOptions.None);
            instructionLine = Regex.Replace(instructionLine, String.Format(@"\b{0}\b", "declaravariaveis"), " {22,declaravariaveis} ", RegexOptions.None);
            instructionLine = Regex.Replace(instructionLine, String.Format(@"\b{0}\b", "const"), " {23,const} ", RegexOptions.None);
            instructionLine = Regex.Replace(instructionLine, String.Format(@"\b{0}\b", "char"), " {24,char} ", RegexOptions.None);
            instructionLine = Regex.Replace(instructionLine, String.Format(@"\b{0}\b", "chamaprocedure"), " {25,chamaprocedure} ", RegexOptions.None);
            instructionLine = Regex.Replace(instructionLine, String.Format(@"\b{0}\b", "begin"), " {26,begin} ", RegexOptions.None);
            instructionLine = Regex.Replace(instructionLine, String.Format(@"\b{0}\b", "array"), " {27,array} ", RegexOptions.None);
            instructionLine = Regex.Replace(instructionLine, String.Format(@"\b{0}\b", "and"), " {28,and} ", RegexOptions.None);
            instructionLine = Regex.Replace(instructionLine, String.Format(@"\b{0}\b", "numreal"), " {36,numreal} ", RegexOptions.None);
            instructionLine = Regex.Replace(instructionLine, String.Format(@"\b{0}\b", "numinteiro"), " {37,numinteiro} ", RegexOptions.None);
            instructionLine = Regex.Replace(instructionLine, String.Format(@"\b{0}\b", "nomestring"), " {38,nomestring} ", RegexOptions.None);
            instructionLine = Regex.Replace(instructionLine, String.Format(@"\b{0}\b", "real"), "{7,real} ", RegexOptions.None);

            return instructionLine;
        }


        private void bt_Analize_Click(object sender, EventArgs e)
        {
            try
            {
                //textBox_Input.Text = instructionLine = Regex.Replace(instructionLine, @"^\s*$(\n|\r|\r\n)", "", RegexOptions.Multiline);//Remove linha vazia
                remove_comentarios();// Remove comentarios e identifica pontos simples e duplos
                instructionLine = textBox_Tokens.Text;
                textBox_Tokens.Text = converte_caracteres();
                textBox_Tokens.Text = converte_palavras();
                textBox_Tokens.Text = "";
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
            string replacement = " ";
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

        private void visualizarAutômatoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("https://drive.google.com/file/d/1wfmopC3mEFdXVNan-lZdaoaMBhbQ7gfW/view?usp=sharing");
            }
            catch
            {
                MessageBox.Show("https://drive.google.com/file/d/1wfmopC3mEFdXVNan-lZdaoaMBhbQ7gfW/view?usp=sharing","Erro ao acessar Link, tente manualmente.");
            }
            
        }

        private void removeComentarioTesteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            remove_comentarios();

        }


    }
}
