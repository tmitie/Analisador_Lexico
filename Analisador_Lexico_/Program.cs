using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Analisador_Lexico_
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());


            /*
            Lexico lexico = new Lexico();
            Sintatico sintatico = new Sintatico();
            Semantico semantico = new Semantico();

            LineNumberReader @in = new LineNumberReader(new StreamReader(System.in));
            string line = @in.readLine();

            lexico.Input = line;

            try
            {
                sintatico.parse(lexico, semantico);
                Console.WriteLine(" = ");
                Console.WriteLine();
                //System.out.println(" = ");
                System.out.println(tran.getResult());
            }
            catch (LexicalError e)
            {
                //e.printStackTrace();
                Console.WriteLine(e.StackTrace);
            }
            catch (SyntaticError e)
            {
                //e.printStackTrace();
                Console.WriteLine(e.StackTrace);
            }
            catch (SemanticError e)
            {
                //e.printStackTrace();
                Console.WriteLine(e.StackTrace);
            }*/
        }
    }
    }

