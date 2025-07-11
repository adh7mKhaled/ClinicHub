var updatedRow;
var datatable;

function showSuccessMessage(message = "Saved Successfully!") {
    toastr.success(message);
}
function showErrorMessage(message = "Something went wrong!") {
    toastr.error(message);
}
function onModalSuccess(item) {
    showSuccessMessage();
    $('#Modal').modal('hide');

    if (updatedRow !== undefined) {
        datatable.row(updatedRow).remove().draw();
        updatedRow = undefined;
    }

    var newRow = $(item);
    datatable.row.add(newRow).draw();
}

var initDatatable = function () {
    datatable = $('#js-datatable').DataTable({
        info: false,
        stateSave: true,
        responsive: true,
    });
};

$(document).ready(function () {

    initDatatable();

    // Handel bootstrap modal
    $('body').delegate('.js-render-modal', 'click', function () {
        var btn = $(this);
        var modal = $('#Modal');

        modal.find('#ModalLabel').text(btn.data('title'));

        if (btn.data('update') !== undefined) {
            updatedRow = btn.parents('tr');
        }

        $.ajax({
            url: btn.data('url'),
            success: function (form) {
                modal.find('.modal-body').html(form);
                $.validator.unobtrusive.parse(modal); // handel clientSide validations on modal
            },
            error: function () {
                showErrorMessage()
            }
        })

        modal.modal('show');
    });
});