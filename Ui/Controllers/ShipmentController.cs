
using BL.Contracts.Shipment;
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
        public async Task<IActionResult> List()
        {
            var shipment=await _shipment.GetShipments();
            return View(shipment);
        }

        
    }
} 
