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
    public partial class Asignaciones : System.Web.UI.Page
    {
        
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarAsignaciones(); 
            }
        }


        private void CargarAsignaciones()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("ConsultarAsignaciones", connection);
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
                    lblMensaje.Text = "Error al cargar asignaciones: " + ex.Message;
                }
            }
        }


        protected void bagregar_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("InsertarAsignacion", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@ReparacionID", int.Parse(treparacionid.Text.Trim()));
                command.Parameters.AddWithValue("@TecnicoID", int.Parse(ttecnicoid.Text.Trim()));
                command.Parameters.AddWithValue("@FechaAsignacion", DateTime.Parse(tfechaasignacion.Text.Trim()));

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    lblMensaje.Text = "Asignación agregada correctamente.";
                    CargarAsignaciones();
                }
                catch (Exception ex)
                {
                    lblMensaje.Text = "Error al agregar asignación: " + ex.Message;
                }
            }
        }

 
        protected void bmodificar_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("ActualizarAsignacion", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@AsignacionID", int.Parse(tasignacionid.Text.Trim()));
                command.Parameters.AddWithValue("@ReparacionID", int.Parse(treparacionid.Text.Trim()));
                command.Parameters.AddWithValue("@TecnicoID", int.Parse(ttecnicoid.Text.Trim()));
                command.Parameters.AddWithValue("@FechaAsignacion", DateTime.Parse(tfechaasignacion.Text.Trim()));

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    lblMensaje.Text = "Asignación modificada correctamente.";
                    CargarAsignaciones();
                }
                catch (Exception ex)
                {
                    lblMensaje.Text = "Error al modificar asignación: " + ex.Message;
                }
            }
        }

    
        protected void bborrar_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("EliminarAsignacion", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@AsignacionID", int.Parse(tasignacionid.Text.Trim()));

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    lblMensaje.Text = "Asignación eliminada correctamente.";
                    CargarAsignaciones();
                }
                catch (Exception ex)
                {
                    lblMensaje.Text = "Error al eliminar asignación: " + ex.Message;
                }
            }
        }

    
        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = GridView1.SelectedRow;

            tasignacionid.Text = row.Cells[0].Text;   // ID Asignación
            treparacionid.Text = row.Cells[1].Text;  // ID Reparación
            ttecnicoid.Text = row.Cells[2].Text;     // ID Técnico
            tfechaasignacion.Text = row.Cells[3].Text; // Fecha Asignación
        }
    }
}