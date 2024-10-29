using Xunit;
using CarWorkShop.Application.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using CarWorkShop.Application.ApplicationUser;
using AutoMapper;
using CarWorkShop.Application.CarWorkShop;
using FluentAssertions;

namespace CarWorkShop.Application.Mappings.Tests
{
    public class CarWorkShopMappingProfileTests
    {
        [Fact()]
        public void MappingProfile_ShouldMapCarWorkshopDtoToCarWorkshop()
        {
            // arrange

            var userContextMock = new Mock<IUserContext>();

            userContextMock
                .Setup(c => c.GetCurrentUser())
                .Returns(new CurrentUser("1", "test@test.com", new[] { "Moderator" }));

            var configuration = new MapperConfiguration( cfg => cfg.AddProfile(new CarWorkShopMappingProfile(userContextMock.Object)));

            var mapper = configuration.CreateMapper();

            var dto = new CarWorkShopDTO()
            {
                City = "City",
                PhoneNumber = "123456789",
                PostalCode = "12345",
                Street = "street"
            };

            //act

            var result = mapper.Map<Domain.Entities.CarWorkShop>(dto);


            //assert

            result.Should().NotBeNull();
            result.ContactDetails.Should().NotBeNull();
            result.ContactDetails.City.Should().Be(dto.City);
            result.ContactDetails.PhoneNumber.Should().Be(dto.PhoneNumber);
            result.ContactDetails.PostalCode.Should().Be(dto.PostalCode);
            result.ContactDetails.Street.Should().Be(dto.Street);
        }


        [Fact()]
        public void MappingProfile_ShouldMapCarWorkshopToCarWorkshopDto()
        {
            // arrange

            var userContextMock = new Mock<IUserContext>();

            userContextMock
                .Setup(c => c.GetCurrentUser())
                .Returns(new CurrentUser("1", "test@test.com", new[] { "Moderator" }));

            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(new CarWorkShopMappingProfile(userContextMock.Object)));

            var mapper = configuration.CreateMapper();

            var carWorkshop = new Domain.Entities.CarWorkShop
            {
                Id = 1,
                CreatedById ="1",
                ContactDetails = new Domain.Entities.CarWorkShopContactDetails
                {
                    City = "City",
                    PhoneNumber = "123456789",
                    PostalCode = "12345",
                    Street = "street"
                }
            };

            //act

            var result = mapper.Map<CarWorkShopDTO>(carWorkshop);

            //assert

            result.Should().NotBeNull();
            result.IsEditable.Should().BeTrue();
            result.City.Should().Be(carWorkshop.ContactDetails.City);
            result.PostalCode.Should().Be(carWorkshop.ContactDetails.PostalCode);
            result.PhoneNumber.Should().Be(carWorkshop.ContactDetails.PhoneNumber);
            result.Street.Should().Be(carWorkshop.ContactDetails.Street);

        }
    }
}