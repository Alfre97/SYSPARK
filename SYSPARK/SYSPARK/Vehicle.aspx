<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Vehicle.aspx.cs" Inherits="SYSPARK.VehiclePage" %>

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
    <script type="text/javascript" src="assets/js/Vehicle.js"></script>
    <style type="text/css">
        .desmarcado {
            background: white;
        }

        .marcado {
            background: #629675;
        }
    </style>
</head>
<body>
    <form id="formVehicle" runat="server">
        <div class="Header">
            <div class="navbar navbar-default" id="navDefault" role="navigation">
                <div class="container">
                    <div class="navbar-header">
                        <a class="navbar-brand" id="navbar-brand" href="Home.aspx">
                            <!-- Company and name logo -->
                            <span>
                                <img src="assets/img/LogoSYSPARK.jpg" height="50" style="border-radius: 5px 5px;" />
                                <span class="logo">| SYSPARK | Home | Profile | Vehicle |</span>
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
                        <span>Select a Vehicle Type</span><br />
                        <input type="hidden" runat="server" id="hiddenTypeValue" />
                        <select id="selectType" runat="server" onchange="setValue('selectType', 'hiddenTypeValue')">
                        </select>
                    </td>
                </tr>
                <tr>
                    <td>
                        <button onserverclick=" ButtonVehicle_Click" type="button" id="buttonAddNewCar" runat="server">Add Vehicle</button>
                        <button onclick="clearingSomeControls()" type="button" id="buttonClear" runat="server">Clean</button>
                        <button onclick="cancelUpdate()" type="button" id="buttonCancelUpdate" runat="server">Cancel</button>
                        <button onserverclick=" Update_Click" type="button" id="buttonUpdate" runat="server">Update</button>
                        <br />
                    </td>
                </tr>
            </table>
            <br />
            <button onserverclick=" Delete_Click" type="button" id="buttonDelete" runat="server" disabled="disabled" style="background-color: transparent; border: none;" />
            <input type="hidden" id="hiddenVehiclePlate" runat="server"/>
            <div class="table-responsive">
                <table id="tableGray">
                    <tr>
                        <td>
                            <input type="button" runat="server" id="buttonInfoVehicleTable" value="Please, after any operation select one role!" />
                            <table class="table" id="tableVehicles">
                                <asp:PlaceHolder ID="placeHolderTableVehicle" runat="server"></asp:PlaceHolder>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
            <br />
            <br />
            <br />
        </div>
    </form>
</body>
</html>
