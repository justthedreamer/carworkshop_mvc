using CarWorkshop.Application.ApplicationUser;
using CarWorkshop.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarWorkshop.Application.CarWorkshopRating.Commands
{
    internal class CreateCarWorkshopRatingHandler : IRequestHandler<CreateCarWorkshopRatingCommand>
    {


        private readonly IUserContext userContext;
        private readonly ICarWorkshopRepository carWorkshopRepository;
        private readonly ICarWorkshopRatingRepository carWorkshopRatingRepository;

        public CreateCarWorkshopRatingHandler(IUserContext userContext, ICarWorkshopRepository carWorkshopRepository, ICarWorkshopRatingRepository carWorkshopRatingRepository)
        {
            this.userContext = userContext;
            this.carWorkshopRepository = carWorkshopRepository;
            this.carWorkshopRatingRepository = carWorkshopRatingRepository;
        }

        public async Task<Unit> Handle(CreateCarWorkshopRatingCommand request, CancellationToken cancellationToken)
        {
            var carworkshop = await carWorkshopRepository.GetByEncodedName(request.CarWorkshopEncodedName);

            var currentUser = userContext.GetCurrentUser();

            var newRate = new Domain.Entities.CarWorkshopRating()
            {
                Rate = request.Rate,
                Description = request.Description,
                CreatedById = currentUser!.Id,
                CarWorkshopId = carworkshop.Id,
                CreatedAt = DateTime.UtcNow,
            };
            
            await carWorkshopRatingRepository.CreateNewRating(newRate);
            await carWorkshopRatingRepository.Commit();

            return Unit.Value;
        }
    }
}
