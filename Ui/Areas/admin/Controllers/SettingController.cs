using BL.Contracts;
using BL.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Ui.Areas.admin.Controllers
{
    [Area("admin")]

    public class SettingController : Controller
    {
        private readonly ISetting setting;

        public SettingController(ISetting _setting)
        {
            this.setting = _setting;
        }

        public IActionResult Index()
        {
            return View(setting.GetAll());
        }

        public IActionResult Edit(Guid id)
        {
            SettingDTO item;

            if (id != Guid.Empty)
            {
                item = setting.GetById(id);
                if (item == null)
                    return NotFound();
            }
            else
            {
                item = new SettingDTO();
            }

            return View(item);
        }
        public IActionResult Actions()
        {
            return View(setting.GetAll());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Save(SettingDTO dto)
        {
            if (dto.Id == Guid.Empty)
                await setting.Add(dto, dto.Id);
            else
                await setting.Update(dto, dto.Id);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid Id)
        {
            await setting.ChangeStatus(Id, Guid.Empty, 0);
            return RedirectToAction(nameof(Index));
        }



    }
}