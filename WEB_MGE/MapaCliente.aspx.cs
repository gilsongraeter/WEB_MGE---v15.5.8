using System;
using MySql.Data.MySqlClient;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;

namespace WEB_MGE
{
    public partial class MapaCliente : System.Web.UI.Page
    {
        #region Atributos

        private int i;

        MySqlConnection conexao = null;
        MySqlDataAdapter adapter = null;
        DataSet dataSet = new DataSet();

        ListBox ListaCelulares = new ListBox();
        ListBox LatitudeCelulares = new ListBox();
        ListBox LongitudeCelulares = new ListBox();
        ListBox DataHoraCelulares = new ListBox();
        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            string DiretorioAtual;

            //User_IP = System.Security.Principal.WindowsIdentity.GetCurrent().Name.ToString();
            //DiretorioAtual = "C:\\MGE\\T.I\\Desenvolvimento\\WEB_MGE\\WEB_MGE - v15\\WEB_MGE\\scripts\\" + User_IP;
            DiretorioAtual = "C:\\FTP\\WEB_MGE\\scripts";
            Label1.Text = Variaveis_Globais.Cliente;
            if (Label1.Text == "AMPLA")
            {
                Label1.Text = "ENEL";
            }
            conexao = new MySqlConnection("server = " + Constantes.ENDERECO_SERVIDOR + "; User ID = " + Constantes.USUARIO_DATABASE + "; database = " + Variaveis_Globais.Cliente + "; password = " + Constantes.SENHA_DATABASE);
            if (!System.IO.Directory.Exists(DiretorioAtual))
            {
                System.IO.Directory.CreateDirectory(DiretorioAtual);
            }
            if (System.IO.File.Exists(@DiretorioAtual + "\\MapaAuxiliar.js"))
            {
                System.IO.File.Delete(DiretorioAtual + "\\MapaAuxiliar.js");
            }
            BtnVisaoGeral_Click(sender, e);
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

        protected void BtnVisaoGeral_Click(object sender, EventArgs e)
        {
            // Gera um arquivo de Log das UCs atendidas
            System.IO.Stream Maps;
            System.IO.StreamWriter ArquivoMapa;
            string DiretorioAtual;
            int indice_ultimo_elemento = 0;

            try
            {
                //DiretorioAtual = "C:\\MGE\\T.I\\Desenvolvimento\\WEB_MGE\\WEB_MGE - v15\\WEB_MGE\\scripts\\" + User_IP;
                DiretorioAtual = "C:\\FTP\\WEB_MGE\\scripts";

                Maps = System.IO.File.Open(@DiretorioAtual + "\\MapaAuxiliar.js", System.IO.FileMode.Create);
                ArquivoMapa = new System.IO.StreamWriter(Maps);
                ArquivoMapa.WriteLine("function initialize() {");

                // Seleciona do banco de dados todas instalacao e retiradas concluidas e começa a escrever latlng dessas ucs
                //----------------------------------------------------------------------------------------------------------
                ListaCelulares.Items.Clear();
                LatitudeCelulares.Items.Clear();
                LongitudeCelulares.Items.Clear();
                DataHoraCelulares.Items.Clear();
                conexao = new MySqlConnection("server = " + Constantes.ENDERECO_SERVIDOR + "; User ID = " + Constantes.USUARIO_DATABASE + "; database = " + Constantes.DATABASE_TB_PAINEL + "; password = " + Constantes.SENHA_DATABASE);
                adapter = new MySqlDataAdapter(string.Format("SELECT * FROM CELULARES;"), conexao);
                adapter.Fill(dataSet);
                for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                {
                    if (dataSet.Tables[0].Rows[i][Constantes.PROPRIETARIO_CELULAR].ToString() != "")
                    {
                        ListaCelulares.Items.Add(dataSet.Tables[0].Rows[i][Constantes.PROPRIETARIO_CELULAR].ToString());
                    }

                    if (dataSet.Tables[0].Rows[i][Constantes.LATITUDE_ATUAL_CELULAR].ToString() != "")
                    {
                        LatitudeCelulares.Items.Add(dataSet.Tables[0].Rows[i][Constantes.LATITUDE_ATUAL_CELULAR].ToString());
                    }

                    if (dataSet.Tables[0].Rows[i][Constantes.LONGITUDE_ATUAL_CELULAR].ToString() != "")
                    {
                        LongitudeCelulares.Items.Add(dataSet.Tables[0].Rows[i][Constantes.LONGITUDE_ATUAL_CELULAR].ToString());
                    }

                    if (dataSet.Tables[0].Rows[i][Constantes.DATA_HORA_CELULAR].ToString() != "")
                    {
                        DataHoraCelulares.Items.Add(dataSet.Tables[0].Rows[i][Constantes.DATA_HORA_CELULAR].ToString());
                    }
                }

                adapter = null;
                dataSet.Clear();

                conexao = new MySqlConnection("server = " + Constantes.ENDERECO_SERVIDOR + "; User ID = " + Constantes.USUARIO_DATABASE + "; database = " + Variaveis_Globais.Cliente + "; password = " + Constantes.SENHA_DATABASE);
                adapter = new MySqlDataAdapter(string.Format("SELECT * FROM TBCADASTRO WHERE (((STATUS_INSTALACAO = 'concluida') OR (STATUS_INSTALACAO = 'reinstalacao')) AND (STATUS_RETIRADA = 'concluida')) ORDER BY GRUPO, POSICAO;"), conexao);
                adapter.Fill(dataSet);
                for (i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                {
                    if ((dataSet.Tables[0].Rows[i][Constantes.LATITUDE_INSTALACAO].ToString() != null)&&(dataSet.Tables[0].Rows[i][Constantes.LATITUDE_INSTALACAO].ToString() != ""))
                    {
                        ArquivoMapa.WriteLine("    var latlng" + i.ToString() + " = new google.maps.LatLng(" + dataSet.Tables[0].Rows[i][Constantes.LATITUDE_INSTALACAO].ToString() + ", " + dataSet.Tables[0].Rows[i][Constantes.LONGITUDE_INSTALACAO].ToString() + ");");
                    }
                    else
                    {
                        ArquivoMapa.WriteLine("    var latlng" + i.ToString() + " = new google.maps.LatLng(" + dataSet.Tables[0].Rows[i][Constantes.LATITUDE].ToString() + ", " + dataSet.Tables[0].Rows[i][Constantes.LONGITUDE].ToString() + ");");
                    }
                }
                indice_ultimo_elemento = dataSet.Tables[0].Rows.Count;
                //-----------------------------------------------------------------------------------------------------------------------------

                // Seleciona do banco de dados todas instalacao concluidas e que ainda não foram retiradas e começa a escrever latlng dessas ucs
                //------------------------------------------------------------------------------------------------------------------------------
                adapter = null;
                dataSet.Clear();

                adapter = new MySqlDataAdapter(string.Format("SELECT * FROM TBCADASTRO WHERE (((STATUS_INSTALACAO = 'concluida') OR (STATUS_INSTALACAO = 'reinstalacao')) AND ((STATUS_RETIRADA = 'aberto') OR (STATUS_RETIRADA IS NULL))) ORDER BY GRUPO, POSICAO;"), conexao);
                adapter.Fill(dataSet);

                for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                {
                    if ((dataSet.Tables[0].Rows[i][Constantes.LATITUDE_INSTALACAO].ToString() != null) && (dataSet.Tables[0].Rows[i][Constantes.LATITUDE_INSTALACAO].ToString() != ""))
                    {
                        ArquivoMapa.WriteLine("    var latlng" + (i + indice_ultimo_elemento).ToString() + " = new google.maps.LatLng(" + dataSet.Tables[0].Rows[i][Constantes.LATITUDE_INSTALACAO].ToString() + ", " + dataSet.Tables[0].Rows[i][Constantes.LONGITUDE_INSTALACAO].ToString() + ");");
                    }
                    else
                    {
                        ArquivoMapa.WriteLine("    var latlng" + (i + indice_ultimo_elemento).ToString() + " = new google.maps.LatLng(" + dataSet.Tables[0].Rows[i][Constantes.LATITUDE].ToString() + ", " + dataSet.Tables[0].Rows[i][Constantes.LONGITUDE].ToString() + ");");
                    }
                }
                indice_ultimo_elemento = indice_ultimo_elemento + dataSet.Tables[0].Rows.Count;
                //-----------------------------------------------------------------------------------------------------------------------------

                // Seleciona do banco de dados todas retirada canceladas e começa a escrever latlng dessas ucs
                //------------------------------------------------------------------------------------------------------------------------------
                adapter = null;
                dataSet.Clear();

                adapter = new MySqlDataAdapter(string.Format("SELECT * FROM TBCADASTRO WHERE STATUS_RETIRADA = 'cancelada' ORDER BY GRUPO, POSICAO;"), conexao);
                adapter.Fill(dataSet);

                for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                {
                    if ((dataSet.Tables[0].Rows[i][Constantes.LATITUDE_INSTALACAO].ToString() != null) && (dataSet.Tables[0].Rows[i][Constantes.LATITUDE_INSTALACAO].ToString() != ""))
                    {
                        ArquivoMapa.WriteLine("    var latlng" + (i + indice_ultimo_elemento).ToString() + " = new google.maps.LatLng(" + dataSet.Tables[0].Rows[i][Constantes.LATITUDE_INSTALACAO].ToString() + ", " + dataSet.Tables[0].Rows[i][Constantes.LONGITUDE_INSTALACAO].ToString() + ");");
                    }
                    else
                    {
                        ArquivoMapa.WriteLine("    var latlng" + (i + indice_ultimo_elemento).ToString() + " = new google.maps.LatLng(" + dataSet.Tables[0].Rows[i][Constantes.LATITUDE].ToString() + ", " + dataSet.Tables[0].Rows[i][Constantes.LONGITUDE].ToString() + ");");
                    }
                }
                indice_ultimo_elemento = indice_ultimo_elemento + dataSet.Tables[0].Rows.Count;
                //-----------------------------------------------------------------------------------------------------------------------------

                // Seleciona do banco de dados todos os grupos concluidos e monta uma lista
                //-------------------------------------------------------------------------
                adapter = null;
                dataSet.Clear();

                adapter = new MySqlDataAdapter(string.Format("SELECT distinct GRUPO FROM TBCADASTRO WHERE (STATUS_INSTALACAO = 'concluida' OR STATUS_INSTALACAO = 'reinstalacao') ORDER BY GRUPO, POSICAO;"), conexao);
                adapter.Fill(dataSet);

                List<string> lista_grupos_concluidos = new List<string>();

                for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                {
                    lista_grupos_concluidos.Add(dataSet.Tables[0].Rows[i][Constantes.GRUPO].ToString());
                }
                //-------------------------------------------------------------------------

                // Seleciona do banco de dados todos os grupos que são posicao 1 e não tem instalacao
                //-----------------------------------------------------------------------------------
                adapter = null;
                dataSet.Clear();

                adapter = new MySqlDataAdapter(string.Format("SELECT * FROM TBCADASTRO WHERE (POSICAO = '1' AND (STATUS_INSTALACAO IS NULL OR STATUS_INSTALACAO = 'aberto')) ORDER BY GRUPO, POSICAO;"), conexao);
                adapter.Fill(dataSet);
                for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                {
                    // Se nesta selecao o grupo já tiver uc concluida, passa pra proxima
                    if (lista_grupos_concluidos.Any(dataSet.Tables[0].Rows[i][Constantes.GRUPO].ToString().Equals))
                    {
                        continue;                    
                    }

                    if ((dataSet.Tables[0].Rows[i][Constantes.LATITUDE_INSTALACAO].ToString() != null) && (dataSet.Tables[0].Rows[i][Constantes.LATITUDE_INSTALACAO].ToString() != ""))
                    {
                        ArquivoMapa.WriteLine("    var latlng" + (i + indice_ultimo_elemento).ToString() + " = new google.maps.LatLng(" + dataSet.Tables[0].Rows[i][Constantes.LATITUDE_INSTALACAO].ToString() + ", " + dataSet.Tables[0].Rows[i][Constantes.LONGITUDE_INSTALACAO].ToString() + ");");
                    }
                    else
                    {
                        ArquivoMapa.WriteLine("    var latlng" + (i + indice_ultimo_elemento).ToString() + " = new google.maps.LatLng(" + dataSet.Tables[0].Rows[i][Constantes.LATITUDE].ToString() + ", " + dataSet.Tables[0].Rows[i][Constantes.LONGITUDE].ToString() + ");");
                    }
                }
                //-------------------------------------------------------------------------
                // Monta a lista de carros
                
                if (Variaveis_Globais.Usuario == "Admin")
                {
                    for (int i = 0; i < ListaCelulares.Items.Count; i++)
                    {
                        ArquivoMapa.WriteLine("    var latlng_cars" + i.ToString() + " = new google.maps.LatLng(" + LatitudeCelulares.Items[i].ToString() + ", " + LongitudeCelulares.Items[i].ToString() + ");");
                    }
                }
                //-------------------------------------------------------------------------
                ArquivoMapa.WriteLine("");

                ArquivoMapa.WriteLine("    var map = new google.maps.Map(document.getElementById('map'), {");
                ArquivoMapa.WriteLine("        zoom: 7,");
                ArquivoMapa.WriteLine("        center: latlng0,");
                ArquivoMapa.WriteLine("        mapTypeId: google.maps.MapTypeId.ROADMAP");
                ArquivoMapa.WriteLine("    });");
                ArquivoMapa.WriteLine("");

                ArquivoMapa.WriteLine("    var pinColorBase = " + '\u0022' + "FFFFFF" + '\u0022' + ";");
                ArquivoMapa.WriteLine("    var pinColorAberto = " + '\u0022' + "0000FF" + '\u0022' + ";");
                ArquivoMapa.WriteLine("    var pinColorConcluido = " + '\u0022' + "00FF00" + '\u0022' + ";");
                ArquivoMapa.WriteLine("    var pinColorCancelado = " + '\u0022' + "FF0000" + '\u0022' + ";");
                ArquivoMapa.WriteLine("    var pinColorTrafo = " + '\u0022' + "FFFF00" + '\u0022' + ";");
                ArquivoMapa.WriteLine("    var pinColorRural = " + '\u0022' + "FF00F0" + '\u0022' + ";");
                ArquivoMapa.WriteLine("    var pinColorResidencial = " + '\u0022' + "00FFFF" + '\u0022' + ";");
                ArquivoMapa.WriteLine("    var pinColorComercial = " + '\u0022' + "FF0000" + '\u0022' + ";");
                ArquivoMapa.WriteLine("    var pinColorIndustrial = " + '\u0022' + "00FF00" + '\u0022' + ";");
                ArquivoMapa.WriteLine("    var pinColorPoderPublico = " + '\u0022' + "0000FF" + '\u0022' + ";");
                ArquivoMapa.WriteLine("    var pinColorNULL = " + '\u0022' + "F00FFF" + '\u0022' + ";");
                ArquivoMapa.WriteLine("    var pinImageCar = new google.maps.MarkerImage(" + '\u0022' + "http://icons.iconarchive.com/icons/icons-land/transporter/32/Taxi-Top-Yellow-icon.png" + '\u0022' + ",");
                ArquivoMapa.WriteLine("        new google.maps.Size(42, 68),");
                ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                ArquivoMapa.WriteLine("        new google.maps.Point(20, 68));");
                ArquivoMapa.WriteLine("");
                ArquivoMapa.WriteLine("    var pinImageBase = new google.maps.MarkerImage(" + '\u0022' + "http://chart.apis.google.com/chart?chst=d_map_pin_letter&chld=%E2%80%A2|" + '\u0022' + " + pinColorBase,");
                ArquivoMapa.WriteLine("        new google.maps.Size(42, 68),");
                ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                ArquivoMapa.WriteLine("        new google.maps.Point(20, 68));");
                ArquivoMapa.WriteLine("");
                ArquivoMapa.WriteLine("    var pinImageAberto = new google.maps.MarkerImage(" + '\u0022' + "http://chart.apis.google.com/chart?chst=d_map_pin_letter&chld=%E2%80%A2|" + '\u0022' + " + pinColorAberto,");
                ArquivoMapa.WriteLine("        new google.maps.Size(21, 34),");
                ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                ArquivoMapa.WriteLine("        new google.maps.Point(10, 34));");
                ArquivoMapa.WriteLine("");
                ArquivoMapa.WriteLine("    var pinImageConcluido = new google.maps.MarkerImage(" + '\u0022' + "http://chart.apis.google.com/chart?chst=d_map_pin_letter&chld=%E2%80%A2|" + '\u0022' + " + pinColorConcluido,");
                ArquivoMapa.WriteLine("        new google.maps.Size(21, 34),");
                ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                ArquivoMapa.WriteLine("        new google.maps.Point(10, 34));");
                ArquivoMapa.WriteLine("");
                ArquivoMapa.WriteLine("    var pinImageCancelado = new google.maps.MarkerImage(" + '\u0022' + "http://chart.apis.google.com/chart?chst=d_map_pin_letter&chld=%E2%80%A2|" + '\u0022' + " + pinColorCancelado,");
                ArquivoMapa.WriteLine("        new google.maps.Size(21, 34),");
                ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                ArquivoMapa.WriteLine("        new google.maps.Point(10, 34));");
                ArquivoMapa.WriteLine("");
                ArquivoMapa.WriteLine("    var pinImageNULL = new google.maps.MarkerImage(" + '\u0022' + "http://chart.apis.google.com/chart?chst=d_map_pin_letter&chld=%E2%80%A2|" + '\u0022' + " + pinColorNULL,");
                ArquivoMapa.WriteLine("        new google.maps.Size(21, 34),");
                ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                ArquivoMapa.WriteLine("        new google.maps.Point(10, 34));");
                ArquivoMapa.WriteLine("");
                ArquivoMapa.WriteLine("    var pinImageTrafo = new google.maps.MarkerImage(" + '\u0022' + "http://icons.iconarchive.com/icons/iconsmind/outline/16/Communication-Tower-2-icon.png" + '\u0022' + ",");
                ArquivoMapa.WriteLine("        new google.maps.Size(21, 34),");
                ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                ArquivoMapa.WriteLine("        new google.maps.Point(21, 34));");
                ArquivoMapa.WriteLine("");
                ArquivoMapa.WriteLine("    var pinImageTrafoDisponivel = new google.maps.MarkerImage(" + '\u0022' + "images/trafo_disponivel.png" + '\u0022' + ",");
                ArquivoMapa.WriteLine("        new google.maps.Size(21, 34),");
                ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                ArquivoMapa.WriteLine("        new google.maps.Point(21, 34));");
                ArquivoMapa.WriteLine("");
                ArquivoMapa.WriteLine("    var pinImageTrafoConcluido = new google.maps.MarkerImage(" + '\u0022' + "images/trafo_concluido.png" + '\u0022' + ",");
                ArquivoMapa.WriteLine("        new google.maps.Size(21, 34),");
                ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                ArquivoMapa.WriteLine("        new google.maps.Point(21, 34));");
                ArquivoMapa.WriteLine("");
                ArquivoMapa.WriteLine("    var pinImageTrafoInstConcluido = new google.maps.MarkerImage(" + '\u0022' + "images/trafo_inst_conc.png" + '\u0022' + ",");
                ArquivoMapa.WriteLine("        new google.maps.Size(21, 34),");
                ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                ArquivoMapa.WriteLine("        new google.maps.Point(21, 34));");
                ArquivoMapa.WriteLine("");
                ArquivoMapa.WriteLine("    var pinImageTrafoInstCancelado = new google.maps.MarkerImage(" + '\u0022' + "images/trafo_inst_canc.png" + '\u0022' + ",");
                ArquivoMapa.WriteLine("        new google.maps.Size(21, 34),");
                ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                ArquivoMapa.WriteLine("        new google.maps.Point(21, 34));");
                ArquivoMapa.WriteLine("");
                ArquivoMapa.WriteLine("    var pinImageTrafoCancelado = new google.maps.MarkerImage(" + '\u0022' + "images/trafo_cancelado.png" + '\u0022' + ",");
                ArquivoMapa.WriteLine("        new google.maps.Size(21, 34),");
                ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                ArquivoMapa.WriteLine("        new google.maps.Point(10, 34));");
                ArquivoMapa.WriteLine("");
                ArquivoMapa.WriteLine("    var pinImageRural = new google.maps.MarkerImage(" + '\u0022' + "http://chart.apis.google.com/chart?chst=d_map_pin_letter&chld=%E2%80%A2|" + '\u0022' + " + pinColorRural,");
                ArquivoMapa.WriteLine("        new google.maps.Size(32, 32),");
                ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                ArquivoMapa.WriteLine("        new google.maps.Point(32, 32));");
                ArquivoMapa.WriteLine("");
                ArquivoMapa.WriteLine("    var pinImageRuralDisponivel = new google.maps.MarkerImage(" + '\u0022' + "images/rural_disponivel.png" + '\u0022' + ",");
                ArquivoMapa.WriteLine("        new google.maps.Size(32, 32),");
                ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                ArquivoMapa.WriteLine("        new google.maps.Point(32, 32));");
                ArquivoMapa.WriteLine("");
                ArquivoMapa.WriteLine("    var pinImageRuralConcluido = new google.maps.MarkerImage(" + '\u0022' + "images/rural_concluido.png" + '\u0022' + ",");
                ArquivoMapa.WriteLine("        new google.maps.Size(32, 32),");
                ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                ArquivoMapa.WriteLine("        new google.maps.Point(32, 32));");
                ArquivoMapa.WriteLine("");
                ArquivoMapa.WriteLine("    var pinImageRuralInstConcluido = new google.maps.MarkerImage(" + '\u0022' + "images/rural_inst_conc.png" + '\u0022' + ",");
                ArquivoMapa.WriteLine("        new google.maps.Size(32, 32),");
                ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                ArquivoMapa.WriteLine("        new google.maps.Point(32, 32));");
                ArquivoMapa.WriteLine("");
                ArquivoMapa.WriteLine("    var pinImageRuralInstCancelado = new google.maps.MarkerImage(" + '\u0022' + "images/rural_inst_canc.png" + '\u0022' + ",");
                ArquivoMapa.WriteLine("        new google.maps.Size(32, 32),");
                ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                ArquivoMapa.WriteLine("        new google.maps.Point(32, 32));");
                ArquivoMapa.WriteLine("");
                ArquivoMapa.WriteLine("    var pinImageRuralCancelado = new google.maps.MarkerImage(" + '\u0022' + "images/rural_cancelado.png" + '\u0022' + ",");
                ArquivoMapa.WriteLine("        new google.maps.Size(32, 32),");
                ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                ArquivoMapa.WriteLine("        new google.maps.Point(32, 32));");
                ArquivoMapa.WriteLine("");
                ArquivoMapa.WriteLine("    var pinImageResidencial = new google.maps.MarkerImage(" + '\u0022' + "http://chart.apis.google.com/chart?chst=d_map_pin_letter&chld=%E2%80%A2|" + '\u0022' + " + pinColorResidencial,");
                ArquivoMapa.WriteLine("        new google.maps.Size(32, 32),");
                ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                ArquivoMapa.WriteLine("        new google.maps.Point(32, 32));");
                ArquivoMapa.WriteLine("");
                ArquivoMapa.WriteLine("    var pinImageResidencialDisponivel = new google.maps.MarkerImage(" + '\u0022' + "images/residencial_disponivel.png" + '\u0022' + ",");
                ArquivoMapa.WriteLine("        new google.maps.Size(32, 32),");
                ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                ArquivoMapa.WriteLine("        new google.maps.Point(32, 32));");
                ArquivoMapa.WriteLine("");
                ArquivoMapa.WriteLine("    var pinImageResidencialConcluido = new google.maps.MarkerImage(" + '\u0022' + "images/residencial_concluido.png" + '\u0022' + ",");
                ArquivoMapa.WriteLine("        new google.maps.Size(32, 32),");
                ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                ArquivoMapa.WriteLine("        new google.maps.Point(32, 32));");
                ArquivoMapa.WriteLine("");
                ArquivoMapa.WriteLine("    var pinImageResidencialInstConcluido = new google.maps.MarkerImage(" + '\u0022' + "images/residencial_inst_conc.png" + '\u0022' + ",");
                ArquivoMapa.WriteLine("        new google.maps.Size(32, 32),");
                ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                ArquivoMapa.WriteLine("        new google.maps.Point(32, 32));");
                ArquivoMapa.WriteLine("");
                ArquivoMapa.WriteLine("    var pinImageResidencialInstCancelado = new google.maps.MarkerImage(" + '\u0022' + "images/residencial_inst_canc.png" + '\u0022' + ",");
                ArquivoMapa.WriteLine("        new google.maps.Size(32, 32),");
                ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                ArquivoMapa.WriteLine("        new google.maps.Point(32, 32));");
                ArquivoMapa.WriteLine("");
                ArquivoMapa.WriteLine("    var pinImageResidencialCancelado = new google.maps.MarkerImage(" + '\u0022' + "images/residencial_cancelado.png" + '\u0022' + ",");
                ArquivoMapa.WriteLine("        new google.maps.Size(32, 32),");
                ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                ArquivoMapa.WriteLine("        new google.maps.Point(32, 32));");
                ArquivoMapa.WriteLine("");
                ArquivoMapa.WriteLine("    var pinImageIndustrial = new google.maps.MarkerImage(" + '\u0022' + "http://chart.apis.google.com/chart?chst=d_map_pin_letter&chld=%E2%80%A2|" + '\u0022' + " + pinColorIndustrial,");
                ArquivoMapa.WriteLine("        new google.maps.Size(32, 32),");
                ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                ArquivoMapa.WriteLine("        new google.maps.Point(32, 32));");
                ArquivoMapa.WriteLine("");
                ArquivoMapa.WriteLine("    var pinImageIndustrialDisponivel = new google.maps.MarkerImage(" + '\u0022' + "images/industrial_disponivel.png" + '\u0022' + ",");
                ArquivoMapa.WriteLine("        new google.maps.Size(32, 32),");
                ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                ArquivoMapa.WriteLine("        new google.maps.Point(32, 32));");
                ArquivoMapa.WriteLine("");
                ArquivoMapa.WriteLine("    var pinImageIndustrialConcluido = new google.maps.MarkerImage(" + '\u0022' + "images/industrial_concluido.png" + '\u0022' + ",");
                ArquivoMapa.WriteLine("        new google.maps.Size(32, 32),");
                ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                ArquivoMapa.WriteLine("        new google.maps.Point(32, 32));");
                ArquivoMapa.WriteLine("");
                ArquivoMapa.WriteLine("    var pinImageIndustrialInstConcluido = new google.maps.MarkerImage(" + '\u0022' + "images/industrial_inst_conc.png" + '\u0022' + ",");
                ArquivoMapa.WriteLine("        new google.maps.Size(32, 32),");
                ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                ArquivoMapa.WriteLine("        new google.maps.Point(32, 32));");
                ArquivoMapa.WriteLine("");
                ArquivoMapa.WriteLine("    var pinImageIndustrialInstCancelado = new google.maps.MarkerImage(" + '\u0022' + "images/industrial_inst_canc.png" + '\u0022' + ",");
                ArquivoMapa.WriteLine("        new google.maps.Size(32, 32),");
                ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                ArquivoMapa.WriteLine("        new google.maps.Point(32, 32));");
                ArquivoMapa.WriteLine("");
                ArquivoMapa.WriteLine("    var pinImageIndustrialCancelado = new google.maps.MarkerImage(" + '\u0022' + "images/industrial_cancelado.png" + '\u0022' + ",");
                ArquivoMapa.WriteLine("        new google.maps.Size(32, 32),");
                ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                ArquivoMapa.WriteLine("        new google.maps.Point(32, 32));");
                ArquivoMapa.WriteLine("");
                ArquivoMapa.WriteLine("    var pinImageComercial = new google.maps.MarkerImage(" + '\u0022' + "http://chart.apis.google.com/chart?chst=d_map_pin_letter&chld=%E2%80%A2|" + '\u0022' + " + pinColorComercial,");
                ArquivoMapa.WriteLine("        new google.maps.Size(32, 32),");
                ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                ArquivoMapa.WriteLine("        new google.maps.Point(32, 32));");
                ArquivoMapa.WriteLine("");
                ArquivoMapa.WriteLine("    var pinImageComercialDisponivel = new google.maps.MarkerImage(" + '\u0022' + "images/comercial_disponivel.png" + '\u0022' + ",");
                ArquivoMapa.WriteLine("        new google.maps.Size(32, 32),");
                ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                ArquivoMapa.WriteLine("        new google.maps.Point(32, 32));");
                ArquivoMapa.WriteLine("");
                ArquivoMapa.WriteLine("    var pinImageComercialConcluido = new google.maps.MarkerImage(" + '\u0022' + "images/comercial_concluido.png" + '\u0022' + ",");
                ArquivoMapa.WriteLine("        new google.maps.Size(32, 32),");
                ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                ArquivoMapa.WriteLine("        new google.maps.Point(32, 32));");
                ArquivoMapa.WriteLine("");
                ArquivoMapa.WriteLine("    var pinImageComercialInstConcluido = new google.maps.MarkerImage(" + '\u0022' + "images/comercial_inst_conc.png" + '\u0022' + ",");
                ArquivoMapa.WriteLine("        new google.maps.Size(32, 32),");
                ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                ArquivoMapa.WriteLine("        new google.maps.Point(32, 32));");
                ArquivoMapa.WriteLine("");
                ArquivoMapa.WriteLine("    var pinImageComercialInstCancelado = new google.maps.MarkerImage(" + '\u0022' + "images/comercial_inst_canc.png" + '\u0022' + ",");
                ArquivoMapa.WriteLine("        new google.maps.Size(32, 32),");
                ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                ArquivoMapa.WriteLine("        new google.maps.Point(32, 32));");
                ArquivoMapa.WriteLine("");
                ArquivoMapa.WriteLine("    var pinImageComercialCancelado = new google.maps.MarkerImage(" + '\u0022' + "images/comercial_cancelado.png" + '\u0022' + ",");
                ArquivoMapa.WriteLine("        new google.maps.Size(32, 32),");
                ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                ArquivoMapa.WriteLine("        new google.maps.Point(32, 32));");
                ArquivoMapa.WriteLine("");
                ArquivoMapa.WriteLine("    var pinImageServicoPublico = new google.maps.MarkerImage(" + '\u0022' + "http://chart.apis.google.com/chart?chst=d_map_pin_letter&chld=%E2%80%A2|" + '\u0022' + " + pinColorPoderPublico,");
                ArquivoMapa.WriteLine("        new google.maps.Size(24, 24),");
                ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                ArquivoMapa.WriteLine("        new google.maps.Point(24, 24));");
                ArquivoMapa.WriteLine("");
                ArquivoMapa.WriteLine("    var pinImageServicoPublicoDisponivel = new google.maps.MarkerImage(" + '\u0022' + "images/servico_publilco_disponivel.png" + '\u0022' + ",");
                ArquivoMapa.WriteLine("        new google.maps.Size(24, 24),");
                ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                ArquivoMapa.WriteLine("        new google.maps.Point(24, 24));");
                ArquivoMapa.WriteLine("");
                ArquivoMapa.WriteLine("    var pinImageServicoPublicoConcluido = new google.maps.MarkerImage(" + '\u0022' + "images/servico_publico_concluido.png" + '\u0022' + ",");
                ArquivoMapa.WriteLine("        new google.maps.Size(24, 24),");
                ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                ArquivoMapa.WriteLine("        new google.maps.Point(24, 24));");
                ArquivoMapa.WriteLine("");
                ArquivoMapa.WriteLine("    var pinImageServicoPublicoInstConcluido = new google.maps.MarkerImage(" + '\u0022' + "images/servico_publico_inst_conc.png" + '\u0022' + ",");
                ArquivoMapa.WriteLine("        new google.maps.Size(24, 24),");
                ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                ArquivoMapa.WriteLine("        new google.maps.Point(24, 24));");
                ArquivoMapa.WriteLine("");
                ArquivoMapa.WriteLine("    var pinImageServicoPublicoInstCancelado = new google.maps.MarkerImage(" + '\u0022' + "images/servico_publico_inst_canc.png" + '\u0022' + ",");
                ArquivoMapa.WriteLine("        new google.maps.Size(24, 24),");
                ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                ArquivoMapa.WriteLine("        new google.maps.Point(24, 24));");
                ArquivoMapa.WriteLine("");
                ArquivoMapa.WriteLine("    var pinImageServicoPublicoCancelado = new google.maps.MarkerImage(" + '\u0022' + "images/servico_publico_cancelado.png" + '\u0022' + ",");
                ArquivoMapa.WriteLine("        new google.maps.Size(24, 24),");
                ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                ArquivoMapa.WriteLine("        new google.maps.Point(24, 24));");
                ArquivoMapa.WriteLine("");

                // Seleciona do banco de dados todas instalacao e retiradas concluidas e começa a escrever latlng dessas ucs
                //----------------------------------------------------------------------------------------------------------
                adapter = null;
                dataSet.Clear();

                adapter = new MySqlDataAdapter(string.Format("SELECT * FROM TBCADASTRO WHERE (((STATUS_INSTALACAO = 'concluida') OR (STATUS_INSTALACAO = 'reinstalacao')) AND (STATUS_RETIRADA = 'concluida')) ORDER BY GRUPO, POSICAO;"), conexao);
                adapter.Fill(dataSet);

                for (int i = 1; i < dataSet.Tables[0].Rows.Count; i++)
                {
                    string IndiceMarcador = i.ToString();

                    ArquivoMapa.WriteLine("    var marker" + i.ToString() + " = new google.maps.Marker({");
                    ArquivoMapa.WriteLine("        position: latlng" + i.ToString() + ",");
                    ArquivoMapa.WriteLine("        map: map,");
                    ArquivoMapa.WriteLine("        title: " + '\u0022' + "UC: " + dataSet.Tables[0].Rows[i][Constantes.UC].ToString() + '\u0022' + ",");

                    if (dataSet.Tables[0].Rows[i][Constantes.CLASSE].ToString() == "Transformador")
                    {
                        ArquivoMapa.WriteLine("        icon: pinImageTrafoConcluido");
                    }
                    else if (dataSet.Tables[0].Rows[i][Constantes.CLASSE].ToString() == "Residencial")
                    {
                        ArquivoMapa.WriteLine("        icon: pinImageResidencialConcluido");
                    }
                    else if (dataSet.Tables[0].Rows[i][Constantes.CLASSE].ToString() == "Rural")
                    {
                        ArquivoMapa.WriteLine("        icon: pinImageRuralConcluido");
                    }
                    else if (dataSet.Tables[0].Rows[i][Constantes.CLASSE].ToString() == "Comercial")
                    {
                        ArquivoMapa.WriteLine("        icon: pinImageComercialConcluido");
                    }
                    else if (dataSet.Tables[0].Rows[i][Constantes.CLASSE].ToString() == "Servico Publico")
                    {
                        ArquivoMapa.WriteLine("        icon: pinImageServicoPublicoConcluido");
                    }
                    else if (dataSet.Tables[0].Rows[i][Constantes.CLASSE].ToString() == "Industrial")
                    {
                        ArquivoMapa.WriteLine("        icon: pinImageIndustrialConcluido");
                    }
                    ArquivoMapa.WriteLine("    });");

                    // Listen for click event  
                    ArquivoMapa.WriteLine("    google.maps.event.addListener(marker" + IndiceMarcador + ", " + '\u0022' + "click" + '\u0022' + ", function (e) {");
                    ArquivoMapa.WriteLine("        var infoWindow = new google.maps.InfoWindow({");
                    ArquivoMapa.WriteLine("        content: 'UC: " + dataSet.Tables[0].Rows[i][Constantes.UC].ToString() + "' + '<br />Grupo: " + dataSet.Tables[0].Rows[i][Constantes.GRUPO].ToString() + "' + '<br />Posição: " + dataSet.Tables[0].Rows[i][Constantes.POSICAO].ToString() + "' + '<br />Subclasse: " + dataSet.Tables[0].Rows[i][Constantes.SUBCLASSE].ToString() + "' + '<br />Equipamento: " + dataSet.Tables[0].Rows[i][Constantes.TIPO_EQUIPAMENTO].ToString() + "' + '<br />Fases: " + dataSet.Tables[0].Rows[i][Constantes.FASE_CONSUMIDOR].ToString() + "' + '<br /><br />Consumidor: " + dataSet.Tables[0].Rows[i][Constantes.NOME_CONSUMIDOR].ToString() + "' + '<br />Latitude: ' + latlng" + i.ToString() + ".lat() + '<br />Longitude: ' + latlng" + i.ToString() + ".lng()");
                    ArquivoMapa.WriteLine("        });");
                    ArquivoMapa.WriteLine("        infoWindow.open(map, marker" + i.ToString() + ");");
                    ArquivoMapa.WriteLine("    });");
                }
                indice_ultimo_elemento = dataSet.Tables[0].Rows.Count;
                //-------------------------------------------------------------------------------------------------------------

                // Seleciona do banco de dados todas instalacao concluidas e que ainda não foram retiradas e começa a escrever latlng dessas ucs
                //------------------------------------------------------------------------------------------------------------------------------
                adapter = null;
                dataSet.Clear();

                adapter = new MySqlDataAdapter(string.Format("SELECT * FROM TBCADASTRO WHERE (((STATUS_INSTALACAO = 'concluida') OR (STATUS_INSTALACAO = 'reinstalacao')) AND ((STATUS_RETIRADA = 'aberto') OR (STATUS_RETIRADA IS NULL))) ORDER BY GRUPO, POSICAO;"), conexao);
                adapter.Fill(dataSet);

                for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                {
                    ArquivoMapa.WriteLine("    var marker" + (i+indice_ultimo_elemento).ToString() + " = new google.maps.Marker({");
                    ArquivoMapa.WriteLine("        position: latlng" + (i+indice_ultimo_elemento).ToString() + ",");
                    ArquivoMapa.WriteLine("        map: map,");
                    ArquivoMapa.WriteLine("        title: " + '\u0022' + "UC: " + dataSet.Tables[0].Rows[i][Constantes.UC].ToString() + '\u0022' + ",");

                    if (dataSet.Tables[0].Rows[i][Constantes.CLASSE].ToString() == "Transformador")
                    {
                        ArquivoMapa.WriteLine("        icon: pinImageTrafoInstConcluido");
                    }
                    else if (dataSet.Tables[0].Rows[i][Constantes.CLASSE].ToString() == "Residencial")
                    {
                        ArquivoMapa.WriteLine("        icon: pinImageResidencialInstConcluido");
                    }
                    else if (dataSet.Tables[0].Rows[i][Constantes.CLASSE].ToString() == "Rural")
                    {
                        ArquivoMapa.WriteLine("        icon: pinImageRuralInstConcluido");
                    }
                    else if (dataSet.Tables[0].Rows[i][Constantes.CLASSE].ToString() == "Comercial")
                    {
                        ArquivoMapa.WriteLine("        icon: pinImageComercialInstConcluido");
                    }
                    else if (dataSet.Tables[0].Rows[i][Constantes.CLASSE].ToString() == "Servico Publico")
                    {
                        ArquivoMapa.WriteLine("        icon: pinImageServicoPublicoInstConcluido");
                    }
                    else if (dataSet.Tables[0].Rows[i][Constantes.CLASSE].ToString() == "Industrial")
                    {
                        ArquivoMapa.WriteLine("        icon: pinImageIndustrialInstConcluido");
                    }
                    ArquivoMapa.WriteLine("    });");

                    // Listen for click event  
                    ArquivoMapa.WriteLine("    google.maps.event.addListener(marker" + (i+indice_ultimo_elemento).ToString() + ", " + '\u0022' + "click" + '\u0022' + ", function (e) {");
                    ArquivoMapa.WriteLine("        var infoWindow = new google.maps.InfoWindow({");
                    //ArquivoMapa.WriteLine("        content: 'UC: " + dataSet.Tables[0].Rows[i][Constantes.UC].ToString() + "' + '<br />Grupo: " + dataSet.Tables[0].Rows[i][Constantes.GRUPO].ToString() + "' + '<br />Posição: " + dataSet.Tables[0].Rows[i][Constantes.POSICAO].ToString() + "' + '<br />Subclasse: " + dataSet.Tables[0].Rows[i][Constantes.SUBCLASSE].ToString() + "' + '<br /><br />Consumidor: " + dataSet.Tables[0].Rows[i][Constantes.NOME_CONSUMIDOR].ToString() + "' + '<br />Latitude: ' + latlng" + (i+indice_ultimo_elemento).ToString() + ".lat() + '<br />Longitude: ' + latlng" + (i+indice_ultimo_elemento).ToString() + ".lng()");
                    ArquivoMapa.WriteLine("        content: 'UC: " + dataSet.Tables[0].Rows[i][Constantes.UC].ToString() + "' + '<br />Grupo: " + dataSet.Tables[0].Rows[i][Constantes.GRUPO].ToString() + "' + '<br />Posição: " + dataSet.Tables[0].Rows[i][Constantes.POSICAO].ToString() + "' + '<br />Subclasse: " + dataSet.Tables[0].Rows[i][Constantes.SUBCLASSE].ToString() + "' + '<br />Equipamento: " + dataSet.Tables[0].Rows[i][Constantes.TIPO_EQUIPAMENTO].ToString() + "' + '<br />Fases: " + dataSet.Tables[0].Rows[i][Constantes.FASE_CONSUMIDOR].ToString() + "' + '<br /><br />Consumidor: " + dataSet.Tables[0].Rows[i][Constantes.NOME_CONSUMIDOR].ToString() + "' + '<br />Latitude: ' + latlng" + (i + indice_ultimo_elemento).ToString() + ".lat() + '<br />Longitude: ' + latlng" + (i + indice_ultimo_elemento).ToString() + ".lng()");
                    ArquivoMapa.WriteLine("        });");
                    ArquivoMapa.WriteLine("        infoWindow.open(map, marker" + (i+indice_ultimo_elemento).ToString() + ");");
                    ArquivoMapa.WriteLine("    });");
                }
                indice_ultimo_elemento = indice_ultimo_elemento + dataSet.Tables[0].Rows.Count;
                //-------------------------------------------------------------------------------------------------------------

                // Seleciona do banco de dados todas retiradas canceladas e começa a escrever latlng dessas ucs
                //-------------------------------------------------------------------------------------------------------------
                adapter = null;
                dataSet.Clear();

                adapter = new MySqlDataAdapter(string.Format("SELECT * FROM TBCADASTRO WHERE STATUS_RETIRADA = 'cancelada' ORDER BY GRUPO, POSICAO;"), conexao);
                adapter.Fill(dataSet);

                for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                {
                    ArquivoMapa.WriteLine("    var marker" + (i + indice_ultimo_elemento).ToString() + " = new google.maps.Marker({");
                    ArquivoMapa.WriteLine("        position: latlng" + (i + indice_ultimo_elemento).ToString() + ",");
                    ArquivoMapa.WriteLine("        map: map,");
                    ArquivoMapa.WriteLine("        title: " + '\u0022' + "UC: " + dataSet.Tables[0].Rows[i][Constantes.UC].ToString() + '\u0022' + ",");

                    if (dataSet.Tables[0].Rows[i][Constantes.CLASSE].ToString() == "Transformador")
                    {
                        ArquivoMapa.WriteLine("        icon: pinImageTrafoCancelado");
                    }
                    else if (dataSet.Tables[0].Rows[i][Constantes.CLASSE].ToString() == "Residencial")
                    {
                        ArquivoMapa.WriteLine("        icon: pinImageResidencialCancelado");
                    }
                    else if (dataSet.Tables[0].Rows[i][Constantes.CLASSE].ToString() == "Rural")
                    {
                        ArquivoMapa.WriteLine("        icon: pinImageRuralCancelado");
                    }
                    else if (dataSet.Tables[0].Rows[i][Constantes.CLASSE].ToString() == "Comercial")
                    {
                        ArquivoMapa.WriteLine("        icon: pinImageComercialCancelado");
                    }
                    else if (dataSet.Tables[0].Rows[i][Constantes.CLASSE].ToString() == "Servico Publico")
                    {
                        ArquivoMapa.WriteLine("        icon: pinImageServicoPublicoCancelado");
                    }
                    else if (dataSet.Tables[0].Rows[i][Constantes.CLASSE].ToString() == "Industrial")
                    {
                        ArquivoMapa.WriteLine("        icon: pinImageIndustrialCancelado");
                    }
                    ArquivoMapa.WriteLine("    });");

                    // Listen for click event  
                    ArquivoMapa.WriteLine("    google.maps.event.addListener(marker" + (i + indice_ultimo_elemento).ToString() + ", " + '\u0022' + "click" + '\u0022' + ", function (e) {");
                    ArquivoMapa.WriteLine("        var infoWindow = new google.maps.InfoWindow({");
                    //ArquivoMapa.WriteLine("        content: 'UC: " + dataSet.Tables[0].Rows[i][Constantes.UC].ToString() + "' + '<br />Grupo: " + dataSet.Tables[0].Rows[i][Constantes.GRUPO].ToString() + "' + '<br />Posição: " + dataSet.Tables[0].Rows[i][Constantes.POSICAO].ToString() + "' + '<br />Subclasse: " + dataSet.Tables[0].Rows[i][Constantes.SUBCLASSE].ToString() + "' + '<br /><br />Consumidor: " + dataSet.Tables[0].Rows[i][Constantes.NOME_CONSUMIDOR].ToString() + "' + '<br />Latitude: ' + latlng" + (i+indice_ultimo_elemento).ToString() + ".lat() + '<br />Longitude: ' + latlng" + (i+indice_ultimo_elemento).ToString() + ".lng()");
                    ArquivoMapa.WriteLine("        content: 'UC: " + dataSet.Tables[0].Rows[i][Constantes.UC].ToString() + "' + '<br />Grupo: " + dataSet.Tables[0].Rows[i][Constantes.GRUPO].ToString() + "' + '<br />Posição: " + dataSet.Tables[0].Rows[i][Constantes.POSICAO].ToString() + "' + '<br />Subclasse: " + dataSet.Tables[0].Rows[i][Constantes.SUBCLASSE].ToString() + "' + '<br />Equipamento: " + dataSet.Tables[0].Rows[i][Constantes.TIPO_EQUIPAMENTO].ToString() + "' + '<br />Fases: " + dataSet.Tables[0].Rows[i][Constantes.FASE_CONSUMIDOR].ToString() + "' + '<br /><br />Consumidor: " + dataSet.Tables[0].Rows[i][Constantes.NOME_CONSUMIDOR].ToString() + "' + '<br />Latitude: ' + latlng" + (i + indice_ultimo_elemento).ToString() + ".lat() + '<br />Longitude: ' + latlng" + (i + indice_ultimo_elemento).ToString() + ".lng()");
                    ArquivoMapa.WriteLine("        });");
                    ArquivoMapa.WriteLine("        infoWindow.open(map, marker" + (i + indice_ultimo_elemento).ToString() + ");");
                    ArquivoMapa.WriteLine("    });");
                }
                indice_ultimo_elemento = indice_ultimo_elemento + dataSet.Tables[0].Rows.Count;
                //-------------------------------------------------------------------------------------------------------------

                // Seleciona do banco de dados todos os grupos que são posicao 1 e não tem instalacao
                //-----------------------------------------------------------------------------------
                adapter = null;
                dataSet.Clear();

                adapter = new MySqlDataAdapter(string.Format("SELECT * FROM TBCADASTRO WHERE (POSICAO = '1' AND (STATUS_INSTALACAO IS NULL OR STATUS_INSTALACAO = 'aberto')) ORDER BY GRUPO, POSICAO;"), conexao);
                adapter.Fill(dataSet);

                for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                {
                    // Se nesta selecao o grupo já tiver uc concluida, passa pra proxima
                    if (lista_grupos_concluidos.Any(dataSet.Tables[0].Rows[i][Constantes.GRUPO].ToString().Equals))
                    {
                        continue;
                    }

                    ArquivoMapa.WriteLine("    var marker" + (i + indice_ultimo_elemento).ToString() + " = new google.maps.Marker({");
                    ArquivoMapa.WriteLine("        position: latlng" + (i + indice_ultimo_elemento).ToString() + ",");
                    ArquivoMapa.WriteLine("        map: map,");
                    ArquivoMapa.WriteLine("        title: " + '\u0022' + "UC: " + dataSet.Tables[0].Rows[i][Constantes.UC].ToString() + '\u0022' + ",");

                    if (dataSet.Tables[0].Rows[i][Constantes.CLASSE].ToString() == "Transformador")
                    {
                        ArquivoMapa.WriteLine("        icon: pinImageTrafoDisponivel");
                    }
                    else if (dataSet.Tables[0].Rows[i][Constantes.CLASSE].ToString() == "Residencial")
                    {
                        ArquivoMapa.WriteLine("        icon: pinImageResidencialDisponivel");
                    }
                    else if (dataSet.Tables[0].Rows[i][Constantes.CLASSE].ToString() == "Rural")
                    {
                        ArquivoMapa.WriteLine("        icon: pinImageRuralDisponivel");
                    }
                    else if (dataSet.Tables[0].Rows[i][Constantes.CLASSE].ToString() == "Comercial")
                    {
                        ArquivoMapa.WriteLine("        icon: pinImageComercialDisponivel");
                    }
                    else if (dataSet.Tables[0].Rows[i][Constantes.CLASSE].ToString() == "Servico Publico")
                    {
                        ArquivoMapa.WriteLine("        icon: pinImageServicoPublicoDisponivel");
                    }
                    else if (dataSet.Tables[0].Rows[i][Constantes.CLASSE].ToString() == "Industrial")
                    {
                        ArquivoMapa.WriteLine("        icon: pinImageIndustrialDisponivel");
                    }
                    ArquivoMapa.WriteLine("    });");

                    // Listen for click event  
                    ArquivoMapa.WriteLine("    google.maps.event.addListener(marker" + (i + indice_ultimo_elemento).ToString() + ", " + '\u0022' + "click" + '\u0022' + ", function (e) {");
                    ArquivoMapa.WriteLine("        var infoWindow = new google.maps.InfoWindow({");
                    //ArquivoMapa.WriteLine("        content: 'UC: " + dataSet.Tables[0].Rows[i][Constantes.UC].ToString() + "' + '<br />Grupo: " + dataSet.Tables[0].Rows[i][Constantes.GRUPO].ToString() + "' + '<br />Posição: " + dataSet.Tables[0].Rows[i][Constantes.POSICAO].ToString() + "' + '<br />Subclasse: " + dataSet.Tables[0].Rows[i][Constantes.SUBCLASSE].ToString() + "' + '<br /><br />Consumidor: " + dataSet.Tables[0].Rows[i][Constantes.NOME_CONSUMIDOR].ToString() + "' + '<br />Latitude: ' + latlng" + (i+indice_ultimo_elemento).ToString() + ".lat() + '<br />Longitude: ' + latlng" + (i+indice_ultimo_elemento).ToString() + ".lng()");
                    ArquivoMapa.WriteLine("        content: 'UC: " + dataSet.Tables[0].Rows[i][Constantes.UC].ToString() + "' + '<br />Grupo: " + dataSet.Tables[0].Rows[i][Constantes.GRUPO].ToString() + "' + '<br />Posição: " + dataSet.Tables[0].Rows[i][Constantes.POSICAO].ToString() + "' + '<br />Subclasse: " + dataSet.Tables[0].Rows[i][Constantes.SUBCLASSE].ToString() + "' + '<br />Equipamento: " + dataSet.Tables[0].Rows[i][Constantes.TIPO_EQUIPAMENTO].ToString() + "' + '<br />Fases: " + dataSet.Tables[0].Rows[i][Constantes.FASE_CONSUMIDOR].ToString() + "' + '<br /><br />Consumidor: " + dataSet.Tables[0].Rows[i][Constantes.NOME_CONSUMIDOR].ToString() + "' + '<br />Latitude: ' + latlng" + (i + indice_ultimo_elemento).ToString() + ".lat() + '<br />Longitude: ' + latlng" + (i + indice_ultimo_elemento).ToString() + ".lng()");
                    ArquivoMapa.WriteLine("        });");
                    ArquivoMapa.WriteLine("        infoWindow.open(map, marker" + (i + indice_ultimo_elemento).ToString() + ");");
                    ArquivoMapa.WriteLine("    });");
                }
                indice_ultimo_elemento = indice_ultimo_elemento + dataSet.Tables[0].Rows.Count;
                //-------------------------------------------------------------------------------------------------------------

                if (Variaveis_Globais.Usuario == "Admin")
                {
                    for (int i = 0; i < ListaCelulares.Items.Count; i++)
                    {
                        ArquivoMapa.WriteLine("    var marker_cars" + i.ToString() + " = new google.maps.Marker({");
                        ArquivoMapa.WriteLine("        position: latlng_cars" + i.ToString() + ",");
                        ArquivoMapa.WriteLine("        map: map,");
                        ArquivoMapa.WriteLine("        title: " + '\u0022' + ListaCelulares.Items[i].ToString() + '\u0022' + ",");
                        ArquivoMapa.WriteLine("        icon: pinImageCar");
                        ArquivoMapa.WriteLine("    });");

                        // Listen for click event  
                        ArquivoMapa.WriteLine("    google.maps.event.addListener(marker_cars" + (i).ToString() + ", " + '\u0022' + "click" + '\u0022' + ", function (e) {");
                        ArquivoMapa.WriteLine("        var infoWindow = new google.maps.InfoWindow({");
                        ArquivoMapa.WriteLine("        content: 'Equipe: " + ListaCelulares.Items[i].ToString() + "' + '<br />DataHora: " + DataHoraCelulares.Items[i].ToString() + "' + '<br />Latitude: ' + latlng_cars" + i.ToString() + ".lat() + '<br />Longitude: ' + latlng_cars" + i.ToString() + ".lng()");
                        ArquivoMapa.WriteLine("        });");
                        ArquivoMapa.WriteLine("        infoWindow.open(map, marker_cars" + (i).ToString() + ");");
                        ArquivoMapa.WriteLine("    });");
                    }
                }
                //-------------------------------------------------------------------------------------------------------------

                ArquivoMapa.WriteLine("}");
                ArquivoMapa.WriteLine("");
                ArquivoMapa.WriteLine("function error(msg){");
                ArquivoMapa.WriteLine("    var s = document.querySelector('#status');");
                ArquivoMapa.WriteLine("    s.InnerHTML = typeof msg == 'string' ? msg : " + '\u0022' + "failed" + '\u0022' + ";");
                ArquivoMapa.WriteLine("    s.className = 'fail';");
                ArquivoMapa.WriteLine("}");
                ArquivoMapa.WriteLine("");
                ArquivoMapa.WriteLine("if (navigator.geolocation) {");
                ArquivoMapa.WriteLine("    navigator.geolocation.getCurrentPosition(success, error);");
                ArquivoMapa.WriteLine("} else {");
                ArquivoMapa.WriteLine("    error('not supported');");
                ArquivoMapa.WriteLine("}");
                ArquivoMapa.WriteLine("");
                ArquivoMapa.WriteLine("google.maps.event.addDomListener(window, 'load', initialize);");
                ArquivoMapa.WriteLine("");
                ArquivoMapa.WriteLine("initialize();");
                
                ArquivoMapa.Close();
                Maps.Close();
            }
            catch
            {
                // Deu erro na geração do arquivo de Serviços Atendidos
            }
        }

        protected void BtnConcluidos_Click(object sender, EventArgs e)
        {
            ListaCelulares.Items.Clear();
            LatitudeCelulares.Items.Clear();
            LongitudeCelulares.Items.Clear();
            DataHoraCelulares.Items.Clear();
            conexao = new MySqlConnection("server = " + Constantes.ENDERECO_SERVIDOR + "; User ID = " + Constantes.USUARIO_DATABASE + "; database = " + Constantes.DATABASE_TB_PAINEL + "; password = " + Constantes.SENHA_DATABASE);
            adapter = new MySqlDataAdapter(string.Format("SELECT * FROM CELULARES;"), conexao);
            adapter.Fill(dataSet);
            for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
            {
                if (dataSet.Tables[0].Rows[i][Constantes.PROPRIETARIO_CELULAR].ToString() != "")
                {
                    ListaCelulares.Items.Add(dataSet.Tables[0].Rows[i][Constantes.PROPRIETARIO_CELULAR].ToString());
                }

                if (dataSet.Tables[0].Rows[i][Constantes.LATITUDE_ATUAL_CELULAR].ToString() != "")
                {
                    LatitudeCelulares.Items.Add(dataSet.Tables[0].Rows[i][Constantes.LATITUDE_ATUAL_CELULAR].ToString());
                }

                if (dataSet.Tables[0].Rows[i][Constantes.LONGITUDE_ATUAL_CELULAR].ToString() != "")
                {
                    LongitudeCelulares.Items.Add(dataSet.Tables[0].Rows[i][Constantes.LONGITUDE_ATUAL_CELULAR].ToString());
                }

                if (dataSet.Tables[0].Rows[i][Constantes.DATA_HORA_CELULAR].ToString() != "")
                {
                    DataHoraCelulares.Items.Add(dataSet.Tables[0].Rows[i][Constantes.DATA_HORA_CELULAR].ToString());
                }
            }

            adapter = null;
            dataSet.Clear();

            conexao = new MySqlConnection("server = " + Constantes.ENDERECO_SERVIDOR + "; User ID = " + Constantes.USUARIO_DATABASE + "; database = " + Variaveis_Globais.Cliente + "; password = " + Constantes.SENHA_DATABASE);
            adapter = new MySqlDataAdapter(string.Format("SELECT * FROM TBCADASTRO WHERE (((STATUS_INSTALACAO = 'concluida') OR (STATUS_INSTALACAO = 'reinstalacao')) AND (STATUS_RETIRADA = 'concluida')) UNION SELECT * FROM TBCADASTRO WHERE (((STATUS_INSTALACAO = 'concluida') OR (STATUS_INSTALACAO = 'reinstalacao')) AND ((STATUS_RETIRADA = 'aberto') OR (STATUS_RETIRADA IS NULL))) ORDER BY GRUPO, POSICAO;"), conexao);
            adapter.Fill(dataSet);

            // Gera um arquivo de Log das UCs atendidas
            System.IO.Stream Maps;
            System.IO.StreamWriter ArquivoMapa;
            string DiretorioAtual;

            try
            {
                //DiretorioAtual = "C:\\MGE\\T.I\\Desenvolvimento\\WEB_MGE\\WEB_MGE - v15\\WEB_MGE\\scripts\\" + User_IP;
                DiretorioAtual = "C:\\FTP\\WEB_MGE\\scripts";
                Maps = System.IO.File.Open(@DiretorioAtual + "\\MapaAuxiliar.js", System.IO.FileMode.Create);
                ArquivoMapa = new System.IO.StreamWriter(Maps);
                ArquivoMapa.WriteLine("function initialize() {");

                for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                {
                    if (i > 0)
                    {
                        if ((dataSet.Tables[0].Rows[i][Constantes.LATITUDE_INSTALACAO].ToString() != null) && (dataSet.Tables[0].Rows[i][Constantes.LATITUDE_INSTALACAO].ToString() != ""))
                        {
                            ArquivoMapa.WriteLine("    var latlng" + i.ToString() + " = new google.maps.LatLng(" + dataSet.Tables[0].Rows[i][Constantes.LATITUDE_INSTALACAO].ToString() + ", " + dataSet.Tables[0].Rows[i][Constantes.LONGITUDE_INSTALACAO].ToString() + ");");
                        }
                        else
                        {
                            ArquivoMapa.WriteLine("    var latlng" + i.ToString() + " = new google.maps.LatLng(" + dataSet.Tables[0].Rows[i][Constantes.LATITUDE].ToString() + ", " + dataSet.Tables[0].Rows[i][Constantes.LONGITUDE].ToString() + ");");
                        }
                    }
                    else
                    {
                        if ((dataSet.Tables[0].Rows[i][Constantes.LATITUDE_INSTALACAO].ToString() != null) && (dataSet.Tables[0].Rows[i][Constantes.LATITUDE_INSTALACAO].ToString() != ""))
                        {
                            ArquivoMapa.WriteLine("    var base = new google.maps.LatLng(" + dataSet.Tables[0].Rows[i][Constantes.LATITUDE_INSTALACAO].ToString() + ", " + dataSet.Tables[0].Rows[i][Constantes.LONGITUDE_INSTALACAO].ToString() + ");");
                        }
                        else
                        {
                            ArquivoMapa.WriteLine("    var base = new google.maps.LatLng(" + dataSet.Tables[0].Rows[i][Constantes.LATITUDE].ToString() + ", " + dataSet.Tables[0].Rows[i][Constantes.LONGITUDE].ToString() + ");");
                        }
                    }
                }
                //-------------------------------------------------------------------------
                // Monta a lista de carros

                if (Variaveis_Globais.Usuario == "Admin")
                {
                    for (int i = 0; i < ListaCelulares.Items.Count; i++)
                    {
                        ArquivoMapa.WriteLine("    var latlng_cars" + i.ToString() + " = new google.maps.LatLng(" + LatitudeCelulares.Items[i].ToString() + ", " + LongitudeCelulares.Items[i].ToString() + ");");
                    }
                }
                //-------------------------------------------------------------------------
                ArquivoMapa.WriteLine("");

                dataSet.Clear();
                adapter.Fill(dataSet);

                for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        ArquivoMapa.WriteLine("    var map = new google.maps.Map(document.getElementById('map'), {");
                        ArquivoMapa.WriteLine("        zoom: 8,");
                        ArquivoMapa.WriteLine("        center: base,");
                        ArquivoMapa.WriteLine("        mapTypeId: google.maps.MapTypeId.ROADMAP");
                        ArquivoMapa.WriteLine("    });");
                        ArquivoMapa.WriteLine("");

                        ArquivoMapa.WriteLine("    var pinColorBase = " + '\u0022' + "FFFFFF" + '\u0022' + ";");
                        ArquivoMapa.WriteLine("    var pinColorAberto = " + '\u0022' + "0000FF" + '\u0022' + ";");
                        ArquivoMapa.WriteLine("    var pinColorConcluido = " + '\u0022' + "00FF00" + '\u0022' + ";");
                        ArquivoMapa.WriteLine("    var pinColorCancelado = " + '\u0022' + "FF0000" + '\u0022' + ";");
                        ArquivoMapa.WriteLine("    var pinColorTrafo = " + '\u0022' + "FFFF00" + '\u0022' + ";");
                        ArquivoMapa.WriteLine("    var pinColorRural = " + '\u0022' + "FF00F0" + '\u0022' + ";");
                        ArquivoMapa.WriteLine("    var pinColorResidencial = " + '\u0022' + "00FFFF" + '\u0022' + ";");
                        ArquivoMapa.WriteLine("    var pinColorComercial = " + '\u0022' + "FF0000" + '\u0022' + ";");
                        ArquivoMapa.WriteLine("    var pinColorIndustrial = " + '\u0022' + "00FF00" + '\u0022' + ";");
                        ArquivoMapa.WriteLine("    var pinColorPoderPublico = " + '\u0022' + "0000FF" + '\u0022' + ";");
                        ArquivoMapa.WriteLine("    var pinColorNULL = " + '\u0022' + "F00FFF" + '\u0022' + ";");
                        ArquivoMapa.WriteLine("    var pinImageCar = new google.maps.MarkerImage(" + '\u0022' + "http://icons.iconarchive.com/icons/icons-land/transporter/32/Taxi-Top-Yellow-icon.png" + '\u0022' + ",");
                        ArquivoMapa.WriteLine("        new google.maps.Size(42, 68),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(20, 68));");
                        ArquivoMapa.WriteLine("");
                        ArquivoMapa.WriteLine("    var pinImageBase = new google.maps.MarkerImage(" + '\u0022' + "http://chart.apis.google.com/chart?chst=d_map_pin_letter&chld=%E2%80%A2|" + '\u0022' + " + pinColorBase,");
                        ArquivoMapa.WriteLine("        new google.maps.Size(42, 68),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(20, 68));");
                        ArquivoMapa.WriteLine("");
                        ArquivoMapa.WriteLine("    var pinImageAberto = new google.maps.MarkerImage(" + '\u0022' + "http://chart.apis.google.com/chart?chst=d_map_pin_letter&chld=%E2%80%A2|" + '\u0022' + " + pinColorAberto,");
                        ArquivoMapa.WriteLine("        new google.maps.Size(21, 34),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(10, 34));");
                        ArquivoMapa.WriteLine("");
                        ArquivoMapa.WriteLine("    var pinImageConcluido = new google.maps.MarkerImage(" + '\u0022' + "http://chart.apis.google.com/chart?chst=d_map_pin_letter&chld=%E2%80%A2|" + '\u0022' + " + pinColorConcluido,");
                        ArquivoMapa.WriteLine("        new google.maps.Size(21, 34),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(10, 34));");
                        ArquivoMapa.WriteLine("");
                        ArquivoMapa.WriteLine("    var pinImageCancelado = new google.maps.MarkerImage(" + '\u0022' + "http://chart.apis.google.com/chart?chst=d_map_pin_letter&chld=%E2%80%A2|" + '\u0022' + " + pinColorCancelado,");
                        ArquivoMapa.WriteLine("        new google.maps.Size(21, 34),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(10, 34));");
                        ArquivoMapa.WriteLine("");
                        ArquivoMapa.WriteLine("    var pinImageNULL = new google.maps.MarkerImage(" + '\u0022' + "http://chart.apis.google.com/chart?chst=d_map_pin_letter&chld=%E2%80%A2|" + '\u0022' + " + pinColorNULL,");
                        ArquivoMapa.WriteLine("        new google.maps.Size(21, 34),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(10, 34));");
                        ArquivoMapa.WriteLine("");
                        ArquivoMapa.WriteLine("    var pinImageTrafo = new google.maps.MarkerImage(" + '\u0022' + "http://icons.iconarchive.com/icons/iconsmind/outline/16/Communication-Tower-2-icon.png" + '\u0022' + ",");
                        ArquivoMapa.WriteLine("        new google.maps.Size(21, 34),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(21, 34));");
                        ArquivoMapa.WriteLine("");
                        ArquivoMapa.WriteLine("    var pinImageTrafoDisponivel = new google.maps.MarkerImage(" + '\u0022' + "images/trafo_disponivel.png" + '\u0022' + ",");
                        ArquivoMapa.WriteLine("        new google.maps.Size(21, 34),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(21, 34));");
                        ArquivoMapa.WriteLine("");
                        ArquivoMapa.WriteLine("    var pinImageTrafoConcluido = new google.maps.MarkerImage(" + '\u0022' + "images/trafo_concluido.png" + '\u0022' + ",");
                        ArquivoMapa.WriteLine("        new google.maps.Size(21, 34),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(21, 34));");
                        ArquivoMapa.WriteLine("");
                        ArquivoMapa.WriteLine("    var pinImageTrafoInstConcluido = new google.maps.MarkerImage(" + '\u0022' + "images/trafo_inst_conc.png" + '\u0022' + ",");
                        ArquivoMapa.WriteLine("        new google.maps.Size(21, 34),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(21, 34));");
                        ArquivoMapa.WriteLine("");
                        ArquivoMapa.WriteLine("    var pinImageTrafoInstCancelado = new google.maps.MarkerImage(" + '\u0022' + "images/trafo_inst_canc.png" + '\u0022' + ",");
                        ArquivoMapa.WriteLine("        new google.maps.Size(21, 34),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(21, 34));");
                        ArquivoMapa.WriteLine("");
                        ArquivoMapa.WriteLine("    var pinImageTrafoCancelado = new google.maps.MarkerImage(" + '\u0022' + "images/trafo_cancelado.png" + '\u0022' + ",");
                        ArquivoMapa.WriteLine("        new google.maps.Size(21, 34),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(10, 34));");
                        ArquivoMapa.WriteLine("");
                        ArquivoMapa.WriteLine("    var pinImageRural = new google.maps.MarkerImage(" + '\u0022' + "http://chart.apis.google.com/chart?chst=d_map_pin_letter&chld=%E2%80%A2|" + '\u0022' + " + pinColorRural,");
                        ArquivoMapa.WriteLine("        new google.maps.Size(32, 32),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(32, 32));");
                        ArquivoMapa.WriteLine("");
                        ArquivoMapa.WriteLine("    var pinImageRuralDisponivel = new google.maps.MarkerImage(" + '\u0022' + "images/rural_disponivel.png" + '\u0022' + ",");
                        ArquivoMapa.WriteLine("        new google.maps.Size(32, 32),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(32, 32));");
                        ArquivoMapa.WriteLine("");
                        ArquivoMapa.WriteLine("    var pinImageRuralConcluido = new google.maps.MarkerImage(" + '\u0022' + "images/rural_concluido.png" + '\u0022' + ",");
                        ArquivoMapa.WriteLine("        new google.maps.Size(32, 32),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(32, 32));");
                        ArquivoMapa.WriteLine("");
                        ArquivoMapa.WriteLine("    var pinImageRuralInstConcluido = new google.maps.MarkerImage(" + '\u0022' + "images/rural_inst_conc.png" + '\u0022' + ",");
                        ArquivoMapa.WriteLine("        new google.maps.Size(32, 32),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(32, 32));");
                        ArquivoMapa.WriteLine("");
                        ArquivoMapa.WriteLine("    var pinImageRuralInstCancelado = new google.maps.MarkerImage(" + '\u0022' + "images/rural_inst_canc.png" + '\u0022' + ",");
                        ArquivoMapa.WriteLine("        new google.maps.Size(32, 32),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(32, 32));");
                        ArquivoMapa.WriteLine("");
                        ArquivoMapa.WriteLine("    var pinImageRuralCancelado = new google.maps.MarkerImage(" + '\u0022' + "images/rural_cancelado.png" + '\u0022' + ",");
                        ArquivoMapa.WriteLine("        new google.maps.Size(32, 32),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(32, 32));");
                        ArquivoMapa.WriteLine("");
                        ArquivoMapa.WriteLine("    var pinImageResidencial = new google.maps.MarkerImage(" + '\u0022' + "http://chart.apis.google.com/chart?chst=d_map_pin_letter&chld=%E2%80%A2|" + '\u0022' + " + pinColorResidencial,");
                        ArquivoMapa.WriteLine("        new google.maps.Size(32, 32),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(32, 32));");
                        ArquivoMapa.WriteLine("");
                        ArquivoMapa.WriteLine("    var pinImageResidencialDisponivel = new google.maps.MarkerImage(" + '\u0022' + "images/residencial_disponivel.png" + '\u0022' + ",");
                        ArquivoMapa.WriteLine("        new google.maps.Size(32, 32),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(32, 32));");
                        ArquivoMapa.WriteLine("");
                        ArquivoMapa.WriteLine("    var pinImageResidencialConcluido = new google.maps.MarkerImage(" + '\u0022' + "images/residencial_concluido.png" + '\u0022' + ",");
                        ArquivoMapa.WriteLine("        new google.maps.Size(32, 32),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(32, 32));");
                        ArquivoMapa.WriteLine("");
                        ArquivoMapa.WriteLine("    var pinImageResidencialInstConcluido = new google.maps.MarkerImage(" + '\u0022' + "images/residencial_inst_conc.png" + '\u0022' + ",");
                        ArquivoMapa.WriteLine("        new google.maps.Size(32, 32),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(32, 32));");
                        ArquivoMapa.WriteLine("");
                        ArquivoMapa.WriteLine("    var pinImageResidencialInstCancelado = new google.maps.MarkerImage(" + '\u0022' + "images/residencial_inst_canc.png" + '\u0022' + ",");
                        ArquivoMapa.WriteLine("        new google.maps.Size(32, 32),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(32, 32));");
                        ArquivoMapa.WriteLine("");
                        ArquivoMapa.WriteLine("    var pinImageResidencialCancelado = new google.maps.MarkerImage(" + '\u0022' + "images/residencial_cancelado.png" + '\u0022' + ",");
                        ArquivoMapa.WriteLine("        new google.maps.Size(32, 32),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(32, 32));");
                        ArquivoMapa.WriteLine("");
                        ArquivoMapa.WriteLine("    var pinImageIndustrial = new google.maps.MarkerImage(" + '\u0022' + "http://chart.apis.google.com/chart?chst=d_map_pin_letter&chld=%E2%80%A2|" + '\u0022' + " + pinColorIndustrial,");
                        ArquivoMapa.WriteLine("        new google.maps.Size(32, 32),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(32, 32));");
                        ArquivoMapa.WriteLine("");
                        ArquivoMapa.WriteLine("    var pinImageIndustrialDisponivel = new google.maps.MarkerImage(" + '\u0022' + "images/industrial_disponivel.png" + '\u0022' + ",");
                        ArquivoMapa.WriteLine("        new google.maps.Size(32, 32),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(32, 32));");
                        ArquivoMapa.WriteLine("");
                        ArquivoMapa.WriteLine("    var pinImageIndustrialConcluido = new google.maps.MarkerImage(" + '\u0022' + "images/industrial_concluido.png" + '\u0022' + ",");
                        ArquivoMapa.WriteLine("        new google.maps.Size(32, 32),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(32, 32));");
                        ArquivoMapa.WriteLine("");
                        ArquivoMapa.WriteLine("    var pinImageIndustrialInstConcluido = new google.maps.MarkerImage(" + '\u0022' + "images/industrial_inst_conc.png" + '\u0022' + ",");
                        ArquivoMapa.WriteLine("        new google.maps.Size(32, 32),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(32, 32));");
                        ArquivoMapa.WriteLine("");
                        ArquivoMapa.WriteLine("    var pinImageIndustrialInstCancelado = new google.maps.MarkerImage(" + '\u0022' + "images/industrial_inst_canc.png" + '\u0022' + ",");
                        ArquivoMapa.WriteLine("        new google.maps.Size(32, 32),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(32, 32));");
                        ArquivoMapa.WriteLine("");
                        ArquivoMapa.WriteLine("    var pinImageIndustrialCancelado = new google.maps.MarkerImage(" + '\u0022' + "images/industrial_cancelado.png" + '\u0022' + ",");
                        ArquivoMapa.WriteLine("        new google.maps.Size(32, 32),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(32, 32));");
                        ArquivoMapa.WriteLine("");
                        ArquivoMapa.WriteLine("    var pinImageComercial = new google.maps.MarkerImage(" + '\u0022' + "http://chart.apis.google.com/chart?chst=d_map_pin_letter&chld=%E2%80%A2|" + '\u0022' + " + pinColorComercial,");
                        ArquivoMapa.WriteLine("        new google.maps.Size(32, 32),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(32, 32));");
                        ArquivoMapa.WriteLine("");
                        ArquivoMapa.WriteLine("    var pinImageComercialDisponivel = new google.maps.MarkerImage(" + '\u0022' + "images/comercial_disponivel.png" + '\u0022' + ",");
                        ArquivoMapa.WriteLine("        new google.maps.Size(32, 32),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(32, 32));");
                        ArquivoMapa.WriteLine("");
                        ArquivoMapa.WriteLine("    var pinImageComercialConcluido = new google.maps.MarkerImage(" + '\u0022' + "images/comercial_concluido.png" + '\u0022' + ",");
                        ArquivoMapa.WriteLine("        new google.maps.Size(32, 32),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(32, 32));");
                        ArquivoMapa.WriteLine("");
                        ArquivoMapa.WriteLine("    var pinImageComercialInstConcluido = new google.maps.MarkerImage(" + '\u0022' + "images/comercial_inst_conc.png" + '\u0022' + ",");
                        ArquivoMapa.WriteLine("        new google.maps.Size(32, 32),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(32, 32));");
                        ArquivoMapa.WriteLine("");
                        ArquivoMapa.WriteLine("    var pinImageComercialInstCancelado = new google.maps.MarkerImage(" + '\u0022' + "images/comercial_inst_canc.png" + '\u0022' + ",");
                        ArquivoMapa.WriteLine("        new google.maps.Size(32, 32),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(32, 32));");
                        ArquivoMapa.WriteLine("");
                        ArquivoMapa.WriteLine("    var pinImageComercialCancelado = new google.maps.MarkerImage(" + '\u0022' + "images/comercial_cancelado.png" + '\u0022' + ",");
                        ArquivoMapa.WriteLine("        new google.maps.Size(32, 32),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(32, 32));");
                        ArquivoMapa.WriteLine("");
                        ArquivoMapa.WriteLine("    var pinImageServicoPublico = new google.maps.MarkerImage(" + '\u0022' + "http://chart.apis.google.com/chart?chst=d_map_pin_letter&chld=%E2%80%A2|" + '\u0022' + " + pinColorPoderPublico,");
                        ArquivoMapa.WriteLine("        new google.maps.Size(24, 24),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(24, 24));");
                        ArquivoMapa.WriteLine("");
                        ArquivoMapa.WriteLine("    var pinImageServicoPublicoDisponivel = new google.maps.MarkerImage(" + '\u0022' + "images/servico_publilco_disponivel.png" + '\u0022' + ",");
                        ArquivoMapa.WriteLine("        new google.maps.Size(24, 24),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(24, 24));");
                        ArquivoMapa.WriteLine("");
                        ArquivoMapa.WriteLine("    var pinImageServicoPublicoConcluido = new google.maps.MarkerImage(" + '\u0022' + "images/servico_publico_concluido.png" + '\u0022' + ",");
                        ArquivoMapa.WriteLine("        new google.maps.Size(24, 24),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(24, 24));");
                        ArquivoMapa.WriteLine("");
                        ArquivoMapa.WriteLine("    var pinImageServicoPublicoInstConcluido = new google.maps.MarkerImage(" + '\u0022' + "images/servico_publico_inst_conc.png" + '\u0022' + ",");
                        ArquivoMapa.WriteLine("        new google.maps.Size(24, 24),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(24, 24));");
                        ArquivoMapa.WriteLine("");
                        ArquivoMapa.WriteLine("    var pinImageServicoPublicoInstCancelado = new google.maps.MarkerImage(" + '\u0022' + "images/servico_publico_inst_canc.png" + '\u0022' + ",");
                        ArquivoMapa.WriteLine("        new google.maps.Size(24, 24),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(24, 24));");
                        ArquivoMapa.WriteLine("");
                        ArquivoMapa.WriteLine("    var pinImageServicoPublicoCancelado = new google.maps.MarkerImage(" + '\u0022' + "images/servico_publico_cancelado.png" + '\u0022' + ",");
                        ArquivoMapa.WriteLine("        new google.maps.Size(24, 24),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(24, 24));");
                        ArquivoMapa.WriteLine("");
                    }
                    if (i > 0)
                    {
                        ArquivoMapa.WriteLine("    var marker" + i.ToString() + " = new google.maps.Marker({");
                        ArquivoMapa.WriteLine("        position: latlng" + i.ToString() + ",");
                    }
                    else
                    {
                        ArquivoMapa.WriteLine("    var marker = new google.maps.Marker({");
                        ArquivoMapa.WriteLine("        position: base,");
                    }
                    ArquivoMapa.WriteLine("        map: map,");
                    ArquivoMapa.WriteLine("        title: " + '\u0022' + "UC: " + dataSet.Tables[0].Rows[i][Constantes.UC].ToString() + '\u0022' + ",");
                    if (dataSet.Tables[0].Rows[i][Constantes.CLASSE].ToString() == "Transformador")
                    {
                        if (((dataSet.Tables[0].Rows[i][Constantes.STATUS_INSTALACAO].ToString() == "concluida") || (dataSet.Tables[0].Rows[i][Constantes.STATUS_REINSTALACAO].ToString() == "concluida")) && (dataSet.Tables[0].Rows[i][Constantes.STATUS_RETIRADA].ToString() == "concluida"))
                        {
                            ArquivoMapa.WriteLine("        icon: pinImageTrafoConcluido");
                        }
                        else if (((dataSet.Tables[0].Rows[i][Constantes.STATUS_INSTALACAO].ToString() == "concluida") || (dataSet.Tables[0].Rows[i][Constantes.STATUS_REINSTALACAO].ToString() == "concluida")) && ((dataSet.Tables[0].Rows[i][Constantes.STATUS_RETIRADA].ToString() == "") || (dataSet.Tables[0].Rows[i][Constantes.STATUS_RETIRADA].ToString() == "aberto")))
                        {
                            ArquivoMapa.WriteLine("        icon: pinImageTrafoInstConcluido");
                        }
                    }
                    else if (dataSet.Tables[0].Rows[i][Constantes.CLASSE].ToString() == "Residencial")
                    {
                        if (((dataSet.Tables[0].Rows[i][Constantes.STATUS_INSTALACAO].ToString() == "concluida") || (dataSet.Tables[0].Rows[i][Constantes.STATUS_REINSTALACAO].ToString() == "concluida")) && (dataSet.Tables[0].Rows[i][Constantes.STATUS_RETIRADA].ToString() == "concluida"))
                        {
                            ArquivoMapa.WriteLine("        icon: pinImageResidencialConcluido");
                        }
                        else if (((dataSet.Tables[0].Rows[i][Constantes.STATUS_INSTALACAO].ToString() == "concluida") || (dataSet.Tables[0].Rows[i][Constantes.STATUS_REINSTALACAO].ToString() == "concluida")) && ((dataSet.Tables[0].Rows[i][Constantes.STATUS_RETIRADA].ToString() == "") || (dataSet.Tables[0].Rows[i][Constantes.STATUS_RETIRADA].ToString() == "aberto")))
                        {
                            ArquivoMapa.WriteLine("        icon: pinImageResidencialInstConcluido");
                        }
                    }
                    else if (dataSet.Tables[0].Rows[i][Constantes.CLASSE].ToString() == "Rural")
                    {
                        if (((dataSet.Tables[0].Rows[i][Constantes.STATUS_INSTALACAO].ToString() == "concluida") || (dataSet.Tables[0].Rows[i][Constantes.STATUS_REINSTALACAO].ToString() == "concluida")) && (dataSet.Tables[0].Rows[i][Constantes.STATUS_RETIRADA].ToString() == "concluida"))
                        {
                            ArquivoMapa.WriteLine("        icon: pinImageRuralConcluido");
                        }
                        else if (((dataSet.Tables[0].Rows[i][Constantes.STATUS_INSTALACAO].ToString() == "concluida") || (dataSet.Tables[0].Rows[i][Constantes.STATUS_REINSTALACAO].ToString() == "concluida")) && ((dataSet.Tables[0].Rows[i][Constantes.STATUS_RETIRADA].ToString() == "") || (dataSet.Tables[0].Rows[i][Constantes.STATUS_RETIRADA].ToString() == "aberto")))
                        {
                            ArquivoMapa.WriteLine("        icon: pinImageRuralInstConcluido");
                        }
                    }
                    else if (dataSet.Tables[0].Rows[i][Constantes.CLASSE].ToString() == "Comercial")
                    {
                        if (((dataSet.Tables[0].Rows[i][Constantes.STATUS_INSTALACAO].ToString() == "concluida") || (dataSet.Tables[0].Rows[i][Constantes.STATUS_REINSTALACAO].ToString() == "concluida")) && (dataSet.Tables[0].Rows[i][Constantes.STATUS_RETIRADA].ToString() == "concluida"))
                        {
                            ArquivoMapa.WriteLine("        icon: pinImageComercialConcluido");
                        }
                        else if (((dataSet.Tables[0].Rows[i][Constantes.STATUS_INSTALACAO].ToString() == "concluida") || (dataSet.Tables[0].Rows[i][Constantes.STATUS_REINSTALACAO].ToString() == "concluida")) && ((dataSet.Tables[0].Rows[i][Constantes.STATUS_RETIRADA].ToString() == "") || (dataSet.Tables[0].Rows[i][Constantes.STATUS_RETIRADA].ToString() == "aberto")))
                        {
                            ArquivoMapa.WriteLine("        icon: pinImageComercialInstConcluido");
                        }
                    }
                    else if (dataSet.Tables[0].Rows[i][Constantes.CLASSE].ToString() == "Servico Publico")
                    {
                        if (((dataSet.Tables[0].Rows[i][Constantes.STATUS_INSTALACAO].ToString() == "concluida") || (dataSet.Tables[0].Rows[i][Constantes.STATUS_REINSTALACAO].ToString() == "concluida")) && (dataSet.Tables[0].Rows[i][Constantes.STATUS_RETIRADA].ToString() == "concluida"))
                        {
                            ArquivoMapa.WriteLine("        icon: pinImageResidencialConcluido");
                        }
                        else if (((dataSet.Tables[0].Rows[i][Constantes.STATUS_INSTALACAO].ToString() == "concluida") || (dataSet.Tables[0].Rows[i][Constantes.STATUS_REINSTALACAO].ToString() == "concluida")) && ((dataSet.Tables[0].Rows[i][Constantes.STATUS_RETIRADA].ToString() == "") || (dataSet.Tables[0].Rows[i][Constantes.STATUS_RETIRADA].ToString() == "aberto")))
                        {
                            ArquivoMapa.WriteLine("        icon: pinImageResidencialInstConcluido");
                        }
                    }
                    else if (dataSet.Tables[0].Rows[i][Constantes.CLASSE].ToString() == "Industrial")
                    {
                        if (((dataSet.Tables[0].Rows[i][Constantes.STATUS_INSTALACAO].ToString() == "concluida") || (dataSet.Tables[0].Rows[i][Constantes.STATUS_REINSTALACAO].ToString() == "concluida")) && (dataSet.Tables[0].Rows[i][Constantes.STATUS_RETIRADA].ToString() == "concluida"))
                        {
                            ArquivoMapa.WriteLine("        icon: pinImageIndustrialConcluido");
                        }
                        else if (((dataSet.Tables[0].Rows[i][Constantes.STATUS_INSTALACAO].ToString() == "concluida") || (dataSet.Tables[0].Rows[i][Constantes.STATUS_REINSTALACAO].ToString() == "concluida")) && ((dataSet.Tables[0].Rows[i][Constantes.STATUS_RETIRADA].ToString() == "") || (dataSet.Tables[0].Rows[i][Constantes.STATUS_RETIRADA].ToString() == "aberto")))
                        {
                            ArquivoMapa.WriteLine("        icon: pinImageIndustrialInstConcluido");
                        }
                    }
                    ArquivoMapa.WriteLine("    });");

                    // Listen for click event
                    if(i > 0)
                    {
                        ArquivoMapa.WriteLine("    google.maps.event.addListener(marker" + i.ToString() + ", " + '\u0022' + "click" + '\u0022' + ", function (e) {");
                    }
                    else
                    {
                        ArquivoMapa.WriteLine("    google.maps.event.addListener(marker, " + '\u0022' + "click" + '\u0022' + ", function (e) {");
                    }
                    ArquivoMapa.WriteLine("        var infoWindow = new google.maps.InfoWindow({");
                    //ArquivoMapa.WriteLine("        content: 'UC: " + dataSet.Tables[0].Rows[i][Constantes.UC].ToString() + "' + '<br />Grupo: " + dataSet.Tables[0].Rows[i][Constantes.GRUPO].ToString() + "' + '<br />Posição: " + dataSet.Tables[0].Rows[i][Constantes.POSICAO].ToString() + "' + '<br />Subclasse: " + dataSet.Tables[0].Rows[i][Constantes.SUBCLASSE].ToString() + "' + '<br /><br />Consumidor: " + dataSet.Tables[0].Rows[i][Constantes.NOME_CONSUMIDOR].ToString() + "' + '<br />Latitude: ' + latlng" + i.ToString() + ".lat() + '<br />Longitude: ' + latlng" + i.ToString() + ".lng()");
                    ArquivoMapa.WriteLine("        content: 'UC: " + dataSet.Tables[0].Rows[i][Constantes.UC].ToString() + "' + '<br />Grupo: " + dataSet.Tables[0].Rows[i][Constantes.GRUPO].ToString() + "' + '<br />Posição: " + dataSet.Tables[0].Rows[i][Constantes.POSICAO].ToString() + "' + '<br />Subclasse: " + dataSet.Tables[0].Rows[i][Constantes.SUBCLASSE].ToString() + "' + '<br />Equipamento: " + dataSet.Tables[0].Rows[i][Constantes.TIPO_EQUIPAMENTO].ToString() + "' + '<br />Fases: " + dataSet.Tables[0].Rows[i][Constantes.FASE_CONSUMIDOR].ToString() + "' + '<br /><br />Consumidor: " + dataSet.Tables[0].Rows[i][Constantes.NOME_CONSUMIDOR].ToString() + "' + '<br />Latitude: ' + latlng" + i.ToString() + ".lat() + '<br />Longitude: ' + latlng" + i.ToString() + ".lng()");
                    ArquivoMapa.WriteLine("        });");
                    if(i > 0)
                    {
                        ArquivoMapa.WriteLine("        infoWindow.open(map, marker" + i.ToString() + ");");
                    }
                    else
                    {
                        ArquivoMapa.WriteLine("        infoWindow.open(map, marker);");
                    }
                    ArquivoMapa.WriteLine("    });");
                }
                if (Variaveis_Globais.Usuario == "Admin")
                {
                    for (int i = 0; i < ListaCelulares.Items.Count; i++)
                    {
                        ArquivoMapa.WriteLine("    var marker_cars" + i.ToString() + " = new google.maps.Marker({");
                        ArquivoMapa.WriteLine("        position: latlng_cars" + i.ToString() + ",");
                        ArquivoMapa.WriteLine("        map: map,");
                        ArquivoMapa.WriteLine("        title: " + '\u0022' + ListaCelulares.Items[i].ToString() + '\u0022' + ",");
                        ArquivoMapa.WriteLine("        icon: pinImageCar");
                        ArquivoMapa.WriteLine("    });");

                        // Listen for click event  
                        ArquivoMapa.WriteLine("    google.maps.event.addListener(marker_cars" + (i).ToString() + ", " + '\u0022' + "click" + '\u0022' + ", function (e) {");
                        ArquivoMapa.WriteLine("        var infoWindow = new google.maps.InfoWindow({");
                        ArquivoMapa.WriteLine("        content: 'Equipe: " + ListaCelulares.Items[i].ToString() + "' + '<br />DataHora: " + DataHoraCelulares.Items[i].ToString() + "' + '<br />Latitude: ' + latlng_cars" + i.ToString() + ".lat() + '<br />Longitude: ' + latlng_cars" + i.ToString() + ".lng()");
                        ArquivoMapa.WriteLine("        });");
                        ArquivoMapa.WriteLine("        infoWindow.open(map, marker_cars" + (i).ToString() + ");");
                        ArquivoMapa.WriteLine("    });");
                    }
                }
                //-------------------------------------------------------------------------------------------------------------

                ArquivoMapa.WriteLine("}");
                ArquivoMapa.WriteLine("");
                ArquivoMapa.WriteLine("function error(msg){");
                ArquivoMapa.WriteLine("    var s = document.querySelector('#status');");
                ArquivoMapa.WriteLine("    s.InnerHTML = typeof msg == 'string' ? msg : " + '\u0022' + "failed" + '\u0022' + ";");
                ArquivoMapa.WriteLine("    s.className = 'fail';");
                ArquivoMapa.WriteLine("}");
                ArquivoMapa.WriteLine("");
                ArquivoMapa.WriteLine("if (navigator.geolocation) {");
                ArquivoMapa.WriteLine("    navigator.geolocation.getCurrentPosition(success, error);");
                ArquivoMapa.WriteLine("} else {");
                ArquivoMapa.WriteLine("    error('not supported');");
                ArquivoMapa.WriteLine("}");
                ArquivoMapa.WriteLine("");
                ArquivoMapa.WriteLine("google.maps.event.addDomListener(window, 'load', initialize);");
                ArquivoMapa.WriteLine("");
                ArquivoMapa.WriteLine("initialize();");

                ArquivoMapa.Close();
                Maps.Close();
            }
            catch
            {
                // Deu erro na geração do arquivo de Serviços Atendidos
            }
        }

        protected void BtnCancelados_Click(object sender, EventArgs e)
        {
            ListaCelulares.Items.Clear();
            LatitudeCelulares.Items.Clear();
            LongitudeCelulares.Items.Clear();
            DataHoraCelulares.Items.Clear();
            conexao = new MySqlConnection("server = " + Constantes.ENDERECO_SERVIDOR + "; User ID = " + Constantes.USUARIO_DATABASE + "; database = " + Constantes.DATABASE_TB_PAINEL + "; password = " + Constantes.SENHA_DATABASE);
            adapter = new MySqlDataAdapter(string.Format("SELECT * FROM CELULARES;"), conexao);
            adapter.Fill(dataSet);
            for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
            {
                if (dataSet.Tables[0].Rows[i][Constantes.PROPRIETARIO_CELULAR].ToString() != "")
                {
                    ListaCelulares.Items.Add(dataSet.Tables[0].Rows[i][Constantes.PROPRIETARIO_CELULAR].ToString());
                }

                if (dataSet.Tables[0].Rows[i][Constantes.LATITUDE_ATUAL_CELULAR].ToString() != "")
                {
                    LatitudeCelulares.Items.Add(dataSet.Tables[0].Rows[i][Constantes.LATITUDE_ATUAL_CELULAR].ToString());
                }

                if (dataSet.Tables[0].Rows[i][Constantes.LONGITUDE_ATUAL_CELULAR].ToString() != "")
                {
                    LongitudeCelulares.Items.Add(dataSet.Tables[0].Rows[i][Constantes.LONGITUDE_ATUAL_CELULAR].ToString());
                }

                if (dataSet.Tables[0].Rows[i][Constantes.DATA_HORA_CELULAR].ToString() != "")
                {
                    DataHoraCelulares.Items.Add(dataSet.Tables[0].Rows[i][Constantes.DATA_HORA_CELULAR].ToString());
                }
            }

            adapter = null;
            dataSet.Clear();

            conexao = new MySqlConnection("server = " + Constantes.ENDERECO_SERVIDOR + "; User ID = " + Constantes.USUARIO_DATABASE + "; database = " + Variaveis_Globais.Cliente + "; password = " + Constantes.SENHA_DATABASE);
            // Pegar UCs com instalação Canceladas
            adapter = new MySqlDataAdapter(string.Format("SELECT * FROM TBCADASTRO WHERE ((STATUS_INSTALACAO = 'cancelada') OR (STATUS_REINSTALACAO = 'cancelada') OR (STATUS_RETIRADA = 'cancelada')) ORDER BY GRUPO, POSICAO;"), conexao);
            adapter.Fill(dataSet);

            // Gera um arquivo de Log das UCs atendidas
            System.IO.Stream Maps;
            System.IO.StreamWriter ArquivoMapa;
            string DiretorioAtual;

            try
            {
                //DiretorioAtual = "C:\\MGE\\T.I\\Desenvolvimento\\WEB_MGE\\WEB_MGE - v15\\WEB_MGE\\scripts\\" + User_IP;
                DiretorioAtual = "C:\\FTP\\WEB_MGE\\scripts";
                Maps = System.IO.File.Open(@DiretorioAtual + "\\MapaAuxiliar.js", System.IO.FileMode.Create);
                ArquivoMapa = new System.IO.StreamWriter(Maps);
                ArquivoMapa.WriteLine("function initialize() {");

                for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                {
                    if (i > 0)
                    {
                        if ((dataSet.Tables[0].Rows[i][Constantes.LATITUDE_INSTALACAO].ToString() != null) && (dataSet.Tables[0].Rows[i][Constantes.LATITUDE_INSTALACAO].ToString() != ""))
                        {
                            ArquivoMapa.WriteLine("    var latlng" + i.ToString() + " = new google.maps.LatLng(" + dataSet.Tables[0].Rows[i][Constantes.LATITUDE_INSTALACAO].ToString() + ", " + dataSet.Tables[0].Rows[i][Constantes.LONGITUDE_INSTALACAO].ToString() + ");");
                        }
                        else
                        {
                            ArquivoMapa.WriteLine("    var latlng" + i.ToString() + " = new google.maps.LatLng(" + dataSet.Tables[0].Rows[i][Constantes.LATITUDE].ToString() + ", " + dataSet.Tables[0].Rows[i][Constantes.LONGITUDE].ToString() + ");");
                        }
                    }
                    else
                    {
                        if ((dataSet.Tables[0].Rows[i][Constantes.LATITUDE_INSTALACAO].ToString() != null) && (dataSet.Tables[0].Rows[i][Constantes.LATITUDE_INSTALACAO].ToString() != ""))
                        {
                            ArquivoMapa.WriteLine("    var base = new google.maps.LatLng(" + dataSet.Tables[0].Rows[i][Constantes.LATITUDE_INSTALACAO].ToString() + ", " + dataSet.Tables[0].Rows[i][Constantes.LONGITUDE_INSTALACAO].ToString() + ");");
                        }
                        else
                        {
                            ArquivoMapa.WriteLine("    var base = new google.maps.LatLng(" + dataSet.Tables[0].Rows[i][Constantes.LATITUDE].ToString() + ", " + dataSet.Tables[0].Rows[i][Constantes.LONGITUDE].ToString() + ");");
                        }
                    }
                }
                //-------------------------------------------------------------------------
                // Monta a lista de carros

                if (Variaveis_Globais.Usuario == "Admin")
                {
                    for (int i = 0; i < ListaCelulares.Items.Count; i++)
                    {
                        ArquivoMapa.WriteLine("    var latlng_cars" + i.ToString() + " = new google.maps.LatLng(" + LatitudeCelulares.Items[i].ToString() + ", " + LongitudeCelulares.Items[i].ToString() + ");");
                    }
                }
                //-------------------------------------------------------------------------
                ArquivoMapa.WriteLine("");

                dataSet.Clear();
                adapter.Fill(dataSet);

                for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        ArquivoMapa.WriteLine("    var map = new google.maps.Map(document.getElementById('map'), {");
                        ArquivoMapa.WriteLine("        zoom: 8,");
                        ArquivoMapa.WriteLine("        center: base,");
                        ArquivoMapa.WriteLine("        mapTypeId: google.maps.MapTypeId.ROADMAP");
                        ArquivoMapa.WriteLine("    });");
                        ArquivoMapa.WriteLine("");

                        ArquivoMapa.WriteLine("    var pinColorBase = " + '\u0022' + "FFFFFF" + '\u0022' + ";");
                        ArquivoMapa.WriteLine("    var pinColorAberto = " + '\u0022' + "0000FF" + '\u0022' + ";");
                        ArquivoMapa.WriteLine("    var pinColorConcluido = " + '\u0022' + "00FF00" + '\u0022' + ";");
                        ArquivoMapa.WriteLine("    var pinColorCancelado = " + '\u0022' + "FF0000" + '\u0022' + ";");
                        ArquivoMapa.WriteLine("    var pinColorTrafo = " + '\u0022' + "FFFF00" + '\u0022' + ";");
                        ArquivoMapa.WriteLine("    var pinColorRural = " + '\u0022' + "FF00F0" + '\u0022' + ";");
                        ArquivoMapa.WriteLine("    var pinColorResidencial = " + '\u0022' + "00FFFF" + '\u0022' + ";");
                        ArquivoMapa.WriteLine("    var pinColorComercial = " + '\u0022' + "FF0000" + '\u0022' + ";");
                        ArquivoMapa.WriteLine("    var pinColorIndustrial = " + '\u0022' + "00FF00" + '\u0022' + ";");
                        ArquivoMapa.WriteLine("    var pinColorPoderPublico = " + '\u0022' + "0000FF" + '\u0022' + ";");
                        ArquivoMapa.WriteLine("    var pinColorNULL = " + '\u0022' + "F00FFF" + '\u0022' + ";");
                        ArquivoMapa.WriteLine("    var pinImageCar = new google.maps.MarkerImage(" + '\u0022' + "http://icons.iconarchive.com/icons/icons-land/transporter/32/Taxi-Top-Yellow-icon.png" + '\u0022' + ",");
                        ArquivoMapa.WriteLine("        new google.maps.Size(42, 68),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(20, 68));");
                        ArquivoMapa.WriteLine("");
                        ArquivoMapa.WriteLine("    var pinImageBase = new google.maps.MarkerImage(" + '\u0022' + "http://chart.apis.google.com/chart?chst=d_map_pin_letter&chld=%E2%80%A2|" + '\u0022' + " + pinColorBase,");
                        ArquivoMapa.WriteLine("        new google.maps.Size(42, 68),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(20, 68));");
                        ArquivoMapa.WriteLine("");
                        ArquivoMapa.WriteLine("    var pinImageAberto = new google.maps.MarkerImage(" + '\u0022' + "http://chart.apis.google.com/chart?chst=d_map_pin_letter&chld=%E2%80%A2|" + '\u0022' + " + pinColorAberto,");
                        ArquivoMapa.WriteLine("        new google.maps.Size(21, 34),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(10, 34));");
                        ArquivoMapa.WriteLine("");
                        ArquivoMapa.WriteLine("    var pinImageConcluido = new google.maps.MarkerImage(" + '\u0022' + "http://chart.apis.google.com/chart?chst=d_map_pin_letter&chld=%E2%80%A2|" + '\u0022' + " + pinColorConcluido,");
                        ArquivoMapa.WriteLine("        new google.maps.Size(21, 34),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(10, 34));");
                        ArquivoMapa.WriteLine("");
                        ArquivoMapa.WriteLine("    var pinImageCancelado = new google.maps.MarkerImage(" + '\u0022' + "http://chart.apis.google.com/chart?chst=d_map_pin_letter&chld=%E2%80%A2|" + '\u0022' + " + pinColorCancelado,");
                        ArquivoMapa.WriteLine("        new google.maps.Size(21, 34),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(10, 34));");
                        ArquivoMapa.WriteLine("");
                        ArquivoMapa.WriteLine("    var pinImageNULL = new google.maps.MarkerImage(" + '\u0022' + "http://chart.apis.google.com/chart?chst=d_map_pin_letter&chld=%E2%80%A2|" + '\u0022' + " + pinColorNULL,");
                        ArquivoMapa.WriteLine("        new google.maps.Size(21, 34),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(10, 34));");
                        ArquivoMapa.WriteLine("");
                        ArquivoMapa.WriteLine("    var pinImageTrafo = new google.maps.MarkerImage(" + '\u0022' + "http://icons.iconarchive.com/icons/iconsmind/outline/16/Communication-Tower-2-icon.png" + '\u0022' + ",");
                        ArquivoMapa.WriteLine("        new google.maps.Size(21, 34),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(21, 34));");
                        ArquivoMapa.WriteLine("");
                        ArquivoMapa.WriteLine("    var pinImageTrafoDisponivel = new google.maps.MarkerImage(" + '\u0022' + "images/trafo_disponivel.png" + '\u0022' + ",");
                        ArquivoMapa.WriteLine("        new google.maps.Size(21, 34),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(21, 34));");
                        ArquivoMapa.WriteLine("");
                        ArquivoMapa.WriteLine("    var pinImageTrafoConcluido = new google.maps.MarkerImage(" + '\u0022' + "images/trafo_concluido.png" + '\u0022' + ",");
                        ArquivoMapa.WriteLine("        new google.maps.Size(21, 34),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(21, 34));");
                        ArquivoMapa.WriteLine("");
                        ArquivoMapa.WriteLine("    var pinImageTrafoInstConcluido = new google.maps.MarkerImage(" + '\u0022' + "images/trafo_inst_conc.png" + '\u0022' + ",");
                        ArquivoMapa.WriteLine("        new google.maps.Size(21, 34),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(21, 34));");
                        ArquivoMapa.WriteLine("");
                        ArquivoMapa.WriteLine("    var pinImageTrafoInstCancelado = new google.maps.MarkerImage(" + '\u0022' + "images/trafo_inst_canc.png" + '\u0022' + ",");
                        ArquivoMapa.WriteLine("        new google.maps.Size(21, 34),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(21, 34));");
                        ArquivoMapa.WriteLine("");
                        ArquivoMapa.WriteLine("    var pinImageTrafoCancelado = new google.maps.MarkerImage(" + '\u0022' + "images/trafo_cancelado.png" + '\u0022' + ",");
                        ArquivoMapa.WriteLine("        new google.maps.Size(21, 34),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(10, 34));");
                        ArquivoMapa.WriteLine("");
                        ArquivoMapa.WriteLine("    var pinImageRural = new google.maps.MarkerImage(" + '\u0022' + "http://chart.apis.google.com/chart?chst=d_map_pin_letter&chld=%E2%80%A2|" + '\u0022' + " + pinColorRural,");
                        ArquivoMapa.WriteLine("        new google.maps.Size(32, 32),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(32, 32));");
                        ArquivoMapa.WriteLine("");
                        ArquivoMapa.WriteLine("    var pinImageRuralDisponivel = new google.maps.MarkerImage(" + '\u0022' + "images/rural_disponivel.png" + '\u0022' + ",");
                        ArquivoMapa.WriteLine("        new google.maps.Size(32, 32),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(32, 32));");
                        ArquivoMapa.WriteLine("");
                        ArquivoMapa.WriteLine("    var pinImageRuralConcluido = new google.maps.MarkerImage(" + '\u0022' + "images/rural_concluido.png" + '\u0022' + ",");
                        ArquivoMapa.WriteLine("        new google.maps.Size(32, 32),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(32, 32));");
                        ArquivoMapa.WriteLine("");
                        ArquivoMapa.WriteLine("    var pinImageRuralInstConcluido = new google.maps.MarkerImage(" + '\u0022' + "images/rural_inst_conc.png" + '\u0022' + ",");
                        ArquivoMapa.WriteLine("        new google.maps.Size(32, 32),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(32, 32));");
                        ArquivoMapa.WriteLine("");
                        ArquivoMapa.WriteLine("    var pinImageRuralInstCancelado = new google.maps.MarkerImage(" + '\u0022' + "images/rural_inst_canc.png" + '\u0022' + ",");
                        ArquivoMapa.WriteLine("        new google.maps.Size(32, 32),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(32, 32));");
                        ArquivoMapa.WriteLine("");
                        ArquivoMapa.WriteLine("    var pinImageRuralCancelado = new google.maps.MarkerImage(" + '\u0022' + "images/rural_cancelado.png" + '\u0022' + ",");
                        ArquivoMapa.WriteLine("        new google.maps.Size(32, 32),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(32, 32));");
                        ArquivoMapa.WriteLine("");
                        ArquivoMapa.WriteLine("    var pinImageResidencial = new google.maps.MarkerImage(" + '\u0022' + "http://chart.apis.google.com/chart?chst=d_map_pin_letter&chld=%E2%80%A2|" + '\u0022' + " + pinColorResidencial,");
                        ArquivoMapa.WriteLine("        new google.maps.Size(32, 32),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(32, 32));");
                        ArquivoMapa.WriteLine("");
                        ArquivoMapa.WriteLine("    var pinImageResidencialDisponivel = new google.maps.MarkerImage(" + '\u0022' + "images/residencial_disponivel.png" + '\u0022' + ",");
                        ArquivoMapa.WriteLine("        new google.maps.Size(32, 32),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(32, 32));");
                        ArquivoMapa.WriteLine("");
                        ArquivoMapa.WriteLine("    var pinImageResidencialConcluido = new google.maps.MarkerImage(" + '\u0022' + "images/residencial_concluido.png" + '\u0022' + ",");
                        ArquivoMapa.WriteLine("        new google.maps.Size(32, 32),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(32, 32));");
                        ArquivoMapa.WriteLine("");
                        ArquivoMapa.WriteLine("    var pinImageResidencialInstConcluido = new google.maps.MarkerImage(" + '\u0022' + "images/residencial_inst_conc.png" + '\u0022' + ",");
                        ArquivoMapa.WriteLine("        new google.maps.Size(32, 32),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(32, 32));");
                        ArquivoMapa.WriteLine("");
                        ArquivoMapa.WriteLine("    var pinImageResidencialInstCancelado = new google.maps.MarkerImage(" + '\u0022' + "images/residencial_inst_canc.png" + '\u0022' + ",");
                        ArquivoMapa.WriteLine("        new google.maps.Size(32, 32),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(32, 32));");
                        ArquivoMapa.WriteLine("");
                        ArquivoMapa.WriteLine("    var pinImageResidencialCancelado = new google.maps.MarkerImage(" + '\u0022' + "images/residencial_cancelado.png" + '\u0022' + ",");
                        ArquivoMapa.WriteLine("        new google.maps.Size(32, 32),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(32, 32));");
                        ArquivoMapa.WriteLine("");
                        ArquivoMapa.WriteLine("    var pinImageIndustrial = new google.maps.MarkerImage(" + '\u0022' + "http://chart.apis.google.com/chart?chst=d_map_pin_letter&chld=%E2%80%A2|" + '\u0022' + " + pinColorIndustrial,");
                        ArquivoMapa.WriteLine("        new google.maps.Size(32, 32),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(32, 32));");
                        ArquivoMapa.WriteLine("");
                        ArquivoMapa.WriteLine("    var pinImageIndustrialDisponivel = new google.maps.MarkerImage(" + '\u0022' + "images/industrial_disponivel.png" + '\u0022' + ",");
                        ArquivoMapa.WriteLine("        new google.maps.Size(32, 32),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(32, 32));");
                        ArquivoMapa.WriteLine("");
                        ArquivoMapa.WriteLine("    var pinImageIndustrialConcluido = new google.maps.MarkerImage(" + '\u0022' + "images/industrial_concluido.png" + '\u0022' + ",");
                        ArquivoMapa.WriteLine("        new google.maps.Size(32, 32),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(32, 32));");
                        ArquivoMapa.WriteLine("");
                        ArquivoMapa.WriteLine("    var pinImageIndustrialInstConcluido = new google.maps.MarkerImage(" + '\u0022' + "images/industrial_inst_conc.png" + '\u0022' + ",");
                        ArquivoMapa.WriteLine("        new google.maps.Size(32, 32),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(32, 32));");
                        ArquivoMapa.WriteLine("");
                        ArquivoMapa.WriteLine("    var pinImageIndustrialInstCancelado = new google.maps.MarkerImage(" + '\u0022' + "images/industrial_inst_canc.png" + '\u0022' + ",");
                        ArquivoMapa.WriteLine("        new google.maps.Size(32, 32),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(32, 32));");
                        ArquivoMapa.WriteLine("");
                        ArquivoMapa.WriteLine("    var pinImageIndustrialCancelado = new google.maps.MarkerImage(" + '\u0022' + "images/industrial_cancelado.png" + '\u0022' + ",");
                        ArquivoMapa.WriteLine("        new google.maps.Size(32, 32),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(32, 32));");
                        ArquivoMapa.WriteLine("");
                        ArquivoMapa.WriteLine("    var pinImageComercial = new google.maps.MarkerImage(" + '\u0022' + "http://chart.apis.google.com/chart?chst=d_map_pin_letter&chld=%E2%80%A2|" + '\u0022' + " + pinColorComercial,");
                        ArquivoMapa.WriteLine("        new google.maps.Size(32, 32),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(32, 32));");
                        ArquivoMapa.WriteLine("");
                        ArquivoMapa.WriteLine("    var pinImageComercialDisponivel = new google.maps.MarkerImage(" + '\u0022' + "images/comercial_disponivel.png" + '\u0022' + ",");
                        ArquivoMapa.WriteLine("        new google.maps.Size(32, 32),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(32, 32));");
                        ArquivoMapa.WriteLine("");
                        ArquivoMapa.WriteLine("    var pinImageComercialConcluido = new google.maps.MarkerImage(" + '\u0022' + "images/comercial_concluido.png" + '\u0022' + ",");
                        ArquivoMapa.WriteLine("        new google.maps.Size(32, 32),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(32, 32));");
                        ArquivoMapa.WriteLine("");
                        ArquivoMapa.WriteLine("    var pinImageComercialInstConcluido = new google.maps.MarkerImage(" + '\u0022' + "images/comercial_inst_conc.png" + '\u0022' + ",");
                        ArquivoMapa.WriteLine("        new google.maps.Size(32, 32),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(32, 32));");
                        ArquivoMapa.WriteLine("");
                        ArquivoMapa.WriteLine("    var pinImageComercialInstCancelado = new google.maps.MarkerImage(" + '\u0022' + "images/comercial_inst_canc.png" + '\u0022' + ",");
                        ArquivoMapa.WriteLine("        new google.maps.Size(32, 32),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(32, 32));");
                        ArquivoMapa.WriteLine("");
                        ArquivoMapa.WriteLine("    var pinImageComercialCancelado = new google.maps.MarkerImage(" + '\u0022' + "images/comercial_cancelado.png" + '\u0022' + ",");
                        ArquivoMapa.WriteLine("        new google.maps.Size(32, 32),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(32, 32));");
                        ArquivoMapa.WriteLine("");
                        ArquivoMapa.WriteLine("    var pinImageServicoPublico = new google.maps.MarkerImage(" + '\u0022' + "http://chart.apis.google.com/chart?chst=d_map_pin_letter&chld=%E2%80%A2|" + '\u0022' + " + pinColorPoderPublico,");
                        ArquivoMapa.WriteLine("        new google.maps.Size(24, 24),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(24, 24));");
                        ArquivoMapa.WriteLine("");
                        ArquivoMapa.WriteLine("    var pinImageServicoPublicoDisponivel = new google.maps.MarkerImage(" + '\u0022' + "images/servico_publilco_disponivel.png" + '\u0022' + ",");
                        ArquivoMapa.WriteLine("        new google.maps.Size(24, 24),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(24, 24));");
                        ArquivoMapa.WriteLine("");
                        ArquivoMapa.WriteLine("    var pinImageServicoPublicoConcluido = new google.maps.MarkerImage(" + '\u0022' + "images/servico_publico_concluido.png" + '\u0022' + ",");
                        ArquivoMapa.WriteLine("        new google.maps.Size(24, 24),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(24, 24));");
                        ArquivoMapa.WriteLine("");
                        ArquivoMapa.WriteLine("    var pinImageServicoPublicoInstConcluido = new google.maps.MarkerImage(" + '\u0022' + "images/servico_publico_inst_conc.png" + '\u0022' + ",");
                        ArquivoMapa.WriteLine("        new google.maps.Size(24, 24),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(24, 24));");
                        ArquivoMapa.WriteLine("");
                        ArquivoMapa.WriteLine("    var pinImageServicoPublicoInstCancelado = new google.maps.MarkerImage(" + '\u0022' + "images/servico_publico_inst_canc.png" + '\u0022' + ",");
                        ArquivoMapa.WriteLine("        new google.maps.Size(24, 24),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(24, 24));");
                        ArquivoMapa.WriteLine("");
                        ArquivoMapa.WriteLine("    var pinImageServicoPublicoCancelado = new google.maps.MarkerImage(" + '\u0022' + "images/servico_publico_cancelado.png" + '\u0022' + ",");
                        ArquivoMapa.WriteLine("        new google.maps.Size(24, 24),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(24, 24));");
                        ArquivoMapa.WriteLine("");
                    }
                    if (i > 0)
                    {
                        ArquivoMapa.WriteLine("    var marker" + i.ToString() + " = new google.maps.Marker({");
                        ArquivoMapa.WriteLine("        position: latlng" + i.ToString() + ",");
                    }
                    else
                    {
                        ArquivoMapa.WriteLine("    var marker = new google.maps.Marker({");
                        ArquivoMapa.WriteLine("        position: base,");
                    }
                    ArquivoMapa.WriteLine("        map: map,");
                    ArquivoMapa.WriteLine("        title: " + '\u0022' + "UC: " + dataSet.Tables[0].Rows[i][Constantes.UC].ToString() + '\u0022' + ",");
                    if (dataSet.Tables[0].Rows[i][Constantes.CLASSE].ToString() == "Transformador")
                    {
                        if((dataSet.Tables[0].Rows[i][Constantes.STATUS_INSTALACAO].ToString() == "cancelada") || (dataSet.Tables[0].Rows[i][Constantes.STATUS_REINSTALACAO].ToString() == "cancelada"))
                        {
                            ArquivoMapa.WriteLine("        icon: pinImageTrafoInstCancelado");
                        }
                        else if (dataSet.Tables[0].Rows[i][Constantes.STATUS_RETIRADA].ToString() == "cancelada")
                        {
                            ArquivoMapa.WriteLine("        icon: pinImageTrafoCancelado");
                        }
                    }
                    else if (dataSet.Tables[0].Rows[i][Constantes.CLASSE].ToString() == "Residencial")
                    {
                        if ((dataSet.Tables[0].Rows[i][Constantes.STATUS_INSTALACAO].ToString() == "cancelada") || (dataSet.Tables[0].Rows[i][Constantes.STATUS_REINSTALACAO].ToString() == "cancelada"))
                        {
                            ArquivoMapa.WriteLine("        icon: pinImageResidencialInstCancelado");
                        }
                        else if (dataSet.Tables[0].Rows[i][Constantes.STATUS_RETIRADA].ToString() == "cancelada")
                        {
                            ArquivoMapa.WriteLine("        icon: pinImageResidencialCancelado");
                        }
                    }
                    else if (dataSet.Tables[0].Rows[i][Constantes.CLASSE].ToString() == "Rural")
                    {
                        if ((dataSet.Tables[0].Rows[i][Constantes.STATUS_INSTALACAO].ToString() == "cancelada") || (dataSet.Tables[0].Rows[i][Constantes.STATUS_REINSTALACAO].ToString() == "cancelada"))
                        {
                            ArquivoMapa.WriteLine("        icon: pinImageRuralInstCancelado");
                        }
                        else if (dataSet.Tables[0].Rows[i][Constantes.STATUS_RETIRADA].ToString() == "cancelada")
                        {
                            ArquivoMapa.WriteLine("        icon: pinImageRuralCancelado");
                        }
                    }
                    else if (dataSet.Tables[0].Rows[i][Constantes.CLASSE].ToString() == "Comercial")
                    {
                        if ((dataSet.Tables[0].Rows[i][Constantes.STATUS_INSTALACAO].ToString() == "cancelada") || (dataSet.Tables[0].Rows[i][Constantes.STATUS_REINSTALACAO].ToString() == "cancelada"))
                        {
                            ArquivoMapa.WriteLine("        icon: pinImageComercialInstCancelado");
                        }
                        else if (dataSet.Tables[0].Rows[i][Constantes.STATUS_RETIRADA].ToString() == "cancelada")
                        {
                            ArquivoMapa.WriteLine("        icon: pinImageComercialCancelado");
                        }
                    }
                    else if (dataSet.Tables[0].Rows[i][Constantes.CLASSE].ToString() == "Servico Publico")
                    {
                        if ((dataSet.Tables[0].Rows[i][Constantes.STATUS_INSTALACAO].ToString() == "cancelada") || (dataSet.Tables[0].Rows[i][Constantes.STATUS_REINSTALACAO].ToString() == "cancelada"))
                        {
                            ArquivoMapa.WriteLine("        icon: pinImageServicoPublicoInstCancelado");
                        }
                        else if (dataSet.Tables[0].Rows[i][Constantes.STATUS_RETIRADA].ToString() == "cancelada")
                        {
                            ArquivoMapa.WriteLine("        icon: pinImageServicoPublicoCancelado");
                        }
                    }
                    else if (dataSet.Tables[0].Rows[i][Constantes.CLASSE].ToString() == "Industrial")
                    {
                        if ((dataSet.Tables[0].Rows[i][Constantes.STATUS_INSTALACAO].ToString() == "cancelada") || (dataSet.Tables[0].Rows[i][Constantes.STATUS_REINSTALACAO].ToString() == "cancelada"))
                        {
                            ArquivoMapa.WriteLine("        icon: pinImageIndustrialInstCancelado");
                        }
                        else if (dataSet.Tables[0].Rows[i][Constantes.STATUS_RETIRADA].ToString() == "cancelada")
                        {
                            ArquivoMapa.WriteLine("        icon: pinImageIndustrialCancelado");
                        }
                    }
                    ArquivoMapa.WriteLine("    });");

                    // Listen for click event
                    if(i > 0)
                    {
                        ArquivoMapa.WriteLine("    google.maps.event.addListener(marker" + i.ToString() + ", " + '\u0022' + "click" + '\u0022' + ", function (e) {");
                    }
                    else
                    {
                        ArquivoMapa.WriteLine("    google.maps.event.addListener(marker, " + '\u0022' + "click" + '\u0022' + ", function (e) {");
                    }
                    ArquivoMapa.WriteLine("        var infoWindow = new google.maps.InfoWindow({");
                    //ArquivoMapa.WriteLine("        content: 'UC: " + dataSet.Tables[0].Rows[i][Constantes.UC].ToString() + "' + '<br />Grupo: " + dataSet.Tables[0].Rows[i][Constantes.GRUPO].ToString() + "' + '<br />Posição: " + dataSet.Tables[0].Rows[i][Constantes.POSICAO].ToString() + "' + '<br />Subclasse: " + dataSet.Tables[0].Rows[i][Constantes.SUBCLASSE].ToString() + "' + '<br /><br />Consumidor: " + dataSet.Tables[0].Rows[i][Constantes.NOME_CONSUMIDOR].ToString() + "' + '<br />Latitude: ' + latlng" + i.ToString() + ".lat() + '<br />Longitude: ' + latlng" + i.ToString() + ".lng()");
                    ArquivoMapa.WriteLine("        content: 'UC: " + dataSet.Tables[0].Rows[i][Constantes.UC].ToString() + "' + '<br />Grupo: " + dataSet.Tables[0].Rows[i][Constantes.GRUPO].ToString() + "' + '<br />Posição: " + dataSet.Tables[0].Rows[i][Constantes.POSICAO].ToString() + "' + '<br />Subclasse: " + dataSet.Tables[0].Rows[i][Constantes.SUBCLASSE].ToString() + "' + '<br />Equipamento: " + dataSet.Tables[0].Rows[i][Constantes.TIPO_EQUIPAMENTO].ToString() + "' + '<br />Fases: " + dataSet.Tables[0].Rows[i][Constantes.FASE_CONSUMIDOR].ToString() + "' + '<br /><br />Consumidor: " + dataSet.Tables[0].Rows[i][Constantes.NOME_CONSUMIDOR].ToString() + "' + '<br />Latitude: ' + latlng" + i.ToString() + ".lat() + '<br />Longitude: ' + latlng" + i.ToString() + ".lng()");
                    ArquivoMapa.WriteLine("        });");
                    if(i > 0)
                    {
                        ArquivoMapa.WriteLine("        infoWindow.open(map, marker" + i.ToString() + ");");
                    }
                    else
                    {
                        ArquivoMapa.WriteLine("        infoWindow.open(map, marker);");
                    }
                    ArquivoMapa.WriteLine("    });");
                }
                if (Variaveis_Globais.Usuario == "Admin")
                {
                    for (int i = 0; i < ListaCelulares.Items.Count; i++)
                    {
                        ArquivoMapa.WriteLine("    var marker_cars" + i.ToString() + " = new google.maps.Marker({");
                        ArquivoMapa.WriteLine("        position: latlng_cars" + i.ToString() + ",");
                        ArquivoMapa.WriteLine("        map: map,");
                        ArquivoMapa.WriteLine("        title: " + '\u0022' + ListaCelulares.Items[i].ToString() + '\u0022' + ",");
                        ArquivoMapa.WriteLine("        icon: pinImageCar");
                        ArquivoMapa.WriteLine("    });");

                        // Listen for click event  
                        ArquivoMapa.WriteLine("    google.maps.event.addListener(marker_cars" + (i).ToString() + ", " + '\u0022' + "click" + '\u0022' + ", function (e) {");
                        ArquivoMapa.WriteLine("        var infoWindow = new google.maps.InfoWindow({");
                        ArquivoMapa.WriteLine("        content: 'Equipe: " + ListaCelulares.Items[i].ToString() + "' + '<br />DataHora: " + DataHoraCelulares.Items[i].ToString() + "' + '<br />Latitude: ' + latlng_cars" + i.ToString() + ".lat() + '<br />Longitude: ' + latlng_cars" + i.ToString() + ".lng()");
                        ArquivoMapa.WriteLine("        });");
                        ArquivoMapa.WriteLine("        infoWindow.open(map, marker_cars" + (i).ToString() + ");");
                        ArquivoMapa.WriteLine("    });");
                    }
                }
                //-------------------------------------------------------------------------------------------------------------
                ArquivoMapa.WriteLine("}");
                ArquivoMapa.WriteLine("");
                ArquivoMapa.WriteLine("function error(msg){");
                ArquivoMapa.WriteLine("    var s = document.querySelector('#status');");
                ArquivoMapa.WriteLine("    s.InnerHTML = typeof msg == 'string' ? msg : " + '\u0022' + "failed" + '\u0022' + ";");
                ArquivoMapa.WriteLine("    s.className = 'fail';");
                ArquivoMapa.WriteLine("}");
                ArquivoMapa.WriteLine("");
                ArquivoMapa.WriteLine("if (navigator.geolocation) {");
                ArquivoMapa.WriteLine("    navigator.geolocation.getCurrentPosition(success, error);");
                ArquivoMapa.WriteLine("} else {");
                ArquivoMapa.WriteLine("    error('not supported');");
                ArquivoMapa.WriteLine("}");
                ArquivoMapa.WriteLine("");
                ArquivoMapa.WriteLine("google.maps.event.addDomListener(window, 'load', initialize);");
                ArquivoMapa.WriteLine("");
                ArquivoMapa.WriteLine("initialize();");

                ArquivoMapa.Close();
                Maps.Close();
            }
            catch
            {
                // Deu erro na geração do arquivo de Serviços Atendidos
            }
        }

        protected void BtnAbertos_Click(object sender, EventArgs e)
        {
            // Gera um arquivo de Log das UCs atendidas
            System.IO.Stream Maps;
            System.IO.StreamWriter ArquivoMapa;
            string DiretorioAtual;

            try
            {
                //DiretorioAtual = "C:\\MGE\\T.I\\Desenvolvimento\\WEB_MGE\\WEB_MGE - v14\\WEB_MGE\\scripts\\" + User_IP;
                DiretorioAtual = "C:\\FTP\\WEB_MGE\\scripts";
                Maps = System.IO.File.Open(@DiretorioAtual + "\\MapaAuxiliar.js", System.IO.FileMode.Create);
                ArquivoMapa = new System.IO.StreamWriter(Maps);
                ArquivoMapa.WriteLine("function initialize() {");

                // Seleciona do banco de dados todos os grupos concluidos e monta uma lista
                //-------------------------------------------------------------------------
                ListaCelulares.Items.Clear();
                LatitudeCelulares.Items.Clear();
                LongitudeCelulares.Items.Clear();
                DataHoraCelulares.Items.Clear();
                conexao = new MySqlConnection("server = " + Constantes.ENDERECO_SERVIDOR + "; User ID = " + Constantes.USUARIO_DATABASE + "; database = " + Constantes.DATABASE_TB_PAINEL + "; password = " + Constantes.SENHA_DATABASE);
                adapter = new MySqlDataAdapter(string.Format("SELECT * FROM CELULARES;"), conexao);
                adapter.Fill(dataSet);
                for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                {
                    if (dataSet.Tables[0].Rows[i][Constantes.PROPRIETARIO_CELULAR].ToString() != "")
                    {
                        ListaCelulares.Items.Add(dataSet.Tables[0].Rows[i][Constantes.PROPRIETARIO_CELULAR].ToString());
                    }

                    if (dataSet.Tables[0].Rows[i][Constantes.LATITUDE_ATUAL_CELULAR].ToString() != "")
                    {
                        LatitudeCelulares.Items.Add(dataSet.Tables[0].Rows[i][Constantes.LATITUDE_ATUAL_CELULAR].ToString());
                    }

                    if (dataSet.Tables[0].Rows[i][Constantes.LONGITUDE_ATUAL_CELULAR].ToString() != "")
                    {
                        LongitudeCelulares.Items.Add(dataSet.Tables[0].Rows[i][Constantes.LONGITUDE_ATUAL_CELULAR].ToString());
                    }

                    if (dataSet.Tables[0].Rows[i][Constantes.DATA_HORA_CELULAR].ToString() != "")
                    {
                        DataHoraCelulares.Items.Add(dataSet.Tables[0].Rows[i][Constantes.DATA_HORA_CELULAR].ToString());
                    }
                }

                adapter = null;
                dataSet.Clear();

                conexao = new MySqlConnection("server = " + Constantes.ENDERECO_SERVIDOR + "; User ID = " + Constantes.USUARIO_DATABASE + "; database = " + Variaveis_Globais.Cliente + "; password = " + Constantes.SENHA_DATABASE);
                adapter = new MySqlDataAdapter(string.Format("SELECT distinct GRUPO FROM TBCADASTRO WHERE (STATUS_INSTALACAO = 'concluida' OR STATUS_INSTALACAO = 'reinstalacao') ORDER BY GRUPO, POSICAO;"), conexao);
                adapter.Fill(dataSet);

                List<string> lista_grupos_concluidos = new List<string>();

                for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                {
                    lista_grupos_concluidos.Add(dataSet.Tables[0].Rows[i][Constantes.GRUPO].ToString());
                }
                //-------------------------------------------------------------------------

                // Seleciona do banco de dados todos os grupos que são posicao 1 e não tem instalacao
                //-----------------------------------------------------------------------------------
                adapter = null;
                dataSet.Clear();

                adapter = new MySqlDataAdapter(string.Format("SELECT * FROM TBCADASTRO WHERE (POSICAO = '1' AND (STATUS_INSTALACAO IS NULL OR STATUS_INSTALACAO = 'aberto')) ORDER BY GRUPO, POSICAO;"), conexao);
                adapter.Fill(dataSet);
                for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                {
                    // Se nesta selecao o grupo já tiver uc concluida, passa pra proxima
                    if (lista_grupos_concluidos.Any(dataSet.Tables[0].Rows[i][Constantes.GRUPO].ToString().Equals))
                    {
                        continue;
                    }

                    if (i > 0)
                    {
                        if ((dataSet.Tables[0].Rows[i][Constantes.LATITUDE_INSTALACAO].ToString() != null) && (dataSet.Tables[0].Rows[i][Constantes.LATITUDE_INSTALACAO].ToString() != ""))
                        {
                            ArquivoMapa.WriteLine("    var latlng" + i.ToString() + " = new google.maps.LatLng(" + dataSet.Tables[0].Rows[i][Constantes.LATITUDE_INSTALACAO].ToString() + ", " + dataSet.Tables[0].Rows[i][Constantes.LONGITUDE_INSTALACAO].ToString() + ");");
                        }
                        else
                        {
                            ArquivoMapa.WriteLine("    var latlng" + i.ToString() + " = new google.maps.LatLng(" + dataSet.Tables[0].Rows[i][Constantes.LATITUDE].ToString() + ", " + dataSet.Tables[0].Rows[i][Constantes.LONGITUDE].ToString() + ");");
                        }
                    }
                    else
                    {
                        if ((dataSet.Tables[0].Rows[i][Constantes.LATITUDE_INSTALACAO].ToString() != null) && (dataSet.Tables[0].Rows[i][Constantes.LATITUDE_INSTALACAO].ToString() != ""))
                        {
                            ArquivoMapa.WriteLine("    var base = new google.maps.LatLng(" + dataSet.Tables[0].Rows[i][Constantes.LATITUDE_INSTALACAO].ToString() + ", " + dataSet.Tables[0].Rows[i][Constantes.LONGITUDE_INSTALACAO].ToString() + ");");
                        }
                        else
                        {
                            ArquivoMapa.WriteLine("    var base = new google.maps.LatLng(" + dataSet.Tables[0].Rows[i][Constantes.LATITUDE].ToString() + ", " + dataSet.Tables[0].Rows[i][Constantes.LONGITUDE].ToString() + ");");
                        }
                    }

                    //ArquivoMapa.WriteLine("    var latlng" + i.ToString() + " = new google.maps.LatLng(" + dataSet.Tables[0].Rows[i][Constantes.LATITUDE].ToString() + ", " + dataSet.Tables[0].Rows[i][Constantes.LONGITUDE].ToString() + ");");
                }
                //-------------------------------------------------------------------------
                // Monta a lista de carros

                if (Variaveis_Globais.Usuario == "Admin")
                {
                    for (int i = 0; i < ListaCelulares.Items.Count; i++)
                    {
                        ArquivoMapa.WriteLine("    var latlng_cars" + i.ToString() + " = new google.maps.LatLng(" + LatitudeCelulares.Items[i].ToString() + ", " + LongitudeCelulares.Items[i].ToString() + ");");
                    }
                }
                //-------------------------------------------------------------------------
                ArquivoMapa.WriteLine("");

                ArquivoMapa.WriteLine("    var map = new google.maps.Map(document.getElementById('map'), {");
                ArquivoMapa.WriteLine("        zoom: 7,");
                ArquivoMapa.WriteLine("        center: base,");
                ArquivoMapa.WriteLine("        mapTypeId: google.maps.MapTypeId.ROADMAP");
                ArquivoMapa.WriteLine("    });");
                ArquivoMapa.WriteLine("");

                ArquivoMapa.WriteLine("    var pinColorBase = " + '\u0022' + "FFFFFF" + '\u0022' + ";");
                ArquivoMapa.WriteLine("    var pinColorAberto = " + '\u0022' + "0000FF" + '\u0022' + ";");
                ArquivoMapa.WriteLine("    var pinColorConcluido = " + '\u0022' + "00FF00" + '\u0022' + ";");
                ArquivoMapa.WriteLine("    var pinColorCancelado = " + '\u0022' + "FF0000" + '\u0022' + ";");
                ArquivoMapa.WriteLine("    var pinColorTrafo = " + '\u0022' + "FFFF00" + '\u0022' + ";");
                ArquivoMapa.WriteLine("    var pinColorRural = " + '\u0022' + "FF00F0" + '\u0022' + ";");
                ArquivoMapa.WriteLine("    var pinColorResidencial = " + '\u0022' + "00FFFF" + '\u0022' + ";");
                ArquivoMapa.WriteLine("    var pinColorComercial = " + '\u0022' + "FF0000" + '\u0022' + ";");
                ArquivoMapa.WriteLine("    var pinColorIndustrial = " + '\u0022' + "00FF00" + '\u0022' + ";");
                ArquivoMapa.WriteLine("    var pinColorPoderPublico = " + '\u0022' + "0000FF" + '\u0022' + ";");
                ArquivoMapa.WriteLine("    var pinColorNULL = " + '\u0022' + "F00FFF" + '\u0022' + ";");
                ArquivoMapa.WriteLine("    var pinImageCar = new google.maps.MarkerImage(" + '\u0022' + "http://icons.iconarchive.com/icons/icons-land/transporter/32/Taxi-Top-Yellow-icon.png" + '\u0022' + ",");
                ArquivoMapa.WriteLine("        new google.maps.Size(42, 68),");
                ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                ArquivoMapa.WriteLine("        new google.maps.Point(20, 68));");
                ArquivoMapa.WriteLine("");
                ArquivoMapa.WriteLine("    var pinImageBase = new google.maps.MarkerImage(" + '\u0022' + "http://chart.apis.google.com/chart?chst=d_map_pin_letter&chld=%E2%80%A2|" + '\u0022' + " + pinColorBase,");
                ArquivoMapa.WriteLine("        new google.maps.Size(42, 68),");
                ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                ArquivoMapa.WriteLine("        new google.maps.Point(20, 68));");
                ArquivoMapa.WriteLine("");
                ArquivoMapa.WriteLine("    var pinImageAberto = new google.maps.MarkerImage(" + '\u0022' + "http://chart.apis.google.com/chart?chst=d_map_pin_letter&chld=%E2%80%A2|" + '\u0022' + " + pinColorAberto,");
                ArquivoMapa.WriteLine("        new google.maps.Size(21, 34),");
                ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                ArquivoMapa.WriteLine("        new google.maps.Point(10, 34));");
                ArquivoMapa.WriteLine("");
                ArquivoMapa.WriteLine("    var pinImageConcluido = new google.maps.MarkerImage(" + '\u0022' + "http://chart.apis.google.com/chart?chst=d_map_pin_letter&chld=%E2%80%A2|" + '\u0022' + " + pinColorConcluido,");
                ArquivoMapa.WriteLine("        new google.maps.Size(21, 34),");
                ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                ArquivoMapa.WriteLine("        new google.maps.Point(10, 34));");
                ArquivoMapa.WriteLine("");
                ArquivoMapa.WriteLine("    var pinImageCancelado = new google.maps.MarkerImage(" + '\u0022' + "http://chart.apis.google.com/chart?chst=d_map_pin_letter&chld=%E2%80%A2|" + '\u0022' + " + pinColorCancelado,");
                ArquivoMapa.WriteLine("        new google.maps.Size(21, 34),");
                ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                ArquivoMapa.WriteLine("        new google.maps.Point(10, 34));");
                ArquivoMapa.WriteLine("");
                ArquivoMapa.WriteLine("    var pinImageNULL = new google.maps.MarkerImage(" + '\u0022' + "http://chart.apis.google.com/chart?chst=d_map_pin_letter&chld=%E2%80%A2|" + '\u0022' + " + pinColorNULL,");
                ArquivoMapa.WriteLine("        new google.maps.Size(21, 34),");
                ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                ArquivoMapa.WriteLine("        new google.maps.Point(10, 34));");
                ArquivoMapa.WriteLine("");
                ArquivoMapa.WriteLine("    var pinImageTrafo = new google.maps.MarkerImage(" + '\u0022' + "http://icons.iconarchive.com/icons/iconsmind/outline/16/Communication-Tower-2-icon.png" + '\u0022' + ",");
                ArquivoMapa.WriteLine("        new google.maps.Size(21, 34),");
                ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                ArquivoMapa.WriteLine("        new google.maps.Point(21, 34));");
                ArquivoMapa.WriteLine("");
                ArquivoMapa.WriteLine("    var pinImageTrafoDisponivel = new google.maps.MarkerImage(" + '\u0022' + "images/trafo_disponivel.png" + '\u0022' + ",");
                ArquivoMapa.WriteLine("        new google.maps.Size(21, 34),");
                ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                ArquivoMapa.WriteLine("        new google.maps.Point(21, 34));");
                ArquivoMapa.WriteLine("");
                ArquivoMapa.WriteLine("    var pinImageTrafoConcluido = new google.maps.MarkerImage(" + '\u0022' + "images/trafo_concluido.png" + '\u0022' + ",");
                ArquivoMapa.WriteLine("        new google.maps.Size(21, 34),");
                ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                ArquivoMapa.WriteLine("        new google.maps.Point(21, 34));");
                ArquivoMapa.WriteLine("");
                ArquivoMapa.WriteLine("    var pinImageTrafoInstConcluido = new google.maps.MarkerImage(" + '\u0022' + "images/trafo_inst_conc.png" + '\u0022' + ",");
                ArquivoMapa.WriteLine("        new google.maps.Size(21, 34),");
                ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                ArquivoMapa.WriteLine("        new google.maps.Point(21, 34));");
                ArquivoMapa.WriteLine("");
                ArquivoMapa.WriteLine("    var pinImageTrafoInstCancelado = new google.maps.MarkerImage(" + '\u0022' + "images/trafo_inst_canc.png" + '\u0022' + ",");
                ArquivoMapa.WriteLine("        new google.maps.Size(21, 34),");
                ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                ArquivoMapa.WriteLine("        new google.maps.Point(21, 34));");
                ArquivoMapa.WriteLine("");
                ArquivoMapa.WriteLine("    var pinImageTrafoCancelado = new google.maps.MarkerImage(" + '\u0022' + "images/trafo_cancelado.png" + '\u0022' + ",");
                ArquivoMapa.WriteLine("        new google.maps.Size(21, 34),");
                ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                ArquivoMapa.WriteLine("        new google.maps.Point(10, 34));");
                ArquivoMapa.WriteLine("");
                ArquivoMapa.WriteLine("    var pinImageRural = new google.maps.MarkerImage(" + '\u0022' + "http://chart.apis.google.com/chart?chst=d_map_pin_letter&chld=%E2%80%A2|" + '\u0022' + " + pinColorRural,");
                ArquivoMapa.WriteLine("        new google.maps.Size(32, 32),");
                ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                ArquivoMapa.WriteLine("        new google.maps.Point(32, 32));");
                ArquivoMapa.WriteLine("");
                ArquivoMapa.WriteLine("    var pinImageRuralDisponivel = new google.maps.MarkerImage(" + '\u0022' + "images/rural_disponivel.png" + '\u0022' + ",");
                ArquivoMapa.WriteLine("        new google.maps.Size(32, 32),");
                ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                ArquivoMapa.WriteLine("        new google.maps.Point(32, 32));");
                ArquivoMapa.WriteLine("");
                ArquivoMapa.WriteLine("    var pinImageRuralConcluido = new google.maps.MarkerImage(" + '\u0022' + "images/rural_concluido.png" + '\u0022' + ",");
                ArquivoMapa.WriteLine("        new google.maps.Size(32, 32),");
                ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                ArquivoMapa.WriteLine("        new google.maps.Point(32, 32));");
                ArquivoMapa.WriteLine("");
                ArquivoMapa.WriteLine("    var pinImageRuralInstConcluido = new google.maps.MarkerImage(" + '\u0022' + "images/rural_inst_conc.png" + '\u0022' + ",");
                ArquivoMapa.WriteLine("        new google.maps.Size(32, 32),");
                ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                ArquivoMapa.WriteLine("        new google.maps.Point(32, 32));");
                ArquivoMapa.WriteLine("");
                ArquivoMapa.WriteLine("    var pinImageRuralInstCancelado = new google.maps.MarkerImage(" + '\u0022' + "images/rural_inst_canc.png" + '\u0022' + ",");
                ArquivoMapa.WriteLine("        new google.maps.Size(32, 32),");
                ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                ArquivoMapa.WriteLine("        new google.maps.Point(32, 32));");
                ArquivoMapa.WriteLine("");
                ArquivoMapa.WriteLine("    var pinImageRuralCancelado = new google.maps.MarkerImage(" + '\u0022' + "images/rural_cancelado.png" + '\u0022' + ",");
                ArquivoMapa.WriteLine("        new google.maps.Size(32, 32),");
                ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                ArquivoMapa.WriteLine("        new google.maps.Point(32, 32));");
                ArquivoMapa.WriteLine("");
                ArquivoMapa.WriteLine("    var pinImageResidencial = new google.maps.MarkerImage(" + '\u0022' + "http://chart.apis.google.com/chart?chst=d_map_pin_letter&chld=%E2%80%A2|" + '\u0022' + " + pinColorResidencial,");
                ArquivoMapa.WriteLine("        new google.maps.Size(32, 32),");
                ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                ArquivoMapa.WriteLine("        new google.maps.Point(32, 32));");
                ArquivoMapa.WriteLine("");
                ArquivoMapa.WriteLine("    var pinImageResidencialDisponivel = new google.maps.MarkerImage(" + '\u0022' + "images/residencial_disponivel.png" + '\u0022' + ",");
                ArquivoMapa.WriteLine("        new google.maps.Size(32, 32),");
                ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                ArquivoMapa.WriteLine("        new google.maps.Point(32, 32));");
                ArquivoMapa.WriteLine("");
                ArquivoMapa.WriteLine("    var pinImageResidencialConcluido = new google.maps.MarkerImage(" + '\u0022' + "images/residencial_concluido.png" + '\u0022' + ",");
                ArquivoMapa.WriteLine("        new google.maps.Size(32, 32),");
                ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                ArquivoMapa.WriteLine("        new google.maps.Point(32, 32));");
                ArquivoMapa.WriteLine("");
                ArquivoMapa.WriteLine("    var pinImageResidencialInstConcluido = new google.maps.MarkerImage(" + '\u0022' + "images/residencial_inst_conc.png" + '\u0022' + ",");
                ArquivoMapa.WriteLine("        new google.maps.Size(32, 32),");
                ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                ArquivoMapa.WriteLine("        new google.maps.Point(32, 32));");
                ArquivoMapa.WriteLine("");
                ArquivoMapa.WriteLine("    var pinImageResidencialInstCancelado = new google.maps.MarkerImage(" + '\u0022' + "images/residencial_inst_canc.png" + '\u0022' + ",");
                ArquivoMapa.WriteLine("        new google.maps.Size(32, 32),");
                ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                ArquivoMapa.WriteLine("        new google.maps.Point(32, 32));");
                ArquivoMapa.WriteLine("");
                ArquivoMapa.WriteLine("    var pinImageResidencialCancelado = new google.maps.MarkerImage(" + '\u0022' + "images/residencial_cancelado.png" + '\u0022' + ",");
                ArquivoMapa.WriteLine("        new google.maps.Size(32, 32),");
                ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                ArquivoMapa.WriteLine("        new google.maps.Point(32, 32));");
                ArquivoMapa.WriteLine("");
                ArquivoMapa.WriteLine("    var pinImageIndustrial = new google.maps.MarkerImage(" + '\u0022' + "http://chart.apis.google.com/chart?chst=d_map_pin_letter&chld=%E2%80%A2|" + '\u0022' + " + pinColorIndustrial,");
                ArquivoMapa.WriteLine("        new google.maps.Size(32, 32),");
                ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                ArquivoMapa.WriteLine("        new google.maps.Point(32, 32));");
                ArquivoMapa.WriteLine("");
                ArquivoMapa.WriteLine("    var pinImageIndustrialDisponivel = new google.maps.MarkerImage(" + '\u0022' + "images/industrial_disponivel.png" + '\u0022' + ",");
                ArquivoMapa.WriteLine("        new google.maps.Size(32, 32),");
                ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                ArquivoMapa.WriteLine("        new google.maps.Point(32, 32));");
                ArquivoMapa.WriteLine("");
                ArquivoMapa.WriteLine("    var pinImageIndustrialConcluido = new google.maps.MarkerImage(" + '\u0022' + "images/industrial_concluido.png" + '\u0022' + ",");
                ArquivoMapa.WriteLine("        new google.maps.Size(32, 32),");
                ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                ArquivoMapa.WriteLine("        new google.maps.Point(32, 32));");
                ArquivoMapa.WriteLine("");
                ArquivoMapa.WriteLine("    var pinImageIndustrialInstConcluido = new google.maps.MarkerImage(" + '\u0022' + "images/industrial_inst_conc.png" + '\u0022' + ",");
                ArquivoMapa.WriteLine("        new google.maps.Size(32, 32),");
                ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                ArquivoMapa.WriteLine("        new google.maps.Point(32, 32));");
                ArquivoMapa.WriteLine("");
                ArquivoMapa.WriteLine("    var pinImageIndustrialInstCancelado = new google.maps.MarkerImage(" + '\u0022' + "images/industrial_inst_canc.png" + '\u0022' + ",");
                ArquivoMapa.WriteLine("        new google.maps.Size(32, 32),");
                ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                ArquivoMapa.WriteLine("        new google.maps.Point(32, 32));");
                ArquivoMapa.WriteLine("");
                ArquivoMapa.WriteLine("    var pinImageIndustrialCancelado = new google.maps.MarkerImage(" + '\u0022' + "images/industrial_cancelado.png" + '\u0022' + ",");
                ArquivoMapa.WriteLine("        new google.maps.Size(32, 32),");
                ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                ArquivoMapa.WriteLine("        new google.maps.Point(32, 32));");
                ArquivoMapa.WriteLine("");
                ArquivoMapa.WriteLine("    var pinImageComercial = new google.maps.MarkerImage(" + '\u0022' + "http://chart.apis.google.com/chart?chst=d_map_pin_letter&chld=%E2%80%A2|" + '\u0022' + " + pinColorComercial,");
                ArquivoMapa.WriteLine("        new google.maps.Size(32, 32),");
                ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                ArquivoMapa.WriteLine("        new google.maps.Point(32, 32));");
                ArquivoMapa.WriteLine("");
                ArquivoMapa.WriteLine("    var pinImageComercialDisponivel = new google.maps.MarkerImage(" + '\u0022' + "images/comercial_disponivel.png" + '\u0022' + ",");
                ArquivoMapa.WriteLine("        new google.maps.Size(32, 32),");
                ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                ArquivoMapa.WriteLine("        new google.maps.Point(32, 32));");
                ArquivoMapa.WriteLine("");
                ArquivoMapa.WriteLine("    var pinImageComercialConcluido = new google.maps.MarkerImage(" + '\u0022' + "images/comercial_concluido.png" + '\u0022' + ",");
                ArquivoMapa.WriteLine("        new google.maps.Size(32, 32),");
                ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                ArquivoMapa.WriteLine("        new google.maps.Point(32, 32));");
                ArquivoMapa.WriteLine("");
                ArquivoMapa.WriteLine("    var pinImageComercialInstConcluido = new google.maps.MarkerImage(" + '\u0022' + "images/comercial_inst_conc.png" + '\u0022' + ",");
                ArquivoMapa.WriteLine("        new google.maps.Size(32, 32),");
                ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                ArquivoMapa.WriteLine("        new google.maps.Point(32, 32));");
                ArquivoMapa.WriteLine("");
                ArquivoMapa.WriteLine("    var pinImageComercialInstCancelado = new google.maps.MarkerImage(" + '\u0022' + "images/comercial_inst_canc.png" + '\u0022' + ",");
                ArquivoMapa.WriteLine("        new google.maps.Size(32, 32),");
                ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                ArquivoMapa.WriteLine("        new google.maps.Point(32, 32));");
                ArquivoMapa.WriteLine("");
                ArquivoMapa.WriteLine("    var pinImageComercialCancelado = new google.maps.MarkerImage(" + '\u0022' + "images/comercial_cancelado.png" + '\u0022' + ",");
                ArquivoMapa.WriteLine("        new google.maps.Size(32, 32),");
                ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                ArquivoMapa.WriteLine("        new google.maps.Point(32, 32));");
                ArquivoMapa.WriteLine("");
                ArquivoMapa.WriteLine("    var pinImageServicoPublico = new google.maps.MarkerImage(" + '\u0022' + "http://chart.apis.google.com/chart?chst=d_map_pin_letter&chld=%E2%80%A2|" + '\u0022' + " + pinColorPoderPublico,");
                ArquivoMapa.WriteLine("        new google.maps.Size(24, 24),");
                ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                ArquivoMapa.WriteLine("        new google.maps.Point(24, 24));");
                ArquivoMapa.WriteLine("");
                ArquivoMapa.WriteLine("    var pinImageServicoPublicoDisponivel = new google.maps.MarkerImage(" + '\u0022' + "images/servico_publilco_disponivel.png" + '\u0022' + ",");
                ArquivoMapa.WriteLine("        new google.maps.Size(24, 24),");
                ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                ArquivoMapa.WriteLine("        new google.maps.Point(24, 24));");
                ArquivoMapa.WriteLine("");
                ArquivoMapa.WriteLine("    var pinImageServicoPublicoConcluido = new google.maps.MarkerImage(" + '\u0022' + "images/servico_publico_concluido.png" + '\u0022' + ",");
                ArquivoMapa.WriteLine("        new google.maps.Size(24, 24),");
                ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                ArquivoMapa.WriteLine("        new google.maps.Point(24, 24));");
                ArquivoMapa.WriteLine("");
                ArquivoMapa.WriteLine("    var pinImageServicoPublicoInstConcluido = new google.maps.MarkerImage(" + '\u0022' + "images/servico_publico_inst_conc.png" + '\u0022' + ",");
                ArquivoMapa.WriteLine("        new google.maps.Size(24, 24),");
                ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                ArquivoMapa.WriteLine("        new google.maps.Point(24, 24));");
                ArquivoMapa.WriteLine("");
                ArquivoMapa.WriteLine("    var pinImageServicoPublicoInstCancelado = new google.maps.MarkerImage(" + '\u0022' + "images/servico_publico_inst_canc.png" + '\u0022' + ",");
                ArquivoMapa.WriteLine("        new google.maps.Size(24, 24),");
                ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                ArquivoMapa.WriteLine("        new google.maps.Point(24, 24));");
                ArquivoMapa.WriteLine("");
                ArquivoMapa.WriteLine("    var pinImageServicoPublicoCancelado = new google.maps.MarkerImage(" + '\u0022' + "images/servico_publico_cancelado.png" + '\u0022' + ",");
                ArquivoMapa.WriteLine("        new google.maps.Size(24, 24),");
                ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                ArquivoMapa.WriteLine("        new google.maps.Point(24, 24));");
                ArquivoMapa.WriteLine("");

                // Seleciona do banco de dados todos os grupos que são posicao 1 e não tem instalacao
                //-----------------------------------------------------------------------------------
                adapter = null;
                dataSet.Clear();

                adapter = new MySqlDataAdapter(string.Format("SELECT * FROM TBCADASTRO WHERE (POSICAO = '1' AND (STATUS_INSTALACAO IS NULL OR STATUS_INSTALACAO = 'aberto')) ORDER BY GRUPO, POSICAO;"), conexao);
                adapter.Fill(dataSet);

                for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                {
                    // Se nesta selecao o grupo já tiver uc concluida, passa pra proxima
                    if (lista_grupos_concluidos.Any(dataSet.Tables[0].Rows[i][Constantes.GRUPO].ToString().Equals))
                    {
                        continue;
                    }

                    if (i > 0)
                    {
                        ArquivoMapa.WriteLine("    var marker" + i.ToString() + " = new google.maps.Marker({");
                        ArquivoMapa.WriteLine("        position: latlng" + i.ToString() + ",");
                    }
                    else
                    {
                        ArquivoMapa.WriteLine("    var marker = new google.maps.Marker({");
                        ArquivoMapa.WriteLine("        position: base,");
                    }
                    ArquivoMapa.WriteLine("        map: map,");
                    ArquivoMapa.WriteLine("        title: " + '\u0022' + "UC: " + dataSet.Tables[0].Rows[i][Constantes.UC].ToString() + '\u0022' + ",");


                    //ArquivoMapa.WriteLine("    var marker" + i.ToString() + " = new google.maps.Marker({");
                    //ArquivoMapa.WriteLine("        position: latlng" + i.ToString() + ",");
                    //ArquivoMapa.WriteLine("        map: map,");
                    //ArquivoMapa.WriteLine("        title: " + '\u0022' + "UC: " + dataSet.Tables[0].Rows[i][Constantes.UC].ToString() + '\u0022' + ",");

                    if (dataSet.Tables[0].Rows[i][Constantes.CLASSE].ToString() == "Transformador")
                    {
                        ArquivoMapa.WriteLine("        icon: pinImageTrafoDisponivel");
                    }
                    else if (dataSet.Tables[0].Rows[i][Constantes.CLASSE].ToString() == "Residencial")
                    {
                        ArquivoMapa.WriteLine("        icon: pinImageResidencialDisponivel");
                    }
                    else if (dataSet.Tables[0].Rows[i][Constantes.CLASSE].ToString() == "Rural")
                    {
                        ArquivoMapa.WriteLine("        icon: pinImageRuralDisponivel");
                    }
                    else if (dataSet.Tables[0].Rows[i][Constantes.CLASSE].ToString() == "Comercial")
                    {
                        ArquivoMapa.WriteLine("        icon: pinImageComercialDisponivel");
                    }
                    else if (dataSet.Tables[0].Rows[i][Constantes.CLASSE].ToString() == "Servico Publico")
                    {
                        ArquivoMapa.WriteLine("        icon: pinImageServicoPublicoDisponivel");
                    }
                    else if (dataSet.Tables[0].Rows[i][Constantes.CLASSE].ToString() == "Industrial")
                    {
                        ArquivoMapa.WriteLine("        icon: pinImageIndustrialDisponivel");
                    }
                    ArquivoMapa.WriteLine("    });");

                    // Listen for click event
                    if(i > 0)
                    {
                        ArquivoMapa.WriteLine("    google.maps.event.addListener(marker" + i.ToString() + ", " + '\u0022' + "click" + '\u0022' + ", function (e) {");
                    }
                    else
                    {
                        ArquivoMapa.WriteLine("    google.maps.event.addListener(marker, " + '\u0022' + "click" + '\u0022' + ", function (e) {");
                    }
                    ArquivoMapa.WriteLine("        var infoWindow = new google.maps.InfoWindow({");
                    //ArquivoMapa.WriteLine("        content: 'UC: " + dataSet.Tables[0].Rows[i][Constantes.UC].ToString() + "' + '<br />Grupo: " + dataSet.Tables[0].Rows[i][Constantes.GRUPO].ToString() + "' + '<br />Posição: " + dataSet.Tables[0].Rows[i][Constantes.POSICAO].ToString() + "' + '<br />Subclasse: " + dataSet.Tables[0].Rows[i][Constantes.SUBCLASSE].ToString() + "' + '<br /><br />Consumidor: " + dataSet.Tables[0].Rows[i][Constantes.NOME_CONSUMIDOR].ToString() + "' + '<br />Latitude: ' + latlng" + i.ToString() + ".lat() + '<br />Longitude: ' + latlng" + i.ToString() + ".lng()");
                    ArquivoMapa.WriteLine("        content: 'UC: " + dataSet.Tables[0].Rows[i][Constantes.UC].ToString() + "' + '<br />Grupo: " + dataSet.Tables[0].Rows[i][Constantes.GRUPO].ToString() + "' + '<br />Posição: " + dataSet.Tables[0].Rows[i][Constantes.POSICAO].ToString() + "' + '<br />Subclasse: " + dataSet.Tables[0].Rows[i][Constantes.SUBCLASSE].ToString() + "' + '<br />Equipamento: " + dataSet.Tables[0].Rows[i][Constantes.TIPO_EQUIPAMENTO].ToString() + "' + '<br />Fases: " + dataSet.Tables[0].Rows[i][Constantes.FASE_CONSUMIDOR].ToString() + "' + '<br /><br />Consumidor: " + dataSet.Tables[0].Rows[i][Constantes.NOME_CONSUMIDOR].ToString() + "' + '<br />Latitude: ' + latlng" + i.ToString() + ".lat() + '<br />Longitude: ' + latlng" + i.ToString() + ".lng()");
                    ArquivoMapa.WriteLine("        });");
                    if(i > 0)
                    {
                        ArquivoMapa.WriteLine("        infoWindow.open(map, marker" + i.ToString() + ");");
                    }
                    else
                    {
                        ArquivoMapa.WriteLine("        infoWindow.open(map, marker);");
                    }
                    ArquivoMapa.WriteLine("    });");
                }
                //-------------------------------------------------------------------------------------------------------------

                if (Variaveis_Globais.Usuario == "Admin")
                {
                    for (int i = 0; i < ListaCelulares.Items.Count; i++)
                    {
                        ArquivoMapa.WriteLine("    var marker_cars" + i.ToString() + " = new google.maps.Marker({");
                        ArquivoMapa.WriteLine("        position: latlng_cars" + i.ToString() + ",");
                        ArquivoMapa.WriteLine("        map: map,");
                        ArquivoMapa.WriteLine("        title: " + '\u0022' + ListaCelulares.Items[i].ToString() + '\u0022' + ",");
                        ArquivoMapa.WriteLine("        icon: pinImageCar");
                        ArquivoMapa.WriteLine("    });");

                        // Listen for click event  
                        ArquivoMapa.WriteLine("    google.maps.event.addListener(marker_cars" + (i).ToString() + ", " + '\u0022' + "click" + '\u0022' + ", function (e) {");
                        ArquivoMapa.WriteLine("        var infoWindow = new google.maps.InfoWindow({");
                        ArquivoMapa.WriteLine("        content: 'Equipe: " + ListaCelulares.Items[i].ToString() + "' + '<br />DataHora: " + DataHoraCelulares.Items[i].ToString() + "' + '<br />Latitude: ' + latlng_cars" + i.ToString() + ".lat() + '<br />Longitude: ' + latlng_cars" + i.ToString() + ".lng()");
                        ArquivoMapa.WriteLine("        });");
                        ArquivoMapa.WriteLine("        infoWindow.open(map, marker_cars" + (i).ToString() + ");");
                        ArquivoMapa.WriteLine("    });");
                    }
                }
                //-------------------------------------------------------------------------------------------------------------
                ArquivoMapa.WriteLine("}");
                ArquivoMapa.WriteLine("");
                ArquivoMapa.WriteLine("function error(msg){");
                ArquivoMapa.WriteLine("    var s = document.querySelector('#status');");
                ArquivoMapa.WriteLine("    s.InnerHTML = typeof msg == 'string' ? msg : " + '\u0022' + "failed" + '\u0022' + ";");
                ArquivoMapa.WriteLine("    s.className = 'fail';");
                ArquivoMapa.WriteLine("}");
                ArquivoMapa.WriteLine("");
                ArquivoMapa.WriteLine("if (navigator.geolocation) {");
                ArquivoMapa.WriteLine("    navigator.geolocation.getCurrentPosition(success, error);");
                ArquivoMapa.WriteLine("} else {");
                ArquivoMapa.WriteLine("    error('not supported');");
                ArquivoMapa.WriteLine("}");
                ArquivoMapa.WriteLine("");
                ArquivoMapa.WriteLine("google.maps.event.addDomListener(window, 'load', initialize);");
                ArquivoMapa.WriteLine("");
                ArquivoMapa.WriteLine("initialize();");

                ArquivoMapa.Close();
                Maps.Close();
            }
            catch
            {
                // Deu erro na geração do arquivo de Serviços Atendidos
            }
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("Default.aspx");
        }
        #endregion
    }
}