﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="SYSPARK.Home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Home SYSPARK</title>
    <link href="assets/favicon.ico.ico" type="image/x-icon" rel="shorcut icon" />
    <meta name="viewport" content="initial-scale=1, maximum-scale=1" />
    <!-- JQuery Library -->
    <script src="App_Utility/jquery.js"></script>
    <!-- Bootstrap -->
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap.min.css" />
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/js/bootstrap.min.js"></script>
    <!-- CSS -->
    <link rel="stylesheet" type="text/css" href="assets/css/home.css" />
    <script src="assets/js/HomeMenu.js"></script>

</head>
<body>
    <form runat="server">
        <!-- Navbar -->
        <div class="nav navbar-default" id="navDefault" role="navigation">
            <div class="container" id="containerNavbar-top">
                <nav class="navbar-header" id="navbar-top-header">
                    <a class="navbar-brand" id="navbar-brand"  href="Home.aspx" >
                        <!-- Company and name logo -->
                        <span>
                            <img src="assets/img/LogoSYSPARK.jpg" height="50" style="border-radius: 5px 5px;" />
                            <span class="logo">| SYSPARK | Home |</span>
                        </span>
                    </a>
                    <!-- TextBox and Button Search -->
                    <div class="nav" id="search">
                        <input type="text" placeholder=" Search" id="TextBoxSearch" />
                        <button type="button" id="buttonSearch" runat="server">
                            <img id="imageSearch" src="assets/img/LogoSearch.jpg" /></button>
                    </div>
                </nav>
            </div>
        </div>

        <!-- Table home -->
        <div>
            <table id="tableHome">
                <tr>
                    <td>
                        <br />
                        <br />
                    </td>
                </tr>
                <tr id="trReservation" runat="server">
                    <td>
                        <button type="button" id="buttonParkReservation" onclick="location.href='ParkingReservation.aspx'" runat="server">Reservation</button>
                    </td>
                    <td>
                        <button type="button" id="buttonMyProfile" onclick="location.href='Profile.aspx'" runat="server">My Profile</button>
                        <br />
                    </td>
                </tr>
                <tr id="trReports" runat="server">
                    <td>
                        <button type="button" id="buttonReports" onclick="location.href='Reports.aspx'" runat="server">Reports</button>
                    </td>
                    <td>
                        <button type="button" id="buttonHistory" onclick="location.href='History.aspx'" runat="server">History</button>
                        <br />
                    </td>
                </tr>
                <tr id="trUser" runat="server">
                    <td>
                        <button type="button" id="buttonUser" onclick="location.href='Registration.aspx'" runat="server">User</button>
                    </td>
                    <td>
                        <button type="button" id="buttonParking" onclick="location.href='Parking.aspx'" runat="server">Parking</button>
                        <br />
                    </td>
                </tr>
                <tr id="trCampus" runat="server">
                    <td>
                        <button type="button" id="buttonCampus" onclick="location.href='CampusPage.aspx'" runat="server">Campus</button>
                    </td>
                    <td>
                        <button type="button" id="buttonRole" onclick="location.href='Role.aspx'" runat="server">Role</button>
                        <br />
                    </td>
                </tr>
                <tr id="trVehicleType" runat="server">
                    <td>
                        <button type="button" id="buttonVehicleType" onclick="location.href='VehicleTypePage.aspx'" runat="server">Vehicle Type</button>
                    </td>
                    <td>
                        <button type="button" id="buttonLapse" onclick="location.href='LapsePage.aspx'" runat="server">Lapse</button>
                        <br />
                    </td>
                </tr>
                <tr id="trEnrollment" runat="server">
                    <td>
                        <button type="button" id="buttonEnrollment" onclick="location.href='EnrollmentPage.aspx'" runat="server">Enrollment</button>
                    </td>
                    <td>
                        <button type="button" id="buttonConfiguration" runat="server">Configuration</button>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td>
                        <br />
                        <br />
                        <br />
                        <br />
                    </td>
                </tr>
            </table>
        </div>

        <div class="navbar-nav navbar-fixed-bottom" id="navbarBottom">
            <div class="container">
                <nav class="navbar-header" id="navbar-header-bottom">
                    <label id="options">Options</label>
                    <button onclick="main()" type="button" id="buttonNavToogleOptions" class="navbar-toggle" data-toggle="collapse" data-target="#navbar-right">
                        <span class="sr-only">Toggle Navigation</span>
                        <span class="icon-bar" style="background-color: #3d3737"></span>
                        <span class="icon-bar" style="background-color: #3d3737"></span>
                        <span class="icon-bar" style="background-color: #3d3737"></span>
                    </button>

                    <div id="navbar-right">
                        <!-- Options of navbar-header-bottom -->
                        <nav id="navbarFromRight">
                            <ul>
                                <li>
                                    <button onserverclick=" buttonLogout_Click" type="button" id="buttonLogout" runat="server">Log out</button>
                                </li>
                                <li>
                                    <button type="button" id="buttonInfo" runat="server">Info</button>
                                </li>
                            </ul>
                        </nav>
                    </div>
                </nav>
            </div>
        </div>
    </form>
</body>
</html>
