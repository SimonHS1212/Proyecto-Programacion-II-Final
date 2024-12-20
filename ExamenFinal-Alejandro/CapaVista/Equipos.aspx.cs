using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExamenFinal_Alejandro.CapaVista
{
    public partial class Equipos : System.Web.UI.Page
    {
        // Cadena de conexión a la base de datos
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarEquipos(); // Cargar los equipos al cargar la página
            }
        }

        // Método para cargar los equipos en el GridView
        private void CargarEquipos()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("ConsultarEquipos", connection);
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
                    lblMensaje.Text = "Error al cargar equipos: " + ex.Message;
                }
            }
        }

        // Método para agregar un equipo
        protected void bagregar_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("InsertarEquipo", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@TipoEquipo", ttipo.Text.Trim());
                command.Parameters.AddWithValue("@Modelo", tmodelo.Text.Trim());
                command.Parameters.AddWithValue("@UsuarioID", string.IsNullOrEmpty(tusuarioid.Text) ? (object)DBNull.Value : int.Parse(tusuarioid.Text.Trim()));

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    lblMensaje.Text = "Equipo agregado correctamente.";
                    CargarEquipos();
                }
                catch (Exception ex)
                {
                    lblMensaje.Text = "Error al agregar equipo: " + ex.Message;
                }
            }
        }

        // Método para modificar un equipo
        protected void bmodificar_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("ActualizarEquipo", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@EquipoID", int.Parse(tcodigo.Text.Trim()));
                command.Parameters.AddWithValue("@TipoEquipo", ttipo.Text.Trim());
                command.Parameters.AddWithValue("@Modelo", tmodelo.Text.Trim());
                command.Parameters.AddWithValue("@UsuarioID", string.IsNullOrEmpty(tusuarioid.Text) ? (object)DBNull.Value : int.Parse(tusuarioid.Text.Trim()));

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    lblMensaje.Text = "Equipo modificado correctamente.";
                    CargarEquipos();
                }
                catch (Exception ex)
                {
                    lblMensaje.Text = "Error al modificar equipo: " + ex.Message;
                }
            }
        }

        // Método para borrar un equipo
        protected void bborrar_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("EliminarEquipo", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@EquipoID", int.Parse(tcodigo.Text.Trim()));

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    lblMensaje.Text = "Equipo eliminado correctamente.";
                    CargarEquipos();
                }
                catch (Exception ex)
                {
                    lblMensaje.Text = "Error al eliminar equipo: " + ex.Message;
                }
            }
        }

        // Método para seleccionar un equipo desde el GridView
        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = GridView1.SelectedRow;

            tcodigo.Text = row.Cells[0].Text; // ID Equipo
            ttipo.Text = row.Cells[1].Text;   // Tipo de Equipo
            tmodelo.Text = row.Cells[2].Text; // Modelo
            tusuarioid.Text = row.Cells[3].Text; // ID Usuario
        }
    }
}