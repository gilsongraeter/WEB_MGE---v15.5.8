<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WEB_MGE.Default1" %>

<!DOCTYPE html>
<html>
    <head><!--########################################################################################################-->

	    <!-- META -->
	    <title>Painel de Controle</title>
	    <meta charset="UTF-8">
	    <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
	    <meta name="description" content="" />
    
	    <link rel="icon" href="images/icone.ico" type="image/x-icon">

	    <!-- CSS -->
	    <link rel="stylesheet" type="text/css" href="styles/Default.css" media="all" /> 
	    <link rel="stylesheet" href="styles/Table.css" type="text/css"/>		    

        <!-- Javascript -->            
        <script src="scripts/Default.js"></script>
        <script src="scripts/Chart.js"></script>

        <style type="text/css">
            .auto-style1 {
                height: 110px;
            }
            .auto-style2 {
                width: 220px;
                height: 81px;
            }
            </style>

    </head><!--########################################################################################################-->


    <body><!--#########################################################################################################-->                       

        <!-- CABEÇALHO DA PÁGINA ---------------------------------------------------------------------------------------->
        <form runat="server" />
	    <div>
		    <nav background="#ff5757" width="100%">
			    <table class="navbar" sytle="border:5px solid #fff">
				    <tr>				
					    <td align="left" width="30%" class="auto-style1">
						    <img class="logomge" src="images/logo-preto.png" height="70px">
					    </td>
					    <td align="center" class="auto-style1">
						    <h1 class="auto-style2">Painel de Controle</h1>	
					    </td>
					    <td align="right" width="30%" class="auto-style1">
						    <h3>Última atualização</h3>
						    <h2 id="vDHAtualiza" runat="server">vDHAtualiza</h2>
					    </td>
				    </tr>
				    <tr class="ptbrow">
					    <td align="left"><h1 id="vCliente" runat="server">Cliente</h1></td>
                        <td align="center"><h1 id="vServico" runat="server">Serviço</h1></td>
					    <td align="right"><h1 id="vAmostraTot" runat="server"></h1></td>
				    </tr>
			    </table>
		    </nav>
	    </div>
	    <!-- FIM DO CABEÇALHO DA PÁGINA --------------------------------------------------------------------------------->        


	    <div>
		    <br><br><br><br><br><br><br><br>	<br>	
	    </div>


        <!-- ÁREA DOS GRÁFICOS DA PÁGINA -------------------------------------------------------------------------------->        
	    <div style="width: 100%">            
		    <table style="width: 100%">                
		    <tr>
			    <td align="center">                 
                    <h1>Instalações</h1>   
				    <canvas id="GraficoInstalacoes" height="220%" width="220%"; />
			    </td>
			    <td align="center">
				    <h1>Leituras</h1>
				    <canvas id="GraficoLeituras" height="220%" width="220%"; />
			    </td>
			    <td align="center">
				    <h1>Retiradas</h1>
				    <canvas id="GraficoRetirados" height="220%" width="220%"; />
			    </td>
			    <td align="center">
				    <h1>Depuradas</h1>
				    <canvas id="GraficoDepuracao" height="220%" width="220%"; />
			    </td>
			    <td align="center">
				    <h1>Enviadas</h1>
				    <canvas id="GraficoEnviados" height="220%" width="220%"; />
			    </td>
		    </tr>
		    <tr>
			    <td align="center" valign="top">			
				    <p class="pPlan" id="dnInsPlanejado" runat="server">dnInsPlanejado</p>	
				    <p class="pExec" id="dnInsExecutado" runat="server">dnInsExecutado</p>	
				    <p class="pPend" id="dnInsPendentes" runat="server">dnInsPendentes</p>	
				    <p class="pVisi" id="dnInsVisitas" runat="server">dnInsVisitas</p>	
				    <p class="pRein" id="dnInsReinstalados" runat="server">dnInsReinstalados</p>	
			    </td>
			    <td align="center" valign="top">	
				    <p class="pPlan" id="dnLeiPlanejado" runat="server">dnLeiPlanejado</p>	
				    <p class="pExec" id="dnLeiExecutado" runat="server">dnLeiExecutado</p>	
				    <p class="pPend" id="dnLeiPendentes" runat="server">dnLeiPendentes</p>	
				    <p class="pPerd" id="dnLeiPerdida" runat="server">dnLeiPerdida</p>
			    </td>
			    <td align="center" valign="top">
				    <p class="pPlan" id="dnRetPlanejado" runat="server">dnRetPlanejado</p>	
				    <p class="pExec" id="dnRetExecutado" runat="server">dnRetExecutado</p>	
				    <p class="pPend" id="dnRetPendentes" runat="server">dnRetPendentes</p>
			    </td>
			    <td align="center" valign="top">
				    <p class="pPlan" id="dnDepPlanejado" runat="server">dnDepPlanejado</p>	
				    <p class="pExec" id="dnDepExecutado" runat="server">dnDepExecutado</p>	
				    <p class="pPend" id="dnDepPendentes" runat="server">dnDepPendentes</p>
			    </td>
			    <td align="center" valign="top">
				    <p class="pPlan" id="dnEnvPlanejado" runat="server">dnEnvPlanejado</p>	
				    <p class="pExec" id="dnEnvExecutado" runat="server">dnEnvExecutado</p>	
				    <p class="pPend" id="dnEnvPendentes" runat="server">dnEnvPendentes</p>
			    </td>                
		    </tr>
		
		    </table>
	    </div>
        <!-- FIM DA ÁREA DOS GRÁFICOS DA PÁGINA ------------------------------------------------------------------------->


        <hr>


        <!-- TABELA RESUMO ---------------------------------------------------------------------------------------------->
	    <div class="datagrid" style="width:70%">
		    <table>
			    <thead>
				    <tr  align="center">
					    <th>Extrato por Classe</th>
					    <th>Amostra</th>
					    <th>Instalações</th>
					    <th>Leituras</th>
					    <th>Retiradas</th>
					    <th>Depuradas</th>
					    <th>Enviadas</th>
				    </tr>
			    </thead>
			    <tbody  align="center">
				    <tr>
					    <td align="right">Residencial</td>
					    <td id="vAmostraRes" runat="server">vAmostraRes</td>
					    <td id="vInsRes" runat="server">vInsRes</td>
					    <td id="vLeiRes" runat="server">vLeiRes</td>
					    <td id="vRetRes" runat="server">vRetRes</td>
					    <td id="vDepRes" runat="server">vDepRes</td>
					    <td id="vEnvRes" runat="server">vEnvRes</td>
				    </tr>
				    <tr>
					    <td align="right">Comercial</td>
					    <td id="vAmostraCom" runat="server">vAmostraCom</td>
					    <td id="vInsCom" runat="server">vInsCom</td>
					    <td id="vLeiCom" runat="server">vLeiCom</td>
					    <td id="vRetCom" runat="server">vRetCom</td>
					    <td id="vDepCom" runat="server">vDepCom</td>
					    <td id="vEnvCom" runat="server">vEnvCom</td>
				    </tr>
				    <tr>
					    <td align="right">Industrial</td>
					    <td id="vAmostraInd" runat="server">vAmostraInd</td>
					    <td id="vInsInd" runat="server">vInsInd</td>
					    <td id="vLeiInd" runat="server">vLeiInd</td>
					    <td id="vRetInd" runat="server">vRetInd</td>
					    <td id="vDepInd" runat="server">vDepInd</td>
					    <td id="vEnvInd" runat="server">vEnvInd</td>
				    </tr>
				    <tr>
					    <td align="right">Poder Publico</td>
					    <td id="vAmostraPub" runat="server">vAmostraPub</td>
					    <td id="vInsPub" runat="server">vInsPub</td>
					    <td id="vLeiPub" runat="server">vLeiPub</td>
					    <td id="vRetPub" runat="server">vRetPub</td>
					    <td id="vDepPub" runat="server">vDepPub</td>
					    <td id="vEnvPub" runat="server">vEnvPub</td>
				    </tr>
				    <tr>
					    <td align="right">Rural</td>
					    <td id="vAmostraRur" runat="server">vAmostraRur</td>
					    <td id="vInsRur" runat="server">vInsRur</td>
					    <td id="vLeiRur" runat="server">vLeiRur</td>
					    <td id="vRetRur" runat="server">vRetRur</td>
					    <td id="vDepRur" runat="server">vDepRur</td>
					    <td id="vEnvRur" runat="server">vEnvRur</td>
				    </tr>
				    <tr>
					    <td align="right">Transformadores</td>
					    <td id="vAmostraTra" runat="server">vAmostraTra</td>
					    <td id="vInsTra" runat="server">vInsTra</td>
					    <td id="vLeiTra" runat="server">vLeiTra</td>
					    <td id="vRetTra" runat="server">vRetTra</td>
					    <td id="vDepTra" runat="server">vDepTra</td>
					    <td id="vEnvTra" runat="server">vEnvTra</td>
				    </tr>
				    <tr>
					    <td align="right">Total</td>
					    <td id="AmostraTotTabela" runat="server">AmostraTotTabela</td>
					    <td id="vInsTot" runat="server">vInsTot</td>
					    <td id="vLeiTot" runat="server">vLeiTot</td>
					    <td id="vRetTot" runat="server">vRetTot</td>
					    <td id="vDepTot" runat="server">vDepTot</td>
					    <td id="vEnvTot" runat="server">vEnvTot</td>                        
				    </tr>
			    </tbody>
		    </table>                        
	    </div>
            <br />
        <!-- FIM DA TABELA RESUMO --------------------------------------------------------------------------------------->
            
            <asp:HyperLink ID="HyperLink1" runat="server">Mapa</asp:HyperLink>
        
            <br />
						    <a href="MapaCliente.aspx"> 
                                <asp:Image ID="Image1" runat="server" Height="92px" ImageUrl="/images/MapaLink.png" Width="92px" />
                            </a>
					            
    </body><!--########################################################################################################-->			
</html>