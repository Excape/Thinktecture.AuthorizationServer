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

        //as.util.addRequired(this, "optionLabel", "Label");
        //as.util.addRequired(this, "value", "Value in hours");
        //// todo: validation
        ////as.util.addValidation(this, "value", "Value in hours");
        as.util.addAnyErrors(this);

        vm.save = function() {
            if (vm.isNew()) {
                svc.post(ko.mapping.toJS(vm)).then(function(data, status, xhr) {
                    rememberOptionID = data.id;
                    var url = window.location.toString();
                    url = url.substring(0, url.indexOf('#'));
                    url += "#" + rememberOptionID;
                    window.location = url;

                    vm.id(data.id);
                    vm.isNew(false);

                    svc = new as.Service("admin/RememberOptions/" + rememberOptionID);

                });
            } else {
                svc.put(ko.mapping.toJS(vm));
            }
        };
    }

});