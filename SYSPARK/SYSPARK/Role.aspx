﻿-<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Role.aspx.cs" Inherits="SYSPARK.RolePage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <div id="google_translate_element"></div><script type="text/javascript">
function googleTranslateElementInit() {
  new google.translate.TranslateElement({pageLanguage: 'en', layout: google.translate.TranslateElement.InlineLayout.SIMPLE}, 'google_translate_element');
}
</script><script type="text/javascript" src="//translate.google.com/translate_a/element.js?cb=googleTranslateElementInit"></script>
    <title>Role</title>
    <link href="assets/favicon.ico.ico" type="image/x-icon" rel="shorcut icon" />
    <meta name="viewport" content="initial-scale=1, maximum-scale=1" />
    <!-- JQuery Library -->
    <script src="App_Utility/jquery.js"></script>
    <!-- Bootstrap -->
    <link rel="stylesheet" href="App_Utility/bootstrap.css" />
    <link rel="stylesheet" href="App_Utility/bootstrap.min.css" />
    <script src="App_Utility/bootstrap.min.js"></script>
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
    <form id="formRole" runat="server">
            <div class="nav navbar-default" id="navDefault" role="navigation">
                <div class="container">
                    <div class="navbar-header">
                        <a class="navbar-brand" id="navbar-brand" href="Home.aspx">
                            <!-- Company and name logo -->
                            <span>
                                <img src="assets/img/LogoSYSPARK.jpg" height="50" style="border-radius: 5px 5px;" />
                                <span class="logo">| SYSPARK | Home | Role |</span>
                            </span>
                        </a>
                    </div>
                </div>
            </div>
        <div id="allContentInRole">
            <br />
            <input type="button" id="buttonErrors" runat="server" visible="false"/>
            <br />
            <h1>Role</h1>
            <br />
            <table id="tableRole" border="0">
                <tr>
                    <td>
                        <br />
                        <input type="text" id="textboxRole" runat="server" placeholder=" Role name" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <button onclick="clearingSomeControls()" type="button" id="buttonClear" runat="server">Clean</button>
                        <button onserverclick=" AddRole_Click" type="button" id="buttonAddRole" runat="server">Add role</button>
                        <button onclick="cancelUpdate()" type="button" id="buttonCancelUpdate" runat="server">Cancel</button>
                        <button onserverclick=" Update_Click" type="button" id="buttonUpdate" runat="server">Update</button>
                        <br />
                    </td>
                </tr>
            </table>
            <br />
            <button onserverclick=" Delete_Click" type="button" id="buttonDelete" runat="server" disabled="disabled" style="background-color: transparent; border: none;" />
            <input type="hidden" id="hiddenRoleId" runat="server"/>
            <div class="table-responsive">
                <table id="tableGray">
                    <tr>
                        <td>
                            <input type="button" runat="server" id="buttonInfoRoleTable" value="Please, after any operation select one role!" />
                            <table class="table" id="tableRoles">
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
