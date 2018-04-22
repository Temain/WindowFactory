var CalculatorViewModel = function (app, dataModel) {
    var self = this;

    self.Initialized = false;

    Sammy(function () {
        this.get('#calculator', function () {
            app.markLinkAsActive('calculator');
            app.view(self);

            $lwc.init();

            setTimeout(function () {
                $("#lwc-economy-sum").parent().hide();
            }, 500);
        });
    });

    return self;
}
 
app.addViewModel({
    name: "Calculator",
    bindingMemberName: "calculator",
    factory: CalculatorViewModel
});

