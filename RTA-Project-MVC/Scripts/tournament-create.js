
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

    $(".plus-btn").click(function () {

        var hostId = $("#hosts-dropdown").val();

        if (!$('#' + hostId).length) {

            var hostName = $("#hosts-dropdown option:selected").text();
            var hostWrapper = $(document.createElement('div')).attr('id', hostId);
            var hiddenInput = $(document.createElement('input')).attr({ 'type': 'hidden', 'name': 'selectedHost', 'value': hostId });
            var nameLabel = $(document.createElement('label')).attr('class', 'host-name').append(hostName);
            var minusBtn = $(document.createElement('div')).attr('class', 'btn jog-btn minus-btn');

            $(hiddenInput).appendTo(nameLabel);
            $(nameLabel).appendTo(hostWrapper);
            $(minusBtn).appendTo(hostWrapper);

            $(hostWrapper).appendTo("#hosts-container");

        }

    });
});

$(document).on('click', ".minus-btn", function () {

    var id = $(this).parent().attr('id');
    $('#' + id).remove();

});

$(document).ready(function () {

    $("#hosts-dropdown").change(function () {

        if ($(this).val() != "") {
            $(".plus-btn").show();
        }
        else {
            $(".plus-btn").hide();
        }
    });
});
