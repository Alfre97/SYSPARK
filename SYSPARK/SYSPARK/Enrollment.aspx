<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Enrollment.aspx.cs" Inherits="SYSPARK.Enrollment" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Enrollment</title>
    <link href="assets/favicon.ico.ico" type="image/x-icon" rel="shorcut icon" />
    <meta name="viewport" content="initial-scale=1, maximum-scale=1" />
    <!-- JQuery Library -->
    <script src="App_Utility/jquery.js"></script>
    <!-- Bootstrap -->
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap.min.css" />
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/js/bootstrap.min.js"></script>
    <!-- CSS -->
    <link rel="stylesheet" type="text/css" href="assets/css/Vehicle.css" />
</head>
<body>
    <form id="formEnrollment" runat="server">
        <div class="Header">
            <div class="navbar default-navbar navbar-static-top" role="navigation">
                <div class="container">
                    <div class="navbar-header">
                        <a class="navbar-brand" id="navbar-brand" href="Home.aspx">
                            <!-- Company and name logo -->
                            <span>
                                <img src="assets/img/LogoSYSPARK.jpg" height="50" style="border-radius: 5px 5px;" />
                                <span class="logo">SYSPARK > Home > Profile > Vehicle</span>
                            </span>
                        </a>
                    </div>
                </div>
            </div>
        </div>

        <div id="allContentInEnrollment">
            <br />
            <input type="button" id="buttonErrors" runat="server" visible="false" />
            <br />
            <h1>Enrollment</h1>
            <br />
            <table id="tableEnrollment" border="0">
                <tr>
                    <td>
                        <br />
                        <input type="text" id="textboxLicense" runat="server" placeholder=" Lisence" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <br />
                        <input type="text" id="textboxParkingName" placeholder=" Parking name" runat="server" />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td>
                        <input type="text" id="textboxTotalSpace" placeholder=" Total space" runat="server" />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td>
                        <input type="text" id="textboxCarSpace" placeholder=" Car space" runat="server" />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td>
                        <input type="text" id="textboxMotorCycleSpace" placeholder=" Motorcycle space" runat="server" />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td>
                        <input type="text" id="textboxBusSpace" placeholder=" Bus space" runat="server" />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td>
                        <input type="text" id="textboxHandicapSpace" placeholder=" Handicap space" runat="server" />
                        <br />
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
