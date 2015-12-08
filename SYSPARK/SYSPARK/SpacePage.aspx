<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SpacePage.aspx.cs" Inherits="SYSPARK.SpacePage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Space</title>
    <link href="assets/favicon.ico.ico" type="image/x-icon" rel="shorcut icon" />
    <meta name="viewport" content="initial-scale=1, maximum-scale=1" />
    <!-- JQuery Library -->
    <script src="App_Utility/jquery.js"></script>
    <!-- Bootstrap -->
    <link rel="stylesheet" href="App_Utility/bootstrap.css" />
    <link rel="stylesheet" href="App_Utility/bootstrap.min.css" />
    <script src="App_Utility/bootstrap.min.js"></script>
    <!-- CSS -->
    <link rel="stylesheet" type="text/css" href="assets/css/space.css" />
    <script type="text/javascript" src="assets/js/Space.js"></script>
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
    <form id="formSpace" runat="server">
        <div class="nav navbar-default" id="navDefault" role="navigation">
            <div class="container">
                <div class="navbar-header">
                    <a class="navbar-brand" id="navbar-brand" href="Home.aspx">
                        <!-- Company and name logo -->
                        <span>
                            <img src="assets/img/LogoSYSPARK.jpg" height="50" style="border-radius: 5px 5px;" />
                            <span class="logo">| SYSPARK | Home | Space |</span>
                        </span>
                    </a>
                </div>
            </div>
        </div>
        <div id="allContentInSpace">
            <br />
            <input type="button" id="buttonErrors" runat="server" visible="false" />
            <br />
            <h1>Space</h1>
            <br />
            <table id="tableSpace" border="0">
                <tr>
                    <td>
                        <br />
                        <input type="text" id="textboxAmount" runat="server" placeholder=" Amount" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <span>Space Type</span><br />
                        <input type="hidden" id="hiddenSpaceTypeValue" runat="server" />
                        <select id="selectSpaceType" runat="server" onchange="setValue('selectSpaceType', 'hiddenSpaceTypeValue')"></select>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td>
                        <input type="hidden" id="hiddenCampusValue" runat="server" />
                        <span>Campus</span><br />
                        <select id="selectCampus" runat="server" onchange="setValueCampus('selectCampus', 'hiddenCampusValue')"></select>
                        <button onserverclick=" FillSelectParking" id="buttonSearchCampus">Search</button>
                    </td>
                </tr>
                <tr>
                    <td>
                        <span>Parking</span><br />
                        <input type="hidden" id="hiddenPakingValue" runat="server" />
                        <select id="selectParking" runat="server" onchange="setValue('selectParking', 'hiddenPakingValue')"></select>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td>
                        <span>Status</span><br />
                        <input type="hidden" id="hiddenStatusValue" runat="server" />
                        <span id="spanOn">On</span><span id="spanOff">Off</span><br />
                        <input type="checkbox" id="checkBoxStatusOn" onchange="checkedDesicion('checkBoxStatusOn', 'checkBoxStatusOff', 'hiddenStatusValue')" />
                        <input type="checkbox" id="checkBoxStatusOff" onchange="checkedDesicion('checkBoxStatusOn', 'checkBoxStatusOff', 'hiddenStatusValue')" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <button onclick="clearingSomeControls()" type="button" id="buttonClear" runat="server">Clean</button>
                        <button onserverclick=" AddSpace_Click" type="button" id="buttonAddSpace" runat="server">Add Space</button>
                        <button onclick="cancelUpdate()" type="button" id="buttonCancelUpdate" runat="server">Cancel</button>
                        <button type="button" id="buttonUpdate" runat="server">Update</button>
                        <br />
                    </td>
                </tr>
            </table>
            <br />
            <button onserverclick=" Delete_Click" type="button" id="buttonDelete" runat="server" disabled="disabled" style="background-color: transparent; border: none;" />
            <input type="hidden" id="hiddenSpaceTypeId" runat="server" />
            <input type="hidden" id="hiddenSpaceId" runat="server" />
            <div class="table-responsive">
                <table id="tableGray">
                    <tr>
                        <td>
                            <input type="button" runat="server" id="buttonInfoSpaceTable" value="Please, after any operation select one space!" />

                            <input type="hidden" id="hiddenCampusToViewValue" runat="server" />
                            <select id="selectCampusToView" runat="server" onchange="setValueCampusToView('selectCampusToView', 'hiddenCampusToViewValue')"></select>
                            <button onserverclick=" FillSelectCampusToView" id="buttonSearchCampusToView">Search</button>

                            <input type="hidden" id="hiddenParkingToViewValue" runat="server" />
                            <select id="selectParkingToView" runat="server" onchange="setValueParkingToView('selectParkingToView', 'hiddenParkingToViewValue')"></select>
                            <button onserverclick=" FillSelectParkingToView" id="buttonSearchParking">Search</button>

                            <table class="table" id="tableSpaces">
                                <asp:PlaceHolder ID="placeHolderTableSpace" runat="server"></asp:PlaceHolder>
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
