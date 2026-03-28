using BL.Contracts;
using BL.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Ui.Areas.admin.Controllers
{
    [Area("admin")]

    public class UserReceiverController : Controller
    {
        private readonly IUserReceiver userReceiver;

        public UserReceiverController(IUserReceiver _userReceiver)
        {
            this.userReceiver = _userReceiver;
        }

        public IActionResult Index()
        {
            return View(userReceiver.GetAll());
        }

        public IActionResult Edit(Guid id)
        {
            UserReceiverDTO item;

            if (id != Guid.Empty)
            {
                item = userReceiver.GetById(id);
                if (item == null)
                    return NotFound();
            }
            else
            {
                item = new UserReceiverDTO();
            }

            return View(item);
        }
        public IActionResult Actions()
        {
            return View(userReceiver.GetAll());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Save(UserReceiverDTO dto)
        {
            if (dto.Id == Guid.Empty)
                await userReceiver.Add(dto, dto.Id);
            else
                await userReceiver.Update(dto, dto.Id);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid Id)
        {
            await userReceiver.ChangeStatus(Id, Guid.Empty, 0);
            return RedirectToAction(nameof(Index));
        }



    }
}