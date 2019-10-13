$(document).on('change', '#IntimateService', function () {
    debugger
    if (_$.GetRadioButtonValue("IntimateService") == 'yes') {
        $(".IntimateServiceDiv").removeClass("hidden");
    } else {
        $(".IntimateServiceDiv").addClass("hidden");
    }
});


$(document).on('change', '#TrakkerInstall', function () {
    if (_$.GetRadioButtonValue("TrakkerInstall") == 'yes') {
        $(".TrakkerInstallDiv").removeClass("hidden");
    } else {
        $(".TrakkerInstallDiv").addClass("hidden");
    }
});
