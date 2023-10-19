using AutoMapper;
using CarWorkshop.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarWorkshop.Application.CarWorkshopRating.Queries.GetCarWorkshopRatingQuery
{
    public class GetCarWorkshopRatingQueryHandler : IRequestHandler<GetCarWorkshopRatingQuery, IEnumerable<CarWorkshopRatingDto>>
    {
        private readonly ICarWorkshopRatingRepository _ratingRepository;
        private readonly ICarWorkshopRepository _carworkshopRepository;
        private IMapper _mapper;

        public GetCarWorkshopRatingQueryHandler(IMapper mapper, ICarWorkshopRatingRepository ratingRepository, ICarWorkshopRepository carWorkshopRepository)
        {
            _ratingRepository = ratingRepository;
            _carworkshopRepository = carWorkshopRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CarWorkshopRatingDto>> Handle(GetCarWorkshopRatingQuery request, CancellationToken cancellationToken)
        {
            var carWorkshop = await _carworkshopRepository.GetByEncodedName(request.EncodedName);
            var ratings = await _ratingRepository.GetCarworkshopRatings(carWorkshop);
            var dtos = _mapper.Map<IEnumerable<CarWorkshopRatingDto>>(ratings);
            
            return dtos;
        }

    }
}
