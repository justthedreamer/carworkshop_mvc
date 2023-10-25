$(document).ready(function () {

    var ratingInput = $("#rateInputStars + input")
    var ratingContainer = $("#rateInputStars");
    var starsArray = [];

    function setRate(clickedStar) {
        var starId = $(clickedStar).attr("id");
        let starIndex = 0;

        starsArray.map((value, index) => {
            if ($(value).attr("id") === starId) {
                render(index)
            }
        });

        function render(index) {
            for (i = 0; i <= 4; i++) {
                if (i <= index) {
                    $(starsArray[i]).css("--rating", "1")
                    ratingInput.val(index+1)
                } else {
                    $(starsArray[i]).css("--rating", "0")

                }
            }
        }
    }

    var RatingComponent = () => {
        starsArray = [];
        ratingContainer.empty();

        for (var i = 1; i <= 5; i++) {
            const star = $(`<span id="star${i}" class="starInput" style="--rating:0"></span>`);
            star.click(() => setRate(star));
            ratingContainer.append(star);
            starsArray.push(star);
        }
    }

    RatingComponent();



    /* Hadle default behavior of create rating form */
    $("#createCarWorkshopRating form").submit(function (event) {
        event.preventDefault();

        $.ajax({
            url: $(this).attr('action'),
            type: $(this).attr('method'),
            data: $(this).serialize(),
            success: function (data) {
                toastr["success"]("Thanks for your opinon")
                window.location.reload();
            },
            error: function (error) {
                console.log(error)
            }
        })

    });
})