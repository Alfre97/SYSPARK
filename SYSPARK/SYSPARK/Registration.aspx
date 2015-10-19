<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Registration.aspx.cs" Inherits="SYSPARK.Register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Registration</title>
    <link href="assets/favicon.ico.ico" type="image/x-icon" rel="shorcut icon" />
    <meta name="viewport" content="initial-scale=1, maximum-scale=1" />
    <!-- JQuery Library -->
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.3/jquery.min.js"></script>
    <!-- Bootstrap -->
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap.min.css" />
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/js/bootstrap.min.js"></script>
    <!-- CSS -->
    <link rel="stylesheet" type="text/css" href="assets/css/registration.css" />
</head>
<body>
    <form id="Registration" runat="server">
        <div class="Header">
            <div class="navbar default-navbar navbar-static-top" role="navigation">
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
        </section>
        <div id="allContentInRegistration">
            <h1>Registration</h1>
            <br />
            <table id="tableRegistration" border="0">
                <tr>
                    <td>
                        <input type="text" id="textboxName" placeholder=" Name" />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td>
                        <input type="text" id="textboxLastName" placeholder=" Last name" />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td>
                        <input type="text" id="textboxUsername" placeholder=" User name" />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td>
                        <input type="text" id="textboxPassword" placeholder=" Password" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <input type="text" id="textboxVehicle" placeholder=" Vehicle" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <button type="button" id="buttonRegister">Register</button>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
