<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WEB_MGE.Default" %>

<!DOCTYPE html>

<html>

<head>

    <!-- META -->
    <title>Painel de Controle</title>
    <meta charset="UTF-8">
	<meta name="viewport" content="width=device-width, initial-scale=1.0"/>
	<meta name="description" content="" />

    <link rel="icon" href="images/icone.ico" type="image/x-icon"/>

    <link style="text/css" rel="stylesheet" href="styles/Login.css" />
    <link style="text/css" rel="stylesheet" href="bootstrap/css/bootstrap.css" />

    <style type="text/css">
        .auto-style1 {
            left: 517px;
            top: 121px;
            width: 167px;
            height: 267px;
            margin-left: 0px;
        }
        .auto-style2 {
            left: 150px;
            top: 0px;
            width: 90px;
        }
        .auto-style3 {
            height: 449px;
            width: 409px;
            margin-bottom: 5px;
        }
        .auto-style4 {
            height: 564px;
        }
        .auto-style5 {
            height: 188px;
        }
        .auto-style6 {
            height: 32px;
        }
    </style>

</head>

<body id="body" style="height: 660px">

    <div runat="server" class="alert alert-danger" role="alert" id="loginError" visible="false">
        <span class="glyphicon glyphicon-exclamation-sign" aria-hidden="true"></span>
        <span class="sr-only">Error:</span>
        Usuário e/ou senha inválido(s)!
    </div>    

    <form runat="server" class="auto-style4">

        <div id="login" class="auto-style3">            

            <div id="logo" class="auto-style5">

                <img id="mge" src="images/STARMEANSURE%20-%20turquesa%20fundo%20branco.png" class="auto-style1" />

		    </div><!-- logo -->		

            <div id="data" style="font-family: 'Times New Roman'; font-size: large; font-style: normal" class="auto-style6">

                <br />
                <asp:Label id="labelTitulo" runat="server" Font-Size="Large" Font-Bold="true" Font-Names="Arial" Text="SISTEMA GESTÃO A VISTA" ForeColor="#29858C"></asp:Label>
                
                <br /><br />
                <asp:Label id="lblUsuario" runat="server" Font-Names="Arial" Text="Usuário:" ForeColor="#29858C"></asp:Label>
                <asp:TextBox id="tbUsuario" runat="server" ForeColor="#29858C" BorderColor="#29858C"></asp:TextBox>            
                
                <br /><br />
                <asp:Label id="lblSenha" runat="server" Font-Names="Arial" Text="Senha:" ForeColor="#29858C"></asp:Label>
                <asp:TextBox id="tbSenha" TextMode="Password" runat="server" ForeColor="#29858C" BorderColor="#29858C"></asp:TextBox>

                <br /><br />
                <asp:Button id="btnEnviar" runat="server" Text="Entrar" OnClick="btnEnviar_Click" CssClass="auto-style2" Font-Size="16pt" />

		    </div><!-- data -->		

        </div><!-- login -->

    </form>

</body>

</html>