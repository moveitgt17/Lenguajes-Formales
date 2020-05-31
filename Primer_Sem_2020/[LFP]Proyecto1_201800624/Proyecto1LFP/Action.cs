using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Proyecto1LFP
{
    class Action
    {
        public void colorChange(int place, string token, RichTextBox area, Color newColor)
        {
            if (area.Text.Contains(token))
            {
                int position = - 1;
                int selection = area.SelectionStart;
                while((position = area.Text.IndexOf(token, (position+1))) != -1)
                {
                    area.Select((position + place), token.Length);
                    area.SelectionColor = newColor;
                    area.Select(selection, 0);
                    area.SelectionColor = Color.Black;
                }
            }
        }
    }
}
