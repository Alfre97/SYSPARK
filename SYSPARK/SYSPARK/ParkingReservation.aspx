<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ParkingReservation.aspx.cs" Inherits="SYSPARK.ParkingReservation" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <div id="google_translate_element"></div><script type="text/javascript">
function googleTranslateElementInit() {
  new google.translate.TranslateElement({pageLanguage: 'en', layout: google.translate.TranslateElement.InlineLayout.SIMPLE}, 'google_translate_element');
}
</script><script type="text/javascript" src="//translate.google.com/translate_a/element.js?cb=googleTranslateElementInit"></script>
    <title>Reservation</title>
    <link href="assets/favicon.ico.ico" type="image/x-icon" rel="shorcut icon" />
    <meta name="viewport" content="initial-scale=1, maximum-scale=1" />
    <!-- JQuery Library -->
    <script src="App_Utility/jquery.js"></script>
    <!-- Bootstrap -->
    <link rel="stylesheet" href="App_Utility/bootstrap.css" />
    <link rel="stylesheet" href="App_Utility/bootstrap.min.css" />
    <script src="App_Utility/bootstrap.min.js"></script>
    <!-- CSS -->
    <link rel="stylesheet" type="text/css" href="assets/css/parkingReservation.css" />
    <script type="text/javascript" src="assets/js/Reservation.js"></script>
</head>
<body>
    <form id="formParkReservation" runat="server">
        <div class="Header">
            <div class="nav navbar-default" id="navDefault" role="navigation">
                <div class="container">
                    <div class="navbar-header">
                        <a class="navbar-brand" id="navbar-brand" href="Home.aspx">
                            <!-- Company and name logo -->
                            <span>
                                <img src="assets/img/LogoSYSPARK.jpg" height="50" style="border-radius: 5px 5px;" />
                                <span class="logo">| SYSPARK | Home | Reservation |</span>
                            </span>
                        </a>
                    </div>
                </div>
            </div>
        </div>
        <div id="allContentInReservation">
            <br />
            <input type="button" id="buttonErrors" runat="server" visible="false" />
            <br />
            <h1>Reservation</h1>
            <br />
            <table id="tableReservation">
                <tr id="trCampus" runat="server">
                    <td>
                        <input type="hidden" id="hiddenCampusValue" runat="server" />
                        <span>Campus</span><br />
                        <select id="selectCampus" runat="server" onchange="setValue('selectCampus', 'hiddenCampusValue')">
                        </select>
                        <button onserverclick=" FillSelectParking" id="buttonSearchParking">Search</button>
                    </td>
                </tr>
                <tr>
                    <td>
                        <input id="hiddenParkingValue" type="hidden" runat="server" />
                        <span>Parking</span><br />
                        <select id="selectParking" runat="server" onchange="setValue('selectParking', 'hiddenParkingValue')">
                        </select>
                        <button onserverclick=" FillSelectSpace" id="buttonSearchSpace">Search</button>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:ScriptManager ID="ScriptManagerMap" runat="server"></asp:ScriptManager>
                        <asp:UpdatePanel ID="UpdatePanelMap" runat="server">
                            <ContentTemplate>
                                <input type="button" runat="server" visible="false" id="buttonErrors2" disabled="disabled" /><br />
                                <span>Parking Map</span>
                                <div class="table-responsive">
                                    <table id="tableGray2" runat="server">
                                        <tr>
                                            <td>
                                                <input type="hidden" id="hiddenSpaceValue" runat="server" />
                                                <table id="tableParkingMap">
                                                    <asp:PlaceHolder ID="placeHolderMap" runat="server"></asp:PlaceHolder>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <button type="button" id="buttonCancel" onclick="cancelSelection(this)">Cancel</button>
                                <button onserverclick=" GenerateMap_Click" type="button" id="buttonViewMap" runat="server">View Map</button>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td>
                        <span>Space:</span><span id="spanSpace"></span>
                    </td>
                </tr>
                <tr>
                    <td>
                        <input id="hiddenVehicleValue" type="hidden" runat="server" />
                        <span>Vehicle</span><br />
                        <select id="selectVehicle" runat="server" onchange="setValue('selectVehicle', 'hiddenVehicleValue')">
                        </select>
                    </td>
                </tr>
                <tr>
                    <td>
                        <input id="hiddenInitialHourValue" type="hidden" runat="server" />
                        <span>Initial Hour</span><br />
                        <select id="selectInitialHour" runat="server" onchange="setValue('selectInitialHour', 'hiddenInitialHourValue')">
                            <option value="6">6 am</option>
                            <option value="7">7 am</option>
                            <option value="8">8 am</option>
                            <option value="9">9 am</option>
                            <option value="10">10 am</option>
                            <option value="11">11 am</option>
                            <option value="12">12 md</option>
                            <option value="13">1 pm</option>
                            <option value="14">2 pm</option>
                            <option value="15">3 pm</option>
                            <option value="16">4 pm</option>
                            <option value="17">5 pm</option>
                            <option value="18">6 pm</option>
                            <option value="19">7 pm</option>
                            <option value="20">8 pm</option>
                            <option value="21">9 pm</option>
                            <option value="22">10 pm</option>
                        </select>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td>
                        <input id="hiddenFinalHourValue" type="hidden" runat="server" />
                        <span>Final Hour</span><br />
                        <select id="selectFinalHour" runat="server" onchange="setValue('selectFinalHour', 'hiddenFinalHourValue')">
                            <option value="6">6 am</option>
                            <option value="7">7 am</option>
                            <option value="8">8 am</option>
                            <option value="9">9 am</option>
                            <option value="10">10 am</option>
                            <option value="11">11 am</option>
                            <option value="12">12 md</option>
                            <option value="13">1 pm</option>
                            <option value="14">2 pm</option>
                            <option value="15">3 pm</option>
                            <option value="16">4 pm</option>
                            <option value="17">5 pm</option>
                            <option value="18">6 pm</option>
                            <option value="19">7 pm</option>
                            <option value="20">8 pm</option>
                            <option value="21">9 pm</option>
                            <option value="22">10 pm</option>
                        </select>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td>
                        <button onclick="clearingSomeControls()" type="button" id="buttonClear" runat="server">Clean</button>
                        <button onserverclick="ButtonReserve_Click" type="button" id="buttonReserve" runat="server">Reserve</button>
                        <button onclick="cancelUpdate()" type="button" id="buttonCancelUpdate" runat="server">Cancel</button>
                        <button type="button" id="buttonUpdate" runat="server">Update</button>
                        <br />
                    </td>
                </tr>
            </table>
            <br />
            <button type="button" id="buttonDelete" runat="server" disabled="disabled" style="background-color: transparent; border: none;" />
            <input type="hidden" id="hiddenReservationId" runat="server" />
            <div class="table-responsive">
                <table id="tableGray">
                    <tr>
                        <td>
                            <input type="hidden" id="hiddenCampusToViewValue" runat="server" />
                            <select id="selectCampusToView" runat="server" onchange="setValue('selectCampusToView', 'hiddenCampusToViewValue')"></select><input type="button" runat="server" id="buttonInfoReservationTable" value="Please, after any operation select one reservation!" />
                            <table class="table" id="tableReservations">
                                <asp:PlaceHolder ID="placeHolderTableReservations" runat="server"></asp:PlaceHolder>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </form>
</body>
</html>
