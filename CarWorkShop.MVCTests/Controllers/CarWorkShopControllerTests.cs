using Xunit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using CarWorkShop.Application.CarWorkShop;
using Moq;
using MediatR;
using CarWorkShop.Application.CarWorkShop.Commands.CreateCarWorkshop.Queries.GetAllWorkshops;
using Microsoft.AspNetCore.TestHost;
using FluentAssertions;
using System.Net;

namespace Tests
{
    public class CarWorkShopControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public CarWorkShopControllerTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }


        [Fact()]
        public async Task Index_ReturnsViewWithExpectedData_ForExisitngWorkshops()
        {
            //arrange

            var carWorkshops = new List<CarWorkShopDTO>()
            {
                new CarWorkShopDTO
                {
                    Name =  "1",
                },
                new CarWorkShopDTO
                {
                    Name =  "2",
                },
                new CarWorkShopDTO
                {
                    Name =  "3",
                },
            };

            var mediatorMock = new Mock<IMediator>();
            mediatorMock.Setup(m => m.Send(It.IsAny<GetAllCarWorkshopsQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(carWorkshops);

            var client = _factory
                .WithWebHostBuilder(builder => 
                    builder.ConfigureTestServices(services => services.AddScoped(_ => mediatorMock.Object) ))
                .CreateClient();

            //act

            var response = await client.GetAsync("/CarWorkshop/Index");

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var contentStream = await response.Content.ReadAsStreamAsync();
            string content;
            using (var reader = new StreamReader(contentStream))
            {
                content = await reader.ReadToEndAsync();
            }


            content.Should().Contain("<h1>Car WorkShops</h1>")
                .And.Contain("1")
                .And.Contain("2")
                .And.Contain("3");

        }

        [Fact()]
        public async Task Index_ReturnsEmptyView_WhenNoCarshopsExist()
        {
            //arrange

            var carWorkshops = new List<CarWorkShopDTO>();
         

            var mediatorMock = new Mock<IMediator>();
            mediatorMock.Setup(m => m.Send(It.IsAny<GetAllCarWorkshopsQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(carWorkshops);

            var client = _factory
                .WithWebHostBuilder(builder =>
                    builder.ConfigureTestServices(services => services.AddScoped(_ => mediatorMock.Object)))
                .CreateClient();

            //act

            var response = await client.GetAsync("/CarWorkshop/Index");

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var contentStream = await response.Content.ReadAsStreamAsync();
            string content;
            using (var reader = new StreamReader(contentStream))
            {
                content = await reader.ReadToEndAsync();
            }


            content.Should().NotContain("div class=\"card m-3\"");

        }

    }
}