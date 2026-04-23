using BL.Contracts;
using BL.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Ui.Areas.admin.Controllers
{
    [Area("admin")]

    public class ShippmentStatusController : Controller
    {
        private readonly IShipmentStatus shippmentStatus;

        public ShippmentStatusController(IShipmentStatus _shippmentStatus)
        {
            this.shippmentStatus = _shippmentStatus;    
        }

        public IActionResult Index()
        {
            return View(shippmentStatus.GetAll());
        }

        public IActionResult Edit(Guid id)
        {
            ShipmentStatusDTO item;

            if (id != Guid.Empty)
            {
                item = shippmentStatus.GetById(id);
                if (item == null)
                    return NotFound();
            }
            else
            {
                item = new ShipmentStatusDTO();
            }

            return View(item);
        }
        public IActionResult Actions()
        {
            return View(shippmentStatus.GetAll());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Save(ShipmentStatusDTO dto)
        {
            if (dto.Id == Guid.Empty)
                shippmentStatus.Add(dto);
            else
                 shippmentStatus.Update(dto);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid Id)
        {
             shippmentStatus.ChangeStatus(Id, Guid.Empty, 0);
            return RedirectToAction(nameof(Index));
        }



    }
}
    