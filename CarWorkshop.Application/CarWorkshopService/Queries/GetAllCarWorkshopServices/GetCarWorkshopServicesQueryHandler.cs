using AutoMapper;
using CarWorkshop.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarWorkshop.Application.CarWorkshopService.Queries.GetAllCarWorkshopServices
{
    public class GetCarWorkshopServicesQueryHandler : IRequestHandler<GetCarWorkshopServicesQuery, IEnumerable<CarWorkshopServiceDto>>
    {
        private readonly ICarWorkshopServiceRepository carWorkshopServiceRepository;
        private readonly IMapper mapper;

        public GetCarWorkshopServicesQueryHandler(ICarWorkshopServiceRepository carWorkshopServiceRepository, IMapper mapper)
        {
            this.carWorkshopServiceRepository = carWorkshopServiceRepository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<CarWorkshopServiceDto>> Handle(GetCarWorkshopServicesQuery request, CancellationToken cancellationToken)
        {
            var result = await carWorkshopServiceRepository.GetAllByEncodedName(request.EncodedName);

            var dtos = mapper.Map<IEnumerable<CarWorkshopServiceDto>>(result);

            return dtos;
        }

    }
}
