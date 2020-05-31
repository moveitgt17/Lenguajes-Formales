using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticaUnoLFP
{
    class Año
    {
        public int año;
        public List<Mes> mes;

        public Año()
        {
            this.mes = new List<Mes>();
        }
    }
}
