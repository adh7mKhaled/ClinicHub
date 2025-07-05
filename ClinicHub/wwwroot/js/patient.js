tinymce.init({
    selector: '.js-tinymce',
    toolbar: 'undo redo | blocks fontfamily fontsize | bold italic underline strikethrough | link image media table mergetags | addcomment showcomments | spellcheckdialog a11ycheck typography | align lineheight | checklist numlist bullist indent outdent | emoticons charmap | removeformat',
});

$(document).ready(function () {

    $('.js-toggle-status').on('click', function () {
        var btn = $(this);

        bootbox.confirm({
            message: "Are you sure that you need to toggle this patient status?",
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
                            row.addClass('animate__animated animate__fadeOut');
                            setTimeout(function () {
                                row.remove();
                            }, 1000);

                            showSuccessMessage('Patient deleted');
                        },
                        error: function () {
                            showErrorMessage('Something went wrong!')
                        }
                    });
                }
            }
        });
    })
});