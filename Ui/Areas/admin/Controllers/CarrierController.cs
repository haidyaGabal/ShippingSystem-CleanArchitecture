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
    public class CarrierController : Controller
    {
        private readonly ICarrier carrier;

        public CarrierController(ICarrier _carrier)
        {
       
            this.carrier = _carrier;
        }

        public IActionResult Index()
        {
            return View(carrier.GetAll());
        }

        public IActionResult Edit(Guid id)
        {
            CarrierDTO item;

            if (id != Guid.Empty)
            {
                item = carrier.GetById(id);
                if (item == null)
                    return NotFound();
            }
            else
            {
                item = new CarrierDTO();
            }

            return View(item);
        }
        public IActionResult Actions()
        {
            return View(carrier.GetAll());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
      
        public async Task<IActionResult> Save(CarrierDTO dto)
        {
            if (dto.Id == Guid.Empty)
               await carrier.Add(dto, dto.Id);
            else
               await carrier.Update(dto, dto.Id);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid Id)
        {
            await carrier.ChangeStatus(Id, Guid.Empty, 0);
            return RedirectToAction(nameof(Index));
        }



    }
}