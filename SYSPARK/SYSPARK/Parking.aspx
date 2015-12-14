<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Parking.aspx.cs" Inherits="SYSPARK.Paking" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Parking</title>
    <link href="assets/favicon.ico.ico" type="image/x-icon" rel="shorcut icon" />
    <meta name="viewport" content="initial-scale=1, maximum-scale=1" />
    <!-- JQuery Library -->
    <script src="App_Utility/jquery.js"></script>
    <!-- Bootstrap -->
    <link rel="stylesheet" href="App_Utility/bootstrap.css" />
    <link rel="stylesheet" href="App_Utility/bootstrap.min.css" />
    <script src="App_Utility/bootstrap.min.js"></script>
    <!-- CSS -->
    <link rel="stylesheet" type="text/css" href="assets/css/parking.css" />
    <script type="text/javascript" src="assets/js/Parking.js"></script>
    <style type="text/css">
        .desmarcado {
            background-color: white;
        }

        .marcado {
            background-color: #629675;
        }
    </style>
</head>
<body>
    <form id="formParking" runat="server">
        <div class="nav navbar-default" id="navDefault" role="navigation">
            <div class="container">
                <div class="navbar-header">
                    <a class="navbar-brand" id="navbar-brand" href="Home.aspx">
                        <!-- Company and name logo -->
                        <span>
                            <img src="assets/img/LogoSYSPARK.jpg" height="50" style="border-radius: 5px 5px;" />
                            <span class="logo">| SYSPARK | Home | Parking |</span>
                        </span>
                    </a>
                </div>
            </div>
        </div>
        <div id="allContentInParking">
            <input type="button" id="buttonErrors" runat="server" disabled="disabled" visible="false" />
            <br />
            <h1>Parking</h1>
            <br />
            <table id="tableParking" border="0">
                <tr>
                    <td>
                        <span>Name</span><br />
                        <input type="text" id="textboxParkingName" placeholder=" Parking name" runat="server" />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td>
                        <span>Total Space</span><br />
                        <input type="text" id="textboxTotalSpace" placeholder=" Total space" runat="server" />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td>
                        <span>Wide Spaces In The Parking</span><br />
                        <input type="text" id="textHeight" placeholder=" Parking space height" runat="server" />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td>
                        <span>Long Spaces In The Parking</span><br />
                        <input type="text" id="textWidth" placeholder=" Parking space widht" runat="server" />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td>
                        <span>Car Space</span><br />
                        <input type="text" id="textboxCarSpace" placeholder=" Car space" runat="server" />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td>
                        <span>MotorcycleSpace</span><br />
                        <input type="text" id="textboxMotorCycleSpace" placeholder=" Motorcycle space" runat="server" />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td><span>Bus Space</span><br />
                        <input type="text" id="textboxBusSpace" placeholder=" Bus space" runat="server" />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td>
                        <span>Handicap Space</span><br />
                        <input type="text" id="textboxHandicapSpace" placeholder=" Handicap space" runat="server" />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td>
                        <input id="hiddenCampusValue" type="hidden" runat="server" />
                        <span>Campus</span><br />
                        <select id="selectCampus" runat="server" onchange="setValue('selectCampus', 'hiddenCampusValue')">
                        </select>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td>
                        <button type="button" id="buttonCancel" onclick="cancelGenerateMap()" runat="server">Cancel</button>
                        <button type="button" id="buttonClear" onclick="clearingSomeControls()" runat="server">Clear</button>
                        <button onserverclick=" GenerateMap_Click" type="button" id="buttonGenerateMap" runat="server">Generate Map</button>
                        <button onserverclick=" Update_Click" type="button" id="buttonUpdate" runat="server">Update</button>
                    </td>
                </tr>
            </table>
            <br />
            <input type="button" runat="server" visible="false" id="buttonErrors2" disabled="disabled" />
            <h1 id="h1ParkingMap" runat="server">Parking Map</h1>
            <div class="table-responsive">
            <table id="tableGray2" runat="server">
                <tr>
                    <td>
                        <select id="selectType" runat="server">
                        </select><br />
                        <span>Car: </span><span id="spanCar" runat="server"></span><span>Motorcycle: </span><span id="spanMotorcycle" runat="server"></span><span>Handicap: </span><span id="spanHandicap" runat="server"></span><span>Bus: </span><span runat="server" id="spanBus"></span>
                        <input type="hidden" id="hidden1" runat="server" />
                        <input type="hidden" id="hidden2" runat="server" />
                        <input type="hidden" id="hidden3" runat="server" />
                        <input type="hidden" id="hidden4" runat="server" />
                        <input type="hidden" id="hidden5" runat="server" />
                        <input type="hidden" id="hidden6" runat="server" />
                        <input type="hidden" id="hidden7" runat="server" />
                        <input type="hidden" id="hidden8" runat="server" />
                        <input type="hidden" id="hidden9" runat="server" />
                        <input type="hidden" id="hidden10" runat="server" />
                        <input type="hidden" id="hidden11" runat="server" />
                        <input type="hidden" id="hidden12" runat="server" />
                        <input type="hidden" id="hidden13" runat="server" />
                        <input type="hidden" id="hidden14" runat="server" />
                        <input type="hidden" id="hidden15" runat="server" />
                        <input type="hidden" id="hidden16" runat="server" />
                        <input type="hidden" id="hidden17" runat="server" />
                        <input type="hidden" id="hidden18" runat="server" />
                        <input type="hidden" id="hidden19" runat="server" />
                        <input type="hidden" id="hidden20" runat="server" />
                        <input type="hidden" id="hidden21" runat="server" />
                        <input type="hidden" id="hidden22" runat="server" />
                        <input type="hidden" id="hidden23" runat="server" />
                        <input type="hidden" id="hidden24" runat="server" />
                        <input type="hidden" id="hidden25" runat="server" />
                        <input type="hidden" id="hidden26" runat="server" />
                        <input type="hidden" id="hidden27" runat="server" />
                        <input type="hidden" id="hidden28" runat="server" />
                        <input type="hidden" id="hidden29" runat="server" />
                        <input type="hidden" id="hidden30" runat="server" />
                        <input type="hidden" id="hidden31" runat="server" />
                        <input type="hidden" id="hidden32" runat="server" />
                        <input type="hidden" id="hidden33" runat="server" />
                        <input type="hidden" id="hidden34" runat="server" />
                        <input type="hidden" id="hidden35" runat="server" />
                        <input type="hidden" id="hidden36" runat="server" />
                        <input type="hidden" id="hidden37" runat="server" />
                        <input type="hidden" id="hidden38" runat="server" />
                        <input type="hidden" id="hidden39" runat="server" />
                        <input type="hidden" id="hidden40" runat="server" />
                        <input type="hidden" id="hidden41" runat="server" />
                        <input type="hidden" id="hidden42" runat="server" />
                        <input type="hidden" id="hidden43" runat="server" />
                        <input type="hidden" id="hidden44" runat="server" />
                        <input type="hidden" id="hidden45" runat="server" />
                        <input type="hidden" id="hidden46" runat="server" />
                        <input type="hidden" id="hidden47" runat="server" />
                        <input type="hidden" id="hidden48" runat="server" />
                        <input type="hidden" id="hidden49" runat="server" />
                        <input type="hidden" id="hidden50" runat="server" />
                        <input type="hidden" id="hidden51" runat="server" />
                        <input type="hidden" id="hidden52" runat="server" />
                        <input type="hidden" id="hidden53" runat="server" />
                        <input type="hidden" id="hidden54" runat="server" />
                        <input type="hidden" id="hidden55" runat="server" />
                        <input type="hidden" id="hidden56" runat="server" />
                        <input type="hidden" id="hidden57" runat="server" />
                        <input type="hidden" id="hidden58" runat="server" />
                        <input type="hidden" id="hidden59" runat="server" />
                        <input type="hidden" id="hidden60" runat="server" />
                        <input type="hidden" id="hidden61" runat="server" />
                        <input type="hidden" id="hidden62" runat="server" />
                        <input type="hidden" id="hidden63" runat="server" />
                        <input type="hidden" id="hidden64" runat="server" />
                        <input type="hidden" id="hidden65" runat="server" />
                        <input type="hidden" id="hidden66" runat="server" />
                        <input type="hidden" id="hidden67" runat="server" />
                        <input type="hidden" id="hidden68" runat="server" />
                        <input type="hidden" id="hidden69" runat="server" />
                        <input type="hidden" id="hidden70" runat="server" />
                        <input type="hidden" id="hidden71" runat="server" />
                        <input type="hidden" id="hidden72" runat="server" />
                        <input type="hidden" id="hidden73" runat="server" />
                        <input type="hidden" id="hidden74" runat="server" />
                        <input type="hidden" id="hidden75" runat="server" />
                        <input type="hidden" id="hidden76" runat="server" />
                        <input type="hidden" id="hidden77" runat="server" />
                        <input type="hidden" id="hidden78" runat="server" />
                        <input type="hidden" id="hidden79" runat="server" />
                        <input type="hidden" id="hidden80" runat="server" />
                        <input type="hidden" id="hidden81" runat="server" />
                        <input type="hidden" id="hidden82" runat="server" />
                        <input type="hidden" id="hidden83" runat="server" />
                        <input type="hidden" id="hidden84" runat="server" />
                        <input type="hidden" id="hidden85" runat="server" />
                        <input type="hidden" id="hidden86" runat="server" />
                        <input type="hidden" id="hidden87" runat="server" />
                        <input type="hidden" id="hidden88" runat="server" />
                        <input type="hidden" id="hidden89" runat="server" />
                        <input type="hidden" id="hidden90" runat="server" />
                        <input type="hidden" id="hidden91" runat="server" />
                        <input type="hidden" id="hidden92" runat="server" />
                        <input type="hidden" id="hidden93" runat="server" />
                        <input type="hidden" id="hidden94" runat="server" />
                        <input type="hidden" id="hidden95" runat="server" />
                        <input type="hidden" id="hidden96" runat="server" />
                        <input type="hidden" id="hidden97" runat="server" />
                        <input type="hidden" id="hidden98" runat="server" />
                        <input type="hidden" id="hidden99" runat="server" />
                        <input type="hidden" id="hidden100" runat="server" />
                        <input type="hidden" id="hidden101" runat="server" />
                        <input type="hidden" id="hidden102" runat="server" />
                        <input type="hidden" id="hidden103" runat="server" />
                        <input type="hidden" id="hidden104" runat="server" />
                        <input type="hidden" id="hidden105" runat="server" />
                        <input type="hidden" id="hidden106" runat="server" />
                        <input type="hidden" id="hidden107" runat="server" />
                        <input type="hidden" id="hidden108" runat="server" />
                        <input type="hidden" id="hidden109" runat="server" />
                        <input type="hidden" id="hidden110" runat="server" />
                        <input type="hidden" id="hidden111" runat="server" />
                        <input type="hidden" id="hidden112" runat="server" />
                        <input type="hidden" id="hidden113" runat="server" />
                        <input type="hidden" id="hidden114" runat="server" />
                        <input type="hidden" id="hidden115" runat="server" />
                        <input type="hidden" id="hidden116" runat="server" />
                        <input type="hidden" id="hidden117" runat="server" />
                        <input type="hidden" id="hidden118" runat="server" />
                        <input type="hidden" id="hidden119" runat="server" />
                        <input type="hidden" id="hidden120" runat="server" />
                        <input type="hidden" id="hidden121" runat="server" />
                        <input type="hidden" id="hidden122" runat="server" />
                        <input type="hidden" id="hidden123" runat="server" />
                        <input type="hidden" id="hidden124" runat="server" />
                        <input type="hidden" id="hidden125" runat="server" />
                        <input type="hidden" id="hidden126" runat="server" />
                        <input type="hidden" id="hidden127" runat="server" />
                        <input type="hidden" id="hidden128" runat="server" />
                        <input type="hidden" id="hidden129" runat="server" />
                        <input type="hidden" id="hidden130" runat="server" />
                        <input type="hidden" id="hidden131" runat="server" />
                        <input type="hidden" id="hidden132" runat="server" />
                        <input type="hidden" id="hidden133" runat="server" />
                        <input type="hidden" id="hidden134" runat="server" />
                        <input type="hidden" id="hidden135" runat="server" />
                        <input type="hidden" id="hidden136" runat="server" />
                        <input type="hidden" id="hidden137" runat="server" />
                        <input type="hidden" id="hidden138" runat="server" />
                        <input type="hidden" id="hidden139" runat="server" />
                        <input type="hidden" id="hidden140" runat="server" />
                        <input type="hidden" id="hidden141" runat="server" />
                        <input type="hidden" id="hidden142" runat="server" />
                        <input type="hidden" id="hidden143" runat="server" />
                        <input type="hidden" id="hidden144" runat="server" />
                        <input type="hidden" id="hidden145" runat="server" />
                        <input type="hidden" id="hidden146" runat="server" />
                        <input type="hidden" id="hidden147" runat="server" />
                        <input type="hidden" id="hidden148" runat="server" />
                        <input type="hidden" id="hidden149" runat="server" />
                        <input type="hidden" id="hidden150" runat="server" />
                        <input type="hidden" id="hidden151" runat="server" />
                        <input type="hidden" id="hidden152" runat="server" />
                        <input type="hidden" id="hidden153" runat="server" />
                        <input type="hidden" id="hidden154" runat="server" />
                        <input type="hidden" id="hidden155" runat="server" />
                        <input type="hidden" id="hidden156" runat="server" />
                        <input type="hidden" id="hidden157" runat="server" />
                        <input type="hidden" id="hidden158" runat="server" />
                        <input type="hidden" id="hidden159" runat="server" />
                        <input type="hidden" id="hidden160" runat="server" />
                        <input type="hidden" id="hidden161" runat="server" />
                        <input type="hidden" id="hidden162" runat="server" />
                        <input type="hidden" id="hidden163" runat="server" />
                        <input type="hidden" id="hidden164" runat="server" />
                        <input type="hidden" id="hidden165" runat="server" />
                        <input type="hidden" id="hidden166" runat="server" />
                        <input type="hidden" id="hidden167" runat="server" />
                        <input type="hidden" id="hidden168" runat="server" />
                        <input type="hidden" id="hidden169" runat="server" />
                        <input type="hidden" id="hidden170" runat="server" />
                        <input type="hidden" id="hidden171" runat="server" />
                        <input type="hidden" id="hidden172" runat="server" />
                        <input type="hidden" id="hidden173" runat="server" />
                        <input type="hidden" id="hidden174" runat="server" />
                        <input type="hidden" id="hidden175" runat="server" />
                        <input type="hidden" id="hidden176" runat="server" />
                        <input type="hidden" id="hidden177" runat="server" />
                        <input type="hidden" id="hidden178" runat="server" />
                        <input type="hidden" id="hidden179" runat="server" />
                        <input type="hidden" id="hidden180" runat="server" />
                        <input type="hidden" id="hidden181" runat="server" />
                        <input type="hidden" id="hidden182" runat="server" />
                        <input type="hidden" id="hidden183" runat="server" />
                        <input type="hidden" id="hidden184" runat="server" />
                        <input type="hidden" id="hidden185" runat="server" />
                        <input type="hidden" id="hidden186" runat="server" />
                        <input type="hidden" id="hidden187" runat="server" />
                        <input type="hidden" id="hidden188" runat="server" />
                        <input type="hidden" id="hidden189" runat="server" />
                        <input type="hidden" id="hidden190" runat="server" />
                        <input type="hidden" id="hidden191" runat="server" />
                        <input type="hidden" id="hidden192" runat="server" />
                        <input type="hidden" id="hidden193" runat="server" />
                        <input type="hidden" id="hidden194" runat="server" />
                        <input type="hidden" id="hidden195" runat="server" />
                        <input type="hidden" id="hidden196" runat="server" />
                        <input type="hidden" id="hidden197" runat="server" />
                        <input type="hidden" id="hidden198" runat="server" />
                        <input type="hidden" id="hidden199" runat="server" />
                        <input type="hidden" id="hidden200" runat="server" />
                        <input type="hidden" id="hidden201" runat="server" />
                        <input type="hidden" id="hidden202" runat="server" />
                        <input type="hidden" id="hidden203" runat="server" />
                        <input type="hidden" id="hidden204" runat="server" />
                        <input type="hidden" id="hidden205" runat="server" />
                        <input type="hidden" id="hidden206" runat="server" />
                        <input type="hidden" id="hidden207" runat="server" />
                        <input type="hidden" id="hidden208" runat="server" />
                        <input type="hidden" id="hidden209" runat="server" />
                        <input type="hidden" id="hidden210" runat="server" />
                        <input type="hidden" id="hidden211" runat="server" />
                        <input type="hidden" id="hidden212" runat="server" />
                        <input type="hidden" id="hidden213" runat="server" />
                        <input type="hidden" id="hidden214" runat="server" />
                        <input type="hidden" id="hidden215" runat="server" />
                        <input type="hidden" id="hidden216" runat="server" />
                        <input type="hidden" id="hidden217" runat="server" />
                        <input type="hidden" id="hidden218" runat="server" />
                        <input type="hidden" id="hidden219" runat="server" />
                        <input type="hidden" id="hidden220" runat="server" />
                        <input type="hidden" id="hidden221" runat="server" />
                        <input type="hidden" id="hidden222" runat="server" />
                        <input type="hidden" id="hidden223" runat="server" />
                        <input type="hidden" id="hidden224" runat="server" />
                        <input type="hidden" id="hidden225" runat="server" />
                        <input type="hidden" id="hidden226" runat="server" />
                        <input type="hidden" id="hidden227" runat="server" />
                        <input type="hidden" id="hidden228" runat="server" />
                        <input type="hidden" id="hidden229" runat="server" />
                        <input type="hidden" id="hidden230" runat="server" />
                        <input type="hidden" id="hidden231" runat="server" />
                        <input type="hidden" id="hidden232" runat="server" />
                        <input type="hidden" id="hidden233" runat="server" />
                        <input type="hidden" id="hidden234" runat="server" />
                        <input type="hidden" id="hidden235" runat="server" />
                        <input type="hidden" id="hidden236" runat="server" />
                        <input type="hidden" id="hidden237" runat="server" />
                        <input type="hidden" id="hidden238" runat="server" />
                        <input type="hidden" id="hidden239" runat="server" />
                        <input type="hidden" id="hidden240" runat="server" />
                        <input type="hidden" id="hidden241" runat="server" />
                        <input type="hidden" id="hidden242" runat="server" />
                        <input type="hidden" id="hidden243" runat="server" />
                        <input type="hidden" id="hidden244" runat="server" />
                        <input type="hidden" id="hidden245" runat="server" />
                        <input type="hidden" id="hidden246" runat="server" />
                        <input type="hidden" id="hidden247" runat="server" />
                        <input type="hidden" id="hidden248" runat="server" />
                        <input type="hidden" id="hidden249" runat="server" />
                        <input type="hidden" id="hidden250" runat="server" />
                        <input type="hidden" id="hidden251" runat="server" />
                        <input type="hidden" id="hidden252" runat="server" />
                        <input type="hidden" id="hidden253" runat="server" />
                        <input type="hidden" id="hidden254" runat="server" />
                        <input type="hidden" id="hidden255" runat="server" />
                        <input type="hidden" id="hidden256" runat="server" />
                        <input type="hidden" id="hidden257" runat="server" />
                        <input type="hidden" id="hidden258" runat="server" />
                        <input type="hidden" id="hidden259" runat="server" />
                        <input type="hidden" id="hidden260" runat="server" />
                        <input type="hidden" id="hidden261" runat="server" />
                        <input type="hidden" id="hidden262" runat="server" />
                        <input type="hidden" id="hidden263" runat="server" />
                        <input type="hidden" id="hidden264" runat="server" />
                        <input type="hidden" id="hidden265" runat="server" />
                        <input type="hidden" id="hidden266" runat="server" />
                        <input type="hidden" id="hidden267" runat="server" />
                        <input type="hidden" id="hidden268" runat="server" />
                        <input type="hidden" id="hidden269" runat="server" />
                        <input type="hidden" id="hidden270" runat="server" />
                        <input type="hidden" id="hidden271" runat="server" />
                        <input type="hidden" id="hidden272" runat="server" />
                        <input type="hidden" id="hidden273" runat="server" />
                        <input type="hidden" id="hidden274" runat="server" />
                        <input type="hidden" id="hidden275" runat="server" />
                        <input type="hidden" id="hidden276" runat="server" />
                        <input type="hidden" id="hidden277" runat="server" />
                        <input type="hidden" id="hidden278" runat="server" />
                        <input type="hidden" id="hidden279" runat="server" />
                        <input type="hidden" id="hidden280" runat="server" />
                        <input type="hidden" id="hidden281" runat="server" />
                        <input type="hidden" id="hidden282" runat="server" />
                        <input type="hidden" id="hidden283" runat="server" />
                        <input type="hidden" id="hidden284" runat="server" />
                        <input type="hidden" id="hidden285" runat="server" />
                        <input type="hidden" id="hidden286" runat="server" />
                        <input type="hidden" id="hidden287" runat="server" />
                        <input type="hidden" id="hidden288" runat="server" />
                        <input type="hidden" id="hidden289" runat="server" />
                        <input type="hidden" id="hidden290" runat="server" />
                        <input type="hidden" id="hidden291" runat="server" />
                        <input type="hidden" id="hidden292" runat="server" />
                        <input type="hidden" id="hidden293" runat="server" />
                        <input type="hidden" id="hidden294" runat="server" />
                        <input type="hidden" id="hidden295" runat="server" />
                        <input type="hidden" id="hidden296" runat="server" />
                        <input type="hidden" id="hidden297" runat="server" />
                        <input type="hidden" id="hidden298" runat="server" />
                        <input type="hidden" id="hidden299" runat="server" />
                        <input type="hidden" id="hidden300" runat="server" />
                        <input type="hidden" id="hidden301" runat="server" />
                        <input type="hidden" id="hidden302" runat="server" />
                        <input type="hidden" id="hidden303" runat="server" />
                        <input type="hidden" id="hidden304" runat="server" />
                        <input type="hidden" id="hidden305" runat="server" />
                        <input type="hidden" id="hidden306" runat="server" />
                        <input type="hidden" id="hidden307" runat="server" />
                        <input type="hidden" id="hidden308" runat="server" />
                        <input type="hidden" id="hidden309" runat="server" />
                        <input type="hidden" id="hidden310" runat="server" />
                        <input type="hidden" id="hidden311" runat="server" />
                        <input type="hidden" id="hidden312" runat="server" />
                        <input type="hidden" id="hidden313" runat="server" />
                        <input type="hidden" id="hidden314" runat="server" />
                        <input type="hidden" id="hidden315" runat="server" />
                        <input type="hidden" id="hidden316" runat="server" />
                        <input type="hidden" id="hidden317" runat="server" />
                        <input type="hidden" id="hidden318" runat="server" />
                        <input type="hidden" id="hidden319" runat="server" />
                        <input type="hidden" id="hidden320" runat="server" />
                        <input type="hidden" id="hidden321" runat="server" />
                        <input type="hidden" id="hidden322" runat="server" />
                        <input type="hidden" id="hidden323" runat="server" />
                        <input type="hidden" id="hidden324" runat="server" />
                        <input type="hidden" id="hidden325" runat="server" />
                        <input type="hidden" id="hidden326" runat="server" />
                        <input type="hidden" id="hidden327" runat="server" />
                        <input type="hidden" id="hidden328" runat="server" />
                        <input type="hidden" id="hidden329" runat="server" />
                        <input type="hidden" id="hidden330" runat="server" />
                        <input type="hidden" id="hidden331" runat="server" />
                        <input type="hidden" id="hidden332" runat="server" />
                        <input type="hidden" id="hidden333" runat="server" />
                        <input type="hidden" id="hidden334" runat="server" />
                        <input type="hidden" id="hidden335" runat="server" />
                        <input type="hidden" id="hidden336" runat="server" />
                        <input type="hidden" id="hidden337" runat="server" />
                        <input type="hidden" id="hidden338" runat="server" />
                        <input type="hidden" id="hidden339" runat="server" />
                        <input type="hidden" id="hidden340" runat="server" />
                        <input type="hidden" id="hidden341" runat="server" />
                        <input type="hidden" id="hidden342" runat="server" />
                        <input type="hidden" id="hidden343" runat="server" />
                        <input type="hidden" id="hidden344" runat="server" />
                        <input type="hidden" id="hidden345" runat="server" />
                        <input type="hidden" id="hidden346" runat="server" />
                        <input type="hidden" id="hidden347" runat="server" />
                        <input type="hidden" id="hidden348" runat="server" />
                        <input type="hidden" id="hidden349" runat="server" />
                        <input type="hidden" id="hidden350" runat="server" />
                        <input type="hidden" id="hidden351" runat="server" />
                        <input type="hidden" id="hidden352" runat="server" />
                        <input type="hidden" id="hidden353" runat="server" />
                        <input type="hidden" id="hidden354" runat="server" />
                        <input type="hidden" id="hidden355" runat="server" />
                        <input type="hidden" id="hidden356" runat="server" />
                        <input type="hidden" id="hidden357" runat="server" />
                        <input type="hidden" id="hidden358" runat="server" />
                        <input type="hidden" id="hidden359" runat="server" />
                        <input type="hidden" id="hidden360" runat="server" />
                        <input type="hidden" id="hidden361" runat="server" />
                        <input type="hidden" id="hidden362" runat="server" />
                        <input type="hidden" id="hidden363" runat="server" />
                        <input type="hidden" id="hidden364" runat="server" />
                        <input type="hidden" id="hidden365" runat="server" />
                        <input type="hidden" id="hidden366" runat="server" />
                        <input type="hidden" id="hidden367" runat="server" />
                        <input type="hidden" id="hidden368" runat="server" />
                        <input type="hidden" id="hidden369" runat="server" />
                        <input type="hidden" id="hidden370" runat="server" />
                        <input type="hidden" id="hidden371" runat="server" />
                        <input type="hidden" id="hidden372" runat="server" />
                        <input type="hidden" id="hidden373" runat="server" />
                        <input type="hidden" id="hidden374" runat="server" />
                        <input type="hidden" id="hidden375" runat="server" />
                        <input type="hidden" id="hidden376" runat="server" />
                        <input type="hidden" id="hidden377" runat="server" />
                        <input type="hidden" id="hidden378" runat="server" />
                        <input type="hidden" id="hidden379" runat="server" />
                        <input type="hidden" id="hidden380" runat="server" />
                        <input type="hidden" id="hidden381" runat="server" />
                        <input type="hidden" id="hidden382" runat="server" />
                        <input type="hidden" id="hidden383" runat="server" />
                        <input type="hidden" id="hidden384" runat="server" />
                        <input type="hidden" id="hidden385" runat="server" />
                        <input type="hidden" id="hidden386" runat="server" />
                        <input type="hidden" id="hidden387" runat="server" />
                        <input type="hidden" id="hidden388" runat="server" />
                        <input type="hidden" id="hidden389" runat="server" />
                        <input type="hidden" id="hidden390" runat="server" />
                        <input type="hidden" id="hidden391" runat="server" />
                        <input type="hidden" id="hidden392" runat="server" />
                        <input type="hidden" id="hidden393" runat="server" />
                        <input type="hidden" id="hidden394" runat="server" />
                        <input type="hidden" id="hidden395" runat="server" />
                        <input type="hidden" id="hidden396" runat="server" />
                        <input type="hidden" id="hidden397" runat="server" />
                        <input type="hidden" id="hidden398" runat="server" />
                        <input type="hidden" id="hidden399" runat="server" />
                        <input type="hidden" id="hidden400" runat="server" />
                        <input type="hidden" id="hidden401" runat="server" />
                        <input type="hidden" id="hidden402" runat="server" />
                        <input type="hidden" id="hidden403" runat="server" />
                        <input type="hidden" id="hidden404" runat="server" />
                        <input type="hidden" id="hidden405" runat="server" />
                        <input type="hidden" id="hidden406" runat="server" />
                        <input type="hidden" id="hidden407" runat="server" />
                        <input type="hidden" id="hidden408" runat="server" />
                        <input type="hidden" id="hidden409" runat="server" />
                        <input type="hidden" id="hidden410" runat="server" />
                        <input type="hidden" id="hidden411" runat="server" />
                        <input type="hidden" id="hidden412" runat="server" />
                        <input type="hidden" id="hidden413" runat="server" />
                        <input type="hidden" id="hidden414" runat="server" />
                        <input type="hidden" id="hidden415" runat="server" />
                        <input type="hidden" id="hidden416" runat="server" />
                        <input type="hidden" id="hidden417" runat="server" />
                        <input type="hidden" id="hidden418" runat="server" />
                        <input type="hidden" id="hidden419" runat="server" />
                        <input type="hidden" id="hidden420" runat="server" />
                        <input type="hidden" id="hidden421" runat="server" />
                        <input type="hidden" id="hidden422" runat="server" />
                        <input type="hidden" id="hidden423" runat="server" />
                        <input type="hidden" id="hidden424" runat="server" />
                        <input type="hidden" id="hidden425" runat="server" />
                        <input type="hidden" id="hidden426" runat="server" />
                        <input type="hidden" id="hidden427" runat="server" />
                        <input type="hidden" id="hidden428" runat="server" />
                        <input type="hidden" id="hidden429" runat="server" />
                        <input type="hidden" id="hidden430" runat="server" />
                        <input type="hidden" id="hidden431" runat="server" />
                        <input type="hidden" id="hidden432" runat="server" />
                        <input type="hidden" id="hidden433" runat="server" />
                        <input type="hidden" id="hidden434" runat="server" />
                        <input type="hidden" id="hidden435" runat="server" />
                        <input type="hidden" id="hidden436" runat="server" />
                        <input type="hidden" id="hidden437" runat="server" />
                        <input type="hidden" id="hidden438" runat="server" />
                        <table id="tableParkingMap">
                            <asp:PlaceHolder ID="placeHolderMap" runat="server"></asp:PlaceHolder>
                        </table>
                    </td>
                </tr>
            </table>
                </div>
            <button type="button" id="buttonCreateParking" runat="server" onserverclick=" AddParking_Click">Create Parking</button>
            <br />
            <br />
            <button onserverclick=" Edit_Click" type="button" id="buttonEdit" runat="server" disabled="disabled" />
            <button onserverclick=" Delete_Click" type="button" id="buttonDelete" runat="server" disabled="disabled" />
            <input type="hidden" id="hiddenParkingId" runat="server" />
            <div class="table-responsive">
                <table id="tableGray">
                    <tr>
                        <td>
                            <input type="button" runat="server" id="buttonInfoParkingTable" value="Please, after any operation select one parking!" />
                            <input type="hidden" id="hiddenCampusToViewValue" runat="server" />
                            <select id="selectCampusToView" runat="server" onchange="setValueCampusToView('selectCampusToView', 'hiddenCampusToViewValue')"></select><table class="table" id="tableParkings">
                                <asp:PlaceHolder ID="placeHolderTableParking" runat="server"></asp:PlaceHolder>
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
