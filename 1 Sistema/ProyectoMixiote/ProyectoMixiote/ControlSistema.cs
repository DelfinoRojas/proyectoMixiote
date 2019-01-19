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

        public int[] getFormacion()
        {
            string query = "SELECT parteFrontal,jardin FROM FormacionMesa"; //Nombre del procedimiento almacenado
            cnx = objeConexion.conectar();
            SqlCommand comando = new SqlCommand(query,cnx);
            SqlDataReader leer = comando.ExecuteReader();

            int []datos=new int[2];

            try
            {
                if (leer.HasRows)
                {
                    while (leer.Read())
                    {
                        //MessageBox.Show(leer.FieldCount.ToString());  Devuelve el número de campos del registro
                        datos[0] = leer.GetInt32(0);
                        datos[1] = leer.GetInt32(1);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Console.Write(ex);
                MessageBox.Show("Falló la consulta getFormacion");
            }

            objeConexion.cerrar();
            return datos;
        }

        public void setFormacion(int option,int mesas,int parametro)
        {
            string query = "";
            if (option==0)
            {
                query = "UPDATE FormacionMesa SET parteFrontal = '"+ mesas +"' WHERE parteFrontal ='"+parametro+"'"; 
            }
            else if(option==1)
            {
                query = "UPDATE FormacionMesa SET jardin = '" + mesas + "' WHERE jardin ='" + parametro + "'";

            }            
            cnx = objeConexion.conectar();
            SqlCommand comando = new SqlCommand(query, cnx);
            try
            {
                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                System.Console.Write(ex);
                MessageBox.Show("Falló la consulta setFormacion");
            }
            objeConexion.cerrar();
        }

        public void createTableMesa()
        {

            cnx = objeConexion.conectar();
            SqlCommand comando = new SqlCommand("SPsistema_CrearEstadoMesa", cnx); //Se ejecuta el procedimiento almacenado
            comando.CommandType = CommandType.StoredProcedure;
            try
            {
                comando.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                //System.Console.Write(ex);
                MessageBox.Show("Falló el procedimiento createTableMesa");
            }

            objeConexion.cerrar();
        }

        public void dropTableMesa()
        {
            cnx = objeConexion.conectar();
            SqlCommand comando = new SqlCommand("SPsistema_EliminarTableMesa", cnx); //Se ejecuta el procedimiento almacenado
            comando.CommandType = CommandType.StoredProcedure;
            try
            {
                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                //System.Console.Write(ex);
                MessageBox.Show("Falló la consulta dropTableMesa");
            }

            objeConexion.cerrar();
        }

        public List<string> getMesasOcupadas()
        {
            string query = "SELECT estado FROM Mesa";
            cnx = objeConexion.conectar();
            SqlCommand comando = new SqlCommand(query, cnx);

            List<string> lista = new List<string>();

            try
            {
                SqlDataReader leer = comando.ExecuteReader();
                if (leer.HasRows)
                {
                    while (leer.Read())
                    {
                        lista.Add(leer["estado"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                System.Console.Write(ex);
                MessageBox.Show("Falló la consulta getMesasOcupadas");
            }

            objeConexion.cerrar();

            return lista;
        }

        public string getFolioDeMesa(string nombreMesa)
        {
            string query = "SELECT estado FROM Mesa WHERE nombreMesa = '"+ nombreMesa + "'";
            cnx = objeConexion.conectar();
            SqlCommand comando = new SqlCommand(query, cnx);

            string folio = "";
            try
            {
                SqlDataReader leer = comando.ExecuteReader();
                if (leer.HasRows)
                {
                    while (leer.Read())
                    {
                        folio=leer["estado"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                System.Console.Write(ex);
                MessageBox.Show("Falló la consulta getFolioDeMesa");
            }

            objeConexion.cerrar();
            return folio;
        }

        public void getcomboMesas(ComboBox cbo)
        {
            int[] formacion = getFormacion(); // Se obtiene el número de mesas de cada área

            string query = "SELECT nombreMesa FROM Mesa WHERE estado=''";
            cnx = objeConexion.conectar();
            SqlCommand comando = new SqlCommand(query, cnx);

            try
            {
                SqlDataReader leer = comando.ExecuteReader();
                int conta = 1;
                if (leer.HasRows)
                {
                    while (leer.Read())
                    {
                        if (conta<=formacion[0]) //Identifica las mesas de la parte frontal
                        {
                            cbo.Items.Add("(F)  "+leer["nombreMesa"].ToString());
                            if (conta==formacion[0])
                            {
                                cbo.Items.Add(""); //Crea una separación dentro del combo
                            }
                        }
                        else //Identifica las mesas del jardin
                        {
                            cbo.Items.Add("(J)  " + leer["nombreMesa"].ToString());
                        }
                        conta++;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Console.Write(ex);
                MessageBox.Show("Falló la consulta getcomboMesas");
            }

            objeConexion.cerrar();
        }

        public void getComboMeseros(ComboBox cbo)
        {
            string query = "SELECT nombre FROM Empleado e INNER JOIN TipoPersonal tp" +
                " ON e.idPuesto=tp.idPuesto WHERE tp.puesto='Mesero'";
            cnx = objeConexion.conectar();
            SqlCommand comando = new SqlCommand(query, cnx);

            try
            {
                SqlDataReader leer = comando.ExecuteReader();
                if (leer.HasRows)
                {
                    while (leer.Read())
                    {
                        cbo.Items.Add(leer["nombre"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                System.Console.Write(ex);
                MessageBox.Show("Falló la consulta getComboMeseros");
            }

            objeConexion.cerrar();
        }

        public Folio verifcarExistenciaCuenta(string folioMesa)
        {
            string query = "SELECT * FROM Folio WHERE folioVenta = '" + folioMesa + "'";
            cnx = objeConexion.conectar();
            SqlCommand comando = new SqlCommand(query, cnx);

            Folio datosFolio = new Folio();
            try
            {
                SqlDataReader leer = comando.ExecuteReader();
                if (leer.HasRows)
                {
                    while (leer.Read())
                    {
                        datosFolio.FolioVenta1=leer["folioVenta"].ToString();
                        datosFolio.Mesa = leer["mesa"].ToString();
                        datosFolio.IdEmpleado = leer["idEmpleado"].ToString();
                        datosFolio.NPersonas = Convert.ToInt32(leer["nPersonas"]);
                        datosFolio.NCuentas = Convert.ToInt32(leer["nCuentas"]);
                        datosFolio.FechaHoy = Convert.ToDateTime(leer["fechaHoy"]).ToString("dd/MM/yyyy");
                        datosFolio.HoraEntrada = Convert.ToDateTime(leer["horaEntrada"]).ToString("hh:mm");
                        datosFolio.HoraSalida = Convert.ToDateTime(leer["horaSalida"]).ToString("hh:mm");

                        //DateTime date = Convert.ToDateTime(18/01/2019);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Console.Write(ex);
                MessageBox.Show("Falló la consulta verifcarExistenciaCuenta");
            }

            objeConexion.cerrar();

            return datosFolio;
        }

    }
}
