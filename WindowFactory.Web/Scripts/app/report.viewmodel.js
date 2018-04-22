var ReportViewModel = function (app, dataModel) {
    var self = this;

    Sammy(function () {
        this.get('#reports', function () {
            app.markLinkAsActive('report');
            var year = 2018;

            app.view(self);

            $.ajax({
                method: 'get',
                url: '/api/Sale/ChartDataYear/' + year,
                contentType: "application/json; charset=utf-8",
                headers: {
                    'Authorization': 'Bearer ' + app.dataModel.getAccessToken()
                },
                success: function (response) {
                    self.showChartYear(response);

                    $.ajax({
                        method: 'get',
                        url: '/api/Sale/ChartDataWeek',
                        contentType: "application/json; charset=utf-8",
                        headers: {
                            'Authorization': 'Bearer ' + app.dataModel.getAccessToken()
                        },
                        success: function (response) {
                            self.showChartWeek(response);
                        }
                    });
                }
            });
        });
    });

    self.showChartYear = function(data) {
        var barChartData = {
            labels: ['Янв.', 'Фев.', 'Март', 'Апр.', 'Май', 'Июнь', 'Июль', 'Авг.', 'Сент.', 'Окт.', 'Нояб.', 'Дек.'],
            datasets: [
                {
                    fillColor: "#FBB03B",
                    strokeColor: "#FBB03B",
                    data: data
                },
                //{
                //    fillColor: "#FBB03B",
                //    strokeColor: "#FBB03B",
                //    data: [30, 45, 55, 70, 40, 25, 15, 8, 5, 2]
                //}
            ]

        };
        new Chart(document.getElementById("bar1").getContext("2d")).Bar(barChartData);
    }

    self.showChartWeek = function (data) {
        var lineChartData = {
            labels: ["Пн.", "Вт.", "Ср.", "Чт.", "Пт.", "Сб.", "Вс."],
            datasets: [
                {
                    fillColor: "#fff",
                    strokeColor: "#1ABC9C",
                    pointColor: "#1ABC9C",
                    pointStrokeColor: "#1ABC9C",
                    data: [20, 35, 45, 30, 10, 65, 40, 41]
                }
            ]
        };
        new Chart(document.getElementById("line1").getContext("2d")).Line(lineChartData);
    }

    return self;
}
 
app.addViewModel({
    name: "Report",
    bindingMemberName: "reports",
    factory: ReportViewModel
});