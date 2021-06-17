using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using MySql.Data.MySqlClient;
using System.Data;
using System.Net.NetworkInformation;
using System.Reflection;

namespace WEB_MGE
{
    public partial class Andon : System.Web.UI.Page
    {
        #region Atributos
        // Lista de informações obtidas com a conexão ao banco
        public string[] listaDeInformacoes = new string[42];

        // Parâmetros injetados no gráfico da função JavaScript
        private int valor1;
        private int valor2;
        private int valor3;
        private int valor4;
        private int valor5;
        private int valor6;
        private int valor7;
        private int valor8;
        private int valor9;
        private int valor10;
        #endregion

        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            string Diretorio;
            string DiretorioAtual;

            Variaveis_Globais.DataHora = DateTime.Now.ToString();
            vDHAtualiza.InnerText = Variaveis_Globais.DataHora.Substring(0, Variaveis_Globais.DataHora.Length-3);

            if (!Page.IsPostBack)
            {
                dnMonPendente.InnerText = "0";
                dnTenApPendente.InnerText = "0";
                dnExatidaoPendente.InnerText = "0";
                dnMarcaLaserPendente.InnerText = "0";
                dnTesteFinalPendente.InnerText = "0";

                try
                {
                    DiretorioAtual = System.AppDomain.CurrentDomain.BaseDirectory.ToString() + "scripts\\";
                    Diretorio = @DiretorioAtual + "GraficoMontagem.js";

                    if (System.IO.File.Exists(Diretorio))
                    {
                        System.IO.File.Delete(Diretorio);
                    }

                    Diretorio = @DiretorioAtual + "GraficoTenAplicada.js";

                    if (System.IO.File.Exists(Diretorio))
                    {
                        System.IO.File.Delete(Diretorio);
                    }

                    Diretorio = @DiretorioAtual + "GraficoExatidao.js";

                    if (System.IO.File.Exists(Diretorio))
                    {
                        System.IO.File.Delete(Diretorio);
                    }

                    Diretorio = @DiretorioAtual + "GraficoMarcaLaser.js";

                    if (System.IO.File.Exists(Diretorio))
                    {
                        System.IO.File.Delete(Diretorio);
                    }

                    Diretorio = @DiretorioAtual + "GraficoTesteFinal.js";

                    if (System.IO.File.Exists(Diretorio))
                    {
                        System.IO.File.Delete(Diretorio);
                    }
                }
                catch
                {

                }
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
                comandoSQL.CommandText = string.Format("SELECT * FROM SM_ORDEMPRODUCAO ORDER BY SM_OP_NUMERO DESC;");
                adaptador = new MySqlDataAdapter(comandoSQL);
                dataset = new DataSet();
                adaptador.Fill(dataset);

                try
                {
                    if (dataset.Tables[0].Rows.Count > 0)
                    {
                        ddlOP.Items.Clear();
                        ddlOP.Items.Add("");
                        for (int x = 0; x < dataset.Tables[0].Rows.Count; x++)
                        {
                            ddlOP.Items.Add(dataset.Tables[0].Rows[x]["SM_OP_NUMERO"].ToString());
                        }
                    }
                }
                catch (Exception erro)
                {
                    // Deu erro na geração do arquivo de Serviços Atendidos
                    String Msg = erro.ToString();
                }
            }
        }

        public static string GetPublicIP()
        {
            string url = "http://checkip.dyndns.org";
            System.Net.WebRequest req = System.Net.WebRequest.Create(url);
            System.Net.WebResponse resp = req.GetResponse();
            System.IO.StreamReader sr = new System.IO.StreamReader(resp.GetResponseStream());
            string response = sr.ReadToEnd().Trim();
            string[] a = response.Split(':');
            string a2 = a[1].Substring(1);
            string[] a3 = a2.Split('<');
            string a4 = a3[0];
            return a4;
        }

        protected string getEnderecoIP()
        {
            string strEnderecoIP;
            strEnderecoIP = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (strEnderecoIP == null)
                strEnderecoIP = Request.ServerVariables["REMOTE_ADDR"];

            return strEnderecoIP;
        }

        protected void btnProcessar_Click(object sender, EventArgs e)
        {
            string QtdPlanejadaMontagem = "";
            string QtdExecutadaMontagem = "";
            string QtdPendenteMontagem = "";

            string QtdPlanejadaTenAplicada = "";
            string QtdExecutadaTenAplicada = "";
            string QtdPendenteTenAplicada = "";

            string QtdPlanejadaExatidao = "";
            string QtdExecutadaExatidao = "";
            string QtdPendenteExatidao = "";

            string QtdPlanejadaMarcaLaser = "";
            string QtdExecutadaMarcaLaser = "";
            string QtdPendenteMarcaLaser = "";

            string QtdPlanejadaTesteFinal = "";
            string QtdExecutadaTesteFinal = "";
            string QtdPendenteTesteFinal = "";

            Variaveis_Globais.NroOP = ddlOP.Items[ddlOP.SelectedIndex].Text;
            if (Variaveis_Globais.NroOP == "")
            {
                return;
            }

            MySqlConnection conexao = null;
            MySqlDataAdapter adaptador = null;
            MySqlCommand comandoSQL = null;
            DataSet dataset = null;

            MySqlDataAdapter adaptador2 = null;
            MySqlCommand comandoSQL2 = null;
            DataSet dataset2 = null;

            MySqlDataAdapter adaptadorProdutos = null;
            MySqlCommand comandoSQLProdutos = null;
            DataSet datasetProdutos = null;

            string Modelo = "";

            Variaveis_Globais.QtdOP = "0";
            Variaveis_Globais.Modelo = "";


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
            comandoSQL.CommandText = string.Format("SELECT * FROM SM_ORDEMPRODUCAO WHERE SM_OP_NUMERO = '{0}';", Variaveis_Globais.NroOP);
            adaptador = new MySqlDataAdapter(comandoSQL);
            dataset = new DataSet();
            adaptador.Fill(dataset);

            //Variaveis_Globais.TrocaProjeto = false;

            try
            {
                if (dataset.Tables[0].Rows.Count > 0)
                {
                    Variaveis_Globais.NroOP = dataset.Tables[0].Rows[0]["SM_OP_NUMERO"].ToString();
                    Variaveis_Globais.QtdOP = dataset.Tables[0].Rows[0]["SM_OP_QUANTIDADE"].ToString();
                    Modelo = dataset.Tables[0].Rows[0]["SM_OP_CODMODELO"].ToString();
                    lblQuantidade.Text = "Quantidade: " + Variaveis_Globais.QtdOP;

                    comandoSQL2 = conexao.CreateCommand();
                    comandoSQL2.CommandText = string.Format("SELECT * FROM SM_MODELOS WHERE SM_MD_CODIGO = '{0}';", Modelo);
                    adaptador2 = new MySqlDataAdapter(comandoSQL2);
                    dataset2 = new DataSet();
                    adaptador2.Fill(dataset2);

                    if (dataset2.Tables[0].Rows.Count > 0)
                    {
                        Variaveis_Globais.Modelo = dataset2.Tables[0].Rows[0]["SM_MD_DESCRICAO"].ToString();
                        lblModelo.Text = "Modelo: " + Variaveis_Globais.Modelo;
                    }

                    // Verificar Qtd produtos montados
                    comandoSQLProdutos = conexao.CreateCommand();
                    comandoSQLProdutos.CommandText = string.Format("SELECT Count(SM_PD_Id) FROM smp.SM_Produtos where SM_PD_NroSerie like '{0}%' and SM_PD_CasModulo = 1", Variaveis_Globais.NroOP);
                    adaptadorProdutos = new MySqlDataAdapter(comandoSQLProdutos);
                    datasetProdutos = new DataSet();
                    adaptadorProdutos.Fill(datasetProdutos);

                    if (datasetProdutos.Tables[0].Rows.Count > 0)
                    {
                        Variaveis_Globais.QtdMontagem = datasetProdutos.Tables[0].Rows[0]["Count(SM_PD_Id)"].ToString();
                        //lblQtdMontagem.Text = Variaveis_Globais.QtdMontagem;//"Qtd: " + Variaveis_Globais.QtdMontagem.ToString();
                        //lblQtdMontagem.Text = QtdExecutadaMontagem;
                    }

                    adaptadorProdutos.Dispose();
                    datasetProdutos.Dispose();
                    comandoSQLProdutos.Dispose();
                    //-------------------------------------------------

                    // Verificar Qtd produtos tensão aplicada
                    comandoSQLProdutos = conexao.CreateCommand();
                    comandoSQLProdutos.CommandText = string.Format("SELECT Count(SM_PD_Id) FROM smp.SM_Produtos where SM_PD_NroSerie like '{0}%' and (SM_PD_StsTenAplicada >= 10) and (SM_PD_StsTenAplicada < 99)", Variaveis_Globais.NroOP);
                    adaptadorProdutos = new MySqlDataAdapter(comandoSQLProdutos);
                    datasetProdutos = new DataSet();
                    adaptadorProdutos.Fill(datasetProdutos);

                    if (datasetProdutos.Tables[0].Rows.Count > 0)
                    {
                        Variaveis_Globais.QtdTenAplicada = datasetProdutos.Tables[0].Rows[0]["Count(SM_PD_Id)"].ToString();
                        //lblQtdTenAplicada.Text = Variaveis_Globais.QtdTenAplicada;
                        //lblQtdTenAplicada.Text = QtdExecutadaTenAplicada;
                    }

                    adaptadorProdutos.Dispose();
                    datasetProdutos.Dispose();
                    comandoSQLProdutos.Dispose();
                    //-------------------------------------------------

                    // Verificar Qtd produtos exatidao
                    comandoSQLProdutos = conexao.CreateCommand();
                    comandoSQLProdutos.CommandText = string.Format("SELECT Count(SM_PD_Id) FROM smp.SM_Produtos where SM_PD_NroSerie like '{0}%' and (SM_PD_StsExatidao >= 10) and (SM_PD_StsExatidao < 99)", Variaveis_Globais.NroOP);
                    adaptadorProdutos = new MySqlDataAdapter(comandoSQLProdutos);
                    datasetProdutos = new DataSet();
                    adaptadorProdutos.Fill(datasetProdutos);

                    if (datasetProdutos.Tables[0].Rows.Count > 0)
                    {
                        Variaveis_Globais.QtdExatidao = datasetProdutos.Tables[0].Rows[0]["Count(SM_PD_Id)"].ToString();
                        //lblQtdExatidao.Text = Variaveis_Globais.QtdExatidao;
                        //lblQtdExatidao.Text = QtdExecutadaExatidao;
                    }

                    adaptadorProdutos.Dispose();
                    datasetProdutos.Dispose();
                    comandoSQLProdutos.Dispose();
                    //-------------------------------------------------

                    // Verificar Qtd produtos marca laser
                    comandoSQLProdutos = conexao.CreateCommand();
                    comandoSQLProdutos.CommandText = string.Format("SELECT Count(SM_PD_Id) FROM smp.SM_Produtos where SM_PD_NroSerie like '{0}%' and (SM_PD_StsMarcaLaser >= 10) and (SM_PD_StsMarcaLaser < 99)", Variaveis_Globais.NroOP);
                    adaptadorProdutos = new MySqlDataAdapter(comandoSQLProdutos);
                    datasetProdutos = new DataSet();
                    adaptadorProdutos.Fill(datasetProdutos);

                    if (datasetProdutos.Tables[0].Rows.Count > 0)
                    {
                        Variaveis_Globais.QtdMarcaLaser = datasetProdutos.Tables[0].Rows[0]["Count(SM_PD_Id)"].ToString();
                        //lblQtdMarcaLaser.Text = Variaveis_Globais.QtdMarcaLaser;
                        //lblQtdMarcaLaser.Text = QtdExecutadaMarcaLaser;
                    }

                    adaptadorProdutos.Dispose();
                    datasetProdutos.Dispose();
                    comandoSQLProdutos.Dispose();
                    //-------------------------------------------------

                    // Verificar Qtd produtos teste final
                    comandoSQLProdutos = conexao.CreateCommand();
                    comandoSQLProdutos.CommandText = string.Format("SELECT Count(SM_PD_Id) FROM smp.SM_Produtos where SM_PD_NroSerie like '{0}%' and SM_PD_FuncionalFinalOK = 1", Variaveis_Globais.NroOP);
                    adaptadorProdutos = new MySqlDataAdapter(comandoSQLProdutos);
                    datasetProdutos = new DataSet();
                    adaptadorProdutos.Fill(datasetProdutos);

                    if (datasetProdutos.Tables[0].Rows.Count > 0)
                    {
                        Variaveis_Globais.QtdTesteFinal = datasetProdutos.Tables[0].Rows[0]["Count(SM_PD_Id)"].ToString();
                        //lblQtdTesteFinal.Text = Variaveis_Globais.QtdTesteFinal;
                        //lblQtdTesteFinal.Text = QtdExecutadaTesteFinal;
                    }

                    adaptadorProdutos.Dispose();
                    datasetProdutos.Dispose();
                    comandoSQLProdutos.Dispose();
                    //-------------------------------------------------



                    // GIULIANO PEDIU PARA DESABILITAR TEMPORARIAMENTE A INFORMAÇÃO DE LEITURA PERDIDA

                    // Verifica se o usuário é administrador
                    if (Session["perfilConectado"].ToString() != "ADMIN")
                    {
                        listaDeInformacoes[0] = "SMP";
                        listaDeInformacoes[1] = Variaveis_Globais.NroOP;
                        listaDeInformacoes[2] = Variaveis_Globais.QtdOP;
                        listaDeInformacoes[3] = "0";
                        listaDeInformacoes[4] = "0";
                        listaDeInformacoes[5] = "0";
                        listaDeInformacoes[6] = "0";
                        listaDeInformacoes[7] = "0";
                        listaDeInformacoes[8] = "0";
                        listaDeInformacoes[9] = "0";
                        listaDeInformacoes[10] = "0";
                        listaDeInformacoes[11] = DateTime.Now.ToString();
                        listaDeInformacoes[12] = Variaveis_Globais.QtdMontagem;
                        listaDeInformacoes[13] = "0";
                        listaDeInformacoes[14] = "0";
                        listaDeInformacoes[15] = "0";
                        listaDeInformacoes[16] = "0";
                        listaDeInformacoes[17] = "0";
                        listaDeInformacoes[18] = Variaveis_Globais.QtdTenAplicada;
                        listaDeInformacoes[19] = "0";
                        listaDeInformacoes[20] = "0";
                        listaDeInformacoes[21] = "0";
                        listaDeInformacoes[22] = "0";
                        listaDeInformacoes[23] = "0";
                        listaDeInformacoes[24] = Variaveis_Globais.QtdExatidao;
                        listaDeInformacoes[25] = "0";
                        listaDeInformacoes[26] = "0";
                        listaDeInformacoes[27] = "0";
                        listaDeInformacoes[28] = "0";
                        listaDeInformacoes[29] = "0";
                        listaDeInformacoes[30] = Variaveis_Globais.QtdMarcaLaser;
                        listaDeInformacoes[31] = "0";
                        listaDeInformacoes[32] = "0";
                        listaDeInformacoes[33] = "0";
                        listaDeInformacoes[34] = "0";
                        listaDeInformacoes[35] = "0";
                        listaDeInformacoes[36] = Variaveis_Globais.QtdTesteFinal;
                        listaDeInformacoes[37] = "0";
                        listaDeInformacoes[38] = "0";
                        listaDeInformacoes[39] = "0";
                        listaDeInformacoes[40] = "0";
                        listaDeInformacoes[41] = "0";

                        decimal VAmostraTot = (Convert.ToDecimal(listaDeInformacoes[2]) +
                                                Convert.ToDecimal(listaDeInformacoes[3]) +
                                                Convert.ToDecimal(listaDeInformacoes[4]) +
                                                Convert.ToDecimal(listaDeInformacoes[5]) +
                                                Convert.ToDecimal(listaDeInformacoes[6]) +
                                                Convert.ToDecimal(listaDeInformacoes[7]));

                        decimal VMonTotAmostra = (Convert.ToDecimal(listaDeInformacoes[2]) +
                                                    Convert.ToDecimal(listaDeInformacoes[3]) +
                                                    Convert.ToDecimal(listaDeInformacoes[4]) +
                                                    Convert.ToDecimal(listaDeInformacoes[5]) +
                                                    Convert.ToDecimal(listaDeInformacoes[6]) +
                                                    Convert.ToDecimal(listaDeInformacoes[7]));

                        decimal VMonTot = (Convert.ToDecimal(listaDeInformacoes[12]) +
                                            Convert.ToDecimal(listaDeInformacoes[13]) +
                                            Convert.ToDecimal(listaDeInformacoes[14]) +
                                            Convert.ToDecimal(listaDeInformacoes[15]) +
                                            Convert.ToDecimal(listaDeInformacoes[16]) +
                                            Convert.ToDecimal(listaDeInformacoes[17]));

                        decimal VMonPendente = (VMonTotAmostra - VMonTot);

                        decimal VTenAplicadaTotAmostra = (Convert.ToDecimal(listaDeInformacoes[2]) +
                                                    Convert.ToDecimal(listaDeInformacoes[3]) +
                                                    Convert.ToDecimal(listaDeInformacoes[4]) +
                                                    Convert.ToDecimal(listaDeInformacoes[5]) +
                                                    Convert.ToDecimal(listaDeInformacoes[6]) +
                                                    Convert.ToDecimal(listaDeInformacoes[7]));

                        decimal VTenAplicadaTot = (Convert.ToDecimal(listaDeInformacoes[18]) +
                                            Convert.ToDecimal(listaDeInformacoes[19]) +
                                            Convert.ToDecimal(listaDeInformacoes[20]) +
                                            Convert.ToDecimal(listaDeInformacoes[21]) +
                                            Convert.ToDecimal(listaDeInformacoes[22]) +
                                            Convert.ToDecimal(listaDeInformacoes[23]));

                        decimal VTenAplicadaPendente = (VTenAplicadaTotAmostra - (Convert.ToDecimal(listaDeInformacoes[10])) - VTenAplicadaTot);

                        decimal VExatidaoTotAmostra = (Convert.ToDecimal(listaDeInformacoes[2]) +
                                                    Convert.ToDecimal(listaDeInformacoes[3]) +
                                                    Convert.ToDecimal(listaDeInformacoes[4]) +
                                                    Convert.ToDecimal(listaDeInformacoes[5]) +
                                                    Convert.ToDecimal(listaDeInformacoes[6]) +
                                                    Convert.ToDecimal(listaDeInformacoes[7]));

                        decimal VExatidaoTot = (Convert.ToDecimal(listaDeInformacoes[24]) +
                                            Convert.ToDecimal(listaDeInformacoes[25]) +
                                            Convert.ToDecimal(listaDeInformacoes[26]) +
                                            Convert.ToDecimal(listaDeInformacoes[27]) +
                                            Convert.ToDecimal(listaDeInformacoes[28]) +
                                            Convert.ToDecimal(listaDeInformacoes[29]));

                        decimal VExatidaoPendente = (VExatidaoTotAmostra - VExatidaoTot);

                        decimal VMarcaLaserTotAmostra = (Convert.ToDecimal(listaDeInformacoes[2]) +
                                                    Convert.ToDecimal(listaDeInformacoes[3]) +
                                                    Convert.ToDecimal(listaDeInformacoes[4]) +
                                                    Convert.ToDecimal(listaDeInformacoes[5]) +
                                                    Convert.ToDecimal(listaDeInformacoes[6]) +
                                                    Convert.ToDecimal(listaDeInformacoes[7]));

                        decimal VMarcaLaserTot = (Convert.ToDecimal(listaDeInformacoes[30]) +
                                            Convert.ToDecimal(listaDeInformacoes[31]) +
                                            Convert.ToDecimal(listaDeInformacoes[32]) +
                                            Convert.ToDecimal(listaDeInformacoes[33]) +
                                            Convert.ToDecimal(listaDeInformacoes[34]) +
                                            Convert.ToDecimal(listaDeInformacoes[35]));

                        decimal VMarcaLaserPendente = (VMarcaLaserTotAmostra - VMarcaLaserTot);

                        decimal VTesteFinalTotAmostra = (Convert.ToDecimal(listaDeInformacoes[2]) +
                                                    Convert.ToDecimal(listaDeInformacoes[3]) +
                                                    Convert.ToDecimal(listaDeInformacoes[4]) +
                                                    Convert.ToDecimal(listaDeInformacoes[5]) +
                                                    Convert.ToDecimal(listaDeInformacoes[6]) +
                                                    Convert.ToDecimal(listaDeInformacoes[7]));

                        decimal VTesteFinalTot = (Convert.ToDecimal(listaDeInformacoes[36]) +
                                            Convert.ToDecimal(listaDeInformacoes[37]) +
                                            Convert.ToDecimal(listaDeInformacoes[38]) +
                                            Convert.ToDecimal(listaDeInformacoes[39]) +
                                            Convert.ToDecimal(listaDeInformacoes[40]) +
                                            Convert.ToDecimal(listaDeInformacoes[41]));

                        decimal VTesteFinalPendente = (VTesteFinalTotAmostra - VTesteFinalTot);

                        //vDHAtualiza.InnerText = listaDeInformacoes[11];

                        vCliente.InnerText = listaDeInformacoes[0];

                        Variaveis_Globais.Cliente = vCliente.InnerText;

                        // ############################################################################# - INFORMAÇÕES INFERIORES AO GRÁFICO

                        // -------------------------------------- Cálcula os parâmetros do gráfico e ajusta os parâmetros da rotina JavaScript

                        QtdPlanejadaMontagem = dadosNr("", VMonTotAmostra, VAmostraTot);//"Planejado"
                        QtdExecutadaMontagem = dadosNr("", VMonTot, VAmostraTot);//"Executado"
                        lblQtdMontagem.Text = QtdExecutadaMontagem;

                        if (VMonTot >= VAmostraTot)
                        {
                            valor1 = 100;
                            valor2 = 0;
                        }
                        else
                        {
                            valor1 = Convert.ToInt32(VMonTot);
                            valor2 = Convert.ToInt32(VMonTotAmostra) - Convert.ToInt32(VMonTot);
                        }

                        QtdPendenteMontagem = dadosNr("", VMonPendente, VAmostraTot);//"Pendente"
                        dnMonPendente.InnerText = QtdPendenteMontagem;
                        // -------------------------------------- Cálcula os parâmetros do gráfico e ajusta os parâmetros da rotina JavaScript

                        QtdPlanejadaTenAplicada = dadosNr("", VTenAplicadaTotAmostra, VAmostraTot);
                        QtdExecutadaTenAplicada = dadosNr("", VTenAplicadaTot, VAmostraTot);
                        lblQtdTenAplicada.Text = QtdExecutadaTenAplicada;

                        if (VTenAplicadaTot >= VAmostraTot)
                        {
                            valor3 = 100;
                            valor4 = 0;
                        }
                        else
                        {
                            valor3 = Convert.ToInt32(VTenAplicadaTot);
                            valor4 = Convert.ToInt32(VTenAplicadaTotAmostra) - Convert.ToInt32(VTenAplicadaTot);
                        }

                        QtdPendenteTenAplicada = dadosNr("", VTenAplicadaPendente, VAmostraTot);
                        dnTenApPendente.InnerText = QtdPendenteTenAplicada;
                        // -------------------------------------- Cálcula os parâmetros do gráfico e ajusta os parâmetros da rotina JavaScript

                        QtdPlanejadaExatidao = dadosNr("", VExatidaoTotAmostra, VAmostraTot);
                        QtdExecutadaExatidao = dadosNr("", VExatidaoTot, VAmostraTot);
                        lblQtdExatidao.Text = QtdExecutadaExatidao;

                        if (VExatidaoTot >= VAmostraTot)
                        {
                            valor5 = 100;
                            valor6 = 0;
                        }
                        else
                        {
                            valor5 = Convert.ToInt32(VExatidaoTot);
                            valor6 = Convert.ToInt32(VExatidaoTotAmostra) - Convert.ToInt32(VExatidaoTot);
                        }

                        QtdPendenteExatidao = dadosNr("", VExatidaoPendente, VAmostraTot);
                        dnExatidaoPendente.InnerText = QtdPendenteExatidao;

                        // -------------------------------------- Cálcula os parâmetros do gráfico e ajusta os parâmetros da rotina JavaScript

                        QtdPlanejadaMarcaLaser = dadosNr("", VMarcaLaserTotAmostra, VAmostraTot);
                        QtdExecutadaMarcaLaser = dadosNr("", VMarcaLaserTot, VAmostraTot);
                        lblQtdMarcaLaser.Text = QtdExecutadaMarcaLaser;

                        if (VMarcaLaserTot >= VAmostraTot)
                        {
                            valor7 = 100;
                            valor8 = 0;
                        }
                        else
                        {
                            valor7 = Convert.ToInt32(VMarcaLaserTot);
                            valor8 = Convert.ToInt32(VMarcaLaserTotAmostra) - Convert.ToInt32(VMarcaLaserTot);
                        }

                        QtdPendenteMarcaLaser = dadosNr("", VMarcaLaserPendente, VAmostraTot);
                        dnMarcaLaserPendente.InnerText = QtdPendenteMarcaLaser;

                        // -------------------------------------- Cálcula os parâmetros do gráfico e ajusta os parâmetros da rotina JavaScript

                        QtdPlanejadaTesteFinal = dadosNr("", VTesteFinalTotAmostra, VAmostraTot);
                        QtdExecutadaTesteFinal = dadosNr("", VTesteFinalTot, VAmostraTot);
                        lblQtdTesteFinal.Text = QtdExecutadaTesteFinal;

                        if (VTesteFinalTot >= VAmostraTot)
                        {
                            valor9 = 100;
                            valor10 = 0;
                        }
                        else
                        {
                            valor9 = Convert.ToInt32(VTesteFinalTot);
                            valor10 = Convert.ToInt32(VTesteFinalTotAmostra) - Convert.ToInt32(VTesteFinalTot);
                        }

                        QtdPendenteTesteFinal = dadosNr("", VTesteFinalPendente, VAmostraTot);
                        dnTesteFinalPendente.InnerText = QtdPendenteTesteFinal;

                        // ############################################################################################### - TABELA INFERIOR

                        //vAmostraRes.InnerText = listaDeInformacoes[2];
                        //vInsRes.InnerText = percentual(listaDeInformacoes[12], listaDeInformacoes[2]);
                        //vLeiRes.InnerText = percentual(listaDeInformacoes[18], listaDeInformacoes[2]);
                        //vRetRes.InnerText = percentual(listaDeInformacoes[24], listaDeInformacoes[2]);
                        //vDepRes.InnerText = percentual(listaDeInformacoes[30], listaDeInformacoes[2]);
                        //vEnvRes.InnerText = percentual(listaDeInformacoes[36], listaDeInformacoes[2]);

                        //vAmostraCom.InnerText = listaDeInformacoes[3];
                        //vInsCom.InnerText = percentual(listaDeInformacoes[13], listaDeInformacoes[3]);
                        //vLeiCom.InnerText = percentual(listaDeInformacoes[19], listaDeInformacoes[3]);
                        //vRetCom.InnerText = percentual(listaDeInformacoes[25], listaDeInformacoes[3]);
                        //vDepCom.InnerText = percentual(listaDeInformacoes[31], listaDeInformacoes[3]);
                        //vEnvCom.InnerText = percentual(listaDeInformacoes[37], listaDeInformacoes[3]);

                        //vAmostraInd.InnerText = listaDeInformacoes[4];
                        //vInsInd.InnerText = percentual(listaDeInformacoes[14], listaDeInformacoes[4]);
                        //vLeiInd.InnerText = percentual(listaDeInformacoes[20], listaDeInformacoes[4]);
                        //vRetInd.InnerText = percentual(listaDeInformacoes[26], listaDeInformacoes[4]);
                        //vDepInd.InnerText = percentual(listaDeInformacoes[32], listaDeInformacoes[4]);
                        //vEnvInd.InnerText = percentual(listaDeInformacoes[38], listaDeInformacoes[4]);

                        //vAmostraPub.InnerText = listaDeInformacoes[5];
                        //vInsPub.InnerText = percentual(listaDeInformacoes[15], listaDeInformacoes[5]);
                        //vLeiPub.InnerText = percentual(listaDeInformacoes[21], listaDeInformacoes[5]);
                        //vRetPub.InnerText = percentual(listaDeInformacoes[27], listaDeInformacoes[5]);
                        //vDepPub.InnerText = percentual(listaDeInformacoes[33], listaDeInformacoes[5]);
                        //vEnvPub.InnerText = percentual(listaDeInformacoes[39], listaDeInformacoes[5]);

                        //vAmostraRur.InnerText = listaDeInformacoes[6];
                        //vInsRur.InnerText = percentual(listaDeInformacoes[16], listaDeInformacoes[6]);
                        //vLeiRur.InnerText = percentual(listaDeInformacoes[22], listaDeInformacoes[6]);
                        //vRetRur.InnerText = percentual(listaDeInformacoes[28], listaDeInformacoes[6]);
                        //vDepRur.InnerText = percentual(listaDeInformacoes[34], listaDeInformacoes[6]);
                        //vEnvRur.InnerText = percentual(listaDeInformacoes[40], listaDeInformacoes[6]);

                        //vAmostraTra.InnerText = listaDeInformacoes[7];
                        //vInsTra.InnerText = percentual(listaDeInformacoes[17], listaDeInformacoes[7]);
                        //vLeiTra.InnerText = percentual(listaDeInformacoes[23], listaDeInformacoes[7]);
                        //vRetTra.InnerText = percentual(listaDeInformacoes[29], listaDeInformacoes[7]);
                        //vDepTra.InnerText = percentual(listaDeInformacoes[35], listaDeInformacoes[7]);
                        //vEnvTra.InnerText = percentual(listaDeInformacoes[41], listaDeInformacoes[7]);

                        //AmostraTotTabela.InnerText = VAmostraTot.ToString();
                        //vInsTot.InnerText = percentual(VInsTot.ToString(), VAmostraTot.ToString());
                        //vLeiTot.InnerText = percentual(VLeiTot.ToString(), VAmostraTot.ToString());
                        //vRetTot.InnerText = percentual(VRetTot.ToString(), VAmostraTot.ToString());
                        //vDepTot.InnerText = percentual(VDepTot.ToString(), VAmostraTot.ToString());
                        //vEnvTot.InnerText = percentual(VEnvTot.ToString(), VAmostraTot.ToString());

                        // Invoca a rotina JavaScript que desenha os gráficos                        
                        inserirGraficosNaPagina(sender, e);
                    }

                    // Caso seja o administrador, redireciona para a página com o menu!
                    else
                    {
                        Response.Redirect("Menu.aspx", false);
                    }
                }
                else
                {
                    lblModelo.Text = "Pedido não encontrado.";
                }
            }
            finally
            {
                conexao.Close();
            }
        }

        protected void MontaJSGraficoMontagem(object sender, EventArgs e)
        {
            // Gera um arquivo de Log das UCs atendidas
            System.IO.Stream Grafico;
            System.IO.StreamWriter ArquivoMontagem;
            string DiretorioAtual;
            string Diretorio;

            try
            {
                DiretorioAtual = System.AppDomain.CurrentDomain.BaseDirectory.ToString() + "scripts\\";
                Diretorio = @DiretorioAtual + "GraficoMontagem.js";

                if (System.IO.File.Exists(Diretorio))
                {
                    System.IO.File.Delete(Diretorio);
                }
                Grafico = System.IO.File.Open(@DiretorioAtual + "\\GraficoMontagem.js", System.IO.FileMode.Create);
                ArquivoMontagem = new System.IO.StreamWriter(Grafico);
                ArquivoMontagem.WriteLine("\tvar ctx = document.getElementById('GraficoMontagem').getContext('2d');");
                ArquivoMontagem.WriteLine("\tvar myChart = new Chart(ctx, {");
                ArquivoMontagem.WriteLine("\t\ttype: 'doughnut',");
                ArquivoMontagem.WriteLine("\t\tdata: {");
                ArquivoMontagem.WriteLine("\t\t\tlabels:['Concluídos', 'Pendentes'],");
                ArquivoMontagem.WriteLine("\t\t\tdatasets: [{");
                ArquivoMontagem.WriteLine("\t\t\t\tlabel: '# of Votes',");
                ArquivoMontagem.WriteLine("\t\t\t\tdata:[" + Variaveis_Globais.QtdMontagem.ToString() + ", " + (int.Parse(Variaveis_Globais.QtdOP) - int.Parse(Variaveis_Globais.QtdMontagem)).ToString() + "],");
                ArquivoMontagem.WriteLine("\t\t\t\tbackgroundColor:[");
                ArquivoMontagem.WriteLine("\t\t\t\t\t'rgba(0, 132, 142, 1)',");
                ArquivoMontagem.WriteLine("\t\t\t\t\t'rgba(128, 128, 128, 1)'");
                ArquivoMontagem.WriteLine("\t\t\t\t],");
                ArquivoMontagem.WriteLine("\t\t\t\tborderColor:[");
                ArquivoMontagem.WriteLine("\t\t\t\t\t'rgba(255, 255, 255, 0.2)',");
                ArquivoMontagem.WriteLine("\t\t\t\t\t'rgba(255, 255, 255, 0.2)'");
                ArquivoMontagem.WriteLine("\t\t\t\t],");
                ArquivoMontagem.WriteLine("\t\t\t\tborderWidth: 1");
                ArquivoMontagem.WriteLine("\t\t\t}]");
                ArquivoMontagem.WriteLine("\t\t},");
                ArquivoMontagem.WriteLine("\t\toptions: {");
                ArquivoMontagem.WriteLine("\t\t");
                ArquivoMontagem.WriteLine("\t\t}");
                ArquivoMontagem.WriteLine("\t});");
                ArquivoMontagem.Close();
                Grafico.Close();
            }
            catch (Exception erro)
            {
                // Deu erro na geração do arquivo de Serviços Atendidos
                String Msg = erro.ToString();
            }
        }

        protected void MontaJSGraficoTenAplicada(object sender, EventArgs e)
        {
            // Gera um arquivo de Log das UCs atendidas
            System.IO.Stream Grafico;
            System.IO.StreamWriter ArquivoTenAplicada;
            string DiretorioAtual;
            string Diretorio;

            try
            {
                DiretorioAtual = System.AppDomain.CurrentDomain.BaseDirectory.ToString() + "scripts\\";
                Diretorio = @DiretorioAtual + "GraficoTenAplicada.js";

                if (System.IO.File.Exists(Diretorio))
                {
                    System.IO.File.Delete(Diretorio);
                }
                Grafico = System.IO.File.Open(@DiretorioAtual + "\\GraficoTenAplicada.js", System.IO.FileMode.Create);
                ArquivoTenAplicada = new System.IO.StreamWriter(Grafico);
                ArquivoTenAplicada.WriteLine("\tvar ctx = document.getElementById('GraficoTenAplicada').getContext('2d');");
                ArquivoTenAplicada.WriteLine("\tvar myChart = new Chart(ctx, {");
                ArquivoTenAplicada.WriteLine("\t\ttype: 'doughnut',");
                ArquivoTenAplicada.WriteLine("\t\tdata: {");
                ArquivoTenAplicada.WriteLine("\t\t\tlabels:['Concluídos', 'Pendentes'],");
                ArquivoTenAplicada.WriteLine("\t\t\tdatasets: [{");
                ArquivoTenAplicada.WriteLine("\t\t\t\tlabel: '# of Votes',");
                ArquivoTenAplicada.WriteLine("\t\t\t\tdata:[" + Variaveis_Globais.QtdTenAplicada.ToString() + ", " + (int.Parse(Variaveis_Globais.QtdOP) - int.Parse(Variaveis_Globais.QtdTenAplicada)).ToString() + "],");
                ArquivoTenAplicada.WriteLine("\t\t\t\tbackgroundColor:[");
                ArquivoTenAplicada.WriteLine("\t\t\t\t\t'rgba(0, 132, 142, 1)',");
                ArquivoTenAplicada.WriteLine("\t\t\t\t\t'rgba(128, 128, 128, 1)'");
                ArquivoTenAplicada.WriteLine("\t\t\t\t],");
                ArquivoTenAplicada.WriteLine("\t\t\t\tborderColor:[");
                ArquivoTenAplicada.WriteLine("\t\t\t\t\t'rgba(255, 255, 255, 0.2)',");
                ArquivoTenAplicada.WriteLine("\t\t\t\t\t'rgba(255, 255, 255, 0.2)'");
                ArquivoTenAplicada.WriteLine("\t\t\t\t],");
                ArquivoTenAplicada.WriteLine("\t\t\t\tborderWidth: 1");
                ArquivoTenAplicada.WriteLine("\t\t\t}]");
                ArquivoTenAplicada.WriteLine("\t\t},");
                ArquivoTenAplicada.WriteLine("\t\toptions: {");
                ArquivoTenAplicada.WriteLine("\t\t");
                ArquivoTenAplicada.WriteLine("\t\t}");
                ArquivoTenAplicada.WriteLine("\t});");
                ArquivoTenAplicada.Close();
                Grafico.Close();
            }
            catch (Exception erro)
            {
                // Deu erro na geração do arquivo de Serviços Atendidos
                String Msg = erro.ToString();
            }
        }
        protected void MontaJSGraficoExatidao(object sender, EventArgs e)
        {
            // Gera um arquivo de Log das UCs atendidas
            System.IO.Stream Grafico;
            System.IO.StreamWriter ArquivoExatidao;
            string DiretorioAtual;
            string Diretorio;

            try
            {
                DiretorioAtual = System.AppDomain.CurrentDomain.BaseDirectory.ToString() + "scripts\\";
                Diretorio = @DiretorioAtual + "GraficoExatidao.js";

                if (System.IO.File.Exists(Diretorio))
                {
                    System.IO.File.Delete(Diretorio);
                }
                Grafico = System.IO.File.Open(@DiretorioAtual + "\\GraficoExatidao.js", System.IO.FileMode.Create);
                ArquivoExatidao = new System.IO.StreamWriter(Grafico);
                ArquivoExatidao.WriteLine("\tvar ctx = document.getElementById('GraficoExatidao').getContext('2d');");
                ArquivoExatidao.WriteLine("\tvar myChart = new Chart(ctx, {");
                ArquivoExatidao.WriteLine("\t\ttype: 'doughnut',");
                ArquivoExatidao.WriteLine("\t\tdata: {");
                ArquivoExatidao.WriteLine("\t\t\tlabels:['Concluídos', 'Pendentes'],");
                ArquivoExatidao.WriteLine("\t\t\tdatasets: [{");
                ArquivoExatidao.WriteLine("\t\t\t\tlabel: '# of Votes',");
                ArquivoExatidao.WriteLine("\t\t\t\tdata:[" + Variaveis_Globais.QtdExatidao.ToString() + ", " + (int.Parse(Variaveis_Globais.QtdOP) - int.Parse(Variaveis_Globais.QtdExatidao)).ToString() + "],");
                ArquivoExatidao.WriteLine("\t\t\t\tbackgroundColor:[");
                ArquivoExatidao.WriteLine("\t\t\t\t\t'rgba(0, 132, 142, 1)',");
                ArquivoExatidao.WriteLine("\t\t\t\t\t'rgba(128, 128, 128, 1)'");
                ArquivoExatidao.WriteLine("\t\t\t\t],");
                ArquivoExatidao.WriteLine("\t\t\t\tborderColor:[");
                ArquivoExatidao.WriteLine("\t\t\t\t\t'rgba(255, 255, 255, 0.2)',");
                ArquivoExatidao.WriteLine("\t\t\t\t\t'rgba(255, 255, 255, 0.2)'");
                ArquivoExatidao.WriteLine("\t\t\t\t],");
                ArquivoExatidao.WriteLine("\t\t\t\tborderWidth: 1");
                ArquivoExatidao.WriteLine("\t\t\t}]");
                ArquivoExatidao.WriteLine("\t\t},");
                ArquivoExatidao.WriteLine("\t\toptions: {");
                ArquivoExatidao.WriteLine("\t\t");
                ArquivoExatidao.WriteLine("\t\t}");
                ArquivoExatidao.WriteLine("\t});");
                ArquivoExatidao.Close();
                Grafico.Close();
            }
            catch (Exception erro)
            {
                // Deu erro na geração do arquivo de Serviços Atendidos
                String Msg = erro.ToString();
            }
        }

        protected void MontaJSGraficoMarcaLaser(object sender, EventArgs e)
        {
            // Gera um arquivo de Log das UCs atendidas
            System.IO.Stream Grafico;
            System.IO.StreamWriter ArquivoMarcaLaser;
            string DiretorioAtual;
            string Diretorio;

            try
            {
                DiretorioAtual = System.AppDomain.CurrentDomain.BaseDirectory.ToString() + "scripts\\";
                Diretorio = @DiretorioAtual + "GraficoMarcaLaser.js";

                if (System.IO.File.Exists(Diretorio))
                {
                    System.IO.File.Delete(Diretorio);
                }
                Grafico = System.IO.File.Open(@DiretorioAtual + "\\GraficoMarcaLaser.js", System.IO.FileMode.Create);
                ArquivoMarcaLaser = new System.IO.StreamWriter(Grafico);
                ArquivoMarcaLaser.WriteLine("\tvar ctx = document.getElementById('GraficoMarcaLaser').getContext('2d');");
                ArquivoMarcaLaser.WriteLine("\tvar myChart = new Chart(ctx, {");
                ArquivoMarcaLaser.WriteLine("\t\ttype: 'doughnut',");
                ArquivoMarcaLaser.WriteLine("\t\tdata: {");
                ArquivoMarcaLaser.WriteLine("\t\t\tlabels:['Concluídos', 'Pendentes'],");
                ArquivoMarcaLaser.WriteLine("\t\t\tdatasets: [{");
                ArquivoMarcaLaser.WriteLine("\t\t\t\tlabel: '# of Votes',");
                ArquivoMarcaLaser.WriteLine("\t\t\t\tdata:[" + Variaveis_Globais.QtdMarcaLaser.ToString() + ", " + (int.Parse(Variaveis_Globais.QtdOP) - int.Parse(Variaveis_Globais.QtdMarcaLaser)).ToString() + "],");
                ArquivoMarcaLaser.WriteLine("\t\t\t\tbackgroundColor:[");
                ArquivoMarcaLaser.WriteLine("\t\t\t\t\t'rgba(0, 132, 142, 1)',");
                ArquivoMarcaLaser.WriteLine("\t\t\t\t\t'rgba(128, 128, 128, 1)'");
                ArquivoMarcaLaser.WriteLine("\t\t\t\t],");
                ArquivoMarcaLaser.WriteLine("\t\t\t\tborderColor:[");
                ArquivoMarcaLaser.WriteLine("\t\t\t\t\t'rgba(255, 255, 255, 0.2)',");
                ArquivoMarcaLaser.WriteLine("\t\t\t\t\t'rgba(255, 255, 255, 0.2)'");
                ArquivoMarcaLaser.WriteLine("\t\t\t\t],");
                ArquivoMarcaLaser.WriteLine("\t\t\t\tborderWidth: 1");
                ArquivoMarcaLaser.WriteLine("\t\t\t}]");
                ArquivoMarcaLaser.WriteLine("\t\t},");
                ArquivoMarcaLaser.WriteLine("\t\toptions: {");
                ArquivoMarcaLaser.WriteLine("\t\t");
                ArquivoMarcaLaser.WriteLine("\t\t}");
                ArquivoMarcaLaser.WriteLine("\t});");
                ArquivoMarcaLaser.Close();
                Grafico.Close();
            }
            catch (Exception erro)
            {
                // Deu erro na geração do arquivo de Serviços Atendidos
                String Msg = erro.ToString();
            }
        }

        protected void MontaJSGraficoTesteFinal(object sender, EventArgs e)
        {
            // Gera um arquivo de Log das UCs atendidas
            System.IO.Stream Grafico;
            System.IO.StreamWriter ArquivoTesteFinal;
            string DiretorioAtual;
            string Diretorio;

            try
            {
                DiretorioAtual = System.AppDomain.CurrentDomain.BaseDirectory.ToString() + "scripts\\";
                Diretorio = @DiretorioAtual + "GraficoTesteFinal.js";

                if (System.IO.File.Exists(Diretorio))
                {
                    System.IO.File.Delete(Diretorio);
                }
                Grafico = System.IO.File.Open(@DiretorioAtual + "\\GraficoTesteFinal.js", System.IO.FileMode.Create);
                ArquivoTesteFinal = new System.IO.StreamWriter(Grafico);
                ArquivoTesteFinal.WriteLine("\tvar ctx = document.getElementById('GraficoTesteFinal').getContext('2d');");
                ArquivoTesteFinal.WriteLine("\tvar myChart = new Chart(ctx, {");
                ArquivoTesteFinal.WriteLine("\t\ttype: 'doughnut',");
                ArquivoTesteFinal.WriteLine("\t\tdata: {");
                ArquivoTesteFinal.WriteLine("\t\t\tlabels:['Concluídos', 'Pendentes'],");
                ArquivoTesteFinal.WriteLine("\t\t\tdatasets: [{");
                ArquivoTesteFinal.WriteLine("\t\t\t\tlabel: '# of Votes',");
                ArquivoTesteFinal.WriteLine("\t\t\t\tdata:[" + Variaveis_Globais.QtdTesteFinal.ToString() + ", " + (int.Parse(Variaveis_Globais.QtdOP) - int.Parse(Variaveis_Globais.QtdTesteFinal)).ToString() + "],");
                ArquivoTesteFinal.WriteLine("\t\t\t\tbackgroundColor:[");
                ArquivoTesteFinal.WriteLine("\t\t\t\t\t'rgba(0, 132, 142, 1)',");
                ArquivoTesteFinal.WriteLine("\t\t\t\t\t'rgba(128, 128, 128, 1)'");
                ArquivoTesteFinal.WriteLine("\t\t\t\t],");
                ArquivoTesteFinal.WriteLine("\t\t\t\tborderColor:[");
                ArquivoTesteFinal.WriteLine("\t\t\t\t\t'rgba(255, 255, 255, 0.2)',");
                ArquivoTesteFinal.WriteLine("\t\t\t\t\t'rgba(255, 255, 255, 0.2)'");
                ArquivoTesteFinal.WriteLine("\t\t\t\t],");
                ArquivoTesteFinal.WriteLine("\t\t\t\tborderWidth: 1");
                ArquivoTesteFinal.WriteLine("\t\t\t}]");
                ArquivoTesteFinal.WriteLine("\t\t},");
                ArquivoTesteFinal.WriteLine("\t\toptions: {");
                ArquivoTesteFinal.WriteLine("\t\t");
                ArquivoTesteFinal.WriteLine("\t\t}");
                ArquivoTesteFinal.WriteLine("\t});");
                ArquivoTesteFinal.Close();
                Grafico.Close();
            }
            catch (Exception erro)
            {
                // Deu erro na geração do arquivo de Serviços Atendidos
                String Msg = erro.ToString();
            }
        }

        //protected void tbNroOP_TextChanged(object sender, EventArgs e)
        //{
        //    if(tbNroOP.Text.Length == 6)
        //    {
        //        btnProcessar_Click(sender, e);
        //    }
        //}

        protected void btnHome_Click(object sender, EventArgs e)
        {
            Server.Transfer("Default.aspx");
        }

        protected void btnInspecao_Click(object sender, EventArgs e)
        {
            Server.Transfer("BoasVindas.aspx");
        }

        protected void btnAndon_Click(object sender, EventArgs e)
        {
            Server.Transfer("Andon.aspx");
        }
        #endregion

        #region Métodos
        // ################################################################################################## - NORMALIZAÇÃO
        private string dadosNr(string texto, decimal valNr, decimal valRef)
        {
            string ret = "";

            if (valRef != 0)
            {
                if (!(valNr < 0))
                {
                    ret = texto + " " + valNr + " [" + decimal.Round((valNr / valRef * 100), 1, MidpointRounding.AwayFromZero) + "%]";
                    return ret;
                }
                else
                {
                    ret = texto + " " + 0 + " [0%]";
                    return ret;
                }
            }
            else
            {
                ret = texto + " " + 0 + " [0%]";
                return ret;
            }
        }


        // ######################################################################################### - CÁLCULO DO PERCENTUAL
        private string percentual(string valor, string valorBase)
        {
            string ret = "";

            if (Convert.ToInt32(valorBase) != 0)
            {
                return ret = valor + " [" + decimal.Round((((Convert.ToDecimal(valor)) * 100) / (Convert.ToDecimal(valorBase))), 1, MidpointRounding.AwayFromZero).ToString() + "%]";
            }
            else
            {
                return ret;
            }
        }


        // ######################################################################### - MÉTODO PARA INVOCAR ROTINA JAVASCRIPT
        public void inserirGraficosNaPagina(object sender, EventArgs e)
        {
            MontaJSGraficoMontagem(sender, e);
            MontaJSGraficoTenAplicada(sender, e);
            MontaJSGraficoExatidao(sender, e);
            MontaJSGraficoMarcaLaser(sender, e);
            MontaJSGraficoTesteFinal(sender, e);

            /*
            string rotinaJavaScript = "GerarGraficos(" + valor1 + ","
                                                        + valor2 + ","
                                                        + valor3 + ","
                                                        + valor4 + ","
                                                        + valor5 + ","
                                                        + valor6 + ","
                                                        + valor7 + ","
                                                        + valor8 + ","
                                                        + valor9 + ","
                                                        + valor10 + ")";

            Page.ClientScript.RegisterClientScriptInclude("FormScript", "~/scripts/Andon.js");
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "mensagem", rotinaJavaScript, true);
            */
        }

        #endregion

        protected void ddlOP_SelectedIndexChanged(object sender, EventArgs e)
        {
            Variaveis_Globais.NroOP = ddlOP.SelectedItem.Text;
        }

        protected void ddlOP_TextChanged(object sender, EventArgs e)
        {
            Variaveis_Globais.NroOP = ddlOP.SelectedItem.Text;
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            Variaveis_Globais.DataHora = DateTime.Now.ToString();
            vDHAtualiza.InnerText = Variaveis_Globais.DataHora.Substring(0, Variaveis_Globais.DataHora.Length - 3);
            btnProcessar_Click(sender, e);
        }
    }
}