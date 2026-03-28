using BL.Contracts;
using BL.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Ui.Areas.admin.Controllers
{
    [Area("admin")]

    public class ShippmentController : Controller
    {
        private readonly IShippment shippment;

        public ShippmentController(IShippment _shippment)
        {  
            this.shippment = _shippment;
        }

        public IActionResult Index()
        {
            return View(shippment.GetAll());
        }

        public IActionResult Edit(Guid id)
        {
            ShippmentDTO item;

            if (id != Guid.Empty)
            {
                item = shippment.GetById(id);
                if (item == null)
                    return NotFound();
            }
            else
            {
                item = new ShippmentDTO();
            }

            return View(item);
        }
        public IActionResult Actions()
        {
            return View(shippment.GetAll());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Save(ShippmentDTO dto)
        {
            if (dto.Id == Guid.Empty)
                await shippment.Add(dto, dto.Id);
            else
                await shippment.Update(dto, dto.Id);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid Id)
        {
            await shippment.ChangeStatus(Id, Guid.Empty, 0);
            return RedirectToAction(nameof(Index));
        }



    }
}
    