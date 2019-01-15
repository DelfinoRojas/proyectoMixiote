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

        public InicializarMesas() 
        {
            InitializeComponent();
           
            controladorSistema = new ControlSistema(); //Se inicializa la variable que conecta con el controlador 

            obtenerFormacion(); // Comienza el proceso de creación de mesas y asignación de estado
            rbpartefrontal.Checked = true;

        }

        private void obtenerFormacion()
        {
            int[] formacion = controladorSistema.getFormacion();

            limpiarMesas();

            GroupBox elegido = new GroupBox();
            int nmesas = formacion[0]; // Limite de mesas a crear
            int iconteo = 1; // Inicio de la numeración de mesas
            elegido = gbfrontal;
            crearMesas(elegido, iconteo, nmesas);

            elegido = gbjardin;
            iconteo = 28;
            nmesas += formacion[1]+ formacion[0];

            crearMesas(elegido, iconteo, nmesas);
        }

        private void limpiarMesas()
        {
            gbfrontal.Controls.Clear();
            gbjardin.Controls.Clear();
        }

        
        private void crearMesas(GroupBox gb, int inicioConteo, int nmesas)
        {
            int distxlabel = 15; //Posición en x de las etiquetas (libre/Ocupado)
            int distxbutton = 90; //Posición en x de los botones (mesa n)

            int distylabel = 45; //Altura de las etiquetas (libre/Ocupado)
            int distybutton = 38; //Altura de los botones (mesa n)

            for (int x = inicioConteo; x <= nmesas; x++)
            {
                Label lbl = new Label();
                lbl.Location = new Point(distxlabel, distylabel);
                lbl.Width = 40;
                lbl.Height = 18;
                lbl.Font = new Font(lbl.Font.FontFamily, 11);
                lbl.ForeColor = Color.FromArgb(0, 192, 0);
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
                btn.Name = x+"";
                btn.Text = "Mesa " + x;

                btn.Click += new EventHandler(handlerComun_Click);
                gb.Controls.Add(lbl);
                gb.Controls.Add(btn);
                // Para desplazar los elementos a la derecha se aumenta el valor de las (x)
                // y se inicializa el valor de las (y)
                if (x % 9 == 0) 
                {
                    distxlabel += 180;
                    distxbutton += 180;

                    distylabel = 45;
                    distybutton = 38;
                }
            }
        }

        private void btnfijar_Click(object sender, EventArgs e)
        {
            GroupBox elegido = new GroupBox();
            int nmesas = Convert.ToInt32(txtmesas.Text); // Limite de mesas a crear
            int iconteo = 0; // Inicio de la numeración de mesas

            if (rbpartefrontal.Checked)
            {
                elegido = gbfrontal;
                iconteo = 1;
            }
            else
            {
                elegido = gbjardin;
                iconteo = 28;
                nmesas += 27;
            }
            elegido.Controls.Clear();

            crearMesas(elegido, iconteo, nmesas);
        }


        // Método que corresponde al evento click de los botones generados con código
        private void handlerComun_Click(object sender, EventArgs e) //Evento clic de los botones de las mesas
        {
            this.Hide();

            Button boton = sender as Button; // Esta línea permite acceder posteriormente a las propiedades del botón
            CreacionDeCuenta form = new CreacionDeCuenta(boton.Name); // Se envía el nombre del botón al formulario []
            form.Visible=true;
        }        

        private void salir(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
            this.Close();
            Application.Exit();
        }
    }
}
