<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BoasVindas.aspx.cs" Inherits="WEB_MGE.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">



.navbar{
	position: absolute; /* fixed absolute */
	top:14px;
	left:0;
	width: 100%;
	background:#ffffff;
	color:#ffb100;
	z-index:1000;
    padding: 3px;
            height: 82px;
        }

        .logomge {
            height: 36px;
            width: 250px;
        }
        .auto-style9 {
            border-collapse: collapse;
            font-size: 12px;
            width: 1664px;
            padding-left: 10px;
            padding-right: 10px;
        }
        .auto-style13 {
            border-collapse: collapse;
            font-size: 12px;
            width: 256px;
            height: 40px;
            padding-left: 10px;
            padding-right: 10px;
            background-color: #29858C;
        }
        .auto-style14 {
            border-collapse: collapse;
            font-size: 12px;
            height: 40px;
            width: 115px;
            padding-left: 10px;
            padding-right: 10px;
            background-color: #29858C;
        }
        .auto-style16 {
            border-collapse: collapse;
            font-size: 12px;
            width: 256px;
            height: 42px;
            padding-left: 10px;
            padding-right: 10px;
            background-color: #29858C;
        }
        .auto-style17 {
            border-collapse: collapse;
            font-size: 12px;
            height: 42px;
            padding-left: 10px;
            padding-right: 10px;
            background-color: #29858C;
        }
        .auto-style19 {
            border-collapse: collapse;
            font-size: 12px;
            height: 40px;
            width: 105px;
            padding-left: 10px;
            padding-right: 10px;
            background-color: #29858C;
        }
        .auto-style20 {
            border-collapse: collapse;
            font-size: 12px;
            height: 40px;
            width: 73px;
            padding-left: 10px;
            padding-right: 10px;
            background-color: #29858C;
        }
        .auto-style21 {
            border-collapse: collapse;
            font-size: 12px;
            height: 42px;
            width: 348px;
            padding-left: 10px;
            padding-right: 10px;
            background-color: #29858C;
        }
        .auto-style22 {
            border-collapse: collapse;
            font-size: 12px;
            height: 40px;
            width: 348px;
            padding-left: 10px;
            padding-right: 10px;
            background-color: #29858C;
        }
        .auto-style23 {
            border-collapse: collapse;
            font-size: 12px;
            height: 42px;
            width: 349px;
            padding-left: 10px;
            padding-right: 10px;
            background-color: #29858C;
        }
        .auto-style24 {
            border-collapse: collapse;
            font-size: 12px;
            height: 40px;
            width: 349px;
            padding-left: 10px;
            padding-right: 10px;
            background-color: #29858C;
        }
    </style>
</head>
<body>
    <form id="BoasVindas" runat="server">
        <table style="width: 100%;">
            <tr>
                <td>
			    <table class="auto-style9" sytle="border:5px solid #fff">
				    <tr>				
					    <td align="left" class="auto-style16">
						    </td>					                         					    
					    <td align="left" class="auto-style17" colspan="3">
						    <img class="logomge" src="images/LogoSMFundoTurquesa.png"></td>					                         					    
					    <td align="left" class="auto-style21">
						    </td>					                         					    
					    <td align="left" class="auto-style23">
						    </td>					                         					    
					    <td align="left" class="auto-style23">
						    </td>					                         					    
				    </tr>
				    <tr>				
					    <td align="left" class="auto-style13">
						    </td>					                         					    
					    <td align="left" class="auto-style20" id="Home">
						    <asp:Button ID="btnHome" runat="server" Style="cursor:pointer" BackColor="#29858C" BorderStyle="None" Font-Size="14pt" ForeColor="White" Text="Home" OnClick="btnHome_Click" />
                        </td>					                         					    
					    <td align="left" class="auto-style14" id="Home0">
						    <asp:Button ID="btnInspecao" runat="server" Style="cursor:pointer" BackColor="#29858C" BorderStyle="None" Font-Size="14pt" ForeColor="White" OnClick="btnInspecao_Click" Text="Inspeção"/>
                        </td>					                         					    
					    <td align="left" class="auto-style19" id="Home1">
						    <asp:Button ID="btnAndon" runat="server" Style="cursor:pointer" BackColor="#29858C" BorderStyle="None" Font-Size="14pt" ForeColor="White" Text="Andon" OnClick="btnAndon_Click" />
                        </td>					                         					    
					    <td align="left" class="auto-style22">
						    </td>					                         					    
					    <td align="left" class="auto-style24">
						    </td>					                         					    
					    <td align="left" class="auto-style24">
						    </td>					                         					    
				    </tr>
			    </table>
                </td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:ImageButton ID="ImgBtnVoltar" runat="server" BorderStyle="None" ImageAlign="Right" ImageUrl="~/images/Voltar_ativo.png" OnClick="ImgBtnVoltar_Click" />
        <asp:Image ID="Image1" runat="server" ImageUrl="~/images/MGE_3 validadaQFullHD_SL.jpg" />
                </td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
        </table>
    <div>
    
    </div>
    </form>
</body>
</html>
