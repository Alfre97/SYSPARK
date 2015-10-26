<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Parking.aspx.cs" Inherits="SYSPARK.Paking" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Parking</title>
    <link href="assets/favicon.ico.ico" type="image/x-icon" rel="shorcut icon" />
    <meta name="viewport" content="initial-scale=1, maximum-scale=1" />
    <!-- JQuery Library -->
    <script src="App_Utility/jquery.js"></script>
    <!-- Bootstrap -->
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap.min.css" />
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/js/bootstrap.min.js"></script>
    <!-- CSS -->
    <link rel="stylesheet" type="text/css" href="assets/css/parking.css" />
</head>
<body>
    <form id="formParking" runat="server">
        <div class="Header">
            <div class="navbar default-navbar navbar-static-top" role="navigation">
                <div class="container">
                    <div class="navbar-header">
                        <a class="navbar-brand" href="Home.aspx">
                            <span style="color: white;">
                                <img class="logo" src="assets/img/LogoSYSPARK.jpg" height="40" style="border-radius: 5px 5px;" />
                                SYSPARK > Registration
                            </span>
                        </a>
                        <label id="info">
                            Note: It is recommended that the name of the parking can be a letter of the alphabet.</label>
                    </div>
                </div>
            </div>
        </div>
        <br />
        <br />
        <br />
        <div id="errors">
            <input type="button" id="buttonErrors" runat="server" value="" visible="false" />
        </div>
        <div id="allContentInParking">
            <h1>Parking</h1>
            <br />
            <table id="tableParking" border="0">
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
                <tr>
                    <td>
                        <button onserverclick=" AddParking_Click" type="button" id="buttonAddParking" runat="server">Add parking</button>
                        <br />
                        <br />
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
