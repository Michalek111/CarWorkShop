using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarWorkShop.Application.CarWorkshopService.Commands
{
    public class CreateCarWorkshopServiceCommand : CarWorkshopServiceDto, IRequest<Unit>
    {
        public string CarWorkshopEncodedName { get; set; } = default!;
    }
}
