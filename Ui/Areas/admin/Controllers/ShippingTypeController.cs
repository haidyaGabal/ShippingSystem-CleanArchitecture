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
        private readonly IShipingType shippingType;

        public ShippingTypeController(IShipingType shippingType)
        {
            this.shippingType = shippingType;
        }

        public IActionResult Index()
        {
            return View(shippingType.GetAll());
        }

        public IActionResult Edit(Guid id)
        {
            ShipingTypeDTO item;

            if (id != Guid.Empty)
            {
                item = shippingType.GetById(id);
                if (item == null)
                    return NotFound();
            }
            else
            {
                item = new ShipingTypeDTO();
            }

            return View(item);
        }
        public IActionResult Actions()
        {
            return View(shippingType.GetAll());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
      
        public async Task<IActionResult> Save(ShipingTypeDTO dto)
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