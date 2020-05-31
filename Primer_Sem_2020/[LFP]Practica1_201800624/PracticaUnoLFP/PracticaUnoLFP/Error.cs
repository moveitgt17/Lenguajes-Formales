using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticaUnoLFP
{
    class Error
    {
        private int linea;
        private int columna;
        private String error;
        private String descripcion;

        public Error()
        {

        }
        public Error(int linea, String error,int columna)
        {
            this.linea = linea;
            this.error = error;
            this.descripcion = "ERROR_DESCONOCIDO";
            this.columna = columna;
        }

        public int getLinea()
        {
            return linea;
        }
        public String desc()
        {
            return descripcion;
        }

        public int getColumna()
        {
            return columna;
        }

        public String getError()
        {
            return error;
        }

        public String getDes()
        {
            return descripcion;
        }

        public String toString()
        {
            return "Linea: " + linea.ToString() + "  -Error: " + error;
        }
    }
}
