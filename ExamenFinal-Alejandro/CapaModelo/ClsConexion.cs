using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ExamenFinal_Alejandro.CapaModelo
{
    public class ClsConexion
    {
        public static SqlConnection obtenerConexion()
        {
            string s = System.Configuration.ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
            SqlConnection conexion = new SqlConnection(s);
            conexion.Open();
            return conexion;
        }
    }
}