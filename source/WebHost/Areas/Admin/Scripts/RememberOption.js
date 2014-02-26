$(function() {
    var hash = window.location.hash.substring(1);
    var rememberOptionID = parseInt(hash);
    var idx = hash.indexOf("a");
    var appID = hash.substring(idx + 1);

    if (rememberOptionID) {
        var svc = new as.Service("admin/RememberOptions/" + rememberOptionID);
        svc.get().then(function(data) {
            ko.applyBindings(new RememberOption(data), document.getElementById("rememberOptions"));
        });
    } else {
        var svc = new as.Service("admin/ApplicationRememberOptions/" + appID);
        ko.applyBindings(new RememberOption(), document.getElementById("rememberOptions"));
    }

    function RememberOption(data) {
        var vm = this;
        vm.isNew = ko.observable(!data);
        data = data || {
            id: 0,
            optionSelect: "",
            userValue: ""
        };

        data.userValue = data.userValue != "-1" ? data.userValue : null;
        
        ko.mapping.fromJS(data, null, this);

        vm.appID = ko.computed(function() {
            return appID;
        });
        vm.nameEnabled = ko.computed(function() {
            return vm.isNew();
        });
        vm.editDescription = ko.computed(function() {
            return vm.isNew() ? "New" : "Manage";
        });
        vm.menusEnabled = ko.computed(function() {
            return !vm.isNew();
        });
        vm.optionSelectNotForever = ko.computed(function() {
            return vm.optionSelect() != "-1";
        });

        as.util.addValidation(this, "userValue", "Value as Integer", ko.computed(function() {
            var str = vm["userValue"]();
            var n = parseInt(str);
            return (!isNaN(Number(str)) && parseFloat(str) == n && n > 0 && n <= 1000)
                || !vm.optionSelectNotForever();
        }));
        as.util.addAnyErrors(this);

        vm.save = function () {
            vm["userValue"] = vm["userValue"] == null ? "0" : vm["userValue"];
            if (vm.isNew()) {
                svc.post(ko.mapping.toJS(vm)).then(function(data, status, xhr) {
                    rememberOptionID = data.id;
                    var url = window.location.toString();
                    url = url.substring(0, url.indexOf('#'));
                    url += "#" + rememberOptionID;
                    window.location = url;

                    vm.id(data.id);
                    vm.isNew(false);
                });
            } else {
                svc.put(ko.mapping.toJS(vm));
            }
        };
    }

});