<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Andon.aspx.cs" Inherits="WEB_MGE.Andon" %>

<%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <!-- META -->
	<title>Andon StarMeasure</title>

    <script src="/scripts/Chart.min.js"></script>
    
    <style type="text/css">
    .navbar{
	    position: absolute; /* fixed absolute */
	    top:0px;
	    left:0;
	    width: 100%;
	    background:#ffffff;
	    color:#ffb100;
	    z-index:1000;
        padding: 3px;
        height: 82px;
    }

    table {
        border-collapse: collapse;
        padding-left: 10px;
        padding-right: 10px;
        font-size: 12px;
    }

    .auto-style4 {
        height: 32px;
        width: 19%;
    }
    
    td {
        border-collapse: collapse;
        padding-left: 10px;
        padding-right: 10px;
        font-size: 12px;
    }


    .logomge {
        height: 36px;
        width: 250px;
    }
    
    #form1 {
        height: 72px;
    }
        
    .auto-style10 {
        width: 1749px;
        height: 593px;
        margin-top: 3px;
        position: absolute;
        top: 228px;
        left: 5px;
        margin-right: 0px;
            margin-bottom: 0px;
        }
        
    .auto-style182 {
        width: 211px;
            height: 12px;
        }
    .auto-style184 {
        width: 100%;
        height: 68px;
            margin-top: 61px;
            margin-left: 0px;
        }
    .auto-style186 {
        width: 138px;
            height: 12px;
        }
    .pPlan
    {
	    font-weight: bold;
	    color: #000539;
    }

    p {
        color: #795400;    
        font-size: 15px;	
    }
        
    .auto-style202 {
        width: 400px;
    }
    .auto-style206 {
        width: 161px;
            height: 12px;
        }
        .auto-style209 {
            position: absolute; /* fixed absolute */
            top: 1px;
            left: 3px;
            width: 100%;
            background: #ffffff;
            color: #ffb100;
            z-index: 1000;
            padding: 3px;
            height: 82px;
            bottom: 528px;
        }
        .auto-style346 {
            width: 83px;
            font-family: Calibri;
            font-size: 28pt;
            height: 12px;
        }
        .auto-style393 {
            width: 368px;
            height: 14px;
        }
        .auto-style399 {
            width: 368px;
            height: 495px;
        }
        .auto-style400 {
            width: 368px;
            height: 495px;
        }
        .auto-style416 {
            width: 368px;
            height: 495px;
        }
        .auto-style417 {
            width: 368px;
            height: 495px;
        }
        .auto-style418 {
            width: 368px;
            height: 495px;
        }
        .auto-style432 {
            width: 197px;
            height: 57px;
        }
        .auto-style452 {
            height: 123px;
            margin-bottom: 8px;
        }
        .auto-style454 {
            width: 420px;
            height: 12px;
        }
        .auto-style455 {
            height: 12px;
        }
        .auto-style459 {
            width: 170px;
            height: 57px;
        }
        .auto-style470 {
            width: 203px;
            height: 57px;
        }
        .auto-style471 {
            width: 60px;
            height: 12px;
        }
        .auto-style477 {
            height: 17px;
            }
        .auto-style482 {
            cursor: pointer;
        }
        .auto-style491 {
            height: 17px;
            width: 33%;
        }
        .auto-style492 {
            height: 32px;
            width: 33%;
        }
        .auto-style522 {
            height: 32px;
            width: 15%;
        }
        .auto-style523 {
            height: 17px;
            width: 15%;
        }
        .auto-style524 {
            height: 17px;
            width: 15px;
        }
        .auto-style525 {
            height: 17px;
            width: 67px;
        }
        .auto-style527 {
            height: 17px;
            width: 21%;
        }
        .auto-style528 {
            height: 32px;
            width: 21%;
        }
        .auto-style529 {
            height: 32px;
            width: 25%;
        }
        .auto-style530 {
            height: 17px;
            width: 25%;
        }
        .auto-style531 {
            width: 279px;
            height: 12px;
        }
        .auto-style532 {
            width: 334px;
        }
        .auto-style533 {
            font-weight: bold;
            color: #000539;
            height: 30px;
        }
        .auto-style534 {
            font-weight: bold;
            color: #000539;
            height: 31px;
        }
        .auto-style537 {
            font-weight: bold;
            color: #000539;
            height: 29px;
        }
        .auto-style538 {
            width: 10px;
            height: 14px;
        }
        .auto-style539 {
            width: 10px;
        }
        .auto-style540 {
            width: 168px;
            height: 57px;
        }
        .auto-style541 {
            font-weight: bold;
            color: #000539;
            height: 31px;
            width: 180px;
        }
        </style>
</head>
<body style="height: 62px; margin-right: 0px; margin-bottom: 9px; margin-top: 5px; width: 1793px;">
    <form id="form1" runat="server" class="auto-style452">
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
        <asp:Timer ID="Timer1" OnTick="Timer1_Tick" runat="server" Interval="60000" />          
        <table class="auto-style209" sytle="border:5px solid #fff">
				    <tr>				
					    <td align="left" style="background-color:#29858C" class="auto-style522">
						    </td>					                         					    
					    <td align="left" style="background-color:#29858C" class="auto-style4" colspan="3">
						    <img class="logomge" src="images/LogoSMFundoTurquesa.png"></td>					                         					    
					    <td align="left" style="background-color:#29858C" class="auto-style529">
						    </td>					                         					    
					    <td align="left" style="background-color:#29858C" class="auto-style528">
						    </td>					                         					    
					    <td align="left" style="background-color:#29858C" class="auto-style492">
                            &nbsp;</td>					                         					    
				    </tr>
				    <tr>				
					    <td align="left" style="background-color:#29858C" class="auto-style523">
						    </td>					                         					    
					    <td align="left" style="background-color:#29858C" class="auto-style477" id="Home">
						    <asp:Button ID="btnHome" runat="server" BackColor="#29858C" BorderStyle="None" Font-Size="14pt" ForeColor="White" Text="Home" OnClick="btnHome_Click" CssClass="auto-style482" Width="62px" />
                        </td>					                         					    
					    <td align="left" style="background-color:#29858C" class="auto-style524" id="Home">
						    <asp:Button ID="btnInspecao" runat="server" BackColor="#29858C" BorderStyle="None" Font-Size="14pt" ForeColor="White" OnClick="btnInspecao_Click" Text="Inspeção" CssClass="auto-style482" Width="82px"/>
                        </td>					                         					    
					    <td align="left" style="background-color:#29858C" class="auto-style525" id="Home">
						    <asp:Button ID="btnAndon" runat="server" BackColor="#29858C" BorderStyle="None" Font-Size="14pt" ForeColor="White" Text="Andon" OnClick="btnAndon_Click" CssClass="auto-style482" Width="67px" />
                        </td>					                         					    
					    <td align="left" style="background-color:#29858C" class="auto-style530">
						    </td>					                         					    
					    <td align="left" style="background-color:#29858C" class="auto-style527">
						    </td>					                         					    
					    <td align="left" style="background-color:#29858C;color:#FFFFFF" class="auto-style491">
						    </td>					                         					    
				    </tr>
			    </table>
        
        <table class="auto-style10"; height="30px">
            <tr>
                <td class="auto-style393" style="background-color: #000000";"position:center" colspan="2";"align-items: center">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblMontagem" runat="server" Font-Size="26pt" ForeColor="White" Text="Montagem" Font-Names="Calibri" Align="Center"></asp:Label>
                    <br />
                    <br />
                    <br />
                    <br />
                    <asp:Label ID="lblQtdMontagem0" runat="server" Font-Names="Calibri" Font-Size="22pt" ForeColor="White" Text="Exe: "></asp:Label>
                &nbsp;<asp:Label ID="lblQtdMontagem" runat="server" Font-Names="Calibri" Font-Size="28pt" ForeColor="#009999" Text="0"></asp:Label>
                </td>
                <td class="auto-style538"></td>
                <td class="auto-style393" style="background-color: #000000" colspan="2">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:Label ID="lblTenAplicada" runat="server" Font-Size="26pt" ForeColor="White" Text="Tensão Aplicada" Font-Names="Calibri"></asp:Label>
                    &nbsp;&nbsp;
                    <br />
                    <br />
                    <br />
                    <br />
                    <asp:Label ID="lblQtdTenAplicada0" runat="server" Font-Names="Calibri" Font-Size="22pt" ForeColor="White" Text="Exe: "></asp:Label>
                &nbsp;<asp:Label ID="lblQtdTenAplicada" runat="server" Font-Names="Calibri" Font-Size="28pt" ForeColor="#009999" Text="0"></asp:Label>
                </td>
                <td class="auto-style538"></td>
                <td class="auto-style393" style="background-color: #000000" colspan="2">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblExatidao" runat="server" Font-Size="26pt" ForeColor="White" Text="Exatidão" Font-Names="Calibri"></asp:Label>
                    <br />
                    <br />
                    <br />
                    <br />
                    <asp:Label ID="lblQtdExatidao0" runat="server" Font-Names="Calibri" Font-Size="22pt" ForeColor="White" Text="Exe: "></asp:Label>
                    <asp:Label ID="lblQtdExatidao" runat="server" Font-Names="Calibri" Font-Size="28pt" ForeColor="#009999" Text="0"></asp:Label>
                </td>
                <td class="auto-style538"></td>
                <td class="auto-style393" style="background-color: #000000" colspan="2">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblMarcaLaser" runat="server" Font-Size="26pt" ForeColor="White" Text="Marca Laser" Font-Names="Calibri"></asp:Label>
                    &nbsp;<br />
                    <br />
                    <br />
                    <br />
                    <asp:Label ID="lblQtdMarcaLaser0" runat="server" Font-Names="Calibri" Font-Size="22pt" ForeColor="White" Text="Exe: "></asp:Label>
                    <asp:Label ID="lblQtdMarcaLaser" runat="server" Font-Names="Calibri" Font-Size="28pt" ForeColor="#009999" Text="0"></asp:Label>
                </td>
                <td class="auto-style538"></td>
                <td class="auto-style393" style="background-color: #000000" colspan="2">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblTesteFinal" runat="server" Font-Size="26pt" ForeColor="White" Text="Teste Final" Font-Names="Calibri"></asp:Label>
                    &nbsp;<br />
                    <br />
                    <br />
                    <br />
                    <asp:Label ID="lblQtdTesteFinal0" runat="server" Font-Names="Calibri" Font-Size="22pt" ForeColor="White" Text="Exe: "></asp:Label>
                    <asp:Label ID="lblQtdTesteFinal" runat="server" Font-Names="Calibri" Font-Size="28pt" ForeColor="#009999" Text="0"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style459" style="background-color: #000000;color:#000000;">
                    <asp:Label ID="lblQtdTenAplicada6" runat="server" Font-Names="Calibri" Font-Size="12pt" ForeColor="White" Text="Pendente: "></asp:Label>
                    <br />
                </td>
                <td class="auto-style459" style="background-color: #000000;color:#000000";"height=15px">
				    <p class="auto-style537" id="dnMonPendente" runat="server" style="font-family: Calibri; color: #FFFFFF; font-size: x-large;">dnMonPendente</p>
                </td>
                <td rowspan="2" class="auto-style539">&nbsp;</td>
                <td class="auto-style432" style="background-color: #000000;color:#000000";"width=17px";"height=15px">
                    <asp:Label ID="lblQtdTenAplicada3" runat="server" Font-Names="Calibri" Font-Size="12pt" ForeColor="White" Text="Pendente: "></asp:Label>
                    <br />
                </td>
                <td class="auto-style470" style="background-color: #000000;color:#000000";"height=15px">
				    <p class="auto-style541" id="dnTenApPendente" runat="server" style="font-family: Calibri; color: #FFFFFF; font-size: x-large;">dnTenApPendente</p>
                </td>
                <td rowspan="2" class="auto-style539">&nbsp;</td>
                <td class="auto-style540" style="background-color: #000000;color:#000000";"width=170px";"height=15px">
                    <asp:Label ID="lblQtdTenAplicada13" runat="server" Font-Names="Calibri" Font-Size="12pt" ForeColor="White" Text="Pendente: "></asp:Label>
                    <br />
                    </td>
                <td class="auto-style459" style="background-color: #000000;color:#000000";"height=15px">
				    <p class="auto-style534" id="dnExatidaoPendente" runat="server" style="font-family: Calibri; color: #FFFFFF; font-size: x-large;">dnExatidaoPendente</p>
                    </td>
                <td rowspan="2" class="auto-style539">&nbsp;</td>
                <td class="auto-style459" style="background-color: #000000;color:#000000";"width=170px";"height=15px">
                    <asp:Label ID="lblQtdTenAplicada14" runat="server" Font-Names="Calibri" Font-Size="12pt" ForeColor="White" Text="Pendente: "></asp:Label>
                    <br />
                    </td>
                <td class="auto-style459" style="background-color: #000000;color:#000000";"height=15px">
				    <p class="auto-style533" id="dnMarcaLaserPendente" runat="server" style="font-family: Calibri; color: #FFFFFF; font-size: x-large;">dnMarcaLaserPendente</p>
                    </td>
                <td rowspan="2" class="auto-style539">&nbsp;</td>
                <td class="auto-style459" style="background-color: #000000;color:#000000";"width=170px";"height=15px">
                    <asp:Label ID="lblQtdTenAplicada15" runat="server" Font-Names="Calibri" Font-Size="12pt" ForeColor="White" Text="Pendente: "></asp:Label>
                    <br />
                    </td>
                <td class="auto-style459" style="background-color: #000000;color:#000000";"height=15px">
				    <p class="auto-style537" id="dnTesteFinalPendente" runat="server" style="font-family: Calibri; color: #FFFFFF; font-size: x-large;">dnTesteFinalPendente</p>
                    </td>
            </tr>
            <tr>
                  
                <td class="auto-style399" style="background-color: #000000;color:#000000" colspan="2">
				    <canvas id="GraficoMontagem" height="320px" width="320px"; style="font-family: Calibri"></canvas>
                    <script type="text/javascript" src="/scripts/GraficoMontagem.js"> </script>
			    </td>
                <td class="auto-style416" style="background-color: #000000;color:#000000" colspan="2">
				    <canvas id="GraficoTenAplicada" height="160px" width="160px"; style="font-family: Calibri"></canvas>
                    <script type="text/javascript" src="/scripts/GraficoTenAplicada.js"> </script>
			    </td>
                <td class="auto-style417" style="background-color: #000000;color:#000000" colspan="2">
				    <canvas id="GraficoExatidao" height="320px" width="320px"; style="font-family: Calibri"></canvas>
                    <script type="text/javascript" src="/scripts/GraficoExatidao.js"> </script>
			    </td>
                <td class="auto-style418" style="background-color: #000000;color:#000000" colspan="2">
				    <canvas id="GraficoMarcaLaser" height="320px" width="320px"; style="font-family: Calibri"></canvas>
                    <script type="text/javascript" src="/scripts/GraficoMarcaLaser.js"> </script>
			    </td>
                <td class="auto-style400" style="background-color: #000000;color:#000000" colspan="2">
				    <canvas id="GraficoTesteFinal" height="320px" width="320px"; style="font-family: Calibri"></canvas>
                    <script type="text/javascript" src="/scripts/GraficoTesteFinal.js"> </script>
			    </td>           
            </tr>
        </table>
               <div></div>
                <table class="auto-style184">
                    <tr>
                        <td class="auto-style346">
                            OP</td>
                        <td class="auto-style206">
                            <asp:DropDownList ID="ddlOP" runat="server" Font-Names="Calibri" Font-Size="X-Large" Height="35px" OnSelectedIndexChanged="ddlOP_SelectedIndexChanged" OnTextChanged="ddlOP_TextChanged">
                            </asp:DropDownList>
                        </td>
                        <td class="auto-style186">
                            <asp:Button ID="btnProcessar" runat="server" Font-Names="Calibri" Font-Size="20pt" OnClick="btnProcessar_Click" Text="Processar" Width="135px" />
                        </td>
                        <td class="auto-style454">
                            <h1 id="vCliente" runat="server" class="auto-style202" style="font-family: Calibri; font-size: x-large" visible="False">Cliente</h1></td>
                        <td class="auto-style531">
                            <asp:Label ID="lblQuantidade" runat="server" Font-Names="Calibri" Font-Size="26pt" Text="Quantidade: "></asp:Label>
                        </td>
                        <td class="auto-style471">
						    </td>
                        <td class="auto-style182">
						    <h2 id="vDHAtualiza" runat="server" style="font-family: 'Times New Roman'; font-size: xx-large; font-style: normal;" class="auto-style532">&nbsp;</h2>
					    </td>
                        <td class="auto-style455">
					    </td>
                        <td class="auto-style455"></td>
                    </tr>
                    <tr>
                        <td colspan="9">
                            <asp:Label ID="lblModelo" runat="server" Font-Names="Calibri" Font-Size="28pt" Text="Modelo: "></asp:Label>
                        </td>
                    </tr>
                </table>
    </form>
    <div></div>
</body>
</html>
