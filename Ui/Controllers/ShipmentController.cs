using BL.Contracts.Shipment;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ui.Controllers
{
    [Authorize]
    public class ShipmentController : Controller
    {
        private readonly ILogger<ShipmentController> _logger;
        private readonly IShipment _shipment;

        public ShipmentController(ILogger<ShipmentController> logger, IShipment shipment)
        {
            _logger = logger;
            _shipment = shipment;
        }

        public IActionResult Index() => View();

        public IActionResult Create() => View();

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

        public IActionResult Edit(Guid id) => View();

        public IActionResult Show(Guid id) => View();
    }
}