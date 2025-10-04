$(document).ready(function () {
    $('.page-link').on('click', function () {
        var btn = $(this);

        var pageNumber = btn.data('page-number');

        if (btn.parent().hasClass('active')) return;

        $('#pageNumber').val(pageNumber);

        $('#Filters').submit();
    });
});