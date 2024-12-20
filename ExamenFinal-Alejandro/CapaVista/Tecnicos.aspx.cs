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
    public partial class Tecnicos : System.Web.UI.Page
    {
        // Cadena de conexión a la base de datos
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarTecnicos(); // Cargar técnicos al cargar la página
            }
        }

        // Método para cargar técnicos en el GridView
        private void CargarTecnicos()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("ConsultarTecnicos", connection);
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
                    lblMensaje.Text = "Error al cargar técnicos: " + ex.Message;
                }
            }
        }

        // Método para agregar un técnico
        protected void bagregar_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("InsertarTecnico", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@Nombre", tnombre.Text.Trim());
                command.Parameters.AddWithValue("@Especialidad", tespecialidad.Text.Trim());

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    lblMensaje.Text = "Técnico agregado correctamente.";
                    CargarTecnicos();
                }
                catch (Exception ex)
                {
                    lblMensaje.Text = "Error al agregar técnico: " + ex.Message;
                }
            }
        }

        // Método para modificar un técnico
        protected void bmodificar_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("ActualizarTecnico", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@TecnicoID", int.Parse(ttecnicoid.Text.Trim()));
                command.Parameters.AddWithValue("@Nombre", tnombre.Text.Trim());
                command.Parameters.AddWithValue("@Especialidad", tespecialidad.Text.Trim());

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    lblMensaje.Text = "Técnico modificado correctamente.";
                    CargarTecnicos();
                }
                catch (Exception ex)
                {
                    lblMensaje.Text = "Error al modificar técnico: " + ex.Message;
                }
            }
        }

        // Método para borrar un técnico
        protected void bborrar_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("EliminarTecnico", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@TecnicoID", int.Parse(ttecnicoid.Text.Trim()));

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    lblMensaje.Text = "Técnico eliminado correctamente.";
                    CargarTecnicos();
                }
                catch (Exception ex)
                {
                    lblMensaje.Text = "Error al eliminar técnico: " + ex.Message;
                }
            }
        }

        // Método para seleccionar un técnico desde el GridView
        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = GridView1.SelectedRow;

            ttecnicoid.Text = row.Cells[0].Text;   // ID Técnico
            tnombre.Text = row.Cells[1].Text;     // Nombre
            tespecialidad.Text = row.Cells[2].Text; // Especialidad
        }
    }
}