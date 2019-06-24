using System;
using System.Web.UI.WebControls;

namespace WEB_MGE
{
    public partial class Menu : System.Web.UI.Page
    {
        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void projetoClick(object sender, EventArgs e)
        {
            Button botaoClicado = sender as Button;

            Session["projetoConectado"] = botaoClicado.Text;
            Session["perfilConectado"] = "USU";
            Response.Redirect("Default.aspx");
        }
        #endregion
    }
}