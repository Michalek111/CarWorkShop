using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarWorkShop.Application.CarWorkShop.Commands.CreateCarWorkshop
{
    public class CreateCarWorkshopCommand : CarWorkShopDTO, IRequest<Unit>
    {
      
    }
}
