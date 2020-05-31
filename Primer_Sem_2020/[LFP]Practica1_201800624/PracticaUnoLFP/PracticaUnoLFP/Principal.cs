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

namespace PracticaUnoLFP
{
    public partial class Principal : Form
    {
       
        String ruta = "";
        String info;
        int contador;
        TabPage f;
              List<RichTextBox> texto = new List<RichTextBox>();
        public Principal()
        {
            InitializeComponent();
        }



        private void button3_Click(object sender, EventArgs e)

        {
            treeView1.Nodes.Clear();
            pictureBox1.Visible = false;
            label1.Text = "";
            int contador = 1;
            String infos = "";
            foreach (Control item in tabControl1.SelectedTab.Controls)
            {
                if (item.GetType().Equals(typeof(RichTextBox)))
                {

                    infos = item.Text;
                }
            }
            analisadorLexico analisadorLex = new analisadorLexico();
            LinkedList<Token> listaTokens = analisadorLex.scanner(infos);
            LinkedList<Error> listaErrores = analisadorLex.Errores();

            if (analisadorLex.errores < 1)
            {
                StreamWriter write = new StreamWriter("C:\\Users\\CARLOS\\Downloads\\Lista de Tokens.html");
                write.WriteLine("<html>");
                write.WriteLine("<head><h1> LISTA DE TOKENS</h1></head>");
                write.WriteLine("<body>");
                write.WriteLine("<p><h3> Aqui se muestra la lista de tokens del archivo: " + ruta + "  que fue previamente analisado </h3></p>");
                write.WriteLine("<p></p>");
                write.WriteLine("<table border='1'>");
                write.WriteLine("<tr>");
                write.WriteLine("<td>No.</td><td>Linea</td><td>Lexema</td><td>Token</td>");
                foreach (Token item in listaTokens)
                {
                    write.WriteLine("<tr>");
                    write.WriteLine("<td>" + contador + "</td>");
                    write.WriteLine("<td>" + item.getLinea() + "</td>");
                    write.WriteLine("<td>" + item.getValue() + "</td>");
                    write.WriteLine("<td>" + item.getType() + "</td>");
                    write.WriteLine("</tr>");

                    contador++;
                }
                write.WriteLine("</table>");
                write.WriteLine("<p></p>");
                write.WriteLine("<p><h4>Se obtuvo un total de:" + (contador - 1) + " Tokens. </h4> </p>");
                write.WriteLine("<p><h3>No hubieron errores en el analisis del archivo</h3></p>");
                write.WriteLine("</body>");
                write.WriteLine("</html>");
                write.Close();

                Process.Start("C:\\Users\\CARLOS\\Downloads\\Lista de Tokens.html");

                Prueba ppp = new Prueba();
                var nodos = ppp.planes(listaTokens);
                Arbol(nodos);

                Negrita(nodos);
            }
            else
            {
                StreamWriter write = new StreamWriter("C:\\Users\\CARLOS\\Downloads\\Lista de Tokens.html");
                write.WriteLine("<html>");
                write.WriteLine("<head><h1> LISTA DE TOKENS</h1></head>");
                write.WriteLine("<body>");
                write.WriteLine("<p><h4> Aqui se muestra la lista de tokens del archivo: " + ruta + "  que fue previamente analisado </h4></p>");
                write.WriteLine("<p></p>");
                write.WriteLine("<table border='1'>");
                write.WriteLine("<tr>");
                write.WriteLine("<td>No.</td><td>Linea</td><td>Lexema</td><td>Token</td>");
                foreach (Token item in listaTokens)
                {
                    write.WriteLine("<tr>");
                    write.WriteLine("<td>" + contador + "</td>");
                    write.WriteLine("<td>" + item.getLinea() + "</td>");
                    write.WriteLine("<td>" + item.getValue() + "</td>");
                    write.WriteLine("<td>" + item.getType() + "</td>");
                    write.WriteLine("</tr>");

                    contador++;
                }
                write.WriteLine("</table>");
                write.WriteLine("<p></p>");
                write.WriteLine("<p><h4>Se obtuvo un total de:" + (contador - 1) + " Tokens. </h4> </p>");

                write.WriteLine("<p></p>");
                write.WriteLine("<p><h2> Aqui se muestra la lista de errores del archivo: " + ruta + "  que fue previamente analisado </h2></p>");
                write.WriteLine("<p></p>");
                write.WriteLine("<table border='1'>");
                write.WriteLine("<tr>");
                write.WriteLine("<td>No.</td><td>Linea</td><td>Columa</td><td>Caracter</td><td>Descripcion</td>");
                contador = 1;
                foreach (var item in listaErrores)
                {
                    write.WriteLine("<tr>");
                    write.WriteLine("<td>" + contador + "</td>");
                    write.WriteLine("<td>" + item.getLinea() + "</td>");
                    write.WriteLine("<td>" + (item.getColumna()-1) + "</td>");
                    write.WriteLine("<td>" + item.getError() + "</td>");
                    write.WriteLine("<td>" + item.getDes() + "</td>");
                    write.WriteLine("</tr>");

                    contador++;
                }
                write.WriteLine("</table>");
                write.WriteLine("<p></p>");
                write.WriteLine("<p><h3>Hubo un total de: " + analisadorLex.errores + " errores.</h3></p>");
                write.WriteLine("<p></p>");
                write.WriteLine("</body>");
                write.WriteLine("</html>");
                write.Close();

                Process.Start("C:\\Users\\CARLOS\\Downloads\\Lista de Tokens.html");



            }




        }

        private void openActivities(object sender, MouseEventArgs e)
        {
            analisadorLexico analisadorLex = new analisadorLexico();

            LinkedList<Token> listaTokens = analisadorLex.scanner(info);
       
        }


        private void button5_Click(object sender, EventArgs e)
        {
            analisadorLexico analisadorLex = new analisadorLexico();
            Prueba ppp = new Prueba();
            LinkedList<Token> tokenLista = analisadorLex.scanner(info);
            var nodos = ppp.planes(tokenLista);
            Arbol(nodos);
           
            Negrita(nodos);
        }

        private void Arbol(List<Planificador> planificar)
        {
            foreach (var item in planificar)
            {
                TreeNode planNodo = new TreeNode(item.nombre);
                foreach (var anio in item.anio)
                {
                    TreeNode anioNodo = new TreeNode(anio.año.ToString());
                    foreach (var mes in anio.mes)
                    {
                        TreeNode mesNodo = new TreeNode(mes.mes.ToString());
                        mesNodo.Tag = mes;
                        foreach (var dia in mes.dia)
                        {
                            TreeNode diaNodo = new TreeNode(dia.dia.ToString());
                            TreeNode des = new TreeNode(dia.descripcion.ToString());
                            TreeNode image = new TreeNode(dia.ruta.ToString());
                            diaNodo.Tag = dia;
                            mesNodo.Nodes.Add(diaNodo);
                            mesNodo.LastNode.Nodes.Add(des);
                            
                            mesNodo.LastNode.LastNode.Nodes.Add(image);
                        }
                        anioNodo.Nodes.Add(mesNodo);
                    }
                    planNodo.Nodes.Add(anioNodo);
                }
                treeView1.Nodes.Add(planNodo);
            }
        }

        private void Negrita(List<Planificador> Calendario)
        {

           
            DateTime[] fecha = new DateTime[100];
           
            int i = 0;
            foreach (var item in Calendario)
            {
                foreach (var añoo in item.anio)
                {
                    foreach (var mess in añoo.mes)
                    {
                        foreach (var diaa in mess.dia)
                        {
                           fecha[i] = new DateTime( añoo.año, mess.mes, diaa.dia);
                            Console.WriteLine(fecha[i].ToString());

                            i++;
                        }
                    }
                }
            }
            this.monthCalendar1.BoldedDates = fecha;    

        }

     

        

       

        private void interfaz(object sender, EventArgs e)
        {

        }

        private void p(object sender, TreeViewEventArgs e)
        {
            try
            {
                int dia = 0, mes = 0, anio = 0;
                String day, month, year, image, description, ruta;
                day = e.Node.Text;
                month = e.Node.Parent.Text;
                year = e.Node.Parent.Parent.Text;
                description = e.Node.LastNode.Text;
                image = e.Node.LastNode.LastNode.Text;
                dia = Convert.ToInt32(day);
                mes = Convert.ToInt32(month);
                anio = Convert.ToInt32(year);
                DateTime plan = new DateTime(anio, mes, dia);
                ruta = image.ToString().Replace('"', ' ');

                Console.WriteLine("************" + plan.ToString() + "*********" + ruta + "**********" + description);
                label1.Text = description.ToString().Replace('"', ' ').Trim();
             
                pictureBox1.Image = Image.FromFile(ruta);
                pictureBox1.Visible = true; 
            }
            catch
            {
                MessageBox.Show("El número de fecha seleccionado no existe, o la ruta de la imagen de la actividad es incorrecta", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
          
         
        }

        

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Principal_Load(object sender, EventArgs e)
        {

        }

        private void acercaDeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Datos datos = new Datos();
            datos.Visible = true;
        }

        private void cargarArchivoToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if(tabControl1.TabCount != 0)
            {
                OpenFileDialog buscarRuta = new OpenFileDialog();
                buscarRuta.Filter = "Extension .ly|*.ly";
                buscarRuta.Title = "Buscando ruta de archivo";

                if (buscarRuta.ShowDialog() == System.Windows.Forms.DialogResult.OK && buscarRuta.FileName.Length > 0)
                {
                    ruta = buscarRuta.FileName;
                    tabControl1.SelectedTab.Text = buscarRuta.SafeFileName;
                    info = System.IO.File.ReadAllText(ruta);

                    foreach (Control item in tabControl1.SelectedTab.Controls)
                    {
                        if (item.GetType().Equals(typeof(RichTextBox)))
                        {
                         
                            item.Text = info;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Debe abrir una pestaña", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            

          /*  if (buscarRuta.ShowDialog() == DialogResult.OK)
            {
                ruta = buscarRuta.FileName;
            }
            TextReader reader;
            reader = new StreamReader(ruta);
            info = reader.ReadToEnd();
            reader.Close();

            tab.Text = ruta;
            richTextBoxes[vista.SelectedIndex ].Text = info;*/


        }

        private void manualAplicacionToolStripMenuItem_Click(object sender, EventArgs e)
        {
          
                Process.Start("C:\\Users\\CARLOS\\Documents\\[LFP]ManualUsarios_PracticaUno_201800624.pdf");
            
        }

        private void nuevaPestañaToolStripMenuItem_Click(object sender, EventArgs e)
        {
                contador++;
            TabPage f = new ventan(contador);
            tabControl1.TabPages.Add(f);
            
        }

        public class ventan: TabPage
        {
            public ventan(int contador)
            {
                RichTextBox caja = new RichTextBox();
                caja.Dock = DockStyle.Fill;
                this.Controls.Add(caja);
                
        }
        }

        private void guardarArchivoToolStripMenuItem_Click(object sender, EventArgs e)
        {

            String ruta = "C: \\Users\\CARLOS\\Desktop\\PracticaUno.txt";
            String texto = "";
            foreach (Control item in tabControl1.SelectedTab.Controls)
            {
                if (item.GetType().Equals(typeof(RichTextBox)))
                {

                    texto = item.Text;
                }
            }

            File.WriteAllText(ruta, texto);
            MessageBox.Show("Archivo guardado correctamente", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            Process.Start("C:\\Users\\CARLOS\\Desktop\\PracticaUno.txt");
        }
    }
    
}
