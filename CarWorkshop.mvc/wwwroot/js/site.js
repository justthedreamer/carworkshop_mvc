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

