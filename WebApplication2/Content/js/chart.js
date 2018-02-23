google.charts.load('current', { 'packages': ['corechart','controls'] });
google.charts.setOnLoadCallback(drawChart);
// Set a callback to run when the Google Visualization API is loaded.


function drawChart() {
    jQuery.ajax({
        type:"GET",
        url: "/dataChart",
        dataType: "json",
        success: function (jsonData) {
            var dashboard = new google.visualization.Dashboard(document.getElementById('programmatic_dashboard_div'));
            var data = new google.visualization.DataTable();
            data.addColumn('string', 'application');
            data.addColumn('number', 'count');
            for (var i = 0; i < jsonData.result.length; i++) {
                data.addRow([jsonData.result[i].application, jsonData.result[i].count]);
            }
            var options = {
                title: 'Error statistics', is3D: true, legend: { 'position': 'labeled' }, pieSliceText: 'value'
            };
            var chart = new google.visualization.PieChart(document.getElementById('chart_div'));
            chart.draw(data, options);
            new google.visualization.events.addListener(chart, 'select', selectionHandler);
            function selectionHandler(e) {
                selectedData = chart.getSelection();
                row = selectedData[0].row;
                item = data.getValue(row, 0);
                time = data.getValue(row, 1);
                window.location.href = "error?" + "searchData=" + encodeURIComponent(item);
            }
            function resizeHandler() {
                chart.draw(data, options);
            }
            if (window.addEventListener) {
                window.addEventListener('resize', resizeHandler, false);
            }
            else if (window.attachEvent) {
                window.attachEvent('onresize', resizeHandler);
            }
        }
    });
}
//setInterval(function () {
//    drawChart();
//},5000)


