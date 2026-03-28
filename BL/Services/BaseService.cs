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

        public BaseService(IRepository<T> repository, IMapper mapper)
        {
            _repo = repository;
            _mapper = mapper;
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

        public async Task<bool> Add(DTO entity, Guid userId)
        {
            var dtObj = _mapper.Map<DTO, T>(entity);
            dtObj.CreatedBy = userId;
            return await _repo.Add(dtObj);
        }

        public async Task<bool> Update(DTO entity, Guid userId)
        {
            var dtObj = _mapper.Map<DTO, T>(entity);
            dtObj.UpdatedBy = userId;
            return await _repo.Update(dtObj);
        }

        public async Task<bool> ChangeStatus(Guid id, Guid userId, int status = 1)
        {
            return await _repo.ChangeStatus(id, status);
        }
    }

}
