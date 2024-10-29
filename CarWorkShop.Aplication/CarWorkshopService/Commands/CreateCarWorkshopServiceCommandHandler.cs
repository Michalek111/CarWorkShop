using CarWorkShop.Application.ApplicationUser;
using CarWorkShop.Domain.Inferfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarWorkShop.Application.CarWorkshopService.Commands
{
    public class CreateCarWorkshopServiceCommandHandler : IRequestHandler<CreateCarWorkshopServiceCommand,Unit>
    {
        private readonly IUserContext _userContext;
        private readonly ICarWorkShopRepository _carWorkShopRepository;
        private readonly ICarWorkshopServiceRepository _carWorkshopServiceRepository;

        public CreateCarWorkshopServiceCommandHandler(IUserContext userContext, ICarWorkShopRepository carWorkShopRepository,ICarWorkshopServiceRepository carWorkshopServiceRepository) 
        {
            _userContext= userContext;
            _carWorkShopRepository= carWorkShopRepository;
            _carWorkshopServiceRepository = carWorkshopServiceRepository;
        }
        public async Task<Unit> Handle(CreateCarWorkshopServiceCommand request, CancellationToken cancellationToken)
        {
            var carWorkshop = await _carWorkShopRepository.GetByEncodedName(request.CarWorkshopEncodedName!);

            var user = _userContext.GetCurrentUser();
            var isEditable = user != null && (carWorkshop.CreatedById == user.ID || user.IsInRole("Moderator"));


            if (!isEditable)
            {
                return Unit.Value;
            }

            var carWorkshopService = new Domain.Entities.CarWorkshopService()
            {
                Cost = request.Cost,
                Description = request.Description,
                carWorkshopId = carWorkshop.Id,
               
            };

            await _carWorkshopServiceRepository.Create(carWorkshopService);

            return Unit.Value;
        }
    }
}
