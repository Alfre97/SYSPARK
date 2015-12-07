e<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CampusPage.aspx.cs" Inherits="SYSPARK.CampusPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<title>Campus</title>
    <link href="assets/favicon.ico.ico" type="image/x-icon" rel="shorcut icon" />
    <meta name="viewport" content="initial-scale=1, maximum-scale=1" />
    <!-- JQuery Library -->
    <script src="App_Utility/jquery.js"></script>
    <!-- Bootstrap -->
    <link rel="stylesheet" href="App_Utility/bootstrap.min.css" />
    <script src="App_Utility/bootstrap.min.js"></script>
    <!-- CSS -->
    <link rel="stylesheet" type="text/css" href="assets/css/campus.css" />
    <script type="text/javascript" src="assets/js/Campus.js"></script>
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
    <form id="formCampus" runat="server">
            <div class="nav navbar-default" id="navDefault" role="navigation">
                <div class="container">
                    <div class="navbar-header">
                        <a class="navbar-brand" id="navbar-brand" href="Home.aspx">
                            <!-- Company and name logo -->
                            <span>
                                <img src="assets/img/LogoSYSPARK.jpg" height="50" style="border-radius: 5px 5px;" />
                                <span class="logo">| SYSPARK | Home | Campus |</span>
                            </span>
                        </a>
                    </div>
                </div>
            </div>

        <div id="allContentInCampus">
            <br />
            <input type="button" id="buttonErrors" runat="server" visible="false" />
            <br />
            <h1>Campus</h1>
            <br />
            <table id="tableCampus" border="0">
                <tr>
                    <td>
                        <br />
                        <input type="text" id="textboxCampus" runat="server" placeholder=" Campus name" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <button onclick="clearingSomeControls()" type="button" id="buttonClear" runat="server">Clean</button>
                        <button onserverclick="AddCampus_Click" type="button" id="buttonAddCampus" runat="server">Add Campus</button>
                        <button onserverclick=" Update_Click" type="button" id="buttonUpdate" runat="server">Update</button>
                        <br />
                    </td>
                </tr>
            </table>
            <br />
            <button onserverclick=" Delete_Click" type="button" id="buttonDelete" runat="server" disabled="disabled" style="background-color: transparent; border: none;" />
            <input type="hidden" id="hiddenCampusId" runat="server" />
            <div class="table-responsive">
                <table id="tableGray">
                    <tr>
                        <td>
                            <input type="button" runat="server" id="buttonInfoCampusTable" value="Please, after any operation select one campus!" />
                            <table class="table" id="tableCampuses">
                                <asp:PlaceHolder ID="placeHolderTableCampus" runat="server"></asp:PlaceHolder>
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

