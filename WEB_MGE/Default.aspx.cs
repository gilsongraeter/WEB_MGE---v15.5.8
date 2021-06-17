using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Web;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;

namespace WEB_MGE
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            /*
            PingReply oPing;

            for (int x = 0; x < 2; x++)
            {
                System.Threading.Thread.Sleep(250);
                oPing = new Ping().Send("192.168.25.3", 5000);

                if (oPing.Status == IPStatus.Success)
                {
                    // Encontrou servidor
                    // Falta conferir o nome
                    IPHostEntry ipHost = Dns.GetHostEntry("192.168.25.3");
                    string hostNome = ipHost.HostName;
                    if ((hostNome.Contains("PRODSERV")) || (hostNome.Contains("mgers")))
                    {
                        // Encontrou o servidor
                        Variaveis_Globais.Servidor = true;
                        //EscreveMensagem("Aviso", "Rede Interna MGE. ", true);
                        break;
                    }
                    else
                    {
                        // Não encontrou o nome do servidor
                        Variaveis_Globais.Servidor = false;
                        //EscreveMensagem("Aviso", "Rede Externa MGE - starmeasure.ddns.net. ", true);
                    }
                }
                else
                {
                    // Não encontrou o ip = 192.168.0.29
                    Variaveis_Globais.Servidor = false;
                    //EscreveMensagem("Aviso", "Rede Externa MGE - starmeasure.ddns.net. ", true);
                }
            }
            */
        }

        protected void btnInspecao_Click(object sender, EventArgs e)
        {
            Server.Transfer("BoasVindas.aspx");
        }

        protected void btnAndon_Click(object sender, EventArgs e)
        {
            Server.Transfer("Andon.aspx");
        }
    }
}