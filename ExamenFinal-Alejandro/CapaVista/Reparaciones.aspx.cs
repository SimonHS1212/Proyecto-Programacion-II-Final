using System.Data.SqlClient;
using System.Data;
using System.Web.UI.WebControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;


namespace ExamenFinal_Alejandro.CapaVista
{
    public partial class Reparaciones : System.Web.UI.Page
    {
        
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarReparaciones(); 
            }
        }

       
        private void CargarReparaciones()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("ConsultarReparaciones", connection);
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
                    lblMensaje.Text = "Error al cargar reparaciones: " + ex.Message;
                }
            }
        }

        
        protected void bagregar_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("InsertarReparacion", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@EquipoID", int.Parse(tequipoid.Text.Trim()));
                command.Parameters.AddWithValue("@Estado", testado.Text.Trim());

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    lblMensaje.Text = "Reparación agregada correctamente.";
                    CargarReparaciones();
                }
                catch (Exception ex)
                {
                    lblMensaje.Text = "Error al agregar reparación: " + ex.Message;
                }
            }
        }

       
        protected void bmodificar_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("ActualizarReparacion", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@ReparacionID", int.Parse(treparacionid.Text.Trim()));
                command.Parameters.AddWithValue("@EquipoID", int.Parse(tequipoid.Text.Trim()));
                command.Parameters.AddWithValue("@Estado", testado.Text.Trim());

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    lblMensaje.Text = "Reparación modificada correctamente.";
                    CargarReparaciones();
                }
                catch (Exception ex)
                {
                    lblMensaje.Text = "Error al modificar reparación: " + ex.Message;
                }
            }
        }

        
        protected void bborrar_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("EliminarReparacion", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@ReparacionID", int.Parse(treparacionid.Text.Trim()));

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    lblMensaje.Text = "Reparación eliminada correctamente.";
                    CargarReparaciones();
                }
                catch (Exception ex)
                {
                    lblMensaje.Text = "Error al eliminar reparación: " + ex.Message;
                }
            }
        }

        
        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = GridView1.SelectedRow;

            treparacionid.Text = row.Cells[0].Text; 
            tequipoid.Text = row.Cells[1].Text;    
            testado.Text = row.Cells[3].Text;     
        }
    }
}