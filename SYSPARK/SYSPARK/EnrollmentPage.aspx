<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EnrollmentPage.aspx.cs" Inherits="SYSPARK.EnrollmentPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Enrollment</title>
    <link href="assets/favicon.ico.ico" type="image/x-icon" rel="shorcut icon" />
    <meta name="viewport" content="initial-scale=1, maximum-scale=1" />
    <!-- JQuery Library -->
    <script src="App_Utility/jquery.js"></script>
    <!-- Bootstrap -->
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap.min.css" />
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/js/bootstrap.min.js"></script>
    <!-- CSS -->
    <link rel="stylesheet" type="text/css" href="assets/css/enrollment.css" />
</head>
<body>
    <form id="formEnrollment" runat="server">
        <div class="Header">
            <div class="navbar default-navbar navbar-static-top" role="navigation">
                <div class="container">
                    <div class="navbar-header">
                        <a class="navbar-brand" id="navbar-brand" href="Home.aspx">
                            <!-- Company and name logo -->
                            <span>
                                <img src="assets/img/LogoSYSPARK.jpg" height="50" style="border-radius: 5px 5px;" />
                                <span class="logo">| SYSPARK | Home | Profile | Vehicle |</span>
                            </span>
                        </a>
                    </div>
                </div>
            </div>
        </div>

        <div id="allContentInEnrollment">
            <br />
            <input type="button" id="buttonErrors" runat="server" visible="false" />
            <br />
            <h1>Enrollment</h1>
            <br />
            <table id="tableEnrollment" border="0">
                <tr>
                    <td>
                        <br />
                        <input type="hidden" id="hiddenEnrollmentId" runat="server" />
                        <input type="text" id="textboxName" runat="server" placeholder=" Name" disabled="disabled"/>
                    </td>
                </tr>
                <tr>
                    <td>
                        <input type="text" id="textboxUnversityCard" placeholder=" University Card" runat="server" disabled="disabled"/>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td>
                        <input type="date" id="dateInitialDate" placeholder=" Initial date: dd/mmmm/aaaa" runat="server" min="2015-09-11" disabled="disabled"/>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td>
                        <input type="date" id="dateFinalDate" placeholder=" Final date: dd/mmmm/aaaa" runat="server" disabled="disabled"/>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td>
                        <select id="selectCampus" placeholder=" Campus" runat="server" disabled="disabled"/>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td>
                        <input type="text" id="textboxStatus" placeholder=" Status" runat="server" disabled="disabled"/>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td>
                        <button type="button" id="ButtonActivateEnrollment" runat="server" disabled="disabled">Activate Enrollment</button>
                        <br />
                        <br />
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
