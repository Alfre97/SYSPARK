<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="SYSPARK.Home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Home SYSPARK</title>
    <!-- Bootstrap -->
    <link rel="stylesheet" type="text/css" href="assets/css/bootstrap.min.css" />
    <link rel="stylesheet" type="text/css" href="assets/css/jasny-bootstrap.min.css" />

    <!-- Responsive Style -->
    <link rel="stylesheet" type="text/css" href="assets/css/responsive.css" />
    <link rel="stylesheet" type="text/css" href="assets/css/home.css" />

</head>
<body>
    <nav class="navbar navbar-default" role="navigation" id="navbardefault">
        <!-- El logotipo y el icono que despliega el menú se agrupan
       para mostrarlos mejor en los dispositivos móviles -->
        <div class="navbar-header">
            <nav class="navbar-left navbar-brand">
                <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-ex1-collapse" style="background-color: white">
                <span class="sr-only">Toggle Navigation</span>
                <span class="icon-bar" style="background-color: darkgray"></span>
                <span class="icon-bar" style="background-color: darkgray"></span>
                <span class="icon-bar" style="background-color: darkgray"></span>
            </button>
            </nav>
            <nav class="navbar-brand">
                <span style="color: white;">
                <img class="logo" src="assets/img/LogoSYSPARK.jpg" height="50" />
                Sistema de control y solicitud de parqueo
            </span>
            </nav>        
        </div>

        <!-- Agrupar los enlaces de navegación, los formularios y cualquier
       otro elemento que se pueda ocultar al minimizar la barra -->
        <div class="collapse navbar-collapse navbar-ex1-collapse">

            <form class="navbar-form navbar-right" role="search">
                    <input type="text" placeholder="Search" id="TextBoxSearch"/>
                <button type="button" class="btnSearch"><img src="assets/img/LogoSearch.jpg" style="height:20px;" /></button>
            </form>

            <form class="navbar-form navbar-right" role="navigation">
                <ul class="nav navbar-nav" id="navbarcenter">
                    <li>
                        <button type="button" id="btnProfile" onmousemove="ToGrayOnFocus(this.id)" onmouseout="ToWhiteOnBlur(this.id)">
                            Profile</button></li>

                    <script>
                        function ToGrayOnFocus(x) {
                            document.getElementById(x).style.backgroundColor = "white";
                            document.getElementById(x).style.color = "darkgray";
                        }
                        function ToWhiteOnBlur(x) {
                            document.getElementById(x).style.backgroundColor = "transparent";
                            document.getElementById(x).style.color = "white";
                        }
                    </script>

                    <li><a id="divider"></a></li>
                    <li>
                        <button type="button" id="btnConfiguration" onmousemove="ToGrayOnFocus(this.id)" onmouseout="ToWhiteOnBlur(this.id)">
                            Configuration</button></li>
                </ul>
            </form>
        </div>
    </nav>
</body>
</html>
