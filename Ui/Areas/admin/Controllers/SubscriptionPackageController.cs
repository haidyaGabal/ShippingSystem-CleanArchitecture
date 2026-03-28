using BL.Contracts;
using BL.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Ui.Areas.admin.Controllers
{
    [Area("admin")]

    public class SubscriptionPackageController : Controller
    {
        private readonly ISubscriptionPackage subscriptionPackage;

        public SubscriptionPackageController(ISubscriptionPackage _subscriptionPackage)
        {
            this.subscriptionPackage = _subscriptionPackage;
        }

        public IActionResult Index()
        {
            return View(subscriptionPackage.GetAll());
        }

        public IActionResult Edit(Guid id)
        {
            SubscriptionPackageDTO item;

            if (id != Guid.Empty)
            {
                item = subscriptionPackage.GetById(id);
                if (item == null)
                    return NotFound();
            }
            else
            {
                item = new SubscriptionPackageDTO();
            }

            return View(item);
        }
        public IActionResult Actions()
        {
            return View(subscriptionPackage.GetAll());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Save(SubscriptionPackageDTO dto)
        {
            if (dto.Id == Guid.Empty)
                await subscriptionPackage.Add(dto, dto.Id);
            else
                await subscriptionPackage.Update(dto, dto.Id);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid Id)
        {
            await subscriptionPackage.ChangeStatus(Id, Guid.Empty, 0);
            return RedirectToAction(nameof(Index));
        }



    }
}
