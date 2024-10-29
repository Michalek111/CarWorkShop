using AutoMapper;
using CarWorkShop.Domain.Inferfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarWorkShop.Application.CarWorkShop.Commands.CreateCarWorkshop.Queries.GetCarWorkShopByEncodedName
{
    public class GetCarWorkShopByEncodedNameQueryHandler : IRequestHandler<GetCarWorkShopByEncodedNameQuery, CarWorkShopDTO>
    {
        private readonly ICarWorkShopRepository _carWorkShopRepository;
        private readonly IMapper _mapper;

        public GetCarWorkShopByEncodedNameQueryHandler(ICarWorkShopRepository carWorkShopRepository, IMapper mapper)
        {
            _carWorkShopRepository = carWorkShopRepository;
            _mapper = mapper;
        }
        public async Task<CarWorkShopDTO> Handle(GetCarWorkShopByEncodedNameQuery request, CancellationToken cancellationToken)
        {
            var carWorkshop = await _carWorkShopRepository.GetByEncodedName(request.EncodedName);

            var dto = _mapper.Map<CarWorkShopDTO>(carWorkshop);
            return dto; 

        }
    }
}
