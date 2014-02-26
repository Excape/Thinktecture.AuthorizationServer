$(function () {
    var svc = new as.Service("admin/ApplicationRememberOptions");
    var remOptionsSvc = new as.Service("admin/RememberOptions");

    var appID = window.location.hash.substring(1);
    svc.get(appID).then(function (data) {
        var vm = new RemOptions(data);
        ko.applyBindings(vm);
    });

    function RemOptions(list) {
        this.appID = ko.observable(appID);
        this.rememberoptions = ko.mapping.fromJS(ko.observableArray(list));
        this.newRememberOption = ko.mapping.fromJS({
            optionLabel: "", value: ""
        });
        as.util.addRequired(this.newRememberOption, "optionLabel", "Label");
        as.util.addRequired(this.newRememberOption, "value", "Value");
        as.util.addAnyErrors(this.newRememberOption);
    }
    RemOptions.prototype.addRememberOption = function (data) {
        var vm = this;
        
        vm.rememberoptions.push(ko.mapping.fromJS(data));
        vm.newRememberOption.optionLabel("");
        vm.newRememberOption.value("");
};

    RemOptions.prototype.populateDefaultOptions = function() {
        var vm = this;
        svc.put(null, appID).then(function(data) {      
            vm.rememberoptions.removeAll();
            data.forEach(function(entry) {
                vm.addRememberOption(entry);
            });
        });
    };

    RemOptions.prototype.deleteRememberOption = function (item) {
        var vm = this;
        remOptionsSvc.delete(item.id()).then(function () {
            vm.rememberoptions.remove(item);
        });
    }
});