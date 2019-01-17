using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoMixiote
{
    public partial class InicializarMesas : Form
    {
        private ControlSistema controladorSistema; //Variable global que conecta con el controlador general del Sistema
        int[] formacion = new int[2]; //Almacena el número de mesas de ambas zonas del restaurante

        public InicializarMesas(int op) 
        {
            InitializeComponent();
           
            controladorSistema = new ControlSistema(); //Se inicializa la variable que conecta con el controlador 

            if (op==1)//El llamado se hace al iniciar el sistema. Se crea la tabla Mesa
            {
                controladorSistema.createTableMesa();
            }
           
            obtenerFormacion(); // Comienza el proceso de creación de mesas y asignación de estado
            rbpartefrontal.Checked = true;
            
        }

        private void obtenerFormacion()
        {
            formacion = controladorSistema.getFormacion();
            List<string> mesas = controladorSistema.getMesasOcupadas();

            //Console.Write("\n-----Número de mesas  "+mesas.Count+ "\n");

            /*
            for (int x=0;x<mesas.Count;x++)
            {
                MessageBox.Show("Mesa n. "+x+":  "+mesas[x]);
            }
            ------------------------------------------------------
            mesas.ForEach(delegate (String name)
            {
                Console.WriteLine(name+"\n");
            });
            */
            //string[] ocupadas= mesas.ToArray();

            limpiarMesas();

            crearMesas(formacion,mesas);
        }

        private void limpiarMesas()
        {
            gbfrontal.Controls.Clear();
            gbjardin.Controls.Clear();
            gbfrontal.Text = "Parte frontal -- Max(27)";
            gbjardin.Text = "Jardín -- Max(18)";
        }

        
        private void crearMesas(int []formacion, List<string> ocupadas)
        {
            int distxlabel = 15; //Posición en x de las etiquetas (libre/Ocupado)
            int distxbutton = 90; //Posición en x de los botones (mesa n)

            int distylabel = 45; //Altura de las etiquetas (libre/Ocupado)
            int distybutton = 38; //Altura de los botones (mesa n)

            int conta = 0; //Permite hacer los recorridos de elementos cada módulo 9

            GroupBox gb = new GroupBox();

            for (int x = 1; x <= formacion[0]+formacion[1]; x++)
            {
                if (x==1) //Se trabaja con el gruopBox parteFronatl
                {
                    gb = gbfrontal;
                    gb.Text += ":  <" + formacion[0] + ">";
                }else if (x==formacion[0]+1){ //Se trabaja con el groupBox jardin
                    gb = gbjardin;
                    gb.Text += ":  <" + formacion[1] + ">";

                    distxlabel = 15;
                    distxbutton = 90;
                    distylabel = 45;
                    distybutton = 38;
                    conta = 0;
                }
                conta++;
                Label lbl = new Label();
                lbl.Location = new Point(distxlabel, distylabel);
                lbl.Width = 40;
                lbl.Height = 18;
                lbl.Font = new Font(lbl.Font.FontFamily, 11);
                
                if (ocupadas[x-1].Equals(""))
                {
                    lbl.ForeColor = Color.FromArgb(0, 192, 0);
                }
                else
                {
                    lbl.ForeColor = Color.FromArgb(192, 0, 0);
                }
                

                distylabel += 34;
                lbl.Name = "lblmesa" + x;
                lbl.Text = "Libre";

                Button btn = new Button();
                btn.Location = new Point(distxbutton, distybutton);
                btn.Width = 80;
                btn.Height = 28;
                btn.Font = new Font(btn.Font.FontFamily, 11);
                distybutton += 34;
                //btn.Name = "btnmesa" + x;
                btn.Name = "Mesa_" + x;
                btn.Text = "Mesa " + x;

                btn.Click += new EventHandler(handlerComun_Click);
                gb.Controls.Add(lbl);
                gb.Controls.Add(btn);
                // Para desplazar los elementos a la derecha se aumenta el valor de las (x)
                // y se inicializa el valor de las (y)
                if (conta % 9 == 0) 
                {
                    distxlabel += 180;
                    distxbutton += 180;

                    distylabel = 45;
                    distybutton = 38;
                }
            }
        }

        private void btnmas_Click(object sender, EventArgs e)
        {
            int nmesas = Convert.ToInt32(txtmesas.Text); // Se obtiene el número que contiene el textbox txtmesas
            nmesas += 1;
            txtmesas.Text = nmesas + "";
        }

        private void btnmenos_Click(object sender, EventArgs e)
        {
            int nmesas = Convert.ToInt32(txtmesas.Text); // Se obtiene el número que contiene el textbox txtmesas
            nmesas -= 1;
            txtmesas.Text = nmesas + "";
        }

        private void btnfijar_Click(object sender, EventArgs e)
        {
            int nmesas = Convert.ToInt32(txtmesas.Text); // Se obtiene el número que contiene el textbox txtmesas
            int option = 0;
            int parametro = formacion[0];
            if (rbjardin.Checked)
            {
                option = 1;
                parametro = formacion[1];
            }
            controladorSistema.setFormacion(option,nmesas,parametro);
            controladorSistema.createTableMesa();
            obtenerFormacion();
        }


        // Método que corresponde al evento click de los botones generados con código
        private void handlerComun_Click(object sender, EventArgs e) //Evento clic de los botones de las mesas
        {
            this.Hide();

            Button boton = sender as Button; // Esta línea permite acceder posteriormente a las propiedades del botón
            CreacionDeCuenta form = new CreacionDeCuenta(1,boton.Name); // Se envía el nombre del botón al formulario []
            form.Visible=true;
        }        

        private void salir(object sender, FormClosedEventArgs e)
        {
            controladorSistema.dropTableMesa();
            this.Dispose();
            this.Close();
            Application.Exit();
        }

        //Método que valida el cambio en el estado "Check" del radioButton Frontal
        private void rbpartefrontal_CheckedChanged(object sender, EventArgs e)
        {
            if (rbpartefrontal.Checked)
            {
                txtmesas.Text = formacion[0]+"";
            }
            else
            {
                txtmesas.Text = formacion[1]+"";
            }
        }
    }
}
