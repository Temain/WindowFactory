var SaleViewModel = function (app, dataModel) {
    var self = this;

    self.list = ko.observableArray([]);
    self.selectedPage = ko.observable(1);
    self.pageSizes = ko.observableArray([10, 25, 50, 100, 200]);
    self.selectedPageSize = ko.observable(10);
    self.salesCount = ko.observable();
    self.pagesCount = ko.observable();
    self.searchQuery = ko.observable('');

    self.selectedPageChanged = function (page) {
        if (page > 0 && page <= self.pagesCount()) {
            self.selectedPage(page);
            self.loadSales();

            window.scrollTo(0, 0);
        }
    }

    self.pageSizeChanged = function () {
        self.selectedPage(1);
        self.loadSales();

        window.scrollTo(0, 0);
    };

    Sammy(function () {
        this.get('#sale', function () {
            app.markLinkAsActive('sale');

            self.loadSales();
        });

        this.get('/', function () { this.app.runRoute('get', '#sale') });
    });

    self.loadSales = function() {
        $.ajax({
            method: 'get',
            url: '/api/Sale',
            data: { query: self.searchQuery(), page: self.selectedPage(), pageSize: self.selectedPageSize() },
            contentType: "application/json; charset=utf-8",
            headers: {
                'Authorization': 'Bearer ' + app.dataModel.getAccessToken()
            },
            success: function (response) {
                ko.mapping.fromJS(response.items, {}, self.list);
                self.pagesCount(response.pagesCount);
                self.salesCount(response.itemsCount);
                app.view(self);
            }
        });
    }

    self.search = _.debounce(function () {
        self.selectedPage(1);
        self.loadSales();
    }, 300);

    self.removeSale = function (sale) {
        $.ajax({
            method: 'delete',
            url: '/api/Sale/' + sale.saleId(),
            data: JSON.stringify(ko.toJS(self)),
            contentType: "application/json; charset=utf-8",
            headers: {
                'Authorization': 'Bearer ' + app.dataModel.getAccessToken()
            },
            success: function (response) {
                self.list.remove(sale);
                showAlert('success', 'Запись успешно удалёна.');
            }
        });
    }

    return self;
}

var EditSaleViewModel = function(app, dataModel) {
    var self = this;

    self.productId = ko.observable();
    //self.productName = ko.observable().extend({
    //    required: {
    //        params: true,
    //        message: "Необходимо указать наименование детали / спецтехники."
    //    }
    //});
    self.productCost = ko.observable(0);
    self.clientId = ko.observable().extend({
        required: {
            params: true,
            message: "Необходимо выбрать покупателя."
        }
    });
    self.clients = ko.observable([]);
    self.employeeId = ko.observable().extend({
        required: {
            params: true,
            message: "Необходимо выбрать продавца."
        }
    });
    self.employees = ko.observable([]);
    self.numberOfProducts = ko.observable();

    self.totalCost = ko.computed(function () {
        return self.numberOfProducts() * self.productCost();
    }, this);


    self.save = function () {
        var result = ko.validation.group(self, { deep: true });
        if (!self.isValid()) {
            result.showAllMessages(true);

            return false;
        }

        var postData = {
            saleId : self.saleId(),
            productId: self.productId(),
            clientId: self.clientId(),
            employeeId: self.employeeId(),
            numberOfProducts: self.numberOfProducts(),
            saleDate: self.saleDate()
        };

        $.ajax({
            method: 'put',
            url: '/api/Sale/',
            data: JSON.stringify(postData),
            contentType: "application/json; charset=utf-8",
            headers: {
                'Authorization': 'Bearer ' + app.dataModel.getAccessToken()
            },
            success: function (response) {
                app.navigateToSale();
                showAlert('success', 'Изменения успешно сохранены.');
            }
        });
    }

    Sammy(function () {
        this.get('#sale/:id', function () {
            app.markLinkAsActive('sale');

            var id = this.params['id'];
            if (id === 'create') {
                $.ajax({
                    method: 'get',
                    url: '/api/Sale/0',
                    contentType: "application/json; charset=utf-8",
                    headers: {
                        'Authorization': 'Bearer ' + app.dataModel.getAccessToken()
                    },
                    success: function (response) {
                        ko.mapping.fromJS(response, {}, app.Views.CreateSale);
                        app.view(app.Views.CreateSale);
                        app.Views.CreateSale.isValidationEnabled(false);

                        $lwc.init();

                        setTimeout(function () {
                            $("#lwc-economy-sum").parent().hide();
                        }, 500);
                    }
                });
            } else {
                $.ajax({
                    method: 'get',
                    url: '/api/Sale/' + id,
                    contentType: "application/json; charset=utf-8",
                    headers: {
                        'Authorization': 'Bearer ' + app.dataModel.getAccessToken()
                    },
                    success: function (response) {
                        ko.mapping.fromJS(response, {}, self);
                        app.view(self);

                        $lwc.init();

                        setTimeout(function () {
                            $("#lwc-economy-sum").parent().hide();
                        }, 500);
                    }
                });
            }
        });
    });
}

var CreateSaleViewModel = function (app, dataModel) {
    var self = this;
    self.isValidationEnabled = ko.observable(false);

    self.productId = ko.observable();
    //self.productName = ko.observable().extend({
    //    required: {
    //        params: true,
    //        message: "Необходимо указать наименование детали / спецтехники.",
    //        onlyIf: function () {
    //            return self.isValidationEnabled();
    //        }
    //    }
    //});
    self.productCost = ko.observable(0);
    self.clientId = ko.observable().extend({
        required: {
            params: true,
            message: "Необходимо выбрать покупателя.",
            onlyIf: function () {
                return self.isValidationEnabled();
            }
        }
    });
    self.clients = ko.observable([]);
    self.employeeId = ko.observable().extend({
        required: {
            params: true,
            message: "Необходимо выбрать продавца.",
            onlyIf: function () {
                return self.isValidationEnabled();
            }
        }
    });
    self.employees = ko.observable([]);
    self.numberOfProducts = ko.observable();
    self.saleDate = ko.observable(moment());

    self.totalCost = ko.computed(function () {
        return self.numberOfProducts() * self.productCost();
    }, this);

    self.save = function () {
        self.isValidationEnabled(true);
        var result = ko.validation.group(self, { deep: true });
        if (!self.isValid()) {
            result.showAllMessages(true);

            return false;
        }

        var windowType = $(".lwc-menu-blank > a.selected").attr('id');
        var windowTypeId = windowType.replace('type_', '')[0];
        var numberOfFlaps = windowType.replace('type_', '')[2];

        var profileName = $("#lwc-profiles option:selected").text();
        var colors = $("#lwc-clrs option:selected").text();
        var glazing = $("#lwc-glasses option:selected").text()
        var glass = $("#lwc-glasstypes option:selected").text();
        var limiter = $("#lwc-ext-limiter option:selected").text();
        var microvolving = $("#lwc-ext-ventilation").prop('checked');

        var complectation = profileName + ", " + colors + ", " + glazing + ", Стекло: " + glass + ", Ограничитель: " + limiter;
        if (microvolving) complectation += ", Мкрпров.: " + (microvolving ? '+' : '-');

        var mosquito = $("#lwc-ext-mosquito").prop('checked');
        if (mosquito) complectation += ", Сетка: " + (mosquito ? '+' : '-');

        var sill = $("#lwc-ext-sill").prop('checked');
        if (sill) complectation += ", Подоконник: " + (sill ? '+' : '-');

        var otliv = $("#lwc-ext-otliv").prop('checked');
        if (otliv) complectation += ", Водоотл.: " + (otliv ? '+' : '-');

        var wndLeftW = $("#lwc-input-wnd-left").val();
        var wndCntrW = $("#lwc-input-wnd-cntr").val();
        var wndRightW = $("#lwc-input-wnd-right").val();

        var wndH = $("#lwc-input-wnd-height").val();
        var doorH = $("#lwc-input-door-height").val();
        var framH = $("#lwc-input-framuga-height").val();

        var sizes = [];

        if (wndLeftW && numberOfFlaps > 0) {
            sizes.push('ш_ок1-' + wndLeftW);
        }

        if (wndCntrW && numberOfFlaps > 1) {
            sizes.push('ш_ок2-' + wndCntrW);
        }

        if (wndRightW && numberOfFlaps > 2) {
            sizes.push('ш_ок3-' + wndRightW);
        }

        if (wndH) {
            sizes.push('в-ок-' + wndH);
        }

        if (doorH && windowTypeId == '3') {
            sizes.push('в_дв-' +doorH);
        }

        if (framH && windowTypeId == '2') {
            sizes.push('в_фр-' + framH);
        }

        complectation += ', ' + sizes.join(', ');
        var price = $("#lwc-price-sum").text().replace(' руб.', '').replace(' ', '')

        console.log(complectation);

        var postData = {
            clientId: self.clientId(),
            employeeId: self.employeeId(),
            numberOfProducts: self.numberOfProducts(),
            saleDate: self.saleDate(),
            complectation: complectation,
            totalCost: price
            //windowTypeId: windowTypeId,
            //numberOfFlaps: numberOfFlaps,
            //windowProfileId: 1,
            //windowColorId: 1,
            //windowGlazingId: 1,
            //windowGlassId: 1,
            //windowOpeningLimiterId: 1,
            //microvolving: 1,
            //mosquitoNet: 1,
            //windowSill: 1,
            //drainage: 1,
            //firstWidth: 1,
            //secondWidth: 1,
            //thirdWidth: 1,
            //firstHeight: 1,
            //secondHeight: 1,
            //windowInstallation: 1,
            //slopeFinishing: 1,
            //typeOfHouseId: 1
        };

        $.ajax({
            method: 'post',
            url: '/api/Sale/',
            data: JSON.stringify(postData),
            contentType: "application/json; charset=utf-8",
            headers: {
                'Authorization': 'Bearer ' + app.dataModel.getAccessToken()
            },
            error: function(response) {
                // showAlert('danger', 'Произошла ошибка при добавлении сотрудника. Обратитесь в службу технической поддержки.');
            },
            success: function (response) {
                self.productId('');
                self.clientId('');
                self.employeeId('');
                self.numberOfProducts('');
                self.saleDate('');

                result.showAllMessages(false);

                app.navigateToSale();
                showAlert('success', 'Запись успешно добавлена.');
            }
        });
    }
}

app.addViewModel({
    name: "Sale",
    bindingMemberName: "sale",
    factory: SaleViewModel
});

app.addViewModel({
    name: "EditSale",
    bindingMemberName: "editSale",
    factory: EditSaleViewModel
});

app.addViewModel({
    name: "CreateSale",
    bindingMemberName: "createSale",
    factory: CreateSaleViewModel
});