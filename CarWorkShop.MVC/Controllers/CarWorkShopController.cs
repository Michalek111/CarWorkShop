using AutoMapper;
using CarWorkShop.Application.ApplicationUser;
using CarWorkShop.Application.CarWorkShop;
using CarWorkShop.Application.CarWorkShop.Commands.CreateCarWorkshop;
using CarWorkShop.Application.CarWorkShop.Commands.CreateCarWorkshop.Queries.GetAllWorkshops;
using CarWorkShop.Application.CarWorkShop.Commands.CreateCarWorkshop.Queries.GetCarWorkShopByEncodedName;
using CarWorkShop.Application.CarWorkShop.Commands.EditCarWorkshop;
using CarWorkShop.Application.CarWorkshopService.Commands;
using CarWorkShop.Application.CarWorkshopService.Queries.GetCarWorkshopServices;
using CarWorkShop.MVC.Extensions;
using CarWorkShop.MVC.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CarWorkShop.MVC.Controllers
{
    public interface IUserContext
    {
        CurrentUser GetCurrentUser();
    }
}
public class CarWorkShopController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CarWorkShopController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

       
        public async Task<IActionResult> Index()
        {
            var carWorkShop =  await _mediator.Send(new GetAllCarWorkshopsQuery());
            return View(carWorkShop);
        }


        [Route("CarWorkshop/{encodedName}/Details")]
        public async Task<IActionResult> Details(string encodedName)
        {
           var dto = await _mediator.Send(new GetCarWorkShopByEncodedNameQuery(encodedName));
            return View(dto);
        }

        [Route("CarWorkshop/{encodedName}/Edit")]
        public async Task<IActionResult> Edit(string encodedName)
        {
            var dto = await _mediator.Send(new GetCarWorkShopByEncodedNameQuery(encodedName));

            if(!dto.IsEditable)
            {
            return RedirectToAction("NoAccess", "Home");
            }    

            EditCarWorkshopCommand model = _mapper.Map<EditCarWorkshopCommand>(dto);

            return View(model);
        }

        [HttpPost]
        [Route("CarWorkshop/{encodedName}/Edit")]
        public async Task<IActionResult> Edit(string encodedName,EditCarWorkshopCommand command)
        {
            if (!ModelState.IsValid)
            {
                return View(command);
            }
            await _mediator.Send(command);
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles ="Owner")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Owner")]
        public async Task<IActionResult> Create(CreateCarWorkshopCommand command)
        {
            if(!ModelState.IsValid)
            {
                return View(command);
            }

            await _mediator.Send(command);

            this.SetNotification("success", $"Created carworkshop: {command.Name}");

            return RedirectToAction(nameof(Index)); 
            
        }


        [HttpPost]
        [Authorize(Roles = "Owner")]
        [Route("CarWorkshop/CarWorkshopService")]
        public async Task<IActionResult> CreateCarWorkshopService(CreateCarWorkshopServiceCommand command)
        {
            if (!ModelState.IsValid)
            {
            return BadRequest(ModelState);
            }

            await _mediator.Send(command);

            return Ok();
        }

        [HttpGet]
        [Route("CarWorkshop/{encodedName}/CarWorkshopService")]
        public async Task<IActionResult> GetCarWorkshopServices(string encodedName)
        {
            var data = await _mediator.Send(new GetCarWorkshopServicesQuery() { EncodedName = encodedName });
            return Ok(data);
        }

}

