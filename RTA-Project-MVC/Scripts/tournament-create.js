
$(document).ready(function () {
    $("#checkbox2stage").click(function () {
        if ($(this).is(":checked")) {
            $("#stage1desc").text("Формат 1го этапа");
            $("#stage2div").show();
        }

        else {
            $("#stage1desc").text("Формат турнира");
            $("#stage2div").hide();
        }

    });
});

$(document).ready(function () {
    $("#checkboxseasonal").click(function () {
        if ($(this).is(":checked"))
            $("#seasondiv").show();

        else
            $("#seasondiv").hide();

    });
});

$(document).ready(function () {
    $("#checkboxofficial").click(function () {
        if ($(this).is(":checked"))
            $("#yeardiv").show();
        else
            $("#yeardiv").hide();

    });
});



$(document).ready(function () {
    $('.selectpicker').selectpicker({
        liveSearch: true,
        showSubtext: true
    });
});