using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExamenFinal_Alejandro.CapaVista
{
    public partial class DetallesReparacion : System.Web.UI.Page
    {
        // Cadena de conexión a la base de datos
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarDetallesReparacion(); // Cargar detalles al cargar la página
            }
        }

        // Método para cargar detalles de reparación en el GridView
        private void CargarDetallesReparacion()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("ConsultarDetallesReparacion", connection);
                command.CommandType = CommandType.StoredProcedure;

                try
                {
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                }
                catch (Exception ex)
                {
                    lblMensaje.Text = "Error al cargar detalles: " + ex.Message;
                }
            }
        }

        // Método para agregar un detalle
        protected void bagregar_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("InsertarDetalleReparacion", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@ReparacionID", int.Parse(treparacionid.Text.Trim()));
                command.Parameters.AddWithValue("@Descripcion", tdescripcion.Text.Trim());
                command.Parameters.AddWithValue("@FechaInicio", DateTime.Parse(tfechainicio.Text.Trim()));
                command.Parameters.AddWithValue("@FechaFin", DateTime.Parse(tfechafin.Text.Trim()));

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    lblMensaje.Text = "Detalle agregado correctamente.";
                    CargarDetallesReparacion();
                }
                catch (Exception ex)
                {
                    lblMensaje.Text = "Error al agregar detalle: " + ex.Message;
                }
            }
        }

        // Método para modificar un detalle
        protected void bmodificar_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("ActualizarDetalleReparacion", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@DetalleID", int.Parse(tdetalleid.Text.Trim()));
                command.Parameters.AddWithValue("@ReparacionID", int.Parse(treparacionid.Text.Trim()));
                command.Parameters.AddWithValue("@Descripcion", tdescripcion.Text.Trim());
                command.Parameters.AddWithValue("@FechaInicio", DateTime.Parse(tfechainicio.Text.Trim()));
                command.Parameters.AddWithValue("@FechaFin", DateTime.Parse(tfechafin.Text.Trim()));

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    lblMensaje.Text = "Detalle modificado correctamente.";
                    CargarDetallesReparacion();
                }
                catch (Exception ex)
                {
                    lblMensaje.Text = "Error al modificar detalle: " + ex.Message;
                }
            }
        }

        // Método para borrar un detalle
        protected void bborrar_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("EliminarDetalleReparacion", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@DetalleID", int.Parse(tdetalleid.Text.Trim()));

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    lblMensaje.Text = "Detalle eliminado correctamente.";
                    CargarDetallesReparacion();
                }
                catch (Exception ex)
                {
                    lblMensaje.Text = "Error al eliminar detalle: " + ex.Message;
                }
            }
        }

        // Método para seleccionar un detalle desde el GridView
        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = GridView1.SelectedRow;

            tdetalleid.Text = row.Cells[0].Text;   // ID Detalle
            treparacionid.Text = row.Cells[1].Text; // ID Reparación
            tdescripcion.Text = row.Cells[2].Text; // Descripción
            tfechainicio.Text = row.Cells[3].Text; // Fecha Inicio
            tfechafin.Text = row.Cells[4].Text;   // Fecha Fin
        }
    }
}