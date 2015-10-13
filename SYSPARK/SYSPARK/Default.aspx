<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SYSPARK.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login SYSPARK</title>
    <!-- JQuery Library -->
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.3/jquery.min.js"></script>

    <!-- Bootstrap -->
    <link rel="stylesheet" type="text/css" href="assets/css/bootstrap.min.css" />
    <link rel="stylesheet" type="text/css" href="assets/css/jasny-bootstrap.min.css" />

    <!-- Responsive Style -->
    <link rel="stylesheet" type="text/css" href="assets/css/responsive.css" />
    <link rel="stylesheet" type="text/css" href="assets/css/default.css" />
</head>
<body>
    <script>
        function click() {
            document.getElementById("hideButton").click();
        }
    </script>
    <!-- Navbar star -->
    <div class="Header">
        <div class="navbar default-navbar navbar-fixed-top" role="navigation">
            <div class="container">
                <div class="navbar-header">
                    <a class="navbar-brand" href="Default.aspx">
                        <span style="color: white;">
                            <img class="logo" src="assets/img/LogoSYSPARK.jpg" height="40" />
                            Sistema de control y solicitud de parqueo
                        </span>
                    </a>
                    <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-ex1-collapse" style="background-color: white">
                        <span class="sr-only">Toggle navigation</span>
                        <span class="icon-bar" style="background-color: darkgray"></span>
                        <span class="icon-bar" style="background-color: darkgray"></span>
                        <span class="icon-bar" style="background-color: darkgray"></span>
                    </button>
                </div>
            </div>
        </div>
    </div>

    <section>
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
    </section>
    <section class="Table">
        <h1>Access</h1>
        <br />
        <table border="0" class="nav-justified">
            <tr>
                <td>
                    <input id="textBoxUsername" type="text" class="TextBoxUsername" runat="server"
                        name="TextBoxUserName" placeholder="  Enter your username" style="color: black" />
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                </td>
            </tr>
            <tr>
                <td>
                    <input id="textBoxPassword" type="text" runat="server" class="TextBoxPassword"
                        name="TextBoxPassword" placeholder="  Enter your password"
                        style="color: black" />
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                </td>
            </tr>
            <tr>
                <td>
                    <button id="buttonEnter" runat="server" onclick="EnterClick()">
                        Enter</button>
                </td>
            </tr>
            <tr>
                <td>
                    <input type="button" class="buttonErrors" id="buttonErrors" runat="server" value="" onclick="click()" />
                    <asp:Button runat="server" ID="hideButton" Visible="false" OnClick="ButtonEnterOnClick"/>
                </td>
            </tr>
        </table>
    </section>
</body>
</html>
