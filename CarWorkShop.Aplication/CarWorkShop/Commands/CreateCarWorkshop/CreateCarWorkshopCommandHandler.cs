using AutoMapper;
using CarWorkShop.Application.ApplicationUser;
using CarWorkShop.Domain.Inferfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarWorkShop.Application.CarWorkShop.Commands.CreateCarWorkshop
{
    public class CreateCarWorkshopCommandHandler : IRequestHandler<CreateCarWorkshopCommand, Unit>
    {
        private readonly ICarWorkShopRepository _carWorkShopRepository;
        private readonly IMapper _mapper;
        private readonly IUserContext _userContext;

        public CreateCarWorkshopCommandHandler(ICarWorkShopRepository carWorkShopRepository, IMapper mapper,IUserContext userContext)
        {
            _carWorkShopRepository = carWorkShopRepository;
            _mapper = mapper;
            _userContext = userContext;
        }

        public async Task<Unit> Handle(CreateCarWorkshopCommand request, CancellationToken cancellationToken)
        {
            var currentUser = _userContext.GetCurrentUser();
            if (currentUser == null || !currentUser.IsInRole("Owner"))
            {
                return Unit.Value;
            }
            var carWorkShop = _mapper.Map<Domain.Entities.CarWorkShop>(request);
            carWorkShop.EncodeName();

            carWorkShop.CreatedById = currentUser.ID;

            await _carWorkShopRepository.Create(carWorkShop);

            return Unit.Value;
        }
    }
}
