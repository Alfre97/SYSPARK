<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VehicleTypePage.aspx.cs" Inherits="SYSPARK.VehicleTypePage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <div id="google_translate_element"></div><script type="text/javascript">
function googleTranslateElementInit() {
  new google.translate.TranslateElement({pageLanguage: 'en', layout: google.translate.TranslateElement.InlineLayout.SIMPLE}, 'google_translate_element');
}
</script><script type="text/javascript" src="//translate.google.com/translate_a/element.js?cb=googleTranslateElementInit"></script>
    <title>Vehicle Type</title>
    <link href="assets/favicon.ico.ico" type="image/x-icon" rel="shorcut icon" />
    <meta name="viewport" content="initial-scale=1, maximum-scale=1" />
    <!-- JQuery Library -->
    <script src="App_Utility/jquery.js"></script>
    <!-- Bootstrap -->
    <link rel="stylesheet" href="App_Utility/bootstrap.css" />
    <link rel="stylesheet" href="App_Utility/bootstrap.min.css" />
    <script src="App_Utility/bootstrap.min.js"></script>
    <!-- CSS -->
    <link rel="stylesheet" type="text/css" href="assets/css/vehicleType.css" />
    <script type="text/javascript" src="assets/js/VehicleType.js"></script>
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
    <form id="formVehicleType" runat="server">
    <div class="Header">
            <div class="nav navbar-default" id="navDefault" role="navigation">
                <div class="container">
                    <div class="navbar-header">
                        <a class="navbar-brand" id="navbar-brand" href="Home.aspx">
                            <!-- Company and name logo -->
                            <span>
                                <img src="assets/img/LogoSYSPARK.jpg" height="50" style="border-radius: 5px 5px;" />
                                <span class="logo">| SYSPARK | Home | Vehicle Type |</span>
                            </span>
                        </a>
                    </div>
                </div>
            </div>
        </div>

        <div id="allContentInVehicleType">
            <br />
            <input type="button" id="buttonErrors" runat="server" visible="false"/>
            <br />
            <h1>Vehicle Type</h1>
            <br />
            <table id="tableVehicleType" border="0">
                <tr>
                    <td>
                        <br />
                        <input type="text" id="textboxVehicleType" runat="server" placeholder=" Vehicle type name" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <button onclick="clearingSomeControls()" type="button" id="buttonClear" runat="server">Clean</button>
                        <button onserverclick=" AddVehicleType_Click" type="button" id="buttonAddVehicleType" runat="server">Add Vehicle Type</button>
                        <button onclick="cancelUpdate()" type="button" id="buttonCancelUpdate" runat="server">Cancel</button>
                        <button onserverclick=" Update_Click" type="button" id="buttonUpdate" runat="server">Update</button>
                        <br />
                    </td>
                </tr>
            </table>
            <br />
            <button onserverclick=" Delete_Click" type="button" id="buttonDelete" runat="server" disabled="disabled" style="background-color: transparent; border: none;" />
            <input type="hidden" id="hiddenVehicleTypeId" runat="server"/>
            <div class="table-responsive">
                <table id="tableGray">
                    <tr>
                        <td>
                            <input type="button" runat="server" id="buttonInfoVehicleTypeTable" value="Please, after any operation select one vehicle type!" />
                            <table class="table" id="tableVehicleTypes">
                                <asp:PlaceHolder ID="placeHolderTableRole" runat="server"></asp:PlaceHolder>
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
