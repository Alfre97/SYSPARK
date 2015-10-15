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

    <script>
        function ToGrayOnFocus(x) {
            document.getElementById(x).style.backgroundColor = "white";
            document.getElementById(x).style.color = "darkgray";
        }
        function ToWhiteOnBlur(x) {
            document.getElementById(x).style.backgroundColor = "transparent";
            document.getElementById(x).style.color = "white";
        }
    </script>

</head>
<body>
    <form runat="server">
        <nav class="navbar navbar-default" role="navigation" id="navbardefault">

            <div class="container">

                <nav class="navbar-header">
                    <!-- navbar-toggle -->
                    <button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#mainNavbar">
                        <span class="sr-only">Toggle Navigation</span>
                        <span class="icon-bar" style="background-color: darkgray"></span>
                        <span class="icon-bar" style="background-color: darkgray"></span>
                        <span class="icon-bar" style="background-color: darkgray"></span>
                    </button>
                    <!-- navbar-brand -->
                    <div class="navbar-brand">
                        <span style="color: white;">
                            <img class="logo" src="assets/img/LogoSYSPARK.jpg" height="50" style="border-radius: 5px 5px;" />
                            SYSPARK
                        </span>
                    </div>
                </nav>

                <div class="collapse navbar-collapse" id="mainNavbar">
                    <!-- Textbox an button search -->
                    <div id="search">
                        <input type="text" placeholder=" Search" id="TextBoxSearch" />
                        <button type="button" class="btnSearch">
                            <img src="assets/img/LogoSearch.jpg" style="height: 20px;" /></button>
                    </div>

                    <ul class="nav navbar-nav" id="navbarOptions">
                        <li>
                            <!-- Options of navbar-header -->
                            <button type="button" id="buttonProfile" onmousemove="ToGrayOnFocus(this.id)" onmouseout="ToWhiteOnBlur(this.id)">
                                Profile</button>
                        </li>
                        <li>
                            <button type="button" id="buttonConfiguration" onmouseover="ToGrayOnFocus(this.id)" onmouseout="ToWhiteOnBlur(this.id)">
                                Configuration</button>
                        </li>
                        <li>
                            <button type="button" id="buttonLogOut" onmouseover="ToGrayOnFocus(this.id)" onmouseout="ToWhiteOnBlur(this.id)"
                                runat="server" onclick="return confirm('Sure?') && !" onserverclick="Logout">
                                Log out</button>
                        </li>
                    </ul>
                </div>
            </div>
        </nav>
    </form>
</body>
</html>
