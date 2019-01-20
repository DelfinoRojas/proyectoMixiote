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
                TextBox[] mesasForm = getCajasMesas(); //Se obtienen las cajas del formulario

                for (int x=0;x< nombreMesas.Length;x++)
                {
                    if (!nombreMesas[x].Equals(""))
                    {
                        mesasForm[x].Text = nombreMesas[x];
                    }
                }

                //Obtener el nombre de los meseros asignados al folio de venta
                string[] nombreMeseros = (info.IdEmpleado).ToString().Split('/');
                TextBox[] meserosForm = getCajasMeseros(); //Se obtienen las cajas del formulario

                for (int x = 0; x < nombreMeseros.Length; x++)
                {
                    if (!nombreMeseros[x].Equals(""))
                    {
                        meserosForm[x].Text = nombreMeseros[x];
                    }
                }

                /**/
                txtncomensales.Text = info.NPersonas.ToString();
                cbondivisiones.Text = info.NCuentas.ToString();
                txtfolio.Text = info.FolioVenta1.ToString();

                
                
            }
            else //Se inserta el nombre de la mesa y se predispone a realizar la asignación de la misma
            {
                txtm1.Text = folio;
                //MessageBox.Show("No se encontró información");
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
            limpiarCombos();
            llenarComboMesas();
            llenarComboMeseros();
        }

        private void limpiarCombos()
        {
            cbomesas.Items.Clear();
            cbomeseros.Items.Clear();
        }

        private void limpiarDatosGenerales()
        {
            limpiarCombos();
            txtm1.Text = "";
            txtm2.Text = "";
            txtm3.Text = "";
            txtmesero1.Text = "";
            txtmesero2.Text = "";
        }

        private void btnasignarMesa_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("1Mesas");
            if (!cbomesas.Text.Equals("")) //Se valida que el combo tenga una selección
            {
                //MessageBox.Show("2Mesas");
                TextBox[] mesasForm = getCajasMesas();
                for (int x=0;x<mesasForm.Length;x++) //Se recorren las cajas (mesas) para encontrar una libre
                {
                    if (mesasForm[x].Text.Equals("")) //Se valida la disponibilidad de la caja de texto (mesas)
                    {
                        //MessageBox.Show("3Mesas");
                        mesasForm[x].Text = cbomesas.Text; //Se asigna el nombre de la mesa
                        cbomesas.Items.Remove(mesasForm[x].Text); //Se libera la mesa del combo
                        break;
                    }
                }
            }
        }

        private void btnasignarMesero_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("1Mesero");
            if (!cbomeseros.Text.Equals("")) //Se valida que el combo tenga una selección
            {
                //MessageBox.Show("2Mesero");
                TextBox[] meserosForm = getCajasMeseros();
                for (int x = 0; x < meserosForm.Length; x++) //Se recorren las cajas (meseros) para encontrar una libre
                {
                    if (meserosForm[x].Text.Equals("")) //Se valida la disponibilidad de la caja de texto (meseros)
                    {
                        //MessageBox.Show("3Mesero");
                        meserosForm[x].Text = cbomeseros.Text; //Se asigna el nombre del mesero
                        cbomeseros.Items.Remove(meserosForm[x].Text); //Se libera el mesero del combo
                        break;
                    }
                }
            }
        }

        private TextBox[] getCajasMesas()
        {
            TextBox[] mesasForm = { txtm1, txtm2, txtm3 }; //Hace referencia a las 3 (mesas posibles) cajas de texto del formulario
            return mesasForm;
        }

        private TextBox[] getCajasMeseros()
        {
            TextBox[] meserosForm = { txtmesero1, txtmesero2 }; //Hace referencia a las 2 (meseros posibles) cajas de texto del formulario
            return meserosForm;
        }

        
    }
}
