<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="SYSPARK.Profile" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <title>My Profile</title>
    <link href="assets/favicon.ico.ico" type="image/x-icon" rel="shorcut icon" />
    <meta name="viewport" content="initial-scale=1, maximum-scale=1" />
    <!-- JQuery Library -->
    <script src="App_Utility/jquery.js"></script>
    <!-- Bootstrap -->
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap.min.css" />
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/js/bootstrap.min.js"></script>
    <!-- CSS -->
    <link rel="stylesheet" type="text/css" href="assets/css/profile.css" />
    <script type="text/javascript" src="assets/js/Profile.js"></script>
</head>
<body>
    <form id="formProfile" runat="server">
        <div class="Header">
            <div class="navbar default-navbar navbar-static-top" role="navigation">
                <div class="container">
                    <div class="navbar-header">
                        <a class="navbar-brand" id="navbar-brand" href="Home.aspx">
                            <!-- Company and name logo -->
                            <span>
                                <img src="assets/img/LogoSYSPARK.jpg" height="50" style="border-radius: 5px 5px;" />
                                <span class="logo">SYSPARK > Home > Profile</span>
                            </span>
                        </a>
                    </div>
                </div>
            </div>
        </div>

        <div id="allContentInProfile">
            <input type="button" id="buttonErrors" runat="server" visible="false" />
            <h1>Profile</h1>
            <br />
            <table id="tableProfile" border="0">
                <tr>
                    <td>
                        <br />
                        <span>Name:</span><br />
                        <input type="text" id="textboxName" runat="server" placeholder=" Name" disabled="disabled" />
                    </td>
                </tr>
                <tr>
                    <td><span>Last name:</span><br />
                        <input type="text" id="textboxLastName" runat="server" placeholder=" Last name" disabled="disabled" />
                    </td>
                </tr>
                <tr>
                    <td><span>Username:</span><br />
                        <input type="text" id="textboxUsername" runat="server" placeholder=" User name" disabled="disabled" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <span>Password:</span><br />
                        <input type="text" id="textboxPasswordShowed" runat="server" placeholder=" Password" disabled="disabled" />
                    </td>
                </tr>
                 <tr>
                    <td>
                        <span>University Card:</span><br />
                        <input type="text" id="textboxUniversityCard" placeholder=" University card" runat="server" disabled="disabled" />
                        <br />
                    </td>
                </tr>
                <tr id="trVehicle" runat="server">
                    <td>
                        <span>Vehicle:</span><br />
                        <select id="selectVehicle" runat="server">
                        </select>
                    </td>
                </tr>
                <tr>
                    <td>
                        <input type="hidden" id="hiddenConditionValue" runat="server" />
                        <span>Role:</span><br />
                        <select id="selectCondition" runat="server" disabled="disabled" onchange="setValue('selectCondition', 'hiddenConditionValue')">
                        </select>
                    </td>
                </tr>
                <tr>
                    <td>
                        <input type="hidden" id="hiddenCampusValue" runat="server" />
                        <span>Role:</span><br />
                        <select id="selectCampus" runat="server" disabled="disabled" onchange="setValue('selectCampus', 'hiddenCampusValue')">
                        </select>
                    </td>
                </tr>
                <tr  id="trFirstOptions" runat="server">
                    <td>
                        <button type="button" id="buttonAddNewCar" runat="server" onclick="location.href='Vehicle.aspx'">Add Vehicle</button>
                        <button onserverclick=" ButtonUpdateMyInfo_Click" type="button" id="buttonUpdateMyInfo" runat="server">Update</button>
                        <br />
                        <br />
                    </td>
                </tr>
                <tr runat="server" id="trUpdate" visible="false">
                    <td>
                        <input type="hidden" id="hiddenUpdate" runat="server" />
                        <button onserverclick=" ButtonCancelUpdate_Click" type="button" id="buttonCancelUpdate" runat="server">Cancel</button>
                        <button onserverclick=" ButtonUpdate_Click" type="button" id="buttonUpdate" runat="server">Update</button>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
