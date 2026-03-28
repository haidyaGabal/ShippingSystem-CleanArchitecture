using BL.Contracts;
using BL.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Ui.Areas.admin.Controllers
{
    [Area("admin")]

    public class UserSubscriptionController : Controller
    {
        private readonly IUserSubscription userSubscription;

        public UserSubscriptionController(IUserSubscription _userSubscription)
        { 
            this.userSubscription = _userSubscription;
        }

        public IActionResult Index()
        {
            return View(userSubscription.GetAll());
        }

        public IActionResult Edit(Guid id)
        {
            UserSubscriptionDTO item;

            if (id != Guid.Empty)
            {
                item = userSubscription.GetById(id);
                if (item == null)
                    return NotFound();
            }
            else
            {
                item = new UserSubscriptionDTO();
            }

            return View(item);
        }
        public IActionResult Actions()
        {
            return View(userSubscription.GetAll());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Save(UserSubscriptionDTO dto)
        {
            if (dto.Id == Guid.Empty)
                await userSubscription.Add(dto, dto.Id);
            else
                await userSubscription.Update(dto, dto.Id);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid Id)
        {
            await userSubscription.ChangeStatus(Id, Guid.Empty, 0);
            return RedirectToAction(nameof(Index));
        }



    }

    
}
