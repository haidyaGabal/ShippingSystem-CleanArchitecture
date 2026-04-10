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
    public class UserSenderService : BaseService<TbUserSender, UserSenderDTO>, IUserSender
    {

        private readonly IMapper _mapper;
        IUnitOfWork _uow;


        public UserSenderService(IUnitOfWork uow, IMapper mapper) :
            base(uow, mapper)
        {

         
            _uow = uow;
            _mapper = mapper;
        }

        
    }
    
}
