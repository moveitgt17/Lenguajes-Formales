using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticaUnoLFP
{
    class Prueba
    {
        List<Planificador> planificador;
        List<string> actual;
        private Planificador plan;
        private Año anio;
        private Mes mes;
        private Dia dia;
        private bool descripcion, imagen, plann, año, mess, diaa;

    public Prueba()
        {
            actual = new List<string>();
            actual.Add("{");
            actual.Add("}");
            actual.Add("[");
            actual.Add("]");
            actual.Add("(");
            actual.Add(")");
            actual.Add("<");
            actual.Add(">");
            actual.Add(";");
            actual.Add(":");
        }
    
    public bool confirmate(String valor)
        {
            if (actual.Contains(valor))
            {
                return false;
            }
            valor = valor.ToLower();

            if (valor.Equals("planificador"))
            {
                plan = new Planificador();
                plann = true;
                return true;
            }else if (valor.Equals("anio"))
            {
                anio = new Año();
                año = true;
                return true;
            }else if (valor.Equals("mes"))
            {
                mes = new Mes();
                mess = true;
                return true;
            }
            else if (valor.Equals("dia"))
            {
                dia = new Dia();
                diaa = true;
                return true;
            }
            else if (valor.Equals("descripcion"))
            {
                
                descripcion = true;
                return true;
            }
            else if (valor.Equals("imagen"))
            {
               
                imagen = true;
                return true;
            }
            return false;
        }

    public bool existe(Token token)
        {
            string valor = token.getValue();
            if(confirmate(valor) || actual.Contains(valor))
                return false;
            return true;
        }
    
    public List<Planificador> planes(LinkedList<Token> token)
        {
            planificador = new List<Planificador>();
            var inicio = token.First;    
            while(inicio != null)
            {
                while (!existe(inicio.Value))
                {
                    inicio = inicio.Next;
                    if (inicio == null)
                    
                        return planificador;
                    
                }
               
                test(inicio.Value);
                inicio = inicio.Next;
            }
            return planificador;
        }

    private void test(Token token)
        {
            string valor = token.getValue();
            if (plann)
            {
                plann = false;
                plan.nombre =  valor;
                planificador.Add(plan);
                Console.WriteLine("jajjajajjajajjajaj" + plan.ToString());
            }else if (descripcion)
            {
                descripcion = false;
                dia.descripcion = valor;
            }
            else if (imagen)
            {
                imagen = false;
                dia.ruta = valor;
            }
            else if (diaa)
            {
                diaa = false;
                dia.dia = Convert.ToInt32(valor);
                mes.dia.Add(dia);
            }
            else if (mess)
            {
                mess = false;
                mes.mes = Convert.ToInt32(valor);
                anio.mes.Add(mes);
            }
            else if (año)
            {
                año = false;
                anio.año = Convert.ToInt32(valor.ToString());
                plan.anio.Add(anio);
            }
        }

    


    }
}
