function GetWorkshop() {
    debugger;
    var workshopType = _$.GetValueCombobox("#WorkshopType", $("#WorkshopType").val());

    if (workshopType !== "-1") {

        $("#Workshop").css("display", "block");

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
    } else {
        $("#Workshop").css("display", "none");
    }
}

//Claim Type Selection
var hideAll = function () {
    $('div[class^=claim]').hide();
}
$('#claimType').on('change', function () {
    hideAll();
    var name = _$.GetTextCombobox("#claimType", $(this).val());
    name = name.replace(/\s/g, '');
    //var category = $(this).val();
    $('.' + name).show();
});

$(document).ready(function () {
    $(".js-example-basic-single").select2();
    _$.toggleWorkshop();
    hideAll();
});
