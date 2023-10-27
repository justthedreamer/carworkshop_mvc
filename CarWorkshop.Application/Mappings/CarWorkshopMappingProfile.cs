using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CarWorkshop.Application.ApplicationUser;
using CarWorkshop.Application.CarWorkshop;
using CarWorkshop.Application.CarWorkshop.Commands.EditCarWorkshop;
using CarWorkshop.Application.CarWorkshopRating;
using CarWorkshop.Application.CarWorkshopService;
using CarWorkshop.Domain.Entities;
using CarWorkshop.Domain.Interfaces;
using Microsoft.AspNetCore.Routing.Constraints;

namespace CarWorkshop.Application.Mappings
{
    public class CarWorkshopMappingProfile : Profile
    {
        private IUserContext userContext;


        public CarWorkshopMappingProfile(IUserContext userContext) {

            var user = userContext.GetCurrentUser();

            CreateMap<CarWorkshopDto, Domain.Entities.CarWorkshop>()
                .ForMember(e => e.ContactDetails, opt => opt.MapFrom(src => new CarWoskshopContactDetails()
                {
                    City = src.City,
                    PhoneNumber = src.PhoneNumber,
                    PostalCode = src.PostalCode,
                    Street = src.Street,
                }));

            CreateMap<Domain.Entities.CarWorkshop, CarWorkshopDto>()
                .ForMember(dto => dto.IsEditable, opt => opt.MapFrom(src => user != null && (src.CreatedById == user.Id || user.IsInRole("Moderator"))))
                .ForMember(dto => dto.Street, opt => opt.MapFrom((src => src.ContactDetails.Street)))
                .ForMember(dto => dto.City, opt => opt.MapFrom((src => src.ContactDetails.City)))
                .ForMember(dto => dto.PostalCode, opt => opt.MapFrom((src => src.ContactDetails.PostalCode)))
                .ForMember(dto => dto.PhoneNumber, opt => opt.MapFrom((src => src.ContactDetails.PhoneNumber)));

            CreateMap<CarWorkshopDto, EditCarWorkshopCommand>();

            CreateMap<CarWorkshopServiceDto, Domain.Entities.CarWorkshopService>().ReverseMap();

            CreateMap<Domain.Entities.CarWorkshopRating, CarWorkshopRatingDto>()
                .ForMember(dto => dto.IsEditiable, opt => opt.MapFrom(src => user != null && (src.CreatedById == user.Id || user.IsInRole("Moderator"))))
                .ForMember(dto => dto.CreatedByName, opt => opt.MapFrom(src => src.CreatedBy!.UserName));
        }
    }
}
