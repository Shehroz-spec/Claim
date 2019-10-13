

$(function () {

    ko.validation.init({
        registerExtenders: true,
        messagesOnModified: true,
        insertMessages: true,
        parseInputAttributes: true,
        errorElementClass: "wrong-field",
        decorateElement: true,
        errorClass: 'wrong-field'
    }, true);

    var ViewModel = function () {

        var self = this;
        self.errors = ko.validation.group(self);
        self.isFormValid = ko.observable(self.errors().length === 0);
        self.validateNow = ko.observable(false);

        self.SalesFormNo = ko.observable().extend({
            maxLength: 35,
            isValid: true,
            onlyIf: function () {
                return self.validateNow();
            }
        });

        self.RegistrationNo = ko.observable().extend({
            maxLength: 10,
            isValid: true,
            onlyIf: function () {
                return self.validateNow();
            }
        });

        self.EngineNo = ko.observable().extend({
            maxLength: 20,
            isValid: true,
            onlyIf: function () {
                return self.validateNow();
            }
        });

        self.Chasis = ko.observable().extend({
            maxLength: 20,
            isValid: true,
            onlyIf: function () {
                return self.validateNow();
            }
        });

        self.NIC = ko.observable().extend({
            maxLength: 15,
            isValid: true
        });

        self.MobileNo = ko.observable().extend({
            minLength: 11,
            maxLength: 11,
            isValid: true
        });

        self.ClaimNo = ko.observable().extend({
            maxLength: 30,
            isValid: true,
            onlyIf: function () {
                return self.validateNow();
            }
        });

        self.submit = function () {

            self.validateNow(true);

            if (self.errors().length === 0 && self.isFormValid()) {
                self.isFormValid(true);
                submitSearchForm(self);
            } else {
                alert('Invalid information');
                self.isFormValid(false);
                self.errors.showAllMessages();
            }
        };

        self.reset = function () {
            self.SalesFormNo(null);
            self.RegistrationNo(null);
            self.MobileNo(null);
            self.Chasis(null);
            self.ClaimNo(null);
            self.EngineNo(null);
            self.NIC(null);
            self.isFormValid(true);
        };

        var submitSearchForm = function (formData) {

            function getLastToast() {
                return _$.obj.lastToast;
            }

            function trimIfNotEmpty(value) {
                return value && value !== null ? value.trim() : "";
            }

            var search = {};
            search.SalesFormNo = trimIfNotEmpty(formData.SalesFormNo());
            search.RegistrationNo = trimIfNotEmpty(formData.RegistrationNo());
            search.NIC = trimIfNotEmpty(formData.NIC());
            search.MobileNo = trimIfNotEmpty(formData.MobileNo());
            search.EngineNo = trimIfNotEmpty(formData.EngineNo());
            search.Chasis = trimIfNotEmpty(formData.Chasis());
            search.ClaimNo = trimIfNotEmpty(formData.ClaimNo());

            $("#ClaimSearch").html("");

            if (_$.isNotEmpty(search.SalesFormNo) ||
                _$.isNotEmpty(search.RegistrationNo) ||
                _$.isNotEmpty(search.NIC) ||
                _$.isNotEmpty(search.MobileNo) ||
                _$.isNotEmpty(search.EngineNo) ||
                _$.isNotEmpty(search.Chasis) ||
                _$.isNotEmpty(search.ClaimNo)) {

                $("#loading").show();
                _$.GETHTML('/Claim/GetSearchResult', { 'searchDetailViewModel': search }, true,
                    function (result) {

                        $("#ClaimSearch").html(result);

                        if (result) {

                            $("#loading").hide();

                            if (_$.CheckUndefineType($("#ClaimSearch #PolicyNo").text())) {

                                $("#ClaimSearch").hide();

                                if (_$.obj.searchCount > 0) {
                                    _$.obj.searchCount = 0;
                                }

                                if (_$.obj.searchCount === 0) {
                                    toastr.clear(getLastToast());
                                    _$.Notification("No Record Found", 400);
                                    _$.obj.searchCount++;
                                }

                            } else {

                                $("#lodgeClaim").attr("disabled", $("#ClaimNo").val() !== "");
                                $("#ClaimSearch").show();
                            }
                        }
                    });
            } else {
                $("#loading").hide();

                if (_$.obj.count > 0) {
                    _$.obj.count = 0;
                }

                if (_$.obj.count === 0) {
                    toastr.clear(getLastToast());
                    _$.Notification("Please Select Any Field", 200);
                    _$.obj.count++;
                }
            }
        };
    };

    var viewModel = new ViewModel();
    ko.bindingHandlers.fireChange = {
        update: function (element, valueAccessor, allBindingsAccessor) {
            var bindings = allBindingsAccessor();
            if (bindings.value != null) {
                $(element).change();
            }
        }
    };

    ko.applyBindings(viewModel);

    function validateAllFields() {
        if (viewModel.Chasis())
            viewModel.isFormValid(viewModel.Chasis() !== null && viewModel.Chasis().length > 0);
        else if (viewModel.ClaimNo())
            viewModel.isFormValid(viewModel.ClaimNo() !== null && viewModel.ClaimNo().length > 0);
        else if (viewModel.EngineNo())
            viewModel.isFormValid(viewModel.EngineNo() !== null && viewModel.EngineNo().length > 0);
        else if (viewModel.MobileNo())
            viewModel.isFormValid(viewModel.MobileNo() !== null && viewModel.MobileNo().length > 0);
        else if (viewModel.NIC())
            viewModel.isFormValid(viewModel.NIC() !== null && viewModel.NIC().length > 0);
        else if (viewModel.RegistrationNo())
            viewModel.isFormValid(viewModel.RegistrationNo() !== null && viewModel.RegistrationNo().length > 0);
        else if (viewModel.SalesFormNo())
            viewModel.isFormValid(viewModel.SalesFormNo() !== null && viewModel.SalesFormNo().length > 0);
        else viewModel.isFormValid(false);
    }
});