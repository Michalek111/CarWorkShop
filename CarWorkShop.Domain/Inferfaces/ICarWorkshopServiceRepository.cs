using CarWorkShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarWorkShop.Domain.Inferfaces
{
    public interface ICarWorkshopServiceRepository
    {
        Task Create(CarWorkshopService carWorkshopService);
        Task<IEnumerable<CarWorkshopService>> GetAllByEncodedName(string encodedName);
    }
}
