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
    public class UserReceiverService : BaseService<TbUserReceiver, UserReceiverDTO>, IUserReceiver
    {
        private readonly IRepository<TbUserReceiver> _repo;
        private readonly IMapper _mapper;
        IUnitOfWork _uow;
        IUserService _userService;

        public UserReceiverService(IUnitOfWork uow, IMapper mapper, IUserService userService) :
        base(uow, mapper, userService)
        {
           
            _uow = uow;
            _mapper = mapper;
            _userService = userService;
        }
       
    }
}
