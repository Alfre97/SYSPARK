<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="History.aspx.cs" Inherits="SYSPARK.History" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>History</title>
    <link href="assets/favicon.ico.ico" type="image/x-icon" rel="shorcut icon" />
    <meta name="viewport" content="initial-scale=1, maximum-scale=1" />
    <!-- JQuery Library -->
    <script src="App_Utility/jquery.js"></script>
    <!-- Bootstrap -->
    <link rel="stylesheet" href="App_Utility/bootstrap.min.css" />
    <script src="App_Utility/bootstrap.min.js"></script>
    <!-- CSS -->
    <link rel="stylesheet" type="text/css" href="assets/css/history.css" />
</head>
<body>
    <form id="formHistory" runat="server">
        <div class="nav navbar-default" id="navDefault" role="navigation">
            <div class="container">
                <div class="navbar-header">
                    <a class="navbar-brand" id="navbar-brand" href="Home.aspx">
                        <!-- Company and name logo -->
                        <span>
                            <img src="assets/img/LogoSYSPARK.jpg" height="50" style="border-radius: 5px 5px;" />
                            <span class="logo">| SYSPARK | Home | History |</span>
                        </span>
                    </a>
                </div>
            </div>
        </div>
        <br />
        <h1>History</h1>
        <br />
        <div class="table-responsive">
            <table id="tableGray">
                <tr>
                    <td>
                        <table class="table" id="tableHistory">
                            <tr>
                                <td>
                                    <input type="hidden" id="hiddenCampusToViewValue" runat="server" />
                                    <select id="selectCampusToView" runat="server" onchange="setValue('selectCampusToView', 'hiddenCampusToViewValue')" onserverchange=" FillTable"></select>
                                    <asp:PlaceHolder ID="placeHolderTableHistory" runat="server"></asp:PlaceHolder>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
