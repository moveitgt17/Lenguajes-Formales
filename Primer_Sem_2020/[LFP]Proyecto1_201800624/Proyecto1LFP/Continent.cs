using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto1LFP
{
    class Continent
    {
        private string name;
        private List<Country> countries;

        public Continent()
        {
            countries = new List<Country>();
        }
        public void setCountries(Country country)
        {
            countries.Add(country);
        }
        public void setName(string name)
        {
            int state = 0;
            string newName = "";
            Char c;
            for (int x = 0; x < name.Length - 1; x++)
            {
                c = name.ElementAt(x);
                switch (state)
                {
                    case 0:
                        {
                            if (Char.IsLetter(c))
                            {
                                newName += c;
                                state = 0;
                            }
                            else if (c.CompareTo(' ') == 0)
                            {
                                state = 0;
                            }
                            else if(c.CompareTo('"')==0)
                            {
                                state = 0;
                            }
                            break;
                        }
                }
            }
            this.name = newName;
        }
        public string getName()
        {
            return name;
        }
        public List<Country> getCountries()
        {
            return countries;
        }
        public string toString()
        {
            foreach (Country contry in countries)
            {
                contry.toString();
            }
            return "Nombre: " + name;

        }
        public string color()
        {
            string color = "";
            if (setSaturacion() >= 0 && setSaturacion() <= 15)
            {
                color = "white";
            }
            else if (setSaturacion() > 15 && setSaturacion() <= 30)
            {
                color = "blue";
            }
            else if (setSaturacion() > 30 && setSaturacion() <= 45)
            {
                color = "green";
            }
            else if (setSaturacion() > 45 && setSaturacion() <= 60)
            {
                color = "yellow";
            }
            else if (setSaturacion() > 60 && setSaturacion() <= 75)
            {
                color = "orange";
            }
            else if (setSaturacion() > 75 && setSaturacion() <= 100)
            {
                color = "red";
            }
            return color;
        }

        public int setSaturacion()
        {
            int addition = 0;
            int accountant=0;
            int total = 0;
            foreach (Country contry in countries)
            {
                addition = addition + contry.getSaturation();
                accountant++;
            }
            total = addition / accountant;

            return total;

        }

        public string getDot()
        {
          
            string graphizStrings = "";
            graphizStrings += name + " [shape=record label=\"{ " + name + " | " + setSaturacion() + "} \" style=filled fillcolor=" + color() + "];\n";

            foreach (Country county in countries)
            {
                graphizStrings += name + "->" + county.getName() + ";\n";
            }
            foreach (Country conty in countries)
            {
                graphizStrings += conty.getDot();
            }
            return graphizStrings;
        }
    }
}
