<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="googleChart.aspx.cs" Inherits="SiCAPV002.googleChart" %>
    <%-- Here We need to write some js code for load google chart with database data --%>
            <script src="Scripts/jquery-1.7.1.js"></script>
            <script type="text/javascript" src="https://www.google.com/jsapi"></script>
 
            <script>
                var chartData; // globar variable for hold chart data
                google.load("visualization", "1", { packages: ["corechart"] });
 
                // Here We will fill chartData
 
                $(document).ready(function () {
           
                    $.ajax({
                        url: "GoogleChart.aspx/GetChartData",
                        data: "",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; chartset=utf-8",
                        success: function (data) {
                            chartData = data.d;
                        },
                        error: function () {
                            alert("Error al leer los datos.");
                        }
                    }).done(function () {
                        // after complete loading data
                        google.setOnLoadCallback(drawChart);
                        drawChart();
                    });
                });
 
 
                function drawChart() {
                    var data = google.visualization.arrayToDataTable(chartData);
 
                    var options = {
                        title: "Sicap",
                        pointSize: 5
                    };
 
                    var pieChart = new google.visualization.BarChart(document.getElementById('chart_div'));
                    pieChart.draw(data, options);
 
                }
 
            </script>
   <div id="chart_div" style="width:500px;height:400px">
                <%-- Here Chart Will Load --%>
            </div>
