using AutoMapper;
using CarWorkShop.Application.ApplicationUser;
using CarWorkShop.Application.CarWorkShop;
using CarWorkShop.Application.CarWorkShop.Commands.EditCarWorkshop;
using CarWorkShop.Application.CarWorkshopService;
using CarWorkShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarWorkShop.Application.Mappings
{
    public class CarWorkShopMappingProfile : Profile
    {
        public CarWorkShopMappingProfile(IUserContext userContext) 
        {
            var user = userContext.GetCurrentUser();
            CreateMap<CarWorkShopDTO, Domain.Entities.CarWorkShop>()
                .ForMember(e => e.ContactDetails, opt => opt.MapFrom(src => new CarWorkShopContactDetails()
                {
                    City = src.City,
                    PhoneNumber = src.PhoneNumber,
                    PostalCode = src.PostalCode,
                    Street = src.Street,
                }));

            CreateMap<Domain.Entities.CarWorkShop, CarWorkShopDTO>()
                .ForMember(dto => dto.IsEditable, opt => opt.MapFrom(src => user != null && (src.CreatedById == user.ID || user.IsInRole("Moderator"))))
                .ForMember(dto => dto.Street, opt => opt.MapFrom(src => src.ContactDetails.Street))
                .ForMember(dto => dto.City, opt => opt.MapFrom(src => src.ContactDetails.City))
                .ForMember(dto => dto.PostalCode, opt => opt.MapFrom(src => src.ContactDetails.PostalCode))
                .ForMember(dto => dto.PhoneNumber, opt => opt.MapFrom(src => src.ContactDetails.PhoneNumber));


            CreateMap<CarWorkShopDTO, EditCarWorkshopCommand>();

            CreateMap<CarWorkshopServiceDto, Domain.Entities.CarWorkshopService>()
                .ReverseMap();

        }
    }
}
