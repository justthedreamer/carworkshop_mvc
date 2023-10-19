using Xunit;
using CarWorkshop.Application.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using CarWorkshop.Application.ApplicationUser;
using AutoMapper;
using CarWorkshop.Application.CarWorkshop;
using FluentAssertions;
using CarWorkshop.Domain.Entities;

namespace CarWorkshop.Application.Mappings.Tests
{
    public class CarWorkshopMappingProfileTests
    {
        [Fact()]
        public void MappingProfile_ShouldMapCarWorkshopDtoToCarWorkshop()
        {
            //arrage
            var userContextMock = new Mock<IUserContext>();
            userContextMock.Setup(c => c.GetCurrentUser())
                .Returns(new CurrentUser("1", "test@example.com", new[] { "Moderator" }, "Maciek"));

            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(
                new CarWorkshopMappingProfile(userContextMock.Object)));

            var mapper = configuration.CreateMapper();

            var dto = new CarWorkshopDto
            {
                City = "City",
                PhoneNumber = "123456789",
                PostalCode = "1234",
                Street = "Street",
            };

            // act

            var result = mapper.Map<Domain.Entities.CarWorkshop>(dto);

            // assert

            result.Should().NotBeNull();
            result.ContactDetails.Should().NotBeNull();
            result.ContactDetails.City.Should().Be(dto.City);
            result.ContactDetails.PhoneNumber.Should().Be(dto.PhoneNumber);
            result.ContactDetails.PostalCode.Should().Be(dto.PostalCode);
            result.ContactDetails.Street.Should().Be(dto.Street);
        }
        [Fact()]
        public void MappingProfile_ShouldMapaCarWorkshopToMapCarWorkshopDto()
        {
            //arrage
            var userContextMock = new Mock<IUserContext>();
            userContextMock.Setup(c => c.GetCurrentUser())
                .Returns(new CurrentUser("1", "test@example.com", new[] { "Moderator" },"Maciek"));

            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(
                new CarWorkshopMappingProfile(userContextMock.Object)));

            var mapper = configuration.CreateMapper();

            var carWorkshop = new Domain.Entities.CarWorkshop
            {
                Id = 1,
                CreatedById = "1",
                ContactDetails = new CarWoskshopContactDetails
                {
                    City = "City",
                    PhoneNumber = "123456789",
                    PostalCode = "12345",
                    Street = "Street",
                }
            };

            // act

            var result = mapper.Map<CarWorkshopDto>(carWorkshop);

            result.Should().NotBeNull();

            result.IsEditable.Should().BeTrue();
            result.Street.Should().Be(carWorkshop.ContactDetails.Street);
            result.City.Should().Be(carWorkshop.ContactDetails.City);
            result.PhoneNumber.Should().Be(carWorkshop.ContactDetails.PhoneNumber);
            result.PostalCode.Should().Be(carWorkshop.ContactDetails.PostalCode);

        }
    }
}