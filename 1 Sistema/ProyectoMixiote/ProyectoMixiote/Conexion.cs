using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;
using System.Windows.Forms;

namespace ProyectoMixiote
{
    class Conexion
    {
        SqlConnection cnx = new SqlConnection(
            "Data Source = DESKTOP-A2AJA16\\SQLEXPRESSD12; " +
            "Initial Catalog = casaMixiote; " +
            "Integrated Security = True"
         );

        public SqlConnection conectar()
        {
            try
            {
                cnx.Open();
                MessageBox.Show("Conexión establecida con SqlServer");
            }
            catch (Exception e)
            {
                MessageBox.Show("Error al conectar con SqlServer");
            }
            return cnx;
        }

        public void cerrar()
        {
            cnx.Close();
            //MessageBox.Show("Se cerró la conexión SqlServer");
        }
    }
}
