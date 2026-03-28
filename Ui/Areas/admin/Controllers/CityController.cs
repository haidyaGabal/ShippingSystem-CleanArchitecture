using BL.Contracts;
using BL.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Ui.Areas.admin.Controllers
{
    [Area("admin")]
    public class CityController : Controller
    {
        private readonly ICity _city;
        private readonly ICountry _country;

        public CityController(ICity city, ICountry country)
        {
            _city = city;
            _country = country;
        }

        // List all cities for a specific country
        public IActionResult Actions(Guid countryId)
        {
            ViewBag.SelectedCountryId = countryId; // for Add New button
            ViewBag.lstCountry = _country.GetAll();

            var cities = _city.GetByCountryId(countryId);
            return View(cities);
        }

        
        [HttpGet]
        public IActionResult Edit(Guid? id, Guid countryId)
        {
            CityDTO item;

            if (id.HasValue && id.Value != Guid.Empty)
            {
                item = _city.GetById(id.Value);
                if (item == null)
                    return NotFound();
            }
            else
            {
             
                item = new CityDTO
                {
                    CountryId = countryId
                };
            }

            return View(item);
        }

    
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(CityDTO dto)
        {
            if (!ModelState.IsValid)
                return View("Edit", dto);

            if (dto.Id == Guid.Empty) 
                await _city.Add(dto, dto.Id);
            else
                await _city.Update(dto, dto.Id);


            return RedirectToAction(nameof(Actions), new { countryId = dto.CountryId });
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id, Guid countryId)
        {
            await _city.ChangeStatus(id, Guid.Empty, 0);
            return RedirectToAction(nameof(Actions), new { countryId });
        }
    }
}
