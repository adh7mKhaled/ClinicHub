$(document).ready(function () {

    $('body').delegate('.js-confirm', 'click', function () {
        var btn = $(this);

        bootbox.confirm({
            message: 'Are you sure that you need to toggle this user status?',
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
                            var row = btn.parents('tr');
                            var status = row.find('.js-status');
                            var newStatus = status.text().trim() === 'Deleted' ? 'Available' : 'Deleted';
                            status.text(newStatus).toggleClass('bg-danger bg-success');
                            row.find('.js-updated-on').html(lastUpdatedOn);
                            row.addClass('animate__animated animate__flash');
                            setTimeout(() => {
                                row.removeClass('animate__animated animate__flash');
                            }, 2000);

                            showSuccessMessage();
                        },
                        error: function () {
                            showErrorMessage();
                        }
                    });
                }
            }
        });
    });
});