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
    internal class CarWorkShopRepository : ICarWorkShopRepository
    {
        private readonly CarWorkShopDbContext _dbContext;

        public CarWorkShopRepository(CarWorkShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task Create(Domain.Entities.CarWorkShop carWorkShop)
        {
            _dbContext.Add(carWorkShop);
            await _dbContext.SaveChangesAsync();
        }

        public Task<Domain.Entities.CarWorkShop?> GetByName(string name) 
            => _dbContext.CarWorkShops.FirstOrDefaultAsync(cw => cw.Name.ToLower() == name.ToLower());

        public async Task<IEnumerable<Domain.Entities.CarWorkShop>> GetAll()
            => await _dbContext.CarWorkShops.ToListAsync();

        public async Task<Domain.Entities.CarWorkShop> GetByEncodedName(string encodedName)
            => await _dbContext.CarWorkShops.FirstAsync(c => c.EncodedName == encodedName);

        public Task Commit() => _dbContext.SaveChangesAsync();
    }
}
