﻿using Xunit;
using CarWorkshop.Application.ApplicationUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using FluentAssertions;

namespace CarWorkshop.Application.ApplicationUser.Tests
{
    public class UserContextTests
    {
        [Fact()]
        public void GetCurrentUser_WithAuthenticatedUser_ShouldReturnCurrentUser()
        {
            // arrange
            
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, "1"),
                new Claim(ClaimTypes.Email, "test@example.com"),
                new Claim(ClaimTypes.Role, "Admin"),
                new Claim(ClaimTypes.Role, "User"),
                new Claim(ClaimTypes.Name, "User"),
            };

            var user = new ClaimsPrincipal(new ClaimsIdentity(claims, "Test"));

            var httpContextAccesorMock = new Mock<IHttpContextAccessor>();

            httpContextAccesorMock.Setup(x => x.HttpContext).Returns(new DefaultHttpContext()
            {
                User = user
            });

            var userContext = new UserContext(httpContextAccesorMock.Object);

            // act

            var currentUser = userContext.GetCurrentUser();
            
            // arrange

            currentUser.Should().NotBeNull();
            currentUser!.Id.Should().Be("1");
            currentUser!.Email.Should().Be("test@example.com");
            currentUser!.Roles.Should().ContainInOrder("Admin", "User");
            currentUser!.UserName.Should().Be("User");

        }
    }
}