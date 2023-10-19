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



const LoadCarWorkshopRatings = () => {
    const container = $("#ratings")
    const carWorkshopEncodedName = container.data("encodedName")

    $.ajax(
        {
            url: `/CarWorkshop/${carWorkshopEncodedName}/CarWorkshopRating`,
            type: 'get',
            success: function (data) {
                if (!data.length) {
                    container.html("There are no rating for this car workshop")
                } else {
                    RednderCarWorkshopRating(data, container)
                }
            },
            error: function () {
                toastr["error"]("Something goes wrong with rating loader")
            }
        })
}

const RenderCarWorkshopRatingStars = (ratings, container) => {

    var encodedName = $(container).data("encodedName");
        let totalRate = 0;

        for (const rate of ratings) {
            totalRate += rate.rate;
        }

        const averageRate = totalRate / ratings.length;

    $(container).append(`<div onclick="RenderRatingsInRightSidebar(event)" class="Stars" style="--rating: ${averageRate}; --star-size: 22px" data-encoded-name="${encodedName}"></div><span class="ratingCount text-secondary">(${ratings.length})</span>`)

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


const AppendRatingsToSidebar = (container, ratings) => {

    $(container).empty();
    ratings.forEach((rate) => {
        container.append(`
           <div class="w-100 mt-5" style="width: 18rem">
            <div class="rate">
            <div class="user-photo"></div>
            <div class="user-name">${rate.createdByName}</div>
            <div class="rating-description">
                <label>
                    <div class="Stars" style="--rating: ${rate.rate}; --star-size: 22px"></div>

                </label>
                <p>${rate.description}</p>
                <span class="rating-date text-secondary">${rate.createdAt}</span>
            </div>
            </div>
        </div>
        `)
    })
}

const RednderCarWorkshopRating = (ratings, container) => {
    container.empty();

    for (const rating of ratings) {
        container.append(
            `  <div class="w-100 mt-5" style="width: 18rem">
            <div class="rate">
            <div class="user-photo"></div>
            <div class="user-name">${rate.createdByName}</div>
            <div class="rating-description">
                <label>
                    <div class="Stars" style="--rating: ${rate.rate}; --star-size: 22px"></div>

                </label>
                <p>${rate.description}</p>
                <span class="rating-date text-secondary">${rate.createdAt}</span>
            </div>
            </div>
        </div>
        `
        );
    }
}


const GetCarWorkshopRatings = (encodedName) => {
    return new Promise((resolve, reject) => {
        $.ajax({
            url: `/CarWorkshop/${encodedName}/CarWorkshopRating`,
            type: 'get',
            success: function (data) {
                resolve(data);
            },
            error: function () {
                reject('Something went wrong'); 
            }
        });
    });
};

/* right sitebar */
const sidebarRight = $("#sidebar-right")
const sidebarRightContent = $('#sidebar-right-content')
const sidebarRightCloseButton = $('#sidebar-right-close-button')
const siberRightFormContainer = $('#sidebar-right-form-container')
$(sidebarRightCloseButton).on('click', () => ToggleSidebar(sidebarRight))


class RatingsSidebarRightStrategy {
    constructor() {
        this.container = sidebarRightContent;
    }
    renderContent(ratings) {
        this.container.empty();

        for (const rating of ratings) {
            this.container.append(
                `
                <div class="w-100 mt-5" style="width: 18rem">
                    <div class="rate">
                    <div class="user-photo"></div>
                    <div class="user-name">${rating.createdByName}</div>
                    <div class="rating-description">
                        <label>
                            <div class="Stars" style="--rating: ${rating.rate} ; --star-size: 22px;"></div>
                        </label>

                        <p>${rating.description}</p>

                        <span class="rating-date text-secondary">
                            ${rating.createdAt}
                        </span>
                    </div>
                    </div>
                </div>
                 `
            );
        }
    }
}
class RatingFormSidebarRightStrategy {
    constructor() {
        this.container = siberRightFormContainer
    }

    renderContent(encodedName) {
        this.container.empty();
        this.container.append(
            `
                <div>
                    <h6>Rating</h6>
                    ${this.ratingComponent()}
                </div>
                <div>
                    <h6>Description</h6>
                </div>
            `
        )
    }

}
class SidebarRender {
    constructor() {
        this.strategy = null;
    }

    setStrategy(strategy) {
        this.strategy = strategy;
    }

    render(data) {
        if (this.strategy) {
            this.strategy.renderContent(data)
        }
    }
}

const RenderRatingsInRightSidebar = async (event) => {

    var object = $(event.target)
    $(object).on('click', ToggleSidebar(sidebarRight))

    try {
        var encodedName = $(event.target).data('encodedName')
        var ratings = await GetCarWorkshopRatings(encodedName);

        var sidebarRender = new SidebarRender();
        sidebarRender.setStrategy(new RatingsSidebarRightStrategy())
        sidebarRender.render(ratings)

    } catch (error) {
        console.log(error)
    }
}

const ToggleSidebar = (object) => {
    var sidebar = object;
    sidebar.toggleClass('hidden');
}

const RenderRatingForm = () => {
    var starsContainer = $("#starsInput")
    var stars = []

    for (let i = 1; i <= 5; i++) {
        starsContainer.append(`<span id="${i}" class="starInput" style="--rating: 0;" onClick="CheckedRating(event)"> </span>`)
        var starObject = $(`#${i}`)
        stars.push(starObject)
    }

    CheckedRating = (event) => {
        var starId = $(event.target).attr("id")
        var ratingInput = $("#ratingInput")
        $(ratingInput).val(starId)


        for (let i = 0; i < 5; i++) {

            if (i < starId) {
                stars[i].css("--rating", "1")
            } else {
                stars[i].css("--rating", "0")
            }
        }
    }
}

RenderRatingForm();