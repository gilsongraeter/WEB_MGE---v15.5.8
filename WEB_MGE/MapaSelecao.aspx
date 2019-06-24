<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MapaSelecao.aspx.cs" Inherits="WEB_MGE.MapaSelecao" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {            height: 52px;
        }
        .auto-style6 {
            width: 155px;
            height: 147px;
        }
        .auto-style7 {
            height: 147px;
            width: 284px;
        }
        .auto-style9 {
            height: 147px;
            width: 138px;
        }
        .logomge {
            height: 120px;
            width: 265px;
        }
        .auto-style10 {
            height: 143px;
        }
        .auto-style13 {
            height: 143px;
            width: 248px;
        }
        .h1Painel {
            width: 1398px;
            height: 17px;
        }
        .auto-style14 {
            height: 339px;
            width: 1319px;
        }
        .auto-style15 {
            width: 100%;
            height: 328px;
        }
        .auto-style16 {
            width: 1321px;
            height: 17px;
        }
        .auto-style17 {
            width: 156px;
            height: 147px;
        }
        .auto-style21 {
            height: 147px;
            width: 248px;
        }
        .auto-style22 {
            height: 147px;
        }
        .auto-style23 {
            width: 136px;
            height: 147px;
        }
        .auto-style25 {
            margin-left: 7px;
        }
        .auto-style26 {
            width: 104px;
            height: 147px;
        }
        .auto-style27 {
            height: 70px;
            width: 156px;
        }
    </style>
</head>
<body style="width: 1325px; height: 342px">
    <form id="form1" runat="server">
    <div class="auto-style14">
    
        <table class="auto-style15">
            <tr>
                <td class="auto-style13" colspan="4">
                    <br />
						    <img class="auto-style27" src="images/logo-preto.png"><br />
                </td>
                <td align="center" class="auto-style10" colspan="4">
                     &nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style1" colspan="8">
                    <asp:Panel ID="Panel6" runat="server" Height="36px" BackColor="#FFCC66" Width="1318px">
                        <h1 align="center" class="auto-style16">FILTRO PARA VISUALIZAÇÃO DE MAPA</h1>
                    </asp:Panel>
                </td>
                </tr>
            <tr>
                <td class="auto-style21">
        <asp:Panel ID="Panel1" runat="server" Height="149px" Width="251px" Font-Bold="True">
            <br />
            Cidades :<br />
            <asp:CheckBoxList ID="CBLCidades" runat="server">
            </asp:CheckBoxList>
        </asp:Panel>
                </td>
                <td class="auto-style23">
                    <asp:Panel ID="Panel8" runat="server" Font-Bold="True" Height="148px" Width="143px">
                        <br />
                        Classe:<asp:CheckBoxList ID="CBLClasse" runat="server">
                        </asp:CheckBoxList>
                    </asp:Panel>
                </td>
                <td class="auto-style26">
                    <asp:Panel ID="Panel9" runat="server" Font-Bold="True" Height="149px" Width="163px">
                        <br />
                        Faixa:<asp:CheckBoxList ID="CBLFaixa" runat="server">
                        </asp:CheckBoxList>
                    </asp:Panel>
                </td>
                <td class="auto-style22">
                    <asp:Panel ID="Panel10" runat="server" CssClass="auto-style25" Font-Bold="True" Height="149px" Width="85px">
                        <br />
                        Posição:<asp:CheckBoxList ID="CBLPosicao" runat="server">
                        </asp:CheckBoxList>
                    </asp:Panel>
                </td>
                <td class="auto-style17">
                    <asp:Panel ID="Panel2" runat="server" Height="149px" style="margin-top: 0px" Font-Bold="True" Width="130px">
                        <br />
                        Fases:<br />
                        <asp:CheckBoxList ID="CBLFases" runat="server">
                        </asp:CheckBoxList>
                    </asp:Panel>
                </td>
                <td class="auto-style6">
                    <asp:Panel ID="Panel3" runat="server" Height="150px" Font-Bold="True" Width="139px">
                        <br />
                        Serviço:<br />
                        <asp:CheckBoxList ID="CBLServico" runat="server">
                        </asp:CheckBoxList>
                        <br />
                    </asp:Panel>
                </td>
                <td class="auto-style9">
                    <asp:Panel ID="Panel4" runat="server" Height="151px" Font-Bold="True" Width="132px">
                        <br />
                        Status:<br />
                        <asp:CheckBoxList ID="CBLStatus" runat="server">
                        </asp:CheckBoxList>
                    </asp:Panel>
                </td>
                <td class="auto-style7">
                    <asp:Panel ID="Panel5" runat="server" Height="155px" Font-Bold="True" Width="158px" EnableTheming="True">
                        <br />
                        Equipamento:<br />
                        <asp:CheckBoxList ID="CBLEquipamento" runat="server">
                        </asp:CheckBoxList>
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <asp:Button ID="Button1" runat="server" BorderStyle="Outset" Font-Bold="True" Font-Size="XX-Large" OnClick="Button1_Click" Text="Processar" />
                        <br />
                    </asp:Panel>
                </td>
            </tr>
        </table>
        &nbsp;</div>
    </form>
</body>
</html>
