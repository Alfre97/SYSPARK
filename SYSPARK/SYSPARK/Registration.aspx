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
<script type="text/javascript" src="assets/js/Registration.js"></script>
</head>
<body>
    <form id="Registration" runat="server">
        <div class="Header">
            <div class="navbar default-navbar navbar-static-top" role="navigation">
                <div class="container">
                    <div class="navbar-header">
                        <a class="navbar-brand" id="navbar-brand" href="Home.aspx">
                            <!-- Company and name logo -->
                            <span>
                                <img src="assets/img/LogoSYSPARK.jpg" height="50" style="border-radius: 5px 5px;" />
                                <span class="logo">| SYSPARK | Home | Registration |</span>
                            </span>
                        </a>
                    </div>
                </div>
            </div>
        </div>
        <br />
        <div id="allContentInRegistration">
            <br />
            <input type="button" id="buttonErrors" runat="server" visible="false" disabled="disabled"/>
            <br />
            <h1>User</h1>
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
                        <input type="text" id="textboxUniversityCard" placeholder=" University card" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <span>Choose a Role</span><br />
                        <input id="hiddenConditionValue" type="hidden" runat="server" />
                        <select id="selectCondition" runat="server" onchange="setValue('selectCondition', 'hiddenConditionValue')"></select>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td>
                        <span>Choose a Campus</span><br />
                        <input id="hiddenCampusValue" type="hidden" runat="server" />
                        <select id="selectCampus" runat="server" onchange="setValue('selectCampus', 'hiddenCampusValue')"></select>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td>
                        <button onserverclick=" ButtonRegister_Click" type="button" id="buttonRegister" runat="server">Register</button>
                        <br />
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
