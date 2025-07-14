var updatedRow;
var datatable;

function showSuccessMessage(message = "Saved Successfully!") {
    Swal.fire({
        icon: "success",
        title: "Success",
        text: message,
        customClass: {
            confirmButton: "btn btn-primary"
        }
    });
}
function showErrorMessage(message = "Something went wrong!") {
    Swal.fire({
        icon: "error",
        title: "Oops...",
        text: message.responseText != undefined ? message.responseText : message,
        customClass: {
            confirmButton: "btn btn-outline btn-outline-dashed btn-outline-primary btn-active-light-primary"
        }
    });
}

function onModalSuccess(item) {
    showSuccessMessage();
    $('#Modal').modal('hide');

    var tableExists = $('body').find('.excludeDatatable').length;

    if (updatedRow !== undefined & tableExists !== 0) {
        $(updatedRow).remove();
        updatedRow = undefined;
    }
    else if (updatedRow !== undefined){
        datatable.row(updatedRow).remove().draw();
        updatedRow = undefined;
    }

    var newRow = $(item);

    if (tableExists !== 0) {
        $('.excludeDatatable').append(newRow);
    }
    else {
        datatable.row.add(newRow).draw();
    }
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