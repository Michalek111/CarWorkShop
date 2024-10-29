using CarWorkShop.Domain.Entities;
using CarWorkShop.Domain.Inferfaces;
using CarWorkShop.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarWorkShop.Infrastructure.Repositories
{
    public class CarWorkshopServiceRepository : ICarWorkshopServiceRepository
    {
        private readonly CarWorkShopDbContext _dbContext;

        public CarWorkshopServiceRepository(CarWorkShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Create(CarWorkshopService carWorkshopService)
        {
            _dbContext.Services.Add(carWorkshopService);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<CarWorkshopService>> GetAllByEncodedName(string encodedName)
        => await _dbContext.Services
            .Where( s => s.CarWorkShop.EncodedName == encodedName)
            .ToListAsync();
    }
}
