google.charts.load('current', { 'packages': ['corechart'] });
google.charts.setOnLoadCallback(drawChart);
function drawChart() {
    var data = google.visualization.arrayToDataTable([
      ['Week',  'Number Of Errors'],
      ['Week1', 15],
      ['Week2', 20],
      ['Week3', 25],
      ['Week4', 30],
      ['Week5', 35],
      ['Week6',  null],
      ['Week7',  30],
      ['Week8',  12]
    ]);

    var options = {
        title: 'Application Performance',
        hAxis: { title: 'Month', titleTextStyle: { color: '#333' } },
        vAxis: { title:'Number Of Errors',minValue: 0 },
        animation: {
            startup: "true",
            duration: 3000,
            easing: 'linear'
        },
        legend: {
            "position": "top"
        },
        selectionMode:'multiple'
    };

    var chart = new google.visualization.AreaChart(document.getElementById('chart_div'));
    chart.draw(data, options);
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