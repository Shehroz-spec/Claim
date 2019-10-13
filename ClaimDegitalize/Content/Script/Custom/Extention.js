
function Extention() {

    //// Fields and objects

    var _$ = this;
    _$.obj = {};
    _$.count = 0;
    _$.obj.count = 0;
    _$.obj.lastToast = null;
    _$.obj.searchCount = 0;
    ////

    //// Validation functions

    _$.validateFormFields = function () {

        var claimType = $("#claimType option:selected").val();
        var incidentdate = $("#Incidentdate").val();
        var incidentCity = $("#IncidentCity option:selected").val();
        var incidentArea = $("#IncidentArea option:selected").val();
        var currentCity = $("#CurrentCity option:selected").val();
        var currentArea = $("#CurrentArea option:selected").val();
        var circumstances = $("#Circumstances").val();
        var damageParts = $("#DamageParts").val();
        var workshopType = $("#WorkshopType option:selected").val();
        var workshop = $("#Workshop option:selected").val();
        var vehicleAvailablity = $("#VehicleAvailablity").val();
        var relationWithInsured = $("#RelationWithInsured option:selected").val();
        var insuredName = $("#InsuredName").val();
        var mobileNo = $("#MobileNo").val();

        if (claimType === "") $("#claimType").addClass("field-required").prop("required", true);
        if (incidentdate === "") $("#Incidentdate").addClass("field-required").prop("required", true);
        if (incidentCity === "" || incidentCity === "0") $("#IncidentCity").addClass("field-required").prop("required", true);
        if (incidentArea === "") $("#IncidentArea").addClass("field-required").prop("required", true);
        if (currentCity === "" || currentCity === "0") $("#CurrentCity").addClass("field-required").prop("required", true);
        if (currentArea === "") $("#CurrentArea").addClass("field-required").prop("required", true);
        if (circumstances === "") $("#Circumstances").addClass("field-required").prop("required", true);
        if (damageParts === "") $("#DamageParts").addClass("field-required").prop("required", true);
        if (workshopType === "") $("#WorkshopType").addClass("field-required").prop("required", true);
        if (workshop === "" || workshop === "-1") $("#Workshop").addClass("field-required").prop("required", true);
        if (vehicleAvailablity === "") $("#VehicleAvailablity").addClass("field-required").prop("required", true);
        if (relationWithInsured === "") $("#RelationWithInsured").addClass("field-required").prop("required", true);
        if (insuredName === "") $("#InsuredName").addClass("field-required").prop("required", true);
        if (mobileNo === "") $("#MobileNo").addClass("field-required").prop("required", true);


        if (claimType !== "") $("#claimType").removeClass("field-required").prop("required", false);
        if (incidentdate !== "") $("#Incidentdate").removeClass("field-required").prop("required", false);
        if (incidentCity !== "" && incidentCity !== "0") $("#IncidentCity").removeClass("field-required").prop("required", false);
        if (incidentArea !== "") $("#IncidentArea").removeClass("field-required").prop("required", false);
        if (currentCity !== "" && currentCity !== "0") $("#CurrentCity").removeClass("field-required").prop("required", false);
        if (currentArea !== "") $("#CurrentArea").removeClass("field-required").prop("required", false);
        if (circumstances !== "") $("#Circumstances").removeClass("field-required").prop("required", false);
        if (damageParts !== "") $("#DamageParts").removeClass("field-required").prop("required", false);
        if (workshopType !== "") $("#WorkshopType").removeClass("field-required").prop("required", false);
        if (workshop !== "" || workshop !== "-1") $("#Workshop").removeClass("field-required").prop("required", false);
        if (vehicleAvailablity !== "") $("#VehicleAvailablity").removeClass("field-required").prop("required", false);
        if (relationWithInsured !== "") $("#RelationWithInsured").removeClass("field-required").prop("required", false);
        if (insuredName !== "") $("#InsuredName").removeClass("field-required").prop("required", false);
        if (mobileNo !== "") $("#MobileNo").removeClass("field-required").prop("required", false);
    };

    _$.setFormValidity = function (key, value) {
        localStorage.setItem(key, JSON.stringify(value));
    };

    _$.ValidIncidentDate = localStorage.getItem("ValidIncidentDate") === "true";

    _$.ValidVADate = localStorage.getItem("ValidVADate") === "true";

    _$.validateInputField = function (fieldToValidate, keyForValidation) {

        var validators = {
            "PhoneNo": {
                ValidationMessageField: $("#phoneValidate"),
                Regex: /^(\((\+|00)92\)( )?|(\+|00)92( )?|0)[1-24-9]([0-9]{1}( )?[0-9]{3}( )?[0-9]{3}( )?[0-9]{1,2}|[0-9]{2}( )?[0-9]{3}( )?[0-9]{3})$/gm
            },
            "CellNo": {
                ValidationMessageField: $("#cellValidate"),
                Regex: /^((\+92)|(0092))-{0}\d{3}-{0}\d{7}$|^\d{11}$|^\d{4}-\d{7}$/gm
            },
            "CNICNo": {
                ValidationMessageField: $("#cnicValidate"),
                Regex: /^[0-9+]{5}-[0-9+]{7}-[0-9]{1}$/gm
            },
            "Email": {
                ValidationMessageField: $("#emailValidate"),
                Regex: /^[a-zA-Z0-9.!#$%&’*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/gm
            }
        };

        if (fieldToValidate !== undefined && fieldToValidate !== null) {

            var textToValidate = fieldToValidate.val();
            var validatorMessageField = validators[keyForValidation];
            var validatorField = "Invalid " + keyForValidation;

            if (textToValidate !== undefined && textToValidate !== null && textToValidate !== "") {

                var isMatched = textToValidate.match(validatorMessageField.Regex);

                if (isMatched !== undefined && isMatched !== null) {

                    var isValid = isMatched.length > 0;
                    if (isValid) {
                        validatorMessageField.ValidationMessageField.hide();
                        localStorage.removeItem(validatorField);

                    } else {
                        validatorMessageField.ValidationMessageField.show();
                        localStorage.setItem(validatorField, validatorField);
                    }

                } else {
                    validatorMessageField.ValidationMessageField.show();
                    localStorage.setItem(validatorField, validatorField);
                }

            } else if (textToValidate !== "") {
                validatorMessageField.ValidationMessageField.hide();
                localStorage.removeItem(validatorField);
            }
        }
    };

    _$.validateIncidentDate = function () {

        var vehicleStartDate = $("#VehicleStartDate").text().trim();
        var vehicleEndDate = $("#VehicleEndDate").text().trim();
        var incidentDate = $("#Incidentdate").val();
        var compDateValMsg = $("#compDateValMsg");

        var dateRegex = /[0-9]{4}-(0[1-9]|1[0-2])-(0[1-9]|[1-2][0-9]|3[0-1]) (2[0-3]|[01][0-9]):[0-5][0-9]/gmi;
        var isValidDate = incidentDate.match(dateRegex);

        if (isValidDate === null || incidentDate === "") {
            _$.Notification("Incident date is required", 100);
            $("#Incidentdate").val("");
            compDateValMsg.hide();
            _$.setFormValidity("ValidIncidentDate", false);
        }
        else {
            incidentDate = incidentDate.replace(/-/g, '/');
            var date = new Date();
            var currentDate = date.getDate() +
                "/" +
                (date.getMonth() + 1) +
                "/" +
                date.getFullYear() +
                " " +
                date.getHours() +
                ":" +
                date.getMinutes() +
                ":" +
                date.getSeconds();

            vehicleEndDate = vehicleEndDate.replace(/-/g, '/');
            vehicleStartDate = vehicleStartDate.replace(/-/g, '/');

            var isValid = new Date(_$.getFormattedDateStrByMonth(vehicleStartDate)) < new Date(incidentDate)
                && new Date(_$.getFormattedDateStrByMonth(vehicleEndDate)) > new Date(incidentDate)
                && new Date(incidentDate).getTime() < new Date(_$.getFormattedDateStr(currentDate)).getTime();


            if (!isValid && incidentDate !== "") {
                compDateValMsg.show();
                compDateValMsg.text("Incident date should be between vehicle start and end dates and less than current date");
                _$.setFormValidity("ValidIncidentDate", false);
            }
            else if (incidentDate === "") {
                compDateValMsg.hide();
                _$.setFormValidity("ValidIncidentDate", false);
            }
            else {
                compDateValMsg.hide();
                _$.setFormValidity("ValidIncidentDate", true);

            }

            var vehicleAvailablity = $("#VehicleAvailablity");

            if (vehicleAvailablity.is(":visible") && vehicleAvailablity.val() !== "") {
                _$.validateVehicleAvailabilityDate();
            }
        }
    };

    _$.validateVehicleAvailabilityDate = function () {

        var vehicleAvailablityDate = $("#VehicleAvailablity").val();
        var incidentDate = $("#Incidentdate").val();

        var isValid = new Date(vehicleAvailablityDate) > new Date(incidentDate)
            && new Date(vehicleAvailablityDate).getTime() > new Date(incidentDate).getTime();

        if (!isValid && vehicleAvailablityDate !== "") {
            $("#compVehicleAvailabilityDateValMsg").show();
            $("#compVehicleAvailabilityDateValMsg").html("Vehicle availability date must be greater than incident date");
            _$.setFormValidity("ValidVADate", false);
        }
        else {
            $("#compVehicleAvailabilityDateValMsg").hide();
            _$.setFormValidity("ValidVADate", true);

        }
    };

    ////

    //// Extension / helper functions

    _$.redirectToUrl = function (url) {
        if (typeof (url) === "string") {
            setTimeout(function () {
                window.location.href = url;
            }, 3000);
        }
    };

    _$.GetTextCombobox = function (id, key) {
        return $(id).find('option[value="' + key + '"]').text();
    };

    _$.GetValueCombobox = function (id) {
        return $(id).find('option:selected').val();
    };

    _$.ModalShow = function (id) {
        return $("#" + id).modal('show');
    };

    _$.ModalHide = function (id) {
        return $("#" + id).modal('hide');
    };

    _$.Notification = function (message, type) {

        var getMessageWithClearButton = function (msg) {
            msg = msg ? msg : 'Clear itself?';
            msg += '<br /><br /><button type="button" class="btn clear">Yes</button>';
            return msg;
        };

        toastr.options = {
            "closeButton": false,
            "debug": false,
            "newestOnTop": false,
            "progressBar": false,
            "positionClass": "toast-top-right",
            "preventDuplicates": true,
            "onclick": null,
            "showDuration": "300",
            "hideDuration": "100",
            "timeOut": "5000",
            "extendedTimeOut": "1000",
            "showEasing": "swing",
            "hideEasing": "linear",
            "showMethod": "fadeIn",
            "hideMethod": "fadeOut"
        };

        var title;
        var $toast;
        switch (type) {
            case 100:
                title = "Success";
                //toastr.options.timeOut = 60000;
                $toast = toastr[title.toLowerCase()](message, title);
                _$.obj.lastToast = $toast;
                break;
            case 200:
                title = "Error";
                $toast = toastr[title.toLowerCase()](message, title);
                _$.obj.lastToast = $toast;
                break;
            case 300:
                title = "Warning";
                $toast = toastr[title.toLowerCase()](message, title);
                _$.obj.lastToast = $toast;
                break;
            case 400:
                title = "NotFound";
                toastr.warning(message, title);
                break;
            case 500:
                title = "Delete";
                toastr.success(message, title);
                break;
            case 600:
                title = "Update";
                toastr.success(message, title);
                break;
            case 700:
                title = "Warning";
                $toast = toastr[title.toLowerCase()](message, title);
                _$.obj.lastToast = $toast;
                break;
        }
    };

    _$.ResetClearContent = function (id) {
        $.each($(id + " input:not(:hidden),input[type=radio],input[type=checkbox],input[type=text],select,input[type=textarea]"), function (i, field) {
            if ($(field).is('input:checkbox')) {
                $(field).prop("checked", false);
            }
            if ($(field).prop("disabled")) {
                $(field).prop("disabled", false);
                $(field).val('');
            }
            else {
                $(field).val('');
            }
        });
    };

    _$.SetCheckBox = function (id, status) {
        return $(id).prop("checked", status).change();
    };

    _$.SetComboboxByValue = function (id, value) {

        var result = false;

        $(id + " option").each(function () {
            if ($(this).val() === value) {
                result = $(id).val($(this).val()).change();
            }
        });

        return result;
    };

    _$.SetComboboxByText = function (id, text) {

        var result = false;

        $(id + " option").each(function () {
            if ($(this).find("option:contains(" + text + ")")) {
                result = $(id + " option:selected").text(text).change();
            }
        });

        return result;
    };

    _$.DisabledField = function (id, status) {

        $.each($(id +
                ".check-line,input:not(:hidden),input[type=radio],input[type=checkbox],input[type=text],select,input[type=textarea],textarea"),
            function (i, field) {

                switch (status) {
                    case "add":
                        $(field).prop("disabled", true);
                        break;
                    case "remove":
                        $(field).removeAttr('disabled');
                        break;
                }
            });
    }

    _$.DisabledEditField = function (id, status) {

        $.each($(id +
            ".check-line,input:not(:hidden),input[type=radio],input[type=checkbox],input[type=text],select,input[type=textarea],textarea"),
            function (i, field) {

                switch (status) {
                    case "add":
                        if (field.name === "Submit") {
                            $(field).removeAttr('disabled');
                            $(field).prop("disabled", false);
                        }
                        else if (field.name === "WorkshopType") {
                            $(field).removeAttr('disabled');
                            $(field).prop("disabled", false);
                        }
                        else if (field.name === "Workshop") {
                            $(field).removeAttr('disabled');
                            $(field).prop("disabled", false);
                        }
                        else if (field.name === "VehicleAvailablity") {
                            $(field).removeAttr('disabled');
                            $(field).prop("disabled", false);
                        }
                        else if (field.name === "WorkshopName") {
                            $(field).removeAttr('disabled');
                            $(field).prop("disabled", false);
                        }
                        else if (field.name === "Damageparts") {
                            $(field).removeAttr('disabled');
                            $(field).prop("disabled", false);
                        }
                        else if (field.name === "Circumstances") {
                            $(field).removeAttr('disabled');
                            $(field).prop("disabled", false);
                        }
                        else if (_$.obj.RelationWithInsured !== "5" &&
                            (field.name === "Email" ||
                            field.name === "PhoneNo" ||
                            field.name === "RelationWithInsured" ||
                            field.name === "Remarks" ||
                            field.name === "MobileNo" ||
                            field.name === "InsuredName")) {
                            $(field).removeAttr('disabled');
                            $(field).prop("disabled", false);
                        }
                        else if (field.name === "Remarks") {
                            $(field).removeAttr('disabled');
                            $(field).prop("disabled", false);
                        }
                        else {
                            $(field).prop("disabled", true);
                            $(field).prop("readonly", true);
                        }
                        break;
                    case "remove":
                        if (field.name === "Submit" ||
                            field.name === "WorkshopType" ||
                            field.name === "Workshop" ||
                            field.name === "VehicleAvailablity" ||
                            field.name === "Remarks") {
                            $(field).prop("disabled", false);
                            $(field).prop("readonly", false);
                        }
                        break;
                }
            });
    };

    _$.GetCheckBoxValue = function (id) {
        return $(id).is(":checked");
    };

    _$.GetRadioButtonValue = function (name) {
        return $('input[name=' + name + ']:checked').val();
    };

    _$.SetRadioButtonByValue = function (name, value, status) {
        $("input[name='" + name + "'][value='" + value + "']").prop('checked', status).change();
    };

    _$.CheckUndefineType = function (val) {
        return (typeof val === 'undefined' || val === 'undefined' || val == null || val.length <= 0)
            ? true : false;
    };

    _$.getFormattedDateStr = function (dateStr) {
        var temp = dateStr.split(' ');
        var temp2 = temp[0].split('/');
        var temp3 = temp2[0].split('-');
        return temp2[2] + '/' + temp2[1] + '/' + temp3[0] + ' ' + temp[1];
    };

    _$.getFormattedDateStrByMonth = function (dateStr) {
        var temp = dateStr.split(' ');
        var temp2 = temp[0].split('/');
        var temp3 = temp2[0].split('-');
        return temp2[1] + '/' + temp2[2] + '/' + temp3[0] + ' ' + temp[1];
    };

    _$.isEmpty = function (val) {
        return val === undefined || val == null || val.length <= 0 && val !== "";
    };

    _$.isNotEmpty = function (val) {
        return val !== undefined && val !== null && val.length > 0 && val !== "";
    };

    _$.checkSessionTimeout = function () {

        var accessToken = JSON.parse(localStorage.getItem("_jwt"));
        if (accessToken) {

            var jwtExpiry = JSON.parse(atob(accessToken.access_token.split('.')[1]));

            if (jwtExpiry && jwtExpiry.exp < Date.now() / 1000) {
                localStorage.removeItem("_jwt");
                _$.redirectToUrl("\\" + "Account" + "\\" + "Login");
            }
        }
        else {
            _$.redirectToUrl("\\" + "Account" + "\\" + "Login");
        }
    };

    ////

    //// Ajax Helpers

    _$.GET = function (url, data, onsuccess) {
        var accessToken = JSON.parse(localStorage.getItem("_jwt"));
        var headers = {};
        if (accessToken) {
            headers.Authorization = 'Bearer ' + accessToken.access_token;
        }
        $.ajax({
            url: url,
            type: "GET",
            dataType: "JSON",
            data: data,
            headers: headers,
            success: onsuccess,
            error: function (jqXhr, textStatus, errorThrown) {
                console.log(textStatus, errorThrown);
            }
        });
    };

    _$.POST = function (url, data, onsuccess) {
        var accessToken = JSON.parse(localStorage.getItem("_jwt"));
        var headers = {};
        if (accessToken) {
            headers.Authorization = 'Bearer ' + accessToken.access_token;
        }
        $.ajax({
            url: url,
            type: "POST",
            dataType: "json",
            contentType: "application/json",
            async: true,
            data: JSON.stringify(data),
            headers: headers,
            success: onsuccess,
            error: function (jqXhr, textStatus, errorThrown) {
                console.log(textStatus, errorThrown);
            }
        });
    };

    _$.GETHTML = function (url, data, async, onsuccess) {
        var accessToken = JSON.parse(localStorage.getItem("_jwt"));
        var headers = {};
        if (accessToken) {
            headers.Authorization = 'Bearer ' + accessToken.access_token;
        }
        $.ajax({
            url: url,
            type: "POST",
            dataType: "html",
            contentType: "application/json",
            data: JSON.stringify(data),
            headers: headers,
            async: async,
            success: onsuccess,
            error: function (jqXhr, textStatus, errorThrown) {
                console.log(textStatus, errorThrown);
            }
        });
    };

    ////
}