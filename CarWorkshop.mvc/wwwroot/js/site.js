/* Car workshop services*/

const RenderCarWorkshopServices = (services, container) => {
        container.empty();

        for (const service of services) {
            container.append(
                `<div class = "card border-secoundary" style="max-width: 18rem">
                    <div class="card-header">${service.cost}</div>
                    <div class="card-body">
                        <h5 class="card-title">${service.description}</>
                    </div>
                </div>`
            );
    }
}

const LoadCarWorkshopServices = () => {
        const container = $("#services")
        const carWorkshopEncodedName = container.data("encodedName")

        $.ajax({
            url: `/CarWorkshop/${carWorkshopEncodedName}/CarWorkshopService`,
            type: 'get',
            success: function (data) {
                if (!data.length) {
                    container.html("There are no services for this car workshop")
                } else {
                    RenderCarWorkshopServices(data, container)
                }
            },
            error: function () {
                toastr["error"]("Something goes wrong")
            }
        })
}

/* Rating Stars */

const RenderCarWorkshopRatingStars = (ratings, container) => {

    /* Calculate everage */
    var encodedName = $(container).data("encodedName");
    let totalRate = 0;

    for (const rate of ratings) {
        totalRate += rate.rate;
    }
    const average = ratings.length === 0 ? 0 : totalRate / ratings.length;

    /* Rounded average */
    const rateAverage = average === 0 ? 0 : average.toFixed(1);

    /* Append component */
    $(container).append(`
    <div class="Stars" style="--rating: ${rateAverage }; --star-size: 22px" data-encoded-name="${encodedName}"></div>
    <span class="ratingCount text-secondary">${rateAverage} (${ratings.length})</span>`)
}

const LoadCarWorkshopRatingStars = () => {
    const containers = $(".rating")

    for (const container of containers) {
        const carWorkshopEncodedName = $(container).data('encodedName');


        $.ajax(
            {
                url: `/CarWorkshop/${carWorkshopEncodedName}/CarWorkshopRating`,
                type: 'get',
                success: function (data) {
                    RenderCarWorkshopRatingStars(data, container)
                },
                error: function () {
                    toastr["error"]("Something goes wrong with rating loader")
                }
            }
        )
    }
}


/* Ratings */
const RenderCarWorkshopRatings = (ratings, container) => {
    container.empty();

    for (const rating of ratings) {
        container.append(
            `<div class="ratingItem rounded shadow border-1 border-dark p-3 mt-3">
                <div class="w-100" style="max-width: 18rem">
                    <div class="ratingHeader">
                        <h5>${rating.createdByName}</h5>
                        <div class="Stars" style="--rating: ${rating.rate}; --star-size: 22px"></div>
                    </div>
                    <div class="ratingContent mt-3 ps-1">
                        <p>${rating.description}</p>
                    </div>
                </div>
            </div>`
        );
    }
}

const LoadCarWorkshopRatings = () => {
    console.log('dddd')
    const container = $("#ratingsSection")
    const carWorkshopEncodedName = container.data("encodedName")

    $.ajax({
        url: `/CarWorkshop/${carWorkshopEncodedName}/CarWorkshopRating`,
        type: 'get',
        success: function (data) {
            if (!data.length) {
                container.html("There are no rating for this car workshop")
            } else {
                RenderCarWorkshopRatings(data, container)
            }
        },
        error: function () {
            toastr["error"]("Something goes wrong")
        }
    })
}

