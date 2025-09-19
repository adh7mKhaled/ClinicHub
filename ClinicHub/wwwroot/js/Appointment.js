var chart;

function appointmentSuccessMessage(message = "Saved Successfully!") {
    showSuccessMessage();

    $('#appointmentForm')[0].reset();

    $('.js-select2').val(null).trigger('change');

    var form = $("#appointmentForm");
    form.find(".text-danger").empty();
}

function loadDateRange() {
    var start = moment().subtract(29, 'days');
    var end = moment();

    function cb(start, end) {
        $('#AppointmentReportRange span').html(start.format('MMMM D, YYYY') + ' - ' + end.format('MMMM D, YYYY'));
    }

    $('#AppointmentReportRange').daterangepicker({
        startDate: start,
        endDate: end,
        ranges: {
            'Today': [moment(), moment()],
            'Yesterday': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
            'Last 7 Days': [moment().subtract(6, 'days'), moment()],
            'Last 30 Days': [moment().subtract(29, 'days'), moment()],
            'This Month': [moment().startOf('month'), moment().endOf('month')],
            'Last Month': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')]
        }
    }, cb);

    cb(start, end);
}

drawAppointmentsChart();

function drawAppointmentsChart(startDate = null, endDate = null) {

    var element = document.getElementById('AppointmentsPerDay');

    if (!element)
        return;

    var height = parseInt(window.getComputedStyle(element).height);

    $.get({
        url: `/Dashboard/GetAppointmentsPerDay?startDate=${startDate}&endDate=${endDate}`,
        success: function (data) {
            console.log(data);
            var options = {
                chart: {
                    type: 'line',
                    height: height,
                    toolbar: {
                        show: false
                    }
                },
                stroke: {
                    curve: 'smooth'
                },
                series: [{
                    name: 'appointments',
                    data: data.map(x => x.value)
                }],
                xaxis: {
                    categories: data.map(x => x.label)
                },
                yaxis: {
                    min: 0,
                    tickAmount: Math.max(... data.map(x => x.value))
                }
            };

            chart = new ApexCharts(element, options);
            chart.render();
        }
    })
}

$(document).ready(function () {

    $('#AppointmentReportRange').on('apply.daterangepicker', function (ev, picker) {
        var startDate = picker.startDate.format('YYYY-MM-DD');
        var endDate = picker.endDate.format('YYYY-MM-DD');

        chart.destroy();
        drawAppointmentsChart(startDate, endDate);
    });

    loadDateRange();

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
            { data: 'timeSlot', "name": "TimeSlot" },
            {
                data: 'status',
                name: "Status",
                render: function (data, type, row) {
                    let text = "";
                    let badgeClass = "";

                    switch (data) {
                        case 0:
                            text = "Scheduled";
                            badgeClass = "bg-primary";
                            break;
                        case 1:
                            text = "Completed";
                            badgeClass = "bg-success";
                            break;
                        case 2:
                            text = "Cancelled";
                            badgeClass = "bg-warning";
                            break;
                    }

                    return `<span class="badge ${badgeClass}">${text}</span>`;
                }
            }

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