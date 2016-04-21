
App.EstimateItem = function (data) {
    var self = this;

    self.itemId = ko.observable();
    self.creationDate = ko.observable();
    self.estimateId = ko.observable();
    self.name = ko.observable();
    self.description = ko.observable();
    self.price = ko.observable(0);
    self.developmentHours = ko.observable(0);
    self.isOptional = ko.observable();
    self.sortOrder = ko.observable();


    self.init = function (data2) {
        if (!data2) return;

        self.itemId(data2.itemId);
        self.creationDate(data2.creationDate);
        self.estimateId(data2.estimateId);
        self.name(data2.name);
        self.description(data2.description);
        self.price(data2.price);
        self.developmentHours(data2.developmentHours);
        self.isOptional(data2.isOptional);
        self.sortOrder(data2.sortOrder);
    }
    if (data) {
        self.init(data);
    }


};

App.Estimate = function (data) {
    var self = this;

    self.estimateId = ko.observable();
    self.creationDate = ko.observable();
    self.guid = ko.observable();
    self.dealId = ko.observable();
    self.discount = ko.observable();
    self.hourlyRate = ko.observable();
    self.total = ko.observable();
    self.creator = ko.observable();
    self.title = ko.observable();
    self.text = ko.observable();
    self.items = ko.observableArray();

    self.subscribeToItem = function (item) {
        item.developmentHours.subscribe(function () {
            item.price(item.developmentHours() * self.hourlyRate());
        });
        item.price.subscribe(function () {
            var total = 0;
            for (var j = 0; j < self.items().length; j++)
                total += parseInt(self.items()[j].price());
            self.total(total);
        });
    }

    self.init = function (data2) {
        if (!data2) return;

        self.estimateId(data2.estimateId);
        self.creationDate(data2.creationDate);
        self.guid(data2.guid);
        self.dealId(data2.dealId);
        self.discount(data2.discount);
        self.hourlyRate(data2.hourlyRate);
        self.total(data2.total);
        self.creator(data2.creator);
        self.title(data2.title);
        self.text(data2.text);

        $.each(data2.items, function (index, item) {
            var newItem = new App.EstimateItem(item);
            self.items.push(newItem);
            self.subscribeToItem(newItem);
        });
    }
    if (data) {
        self.init(data);
    }
    self.removeItem = function (item) {
        self.items.remove(item);
    }
    self.addItem = function () {
        var item = new App.EstimateItem();
        self.items.push(item);
        self.subscribeToItem(item);
    }


    self.totalAfterDiscount = ko.computed(function () {
        return self.total() * (1 - self.discount() / 100);
    });

}