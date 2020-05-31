using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto1LFP
{
    class Mistake
    {
        private int line;
        private int column;
        private int number;
        private string mistake;
        private string description;

        public Mistake()
        {

        }
        public Mistake(int line, string mistake, int column, int number)
        {
            this.line = line;
            this.mistake = mistake;
            this.column = column;
            description = "ERROR_LEXICOGRAFICO";
            this.number = number;
        }

        public int getLine()
        {
            return line;
        }
        public string getDescripticion()
        {
            return description;
        }
        public int getColumn()
        {
            return column;
        }
        public string getMistake()
        {
            return mistake;
        }
        public void setLine(int line)
        {
            this.line = line;
        }
        public void setLColumn(int column)
        {
            this.column = column;
        }
        public void setMistake(string mistake)
        {
            this.mistake = mistake;
        }
        public void setDescription(string description)
        {
            this.description = description;
        }

        public void setNumber(int number)
        {
            this.number = number;
        }

        public int getNumber()
        {
            return number;
        }
        public String toString()
        {
            return "Line: " + line.ToString() + "Mistake: " + mistake + "Column: " + column.ToString() + "Description: " + description + "Number: " + number;
        }
    }
}
