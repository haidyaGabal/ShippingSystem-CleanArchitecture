using BL.Contracts;
using BL.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Ui.Areas.admin.Controllers
{
    [Area("admin")]

    public class UserSebderController : Controller
    {
        private readonly IUserSender userSebder;

        public UserSebderController(IUserSender userSebder)
        {
            this.userSebder = userSebder;
        
        }

        public IActionResult Index()
        {
            return View(userSebder.GetAll());
        }

        public IActionResult Edit(Guid id)
        {
            UserSenderDTO item;

            if (id != Guid.Empty)
            {
                item = userSebder.GetById(id);
                if (item == null)
                    return NotFound();
            }
            else
            {
                item = new UserSenderDTO();
            }

            return View(item);
        }
        public IActionResult Actions()
        {
            return View(userSebder.GetAll());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Save(UserSenderDTO dto)
        {
            if (dto.Id == Guid.Empty)
                 userSebder.Add(dto);
            else
                 userSebder.Update(dto);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid Id)
        {
             userSebder.ChangeStatus(Id, Guid.Empty, 0);
            return RedirectToAction(nameof(Index));
        }



    }

}
