﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;
using System.Windows.Forms;
using System.IO;

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
                        //MessageBox.Show("Ya se leyó");
                    }
                }
            }
            catch (Exception ex)
            {
                System.Console.Write(ex);
                //MessageBox.Show("Falló la consulta getFormacion");
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

        public int[] getMesasOcupadas()
        {
            int[] ocupadas= {1,2};

            return ocupadas;
        }
    }
}
