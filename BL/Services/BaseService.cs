using BL.Contracts;
using Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BL.DTOs.Base;
using AutoMapper;

namespace BL.Services
{
    public class BaseService<T, DTO> : IBaseService<T, DTO>
     where T : BaseEntity
     where DTO : BaseDTOs
    {
        protected readonly IRepository<T> _repo;
        protected readonly IMapper _mapper;
        protected readonly IUnitOfWork _unitOfWork;
        IUserService _userService;

        public BaseService(IRepository<T> repository, IMapper mapper)
        {
            _repo = repository;
            _mapper = mapper;
        }
        public BaseService(IUnitOfWork unitOfWork, IMapper mapper, IUserService userService)
        {
            _unitOfWork = unitOfWork;
            _repo = _unitOfWork.Repository<T>();
            _mapper = mapper;
            _userService = userService;
        }

        public List<DTO> GetAll()
        {
            var list = _repo.GetAll();
            return _mapper.Map<List<T>, List<DTO>>(list);
        }

        public DTO? GetById(Guid id)
        {
            var obj = _repo.GetById(id);
            return _mapper.Map<T, DTO>(obj);
        }

        public bool Add(DTO entity)
        {
            var dtObj = _mapper.Map<DTO, T>(entity);
            dtObj.CreatedBy = _userService.GetLoggedInUser();
            return  _repo.Add(dtObj);
        }
        public bool Add(DTO entity, out Guid id)
        {
            var dtObj = _mapper.Map<DTO, T>(entity);
            dtObj.CreatedBy = _userService.GetLoggedInUser();
            return _repo.Add(dtObj,out id);
        }

        public bool Update(DTO entity)
        {
            var dtObj = _mapper.Map<DTO, T>(entity);
            dtObj.UpdatedBy = _userService.GetLoggedInUser();
            return  _repo.Update(dtObj);
        }

        public bool ChangeStatus(Guid id, Guid userId, int status = 1)
        {
            return  _repo.ChangeStatus(id,_userService.GetLoggedInUser(), status);
        }

       
    }

}
