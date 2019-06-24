using System;
using MySql.Data.MySqlClient;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WEB_MGE
{
    public partial class MapaSelecao : System.Web.UI.Page
    {
        #region Atributos

        private int i;
        private int NumeroCidadesSelecionadas = 0;
        private string CmdAux = "";

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
            CBLCidades.Items.Clear();
            conexao = new MySqlConnection("server = " + Constantes.ENDERECO_SERVIDOR + "; User ID = " + Constantes.USUARIO_DATABASE + "; database = " + Variaveis_Globais.Cliente + "; password = " + Constantes.SENHA_DATABASE);
            adapter = new MySqlDataAdapter(string.Format("SELECT DISTINCT CIDADE_CONSUMIDOR FROM TBCADASTRO ORDER BY CIDADE_CONSUMIDOR ASC;"), conexao);
            adapter.Fill(dataSet);

            for (i = 0; i < dataSet.Tables[0].Rows.Count; i++)
            {
                if(dataSet.Tables[0].Rows[i][Constantes.CIDADE_CONSUMIDOR].ToString() != "")
                {
                    CBLCidades.Items.Add(dataSet.Tables[0].Rows[i][Constantes.CIDADE_CONSUMIDOR].ToString());
                }
            }

            adapter = null;
            dataSet.Clear();

            CBLClasse.Items.Clear();
            adapter = new MySqlDataAdapter(string.Format("SELECT DISTINCT CLASSE FROM TBCADASTRO ORDER BY CLASSE ASC;"), conexao);
            adapter.Fill(dataSet);

            for (i = 0; i < dataSet.Tables[0].Rows.Count; i++)
            {
                if (dataSet.Tables[0].Rows[i][Constantes.CLASSE].ToString() != "")
                {
                    CBLClasse.Items.Add(dataSet.Tables[0].Rows[i][Constantes.CLASSE].ToString());
                }
            }

            adapter = null;
            dataSet.Clear();

            CBLFaixa.Items.Clear();
            adapter = new MySqlDataAdapter(string.Format("SELECT DISTINCT SUBCLASSE FROM TBCADASTRO ORDER BY SUBCLASSE ASC;"), conexao);
            adapter.Fill(dataSet);

            for (i = 0; i < dataSet.Tables[0].Rows.Count; i++)
            {
                if (dataSet.Tables[0].Rows[i][Constantes.SUBCLASSE].ToString() != "")
                {
                    CBLFaixa.Items.Add(dataSet.Tables[0].Rows[i][Constantes.SUBCLASSE].ToString());
                }
            }

            adapter = null;
            dataSet.Clear();

            CBLPosicao.Items.Clear();
            adapter = new MySqlDataAdapter(string.Format("SELECT DISTINCT POSICAO FROM TBCADASTRO ORDER BY POSICAO;"), conexao);
            adapter.Fill(dataSet);

            for (i = 0; i < dataSet.Tables[0].Rows.Count; i++)
            {
                if (dataSet.Tables[0].Rows[i][Constantes.POSICAO].ToString() != "")
                {
                    CBLPosicao.Items.Add(dataSet.Tables[0].Rows[i][Constantes.POSICAO].ToString());
                }
            }

            adapter = null;
            dataSet.Clear();

            CBLFases.Items.Clear();
            adapter = new MySqlDataAdapter(string.Format("SELECT DISTINCT FASE_CONSUMIDOR FROM TBCADASTRO ORDER BY FASE_CONSUMIDOR ASC;"), conexao);
            adapter.Fill(dataSet);

            for (i = 0; i < dataSet.Tables[0].Rows.Count; i++)
            {
                if(dataSet.Tables[0].Rows[i][Constantes.FASE_CONSUMIDOR].ToString() != "")
                {
                    if(dataSet.Tables[0].Rows[i][Constantes.FASE_CONSUMIDOR].ToString() == "A")
                    {
                        CBLFases.Items.Add("Monofásico");
                    }
                    else if(dataSet.Tables[0].Rows[i][Constantes.FASE_CONSUMIDOR].ToString() == "AB")
                    {
                        CBLFases.Items.Add("Bifásico");
                    }
                    else if (dataSet.Tables[0].Rows[i][Constantes.FASE_CONSUMIDOR].ToString() == "ABC")
                    {
                        CBLFases.Items.Add("Trifásico");
                    }
                    else
                    {
                        CBLFases.Items.Add(dataSet.Tables[0].Rows[i][Constantes.FASE_CONSUMIDOR].ToString());
                    }
                }
            }

            adapter = null;
            dataSet.Clear();

            CBLServico.Items.Clear();
            adapter = new MySqlDataAdapter(string.Format("SELECT DISTINCT TIPO_SERVICO FROM TBCADASTRO ORDER BY TIPO_SERVICO ASC;"), conexao);
            adapter.Fill(dataSet);

            for (i = 0; i < dataSet.Tables[0].Rows.Count; i++)
            {
                if(dataSet.Tables[0].Rows[i][Constantes.TIPO_SERVICO].ToString() != "")
                {
                    CBLServico.Items.Add(dataSet.Tables[0].Rows[i][Constantes.TIPO_SERVICO].ToString());
                }
            }

            adapter = null;
            dataSet.Clear();

            CBLStatus.Items.Clear();
            adapter = new MySqlDataAdapter(string.Format("SELECT DISTINCT STATUS_INSTALACAO FROM TBCADASTRO ORDER BY STATUS_INSTALACAO ASC;"), conexao);
            adapter.Fill(dataSet);

            for (i = 0; i < dataSet.Tables[0].Rows.Count; i++)
            {
                if(dataSet.Tables[0].Rows[i][Constantes.STATUS_INSTALACAO].ToString() != "")
                {
                    CBLStatus.Items.Add(dataSet.Tables[0].Rows[i][Constantes.STATUS_INSTALACAO].ToString());
                }
            }

            adapter = null;
            dataSet.Clear();

            CBLEquipamento.Items.Clear();
            adapter = new MySqlDataAdapter(string.Format("SELECT DISTINCT TIPO_EQUIPAMENTO FROM TBCADASTRO ORDER BY TIPO_EQUIPAMENTO ASC;"), conexao);
            adapter.Fill(dataSet);

            for (i = 0; i < dataSet.Tables[0].Rows.Count; i++)
            {
                if(dataSet.Tables[0].Rows[i][Constantes.TIPO_EQUIPAMENTO].ToString() != "")
                {
                    CBLEquipamento.Items.Add(dataSet.Tables[0].Rows[i][Constantes.TIPO_EQUIPAMENTO].ToString());
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

        protected void Button1_Click(object sender, EventArgs e)
        {
            //-------------------------------------------------------------------------
            ListaCelulares.Items.Clear();
            LatitudeCelulares.Items.Clear();
            LongitudeCelulares.Items.Clear();
            DataHoraCelulares.Items.Clear();
            conexao = new MySqlConnection("server = " + Constantes.ENDERECO_SERVIDOR + "; User ID = " + Constantes.USUARIO_DATABASE + "; database = " + Constantes.DATABASE_TB_PAINEL + "; password = " + Constantes.SENHA_DATABASE);
            adapter = new MySqlDataAdapter(string.Format("SELECT * FROM CELULARES;"), conexao);
            adapter.Fill(dataSet);
            for (i = 0; i < dataSet.Tables[0].Rows.Count; i++)
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

            for (i = 0; i < CBLCidades.Items.Count; i++)
            {
                if (CBLCidades.Items[i].Selected)
                {
                    NumeroCidadesSelecionadas++;
                }
            }
            NumeroCidadesSelecionadas = 3;
            CBLCidades.Items[3].Selected = true;
            CBLCidades.Items[4].Selected = true;
            CBLCidades.Items[6].Selected = true;

            conexao = null;
            adapter = null;
            dataSet.Clear();

            conexao = new MySqlConnection("server = " + Constantes.ENDERECO_SERVIDOR + "; User ID = " + Constantes.USUARIO_DATABASE + "; database = " + Variaveis_Globais.Cliente + "; password = " + Constantes.SENHA_DATABASE);
            CmdAux = "SELECT * FROM TBCADASTRO ";
            if(NumeroCidadesSelecionadas > 0)
            {
                CmdAux = CmdAux + "WHERE ";
                for (i = 0; i < CBLCidades.Items.Count; i++)
                {
                    if (CBLCidades.Items[i].Selected)
                    {
                        if (i == CBLCidades.SelectedIndex)
                        {
                            CmdAux = CmdAux + "((CIDADE_CONSUMIDOR = '" + CBLCidades.Items[i].Text.ToString() + "')";
                        }
                        else
                        {
                            CmdAux = CmdAux + " OR (CIDADE_CONSUMIDOR = '" + CBLCidades.Items[i].Text.ToString() + "')";
                        }
                    }
                }
                CmdAux = CmdAux + ")";
                CmdAux = CmdAux + " ORDER BY GRUPO, POSICAO;";             
            }
            adapter = new MySqlDataAdapter(string.Format(CmdAux), conexao);
            adapter.Fill(dataSet);

            // Gera um arquivo de Log das UCs atendidas
            System.IO.Stream Maps;
            System.IO.StreamWriter ArquivoMapa;
            string DiretorioAtual;

            try
            {
                //DiretorioAtual = "C:\\MGE\\T.I\\Desenvolvimento\\WEB_MGE\\WEB_MGE - v14\\WEB_MGE\\scripts";
                DiretorioAtual = "C:\\FTP\\WEB_MGE\\scripts";
                Maps = System.IO.File.Open(@DiretorioAtual + "\\MapaAuxiliar.js", System.IO.FileMode.Create);
                ArquivoMapa = new System.IO.StreamWriter(Maps);
                ArquivoMapa.WriteLine("function initialize() {");
                //for (i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                //{
                //    CBLCidades.Items.Add(dataSet.Tables[0].Rows[i][Constantes.CIDADE_CONSUMIDOR].ToString());
                //    ArquivoMapa.WriteLine("    var latlng" + i.ToString() + " = new google.maps.LatLng(" + dataSet.Tables[0].Rows[i][Constantes.LATITUDE].ToString() + ", " + dataSet.Tables[0].Rows[i][Constantes.LONGITUDE].ToString() + ");");
                //}

                for (i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                {
                    if (i > 0)
                    {
                        if((dataSet.Tables[0].Rows[i][Constantes.LATITUDE_INSTALACAO].ToString() != null)&&(dataSet.Tables[0].Rows[i][Constantes.LATITUDE_INSTALACAO].ToString() != ""))
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
                    for (i = 0; i < ListaCelulares.Items.Count; i++)
                    {
                        ArquivoMapa.WriteLine("    var latlng_cars" + i.ToString() + " = new google.maps.LatLng(" + LatitudeCelulares.Items[i].ToString() + ", " + LongitudeCelulares.Items[i].ToString() + ");");
                    }
                }
                //-------------------------------------------------------------------------
                ArquivoMapa.WriteLine("");

                dataSet.Clear();
                adapter.Fill(dataSet);

                for (i = 0; i < dataSet.Tables[0].Rows.Count; i++)
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
                        ArquivoMapa.WriteLine("    var pinImageTrafo = new google.maps.MarkerImage(" + '\u0022' + "http://chart.apis.google.com/chart?chst=d_map_pin_letter&chld=%E2%80%A2|" + '\u0022' + " + pinColorTrafo,");
                        ArquivoMapa.WriteLine("        new google.maps.Size(21, 34),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(10, 34));");
                        ArquivoMapa.WriteLine("");
                        ArquivoMapa.WriteLine("    var pinImageRural = new google.maps.MarkerImage(" + '\u0022' + "http://chart.apis.google.com/chart?chst=d_map_pin_letter&chld=%E2%80%A2|" + '\u0022' + " + pinColorRural,");
                        ArquivoMapa.WriteLine("        new google.maps.Size(21, 34),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(10, 34));");
                        ArquivoMapa.WriteLine("");
                        ArquivoMapa.WriteLine("    var pinImageResidencial = new google.maps.MarkerImage(" + '\u0022' + "http://chart.apis.google.com/chart?chst=d_map_pin_letter&chld=%E2%80%A2|" + '\u0022' + " + pinColorResidencial,");
                        ArquivoMapa.WriteLine("        new google.maps.Size(21, 34),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(10, 34));");
                        ArquivoMapa.WriteLine("");
                        ArquivoMapa.WriteLine("    var pinImageIndustrial = new google.maps.MarkerImage(" + '\u0022' + "http://chart.apis.google.com/chart?chst=d_map_pin_letter&chld=%E2%80%A2|" + '\u0022' + " + pinColorIndustrial,");
                        ArquivoMapa.WriteLine("        new google.maps.Size(21, 34),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(10, 34));");
                        ArquivoMapa.WriteLine("");
                        ArquivoMapa.WriteLine("    var pinImageComercial = new google.maps.MarkerImage(" + '\u0022' + "http://chart.apis.google.com/chart?chst=d_map_pin_letter&chld=%E2%80%A2|" + '\u0022' + " + pinColorComercial,");
                        ArquivoMapa.WriteLine("        new google.maps.Size(21, 34),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(10, 34));");
                        ArquivoMapa.WriteLine("");
                        ArquivoMapa.WriteLine("    var pinImagePoderPublico = new google.maps.MarkerImage(" + '\u0022' + "http://chart.apis.google.com/chart?chst=d_map_pin_letter&chld=%E2%80%A2|" + '\u0022' + " + pinColorPoderPublico,");
                        ArquivoMapa.WriteLine("        new google.maps.Size(21, 34),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(0,0),");
                        ArquivoMapa.WriteLine("        new google.maps.Point(10, 34));");
                        ArquivoMapa.WriteLine("");
                    }
                    if(i > 0)
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
                    ArquivoMapa.WriteLine("        title: " + '\u0022' + "UC: " + dataSet.Tables[0].Rows[i][Constantes.UC].ToString() + " - Consumidor: " + dataSet.Tables[0].Rows[i][Constantes.NOME_CONSUMIDOR].ToString() + '\u0022' + ",");
                    ArquivoMapa.WriteLine("        icon: pinImageConcluido");
                    ArquivoMapa.WriteLine("    });");
                    /*ArquivoMapa.WriteLine("");
                    if (i > 0)
                    {
                        ArquivoMapa.WriteLine("    google.maps.event.addListener(marker" + i.ToString() + ", " + '\u0022' + "click" + '\u0022' + ", function (e) {");
                        ArquivoMapa.WriteLine("        var infoWindow = new.google.maps.InfoWindow({");
                        ArquivoMapa.WriteLine("            content:  'Consumidor: " + dataSet.Tables[0].Rows[i][Constantes.NOME_CONSUMIDOR].ToString() + "' + '<br />Endereço:  " + dataSet.Tables[0].Rows[i][Constantes.ENDERECO_CONSUMIDOR].ToString() + "' + '<br />Latitude: ' + latlng" + i.ToString() + ".lat() + '<br />Longitude: ' + latlng" + i.ToString() + ".lng()");
                        ArquivoMapa.WriteLine("        });");
                        ArquivoMapa.WriteLine("        infoWindow.open(map, marker" + i.ToString() + ");");
                        ArquivoMapa.WriteLine("    });");
                        ArquivoMapa.WriteLine("");
                    }*/
                }
                if (Variaveis_Globais.Usuario == "Admin")
                {
                    for (i = 0; i < ListaCelulares.Items.Count; i++)
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
            
            Response.Redirect("Mapa.aspx");
        }
        #endregion
    }
}