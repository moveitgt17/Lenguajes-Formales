using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto1LFP
{
    class Token
    {
        public enum Type
        {
            RESERVADA_GRAFICA,
            RESERVADA_CONTINENTE,
            RESERVADA_PAIS,
            RESERVADA_NOMBRE,
            RESERVADA_POBLACION,
            RESERVADA_SATURACION,
            RESERVADA_BANDERA,
            NUMERO_ENTERO,
            LLAVE_IZQ,
            LLAVE_DER,
            SIGNO_DOSPUNTOS,
            SIGNO_PUNTOYCOMA,
            CADENA_CARACTERES,
            SIGNO_PORCENTAJE
        }
        private Type tokenType;
        private string value;
        private int line;
        private int number;
        private int column;

        public Token(Type tokenType, string value, int line, int column)
        {
            this.tokenType = tokenType;
            this.value = value;
            this.line = line;
            this.column = column;
        }

        public string getValue()
        {
            return value;
        }

        public int getLine()
        {
            return line;
        }

        public int getColumn()
        {
            return column;
        }

        public int getNumber()
        {
            return number;
        }

        public void setNumber(int number)
        {
            this.number = number;
        }

        public string getType()
        {
            switch (tokenType)
            {
                case Type.CADENA_CARACTERES:
                    {
                        return "CADENA DE CARACTERES";
                    }
                case Type.SIGNO_PORCENTAJE:

                    {
                        return "SIGNO DE PORCENTAJE";    
                    }
                case Type.SIGNO_PUNTOYCOMA:
                    {
                        return "SIGNO PUNTO Y COMA";
                    }
                case Type.SIGNO_DOSPUNTOS:
                    {
                        return "SIGNO DOS PUNTOS";
                    }
                case Type.LLAVE_IZQ:
                    {
                        return "LLAVE IZQUIERDA";
                    }
                case Type.LLAVE_DER:
                    {
                        return "LLAVE DERECHA";
                    }
                case Type.NUMERO_ENTERO:
                    {
                        return "NUMERO ENTERO";
                    }
                case Type.RESERVADA_BANDERA:
                    {
                        return "RESERVADA_BANDERA";
                    }
                case Type.RESERVADA_GRAFICA:
                    {
                        return "RESERVADA_GRAFICA";
                    }
                case Type.RESERVADA_CONTINENTE:
                    {
                        return "RESERVADA_CONTINENTE";
                    }
                case Type.RESERVADA_PAIS:
                    {
                        return "RESERVADA_PAIS";
                    }
                case Type.RESERVADA_POBLACION:
                    {
                        return "RESERVADA_POBLACION";
                    }
                case Type.RESERVADA_NOMBRE:
                    {
                        return "RESERVADA_NOMBRE";
                    }
                case Type.RESERVADA_SATURACION:
                    {
                        return "RESERVADA_SATURACION";
                    }
                default:
                    {
                        return "PALABRA DESCONOCIDA";
                    }
                
                
            }
        }
        public void setLine(int line)
        {
            this.line = line;
        }
        public void setColumn(int column)
        {
            this.column = column;
        }
        public void setValue(string value)
        {
            this.value = value;
        }
        public string toString()
        {
            return "Number: " + number + "Column: " + column + "Line: " + line + "Type" + tokenType + "Value: " + value; 
        }

    }
}
