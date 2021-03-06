﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SYSPARK.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <div id="google_translate_element"></div><script type="text/javascript">
function googleTranslateElementInit() {
  new google.translate.TranslateElement({pageLanguage: 'en', layout: google.translate.TranslateElement.InlineLayout.SIMPLE}, 'google_translate_element');
}
</script><script type="text/javascript" src="//translate.google.com/translate_a/element.js?cb=googleTranslateElementInit"></script>
    <title>Login</title>
    <link href="assets/favicon.ico.ico" type="image/x-icon" rel="shorcut icon" />
    <meta name="viewport" content="initial-scale=1, maximum-scale=1" />
    <!-- JQuery Library -->
    <script src="App_Utility/jquery.js"></script>
    <!-- Bootstrap -->
    <link rel="stylesheet" href="App_Utility/bootstrap.css" />
    <link rel="stylesheet" href="App_Utility/bootstrap.min.css" />
    <script src="App_Utility/bootstrap.min.js"></script>
    <!-- CSS -->
    <link rel="stylesheet" type="text/css" href="assets/css/default.css" />
    <script type="text/javascript" src="assets/js/Default.js"></script>
</head>
<body>
    <form runat="server">
        <!-- Navbar star -->
        <div class="Header">
            <div class="navbar navbar-default" id="navDefault" role="navigation">
                <div class="container">
                    <div class="navbar-header">
                        <a class="navbar-brand" id="navbar-brand" href="Default.aspx">
                            <!-- Company and name logo -->
                            <span>
                                <img src="assets/img/LogoSYSPARK.jpg" height="50" style="border-radius: 5px 5px;" />
                                <span class="logo">| SYSPARK | Login |</span>
                            </span>
                        </a>
                    </div>
                </div>
            </div>
        </div>
        <div id="allContentInLogin">
            <br />
            <input type="button" id="buttonErrors" runat="server" visible="false" disabled="disabled" />
            <br />
            <h1>Access</h1>
            <br />
            <table border="0" id="tableLogin">
                <tr>
                    <td>
                        <br />
                        <img id="imgUlatinaLogo" src="assets/img/UlatinaLogo.png" />
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
                        <button id="buttonClear" type="button" onclick="clearingSomeControls()">Clean</button>
                        <button onserverclick=" EnterButton_Click" id="buttonEnter" type="submit" runat="server">Enter</button>
                        <br />
                        <br />
                    </td>
                </tr>
            </table>
            <label>@Copyright 2015 - Universidad Latina</label><br />
            <label>All rights reserved</label><br />
        </div>
    </form>
</body>
</html>
