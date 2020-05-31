using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace PracticaUnoLFP
{
    class analisadorLexico
    {
        public static LinkedList<Token> salida;
        public static LinkedList<Error> error = new LinkedList<Error>();
        private int estado;
        private int linea;
        private int columna;
        private string auxLexico;
        public int errores;
    

        public LinkedList<Token> scanner (string entrada)
        {
            entrada = entrada + "#";
            salida = new LinkedList<Token>();
            estado = 0;
            linea = 1;
            columna = 1;
            errores = 0;
            auxLexico = "";
         
          
            Char c;
           
            for (int i = 0; i <=entrada.Length-1; i++)
            {
                c = entrada.ElementAt(i);
                switch (estado)
                {
                    case 0:
                        {
                            if (Char.IsLetter((c)))
                            {
                                estado = 1;
                                i -= 1;
                            } else if (c.CompareTo('"') == 0)
                            {
                                auxLexico += c;
                                estado = 2;
                                columna++;

                            } else if (Char.IsDigit(c))
                            {
                                auxLexico += c;
                                estado = 3;
                                columna++;
                            } else if (c.CompareTo(':') == 0)
                            {
                                auxLexico += c;
                                agregarToken(Token.Type.SIGNO_DOSPUNTOS);
                                columna++;
                            }
                            else if (c.CompareTo(';') == 0)
                            {
                                auxLexico += c;
                                agregarToken(Token.Type.SIGNO_PUNTOYCOMA);
                                columna++;
                            }
                            else if (c.CompareTo('<') == 0)
                            {
                                auxLexico += c;
                                agregarToken(Token.Type.SIGNO_MENOR);
                                columna++;
                            }
                            else if (c.CompareTo('>') == 0)
                            {
                                auxLexico += c;
                                agregarToken(Token.Type.SIGNO_MAYOR);
                                columna++;
                            }
                            else if (c.CompareTo('{') == 0)
                            {
                                auxLexico += c;
                                agregarToken(Token.Type.LLAVE_IZQ);
                                columna++;
                            }
                            else if (c.CompareTo('}') == 0)
                            {
                                auxLexico += c;
                                agregarToken(Token.Type.LLAVE_DER);
                                columna++;
                            }
                            else if (c.CompareTo('(') == 0)
                            {
                                auxLexico += c;
                                agregarToken(Token.Type.PARENTESIS_IZQ);
                                columna++;
                            }
                            else if (c.CompareTo(')') == 0)
                            {
                                auxLexico += c;
                                agregarToken(Token.Type.PARENTESIS_DER);
                                columna++;
                            }
                            else if (c.CompareTo('[') == 0)
                            {
                                auxLexico += c;
                                agregarToken(Token.Type.CORCHETE_IZQ);
                                columna++;
                            }
                            else if (c.CompareTo(']') == 0)
                            {
                                auxLexico += c;
                                agregarToken(Token.Type.CORCHETE_DER);
                                columna++;
                            }
                            else if (c.CompareTo(' ')==0)
                            {
                                estado = 0;
                                columna++;
                            }
                            else if (c.CompareTo('\n') == 0)
                            {
                                columna = 1;
                                estado = 0;
                                linea++;
                            }
                            else if (c.CompareTo('\r') == 0)
                            {
                                estado = 0;
                            }
                            else if (c.CompareTo('\f') == 0)
                            {
                                estado = 0;
                            }
                            else if (c.CompareTo('\t') == 0)
                            {
                                estado = 0;
                            }
                            else if (c.CompareTo('\b') == 0)
                            {
                                estado = 0;
                            }
                            else
                            {

                                if(c.CompareTo('#')== 0 && i == entrada.Length - 1)
                                {
                                    Console.WriteLine("\n Hemos concluido el analisis léxico exito");
                                }
                                else
                                {
                                    Console.WriteLine("Erro léxico con: " + c);
                                    error.AddLast(new Error(linea, c.ToString(), columna));
                                    columna++;
                                    errores++;
                                    estado = 0;
                                }
                            }                   
                            break;
                        }
                    case 1:
                        {
                            if (c.CompareTo('p') == 0 || c.CompareTo('P') == 0)
                            {
                                estado = 4;
                                auxLexico += c;
                                columna++;
                                // i -= 1;
                            }
                            else if(c.CompareTo('a')==0 || c.CompareTo('A')==0)
                            {
                                estado = 5;
                                auxLexico += c;
                                columna++;
                                // i -= 1;
                            }
                            else if (c.CompareTo('m') == 0 || c.CompareTo('M') == 0)
                            {
                                estado = 6;
                                auxLexico += c;
                                columna++;
                                // i -= 1;
                            }
                            else if (c.CompareTo('d') == 0 || c.CompareTo('D') == 0)
                            {
                                estado = 7;
                                auxLexico += c;
                                columna++;
                                // i -= 1;
                            }
                            else if (c.CompareTo('i') == 0 || c.CompareTo('I') == 0)
                            {
                                estado = 8;
                                auxLexico += c;
                                columna++;
                                // i -= 1;
                            }
                            else if (c.CompareTo('#')==0)
                            {
                                Console.WriteLine("Error con " + auxLexico + ".  Palabra reservada no definida");
                                error.AddLast(new Error(linea, auxLexico, columna));
                                columna++;
                                estado = 0;
                                auxLexico = "";
                                errores++;
                            }
                            else
                            {
                                columna++;
                                Console.WriteLine("Error con:" + c + "  Palabra reserada no definida");
                                error.AddLast(new Error(linea, c.ToString(), columna));
                               
                                auxLexico = "";
                                estado = 0;
                                errores++;

                            }
                            break;
                        }
                    case 2:
                        {
                            if (c.CompareTo('"') == 0)
                            {
                                auxLexico += c;
                                columna++;
                                agregarToken(Token.Type.CADENA_CARACTERES);
                            }
                            else if (c.CompareTo('\n') == 0)
                            {
                                linea++;
                                columna = 1;
                                estado = 2;
                            }
                            else
                            {
                                auxLexico += c;
                                columna++;
                                estado = 2;
                            }
                            break;
                        }
                    case 3:
                        {
                            if (Char.IsDigit(c))
                            {
                                estado = 3;
                                auxLexico += c;
                                columna++;
                            }
                            else
                            {
                             
                                agregarToken(Token.Type.NUMERO_ENTERO);
                                i -= 1;
                            }
                            break;
                        }
                    case 4:
                        {
                            if (c.CompareTo('l') == 0 || c.CompareTo('L') == 0)
                            {
                                auxLexico += c;
                                columna++;
                                if (auxLexico.ToLower().Equals("pl"))
                                {
                                    estado = 4;
                                }
                                else
                                {
                                    Console.WriteLine("Error con: " + auxLexico + " Palabra reservada no definida");
                                    error.AddLast(new Error(linea, auxLexico, columna));
                                    estado = 0;
                                    auxLexico = "";
                                    errores++;
                                }
                            }
                            else if (c.CompareTo('a') == 0 || c.CompareTo('A') == 0)
                            {
                                auxLexico += c;
                                columna++;
                                if (auxLexico.ToLower().Equals("pla") || auxLexico.ToLower().Equals("planifica"))
                                {
                                    estado = 4;
                                }
                                else
                                {
                                    Console.WriteLine("Error con: " + auxLexico + " Palabra reservada no definida");
                                    error.AddLast(new Error(linea, auxLexico, columna));
                                    estado = 0;
                                    auxLexico = "";
                                    errores++;
                                }
                            }
                            else if (c.CompareTo('n') == 0 || c.CompareTo('N') == 0)
                            {
                                auxLexico += c;
                                columna++;
                                if (auxLexico.ToLower().Equals("plan"))
                                {
                                    estado = 4;
                                }
                                else
                                {
                                    Console.WriteLine("Error con: " + auxLexico + " Palabra reservada no definida");
                                    error.AddLast(new Error(linea, auxLexico, columna));
                                    estado = 0;
                                    errores++;
                                    auxLexico = "";
                                }
                            }
                            else if (c.CompareTo('i') == 0 || c.CompareTo('I') == 0)
                            {
                                auxLexico += c;
                                columna++;
                                if (auxLexico.ToLower().Equals("plani") || auxLexico.ToLower().Equals("planifi"))
                                {
                                    estado = 4;
                                }
                                else
                                {
                                    Console.WriteLine("Error con: " + auxLexico + " Palabra reservada no definida");
                                    error.AddLast(new Error(linea, auxLexico, columna));
                                    estado = 0;
                                    errores++;
                                    auxLexico = "";
                                }
                            }
                            else if (c.CompareTo('f') == 0 || c.CompareTo('F') == 0)
                            {
                                auxLexico += c;
                                columna++;
                                if (auxLexico.ToLower().Equals("planif"))
                                {
                                    estado = 4;
                                }
                                else
                                {
                                    Console.WriteLine("Error con: " + auxLexico + ". Palabra reservada no definida");
                                    error.AddLast(new Error(linea, auxLexico, columna));
                                    estado = 0;
                                    errores++;
                                    auxLexico = "";
                                }
                            }
                            else if (c.CompareTo('c') == 0 || c.CompareTo('C') == 0)
                            {
                                auxLexico += c;
                                columna++;
                                if (auxLexico.ToLower().Equals("planific"))
                                {
                                    estado = 4;
                                }
                                else
                                {
                                    Console.WriteLine("Error con: " + auxLexico + ". Palabra reservada no definida");
                                    error.AddLast(new Error(linea, auxLexico, columna));
                                    estado = 0;
                                    errores++;
                                    auxLexico = "";
                                }
                            }
                            else if (c.CompareTo('d') == 0 || c.CompareTo('D') == 0)
                            {
                                auxLexico += c;
                                columna++;
                                if (auxLexico.ToLower().Equals("planificad"))
                                {
                                    estado = 4;
                                }
                                else
                                {
                                    Console.WriteLine("Error con: " + auxLexico + ". Palabra reservada no definida");
                                    error.AddLast(new Error(linea, auxLexico, columna));
                                    estado = 0;
                                    errores++;
                                    auxLexico = "";
                                }
                            }
                            else if (c.CompareTo('o') == 0 || c.CompareTo('O') == 0)
                            {
                                auxLexico += c;
                                columna++;
                                if (auxLexico.ToLower().Equals("planificado"))
                                {
                                    estado = 4;
                                }
                                else
                                {
                                    Console.WriteLine("Error con: " + auxLexico + ". Palabra reservada no definida");
                                    error.AddLast(new Error(linea, auxLexico, columna));
                                    estado = 0;
                                    errores++;
                                    auxLexico = "";
                                }
                            }
                            else if (c.CompareTo('r') == 0 || c.CompareTo('R') == 0)
                            {
                                auxLexico += c;
                                columna++;
                                if (auxLexico.ToLower().Equals("planificador"))
                                {
                                    agregarToken(Token.Type.RESERVADA_PLANIFICADOR);
                              
                                }
                                else
                                {
                                    Console.WriteLine("Error con:" + auxLexico);
                                    error.AddLast(new Error(linea, auxLexico, columna));
                                    auxLexico = "";
                                    errores++;
                                    estado = 0;
                                }
                            }
                            else if (c.CompareTo('#') == 0)
                            {
                                Console.WriteLine("Error con " + auxLexico + ".  Palabra reservada no definida");
                                error.AddLast(new Error(linea, auxLexico, columna));
                                estado = 0;
                                errores++;
                                auxLexico = "";
                            }
                            else
                            {
                                Console.WriteLine("error con" + auxLexico);
                                error.AddLast(new Error(linea, auxLexico, columna));
                                auxLexico = "";
                                columna++;
                                i -= 1;
                                estado = 0;
                                errores++;

                            }
                            break;
                        }
                    case 5:
                        {
                            if (c.CompareTo('n') == 0 || c.CompareTo('N') == 0)
                            {
                                auxLexico += c;
                                columna++;
                                if (auxLexico.ToLower().Equals("an"))
                                {
                                    estado = 5;
                                }
                                else
                                {
                                    Console.WriteLine("Error con: " + auxLexico + ". Palabra reservada no definida");
                                    error.AddLast(new Error(linea, auxLexico, columna));
                                    estado = 0;
                                    auxLexico = "";
                                    errores++;
                                }
                            }else if (c.CompareTo('i') == 0 || c.CompareTo('I') == 0)
                            {
                                auxLexico += c;
                                columna++;
                                if (auxLexico.ToLower().Equals("ani"))
                                {
                                    estado = 5;
                                }
                                else
                                {
                                    Console.WriteLine("Error con: " + auxLexico + ". Palabra reservada no definida");
                                    error.AddLast(new Error(linea, auxLexico,columna));
                                    estado = 0;
                                    errores++;
                                    auxLexico = "";
                                }
                            }
                            else if (c.CompareTo('o') == 0 || c.CompareTo('O') == 0)
                            {
                                auxLexico += c;
                                columna++;
                                if (auxLexico.ToLower().Equals("anio"))
                                {
                                    agregarToken(Token.Type.RESERVADA_ANIO);
                                }
                                else
                                {
                                    Console.WriteLine("Error con:" + auxLexico);
                                    error.AddLast(new Error(linea, auxLexico, columna));
                                    auxLexico = "";
                                    errores++;
                                    estado = 0;
                                }
                            }
                            else if (c.CompareTo('#') == 0)
                            {
                                Console.WriteLine("Error con " + auxLexico + ".  Palabra reservada no definida");
                                error.AddLast(new Error(linea, auxLexico, columna));
                                estado = 0;
                                errores++;
                                auxLexico = "";
                            }
                            else
                            {
                                Console.WriteLine("error con" + auxLexico);
                                error.AddLast(new Error(linea, auxLexico, columna));
                          
                                i -= 1;
                                estado = 0;
                                errores++;

                            }
                            break;
                        }
                    case 6:
                        {
                            if (c.CompareTo('e') == 0 || c.CompareTo('E') == 0)
                            {
                                auxLexico += c;
                                columna++;
                                if (auxLexico.ToLower().Equals("me"))
                                {
                                    estado = 6;
                                }
                                else
                                {
                                    Console.WriteLine("Error con: " + auxLexico + ". Palabra reservada no definida");
                                    error.AddLast(new Error(linea, auxLexico, columna));
                                    estado = 0;
                                    errores++;
                                    auxLexico = "";
                                }
                            }
                            else if (c.CompareTo('s') == 0 || c.CompareTo('S') == 0)
                            {
                                auxLexico += c;
                                columna++;
                                if (auxLexico.ToLower().Equals("mes"))
                                {
                                    agregarToken(Token.Type.RESERVADA_MES);
                                }
                                else
                                {
                                    Console.WriteLine("Error con:" + auxLexico);
                                    error.AddLast(new Error(linea, auxLexico, columna));
                                    auxLexico = "";
                                    errores++;
                                    estado = 0;
                                }
                            }
                            else if (c.CompareTo('#') == 0)
                            {
                                Console.WriteLine("Error con " + auxLexico + ".  Palabra reservada no definida");
                                error.AddLast(new Error(linea, auxLexico, columna));
                                estado = 0;
                                errores++;
                                auxLexico = "";
                            }
                            else
                            {
                                Console.WriteLine("error con" + auxLexico);
                                error.AddLast(new Error(linea, auxLexico, columna));
                                auxLexico = "";
                            
                                i -= 1;
                                estado = 0;
                                errores++;
                            }
                            break;
                        }
                    case 7:
                        {
                            if (c.CompareTo('i') == 0 || c.CompareTo('I')==0)
                            {
                                auxLexico += c;
                                columna++;
                                estado = 9;
                                
                            }else if (c.CompareTo('e') == 0 || c.CompareTo('E')==0)
                            {
                                auxLexico += c;
                                columna++;
                                estado = 10;
                            }
                            else
                            {
                                auxLexico += c;
                                Console.WriteLine("Error con: " + auxLexico + ".  Palabra reservada no definida");
                                error.AddLast(new Error(linea, auxLexico, columna));
                                auxLexico = "";
                                columna++;
                                errores++;
                                estado = 0;
                            }
                            break;
                        }
                    case 8:
                        {
                            if (c.CompareTo('m') == 0 || c.CompareTo('M') == 0)
                            {
                                auxLexico += c;
                                columna++;
                                if (auxLexico.ToLower().Equals("im"))
                                {
                                    estado = 8;
                                }
                                else
                                {
                                    Console.WriteLine("Error con: " + auxLexico + ". Palabra reservada no definida");
                                    error.AddLast(new Error(linea, auxLexico, columna));
                                    estado = 0;
                                    errores++;
                                    auxLexico = "";
                                }
                            }
                            else if (c.CompareTo('a') == 0 || c.CompareTo('A') == 0)
                            {
                                auxLexico += c;
                                columna++;
                                if (auxLexico.ToLower().Equals("ima"))
                                {
                                    estado = 8;
                                }
                                else
                                {
                                    Console.WriteLine("Error con: " + auxLexico + ". Palabra reservada no definida");
                                    error.AddLast(new Error(linea, auxLexico, columna));
                                    estado = 0;
                                    errores++;
                                    auxLexico = "";
                                }
                            }
                            else if (c.CompareTo('g') == 0 || c.CompareTo('G') == 0)
                            {
                                auxLexico += c;
                                columna++;
                                if (auxLexico.ToLower().Equals("imag"))
                                {
                                    estado = 8;
                                }
                                else
                                {
                                    Console.WriteLine("Error con: " + auxLexico + ". Palabra reservada no definida");
                                    error.AddLast(new Error(linea, auxLexico, columna));
                                    estado = 0;
                                    errores++;

                                    auxLexico = "";
                                }
                            }
                            else if (c.CompareTo('e') == 0 || c.CompareTo('E') == 0)
                            {
                                auxLexico += c;
                                columna++;
                                if (auxLexico.ToLower().Equals("image"))
                                {
                                    estado = 8;
                                }
                                else
                                {
                                    Console.WriteLine("Error con: " + auxLexico + ". Palabra reservada no definida");
                                    error.AddLast(new Error(linea, auxLexico, columna));
                                    estado = 0;
                                    errores++;
                                    auxLexico = "";
                                }
                            }
                            else if (c.CompareTo('n') == 0 || c.CompareTo('N') == 0)
                            {
                                auxLexico += c;
                                columna++;
                                if (auxLexico.ToLower().Equals("imagen"))
                                {
                                    agregarToken(Token.Type.RESERVADA_IMAGEN);
                                }
                                else
                                {
                                    Console.WriteLine("Error con:" + auxLexico);
                                    error.AddLast(new Error(linea, auxLexico, columna));
                                    auxLexico = "";
                                    errores++;
                                    estado = 0;
                                }
                            }
                            else if (c.CompareTo('#') == 0)
                            {
                                Console.WriteLine("Error con " + auxLexico + ".  Palabra reservada no definida");
                                error.AddLast(new Error(linea, auxLexico, columna));
                                estado = 0;
                                errores++;
                                auxLexico = "";
                            }
                            else
                            {
                                Console.WriteLine("error con" + auxLexico);
                                error.AddLast(new Error(linea, auxLexico, columna));
                                auxLexico = "";
                         
                                i -= 1;
                                estado = 0;
                                errores++;

                            }
                            break;
                        }
                    case 9:
                        {
                            if (c.CompareTo('a') == 0 || c.CompareTo('A') == 0)
                            {
                                auxLexico += c;
                                columna++;
                                if (auxLexico.ToLower().Equals("dia"))
                                {
                                    agregarToken(Token.Type.RESERVADA_DIA);
                                }
                            }else if (c.CompareTo('#') == 0)
                            {
                                Console.WriteLine("Error con: " + auxLexico + "   -Palabra reservada no definida");
                                error.AddLast(new Error(linea, auxLexico, columna));
                                auxLexico = "";
                                errores++;
                                estado = 0;
                            }
                            else
                            {
                                Console.WriteLine("error con" + auxLexico);
                                error.AddLast(new Error(linea, auxLexico, columna));
                                auxLexico = "";
                                i -= 1;
                              
                                estado = 0;
                                errores++;
                            }
                            break;
                        }
                    case 10:
                        {
                            if (c.CompareTo('s') == 0 || c.CompareTo('S') == 0)
                            {
                                auxLexico += c;
                                columna++;
                                if (auxLexico.ToLower().Equals("des"))
                                {
                                    estado = 10;
                                }
                                else
                                {
                                    Console.WriteLine("Error con: " + auxLexico + "   -Palabra reservada no definida");
                                    error.AddLast(new Error(linea, auxLexico, columna));
                                    estado = 0;
                                    errores++;
                                    auxLexico = "";
                                }
                            }
                            else if (c.CompareTo('c') == 0 || c.CompareTo('C') == 0)
                            {
                                auxLexico += c;
                                columna++;
                                if (auxLexico.ToLower().Equals("desc") || auxLexico.ToLower().Equals("descripc"))
                                {
                                    estado = 10;
                                }
                                else
                                {
                                    Console.WriteLine("Error con: " + auxLexico + ". Palabra reservada no definida");
                                    error.AddLast(new Error(linea, auxLexico, columna));
                                    estado = 0;
                                    errores++;
                                    auxLexico = "";
                                }
                            }
                            else if (c.CompareTo('r') == 0 || c.CompareTo('R') == 0)
                            {
                                auxLexico += c;
                                columna++;
                                if (auxLexico.ToLower().Equals("descr"))
                                {
                                    estado = 10;
                                }
                                else
                                {
                                    Console.WriteLine("Error con: " + auxLexico + ". Palabra reservada no definida");
                                    error.AddLast(new Error(linea, auxLexico, columna));
                                    estado = 0;
                                    errores++;
                                    auxLexico = "";
                                }
                            }
                            else if (c.CompareTo('i') == 0 || c.CompareTo('I') == 0)
                            {
                                auxLexico += c;
                                columna++;
                                if (auxLexico.ToLower().Equals("descri") || auxLexico.ToLower().Equals("descripci"))
                                {
                                    estado = 10;
                                }
                                else
                                {
                                    Console.WriteLine("Error con: " + auxLexico + ". Palabra reservada no definida");
                                    error.AddLast(new Error(linea, auxLexico, columna));
                                    estado = 0;
                                    errores++;
                                    auxLexico = "";
                                }
                            }
                            else if (c.CompareTo('p') == 0 || c.CompareTo('P') == 0)
                            {
                                auxLexico += c;
                                columna++;
                                if (auxLexico.ToLower().Equals("descrip"))
                                {
                                    estado = 10;
                                }
                                else
                                {
                                    Console.WriteLine("Error con: " + auxLexico + ". Palabra reservada no definida");
                                    error.AddLast(new Error(linea, auxLexico, columna));
                                    estado = 0;
                                    errores++;
                                    auxLexico = "";
                                }
                            }
                            else if (c.CompareTo('o') == 0 || c.CompareTo('O') == 0)
                            {
                                auxLexico += c;
                                columna++;
                                if (auxLexico.ToLower().Equals("descripcio"))
                                {
                                    estado = 10;
                                }
                                else
                                {
                                    Console.WriteLine("Error con: " + auxLexico + ". Palabra reservada no definida");
                                    error.AddLast(new Error(linea, auxLexico, columna));
                                    estado = 0;
                                    errores++;
                                    auxLexico = "";
                                }
                            }
                           
                       
                            else if (c.CompareTo('n') == 0 || c.CompareTo('N') == 0)
                            {
                                auxLexico += c;
                                columna++;
                                if (auxLexico.ToLower().Equals("descripcion"))
                                {
                                    agregarToken(Token.Type.RESERVADA_DESCRIPCION);
                                }
                                else
                                {
                                    Console.WriteLine("Error con:" + auxLexico);
                                    error.AddLast(new Error(linea, auxLexico, columna));
                                    auxLexico = "";
                                    errores++;
                                    estado = 0;
                                }
                            }
                            else if (c.CompareTo('#') == 0)
                            {
                                Console.WriteLine("Error con " + auxLexico + "   -Palabra reservada no definida");
                                error.AddLast(new Error(linea, auxLexico, columna));
                                estado = 0;
                                errores++;
                                auxLexico = "";
                            }
                            else
                            {
                                Console.WriteLine("error con" + auxLexico);
                                error.AddLast(new Error(linea, auxLexico, columna));
                                auxLexico = "";
                                i -= 1;
                           
                                estado = 0;
                                errores++;

                            }
                            break;
                        }
                  
                }
               
            }
            return salida;
         
        }

        public void agregarToken(Token.Type type)
        {
            salida.AddLast(new Token(type, auxLexico, linea));
            auxLexico = "";
            estado = 0;
        }

        public void imprimirTokens(LinkedList<Token> listaTokens)
        {
            int contador = 1;

            foreach (Token token  in listaTokens)
            {
                Console.WriteLine("\n" + token.getType() + "<-------------------->" + token.getValue() + "<---------------------->" + token.getLinea()+ "<------>" + contador);
                contador++;
            }
            
        }


        public LinkedList<Error> Errores()
        {
            return error;
        }

       
      
    }
}
