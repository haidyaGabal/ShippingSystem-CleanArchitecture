
using BL.Contracts.Shipment;
using DAL.Models;
using Domains;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Ui.Controllers
{
    public class ShipmentController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IShipment _shipment;
        public ShipmentController(ILogger<HomeController> logger, IShipment shipment)
        {
            _logger = logger;
            _shipment = shipment;
            
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
         
            return View();
        }
        //public async Task<IActionResult> List()
        //{
        //    var shipment=await _shipment.GetShipments();
        //    return View(shipment);
        //}
        public async Task<IActionResult> List(int pageNumber = 1, int pageSize = 10)
        {
            var pagination = new PaginationParams
            {
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            var shipments = await _shipment.GetShipments(pagination);
            return View(shipments);
        }


    }
} 
