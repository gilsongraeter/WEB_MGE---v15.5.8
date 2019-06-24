using System;
using MySql.Data.MySqlClient;
using System.Data;

namespace WEB_MGE
{
    public partial class Default : System.Web.UI.Page
    {
        #region Atributos
        MySqlConnection conexao = null;
        MySqlDataAdapter adapter = null;
        DataSet dataSet = new DataSet();
        #endregion


        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {            
            conexao = new MySqlConnection(Constantes.STRING_CONEXAO);
            loginError.Visible = false;
            tbUsuario.Focus();
        }


        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            adapter = new MySqlDataAdapter(string.Format("SELECT * FROM USUARIOS WHERE usuario = '{0}' AND senha = '{1}';", tbUsuario.Text, tbSenha.Text), conexao);
            adapter.Fill(dataSet);                                   

            try
            {
                if (dataSet.Tables[0].Rows.Count > 0)
                {
                    Session["usuarioConectado"] = dataSet.Tables[0].Rows[0]["usuario"].ToString();
                    Variaveis_Globais.Usuario = dataSet.Tables[0].Rows[0]["usuario"].ToString();
                    Variaveis_Globais.PerfilUsuario = dataSet.Tables[0].Rows[0]["perfil"].ToString();
                    Session["senhaConectado"] = dataSet.Tables[0].Rows[0]["senha"].ToString();
                    Session["projetoConectado"] = dataSet.Tables[0].Rows[0]["projeto"].ToString();
                    Session["perfilConectado"] = dataSet.Tables[0].Rows[0]["perfil"].ToString();

                    System.Web.Security.FormsAuthentication.RedirectFromLoginPage(tbUsuario.Text, false);
                }
                else
                {
                    loginError.Visible = true;
                }
            }
            finally {
                conexao.Close();
            }
        }
        #endregion
    }
}