using BL.Contracts;
using BL.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Ui.Areas.admin.Controllers
{
    [Area("admin")]

    public class CounteryController : Controller
    {
        private readonly ICountry country;

        public CounteryController(ICountry _country)
        {
            country = _country;
        }

        public IActionResult Index()
        {
            return View(country.GetAll());
        }

        public IActionResult Edit(Guid id)
        {
            CountryDTO item;

            if (id != Guid.Empty)
            {
                item = country.GetById(id);
                if (item == null)
                    return NotFound();
            }
            else
            {
                item = new CountryDTO();
            }

            return View(item);
        }
        public IActionResult Actions()
        {
            return View(country.GetAll());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Save(CountryDTO dto)
        {
            if (dto.Id == Guid.Empty)
                await country.Add(dto, dto.Id);
            else
                await country.Update(dto, dto.Id);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid Id)
        {
            await country.ChangeStatus(Id, Guid.Empty, 0);
            return RedirectToAction(nameof(Index));
        }



    

    }
}
