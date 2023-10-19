using CarWorkshop.Application.ApplicationUser;
using CarWorkshop.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarWorkshop.Application.CarWorkshopService.Commands
{
    public class CreateCarworkshopServiceCommandHandler : IRequestHandler<CreateCarworkshopServiceCommand>
    {
        private readonly IUserContext userContext;
        private readonly ICarWorkshopRepository carWorkshopRepository;
        private readonly ICarWorkshopServiceRepository carWorkshopServiceRepository;

        public CreateCarworkshopServiceCommandHandler(IUserContext userContext, ICarWorkshopRepository carWorkshopRepository, ICarWorkshopServiceRepository carWorkshopServiceRepository )
        {
            this.userContext = userContext;
            this.carWorkshopRepository = carWorkshopRepository;
            this.carWorkshopServiceRepository = carWorkshopServiceRepository;
        }

        public async Task<Unit> Handle(CreateCarworkshopServiceCommand request, CancellationToken cancellationToken)
        {
            var carWorkshop = await carWorkshopRepository.GetByEncodedName(encodedName: request.CarWorkshopEncodedName!);


            var user =  userContext.GetCurrentUser();
            var isEditible = user != null && (carWorkshop.CreatedById == user.Id || user.IsInRole("Moderator"));

            if (!isEditible)
            {
                return Unit.Value;
            }

            var carWorkshopService = new Domain.Entities.CarWorkshopService()
            {
                Cost = request.Cost,
                Description = request.Description,
                CarWorkshopId = carWorkshop.Id,
            };

            await carWorkshopServiceRepository.Create(carWorkshopService);

            return Unit.Value;
        }
    }
}
