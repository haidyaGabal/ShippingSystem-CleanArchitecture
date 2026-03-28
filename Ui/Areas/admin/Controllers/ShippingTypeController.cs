using BL.Contracts;
using BL.DTOs;
using BL.Services;
using Domains;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Ui.Areas.admin.Controllers
{
    [Area("admin")]
    public class ShippingTypeController : Controller
    {
        private readonly IShippingType shippingType;

        public ShippingTypeController(IShippingType shippingType)
        {
            this.shippingType = shippingType;
        }

        public IActionResult Index()
        {
            return View(shippingType.GetAll());
        }

        public IActionResult Edit(Guid id)
        {
            ShippingTypeDTO item;

            if (id != Guid.Empty)
            {
                item = shippingType.GetById(id);
                if (item == null)
                    return NotFound();
            }
            else
            {
                item = new ShippingTypeDTO();
            }

            return View(item);
        }
        public IActionResult Actions()
        {
            return View(shippingType.GetAll());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
      
        public async Task<IActionResult> Save(ShippingTypeDTO dto)
        {
            if (dto.Id == Guid.Empty)
               await shippingType.Add(dto, dto.Id);
            else
               await shippingType.Update(dto, dto.Id);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid Id)
        {
            await shippingType.ChangeStatus(Id, Guid.Empty, 0);
            return RedirectToAction(nameof(Index));
        }



    }
}