<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Mapa.aspx.cs" Inherits="WEB_MGE.Mapa" %>

<!DOCTYPE html>
<html>
  <head>
    <style>
      #map {
        height: 500px;
        width: 100%;
       }
        .auto-style14 {
            height: 495px;
            width: 1319px;
        }
        .auto-style15 {
            width: 100%;
            height: 173px;
        }
        .auto-style27 {
            height: 70px;
            width: 156px;
        }
        .auto-style1 {            height: 52px;
        }
        .auto-style16 {
            width: 1321px;
            height: 43px;
            margin-top: 0px;
        }
        .auto-style28 {
            margin-top: 71px;
            height: 425px;
            width: 93%;
        }
        .auto-style29 {
            height: 99px;
            width: 1319px;
        }
        .auto-style30 {
            height: 67px;
        }
    </style>
  </head>
  <body>
      <form id="form1" runat="server">
      <div id="map2" class="auto-style29">

        <table class="auto-style15">
            <tr>
                <td class="auto-style30">
						    <img class="auto-style27" src="images/logo-preto.png"><br />
						    &nbsp;<br />
                </td>
            </tr>
            <tr>
                <td class="auto-style1">
                    <asp:Panel ID="Panel6" runat="server" Height="45px" BackColor="#FFCC66" Width="1318px" ForeColor="White">
                        <h1 align="center" class="auto-style16">MAPA - Campanha de Medidas - Projeto
                            <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                        </h1>
                    </asp:Panel>
                </td>
                </tr>
            </table>

      </div>
          <div id="map" class="auto-style28">
    <div class="auto-style14">
    
        &nbsp;</div>
          </div>
            <script src ="scripts/MapaAuxiliar.js"></script>
    <script async defer
    src="https://maps.googleapis.com/maps/api/js?key=AIzaSyCYbfpYyLzUZml_kR1LAaGB4-EaZRJHeKM&callback=initialize">
    </script>
      </form>
  </body>
</html>