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

</head>

<body id="body">

    <div runat="server" class="alert alert-danger" role="alert" id="loginError" visible="false">
        <span class="glyphicon glyphicon-exclamation-sign" aria-hidden="true"></span>
        <span class="sr-only">Error:</span>
        Usuário e/ou senha inválido(s)!
    </div>    

    <form runat="server">

        <div id="login">            

            <div id="logo">

                <img id="mge" src="images/logo-preto.png" />

		    </div><!-- logo -->		

            <div id="data">

                <br /><br /><br />
                <asp:Label id="labelTitulo" runat="server" Font-Size="Large" Font-Bold="true" Font-Names="Arial" Text="ACESSO AO PAINEL MGE"></asp:Label>
                
                <br /><br />
                <asp:Label id="lblUsuario" runat="server" Font-Names="Arial" Text="Usuário:"></asp:Label>
                <asp:TextBox id="tbUsuario" runat="server"></asp:TextBox>            
                
                <br /><br />
                <asp:Label id="lblSenha" runat="server" Font-Names="Arial" Text="Senha:"></asp:Label>
                <asp:TextBox id="tbSenha" TextMode="Password" runat="server"></asp:TextBox>

                <br /><br />
                <asp:Button id="btnEnviar" runat="server" Text="Entrar" OnClick="btnEnviar_Click" />

		    </div><!-- data -->		

        </div><!-- login -->

    </form>

</body>

</html>