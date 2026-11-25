using BL.Contracts.Shipment;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ui.Areas.admin.Controllers
{
    [Area("admin")]
    [Authorize(Roles = "Admin")]
    public class ShippmentController : Controller
    {
        private readonly IAdminShipment _shipment;

        public ShippmentController(IAdminShipment shipment) => _shipment = shipment;

        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10, int? state = null)
        {
            var pagination = new PaginationParams { PageNumber = pageNumber, PageSize = pageSize };
            ViewBag.State = state;
            return View(await _shipment.GetAll(pagination, state));
        }
        public async Task<IActionResult> Details(Guid id)
        {
            var model = await _shipment.GetById(id);
            if (model == null) return NotFound();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeStatus(Guid id, int newState, bool fromDetails = false)
        {
            try
            {
                await _shipment.ChangeStatus(id, newState);
                TempData["Success"] = "Status updated.";
            }
            catch (InvalidOperationException ex) { TempData["Error"] = ex.Message; }
            catch (ArgumentException ex) { TempData["Error"] = ex.Message; }

            return fromDetails
                ? RedirectToAction(nameof(Details), new { id })
                : RedirectToAction(nameof(Index));
        }
    }
}