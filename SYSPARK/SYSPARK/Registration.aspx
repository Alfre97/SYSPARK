<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Registration.aspx.cs" Inherits="SYSPARK.Register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Registration</title>
    <link href="assets/favicon.ico.ico" type="image/x-icon" rel="shorcut icon" />
    <meta name="viewport" content="initial-scale=1, maximum-scale=1" />
    <!-- JQuery Library -->
    <script src="App_Utility/jquery.js"></script>
    <!-- Bootstrap -->
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap.min.css" />
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/js/bootstrap.min.js"></script>
    <!-- CSS -->
    <link rel="stylesheet" type="text/css" href="assets/css/registration.css" />
</head>
<script type="text/javascript" src="assets/js/Registration.js"></script>
<body>
    <form id="Registration" runat="server">
        <div class="Header">
            <div class="navbar default-navbar navbar-static-top" role="navigation">
                <div class="container">
                    <div class="navbar-header">
                        <a class="navbar-brand" id="navbar-brand" href="Registration.aspx">
                            <!-- Company and name logo -->
                            <span>
                                <img src="assets/img/LogoSYSPARK.jpg" height="50" style="border-radius: 5px 5px;" />
                                <span class="logo">SYSPARK > Registration</span>
                            </span>
                        </a>
                        <label id="info">
                            Note: If want to register your acount like a teacher or administrative,
                            <br />
                            you have have to ask for an special code to validate it.</label>
                    </div>
                </div>
            </div>
        </div>
        <br />
        <br />
        <br />
        <div id="allContentInRegistration">
            <br />
            <input type="button" id="buttonErrors" runat="server" visible="false"/>
            <br />
            <h1>Registration</h1>
            <br />
            <table id="tableRegistration" border="0">
                <tr>
                    <td>
                        <input type="text" id="textboxName" placeholder=" Name" runat="server" />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td>
                        <input type="text" id="textboxLastName" placeholder=" Last name" runat="server" />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td>
                        <input type="text" id="textboxUsernameR" placeholder=" User name" runat="server" />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td>
                        <input type="password" id="textboxPasswordR" placeholder=" Password" runat="server" />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td>
                        <input id="hiddenConditionValue" type="hidden" runat="server" value="1" />
                        <select id="selectCondition" runat="server" onchange="setValue('selectCondition', 'hiddenConditionValue')">
                        </select>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td>
                        <input type="hidden" id="hiddenVisibleValue" runat="server" value="0"/>
                        <input type="password" id="textboxCode" placeholder=" University card" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <input id="hiddenTransaction" type="hidden" runat="server" />
                        <button onserverclick=" buttonRegister_Click" type="button" id="buttonRegister" runat="server">Register</button>
                        <br />
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
