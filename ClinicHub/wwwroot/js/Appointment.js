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
            { data: 'id', "name": "Id", "className": "d-none"},
            { data: 'patientName', "name": "PatientName", orderable: false },
            { data: 'doctorName', "name": "DoctorName", orderable: false },
            { data: 'appointmentDate', "name": "AppointmentDate", orderable: false },
            { data: 'timeSlot', "name": "TimeSlot", orderable: false },
            {
                data: 'status',
                name: "Status",
                orderable: false,
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
                            badgeClass = "bg-warning text-dark";
                            break;
                    }

                    return `<span class="badge ${badgeClass} js-badge-status">${text}</span>`;
                }
            },
            {
                data: 'null',
                orderable: false,
                searchable: false,
                "className": "text-end",
                render: function (data, type, row) {
                    return `<div class="btn-group">
			                    <button type="button" class="btn btn-secondary dropdown-toggle btn-sm ps-7" data-bs-toggle="dropdown" aria-expanded="false">
				                    Change status
			                    </button>
			                    <ul class="dropdown-menu">
				                    <li>
					                    <a href="javascript:;" class="dropdown-item js-appointment-change-status"
                                           data-message="Mark this appointment as completed?"
                                           data-status="1"
                                           data-url="/Appointments/ChangeStatus?appointmentId=${row.id}&status=1">
						                    Complete
					                    </a>
				                    </li>
				                    <li>
					                    <a href="javascript:;" class="dropdown-item js-appointment-change-status"
                                           data-message="Are you sure you want to cancel this appointment?"
                                           data-status="2"
                                           data-url="/Appointments/ChangeStatus?appointmentId=${row.id}&status=2">
						                    Cancel
					                    </a>
				                    </li>
			                    </ul>
		                    </div>`;
                }
            }
        ]
    });

    $('body').delegate('.js-appointment-change-status', 'click', function () {
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
                    $.post({
                        url: btn.data('url'),
                        success: function () {
                            let row = btn.closest('tr');
                            let status = row.find('.js-badge-status');

                            const newStatus = btn.data('status');

                            let text = '';
                            let badgeClass = '';

                            switch (parseInt(newStatus)) {
                                case 0: text = 'Scheduled'; badgeClass = 'bg-primary'; break;
                                case 1: text = 'Completed'; badgeClass = 'bg-success'; break;
                                case 2: text = 'Cancelled'; badgeClass = 'bg-warning text-dark'; break;
                            }

                            status.text(text)
                                .removeClass('bg-success bg-warning bg-primary text-dark')
                                .addClass(badgeClass);

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