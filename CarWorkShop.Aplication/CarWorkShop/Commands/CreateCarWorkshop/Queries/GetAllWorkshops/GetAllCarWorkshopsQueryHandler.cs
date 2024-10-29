using AutoMapper;
using CarWorkShop.Domain.Inferfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarWorkShop.Application.CarWorkShop.Commands.CreateCarWorkshop.Queries.GetAllWorkshops
{
    public class GetAllCarWorkshopsQueryHandler : IRequestHandler<GetAllCarWorkshopsQuery, IEnumerable<CarWorkShopDTO>>
    {
        private readonly ICarWorkShopRepository _carWorkShopRepository;
        private readonly IMapper _mapper;

        public GetAllCarWorkshopsQueryHandler(ICarWorkShopRepository carWorkShopRepository, IMapper mapper) 
        {
            _carWorkShopRepository = carWorkShopRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<CarWorkShopDTO>> Handle(GetAllCarWorkshopsQuery request, CancellationToken cancellationToken)
        {
            var carWorkShops = await _carWorkShopRepository.GetAll();

            var dtos = _mapper.Map<IEnumerable<CarWorkShopDTO>>(carWorkShops);

            return dtos;
        }
    }
}
