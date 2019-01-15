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
        public CreacionDeCuenta(String nombre)
        {
            InitializeComponent();
            MessageBox.Show(nombre);
        }

        private void cerrar(object sender, EventArgs e)
        {
            
        }

        private void cargarform(object sender, EventArgs e)
        {
            String[] nombreMesas = {"Mesa1"};
            asignarMesas(nombreMesas);

        }

        private void asignarMesas(String [] nommesas)
        {

        }

        private void salir(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
            InicializarMesas form = new InicializarMesas();
            form.Visible = true;
        }
    }
}
