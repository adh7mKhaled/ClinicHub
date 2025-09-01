function appointmentSuccessMessage(message = "Saved Successfully!") {
    showSuccessMessage();

    $('#appointmentForm')[0].reset();

    $('.js-select2').val(null).trigger('change');

    var form = $("#appointmentForm");
    form.find(".text-danger").empty();
}

$(document).ready(function () {

    $('#SpecialtyId').on('change', function () {
        var specialtyId = $(this).val();
        var doctorsList = $('#DoctorId');

        doctorsList.empty();
        doctorsList.append('<option></option>');

        if (specialtyId !== '') {
            $.ajax({
                url: '/Appointments/GetDoctors?specialtyId=' + specialtyId,
                success: function (doctors) {
                    $.each(doctors, function (i, doctor) {
                        var item = $('<option></option>').attr("value", doctor.value).text(doctor.text);
                        doctorsList.append(item);
                    });
                },
                error: function () {
                    showErrorMessage();
                }
            });
        }
    });

    function loadAvailableTimes() {
        var selectedDate = $('#AppointmentDate').val();
        var doctorId = $('#DoctorId').val();
        var availableTimesList = $('#AvailableDates');

        availableTimesList.empty();
        availableTimesList.append('<option></option>');

        if (selectedDate) {
            $.ajax({
                url: '/Appointments/GetAvailableTimes',
                type: 'GET',
                data: {
                    date: selectedDate,
                    doctorId: doctorId
                },
                success: function (availableTimes) {
                    $.each(availableTimes, function (i, date) {
                        var item = $('<option></option>').attr("value", date.value).text(date.text);
                        availableTimesList.append(item);
                    });
                },
                error: function () {
                    showErrorMessage();
                }
            });
        }
    }

    $('#AppointmentDate').on('change', loadAvailableTimes);
    $('#DoctorId').on('change', loadAvailableTimes);


    var table = $('#Appointments').DataTable({
        serverSide: true,
        processing: true,
        stateSave: true,   
        language: {
            processing: '<div class= "d-flex justify-content-center text-primary align-items-center dt-spinner" ><div class="spinner-border" role="status"><span class="visually-hidden">Loading...</span></div><span class="text-muted ps-2">Loading...</span></div>'
        },
        ajax: {
            url: '/Appointments/GetAppointments',
            type: 'POST',
            data: function (d) {
                var val = $('#appointmentDate').val();
                if (val) {
                    var dates = val.split(" - ");
                    d.startDate = dates[0];
                    d.endDate = dates[1];
                } else {
                    d.startDate = null;
                    d.endDate = null;
                }
            }
        },
        columnDefs: [{
            targets: 0,
            visible: false,
            searchable: false
        }],
        columns: [
            { data: 'id', "name": "Id", "className": "d-none" },
            { data: 'patientName', "name": "PatientName" },
            { data: 'doctorName', "name": "DoctorName" },
            { data: 'appointmentDate', "name": "AppointmentDate" },
            { data: 'timeSlot', "name": "TimeSlot" }
        ]
    });

    $('.js-datepicker-appointment').daterangepicker({
        autoUpdateInput: false,
        ranges: {
            'Today': [moment(), moment()],
            'Yesterday': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
            'Last 30 Days': [moment().subtract(29, 'days'), moment()],
        }
    });

    $('.js-datepicker-appointment').on('apply.daterangepicker', function (ev, picker) {
        $(this).val(picker.startDate.format('YYYY-MM-DD') + ' - ' + picker.endDate.format('YYYY-MM-DD'));
        table.ajax.reload();
    });

    $('.js-datepicker-appointment').on('cancel.daterangepicker', function (ev, picker) {
        $(this).val('');
        table.ajax.reload();
    });

});