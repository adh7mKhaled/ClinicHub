$(document).ready(function () {

    $('.js-confirm').on('click', function () {
        var btn = $(this);

        bootbox.confirm({
            message: btn.data('message'),
            buttons: {
                confirm: {
                    label: 'Yes',
                    className: 'btn-danger'
                },
                cancel: {
                    label: 'No',
                    className: 'btn-secondary'
                }
            },
            callback: function (result) {
                if (result) {
                    $.ajax({
                        url: btn.data('url'),
                        success: function (lastUpdatedOn) {
                            showSuccessMessage();
                        },
                        error: function () {
                            showErrorMessage("User is not locked out.");
                        }
                    });
                }
            }
        });
    });
});