<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SYSPARK.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login SYSPARK</title>
    <!-- JQuery Library -->
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.3/jquery.min.js"></script>
    <!-- Bootstrap -->
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap.min.css" />
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/js/bootstrap.min.js"></script>
    <!-- CSS -->
    <link rel="stylesheet" type="text/css" href="assets/css/default.css" />
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
                                <img class="logo" src="assets/img/LogoSYSPARK.jpg" height="40" style="border-radius: 5px 5px;" />
                                SYSPARK
                            </span>
                        </a>
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
        <div id="AllContentInLogin">
            <h1>Access</h1>
            <br />
            <table border="0" id="tableLogin">
                <tr>
                    <td>
                        <br />
                        <br />
                    </td>
                </tr>
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
                        <input id="textBoxPassword" type="password" runat="server" class="TextBoxPassword"
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
                        <input type="button" id="buttonErrors" runat="server" value="" />
                    </td>
                </tr>
            </table>
            <label>@Copyright 2015 - Universidad Latina</label><br />
            <label>Todos los derechos reservados</label><br />
        </div>
    </form>
</body>
</html>
