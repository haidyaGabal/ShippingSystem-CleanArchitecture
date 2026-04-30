
using BL.Contracts.Shipment;
using Domains;
using Microsoft.AspNetCore.Mvc;

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

        
    }
} 
