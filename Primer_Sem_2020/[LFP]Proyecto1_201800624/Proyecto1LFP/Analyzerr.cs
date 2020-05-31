using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto1LFP
{
    class Analyzerr
    {
        private static LinkedList<Token> analyzedText;
        private static List<Mistake> mistakesList;
        private int state;
        private int line;
        private int column;
        private string auxLexc;
        private string aux;
        private static int numberTokens;
        public static int mistakes;

        public LinkedList<Token> scanner(string text)
        {
            
            text = text + "#";
            analyzedText = new LinkedList<Token>();
            mistakesList = new List<Mistake>();
            state = 0;
            line = 1;
            column = 0;
            aux = "";
            mistakes = 0;
            auxLexc = "";
            Char c;

            for (int x = 0; x <= text.Length - 1; x++)
            {
                c = text.ElementAt(x);
                switch (state)
                {
                    case 0:
                        {
                            if (Char.IsLetter(c))
                            {
                                state = 1;
                                x -= 1;
                            }
                            else if (c.CompareTo('"') == 0)
                            {
                                auxLexc += c;
                                state = 2;
                                column++;
                            }
                            else if (Char.IsDigit(c))
                            {
                                auxLexc += c;
                                state = 3;
                                column++;
                            }
                            else if (c.CompareTo(':') == 0)
                            {
                                auxLexc += c;
                                addToken(Token.Type.SIGNO_DOSPUNTOS);
                                column++;
                            }
                            else if (c.CompareTo(';') == 0)
                            {
                                auxLexc += c;
                                addToken(Token.Type.SIGNO_PUNTOYCOMA);
                                column++;
                            }
                            else if (c.CompareTo('{') == 0)
                            {
                                auxLexc += c;
                                addToken(Token.Type.LLAVE_IZQ);
                                column++;
                            }
                            else if (c.CompareTo('}') == 0)
                            {
                                auxLexc += c;
                                addToken(Token.Type.LLAVE_DER);
                                column++;
                            }
                            else if (c.CompareTo('%') == 0)
                            {
                                auxLexc += c;
                                addToken(Token.Type.SIGNO_PORCENTAJE);
                                column++;
                            }
                            else if (c.CompareTo(' ') == 0)
                            {
                                state = 0;
                                column++;
                            }
                            else if (c.CompareTo('\n') == 0)
                            {
                                column = 0;
                                state = 0;
                                line++;
                            }
                            else
                            {
                                if (c.CompareTo('#') == 0 && x == text.Length - 1)
                                {
                                    Console.WriteLine("Hemos concluido el análisis léxico con exito");
                                }
                                else
                                {
                                    
                                    Console.WriteLine("Error léxico con: " + c);
                                    column += 1; ;
                                    mistakes++;
                                    mistakesList.Add(new Mistake(line, c.ToString(), column, mistakes));
                                 
                                    state = 0;
                                   
                                }
                            }
                            break;
                        }
                    case 1:
                        {
                            if (c.CompareTo('G') == 0)
                            {
                                state = 4;
                                aux += c;
                                auxLexc += c;
                                column++;
                            }
                            else if (c.CompareTo('N') == 0)
                            {
                                state = 5;
                                aux += c;
                                auxLexc += c;
                                column++;
                            }
                            else if (c.CompareTo('C') == 0)
                            {
                                state = 6;
                                aux += c;
                                auxLexc += c;
                                column++;
                            }
                            else if (c.CompareTo('P') == 0)
                            {
                                state = 7;
                                aux += c;
                                auxLexc += c;
                                column++;
                            }
                            else if (c.CompareTo('S') == 0)
                            {
                                state = 8;
                                aux += c;
                                auxLexc += c;
                                column++;
                            }
                            else if (c.CompareTo('B') == 0)
                            {
                                state = 9;
                                aux += c;
                                auxLexc += c;
                                column++;
                            }
                            else
                            {
                             
                                Console.WriteLine("Error con lexico con " + c);
                                auxLexc = "";
                                aux = "";
                                state = 0;
                                column++;
                                mistakes++;
                                mistakesList.Add(new Mistake(line, c.ToString(), column, mistakes));
                                
                            }
                            break;
                        }
                    case 2:
                        {
                            if (c.CompareTo('"') == 0)
                            {
                                auxLexc += c;
                                addToken(Token.Type.CADENA_CARACTERES);
                                column++;
                            }
                            else if (c.CompareTo('\n') == 0)
                            {
                                line++;
                                column = 0;
                                state = 2;
                            }
                            else
                            {
                                auxLexc += c;
                                column++;
                                state = 2;
                            }
                            break;
                        }
                    case 3:
                        {
                            if (Char.IsDigit(c))
                            {
                                state = 3;
                                auxLexc += c;
                                column++;
                            }
                            else
                            {
                                addToken(Token.Type.NUMERO_ENTERO);
                                x -= 1;
                            }
                            break;
                        }
                    case 4:
                        {
                            if (c.CompareTo('r') == 0)
                            {
                                aux += c;
                                column++;
                                if (aux.Equals("Gr"))
                                {
                                    auxLexc += c;
                                    state = 4;
                                }
                                else
                                {
                                    Console.WriteLine("Error con: " + auxLexc);
                                    mistakes++;
                                    mistakesList.Add(new Mistake(line, auxLexc, column, mistakes));
                                    auxLexc = "";
                                    aux = "";
                                    x -= 1;
                                    state = 0;                                 
                                }
                            }
                            else if (c.CompareTo('a') == 0)
                            {
                                aux += c;
                                column++;
                                if (aux.Equals("Gra"))
                                {
                                    auxLexc += c;
                                    state = 4;
                                }
                                else if (aux.Equals("Grafica"))
                                {
                                    auxLexc += c;
                                    addToken(Token.Type.RESERVADA_GRAFICA);
                                    column++;
                                }
                                else
                                {
                                    Console.WriteLine("Error con: " + auxLexc);
                                    mistakes++;
                                    mistakesList.Add(new Mistake(line, auxLexc, column, mistakes));
                                    auxLexc = "";
                                    aux = "";
                                    state = 0;
                                    x -= 1;
                                }
                            }
                            else if (c.CompareTo('f') == 0)
                            {
                                aux += c;
                                column++;
                                if (aux.Equals("Graf"))
                                {
                                    auxLexc += c;
                                    state = 4;
                                }
                                else
                                {
                                    Console.WriteLine("Error con: " + auxLexc);
                                    mistakes++;
                                    mistakesList.Add(new Mistake(line, auxLexc, column, mistakes));
                                    auxLexc = "";
                                    aux = "";
                                    x -= 1;
                                    state = 0;                             
                                }
                            }
                            else if (c.CompareTo('i') == 0)
                            {
                                aux += c;
                                column++;
                                if (aux.Equals("Grafi"))
                                {
                                    auxLexc += c;
                                    state = 4;
                                }
                                else
                                {
                                    Console.WriteLine("Error con: " + auxLexc);
                                    mistakes++;
                                    mistakesList.Add(new Mistake(line, auxLexc, column, mistakes));
                                    auxLexc = "";
                                    aux = "";
                                    state = 0;
                                    x -= 1;                                 
                                }
                            }
                            else if (c.CompareTo('c') == 0)
                            {
                                aux += c;
                                column++;
                                if (aux.Equals("Grafic"))
                                {
                                    auxLexc += c;
                                    state = 4;
                                }
                                else
                                {
                                    Console.WriteLine("Error con: " + auxLexc);
                                    mistakes++;
                                    mistakesList.Add(new Mistake(line, auxLexc, column, mistakes));
                                    auxLexc = "";
                                    aux = "";
                                    state = 0;
                                    x -= 1;                       
                                }
                            }
                            else
                            {
                                Console.WriteLine("Error con: " + auxLexc);
                                mistakes++;
                                mistakesList.Add(new Mistake(line, auxLexc, column, mistakes));
                                auxLexc = "";
                                aux = "";
                                x -= 1;
                                state = 0;      
                            }
                            break;
                        }
                    case 5:
                        {
                            if (c.CompareTo('o') == 0)
                            {
                                aux += c;
                                column++;
                                if (aux.Equals("No"))
                                {
                                    auxLexc += c;
                                    state = 5;
                                }
                                else
                                {
                                    Console.WriteLine("Error léxico con: " + auxLexc);
                                    state = 0;
                                    x -= 1;
                                    mistakes++;
                                    mistakesList.Add(new Mistake(line, auxLexc, column, mistakes));
                                    auxLexc = "";
                                    aux = "";

                                }
                            }
                            else if (c.CompareTo('m') == 0)
                            {
                                aux += c;
                                column++;
                                if (aux.Equals("Nom"))
                                {
                                    auxLexc += c;
                                    state = 5;
                                }
                                else
                                {
                                    Console.WriteLine("Error léxico con: " + auxLexc);
                                    state = 0;
                                    x -= 1;
                                    mistakes++;
                                    mistakesList.Add(new Mistake(line, auxLexc, column, mistakes));
                                    auxLexc = "";
                                    aux = "";
                                }
                            }
                            else if (c.CompareTo('b') == 0)
                            {
                                aux += c;
                                column++;
                                if (aux.Equals("Nomb"))
                                {
                                    auxLexc += c;
                                    state = 5;
                                }
                                else
                                {
                                    Console.WriteLine("Error léxico con: " + auxLexc);
                                    state = 0;
                                    x -= 1;
                                    mistakes++;
                                    mistakesList.Add(new Mistake(line, auxLexc, column, mistakes));
                                    auxLexc = "";
                                    aux = "";
                                }
                            }
                            else if (c.CompareTo('r') == 0)
                            {
                                aux += c;
                                column++;
                                if (aux.Equals("Nombr"))
                                {
                                    auxLexc += c;
                                    state = 5;
                                }
                                else
                                {
                                    Console.WriteLine("Error léxico con: " + auxLexc);
                                    state = 0;
                                    x -= 1;
                                    mistakes++;
                                    mistakesList.Add(new Mistake(line, auxLexc, column, mistakes));
                                    auxLexc = "";
                                    aux = "";
                                }
                            }
                            else if (c.CompareTo('e') == 0)
                            {
                                aux += c;
                                column++;
                                if (aux.Equals("Nombre"))
                                {
                                    auxLexc += c;
                                    addToken(Token.Type.RESERVADA_NOMBRE);
                                    column++;
                                }
                                else
                                {
                                    Console.WriteLine("Error léxico con: " + auxLexc);
                                    state = 0;
                                    x -= 1;
                                    mistakes++;
                                    mistakesList.Add(new Mistake(line, auxLexc, column, mistakes));
                                    auxLexc = "";
                                    aux = "";
                                }
                            }
                            else
                            {
                                Console.WriteLine("Error con: " + auxLexc);
                                mistakes++;
                                mistakesList.Add(new Mistake(line, auxLexc, column, mistakes));
                                auxLexc = "";
                                aux = "";
                                x -= 1;
                                state = 0;    
                            }
                            break;
                        }
                    case 6:
                        {
                            if (c.CompareTo('o') == 0)
                            {
                                aux += c;
                                column++;
                                if (aux.Equals("Co"))
                                {
                                    auxLexc += c;
                                    state = 6;
                                }
                                else
                                {
                                    Console.WriteLine("Error léxico con: " + auxLexc);
                                    state = 0;
                                    x -= 1;
                                    mistakes++;
                                    mistakesList.Add(new Mistake(line, auxLexc, column, mistakes));
                                    auxLexc = "";
                                    aux = "";
                                }
                            }
                            else if (c.CompareTo('n') == 0)
                            {
                                aux += c;
                                column++;
                                if (aux.Equals("Con"))
                                {
                                    auxLexc += c;
                                    state = 6;
                                }
                                else if (aux.Equals("Contin"))
                                {
                                    auxLexc += c;
                                    state = 6;
                                }
                                else if (aux.Equals("Continen"))
                                {
                                    auxLexc += c;
                                    state = 6;
                                }
                                else
                                {
                                    Console.WriteLine("Error léxico con: " + auxLexc);
                                    state = 0;
                                    x -= 1;
                                    mistakes++;
                                    mistakesList.Add(new Mistake(line, auxLexc, column, mistakes));
                                    auxLexc = "";
                                    aux = "";
                                }
                            }
                            else if (c.CompareTo('t') == 0)
                            {
                                aux += c;
                                column++;
                                if (aux.Equals("Cont"))
                                {
                                    auxLexc += c;
                                    state = 6;
                                }
                                else if (aux.Equals("Continent"))
                                {
                                    auxLexc += c;
                                    state = 6;
                                }
                                else
                                {
                                    Console.WriteLine("Error léxico con: " + auxLexc);
                                    state = 0;
                                    x -= 1;
                                    mistakes++;
                                    mistakesList.Add(new Mistake(line, auxLexc, column, mistakes));
                                    auxLexc = "";
                                    aux = "";
                                }
                            }
                            else if (c.CompareTo('i') == 0)
                            {
                                aux += c;
                                column++;
                                if (aux.Equals("Conti"))
                                {
                                    auxLexc += c;
                                    state = 6;
                                }
                                else
                                {
                                    Console.WriteLine("Error léxico con: " + auxLexc);
                                    state = 0;
                                    x -= 1;
                                    mistakes++;
                                    mistakesList.Add(new Mistake(line, auxLexc, column, mistakes));
                                    auxLexc = "";
                                    aux = "";
                                }
                            }
                            else if (c.CompareTo('e') == 0)
                            {
                                aux += c;
                                column++;
                                if (aux.Equals("Contine"))
                                {
                                    auxLexc += c;
                                    state = 6;
                                }
                                else if (aux.Equals("Continente"))
                                {
                                    auxLexc += c;
                                    addToken(Token.Type.RESERVADA_CONTINENTE);
                                    column++;
                                }
                                else
                                {
                                    Console.WriteLine("Error léxico con: " + auxLexc);
                                    state = 0;
                                    x -= 1;
                                    mistakes++;
                                    mistakesList.Add(new Mistake(line, auxLexc, column, mistakes));
                                    auxLexc = "";
                                    aux = "";
                                }
                            }
                            else
                            {
                                Console.WriteLine("Error con: " + auxLexc);
                                mistakes++;
                                mistakesList.Add(new Mistake(line, auxLexc, column, mistakes));
                                auxLexc = "";
                                aux = "";
                                x -= 1;
                                state = 0;
                            }

                            break;
                        }
                    case 7:
                        {
                            if (c.CompareTo('a') == 0)
                            {
                                aux += c;
                                auxLexc += c;
                                state = 10;
                                column++;
                            }
                            else if (c.CompareTo('o') == 0)
                            {
                                aux += c;
                                auxLexc += c;
                                state = 11;
                                column++;
                            }
                            else
                            {
                                Console.WriteLine("Error léxico con:" + auxLexc);
                                mistakes++;
                                mistakesList.Add(new Mistake(line, auxLexc, column, mistakes));
                                aux = "";
                                auxLexc = "";
                                state = 0;
                                x -= 1;
                            }
                            break;
                        }
                    case 8:
                        {
                            if (c.CompareTo('a') == 0)
                            {
                                aux += c;
                                column++;
                                if (aux.Equals("Sa"))
                                {
                                    auxLexc += c;
                                    state = 8;
                                }
                                else if (aux.Equals("Satura"))
                                {
                                    auxLexc += c;
                                    state = 8;
                                }
                                else
                                {
                                    Console.WriteLine("Error léxico con: " + auxLexc);
                                    state = 0;
                                    x -= 1;
                                    mistakes++;
                                    mistakesList.Add(new Mistake(line, auxLexc, column, mistakes));
                                    auxLexc = "";
                                    aux = "";
                                }
                            }
                            else if (c.CompareTo('t') == 0)
                            {
                                aux += c;
                                column++;
                                if (aux.Equals("Sat"))
                                {
                                    auxLexc += c;
                                    state = 8;
                                }
                                else
                                {
                                    Console.WriteLine("Error léxico con: " + auxLexc);
                                    state = 0;
                                    x -= 1;
                                    mistakes++;
                                    mistakesList.Add(new Mistake(line, auxLexc, column, mistakes));
                                    auxLexc = "";
                                    aux = "";
                                }
                            }
                            else if (c.CompareTo('u') == 0)
                            {
                                aux += c;
                                column++;
                                if (aux.Equals("Satu"))
                                {
                                    auxLexc += c;
                                    state = 8;
                                }
                                else
                                {
                                    Console.WriteLine("Error léxico con: " + auxLexc);
                                    state = 0;
                                    x -= 1;
                                    mistakes++;
                                    mistakesList.Add(new Mistake(line, auxLexc, column, mistakes));
                                    auxLexc = "";
                                    aux = "";
                                }
                            }
                            else if (c.CompareTo('r') == 0)
                            {
                                aux += c;
                                column++;
                                if (aux.Equals("Satur"))
                                {
                                    auxLexc += c;
                                    state = 8;
                                }
                                else
                                {
                                    Console.WriteLine("Error léxico con: " + auxLexc);
                                    state = 0;
                                    x -= 1;
                                    mistakes++;
                                    mistakesList.Add(new Mistake(line, auxLexc, column, mistakes));
                                    auxLexc = "";
                                    aux = "";
                                }
                            }
                            else if (c.CompareTo('c') == 0)
                            {
                                aux += c;
                                column++;
                                if (aux.Equals("Saturac"))
                                {
                                    auxLexc += c;
                                    state = 8;
                                }
                                else
                                {
                                    Console.WriteLine("Error léxico con: " + auxLexc);
                                    state = 0;
                                    x -= 1;
                                    mistakes++;
                                    mistakesList.Add(new Mistake(line, auxLexc, column, mistakes));
                                    auxLexc = "";
                                    aux = "";
                                }
                            }
                            else if (c.CompareTo('i') == 0)
                            {
                                aux += c;
                                column++;
                                if (aux.Equals("Saturaci"))
                                {
                                    auxLexc += c;
                                    state = 8;
                                }
                                else
                                {
                                    Console.WriteLine("Error léxico con: " + auxLexc);
                                    state = 0;
                                    x -= 1;
                                    mistakes++;
                                    mistakesList.Add(new Mistake(line, auxLexc, column, mistakes));
                                    auxLexc = "";
                                    aux = "";
                                }
                            }
                            else if (c.CompareTo('o') == 0)
                            {
                                aux += c;
                                column++;
                                if (aux.Equals("Saturacio"))
                                {
                                    auxLexc += c;
                                    state = 8;
                                }
                                else
                                {
                                    Console.WriteLine("Error léxico con: " + auxLexc);
                                    state = 0;
                                    x -= 1;
                                    mistakes++;
                                    mistakesList.Add(new Mistake(line, auxLexc, column, mistakes));
                                    auxLexc = "";
                                    aux = "";
                                }
                            }
                            else if (c.CompareTo('n') == 0)
                            {
                                aux += c;
                                column++;
                                if (aux.Equals("Saturacion"))
                                {
                                    auxLexc += c;
                                    addToken(Token.Type.RESERVADA_SATURACION);
                                    column++;
                                }
                                else
                                {
                                    Console.WriteLine("Error léxico con: " + auxLexc);
                                    state = 0;
                                    x -= 1;
                                    mistakes++;
                                    mistakesList.Add(new Mistake(line, auxLexc, column, mistakes));
                                    auxLexc = "";
                                    aux = "";
                                }
                            }
                            else
                            {
                                Console.WriteLine("Error léxico con:" + auxLexc);
                                mistakes++;
                                mistakesList.Add(new Mistake(line, auxLexc, column, mistakes));
                                aux = "";
                                auxLexc = "";
                                state = 0;
                                x -= 1;
                            }

                            break;
                        }
                    case 9:
                        {
                            if (c.CompareTo('a') == 0)
                            {
                                aux += c;
                                column++;
                                if (aux.Equals("Ba"))
                                {
                                    auxLexc += c;
                                    state = 9;
                                }
                                else if (aux.Equals("Bandera"))
                                {
                                    auxLexc += c;
                                    addToken(Token.Type.RESERVADA_BANDERA);
                                    column++;
                                }
                                else
                                {
                                    Console.WriteLine("Error léxico con: " + auxLexc);
                                    state = 0;
                                    x -= 1;
                                    mistakes++;
                                    mistakesList.Add(new Mistake(line, auxLexc, column, mistakes));
                                    auxLexc = "";
                                    aux = "";
                                }
                            }
                            else if (c.CompareTo('n') == 0)
                            {
                                aux += c;
                                column++;
                                if (aux.Equals("Ban"))
                                {
                                    auxLexc += c;
                                    state = 9;
                                }
                                else
                                {
                                    Console.WriteLine("Error léxico con: " + auxLexc);
                                    state = 0;
                                    x -= 1;
                                    mistakes++;
                                    mistakesList.Add(new Mistake(line, auxLexc, column, mistakes));
                                    auxLexc = "";
                                    aux = "";
                                }
                            }
                            else if (c.CompareTo('d') == 0)
                            {
                                aux += c;
                                column++;
                                if (aux.Equals("Band"))
                                {
                                    auxLexc += c;
                                    state = 9;
                                }
                                else
                                {
                                    Console.WriteLine("Error léxico con: " + auxLexc);
                                    state = 0;
                                    x -= 1;
                                    mistakes++;
                                    mistakesList.Add(new Mistake(line, auxLexc, column, mistakes));
                                    auxLexc = "";
                                    aux = "";
                                }
                            }
                            else if (c.CompareTo('e') == 0)
                            {
                                aux += c;
                                column++;
                                if (aux.Equals("Bande"))
                                {
                                    auxLexc += c;
                                    state = 9;
                                }
                                else
                                {
                                    Console.WriteLine("Error léxico con: " + auxLexc);
                                    state = 0;
                                    x -= 1;
                                    mistakes++;
                                    mistakesList.Add(new Mistake(line, auxLexc, column, mistakes));
                                    auxLexc = "";
                                    aux = "";
                                }
                            }
                            else if (c.CompareTo('r') == 0)
                            {
                                aux += c;
                                column++;
                                if (aux.Equals("Bander"))
                                {
                                    auxLexc += c;
                                    state = 9;
                                }
                                else
                                {
                                    Console.WriteLine("Error léxico con: " + auxLexc);
                                    state = 0;
                                    x -= 1;
                                    mistakes++;
                                    mistakesList.Add(new Mistake(line, auxLexc, column, mistakes));
                                    auxLexc = "";
                                    aux = "";
                                }
                            }
                            else
                            {
                                Console.WriteLine("Error léxico con:" + auxLexc);
                                mistakes++;
                                mistakesList.Add(new Mistake(line, auxLexc, column, mistakes));
                                aux = "";
                                auxLexc = "";
                                state = 0;
                                x -= 1;
                            }

                            break;
                        }
                    case 10:
                        {
                            if (c.CompareTo('i') == 0)
                            {
                                aux += c;
                                column++;
                                if (aux.Equals("Pai"))
                                {
                                    auxLexc += c;
                                    state = 10;
                                }
                                else
                                {
                                    Console.WriteLine("Error léxico con: " + auxLexc);
                                    state = 0;
                                    x -= 1;
                                    mistakes++;
                                    mistakesList.Add(new Mistake(line, auxLexc, column, mistakes));
                                    auxLexc = "";
                                    aux = "";
                                }
                            }
                            else if (c.CompareTo('s') == 0)
                            {
                                aux += c;
                                column++;
                                if (aux.Equals("Pais"))
                                {
                                    auxLexc += c;
                                    addToken(Token.Type.RESERVADA_PAIS);
                                    column++;
                                }
                                else
                                {
                                    Console.WriteLine("Error lexico con: " + auxLexc);
                                    state = 0;
                                    mistakes++;
                                    mistakesList.Add(new Mistake(line, auxLexc, column, mistakes));
                                    auxLexc = "";
                                    aux = "";
                                    x -= 1;
                                }
                            }
                            else
                            {
                                Console.WriteLine("Error léxico con:" + auxLexc);
                                mistakes++;
                                mistakesList.Add(new Mistake(line, auxLexc, column, mistakes));
                                aux = "";
                                auxLexc = "";
                                state = 0;
                                x -= 1;
                            }
                            break;
                        }
                    case 11:
                        {
                            if (c.CompareTo('b') == 0)
                            {
                                aux += c;
                                column++;
                                if (aux.Equals("Pob"))
                                {
                                    auxLexc += c;
                                    state = 11;
                                }
                                else
                                {
                                    Console.WriteLine("Error léxico con: " + auxLexc);
                                    state = 0;
                                    x -= 1;
                                    mistakes++;
                                    mistakesList.Add(new Mistake(line, auxLexc, column, mistakes));
                                    auxLexc = "";
                                    aux = "";
                                }
                            }
                            else if (c.CompareTo('l') == 0)
                            {
                                aux += c;
                                column++;
                                if (aux.Equals("Pobl"))
                                {
                                    auxLexc += c;
                                    state = 11;
                                }
                                else
                                {
                                    Console.WriteLine("Error léxico con: " + auxLexc);
                                    state = 0;
                                    x -= 1;
                                    mistakes++;
                                    mistakesList.Add(new Mistake(line, auxLexc, column, mistakes));
                                    auxLexc = "";
                                    aux = "";
                                }
                            }
                            else if (c.CompareTo('a') == 0)
                            {
                                aux += c;
                                column++;
                                if (aux.Equals("Pobla"))
                                {
                                    auxLexc += c;
                                    state = 11;
                                }
                                else
                                {
                                    Console.WriteLine("Error léxico con: " + auxLexc);
                                    state = 0;
                                    x -= 1;
                                    mistakes++;
                                    mistakesList.Add(new Mistake(line, auxLexc, column, mistakes));
                                    auxLexc = "";
                                    aux = "";
                                }
                            }
                            else if (c.CompareTo('c') == 0)
                            {
                                aux += c;
                                column++;
                                if (aux.Equals("Poblac"))
                                {
                                    auxLexc += c;
                                    state = 11;
                                }
                                else
                                {
                                    Console.WriteLine("Error léxico con: " + auxLexc);
                                    mistakes++;
                                    mistakesList.Add(new Mistake(line, auxLexc, column, mistakes));
                                    state = 0;
                                    x -= 1;
                                    auxLexc = "";
                                    aux = "";
                                }
                            }
                            else if (c.CompareTo('i') == 0)
                            {
                                aux += c;
                                column++;
                                if (aux.Equals("Poblaci"))
                                {
                                    auxLexc += c;
                                    state = 11;
                                }
                                else
                                {
                                    Console.WriteLine("Error léxico con: " + auxLexc);
                                    state = 0;
                                    x -= 1;
                                    mistakes++;
                                    mistakesList.Add(new Mistake(line, auxLexc, column, mistakes));
                                    auxLexc = "";
                                    aux = "";
                                }
                            }
                            else if (c.CompareTo('o') == 0)
                            {
                                aux += c;
                                column++;
                                if (aux.Equals("Poblacio"))
                                {
                                    auxLexc += c;
                                    state = 11;
                                }
                                else
                                {
                                    Console.WriteLine("Error léxico con: " + auxLexc);
                                    state = 0;
                                    x -= 1;
                                    mistakes++;
                                    mistakesList.Add(new Mistake(line, auxLexc, column, mistakes));
                                    auxLexc = "";
                                    aux = "";
                                }
                            }
                            else if (c.CompareTo('n') == 0)
                            {
                                aux += c;
                                column++;
                                if (aux.Equals("Poblacion"))
                                {
                                    auxLexc += c;
                                    addToken(Token.Type.RESERVADA_POBLACION);
                                    column++;
                                }
                                else
                                {
                                    Console.WriteLine("Error léxico con: " + auxLexc);
                                    state = 0;
                                    x -= 1;
                                    mistakes++;
                                    mistakesList.Add(new Mistake(line, auxLexc, column, mistakes));
                                    auxLexc = "";
                                    aux = "";
                                }
                            }
                            else
                            {
                                Console.WriteLine("Error léxico con: " + auxLexc);
                                state = 0;
                                x -= 1;
                                mistakes++;
                                mistakesList.Add(new Mistake(line, auxLexc, column, mistakes));
                                auxLexc = "";
                                aux = "";
                            }
                            break;
                        }
                }
            }
            return analyzedText;
        }

        public void addToken(Token.Type type)
        {
            if(column == 0)
            {
                column++;
            }      
            analyzedText.AddLast(new Token(type, auxLexc, line, column));
            auxLexc = "";
            aux = "";
            state = 0;
            numberTokens++;
        }


        public void printTokens(LinkedList<Token> tokenList)
        {
            int accountant = 1;

            foreach (Token token in tokenList)
            {
                token.setNumber(accountant);
                Console.WriteLine("\n" + token.getType() + "<-------------------->" + token.getValue() + "<---------------------->" + token.getLine() + "<------>" + token.getNumber() + "<----->" + token.getColumn());
                accountant++;
            }

        }

        public void printMistakes(List<Mistake> list)
        {
            foreach(Mistake mistake in list)
            {
                Console.WriteLine(mistake.getLine() + "<--->" + mistake.getColumn() + "<--->" + mistake.getNumber() + "<--->" + mistake.getMistake() + "<--->" + mistake.getDescripticion());
            }
        }

        public List<Mistake> mistakeList()
        {
            return mistakesList;
        }

        public LinkedList<Token> tokenList()
        {
            return analyzedText;
        }

        public int mistakesNo()
        {
            return mistakes;
        }

        public int getNumberTokens()
        {
            return numberTokens;
        }

        public void setMistakes(int mistak)
        {
            mistakes = mistak;
        }

        public void setNumberTokens(int number)
        {
            numberTokens = number;
        }
    }
}
