<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Vehicle.aspx.cs" Inherits="SYSPARK.AddNewCar" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Vehicle</title>
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
    <form id="formVehicle" runat="server">
    <div class="Header">
            <div class="navbar default-navbar navbar-static-top" role="navigation">
                <div class="container">
                    <div class="navbar-header">
                        <a class="navbar-brand" href="Default.aspx">
                            <span style="color: white;">
                                <img class="logo" src="assets/img/LogoSYSPARK.jpg" height="40" style="border-radius: 5px 5px;" />
                                SYSPARK > Home > My Profile > Vehicle
                            </span>
                        </a>
                    </div>
                </div>
            </div>
        </div>

        <div id="allContentInVehicle">
            <h1>Vehicle</h1>
            <br />
            <table id="tableVehicle" border="0">
                <tr>
                    <td>
                        <br />
                        <input type="text" id="textboxLicense" runat="server" placeholder=" Lisence" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <select id="selectType" runat="server">
                        </select>
                    </td>
                </tr>
                <tr>
                    <td>
                        <button type="button" id="buttonAddNewCar" runat="server" onclick="return confirm('Sure?') && !" onserverclick="ButtonVehicle_Click">Add vehicle</button>
                    </td>
                </tr>
                <tr>
                    <td>
                        <input type="button" id="buttonErrors" runat="server" value="" />
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
