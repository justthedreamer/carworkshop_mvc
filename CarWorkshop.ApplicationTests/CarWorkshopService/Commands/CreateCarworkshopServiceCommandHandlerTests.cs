using Xunit;
using CarWorkshop.Application.CarWorkshopService.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using CarWorkshop.Application.ApplicationUser;
using CarWorkshop.Domain.Interfaces;
using FluentAssertions;
using MediatR;

namespace CarWorkshop.Application.CarWorkshopService.Commands.Tests
{
    public class CreateCarworkshopServiceCommandHandlerTests
    {
        [Fact()]
        public async Task Handle_CreatesCarWOrkshopService_WhenUserIsAuthorized()
        {
            // arrange



            var carWorkshop = new Domain.Entities.CarWorkshop()
            {
                Id = 1,
                CreatedById = "1"
            };

            var command = new CreateCarworkshopServiceCommand()
            {
                Cost = "100 PLN",
                Description = "Service description",
                CarWorkshopEncodedName = "workshop1"
            };

            var userContextMock = new Mock<IUserContext>();
            userContextMock.Setup(c => c.GetCurrentUser())
                .Returns(new CurrentUser("1", "test@test.com", new List<string> { "User" }));

            var carWorkshopRepositoryMock = new Mock<ICarWorkshopRepository>();
            carWorkshopRepositoryMock.Setup(c => c.GetByEncodedName(command.CarWorkshopEncodedName)).ReturnsAsync(carWorkshop);

            var carWorkshopServiceRepositoryMock = new Mock<ICarWorkshopServiceRepository>();

            var handler = new CreateCarworkshopServiceCommandHandler(userContextMock.Object,carWorkshopRepositoryMock.Object,carWorkshopServiceRepositoryMock.Object);

            var result = await handler.Handle(command, CancellationToken.None);

            // assert

            result.Should().Be(Unit.Value);
            carWorkshopServiceRepositoryMock.Verify(c => c.Create(It.IsAny<Domain.Entities.CarWorkshopService>()), Times.Once);
        }

        [Fact()]
        public async Task Handle_CreatesCarWOrkshopService_WhenUserIsModerator()
        {
            // arrange



            var carWorkshop = new Domain.Entities.CarWorkshop()
            {
                Id = 1,
                CreatedById = "1"
            };

            var command = new CreateCarworkshopServiceCommand()
            {
                Cost = "100 PLN",
                Description = "Service description",
                CarWorkshopEncodedName = "workshop1"
            };

            var userContextMock = new Mock<IUserContext>();
            userContextMock.Setup(c => c.GetCurrentUser())
                .Returns(new CurrentUser("2", "test@test.com", new List<string> { "Moderator" }));

            var carWorkshopRepositoryMock = new Mock<ICarWorkshopRepository>();
            carWorkshopRepositoryMock.Setup(c => c.GetByEncodedName(command.CarWorkshopEncodedName)).ReturnsAsync(carWorkshop);

            var carWorkshopServiceRepositoryMock = new Mock<ICarWorkshopServiceRepository>();

            var handler = new CreateCarworkshopServiceCommandHandler(userContextMock.Object, carWorkshopRepositoryMock.Object, carWorkshopServiceRepositoryMock.Object);

            var result = await handler.Handle(command, CancellationToken.None);

            // assert

            result.Should().Be(Unit.Value);
            carWorkshopServiceRepositoryMock.Verify(c => c.Create(It.IsAny<Domain.Entities.CarWorkshopService>()), Times.Once);
        }

        [Fact()]
        public async Task Handle_CreatesCarWOrkshopService_WhenUserIsNotAuthorized()
        {
            // arrange

            var carWorkshop = new Domain.Entities.CarWorkshop()
            {
                Id = 1,
                CreatedById = "1"
            };

            var command = new CreateCarworkshopServiceCommand()
            {
                Cost = "100 PLN",
                Description = "Service description",
                CarWorkshopEncodedName = "workshop1"
            };

            var userContextMock = new Mock<IUserContext>();
            userContextMock.Setup(c => c.GetCurrentUser())
                .Returns(new CurrentUser("2", "test@test.com", new List<string> { "User" }));

            var carWorkshopRepositoryMock = new Mock<ICarWorkshopRepository>();
            carWorkshopRepositoryMock.Setup(c => c.GetByEncodedName(command.CarWorkshopEncodedName)).ReturnsAsync(carWorkshop);

            var carWorkshopServiceRepositoryMock = new Mock<ICarWorkshopServiceRepository>();

            var handler = new CreateCarworkshopServiceCommandHandler(userContextMock.Object, carWorkshopRepositoryMock.Object, carWorkshopServiceRepositoryMock.Object);

            var result = await handler.Handle(command, CancellationToken.None);

            // assert

            result.Should().Be(Unit.Value);
            carWorkshopServiceRepositoryMock.Verify(c => c.Create(It.IsAny<Domain.Entities.CarWorkshopService>()), Times.Never);
        }

        [Fact()]
        public async Task Handle_CreatesCarWOrkshopService_WhenUserIsNotAuthenticated()
        {
            // arrange

            var carWorkshop = new Domain.Entities.CarWorkshop()
            {
                Id = 1,
                CreatedById = "1"
            };

            var command = new CreateCarworkshopServiceCommand()
            {
                Cost = "100 PLN",
                Description = "Service description",
                CarWorkshopEncodedName = "workshop1"
            };

            var userContextMock = new Mock<IUserContext>();
            userContextMock.Setup(c => c.GetCurrentUser())
                .Returns((CurrentUser?)null);

            var carWorkshopRepositoryMock = new Mock<ICarWorkshopRepository>();
            carWorkshopRepositoryMock.Setup(c => c.GetByEncodedName(command.CarWorkshopEncodedName)).ReturnsAsync(carWorkshop);

            var carWorkshopServiceRepositoryMock = new Mock<ICarWorkshopServiceRepository>();

            var handler = new CreateCarworkshopServiceCommandHandler(userContextMock.Object, carWorkshopRepositoryMock.Object, carWorkshopServiceRepositoryMock.Object);

            var result = await handler.Handle(command, CancellationToken.None);

            // assert

            result.Should().Be(Unit.Value);
            carWorkshopServiceRepositoryMock.Verify(c => c.Create(It.IsAny<Domain.Entities.CarWorkshopService>()), Times.Never);
        }
    }
}