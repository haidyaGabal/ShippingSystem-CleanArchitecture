using BL.Contracts;
using BL.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Ui.Areas.admin.Controllers
{
    [Area("admin")]

    public class PaymentController : Controller
    {
        private readonly IPaymentMethod payment;

        public PaymentController(IPaymentMethod _payment)
        {
            this.payment = _payment;
        }

        public IActionResult Index()
        {
            return View(payment.GetAll());
        }

        public IActionResult Edit(Guid id)
        {
            PaymentMethodDTO item;

            if (id != Guid.Empty)
            {
                item = payment.GetById(id);
                if (item == null)
                    return NotFound();
            }
            else
            {
                item = new PaymentMethodDTO();
            }

            return View(item);
        }
        public IActionResult Actions()
        {
            return View(payment.GetAll());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Save(PaymentMethodDTO dto)
        {
            if (dto.Id == Guid.Empty)
                await payment.Add(dto, dto.Id);
            else
                await payment.Update(dto, dto.Id);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid Id)
        {
            await payment.ChangeStatus(Id, Guid.Empty, 0);
            return RedirectToAction(nameof(Index));
        }



    }
}
  