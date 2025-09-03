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

function applySelect2() {
    $('.js-select2').select2({
        closeOnSelect: true
    });
    $('.js-select2').select2().on('change', function () {
        var form = $(this).closest('form');
        form.validate().element(this);
    });
}

var initDatatable = function () {
    datatable = $('#js-datatable').DataTable({
        info: false,
        stateSave: true,
        responsive: true,
    });
};

function applyDatePickerOnModal() {
    $(".js-datepicker").daterangepicker({
        singleDatePicker: true,
        showDropdowns: true,
        autoUpdateInput: false,
    });

    $('.js-datepicker').on('apply.daterangepicker', function (ev, picker) {
        $(this).val(picker.startDate.format('MM/DD/YYYY'));
        $(this).valid();
    });

    $('.js-datepicker').on('cancel.daterangepicker', function () {
        $(this).val('');
        $(this).valid();
    });
}

$(document).ready(function () {

    $(".js-datepicker").daterangepicker({
        singleDatePicker: true,
        minDate: new Date(),
    })

    initDatatable();
    applySelect2();
    
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
                applySelect2();
                applyDatePickerOnModal();
            },
            error: function () {
                showErrorMessage()
            }
        })

        modal.modal('show');
    });

    // Handel ToggleStatus 
    $('body').delegate('.js-toggle-status', 'click', function () {
        var btn = $(this);

        bootbox.confirm({
            message: 'Are you sure that you need to toggle this item status ?',
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

    $('body').delegate('.js-toggle-status-patient-doctor', 'click', function () {
        var btn = $(this);

        bootbox.confirm({
            message: 'Are you sure that you need to toggle this item status ?',
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
                        success: function () {
                            var status = $('.card-header .js-status');
                            var newStatus = status.text().trim() === 'Inactive' ? 'Active' : 'Inactive';
                            status.text(newStatus).toggleClass('bg-danger bg-success');
                            status.addClass('animate__animated animate__flash');
                            setTimeout(() => {
                                status.removeClass('animate__animated animate__flash');
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

    //Handle Confirm
    $('body').delegate('.js-confirm', 'click', function () {
        var btn = $(this);

        bootbox.confirm({
            message: btn.data('message'),
            buttons: {
                confirm: {
                    label: 'Yes',
                    className: 'btn-success'
                },
                cancel: {
                    label: 'No',
                    className: 'btn-secondary'
                }
            },
            callback: function (result) {
                if (result) {
                    $.post({
                        url: btn.data('url'),
                        success: function () {
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