using CarWorkShop.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarWorkShop.Infrastructure.Seeders
{
    public class CarWorkShopSeeder
    {
        private readonly CarWorkShopDbContext _dbContext;

        public CarWorkShopSeeder(CarWorkShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Seed()
        {
            if(await _dbContext.Database.CanConnectAsync())
            {
                if(!_dbContext.CarWorkShops.Any()) 
                {
                    var mazdaASO = new Domain.Entities.CarWorkShop()
                    {
                        Name = "Mazda ASO",
                        Description = "Autoryzowany serwis Mazda",
                        ContactDetails = new()
                        {
                            City = "Krakow",
                            Street = "Szewska 2",
                            PostalCode = "30-001",
                            PhoneNumber = "500500500"
                           
                        }
                    };
                    mazdaASO.EncodeName();
                    _dbContext.CarWorkShops.Add(mazdaASO);
                    await _dbContext.SaveChangesAsync();
                }
            }
        }
    }
}
