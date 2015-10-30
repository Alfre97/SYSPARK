﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Vehicle.aspx.cs" Inherits="SYSPARK.AddNewCar" %>

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

        <div id="allContentInVehicle">
            <br />
            <input type="button" id="buttonErrors" runat="server" visible="false"/>
            <br />
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
                        <input type="hidden" runat="server" id="hiddenTypeValue" />
                        <select id="selectType" runat="server" onchange="setValue('selectType', 'hiddenTypeValue')">
                        </select>
                    </td>
                </tr>
                <tr>
                    <td>
                        <button onserverclick=" ButtonVehicle_Click" type="button" id="buttonAddNewCar" runat="server">Add vehicle</button>
                        <br />
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
