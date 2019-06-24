<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Menu.aspx.cs" Inherits="WEB_MGE.Menu" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link style="text/css" rel="stylesheet" href="bootstrap/css/bootstrap.css" />
    <link rel="stylesheet" href="styles/Menu.css" type="text/css"/>		 
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div id="tituloMenu">
            <br />
            <br />
            <br />
            <br />            
            <p>SELECIONE O PAINEL DESEJADO</p>
            <br />
            <br />
        </div>

        <div class="btn-group btn-group-justified" role="group" aria-label="...">
            <div class="btn-group" role="group">
                <asp:Button id="Button0" OnClick="projetoClick" runat="server" Text="ELETROPAULO" tag="1" CssClass="btn btn-default"/>
            </div>
            <div class="btn-group" role="group">
                <asp:Button id="Button1" OnClick="projetoClick" runat="server" Text="ENELCE"  CssClass="btn btn-default"/>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
