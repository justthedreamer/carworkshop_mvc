$(document).ready(function () {

    LoadCarWorkshopServices();


    $("#createCarWorkshopService form").submit(function (event) {
        event.preventDefault();

        $.ajax({
            url: $(this).attr('action'),
            type: $(this).attr('method'),
            data: $(this).serialize(),
            success: function (data) {
                toastr["success"]("Created caworkshop service")
                LoadCarWorkshopServices()
            },
            error: function () {
                toastr["error"]("Someting went wrong")
            }
        })

    });


});