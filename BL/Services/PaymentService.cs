using AutoMapper;
using BL.Contracts;
using BL.DTOs;
using Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    public class PaymentService : BaseService<TbPaymentMethod, PaymentMethodDTO>, IPaymentMethod
    {
        public PaymentService(IRepository<TbPaymentMethod> repo, IMapper mapper) : base(repo, mapper)
        {

        }
    }
}
