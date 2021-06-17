using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WEB_MGE
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string dataInicial = "";
            string dataFinal = "";
            string nomeInspetor = "";
            string nomeEmpresa = "";
            string mensagem = "";
            byte[] byteBLOBFoto = new byte[0];
            byte[] byteBLOBLogo = new byte[0];
            MemoryStream stmBLOBFoto;
            MemoryStream stmBLOBLogo;
            Bitmap LogoOficial = null;
            Bitmap FotoOficial = null;

            MySqlConnection conexao = null;
            MySqlDataAdapter adaptador = null;
            MySqlCommand comandoSQL = null;
            DataSet dataset = null;

            if (Variaveis_Globais.Servidor == true)
            {
                //conexao = new MySqlConnection("server=" + Constantes.ENDERECO_SERVIDOR + "; port=1234; User Id=" + Constantes.USUARIO_DATABASE + "; database=smp; password=" + Constantes.SENHA_DATABASE);
                conexao = new MySqlConnection(Constantes.STRING_CONEXAO_LOCAL);
            }
            else
            {
                //conexao = new MySqlConnection("server=ENDERECO_SERVIDOR_LOCAL; port=3306; User Id=" + Constantes.USUARIO_DATABASE + "; database=smp; password=" + Constantes.SENHA_DATABASE);
                conexao = new MySqlConnection(Constantes.STRING_CONEXAO_REMOTA);
            }

            if (conexao.State == ConnectionState.Closed)
            {
                conexao.Open();
            }

            comandoSQL = conexao.CreateCommand();
            comandoSQL.CommandText = string.Format("SELECT * FROM WEBSM.TBBOASVINDAS WHERE IDBOASVINDAS = ''1'';");
            adaptador = new MySqlDataAdapter(comandoSQL);
            dataset = new DataSet();
            adaptador.Fill(dataset);

            try
            {
                if (dataset.Tables[0].Rows.Count > 0)
                {
                    nomeInspetor = dataset.Tables[0].Rows[0]["inspetor"].ToString();
                    nomeEmpresa = dataset.Tables[0].Rows[0]["empresa"].ToString();
                    mensagem = dataset.Tables[0].Rows[0]["mensagem"].ToString();
                    dataInicial = dataset.Tables[0].Rows[0]["periodoinicial"].ToString();
                    dataFinal = dataset.Tables[0].Rows[0]["periodofinal"].ToString();
                    byteBLOBFoto = (byte[]) dataset.Tables[0].Rows[0]["foto"];
                    byteBLOBLogo = (byte[]) dataset.Tables[0].Rows[0]["logoempresa"];
                    stmBLOBFoto = new MemoryStream(byteBLOBFoto);
                    FotoOficial = new Bitmap(stmBLOBFoto);
                    stmBLOBLogo = new MemoryStream(byteBLOBLogo);
                    LogoOficial = new Bitmap(stmBLOBLogo);
                }
            }
            catch (Exception erro)
            {
                // Deu erro na geração do arquivo de Serviços Atendidos
                String Msg = erro.ToString();
            }

            string s = Server.MapPath("~/images/MGE_3 validadaQFullHD_SL.jpg");
            string s2 = Server.MapPath("~/images/Logo.jpg");
            string s3 = Server.MapPath("~/images/Foto.jpg");

            System.Drawing.Image original = Bitmap.FromFile(s);
            Graphics gra = Graphics.FromImage(original);
            Bitmap logo = new Bitmap(s2);
            //gra.DrawImage(logo, new Point(20, 540));
            gra.DrawImage(LogoOficial, new Point(20, 540));

            Bitmap foto = new Bitmap(s3);
            //gra.DrawImage(foto, new Point(497, 270));
            gra.DrawImage(FotoOficial, new Point(497, 270));

            //Set the alignment based on the coordinates       
            StringFormat stringformat = new StringFormat();
            stringformat.Alignment = StringAlignment.Far;
            stringformat.LineAlignment = StringAlignment.Far;

            StringFormat stringformat2 = new StringFormat();
            stringformat2.Alignment = StringAlignment.Center;
            stringformat2.LineAlignment = StringAlignment.Center;

            //Set the font color/format/size etc..      
            Color StringColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");//direct color adding    
            Color StringColor2 = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");//customise color adding    
            //string Str_TextOnImage = "Altair,";//Your Text On Image    
            //string Str_TextOnImage2 = "21 à 24/12";//Your Text On Image    
            string Str_TextOnImage = nomeInspetor + ",";//Your Text On Image    
            string Str_TextOnImage2 = dataInicial.Substring(0, dataInicial.IndexOf('/')) + " à " + dataFinal;//Your Text On Image    

            gra.DrawString(Str_TextOnImage, new Font("Century Gothic", 96, FontStyle.Bold), new SolidBrush(StringColor), new Point(1450, 400), stringformat); 
            Response.ContentType = "image/jpeg";

            gra.DrawString(Str_TextOnImage2, new Font("Century Gothic", 36, FontStyle.Bold), new SolidBrush(StringColor2), new Point(1320, 675), stringformat2);

            Response.ContentType = "image/JPEG";
            original.Save(Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg);
        }

        protected void ImgBtnVoltar_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("Main.aspx");
        }

        protected void btnInspecao_Click(object sender, EventArgs e)
        {
            Server.Transfer("BoasVindas.aspx");
        }

        protected void btnAndon_Click(object sender, EventArgs e)
        {
            Server.Transfer("Andon.aspx");
        }

        protected void btnHome_Click(object sender, EventArgs e)
        {
            Server.Transfer("Default.aspx");
        }
    }
}