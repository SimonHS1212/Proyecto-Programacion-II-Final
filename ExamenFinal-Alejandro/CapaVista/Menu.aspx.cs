using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExamenFinal_Alejandro.CapaVista
{
    public partial class Menu : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        
        }

        protected void btnUsuarios_Click(object sender, EventArgs e)
        {
            Response.Redirect("Usuarios.aspx");
        }

        protected void btnEquipos_Click(object sender, EventArgs e)
        {
            Response.Redirect("Equipos.aspx");
        }
        protected void btnReparaciones_Click(object sender, EventArgs e)
        {
            Response.Redirect("Reparaciones.aspx");
        }

    }
}