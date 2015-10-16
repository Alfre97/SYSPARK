<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="SYSPARK.Home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Home SYSPARK</title>
    <meta name="viewport" content="initial-scale=1, maximum-scale=1" />
    <!-- JQuery Library -->
    <script src="http://code.jquery.com/jquery-latest.js"></script>
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
        <div class=" nav navbar-default" id="navDefault" role="navigation">
            <div class="container" id="containerNavbar-top">
                <nav class="navbar-header" id="navbar-top-header">
                    <div class="navbar-brand" id="navbar-brand">
                        <!-- Company and name logo -->
                        <label id="logo">
                            <img class="logo" src="assets/img/LogoSYSPARK.jpg" height="50" style="border-radius: 5px 5px;" />
                            SYSPARK
                        </label>
                    </div>
                    <!-- TextBox and Button Search -->
                    <div class="nav" id="search">
                        <input type="text" placeholder=" Search" id="TextBoxSearch" />
                            <button type="button" id="buttonSearch" runat="server"><img id="imageSearch" src="assets/img/LogoSearch.jpg"/></button>
                    </div>
                </nav>
            </div>
        </div>

        <!-- Page options -->
        <div>
            <table id="PageOptions">
                <tr>
                    <td>
                        <br />
                        <br />
                        <br />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td>
                        <div>
                            <button type="button" id="buttonParkReservation">Parking Reservation</button>
                        </div>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td>
                        <div>
                            <button type="button" id="buttonMyProfile">My Profile</button>
                        </div>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td>
                        <div>
                            <button type="button" id="buttonReports">Reports</button>
                        </div>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td>
                        <div>
                            <button type="button" id="buttonConfiguration">Configuration</button>
                        </div>
                        <br />
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
                    <button type="button" id="buttonNavToogleOptions" class="navbar-toggle" data-toggle="collapse" data-target="#navbar-right">
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
                                    <button type="button" id="buttonLogout" runat="server" onclick="return confirm('Sure?') && !" onserverclick="Logout">Log out</button>
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
