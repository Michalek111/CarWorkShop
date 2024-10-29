using Xunit;
using CarWorkShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;

namespace CarWorkShop.Domain.Entities.Tests
{
    public class CarWorkShopTests
    {
        [Fact()]
        public void EncodeName_shouldSetEncodedName()
        {
            //arrange
            var carWorkshop = new CarWorkShop();
            carWorkshop.Name = "Test Workshop";
            //act
            carWorkshop.EncodeName();
            //assert
            carWorkshop.EncodedName.Should().Be("test-workshop");


        }

        [Fact]
        public void EncodeName_shouldThrowException_WhenNameIsNull()
        {
            var carWorkshop = new CarWorkShop();

            Action action = () => carWorkshop.EncodeName();

            action.Invoking(a => a.Invoke()).Should().Throw<NullReferenceException>();
           
            
        }
    }
}