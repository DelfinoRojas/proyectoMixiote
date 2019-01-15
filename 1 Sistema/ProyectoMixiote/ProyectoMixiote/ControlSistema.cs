using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;
using System.Windows.Forms;
using System.IO;
using System.Data;

namespace ProyectoMixiote
{
    class ControlSistema
    {
        SqlConnection cnx; //Variable conexion que almacenará la conexion retornada
        Conexion objeConexion; // Objeto que permite conectar a la clase Conexion y pedir el retorno de la Conexion

        public ControlSistema()
        {

            objeConexion = new Conexion();
        }

        public int getFormacion(int option)
        {
            string query = "SPsistema_GetFormacionMesa"; //Nombre del procedimiento almacenado
            cnx = objeConexion.conectar();
            SqlCommand comando = new SqlCommand(query,cnx);
            comando.CommandType = CommandType.StoredProcedure; // Se debe importar la librería "using System.Data"

            comando.Parameters.Add("@elec", SqlDbType.Int).Value = option;
            comando.Parameters.Add("@result", SqlDbType.Int).Direction = ParameterDirection.Output;

            int resultado = 0;
            try
            {
                SqlDataReader leer = comando.ExecuteReader();
    
                    resultado = leer.GetInt32(0);
                    MessageBox.Show("1 El valor es " + resultado);
                

                MessageBox.Show("El valor es "+ resultado);
            }
            catch (Exception ex)
            {
                System.Console.Write(ex);
                MessageBox.Show("Falló la consulta getFormacion Control Sistema");
            }

            objeConexion.cerrar();
            return resultado;
        }
    }
}
