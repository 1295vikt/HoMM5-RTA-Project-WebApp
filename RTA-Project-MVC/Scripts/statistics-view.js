$(document).on('click', '[name="GetForSingleFaction"]', function () {

    if ($("#GetForSingleFaction").is(":checked"))
        $('#FactionId').prop('disabled', false);
    else
        $('#FactionId').prop('disabled', true);

});


$(document).on('click', '#filter-checkbox', function () {

    if ($(this).is(":checked"))
        $('input[type=date]').prop('disabled', false);
    else
        $('input[type=date]').prop('disabled', true);

});


function successfulEntry() {
    $('#stats-table').load('Message/SuccessMessage');
}
