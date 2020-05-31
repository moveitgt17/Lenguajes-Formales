using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto1LFP
{
    class Graphic
    {
        private string name;
        private List<Continent> continents;

        public Graphic()
        {
            continents = new List<Continent>();
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
        public void setContinents(Continent continent)
        {
            continents.Add(continent);
        }
        public string getName()
        {
            return name;
        }
        public List<Continent> getContinents()
        {
            return continents;
        }
    }
}
