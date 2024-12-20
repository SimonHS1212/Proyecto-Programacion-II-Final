using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExamenFinal_Alejandro.CapaVista
{
    public partial class Usuarios : System.Web.UI.Page
    {
        // Cadena de conexión a la base de datos
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarUsuarios(); // Cargar usuarios al cargar la página
            }
        }

        // Método para cargar los usuarios en el GridView
        private void CargarUsuarios()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("ConsultarUsuarios", connection);
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
                    lblMensaje.Text = "Error al cargar usuarios: " + ex.Message;
                }
            }
        }

        // Método para agregar un usuario
        protected void bagregar_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("InsertarUsuario", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@Nombre", tnombre.Text.Trim());
                command.Parameters.AddWithValue("@CorreoElectronico", tcorreo.Text.Trim());
                command.Parameters.AddWithValue("@Telefono", ttelefono.Text.Trim());

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    lblMensaje.Text = "Usuario agregado correctamente.";
                    CargarUsuarios(); // Refrescar el GridView
                }
                catch (Exception ex)
                {
                    lblMensaje.Text = "Error al agregar usuario: " + ex.Message;
                }
            }
        }

        // Método para modificar un usuario
        protected void bmodificar_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("ActualizarUsuario", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@UsuarioID", int.Parse(tcodigo.Text.Trim()));
                command.Parameters.AddWithValue("@Nombre", tnombre.Text.Trim());
                command.Parameters.AddWithValue("@CorreoElectronico", tcorreo.Text.Trim());
                command.Parameters.AddWithValue("@Telefono", ttelefono.Text.Trim());

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    lblMensaje.Text = "Usuario modificado correctamente.";
                    CargarUsuarios(); // Refrescar el GridView
                }
                catch (Exception ex)
                {
                    lblMensaje.Text = "Error al modificar usuario: " + ex.Message;
                }
            }
        }

        // Método para borrar un usuario
        protected void bborrar_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("EliminarUsuario", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@UsuarioID", int.Parse(tcodigo.Text.Trim()));

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    lblMensaje.Text = "Usuario eliminado correctamente.";
                    CargarUsuarios(); // Refrescar el GridView
                }
                catch (Exception ex)
                {
                    lblMensaje.Text = "Error al eliminar usuario: " + ex.Message;
                }
            }
        }

        // Método para seleccionar un usuario desde el GridView
        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = GridView1.SelectedRow;

            tcodigo.Text = row.Cells[0].Text; // ID Usuario
            tnombre.Text = row.Cells[1].Text; // Nombre
            tcorreo.Text = row.Cells[2].Text; // Correo Electrónico
            ttelefono.Text = row.Cells[3].Text; // Teléfono
        }
    }
}