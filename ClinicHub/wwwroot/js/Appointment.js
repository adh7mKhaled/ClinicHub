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

    //$('#AppointmentDate').on('change', function () {
    //    var selectedDate = $(this).val();
    //    var doctorId = $('#DoctorId').val();
    //    var availableTimesList = $('#AvailableDates');

    //    availableTimesList.empty();
    //    availableTimesList.append('<option></option>');

    //    if (selectedDate) {
    //        $.ajax({
    //            url: '/Appointments/GetAvailableTimes',
    //            type: 'GET',
    //            data: {
    //                date: selectedDate,
    //                doctorId: doctorId
    //            },
    //            success: function (availableTimes) {
    //                $.each(availableTimes, function (i, date) {
    //                    var item = $('<option></option>').attr("value", date.value).text(date.text);
    //                    availableTimesList.append(item);
    //                });
    //            },
    //            error: function () {
    //                showErrorMessage();
    //            }
    //        })
    //    }
    //});

});