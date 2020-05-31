using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto1LFP
{
    class Country
    {
        private string name, flag;
        private int population, saturation;

        public Country()
        {

        }
        public Country(string name, string flag, int population, int saturation)
        {
            this.name = name;
            this.flag = flag;
            this.population = population;
            this.saturation = saturation;
        }
        public void setName(string name)
        {
            int state = 0;
            string newName = "";
            Char c;
            for (int x=0; x<name.Length-1; x++)
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
                            }else if(c.CompareTo(' ') == 0)
                            {
                                state = 0;
                            }
                            else if (c.CompareTo('"') == 0)
                            {
                                state = 0;
                            }
                            break;
                        }
                }
            }
            this.name = newName;
        }
        public void setFlag(string flag)
        {
            this.flag = flag;
        }
        public void setPopulation(int population)
        {
            this.population = population;
        }
        public void setSaturation(int saturation)
        {
            this.saturation = saturation;
        }
        public string getName()
        {
            return name;
        }
        public string getFlag()
        {
            return flag;
        }
        public int getPopulation()
        {
            return population;
        }
        public int getSaturation()
        {
            return saturation;
        }
        public string toString()
        {
            return "Nombre: " + name + "Población: " + population.ToString() + "Saturación: " + saturation.ToString() + "Bandera: " + flag;
        }

        public string getDot()
        {
            return name + " [shape=record label=\"{ " + name + " | " + this.saturation + " }\" style=filled fillcolor=" + color() + "];\n";
      
        }

        public string color()
        {
            string color = "";
            if (saturation >= 0 && saturation <= 15)
            {
                color = "white";
            }
            else if (saturation > 15 && saturation <= 30)
            {
                color = "blue";
            }else if(saturation > 30 && saturation <= 45)
            {
                color = "green";
            }else if(saturation > 45 && saturation <= 60)
            {
                color = "yellow";
            }
            else if (saturation > 60 && saturation <= 75)
            {
                color = "orange";
            }
            else if (saturation > 75 && saturation <= 100)
            {
                color = "red";
            }
            return color;
        }
    }
}
