using BL.Contracts.Shipment;
using BL.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Ui.Areas.admin.Controllers
{
    [Area("admin")]

    public class ShippmentController : Controller
    {
        private readonly IShipment shippment;

        public ShippmentController(IShipment _shippment)
        {  
            this.shippment = _shippment;
        }

        public IActionResult Index()
        {
            return View(shippment.GetAll());
        }

        public IActionResult Edit(Guid id)
        {
            ShipmentDTO item;

            if (id != Guid.Empty)
            {
                item = shippment.GetById(id);
                if (item == null)
                    return NotFound();
            }
            else
            {
                item = new ShipmentDTO();
            }

            return View(item);
        }
        public IActionResult Actions()
        {
            return View(shippment.GetAll());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Save(ShipmentDTO dto)
        {
            if (dto.Id == Guid.Empty)
                shippment.Add(dto);
            else
                 shippment.Update(dto);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid Id)
        {
             shippment.ChangeStatus(Id, Guid.Empty, 0);
            return RedirectToAction(nameof(Index));
        }




    }
}
    