<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="SYSPARK.Home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Home SYSPARK</title>

    <!-- JQuery Library -->
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.3/jquery.min.js"></script>
    <!-- Bootstrap -->
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap.min.css" />
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/js/bootstrap.min.js"></script>
    <!-- CSS -->
    <link rel="stylesheet" type="text/css" href="assets/css/home.css" />

</head>
<body>
    <form runat="server">
        <!-- Navbar -->
        <div class=" nav navbar-default" id="navDefault" role="navigation">
            <img class="logo" src="assets/img/LogoSYSPARK.jpg" height="50" style="border-radius: 5px 5px;" />
            <label id="logo">
                SYSPARK
            </label>

            <div class="navbar-right" id="search">
                <input type="text" placeholder=" Search" id="TextBoxSearch" />
                <button type="button" id="buttonSearch">
                    <img src="assets/img/LogoSearch.jpg" style="height: 20px;" /></button>
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
                    </td>
                </tr>
            </table>
        </div>
        <div class="navbar-nav" id="navbarBottom">
            <div class="container">
                <nav class="navbar-header">
                    <button type="button" class="navbar-toggle">
                        <span class="sr-only">Toggle Navigation</span>
                        <span class="icon-bar" style="background-color: darkgray"></span>
                        <span class="icon-bar" style="background-color: darkgray"></span>
                        <span class="icon-bar" style="background-color: darkgray"></span>
                    </button>
                </nav>
            </div>
        </div>
    </form>
</body>
</html>
