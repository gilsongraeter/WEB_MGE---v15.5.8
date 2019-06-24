<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GoogleMapsMarcardor.aspx.cs" Inherits="GoogleMapsAPItesting.GoogleMapsMarcardor" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<title>Google Maps Markers</title>
<script type="text/javascript"
src="https://maps.googleapis.com/maps/api/js?key=AIzaSyCYbfpYyLzUZml_kR1LAaGB4-EaZRJHeKM&sensor=false">
</script>

<script type="text/javascript">
function initialize() {



}
</script>
</head>
<body onload="initialize()">
<form id="WebForm1" runat="server">
<div id="mapArea" style="width: 500px; height: 500px;">
</div>


<asp:Literal ID="Literal1" runat="server"></asp:Literal>
</form>
</body>
</html>