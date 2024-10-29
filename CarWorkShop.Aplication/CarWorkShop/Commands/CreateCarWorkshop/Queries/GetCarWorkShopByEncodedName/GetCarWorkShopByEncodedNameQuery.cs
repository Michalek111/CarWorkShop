using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarWorkShop.Application.CarWorkShop.Commands.CreateCarWorkshop.Queries.GetCarWorkShopByEncodedName
{
    public class GetCarWorkShopByEncodedNameQuery : IRequest<CarWorkShopDTO>
    {
        public string EncodedName { get; set; }

        public GetCarWorkShopByEncodedNameQuery(string encodedName)
        {
            EncodedName = encodedName;
        }   
    }
}
