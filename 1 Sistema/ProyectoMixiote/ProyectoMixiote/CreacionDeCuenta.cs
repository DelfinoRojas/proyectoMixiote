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

            establecerInfoCuenta(folio);

        }

        private void llenarComboMesas()
        {
            controladorSistema.getcomboMesas(cbomesas);
        }

        private void llenarComboMeseros()
        {

        }

        public void establecerInfoCuenta(string folio)
        {
            MessageBox.Show("establecerInfoCuenta");
        }



        private void salir(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
            InicializarMesas form = new InicializarMesas(2);
            form.Visible = true;
        }

        private void CreacionDeCuenta_Load(object sender, EventArgs e)
        {
            llenarComboMesas();
            llenarComboMeseros();
        }
    }
}
