
$(document).on('click', "#checkbox2stage", function () {
    if ($(this).is(":checked")) {
        $("#stage1desc").text("Формат 1го этапа");
        $("#stage2div").show();
    }

    else {
        $("#stage1desc").text("Формат турнира");
        $("#stage2div").hide();
    }

});

$(document).on('click', "#checkboxseasonal", function () {
    if ($(this).is(":checked"))
        $("#seasondiv").show();

    else
        $("#seasondiv").hide();

});

$(document).on('click', "#checkboxofficial", function () {

    if ($(this).is(":checked"))
        $("#yeardiv").show();
    else
        $("#yeardiv").hide();

});

$(document).on('click', ".plus-btn", function () {

    var hostId = $("#hosts-dropdown").val();

    if (!$('#' + hostId).length) {

        var hostName = $("#hosts-dropdown option:selected").text();
        var hostWrapper = $(document.createElement('div')).attr('id', hostId);
        var hiddenInput = $(document.createElement('input')).attr({ 'type': 'hidden', 'name': 'selectedHost', 'value': hostId });
        var nameLabel = $(document.createElement('label')).attr('class', 'host-name').append(hostName);
        var minusBtn = $(document.createElement('a')).attr('class', 'btn jog-btn minus-btn');

        $(hiddenInput).appendTo(nameLabel);
        $(nameLabel).appendTo(hostWrapper);
        $(minusBtn).prependTo(hostWrapper);

        $(hostWrapper).appendTo("#hosts-container");

    }

});

$(document).on('click', ".minus-btn", function () {

    var id = $(this).parent().attr('id');
    $('#' + id).remove();

});


$(document).on('change', "#hosts-dropdown", function () {


    if ($(this).val() != "") {
        $(".plus-btn").show();
    }
    else {
        $(".plus-btn").hide();
    }
});