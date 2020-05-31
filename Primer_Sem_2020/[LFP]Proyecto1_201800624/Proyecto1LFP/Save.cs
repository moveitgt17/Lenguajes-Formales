using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto1LFP
{
    class Save
    {
        private Graphic finalGraphic;
        private Graphic graphic;
        List<string> actual;
        private Continent continent;
        private Country country;
        private Boolean continentB, countryB, graphicB, population, saturation, flag, name;
        
        public Save()
        {   
            actual = new List<string>();
            actual.Add("{");
            actual.Add("}");
            actual.Add(":");
            actual.Add(";");
            actual.Add("%");
        }
        public Boolean confirmate(string value)
        {
            if (actual.Contains(value))
            {
                return false;
            }
            if (value.Equals("Grafica"))
            {
                graphic = new Graphic();
                graphicB = true;
                return true;
            }else if (value.Equals("Continente"))
            {
                continent = new Continent();
                continentB = true;
                return true;
            }else if (value.Equals("Pais"))
            {
                country = new Country();
                countryB = true;
                return true;
            }else if (value.Equals("Poblacion"))
            {
                population = true;
                return true;
            }else if (value.Equals("Nombre"))
            {
                name = true;
                return true;
            }else if (value.Equals("Saturacion"))
            {
                saturation = true;
                return true;
            }else if (value.Equals("Bandera"))
            {
                flag = true;
                return true;
            }
            return false;
        }

        public Boolean exist(Token token)
        {
            string value = token.getValue();
            if (confirmate(value) || actual.Contains(value))

                return false;
            return true;
        }

        public void test(Token token)
        {
            string value = token.getValue();
            if (graphicB)
            {
                graphicB = false;
                graphic.setName(value);
                finalGraphic = graphic;
                Console.WriteLine("Nombre de la grafica:" + graphic.getName());
            }else if (population)
            {
                population = false;
                country.setPopulation(Convert.ToInt32(value));
            }else if (saturation)
            {
                saturation = false;
                country.setSaturation(Convert.ToInt32(value));
            }else if (flag)
            {
                flag = false;
                country.setFlag(value);
            }else if (countryB)
            {
                countryB = false;
                country.setName(value);
                continent.setCountries(country);
            }else if (continentB) 
            {
                continentB = false;
                continent.setName(value);
                graphic.setContinents(continent);
            }else if (name)
            {
                name = false;
            }

        }

        public Graphic getFinalGraphic(LinkedList<Token> tokens)
        {
            finalGraphic = new Graphic();
            var start = tokens.First;
            while(start != null)
            {
                while (!exist(start.Value))
                {
                    start = start.Next;
                    if (start == null)
                        return finalGraphic;
                }
                test(start.Value);
                start = start.Next;
            }
            return finalGraphic;
        }
    }
}
