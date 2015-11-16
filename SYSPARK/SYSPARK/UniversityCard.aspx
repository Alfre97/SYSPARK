<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UniversityCard.aspx.cs" Inherits="SYSPARK.Code" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>University Card</title>
    <link href="assets/favicon.ico.ico" type="image/x-icon" rel="shorcut icon" />
    <meta name="viewport" content="initial-scale=1, maximum-scale=1" />
    <!-- JQuery Library -->
    <script src="App_Utility/jquery.js"></script>
    <!-- Bootstrap -->
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap.min.css" />
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/js/bootstrap.min.js"></script>
    <!-- CSS -->
    <link rel="stylesheet" type="text/css" href="assets/css/role.css" />
    <script type="text/javascript" src="assets/js/Role.js"></script>
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
    <form id="formUniversityCard" runat="server">
    <div class="Header">
            <div class="navbar default-navbar navbar-static-top" role="navigation">
                <div class="container">
                    <div class="navbar-header">
                        <a class="navbar-brand" id="navbar-brand" href="Home.aspx">
                            <!-- Company and name logo -->
                            <span>
                                <img src="assets/img/LogoSYSPARK.jpg" height="50" style="border-radius: 5px 5px;" />
                                <span class="logo">SYSPARK > Home > University Card</span>
                            </span>
                        </a>
                    </div>
                </div>
            </div>
        </div>
        <div id="allContentInUniversityCard">
            <br />
            <input type="button" id="buttonErrors" runat="server" visible="false"/>
            <br />
            <h1>Vehicle</h1>
            <br />
            <table id="tableUniversityCard" border="0">
                <tr>
                    <td>
                        <br />
                        <input type="text" id="textboxUniversityCard" runat="server" placeholder=" University card" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <button onclick="clearingSomeControls()" type="button" id="buttonClear" runat="server">Clean</button>
                        <button onserverclick=" AddRole_Click" type="button" id="buttonAddUniversityCard" runat="server">Add university card</button>
                        <button onserverclick=" Update_Click" type="button" id="buttonUpdate" runat="server">Update</button>
                        <br />
                    </td>
                </tr>
            </table>
            <br />
            <button onserverclick=" Delete_Click" type="button" id="buttonDelete" runat="server" disabled="disabled" style="background-color: transparent; border: none;" />
            <input type="hidden" id="hiddenUniversityCardId" runat="server"/>
            <div class="table-responsive">
                <table id="tableGray">
                    <tr>
                        <td>
                            <input type="button" runat="server" id="buttonInfoUniversityCardTable" value="Please, after any operation select one role!" />
                            <table class="table" id="tableUniversityCards">
                                <asp:PlaceHolder ID="placeHolderTableUniversityCard" runat="server"></asp:PlaceHolder>
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
