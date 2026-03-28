using BL.Contracts;
using BL.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Ui.Areas.admin.Controllers
{
    [Area("admin")]

    public class ShippmentStatusController : Controller
    {
        private readonly IShippmentStatus shippmentStatus;

        public ShippmentStatusController(IShippmentStatus _shippmentStatus)
        {
            this.shippmentStatus = _shippmentStatus;    
        }

        public IActionResult Index()
        {
            return View(shippmentStatus.GetAll());
        }

        public IActionResult Edit(Guid id)
        {
            ShippmentStatusDTO item;

            if (id != Guid.Empty)
            {
                item = shippmentStatus.GetById(id);
                if (item == null)
                    return NotFound();
            }
            else
            {
                item = new ShippmentStatusDTO();
            }

            return View(item);
        }
        public IActionResult Actions()
        {
            return View(shippmentStatus.GetAll());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Save(ShippmentStatusDTO dto)
        {
            if (dto.Id == Guid.Empty)
                await shippmentStatus.Add(dto, dto.Id);
            else
                await shippmentStatus.Update(dto, dto.Id);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid Id)
        {
            await shippmentStatus.ChangeStatus(Id, Guid.Empty, 0);
            return RedirectToAction(nameof(Index));
        }



    }
}
    