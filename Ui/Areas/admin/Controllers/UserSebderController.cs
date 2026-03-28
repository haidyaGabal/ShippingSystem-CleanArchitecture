using BL.Contracts;
using BL.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Ui.Areas.admin.Controllers
{
    [Area("admin")]

    public class UserSebderController : Controller
    {
        private readonly IUserSebder userSebder;

        public UserSebderController(IUserSebder userSebder)
        {
            this.userSebder = userSebder;
        
        }

        public IActionResult Index()
        {
            return View(userSebder.GetAll());
        }

        public IActionResult Edit(Guid id)
        {
            UserSebderDTO item;

            if (id != Guid.Empty)
            {
                item = userSebder.GetById(id);
                if (item == null)
                    return NotFound();
            }
            else
            {
                item = new UserSebderDTO();
            }

            return View(item);
        }
        public IActionResult Actions()
        {
            return View(userSebder.GetAll());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Save(UserSebderDTO dto)
        {
            if (dto.Id == Guid.Empty)
                await userSebder.Add(dto, dto.Id);
            else
                await userSebder.Update(dto, dto.Id);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid Id)
        {
            await userSebder.ChangeStatus(Id, Guid.Empty, 0);
            return RedirectToAction(nameof(Index));
        }



    }

}
