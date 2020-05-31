using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace Proyecto1LFP
{
    public partial class Form1 : Form
    {
        private  Graphic test = new Graphic();
        public static int z = 0;
        public static int x = 0;
        private static int y = 0;
        public static string exit;
        private static LinkedList<Token> tokenList;
        private static ProcessStartInfo start;

        public Form1()
        {
            InitializeComponent();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                y++;
                pictureBox1.Visible = false;
                label1.Visible = false;
                label2.Visible = false;
                countryP.Visible = false;
                populationP.Visible = false;
                pictureBox2.Visible = false;
                Analyzerr analize = new Analyzerr();
                Graphic g = new Graphic();
                String text = "";
                foreach (RichTextBox tab in Editor.SelectedTab.Controls)
                {
                    if (tab.GetType().Equals(typeof(RichTextBox)))
                    {
                        text = tab.Text;
                    }

                }
                tokenList = analize.scanner(text);
                if (analize.mistakesNo() == 0)
                {

                    analize.printTokens(tokenList);
                    htmlToken();
                    analize.setNumberTokens(0);
                    MessageBox.Show("El archivo fue analizado correctamente", "Error léxico", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    foreach (RichTextBox tab in Editor.SelectedTab.Controls)
                    {
                        if (tab.GetType().Equals(typeof(RichTextBox)))
                        {
                            setNewColor(tab);
                        }

                    }
                    Save s = new Save();
                    g = s.getFinalGraphic(tokenList);
                    test = g;
                    graphiz(g);
                    z++;
                }
                else
                {
                    analize.printTokens(tokenList);
                    MessageBox.Show("El archivo que se analizo contiene errores", "Error léxico", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    analize.printMistakes(analize.mistakeList());
                    htmlMistakes();
                    htmlToken();
                    analize.setNumberTokens(0);
                    analize.setMistakes(0);
                }
            }
            catch (Exception)
            {
               
            }
           
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void acercaDeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Information s = new Information();
            s.Visible = true;
        }

        public class window: TabPage
        {
            public window()
            {
                RichTextBox text = new RichTextBox();
                text.WordWrap = false;
                text.Dock = DockStyle.Fill;
                this.Controls.Add(text);
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void nuevaPestañaToolStripMenuItem_Click(object sender, EventArgs e)
        {
          
            TabPage tab = new window();
            Editor.TabPages.Add(tab);
        }

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(Editor.TabCount != 0)
            {
                OpenFileDialog searchRoute = new OpenFileDialog();
                searchRoute.Filter = "Extension .ORG|*.ORG";
                searchRoute.Title = "Abrir archivo";

                if(searchRoute.ShowDialog()== System.Windows.Forms.DialogResult.OK && searchRoute.FileName.Length>0)
                {
                    string route = searchRoute.FileName;
                    Editor.SelectedTab.Text = searchRoute.SafeFileName;
                    string information = System.IO.File.ReadAllText(route);

                    foreach (Control tab in Editor.SelectedTab.Controls)
                    {
                        if (tab.GetType().Equals(typeof(RichTextBox)))
                        {
                            tab.Text = information;
                        }
                    }
                }
                
            }
            else
            {
                MessageBox.Show("Debe abrir una pestaña", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void guardarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Editor.TabCount != 0)
            {
                x++;
                if (x == 1)
                {
                    SaveFileDialog save = new SaveFileDialog();
                    save.Filter = ".txt | *.txt";
                    save.Title = "Guardar";
                    save.FileName = "";


                    if (save.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        StreamWriter write = new StreamWriter(save.FileName);
                        foreach (RichTextBox tab in Editor.SelectedTab.Controls)
                        {
                            if (tab.GetType().Equals(typeof(RichTextBox)))
                            {
                                write.WriteLineAsync(tab.Text + "\n");
                               
                            }
                        }
                        write.Close();
                        exit = save.FileName;
                        MessageBox.Show("Archivo guardado correctamente", "MENSAJE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    StreamWriter write = new StreamWriter(exit);
                    foreach (RichTextBox tab in Editor.SelectedTab.Controls)
                    {
                        if (tab.GetType().Equals(typeof(RichTextBox)))
                        {
                            write.WriteLine(tab.Text);
                        }
                    }
                    write.Close();
                    MessageBox.Show("Guardado", "MENSAJE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
             
            }
            else
            {
                MessageBox.Show("Debe abrir una pestaña", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void guardarComoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Editor.TabCount != 0)
            {   
                SaveFileDialog save = new SaveFileDialog();
                save.Filter = ".txt | *.txt";
                save.Title = "Guardar";
                save.FileName = "";
                if (save.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    StreamWriter write = new StreamWriter(save.FileName);
                    foreach (RichTextBox tab in Editor.SelectedTab.Controls)
                    {
                        if (tab.GetType().Equals(typeof(RichTextBox)))
                        {
                            write.WriteLine(tab.Text);
                        }
                    }
                    write.Close();
                    MessageBox.Show("Archivo guardado correctamente", "MENSAJE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Debe abrir una pestaña", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void htmlToken()
        {
            Analyzerr analize = new Analyzerr();
            StreamWriter write = new StreamWriter("C:\\Users\\CARLOS\\Documents\\universidad\\4to semestre\\Lenguajes Formales\\Laboratorio\\Proyecto 1\\Toknes.html");
            write.WriteLine("<html>");
            write.WriteLine("<head><h1> LISTA DE TOKENS</h1></head>");
            write.WriteLine("<body>");
            write.WriteLine("<p><h3> Aqui se muestra la lista de tokens del archivo analizado </h3></p>");
            write.WriteLine("<p></p>");
            write.WriteLine("<table border='1'>");
            write.WriteLine("<tr>");
            write.WriteLine("<td>No.</td><td>Linea</td><td>Lexema</td><td>Columna</td><td>Token</td>");
            foreach (Token token in tokenList)
            {
                write.WriteLine("<tr>");
                write.WriteLine("<td>" + token.getNumber() + "</td>");
                write.WriteLine("<td>" + token.getLine() + "</td>");
                write.WriteLine("<td>" + token.getValue() + "</td>");
                write.WriteLine("<td>" + token.getColumn() + "</td>");
                write.WriteLine("<td>" + token.getType() + "</td>");
                write.WriteLine("</tr>");
            }
            write.WriteLine("</table>");
            write.WriteLine("<p></p>");
            write.WriteLine("<p><h4>Se obtuvo un total de:" +  analize.getNumberTokens() + " Tokens. </h4> </p>");
            write.WriteLine("<p><h3>No hubieron errores en el analisis del archivo</h3></p>");
            write.WriteLine("</body>");
            write.WriteLine("</html>");
            write.Close();

            Process.Start("C:\\Users\\CARLOS\\Documents\\universidad\\4to semestre\\Lenguajes Formales\\Laboratorio\\Proyecto 1\\Toknes.html");
        }

        private void htmlMistakes()
        {
            Analyzerr analize = new Analyzerr();
            StreamWriter write = new StreamWriter("C:\\Users\\CARLOS\\Documents\\universidad\\4to semestre\\Lenguajes Formales\\Laboratorio\\Proyecto 1\\Error.html");
            write.WriteLine("<html>");
            write.WriteLine("<head><h1> LISTA DE ERRORES</h1></head>");
            write.WriteLine("<body>");
            write.WriteLine("<p><h3> Aqui se muestra la lista de errores del archivo analizado </h3></p>");
            write.WriteLine("<p></p>");
            write.WriteLine("<table border='1'>");
            write.WriteLine("<tr>");
            write.WriteLine("<td>No.</td><td>Linea</td><td>Error</td><td>Columna</td><td>Descripción</td>");
            foreach (Mistake mistakes in analize.mistakeList())
            {
                write.WriteLine("<tr>");
                write.WriteLine("<td>" + mistakes.getNumber() + "</td>");
                write.WriteLine("<td>" + mistakes.getLine() + "</td>");
                write.WriteLine("<td>" + mistakes.getMistake() + "</td>");
                write.WriteLine("<td>" + mistakes.getColumn() + "</td>");
                write.WriteLine("<td>" + mistakes.getDescripticion() + "</td>");
                write.WriteLine("</tr>");
            }
            write.WriteLine("</table>");
            write.WriteLine("<p></p>");
            write.WriteLine("<p><h4>Se obtuvo un total de:" + analize.mistakesNo() + " errores. </h4> </p>");
            write.WriteLine("</body>");
            write.WriteLine("</html>");
            write.Close();

            Process.Start("C:\\Users\\CARLOS\\Documents\\universidad\\4to semestre\\Lenguajes Formales\\Laboratorio\\Proyecto 1\\Error.html");
        }

        private void changingColor()
        {
            string Grafica = "Grafica";
            List<int> Graf = new List<int>();


            foreach (RichTextBox tab in Editor.SelectedTab.Controls)
            {
                if (tab.GetType().Equals(typeof(RichTextBox)))
                {

                    for(int x=0; x<100; x++)
                    {
                        Graf.Add(tab.Text.IndexOf("Grafica"));
                        int lenght = Grafica.Length;
                        tab.Select(4520, lenght);
                        tab.SelectionColor = Color.Red;
                    }
              
             
                   
                }
            }
         

        }

        private void setNewColor(RichTextBox textArea)
        {
                 Action a = new Action();
                 foreach (RichTextBox tab in Editor.SelectedTab.Controls)
            {
                if (tab.GetType().Equals(typeof(RichTextBox)))
                {
                    a.colorChange(0, "Grafica", textArea, Color.Blue);
                    a.colorChange(0, "Continente", textArea, Color.Blue);
                    a.colorChange(0, "Pais", textArea, Color.Blue);
                    a.colorChange(0, "Nombre", textArea, Color.Blue);
                    a.colorChange(0, "Poblacion", textArea, Color.Blue);
                    a.colorChange(0, "Saturacion", textArea, Color.Blue);
                    a.colorChange(0, "Bandera", textArea, Color.Blue);
                    a.colorChange(0, "{", textArea, Color.Red);
                    a.colorChange(0, "}", textArea, Color.Red);
                    a.colorChange(0, ";", textArea, Color.Orange);

                    foreach (Token token in tokenList)
                    {
                        if (token.getType().Equals("CADENA DE CARACTERES"))
                        {
                            a.colorChange(0, token.getValue(), textArea, Color.Yellow);
                        }
                        else if (token.getType().Equals("NUMERO ENTERO"))
                        {
                            a.colorChange(0, token.getValue(), textArea, Color.Green);
                        }

                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Save s = new Save();
            Graphic g = s.getFinalGraphic(tokenList);
            Console.WriteLine("Grafica: " + g.getName());
            foreach (Continent continent in g.getContinents())
            {
                Console.WriteLine("Continente:" + continent.getName());
                foreach (Country country  in continent.getCountries())
                {
                    Console.WriteLine("Pais: " + country.getName());
                    Console.WriteLine("Población: " + country.getPopulation().ToString());
                    Console.WriteLine("Saturación: " + country.getSaturation().ToString());
                    Console.WriteLine("Bandera: " + country.getFlag());
                }
            }

            graphiz(g);
            
        }

        private void graphiz(Graphic graphic)
        {
            if (z == 0)
            {
                string graphString = "";
                graphString += " digraph G {\n";
                graphString += "  start [shape=Mdiamond label=\"" + graphic.getName() + "\"];\n";
                foreach (Continent conty in graphic.getContinents())
                {
                    graphString += " start ->" + conty.getName() + ";\n";
                }
                foreach (Continent contin in graphic.getContinents())
                {
                    graphString += contin.getDot();
                }
                graphString += "}";
                StreamWriter write = new StreamWriter("C:\\Users\\CARLOS\\Documents\\universidad\\4to semestre\\Lenguajes Formales\\Laboratorio\\P.txt");
                write.WriteLine(graphString);
                write.Close();
                start = new ProcessStartInfo("dot.exe");
                start.Arguments = "-Tjpg \"C:\\Users\\CARLOS\\Documents\\universidad\\4to semestre\\Lenguajes Formales\\Laboratorio\\P.txt\" -o\"C:\\Users\\CARLOS\\Documents\\universidad\\4to semestre\\Lenguajes Formales\\Laboratorio\\ja.jpg";
                Process.Start(start);
            }
            else if (z % 2 == 0)
            {
                File.Delete("C:\\Users\\CARLOS\\Documents\\universidad\\4to semestre\\Lenguajes Formales\\Laboratorio\\ja.jpg");
                string graphString = "";
                graphString += " digraph G {\n";
                graphString += "  start [shape=Mdiamond label=\"" + graphic.getName() + "\"];\n";
                foreach (Continent conty in graphic.getContinents())
                {
                    graphString += " start ->" + conty.getName() + ";\n";
                }
                foreach (Continent contin in graphic.getContinents())
                {
                    graphString += contin.getDot();
                }
                graphString += "}";
                StreamWriter write = new StreamWriter("C:\\Users\\CARLOS\\Documents\\universidad\\4to semestre\\Lenguajes Formales\\Laboratorio\\P.txt");
                write.WriteLine(graphString);
                write.Close();
                start = new ProcessStartInfo("dot.exe");
                start.Arguments = "-Tjpg \"C:\\Users\\CARLOS\\Documents\\universidad\\4to semestre\\Lenguajes Formales\\Laboratorio\\P.txt\" -o\"C:\\Users\\CARLOS\\Documents\\universidad\\4to semestre\\Lenguajes Formales\\Laboratorio\\ja.jpg";
                Process.Start(start);
            }
            else if (z % 2 != 0)
            {
                if (z > 1)
                {
                    File.Delete("C:\\Users\\CARLOS\\Documents\\universidad\\4to semestre\\Lenguajes Formales\\Laboratorio\\ja.png");
                }
                string graphString = "";
                graphString += " digraph G {\n";
                graphString += "  start [shape=Mdiamond label=\"" + graphic.getName() + "\"];\n";
                foreach (Continent conty in graphic.getContinents())
                {
                    graphString += " start ->" + conty.getName() + ";\n";
                }
                foreach (Continent contin in graphic.getContinents())
                {
                    graphString += contin.getDot();
                }
                graphString += "}";
                StreamWriter write = new StreamWriter("C:\\Users\\CARLOS\\Documents\\universidad\\4to semestre\\Lenguajes Formales\\Laboratorio\\P.txt");
                write.WriteLine(graphString);
                write.Close();
                start = new ProcessStartInfo("dot.exe");
                start.Arguments = "-Tpng \"C:\\Users\\CARLOS\\Documents\\universidad\\4to semestre\\Lenguajes Formales\\Laboratorio\\P.txt\" -o\"C:\\Users\\CARLOS\\Documents\\universidad\\4to semestre\\Lenguajes Formales\\Laboratorio\\ja.png";
                Process.Start(start);
            }


        }

        private void button3_Click(object sender, EventArgs e)
        {
            Process.Start("C:\\Users\\CARLOS\\Documents\\universidad\\4to semestre\\Lenguajes Formales\\Laboratorio\\ja.png");
        }

        private void graphiz2(Graphic graphic)
        {
            string graphString = "";
            graphString += " digraph G {\n";
            graphString += "  start [shape=Mdiamond label=\"" + graphic.getName() + "\"];\n";
            foreach (Continent conty in graphic.getContinents())
            {
                graphString += " start ->" + conty.getName() + ";\n";
            }
            foreach (Continent contin in graphic.getContinents())
            {
                graphString += contin.getDot();
            }

            graphString += "}";

            StreamWriter write = new StreamWriter("C:\\Users\\CARLOS\\Documents\\universidad\\4to semestre\\Lenguajes Formales\\Laboratorio\\Pr.txt");
            write.WriteLine(graphString);
            write.Close();
            ProcessStartInfo nuevo = new ProcessStartInfo("dot.exe");
            nuevo.Arguments = "-Tpdf \"C:\\Users\\CARLOS\\Documents\\universidad\\4to semestre\\Lenguajes Formales\\Laboratorio\\Pr.txt\" -o\"C:\\Users\\CARLOS\\Documents\\universidad\\4to semestre\\Lenguajes Formales\\Laboratorio\\prueba.pdf";
            Process.Start(nuevo);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            try
            {
                pictureBox1.Visible = true;
                if ((z-1) == 0)
                {
                    pictureBox1.Image = Image.FromFile("C:\\Users\\CARLOS\\Documents\\universidad\\4to semestre\\Lenguajes Formales\\Laboratorio\\ja.jpg");
                }
                else if ((z-1) % 2 == 0)
                {
                    pictureBox1.Image = Image.FromFile("C:\\Users\\CARLOS\\Documents\\universidad\\4to semestre\\Lenguajes Formales\\Laboratorio\\ja.jpg");
                }
                else if ((z-1) % 2 != 0)
                {
                    pictureBox1.Image = Image.FromFile("C:\\Users\\CARLOS\\Documents\\universidad\\4to semestre\\Lenguajes Formales\\Laboratorio\\ja.png");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("No se ha analizado ningún archivo");
            }    
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            if (z == 0)
            {
                MessageBox.Show("No se ha analizado ningún archivo");
            }
            else
            {
                graphiz2(test);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (z == 0)
            {
                MessageBox.Show("No se ha analizado ningún archivo");
            }
            else
            {
               
                Process.Start("C:\\Users\\CARLOS\\Documents\\universidad\\4to semestre\\Lenguajes Formales\\Laboratorio\\prueba.pdf");
            }
        }

        private void calcularPais(Graphic flag)
        {
            List<Country> paises = new List<Country>();
            
            foreach (Continent c in flag.getContinents())
            {
                foreach(Country cc in c.getCountries())
                {
                    c.getCountries().Min();
                }
            }
        }
    }
}
    