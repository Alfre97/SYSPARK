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
    <script>
</script>
</head>
<body>
    <form id="formProfile" runat="server">
        <div class="Header">
            <div class="navbar default-navbar navbar-static-top" role="navigation">
                <div class="container">
                    <div class="navbar-header">
                        <a class="navbar-brand" id="navbar-toggle" href="Default.aspx">
                            <span style="color: white;">
                                <img class="logo" src="assets/img/LogoSYSPARK.jpg" height="40" style="border-radius: 5px 5px;" />
                                SYSPARK > Home > My Profile
                            </span>
                        </a>
                    </div>
                </div>
            </div>
        </div>

        <div id="allContentInProfile">
            <h1>Profile</h1>
            <br />
            <table id="tableProfile" border="0">
                <tr>
                    <td><br />
                        <a>Name:</a><br />
                        <input type="text" id="textboxName" runat="server" placeholder=" Name" disabled="disabled" />
                    </td>
                </tr>
                <tr>
                    <td><a>Lastname:</a><br />
                        <input type="text" id="textboxLastName" runat="server" placeholder=" Last name" disabled="disabled" />
                    </td>
                </tr>
                <tr>
                    <td><a>Username:</a><br />
                        <input type="text" id="textboxUsername" runat="server" placeholder=" User name" disabled="disabled" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <a>Password:</a><br />
                        <input type="text" id="textboxPasswordShowed" runat="server" placeholder=" Password" disabled="disabled" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <a>Vehicle:</a><br />
                        <select id="selectVehicle" runat="server">
                        </select>
                    </td>
                </tr>
                <tr>
                    <td>
                        <a>Condition:</a><br />
                        <select id="selectCondition" runat="server" disabled="disabled">
                        </select>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td>
                        <button type="button" id="buttonAddNewCar" runat="server" onclick="location.href='Vehicle.aspx'">Vehicle</button>
                        <button onserverclick=" ButtonUpdateMyInfo_Click" type="button" id="buttonUpdateMyInfo" runat="server">Update my info</button>
                    </td>
                </tr>
                <tr>
                    <td>
                        <button onserverclick=" ButtonUpdate_Click" type="button" id="buttonUpdate" runat="server" visible="false">
                            Update</button>
                    </td>
                </tr>
                <tr>
                    <td>
                        <input type="button" id="buttonErrors" runat="server" value="" visible="false" />
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
