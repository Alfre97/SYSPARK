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

    <script>
        function getTextBoxValue() {
            document.getElementById("buttonErrors").value = document.getElementById("textBoxUsername").value;
            
        }
    </script>
</head>
<body>
    <form runat="server">
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
        <div class="Table">
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
                        <button id="buttonEnter" type="submit" runat="server" onclick="return confirm('Sure?') && !" onserverclick="enterButton_Click">
                            Enter</button>
                    </td>
                </tr>
                <tr>
                    <td>
                        <input type="button" class="buttonErrors" id="buttonErrors" runat="server" value="" />
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
