<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MapaCliente.aspx.cs" Inherits="WEB_MGE.MapaCliente" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {            height: 50px;
        }
        .logomge {
            height: 120px;
            width: 265px;
        }
        .auto-style13 {
            height: 50px;
            width: 1px;
        }
        .h1Painel {
            width: 1398px;
            height: 17px;
        }
        .auto-style14 {
            height: 80px;
            width: 1319px;
        }
        .auto-style15 {
            width: 100%;
            height: 104px;
        }
        .auto-style16 {
            width: 1321px;
            height: 39px;
            margin-top: 3px;
        }
        .auto-style27 {
            height: 70px;
            width: 156px;
        }
        .auto-style30 {
            height: 50px;
            width: 201px;
        }
        .auto-style31 {
            margin-top: 18px;
            font-family: Verdana;
        }
        .auto-style32 {
            height: 114px;
        }
        .auto-style34 {
            height: 301px;
            margin-top: 12px;
        }
        .auto-style35 {
            height: 380px;
            width: 1320px;
        }
        .auto-style36 {
            height: 95px;
        }
        .auto-style37 {
            font-family: Verdana;
        }
        .auto-style38 {
            font-family: Verdana;
            font-size: small;
        }
    </style>
</head>
<body style="width: 1325px; height: 118px; margin-top: 5px;">
    <form id="form1" runat="server" class="auto-style32">
          <div id="map1" class="auto-style34">
    <div class="auto-style14">
    
        <table class="auto-style15">
            <tr>
                <td class="auto-style13">
						    <img class="auto-style27" src="images/logo-preto.png"><br />
                </td>
                <td align="center" class="auto-style30">
                     <asp:Panel ID="Panel11" runat="server" Height="85px" Width="1144px">
                         <br />
                         <asp:Button ID="BtnVisaoGeral" runat="server" BorderStyle="Outset" Font-Size="X-Large" ForeColor="Black" OnClick="BtnVisaoGeral_Click" Text="Visão Geral" Width="190px" Font-Bold="True" Height="36px" />
                         &nbsp;<asp:Button ID="BtnConcluidos" runat="server" BorderStyle="Outset" Font-Size="X-Large" OnClick="BtnConcluidos_Click" Text="Concluídos" Width="208px" Font-Bold="True" />
                         &nbsp;<asp:Button ID="BtnCancelados" runat="server" BorderStyle="Outset" Font-Size="X-Large" OnClick="BtnCancelados_Click" Text="Cancelados" Width="210px" Font-Bold="True" />
                         &nbsp;<asp:Button ID="EmAbertos" runat="server" BorderStyle="Outset" Font-Size="X-Large" OnClick="BtnAbertos_Click" Text="em Aberto" Width="197px" Font-Bold="True" />
                         &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                         <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="/Images/botao_voltar.png" OnClick="ImageButton1_Click" />
                         &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                     </asp:Panel>
                </td>
            </tr>
            <tr>
                <td class="auto-style1" colspan="2">
                    <asp:Panel ID="Panel6" runat="server" Height="43px" BackColor="#FFCC66" Width="1320px" CssClass="auto-style31" ForeColor="White">
                        <h1 align="center" class="auto-style16"><span class="auto-style37">MAPA</span> - <span class="auto-style37">Campanha</span> <span class="auto-style37">de</span> <span class="auto-style37">Medidas</span> - Projeto
                            <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                        </h1>
                    </asp:Panel>
                </td>
                </tr>
            </table>
    <div class="auto-style35" id="map">
    
        &nbsp;</div>
        <div class="auto-style36" id="legenda">
    
        &nbsp;<asp:Image ID="Image1" runat="server" Height="20px" ImageUrl="/images/residencial.png" Width="20px" />
            &nbsp;- <span class="auto-style38">Residencial&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span>
            <asp:Image ID="Image5" runat="server" Height="32px" ImageUrl="/Images/comercial.png" Width="32px" CssClass="auto-style38" />
&nbsp;- <span class="auto-style38">Comercial&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span>
            <asp:Image ID="Image10" runat="server" Height="37px" ImageUrl="/Images/rural.png" Width="35px" CssClass="auto-style38" />
&nbsp;- <span class="auto-style38">Rural&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span>
            <asp:Image ID="Image14" runat="server" Height="32px" ImageUrl="/Images/industrial.png" Width="32px" CssClass="auto-style38" />
&nbsp;- <span class="auto-style38">Industrial&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span>
            <asp:Image ID="Image18" runat="server" Height="29px" ImageUrl="/Images/servico_publico.png" Width="28px" CssClass="auto-style38" />
&nbsp;- <span class="auto-style38">Serviço</span> <span class="auto-style38">Público&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span>
            <asp:Image ID="Image19" runat="server" Height="32px" ImageUrl="/Images/trafo.png" Width="20px" CssClass="auto-style38" />
&nbsp;- <span class="auto-style38">Transformador</span><br />
            <asp:Image ID="Image20" runat="server" Height="24px" ImageUrl="/Images/concluido.png" Width="24px" />
&nbsp;- <span class="auto-style38">Medição</span> <span class="auto-style38">Concluída&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; </span>
            <asp:Image ID="Image21" runat="server" Height="24px" ImageUrl="/Images/inst_conc.png" Width="24px" CssClass="auto-style38" />
&nbsp;- <span class="auto-style38">Instalação</span> <span class="auto-style38">concluída</span>.&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Image ID="Image22" runat="server" Height="24px" ImageUrl="/Images/disponivel.png" Width="24px" />
&nbsp;- <span class="auto-style38">Disponível&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; </span>
            <asp:Image ID="Image23" runat="server" Height="24px" ImageUrl="/Images/instalacao_cancelada.png" Width="24px" CssClass="auto-style38" />
&nbsp;- <span class="auto-style38">Instalação</span> <span class="auto-style38">cancelada</span>.&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Image ID="Image24" runat="server" Height="24px" ImageUrl="/Images/retirada_cancelada.png" Width="24px" />
&nbsp;- <span class="auto-style38">Retirada</span> <span class="auto-style38">cancelada</span>.<br />
        </div>
        &nbsp;<br />
        <br />
        </div>
          </div>
            <script src ="scripts/MapaAuxiliar.js"></script>            
    <script async defer
    src="https://maps.googleapis.com/maps/api/js?key=AIzaSyCYbfpYyLzUZml_kR1LAaGB4-EaZRJHeKM&callback=initialize">
    </script>
    </form>
</body>
</html>
