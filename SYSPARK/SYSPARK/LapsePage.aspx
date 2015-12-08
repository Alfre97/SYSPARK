<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LapsePage.aspx.cs" Inherits="SYSPARK.LapsePage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Lapse</title>
    <link href="assets/favicon.ico.ico" type="image/x-icon" rel="shorcut icon" />
    <meta name="viewport" content="initial-scale=1, maximum-scale=1" />
    <!-- JQuery Library -->
    <script src="App_Utility/jquery.js"></script>
    <!-- Bootstrap -->
    <link rel="stylesheet" href="App_Utility/bootstrap.css" />
    <link rel="stylesheet" href="App_Utility/bootstrap.min.css" />
    <script src="App_Utility/bootstrap.min.js"></script>
    <!-- CSS -->
    <link rel="stylesheet" type="text/css" href="assets/css/lapse.css" />
    <script type="text/javascript" src="assets/js/Lapse.js"></script>
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
    <form id="formLapse" runat="server">
            <div class="nav navbar-default" id="navDefault" role="navigation">
                <div class="container">
                    <div class="navbar-header">
                        <a class="navbar-brand" id="navbar-brand" href="Home.aspx">
                            <!-- Company and name logo -->
                            <span>
                                <img src="assets/img/LogoSYSPARK.jpg" height="50" style="border-radius: 5px 5px;" />
                                <span class="logo">| SYSPARK | Home | Lapse |</span>
                            </span>
                        </a>
                    </div>
                </div>
            </div>
        <div id="allContentInLapse">
            <br />
            <input type="button" id="buttonErrors" runat="server" visible="false" />
            <br />
            <h1>Lapse</h1>
            <br />
            <table id="tableLapse" border="0">
                <tr>
                    <td>
                        <br />
                        <input type="text" id="textboxLapse" runat="server" placeholder=" Lapse name" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <span>Initial Date</span><br />
                        <input type="text" id="dateInitialDate" placeholder=" Initial date: dd/mm/aaaa" runat="server" min="2015-09-11" />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td>
                        <span>Final Date</span><br />
                        <input type="text" id="dateFinalDate" placeholder=" Final date: dd/mm/aaaa" runat="server" />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td>
                        <span>Status</span><br />
                        <input type="hidden" id="hiddenStatusValue" runat="server" />
                        <span id="spanOn">On</span><span id="spanOff">Off</span><br />
                        <input type="checkbox" id="checkBoxStatusOn" onchange="checkedDesicion('checkBoxStatusOn', 'checkBoxStatusOff', 'hiddenStatusValue')"/>
                        <input type="checkbox" id="checkBoxStatusOff" onchange="checkedDesicion('checkBoxStatusOn', 'checkBoxStatusOff', 'hiddenStatusValue')"/>
                    </td>
                </tr>
                <tr>
                    <td>
                        <button onclick="clearingSomeControls()" type="button" id="buttonClear" runat="server">Clean</button>
                        <button onserverclick="AddLapse_Click" type="button" id="buttonAddLapse" runat="server">Add Lapse</button>
                        <button onclick="cancelUpdate()" type="button" id="buttonCancelUpdate" runat="server">Cancel</button>
                        <button onserverclick="Update_Click" type="button" id="buttonUpdate" runat="server">Update</button>
                        <br />
                    </td>
                </tr>
            </table>
            <br />
            <button onserverclick=" Delete_Click" type="button" id="buttonDelete" runat="server" disabled="disabled" style="background-color: transparent; border: none;" />
            <input type="hidden" id="hiddenLapseId" runat="server" />
            <div class="table-responsive">
                <table id="tableGray">
                    <tr>
                        <td>
                            <input type="button" runat="server" id="buttonInfoLapseTable" value="Please, after any operation select one role!" />
                            <table class="table" id="tableLapses">
                                <asp:PlaceHolder ID="placeHolderTableLapse" runat="server"></asp:PlaceHolder>
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
