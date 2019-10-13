function GetWorkshop() {
    debugger;
    $("#Workshop").parent().parent().removeClass("Doorstep");

    var workshopType = _$.GetValueCombobox("#WorkshopType", $("#WorkshopType").val());

    if (workshopType !== "-1") {

        $("#Workshop").css("display", "block");

        if (_$.GetValueCombobox("#WorkshopType", $("#WorkshopType").val()) === "Doorstep") {
            $("#Workshop").parent().parent().addClass("Doorstep");
        }
        else {
            _$.Get("/Claim/GetWorkshop",
                { "workshopType": workshopType },
                function (result) {
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
                    _$.SetComboboxByValue("#Workshop", -1);
                });
        }
    } else {
        $("#Workshop").css("display", "none");
    }
}

$(document).ready(function () {
    $(".js-example-basic-single").select2();
});
