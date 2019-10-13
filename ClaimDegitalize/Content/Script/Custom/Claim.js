/// <reference path="Extention.js" />

var _$ = new Extention();

var ClaimObj = (function () {

    var loadDetail = function (e, claimNo, moduleName, redirectModule) {

        var ClaimId = $(e).attr("name");

        var claimVM = {};

        $.each($("#ClaimHiddenField").find("input").serializeArray(), function (i, field) {
            if (i !== 0) {
                claimVM[field.name] = field.value;
            }
        });

        claimVM["ClaimId"] = ClaimId;
        claimVM["ClaimNo"] = claimNo;

        _$.POST("/Claim/" + moduleName, { "claimVM": claimVM }, function (result) {
            if (result !== null) {
                window.location.href = '/Claim/' + redirectModule;
            }
        });
    };

    var addVehicle = function (e) {
        $(e).toggleClass('expand').nextUntil('tr.header').slideToggle(100);
    };

    var setSelectedClaimType = function (id) {

        var claimId = $("#ClaimId").val();
        _$.GET("/Claim/GetClaimDetail",
            { 'claimId': claimId },
            function (result) {

                if (result !== null) {
              
                    _$.obj = result;

                    selectClaimType(id, 'add');
                    
                    if (!_$.obj.WorkshopCollection || _$.obj.WorkshopCollection.length <= 0) {
                        $("#WorkshopGrid").hide();
                    }

                    if (!_$.obj.RemarksList) {
                        $("#Remarks").hide();
                    }

                    $.each(_$.obj.RemarksList, function (i, prop) {
                        var d = new Date(prop.Add_On.match(/\d+/)[0] * 1);
                        var cols = "<tr><td>" + prop.Username + "</td>"
                            + "<td class='remarksdetail'>" + prop.Remarks + "</td>"
                            + "<td>" + d.toLocaleString() + "</td>"
                            + "<td style='visibility:hidden; border-color: white;'><input type='hidden' class='remarksClaimId' name='ClaimId' value='" + prop.ClaimForm_Id + "' /></td>"
                            + "<td style='visibility:hidden; border-color: white;'><input type='hidden' class='remarksId' name='RemarksId' value='" + prop.RemarksId + "' /></td>"
                            + "</tr>";
                        $(cols).appendTo("#tblRemarksList tbody");
                    });

                    _$.SetComboboxByText("#claimType", _$.obj.ClaimTypeName);
                    $(".js-example-basic-single").select2();

                    if (_$.GetValueCombobox("#claimType", $("#claimType").val()) !== "-1")
                        toggleWorkshop();

                    if (result.Circumstances) {
                        $("#Circumstances").text(result.Circumstances);
                    }
                }
            });
    };

    var selectClaimType = function (id, status) {

        var setSelectedContentHtml = function (result) {

            $("#ClaimTypeContent").html(result);

            //$(".form_datetime").datetimepicker({
            //    format: "yyyy-mm-dd hh:ii",
            //    autoclose: true,
            //    todayBtn: true,
            //    pickerPosition: "bottom-left"
            //});
        };

        var setSelectedClaimByType = function (claimTypeName) {

            switch (claimTypeName) {

                case "Accident":
                case "Rain Water Damage":
                    accidentRainWaterDamage(id);
                    break;

                case "Accessory Theft":
                    accessoryTheft(id);
                    break;

                case "Third Party":
                case "Third Party Coverage":
                    thirdPartyCoverage(id);
                    break;

                case "Snatch":
                case "Theft":
                    theftSnatch(id);
                    break;
            }
        };


        var claimTypeName = $("#claimType option:selected").text();
        if (claimTypeName.toLowerCase() === "theft/snatch recovered") {
            claimTypeName = claimTypeName.replace('/', ' ');
        }

        if (id.toLowerCase().includes("viewclaim") || id.toLowerCase().includes("editclaim")) {
            claimTypeName = $("#ClaimTypeName").val();
        }

        $("#ClaimTypeContent").html("");


        if ($("#claimType option:selected").text().toLowerCase() !== "death claim"
            && $("#claimType option:selected").text().toLowerCase() !== "total-loss"
            && claimTypeName !== "Select ") {

            $("#loading").show();

            _$.GETHTML('GetClaimTypeContent',
                { 'claimTypeName': claimTypeName },
                true,
                function (result) {

                    if (result) {
                    
                        $("#loading").hide();

                        setSelectedContentHtml(result);

                        if (id.toLowerCase().includes("viewclaim")) {
                            $(".workshops").hide();
                        }

                        if (claimTypeName.toLowerCase() === "theft/snatch recovered") {
                            claimTypeName = claimTypeName.replace('/', ' ');
                        }

                        setSelectedClaimByType(claimTypeName);

                        if (id.toLowerCase().includes("editclaim")) {
                            _$.DisabledEditField(id, status);
                            _$.validateFormFields();
                        }
                        else if (id.toLowerCase().includes("viewclaim")) {
                            _$.DisabledField(id, status);
                        }
                    }
                });
        }
    };

    var selectIncidentCity = function () {
        var cityId = _$.GetValueCombobox("#IncidentCity", $("#IncidentCity").val());

        if (cityId !== "0") {

            $("#IncidentArea").css("display", "block");

            _$.GET("/Claim/GetAreaByCity",
                { "cityId": cityId },
                function (result) {

                    _$.validateFormFields();

                    $("#IncidentArea").html("");
                    $("#IncidentArea").append("<option value=-1>--Please Select--</option>");
                    $.each(result,
                        function (i, area) {
                            $("#IncidentArea")
                                .append("<option value=" +
                                    area.Value +
                                    ">" +
                                    area.Text +
                                    "</option>");
                        });

                    if (window.location.href.toLowerCase().includes("create"))
                        _$.SetComboboxByValue("#CurrentCity", cityId);

                    _$.SetComboboxByValue("#IncidentArea", _$.obj.IncidentArea);

                });
        } else if (cityId === "0") {
            $("#IncidentArea, #CurrentArea").css("display", "none");
            _$.SetComboboxByValue("#CurrentCity", "0");
        }
    };

    var selectCurrentCity = function () {
        var cityId = _$.GetValueCombobox("#CurrentCity", $("#CurrentCity").val());

        if (cityId !== "0") {

            $("#CurrentArea").css("display", "block");

            _$.GET("/Claim/GetAreaByCity",
                { "cityId": cityId },
                function (result) {

                    _$.validateFormFields();

                    $("#CurrentArea").html("");
                    $("#CurrentArea").append("<option value=-1>--Please Select--</option>");
                    $.each(result,
                        function (i, area) {
                            $("#CurrentArea")
                                .append("<option value=" +
                                    area.Value +
                                    ">" +
                                    area.Text +
                                    "</option>");
                        });

                    _$.SetComboboxByValue("#CurrentArea", _$.obj.CurrentArea);
                });
        } else if (cityId === "0") {
            $("#CurrentArea").css("display", "none");
        }
    };

    var accidentRainWaterDamage = function (id) {

        toggleHeavyDamagePart();
        setSelectedDamageparts();
  
        _$.SetComboboxByValue("#Branch", _$.obj.Branch);
        _$.SetComboboxByValue("#IncidentCity", _$.obj.IncidentCity);
        _$.SetComboboxByValue("#CurrentCity", _$.obj.CurrentCity);
        _$.SetComboboxByValue("#RelationWithInsured", _$.obj.RelationWithInsured);

        if (_$.obj.Incidentdate) $("#Incidentdate").val(_$.obj.Incidentdate);
        if (_$.obj.IncidentDate) $("#Incidentdate").val(_$.obj.IncidentDate);

        $("#Circumstances").val(_$.obj.Circumstances);

        //view 
        if (id.toLowerCase() === "viewclaim") {
            _$.DisabledField("#EditClaim", "add");
        }

        if (id.toLowerCase().includes("viewclaim") || id.toLowerCase().includes("editclaim")) {

            $("#InsuredName").val(_$.obj.InsuredName);

            if (_$.obj.Mobile) $("#MobileNo").val(_$.obj.Mobile);
            if (_$.obj.MobileNo) $("#MobileNo").val(_$.obj.MobileNo);
 
            _$.SetComboboxByValue("#IncidentArea", _$.obj.IncidentArea);
            _$.SetComboboxByValue("#CurrentArea", _$.obj.CurrentArea);

            $("#Email").val(_$.obj.Email);
            $("#PhoneNo").val(_$.obj.PhoneNo);
        }
    };

    var accessoryTheft = function (id) {

        toggleHeavyDamagePart();
        setSelectedDamageparts();

        _$.SetComboboxByValue("#Branch", _$.obj.Branch);
        _$.SetComboboxByValue("#IncidentCity", _$.obj.IncidentCity);
        _$.SetComboboxByValue("#CurrentCity", _$.obj.CurrentCity);
        _$.SetComboboxByValue("#RelationWithInsured", _$.obj.RelationWithInsured);

        if (_$.obj.Incidentdate) $("#Incidentdate").val(_$.obj.Incidentdate);
        if (_$.obj.IncidentDate) $("#Incidentdate").val(_$.obj.IncidentDate);

        $("#Circumstances").val(_$.obj.Circumstances);


        //view 
        if (id.toLowerCase() === "viewclaim") {
            _$.DisabledField("#EditClaim", "add");
        }

        if (id.toLowerCase().includes("viewclaim") || id.toLowerCase().includes("editclaim")) {
            $("#InsuredName").val(_$.obj.InsuredName);

            if (_$.obj.Mobile) $("#MobileNo").val(_$.obj.Mobile);
            if (_$.obj.MobileNo) $("#MobileNo").val(_$.obj.MobileNo);

            _$.SetComboboxByValue("#IncidentArea", _$.obj.IncidentArea);
            _$.SetComboboxByValue("#CurrentArea", _$.obj.CurrentArea);

            $("#Email").val(_$.obj.Email);
            $("#PhoneNo").val(_$.obj.PhoneNo);
        }
    };

    var thirdPartyCoverage = function (id) {

        $(document).off('event', '#IsThirdPartyAccident').on('event', '#IsThirdPartyAccident', function () {
            if (_$.GetRadioButtonValue("IsThirdPartyAccident") === 'yes') {
                $("#DamageObjectType").removeClass("hidden");
            } else {
                $("#DamageObjectType").addClass("hidden");
            }
        });

        _$.SetComboboxByValue("#Branch", _$.obj.Branch);
        _$.SetComboboxByValue("#IncidentCity", _$.obj.IncidentCity);
        _$.SetComboboxByValue("#CurrentCity", _$.obj.CurrentCity);
        _$.SetComboboxByValue("#RelationWithInsured", _$.obj.RelationWithInsured);
        if (_$.obj.Incidentdate) $("#Incidentdate").val(_$.obj.Incidentdate);
        if (_$.obj.IncidentDate) $("#Incidentdate").val(_$.obj.IncidentDate);
        _$.SetCheckBox("#IsThirdPartyAccident", _$.obj.IsThirdPartyAccident === "yes");

        //view 
        if (id.toLowerCase() === "viewclaim") {
            _$.DisabledField("#EditClaim", "add");
        }

        if (id.toLowerCase().includes("viewclaim") || id.toLowerCase().includes("editclaim")) {
            $("#InsuredName").val(_$.obj.InsuredName);
            $("#MobileNo").val(_$.obj.MobileNo);
            _$.SetComboboxByValue("#IncidentArea", _$.obj.IncidentArea);
            _$.SetComboboxByValue("#CurrentArea", _$.obj.CurrentArea);
            $("#Email").val(_$.obj.Email);
            $("#PhoneNo").val(_$.obj.PhoneNo);
        }
    };

    var theftSnatch = function (id) {

        $(document).off('event', '#TrakkerInstall').on('event', '#TrakkerInstall', function () {
            if (_$.GetRadioButtonValue("TrakkerInstall") === 'yes') {
                $(".TrakkerInstallDiv").removeClass("hidden");
            } else {
                $(".TrakkerInstallDiv").addClass("hidden");
            }
        });

        $(document).off('event', '#IntimateService').on('event', '#IntimateService', function () {

            if (_$.GetRadioButtonValue("IntimateService") === 'yes') {
                $(".IntimateServiceDiv").removeClass("hidden");
            } else {
                $(".IntimateServiceDiv").addClass("hidden");
            }
        });

        if (_$.obj.IntimateService === "yes") {
            _$.SetRadioButtonByValue("IntimateService", _$.obj.IntimateService, true);
        }

        if (_$.obj.TrakkerInstall === "yes") {
            _$.SetRadioButtonByValue("TrakkerInstall", _$.obj.TrakkerInstall, true);
        }

        _$.SetComboboxByValue("#Branch", _$.obj.Branch);
        _$.SetComboboxByValue("#IncidentCity", _$.obj.IncidentCity);
        _$.SetComboboxByValue("#CurrentCity", _$.obj.CurrentCity);
        _$.SetComboboxByValue("#RelationWithInsured", _$.obj.RelationWithInsured);
        _$.SetComboboxByValue("#TrackerCompanyName", _$.obj.TrackerCompanyName);

        $("#Incidentdate").val(_$.obj.IncidentDate);
        $("#Circumstances").val(_$.obj.Circumstances);
        $("#ComplaintNo").val(_$.obj.ComplaintNo);

        //view 
        if (id.toLowerCase() === "viewclaim") {
            _$.DisabledField("#EditClaim", "add");
        }

        if (id.toLowerCase().includes("viewclaim") || id.toLowerCase().includes("editclaim")) {
            $("#InsuredName").val(_$.obj.InsuredName);
            $("#MobileNo").val(_$.obj.MobileNo);
            _$.SetComboboxByValue("#IncidentArea", _$.obj.IncidentArea);
            _$.SetComboboxByValue("#CurrentArea", _$.obj.CurrentArea);
            $("#Email").val(_$.obj.Email);
            $("#PhoneNo").val(_$.obj.PhoneNo);
        }
    };

    var getWorkshopCollection = function () {
        var myTableArray = [];

        $("table#WorkshopGrid tbody tr").each(function () {

            var tableData = $(this).find('td');
            var tableObj = {};
            tableObj.WorkshopId = "";
            tableObj.WorkshopType = "";
            tableObj.WorkshopName = "";
            tableObj.VehicleAvailablity = "";

            if (tableData.length > 0) {

                tableData.each(function (i, v) {
                    if (i === 0) {
                        tableObj.WorkshopId = v.children[0].value;
                    }
                    else if (i === 1) {
                        tableObj.WorkshopType = $(this).text();
                    }
                    else if (i === 2) {
                        tableObj.WorkshopName = $(this).text();
                    }
                    else if (i === 3) {
                        tableObj.VehicleAvailablity = $(this).text();
                    }
                });

                myTableArray.push(tableObj);
            }
        });

        return myTableArray;
    };

    var populateWorkshopGrid = function () {

        if (location.href.includes("Edit") || location.href.includes("View")) {
            var html = "";

            $.each(_$.obj.WorkshopCollection, function (i, prop) {
                html += "<tr><td><input type='hidden' value='" + prop.WorkshopId + "' /></td><td>" +
                    prop.WorkshopType + "</td><td class='WorkshopName'>" +
                    prop.WorkshopName + "</td><td>" +
                    prop.VehicleAvailablity + "</td></tr>";
            });

            $("#WorkshopGrid > tbody").append(html);
        }
    };

    var getWorkshops = function () {

        var workshopType = _$.GetValueCombobox("#WorkshopType", $("#WorkshopType").val());
        $("#loading").show();

        _$.GET("/Claim/GetWorkshop",
            { "workshopType": workshopType },
            function (result) {

                $("#loading").hide();
                $("#Workshop").html("");
                $("#Workshop").append("<option value=-1>--Please Select--</option>");

                $.each(result,
                    function (i, data) {

                        $("#Workshop")
                            .append("<option value=" +
                                data.Value +
                                ">" +
                                data.Text +
                                "</option>");
                    });

                _$.SetComboboxByValue("#Workshop", _$.obj.Workshop);
            });
    };

    var saveAccident = function (id) {
        var accident = {};
        var damageParent = [];

        if ($('.form-horizontal').valid()) {
            // edit and view
            if (id.toLowerCase().includes("viewclaim") || id.toLowerCase().includes("editclaim")) {
                accident["ClaimId"] = $("#ClaimId").val();
                accident["HeavyDamagePart"] = false;
            }

            if ($("#IncidentCity option:selected").val() === "0" || $("#CurrentCity option:selected").val() === "0"
                || $("#IncidentArea option:selected").val() === "-1" || $("#CurrentArea option:selected").val() === "-1") {
                alert("Must provide area of incident and current area");
            }
            else if ($("#Circumstances").val() === "" && !$("#Circumstances").val()) {
                alert("Must provide Circumstances of loss");
            }
            else if ($("#Workshop option:selected").val() === "-1" && $("#Workshop option:selected").val()) {
                alert("Must add workshop in grid");
            }
            else if ($("#Branch option:selected").val() === "" && $("#Branch option:selected").val()) {
                alert("Must provide branch");
            }
            else if ($("table#WorkshopGrid tbody tr").length === 0) {
                alert("Must provide workshop details including vehicle availability");
            }
            else if (localStorage.getItem("Invalid CellNo")) {
                alert(localStorage.getItem("Invalid CellNo"));
            }
            else {

                $.each($('.form-horizontal').serializeArray(),
                    function (i, field) {

                        if (field.name === "HeavyDamagePart") {
                            if ($("#" + field.name).is(":checkbox") || $(field).is(':radio')) {
                                accident[field.name] = true;
                            }
                        } else if (field.name === "Damageparts") {

                            damageParent = $("#Damageparts").val();
                            var damageText = "";

                            for (var d in damageParent) {
                                if (damageParent.hasOwnProperty(d)) {
                                    damageText += _$.GetTextCombobox("#Damageparts", damageParent[d]) + ";";
                                }
                            }

                            if (_$.GetCheckBoxValue("#HeavyDamagePart") === true) {

                                damageText = damageText.slice(0, damageText.length - 1);
                                damageText += ":" + $("#HeavyDamagePartRemarks").val() + ";";
                            }

                            accident[field.name] = damageText;

                        } else if (field.name === "IncidentArea" && field.value !== "-1") {
                            accident[field.name] = field.value;
                        } else if (field.name === "CurrentArea" && field.value !== "-1") {
                            accident[field.name] = field.value;
                        } else if (field.name !== "IncidentArea" && field.name !== "CurrentArea") {
                            accident[field.name] = field.value;
                        }
                    });

                var remarksList = [];

                $("#tblRemarksList tbody tr").each(function (i, val) {
                    if ($(this).find('td:eq(4)').html() === undefined)
                        remarksList.push($(this).find('td:eq(1)').text());
                });

                accident["Remarks"] = remarksList;
                accident["Branch"] = $("#Branch option:selected").val();

                if ($("#InsuredMobileNo").val() && $("#RelationWithInsured option:selected").val() === "5")
                    accident["MobileNo"] = $("#InsuredMobileNo").val();

                if ($("#RelationWithInsured").val())
                    accident["RelationWithInsured"] = $("#RelationWithInsured").val();

                if ($("#InsuredName").val())
                    accident["InsuredName"] = $("#InsuredName").val();

                var accessToken = JSON.parse(localStorage.getItem("_jwt"));
                var jwtExpiry = JSON.parse(atob(accessToken.access_token.split('.')[1]));
                accident["UserId"] = jwtExpiry.UserId;

                accident["WorkshopCollection"] = getWorkshopCollection();

                if (location.href.toLowerCase().includes("edit"))
                    accident["claimType"] = _$.obj.claimType;

                accident["ReserveAmount"] = $("#ReserveAmount").val();

                $("#loading").show();

                _$.POST("/Claim/SaveAccident",
                    { "claimVM": accident },
                    function (result) {
                        if (result !== null) {
                            $("#loading").hide();
                            localStorage.removeItem("Invalid CellNo");
                            _$.Notification("", 100);
                            if (location.href.toLowerCase().includes("create")) {
                                _$.Notification(result[0].ClaimNo, 100);
                                $("#GeneratedClaimDetail").show();
                                $("#GeneratedClaimDetail>p>span").html(result[0].ClaimNo);
                            }

                            $("#Save").attr("disabled", "disabled");
                        }
                    });
            }
        }
    };

    var saveAccessoriesTheft = function (id) {


        var accessoriesTheft = {};

        if ($('.form-horizontal').valid()) {

            // edit or view
            if (id.toLowerCase().includes("viewclaim") || id.toLowerCase().includes("editclaim")) {
                accessoriesTheft["ClaimId"] = $("#ClaimId").val();
                accessoriesTheft["HeavyDamagePart"] = false;
            }

            if ($("#IncidentCity option:selected").val() === "0" || $("#CurrentCity option:selected").val() === "0"
                || $("#IncidentArea option:selected").val() === "-1" || $("#CurrentArea option:selected").val() === "-1") {
                alert("Must provide area of incident and current area");
            }
            else if ($("#Circumstances").val() === "" && !$("#Circumstances").val()) {
                alert("Must provide Circumstances of loss");
            }
            else if ($("#Workshop option:selected").val() === "-1" && $("#Workshop option:selected").val()) {
                alert("Must add workshop in grid");
            }
            else if ($("#Branch option:selected").val() === "" && $("#Branch option:selected").val()) {
                alert("Must provide branch");
            }
            else if ($("table#WorkshopGrid tbody tr").length === 0) {
                alert("Must provide workshop details including vehicle availability");
            }
            else if (localStorage.getItem("Invalid CellNo")) {
                alert(localStorage.getItem("Invalid CellNo"));
            }
            else {

                $.each($('.form-horizontal').serializeArray(),
                    function (i, field) {

                        if (field.name === "HeavyDamagePart") {
                            if ($("#" + field.name).is(":checkbox") || $(field).is(':radio')) {
                                accessoriesTheft[field.name] = true;
                            }
                        } else if (field.name === "Damageparts") {
                            var damageParent = $("#Damageparts").val();
                            var damageText = "";

                            for (var d in damageParent) {
                                if (damageParent.hasOwnProperty(d)) {
                                    damageText += _$.GetTextCombobox("#Damageparts", damageParent[d]) + ";";
                                }
                            }

                            if (_$.GetCheckBoxValue("#HeavyDamagePart") === true) {
                                damageText = damageText.slice(0, damageText.length - 1);
                                damageText += ":" + $("#HeavyDamagePartRemarks").val() + ";";
                            }

                            accessoriesTheft[field.name] = damageText;
                        } else if (field.name === "IncidentArea" && field.value !== "-1") {
                            accessoriesTheft[field.name] = field.value;
                        } else if (field.name === "CurrentArea" && field.value !== "-1") {
                            accessoriesTheft[field.name] = field.value;
                        } else if (field.name !== "IncidentArea" && field.name !== "CurrentArea") {
                            accessoriesTheft[field.name] = field.value;
                        }
                    });

                var remarksList = [];

                $("#tblRemarksList tbody tr").each(function (i, val) {
                    if ($(this).find('td:eq(4)').html() === undefined)
                        remarksList.push($(this).find('td:eq(1)').text());
                });

                accessoriesTheft["Remarks"] = remarksList;
                accessoriesTheft["Branch"] = $("#Branch option:selected").val();

                if ($("#InsuredMobileNo").val() && $("#RelationWithInsured option:selected").val() === "5")
                    accessoriesTheft["MobileNo"] = $("#InsuredMobileNo").val();

                if ($("#RelationWithInsured").val())
                    accessoriesTheft["RelationWithInsured"] = $("#RelationWithInsured").val();

                if ($("#InsuredName").val())
                    accessoriesTheft["InsuredName"] = $("#InsuredName").val();

                var accessToken = JSON.parse(localStorage.getItem("_jwt"));
                var jwtExpiry = JSON.parse(atob(accessToken.access_token.split('.')[1]));
                accessoriesTheft["UserId"] = jwtExpiry.UserId;

                accessoriesTheft["WorkshopCollection"] = getWorkshopCollection();

                if (location.href.includes("Edit"))
                    accessoriesTheft["claimType"] = _$.obj.claimType;

                accessoriesTheft["ReserveAmount"] = $("#ReserveAmount").val();

                $("#loading").show();

                _$.POST("/Claim/SaveAccessoriesTheft",
                    { "claimVM": accessoriesTheft },
                    function (result) {
                        if (result !== null) {

                            $("#loading").hide();
                            localStorage.removeItem("Invalid CellNo");
                            _$.Notification("", 100);
                            if (location.href.includes("Create")) {
                               _$.Notification(result[0].ClaimNo, 100);
                                $("#GeneratedClaimDetail").show();
                                $("#GeneratedClaimDetail>p>span").html(result[0].ClaimNo);
                            }
                            $("#Save").attr("disabled", "disabled");

                        }
                    });
            }
        }
    };

    var saveTheftSnatch = function (id) {
        var theftSnatch = {};

        if ($('.form-horizontal').valid()) {

            // edit and view
            if (id.toLowerCase().includes("viewclaim") || id.toLowerCase().includes("editclaim")) {
                theftSnatch["ClaimId"] = $("#ClaimId").val();
                theftSnatch["HeavyDamagePart"] = false;
            }

            if ($("#IncidentCity option:selected").val() === "0" || $("#CurrentCity option:selected").val() === "0"
                || $("#IncidentArea option:selected").val() === "-1" || $("#CurrentArea option:selected").val() === "-1") {
                alert("Must provide area of incident and current area");
            }
            else if ($("#Circumstances").val() === "" && !$("#Circumstances").val()) {
                alert("Must provide Circumstances of loss");
            }
            else if ($("#Branch option:selected").val() === "" && $("#Branch option:selected").val()) {
                alert("Must provide branch");
            }
            else if (localStorage.getItem("Invalid CellNo")) {
                alert(localStorage.getItem("Invalid CellNo"));
            }
            else {
                $.each($('.form-horizontal').serializeArray(), function (i, field) {

                    if (field.name === "HeavyDamagePart") {
                        if ($("#" + field.name).is(":checkbox") || $(field).is(':radio')) {
                            theftSnatch[field.name] = true;
                        }
                    }
                    else if (field.name === "Damageparts") {
                        var damageParent = $("#Damageparts").val();
                        var damageText = "";

                        for (var d in damageParent) {
                            if (damageParent.hasOwnProperty(d)) {
                                damageText += _$.GetTextCombobox("#Damageparts", damageParent[d]) + ";";
                            }
                        }

                        if (_$.GetCheckBoxValue("#HeavyDamagePart") === true) {
                            damageText = damageText.slice(0, damageText.length - 1);
                            damageText += ":" + $("#HeavyDamagePartRemarks").val() + ";";
                        }

                        theftSnatch[field.name] = damageText;
                    }
                    else if (field.name === "IncidentArea" && field.value !== "-1") {
                        theftSnatch[field.name] = field.value;
                    }
                    else if (field.name === "CurrentArea" && field.value !== "-1") {
                        theftSnatch[field.name] = field.value;
                    }
                    else if (field.name !== "IncidentArea" && field.name !== "CurrentArea") {
                        theftSnatch[field.name] = field.value;
                    }
                });

                var remarksList = [];

                $("#tblRemarksList tbody tr").each(function (i, val) {
                    if ($(this).find('td:eq(4)').html() === undefined)
                        remarksList.push($(this).find('td:eq(1)').text());
                });

                theftSnatch["Remarks"] = remarksList;
                theftSnatch["Branch"] = $("#Branch option:selected").val();

                if ($("#InsuredMobileNo").val() && $("#RelationWithInsured option:selected").val() === "5")
                    theftSnatch["MobileNo"] = $("#InsuredMobileNo").val();

                if ($("#RelationWithInsured").val())
                    theftSnatch["RelationWithInsured"] = $("#RelationWithInsured").val();

                if ($("#InsuredName").val())
                    theftSnatch["InsuredName"] = $("#InsuredName").val();

                var accessToken = JSON.parse(localStorage.getItem("_jwt"));
                var jwtExpiry = JSON.parse(atob(accessToken.access_token.split('.')[1]));
                theftSnatch["UserId"] = jwtExpiry.UserId;


                if (location.href.includes("Edit"))
                    theftSnatch["claimType"] = _$.obj.claimType;

                theftSnatch["ReserveAmount"] = $("#ReserveAmount").val();

                $("#loading").show();

                _$.POST("/Claim/SaveTheftSnatch",
                    { "claimVM": theftSnatch },
                    function (result) {
                        if (result !== null) {
                            $("#loading").hide();
                            localStorage.removeItem("Invalid CellNo");
                            _$.Notification("", 100);
                            if (location.href.includes("Create")) {
                                _$.Notification(result[0].ClaimNo, 100);
                                $("#GeneratedClaimDetail").show();
                                $("#GeneratedClaimDetail>p>span").html(result[0].ClaimNo);
                            }
                            $("#Save").attr("disabled", "disabled");
                        }
                    });
            }
        }
    };

    var saveThirdPartyCoverage = function () {

        var thirdPartyCoverage = {};

        if ($('.form-horizontal').valid()) {

            if ($("#IncidentCity option:selected").val() === "0" || $("#CurrentCity option:selected").val() === "0"
                || $("#IncidentArea option:selected").val() === "-1" || $("#CurrentArea option:selected").val() === "-1") {
                alert("Must provide area of incident and current area");
            }
            else if ($("#Circumstances").val() === "" && !$("#Circumstances").val()) {
                alert("Must provide Circumstances of loss");
            }
            else if ($("#IsThirdPartyAccident").is(':checked') && $("#VehicleDetailDD option:selected").val().toLowerCase() === "select") {
                alert("Must provide third party vehicle detail");
            }
            else if ($("#Branch option:selected").val() === "" && $("#Branch option:selected").val()) {
                alert("Must provide branch");
            }
            else if (localStorage.getItem("Invalid CellNo")) {
                alert(localStorage.getItem("Invalid CellNo"));
            }
            else {

                $.each($('.form-horizontal').serializeArray(),
                    function (i, field) {

                        if (field.name === "Damageparts") {
                            var damageParent = $("#Damageparts").val();
                            var damageText = "";

                            for (var d in damageParent) {
                                if (damageParent.hasOwnProperty(d)) {
                                    damageText += _$.GetTextCombobox("#Damageparts", damageParent[d]) + ";";
                                }
                            }

                            if (_$.GetCheckBoxValue("#HeavyDamagePart") === true) {
                                damageText = damageText.slice(0, damageText.length - 1);
                                damageText += ":" + $("#HeavyDamagePartRemarks").val() + ";";
                            }

                            thirdPartyCoverage[field.name] = damageText;
                        } else if (field.name === "IncidentArea" && field.value !== "-1") {
                            thirdPartyCoverage[field.name] = field.value;
                        } else if (field.name === "CurrentArea" && field.value !== "-1") {
                            thirdPartyCoverage[field.name] = field.value;
                        } else if (field.name !== "IncidentArea" && field.name !== "CurrentArea") {
                            thirdPartyCoverage[field.name] = field.value;
                        }
                    });

                var remarksList = [];

                $("#tblRemarksList tbody tr").each(function (i, val) {

                    if ($(this).find('td:eq(4)').html() === undefined)
                        remarksList.push($(this).find('td:eq(1)').text());

                });

                thirdPartyCoverage["Remarks"] = remarksList;
                thirdPartyCoverage["ClaimId"] = $("#ClaimId").val();
                thirdPartyCoverage["Branch"] = $("#Branch option:selected").val();

                var accessToken = JSON.parse(localStorage.getItem("_jwt"));
                var jwtExpiry = JSON.parse(atob(accessToken.access_token.split('.')[1]));
                thirdPartyCoverage["UserId"] = jwtExpiry.UserId;

                if (location.href.includes("Edit"))
                    thirdPartyCoverage["claimType"] = _$.obj.claimType;

                thirdPartyCoverage["ReserveAmount"] = $("#ReserveAmount").val();

                $("#loading").show();
                
                _$.POST("/Claim/SaveThirdPartyCoverage",
                    { "claimVM": thirdPartyCoverage },
                    function (result) {
                        if (result !== null) {
                            $("#loading").hide();
                            localStorage.removeItem("Invalid CellNo");
                            _$.Notification("", 100);
                            if (location.href.includes("Create")) {
                                _$.Notification(result[0].ClaimNo, 100);
                                $("#GeneratedClaimDetail").show();
                                $("#GeneratedClaimDetail>p>span").html(result[0].ClaimNo);
                            }
                            $("#Save").attr("disabled", "disabled");
                        }
                    });
            }
        }
    };

    var save = function (id) {

        var text = _$.GetTextCombobox("#claimType", $("#claimType").val());
        _$.ValidIncidentDate = localStorage.getItem("ValidIncidentDate") === "true";
        _$.ValidVADate = localStorage.getItem("ValidVADate") === "true";

        var savebymodule = function () {
            switch (text) {

                case "Accident":
                case "Rain Water Damage":
                    saveAccident(id);
                    break;

                case "Theft":
                case "Snatch":
                    saveTheftSnatch(id);
                    break;

                case "Accessory Theft":
                    saveAccessoriesTheft(id);
                    break;

                case "Third Party Coverage":
                case "Third Party":
                    saveThirdPartyCoverage();
                    break;
            }
        };

        _$.validateFormFields();

        if (location.href.includes("Edit")) {
            $("#VehicleAvailablity, #Workshop, #WorkshopType").prop('required', false);
        }

        if (window.location.href.toLowerCase().includes("edit") && _$.ValidVADate) {
            savebymodule();
        }
        else if (_$.ValidIncidentDate && _$.ValidVADate) {
            savebymodule();
        }
    };

    var isHeavyDamagePart = function () {
        $("#heavyDamageRemarks").addClass("hidden");
        if (_$.GetCheckBoxValue("#HeavyDamagePart") === true) {
            $("#heavyDamageRemarks").removeClass("hidden");
        }
    };

    var selectIncidentArea = function () {
        var areaId = _$.GetValueCombobox("#IncidentArea", $("#IncidentArea").val());
        if (areaId !== "0") {
            $("#IncidentArea").css("display", "block");
            _$.SetComboboxByValue("#CurrentArea", areaId);
        } else if (areaId === "0") {
            $("#IncidentArea").css("display", "none");
        }
    };

    var toggleHeavyDamagePart = function () {
        if (_$.obj.HeavyDamagePart) {
            $("#HeavyDamagePart, #lblHeavyDamagePart").show();
            _$.SetCheckBox("#HeavyDamagePart", true);
        }
        else {
            $("#HeavyDamagePart, #lblHeavyDamagePart").hide();
        }
    };

    var addWorkshopToGrid = function () {

        if (location.href.includes("Edit")) {
            $("#VehicleAvailablity, #Workshop, #WorkshopType").prop('required', true);
        }

        var workshopId = $("#Workshop option:selected").val();
        var workshopType = $("#WorkshopType option:selected").val();
        var workshopName = "";
        var vehicleAvailablity = $("#VehicleAvailablity").val();
        var workshopGrid = $("#WorkshopGrid > tbody > tr > td");

        if (workshopId && workshopId === "-1" || workshopType && workshopType === "-1") {
            alert("Must provide workshop details");
            $("#WorkshopType").addClass("required");
            $("#Workshop").addClass("required");
            return;
        }

        if (workshopId === "0") {
            workshopName = $("#WorkshopName").val();
        }
        else {
            workshopName = $("#Workshop option:selected").text();
        }

        if (workshopType === "Doorstep") {
            workshopId = "-1";
            workshopName = "Doorstep";
        }

        var html = ["<tr><td><input type='hidden' value='",
            workshopId, "' /></td><td>",
            workshopType, "</td><td class='WorkshopName'>",
            workshopName, "</td><td>",
            vehicleAvailablity, "</td></tr>"].join('');

        var addvalidatedWorkshop = function () {

            if (workshopGrid.length > 0 && workshopName !== "" && vehicleAvailablity !== "") {

                var flag = false;

                $('table tbody tr td:contains("' + workshopName + '")').filter(function () {
                    flag = $.trim($(this).text()) === workshopName;
                });

                if (flag) {
                    alert("Can't add existing value to Workshops list");
                    return;
                }
                
                $("#WorkshopGrid > tbody").append(html);
                _$.count++;
            }
            else if (_$.count < 1 && workshopName !== "--Please Select--" && workshopName !== "" && vehicleAvailablity !== "") {

                $("#WorkshopGrid > tbody").append(html);
                _$.count++;
            }
            else if (_$.count > 1) {
                alert("Only one workshop addition allowed");
            }
        };
       
        if (localStorage.getItem("ValidVADate") !== "true") {
            alert("Vehicle availability date must be greater than incident date");
        }
        else {
            addvalidatedWorkshop();
        }
    };

    var vehicleDetailChange = function () {

        $("#VehicleDetail").addClass("hidden");
        $("#OtherVehicle").addClass("hidden");

        if (_$.GetValueCombobox("#VehicleDetailDD", $("#VehicleDetailDD").val()) === "Vehicle") {
            $("#VehicleDetail").removeClass("hidden");
        } else {
            $("#OtherVehicle").removeClass("hidden");
        }
    };

    var toggleWorkshop = function () {
      
        //$("#WorkshopDiv, #WorkshopNameDiv").hide();
        $(".workshops").hide();

        if (window.location.href.toLowerCase().includes("edit") ||
            window.location.href.toLowerCase().includes("view")) {

            if (_$.obj.Workshop !== "-1") {
                //$("#WorkshopDiv").show();
                $(".workshops").show();
                $("#Workshop option:selected").val(_$.obj.Workshop).change();
            }
            else if (_$.obj.WorkshopType !== "-1") {
                //$("#WorkshopNameDiv").show();
                $(".workshops").show();
            }
        }
    };

    var setSelectedDamageparts = function () {

        var damagepartsList;

        if (_$.obj.Damageparts) {

            if (_$.obj.Damageparts.length > 0) {

                if (_$.obj.Damageparts.includes(":;")) {
                    damagepartsList = _$.obj.Damageparts.split(':;');
                }
                else if (_$.obj.Damageparts.includes(":")) {
                    damagepartsList = _$.obj.Damageparts.split(':');
                }
                else if (_$.obj.Damageparts.includes(";")) {
                    damagepartsList = _$.obj.Damageparts.split(';');
                }


                $('#Damageparts').val(damagepartsList).trigger("change");
            }
        }
    };

    var changeWorkshop = function () {

        var workshop = _$.GetValueCombobox("#Workshop", $("#Workshop").val());

        if (workshop === "0") {
            $("#WorkshopNameDiv").show();
            $("#WorkshopNameDiv").val("");
        } else {
            $("#WorkshopNameDiv").hide();
        }
    };

    var getWorkshop = function () {

        var workshopType = _$.GetValueCombobox("#WorkshopType", $("#WorkshopType").val());
        var workshop = _$.GetValueCombobox("#Workshop", $("#Workshop").val());

        if (workshopType.toLowerCase() === "doorstep") {
            $("#WorkshopDiv, #WorkshopNameDiv").hide();
        }
        else if (workshopType === "-1" && workshopType.toLowerCase() === "doorstep") {
            $("#WorkshopDiv").hide();
        }
        else if (workshop === "0" && workshopType !== "-1" && workshop !== "-1") {
            $("#WorkshopDiv").show();
            $("#WorkshopNameDiv").show();
            $("#WorkshopName").val("");
        }
        else if (workshop === "0" && workshopType === "-1") {
            $("#WorkshopDiv").hide();
            $("#WorkshopNameDiv").hide();
        }
        else if (workshopType === "-1") {
            $("#WorkshopDiv, #WorkshopNameDiv").hide();
        }
        else {
            getWorkshops();
            $("#WorkshopDiv").show();
            $("#WorkshopNameDiv").hide();
        }
    };

    var preSelectCreateCustomer = function (insuredName, mobileNo) {

        if ($("#RelationWithInsured option:selected").val() === "5") {
            $("#InsuredName").val(insuredName);
            $("#MobileNo").val(mobileNo);
        }
        else {
            $("#InsuredName").val("");
            $("#MobileNo").val("");
        }
    };

    var preSelectEditCustomer = function (insuredName, mobileNo) {

        var contactFields = $("#RelationWithInsured, #InsuredName, #MobileNo, #Email, #PhoneNo");

        if ($("#RelationWithInsured option:selected").val() === "5") {
            $("#InsuredName").val(insuredName);
            $("#MobileNo").val(mobileNo);
            contactFields.prop("disabled", true).prop("readonly", true);
        }
        else {
            contactFields.removeAttr('disabled').prop("disabled", false);
        }
    };

    return {

        AddVehicle: addVehicle,
        LoadDetail: loadDetail,
        SetSelectedClaimType: setSelectedClaimType,
        SelectClaimType: selectClaimType,
        SelectIncidentCity: selectIncidentCity,
        SelectCurrentCity: selectCurrentCity,
        PopulateWorkshopGrid: populateWorkshopGrid,
        GetWorkshops: getWorkshops,
        Save: save,
        AddWorkshopToGrid: addWorkshopToGrid,
        VehicleDetailChange: vehicleDetailChange,
        ToggleWorkshop: toggleWorkshop,
        GetWorkshop: getWorkshop,
        ChangeWorkshop: changeWorkshop,
        setSelectedDamageparts: setSelectedDamageparts,
        toggleHeavyDamagePart: toggleHeavyDamagePart,
        SelectIncidentArea: selectIncidentArea,
        IsHeavyDamagePart: isHeavyDamagePart,
        PreSelectCreateCustomer: preSelectCreateCustomer,
        PreSelectEditCustomer: preSelectEditCustomer
    };

}());