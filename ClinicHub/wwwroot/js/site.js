function showSuccessMessage(message = "Saved Successfully!") {
    toastr.success(message);
}
function showErrorMessage(message = "Something went wrong!") {
    toastr.error(message);
}

$(document).ready(function () {

    // Handel bootstrap modal
    $('.js-render-modal').on('click', function () {
        var btn = $(this);
        var modal = $('#Modal');

        modal.find('#ModalLabel').text(btn.data('title'));

        $.ajax({
            url: btn.data('url'),
            success: function (form) {
                modal.find('.modal-body').html(form);
                $.validator.unobtrusive.parse(modal); // handel clientSide validations on modal
            },
            error: function () {
                showErrorMessage('Something went wrong!')
            }
        })

        modal.modal('show');
    });
});