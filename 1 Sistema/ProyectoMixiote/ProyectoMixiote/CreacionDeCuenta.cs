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
    public partial class CreacionDeCuenta : Form
    {

        private ControlSistema controladorSistema; //Variable global que conecta con el controlador general del Sistema

        public CreacionDeCuenta(int option,string parametro)
        {
            InitializeComponent();

            controladorSistema = new ControlSistema(); //Se inicializa la variable que conecta con el controlador 

            string folio = ""; //Variable que almacenará el folio de la mesa en cuestión

            if (option==1)
            {
                folio=controladorSistema.getFolioDeMesa(parametro); //Se envía el nombre de la mesa y se recibe el folio de la misma
            }
            else
            {
                folio = parametro; //Se asigna el folio proveniente de un formulario diferente de "InicializarMesas"
            }

            //MessageBox.Show("El folio es: "+folio);
            establecerInfoCuenta(folio);

        }

        private void llenarComboMesas()
        {
            controladorSistema.getcomboMesas(cbomesas);
        }

        private void llenarComboMeseros()
        {
            controladorSistema.getComboMeseros(cbomeseros);
        }

        public void establecerInfoCuenta(string folio)
        {
            if (!folio.Equals("") && folio!=null) //Se obtiene la informacion asociada al folio
            {
                Folio info = controladorSistema.verifcarExistenciaCuenta(folio);
               //Obtener el nombre de las mesas asignadas al folio de venta
                string [] nombreMesas = (info.Mesa).ToString().Split('/'); 
                TextBox [] mesasForm={txtm1,txtm2,txtm3}; //Hace referencia a las 3 cajas de texto del formulario

                for (int x=0;x< nombreMesas.Length;x++)
                {
                    if (!nombreMesas[x].Equals(""))
                    {
                        MessageBox.Show("El nombre de la mesa es: " + nombreMesas[x]);
                        mesasForm[x].Text = nombreMesas[x];
                    }
                }

                //Obtener el nombre de los meseros asignados al folio de venta
                string[] nombreMeseros = (info.IdEmpleado).ToString().Split('/');
                TextBox[] meserosForm = { txtmesero1, txtmesero2}; //Hace referencia a las 2 cajas de texto del formulario

                for (int x = 0; x < nombreMeseros.Length; x++)
                {
                    if (!nombreMeseros[x].Equals(""))
                    {
                        MessageBox.Show("El nombre del mesero es: " + nombreMeseros[x]);
                        meserosForm[x].Text = nombreMeseros[x];
                    }
                }

                MessageBox.Show("Se llenaron las cajas");
            }
            else //Se inserta el nombre de la mesa y se predispone a realizar la asignación de la misma
            {
                txtm1.Text = folio;
                MessageBox.Show("No se encontró información");
            }
        }



        private void salir(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
            InicializarMesas form = new InicializarMesas(2);
            form.Visible = true;
        }

        private void CreacionDeCuenta_Load(object sender, EventArgs e)
        {
            limpiarFormulario();
            llenarComboMesas();
            llenarComboMeseros();
        }

        private void limpiarFormulario()
        {
            cbomesas.Items.Clear();
            cbomeseros.Items.Clear();
            txtm1.Text = "";
            txtm2.Text = "";
            txtm3.Text = "";
            txtmesero1.Text = "";
            txtmesero2.Text = "";
        }
    }
}
