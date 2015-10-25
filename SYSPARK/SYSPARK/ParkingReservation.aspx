<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ParkingReservation.aspx.cs" Inherits="SYSPARK.ParkingReservation" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Parking Reservation</title>
    <link href="assets/favicon.ico.ico" type="image/x-icon" rel="shorcut icon" />
    <meta name="viewport" content="initial-scale=1, maximum-scale=1" />
    <!-- JQuery Library -->
    <script src="App_Utility/jquery.js"></script>
    <!-- Bootstrap -->
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap.min.css" />
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/js/bootstrap.min.js"></script>
    <!-- CSS -->
    <link rel="stylesheet" type="text/css" href="assets/css/parkingReservation.css" />
    <script>
    function setValue(x, y) {
        var select = document.getElementById(x);
        var hiddenValue = document.getElementById(y);
        var position = select.selectedIndex;
        hiddenValue.value = select.options[position].value;
        alert(position);
        alert(hiddenValue.value);
    }
</script>
</head>
<body>
    <form id="formParkReservation" runat="server">
        <div class="Header">
            <div class="navbar default-navbar navbar-static-top" role="navigation">
                <div class="container">
                    <div class="navbar-header">
                        <a class="navbar-brand" href="Default.aspx">
                            <span style="color: white;">
                                <img class="logo" src="assets/img/LogoSYSPARK.jpg" height="40" style="border-radius: 5px 5px;" />
                                SYSPARK > Home > Parking Reservation
                            </span>
                        </a>
                    </div>
                </div>
            </div>
        </div>
        <div id="allContent">
            <table id="tableReservation">
                <tr>
                    <td>
                        <input id="hiddenParkingValue" type="hidden" runat="server"/>
                        <select id="selectParking" runat="server" onchange="setValue(this.id, 'hiddenParkingValue')">
                        </select>
                        <br /> 
                    </td>
                </tr>
            </table>
        </div>



    </form>
</body>
</html>
