using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticaUnoLFP
{
    class Token
    {
        public enum Type
        {
            SIGNO_PUNTOYCOMA,
            SIGNO_DOSPUNTOS,
            CORCHETE_IZQ,
            CORCHETE_DER,
            LLAVE_IZQ,
            LLAVE_DER,
            PARENTESIS_IZQ,
            PARENTESIS_DER,
            SIGNO_MAYOR,
            SIGNO_MENOR,
            NUMERO_ENTERO,
            CADENA_CARACTERES,
            RESERVADA_PLANIFICADOR,
            RESERVADA_ANIO,
            RESERVADA_MES,
            RESERVADA_DIA,
            RESERVADA_DESCRIPCION,
            RESERVADA_IMAGEN
        }
        private Type tokenType;
        private string value;
        private int linea;

        public Token(Type tokenType, string value, int linea)
        {
            this.tokenType = tokenType;
            this.value = value;
            this.linea = linea;
        }

        public string getValue()
        {
            return value;
        }
         
        public String getType()
        {
            switch (tokenType)
            {
                case Type.CADENA_CARACTERES:
                    {
                        return "CADENA_CARACTERES";
                        
                    }
                case Type.SIGNO_PUNTOYCOMA:
                    {
                        return "SIGNO_PUNTOYCOMA";

                    }
                case Type.SIGNO_DOSPUNTOS:
                    {
                        return "DOS_PUNTOS";

                    }
                case Type.CORCHETE_IZQ:
                    {
                        return "CORCHETE_IZQ";

                    }
                case Type.CORCHETE_DER:
                    {
                        return "CORCHETE_DER";

                    }
                case Type.LLAVE_IZQ:
                    {
                        return "LLAVE_IZQ";

                    }
                case Type.LLAVE_DER:
                    {
                        return "LLAVE_DER";

                    }
                case Type.PARENTESIS_IZQ:
                    {
                        return "PARENTESIS_IZQ";

                    }
                case Type.PARENTESIS_DER:
                    {
                        return "PARENTESIS_DER";

                    }
                case Type.SIGNO_MENOR:
                    {
                        return "SIGNO_MENOR";

                    }
                case Type.SIGNO_MAYOR:
                    {
                        return "SIGNO_MAYOR";

                    }
                case Type.NUMERO_ENTERO:
                    {
                        return "NUMERO_ENTERO";

                    }
                case Type.RESERVADA_PLANIFICADOR:
                    {
                        return "RESERVADA_PLANIFICADOR";

                    }
                case Type.RESERVADA_DESCRIPCION:
                    {
                        return "RESERVADA_DESCRIPCION";

                    }
                case Type.RESERVADA_IMAGEN:
                    {
                        return "RESERVADA_IMAGEN";

                    }
                case Type.RESERVADA_ANIO:
                    {
                        return "RESERVADA_ANIO";

                    }
                case Type.RESERVADA_MES:
                    {
                        return "RESERVADA_MES";

                    }
                case Type.RESERVADA_DIA:
                    {
                        return "RESERVADA_DIA";

                    }
                default:
                    {
                        return "PALABRA_DESCONOCIDA";
                    }
            }
        }
        public int getLinea()
        {
            return linea;
        }
    }
}
