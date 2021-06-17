<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WEB_MGE.WebForm2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        #form1 {
            height: 260px;
        }
            table, th, td {
    border-collapse: collapse;
    padding-left: 10px;
    padding-right: 10px;
    font-size: 12px;
}


.h1Painel
{
	color: #271b01;
    font-size: 45px;
}


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
        .auto-style11 {
            position: absolute; /* fixed absolute */;
            top: -3px;
            left: 0;
            width: 1932px;
            background: #ffffff;
            color: #ffb100;
            z-index: 1000;
            padding: 3px;
            height: 82px;
            margin-top: 3px;
        }
        .auto-style16 {
            height: 32px;
            width: 39px;
        }
        .auto-style22 {
            height: 32px;
            width: 427px;
        }
        .auto-style24 {
            height: 17px;
            width: 427px;
        }
        .auto-style26 {
            height: 32px;
            width: 404px;
        }
        .auto-style27 {
            height: 17px;
            width: 404px;
        }
        .auto-style31 {
            height: 32px;
            width: 540px;
        }
        .auto-style32 {
            height: 17px;
            width: 540px;
        }
        .auto-style33 {
            height: 32px;
            width: 851px;
        }
        .auto-style34 {
            height: 17px;
            width: 851px;
        }
        .auto-style35 {
            width: 1920px;
        }
        .auto-style36 {
            height: 22px;
            width: 1915px;
        }
        .auto-style37 {
            cursor: pointer;
        }
        .auto-style38 {
            height: 17px;
            width: 101px;
        }
        .auto-style39 {
            height: 17px;
            width: 124px;
        }
        .auto-style40 {
            height: 17px;
            width: 105px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" class="auto-style35">
			    <table class="auto-style11" sytle="border:5px solid #fff">
				    <tr>				
					    <td align="left" style="background-color:#29858C" class="auto-style26">
						    </td>					                         					    
					    <td align="left" style="background-color:#29858C" class="auto-style16" colspan="3">
						    <img class="logomge" src="images/LogoSMFundoTurquesa.png"></td>					                         					    
					    <td align="left" style="background-color:#29858C" class="auto-style22">
						    </td>					                         					    
					    <td align="left" style="background-color:#29858C" class="auto-style31">
						    &nbsp;</td>					                         					    
					    <td align="left" style="background-color:#29858C" class="auto-style33">
						    &nbsp;</td>					                         					    
				    </tr>
				    <tr>				
					    <td align="left" style="background-color:#29858C" class="auto-style27">
						    </td>					                         					    
					    <td align="left" style="background-color:#29858C" class="auto-style40" id="Home">
						    <asp:Button ID="btnHome" runat="server" Style="cursor:pointer" BackColor="#29858C" BorderStyle="None" Font-Size="14pt" ForeColor="White" Text="Home" />
                        </td>					                         					    
					    <td align="left" style="background-color:#29858C" class="auto-style39" id="Home">
						    <asp:Button ID="btnInspecao" runat="server" Style="cursor:pointer" BackColor="#29858C" BorderStyle="None" Font-Size="14pt" ForeColor="White" OnClick="btnInspecao_Click" Text="Inspeção"/>
                        </td>					                         					    
					    <td align="left" style="background-color:#29858C" class="auto-style38" id="Home">
						    <asp:Button ID="btnAndon" runat="server" BackColor="#29858C" BorderStyle="None" Font-Size="14pt" ForeColor="White" Text="Andon" OnClick="btnAndon_Click" CssClass="auto-style37" Width="67px" />
                        </td>					                         					    
					    <td align="left" style="background-color:#29858C" class="auto-style24">
						    </td>					                         					    
					    <td align="left" style="background-color:#29858C" class="auto-style32">
						    &nbsp;</td>					                         					    
					    <td align="left" style="background-color:#29858C" class="auto-style34">
						    &nbsp;</td>					                         					    
				    </tr>
			    </table>
    </form>
    <div class="auto-style36">
    
    </div>
    
    </body>
</html>
