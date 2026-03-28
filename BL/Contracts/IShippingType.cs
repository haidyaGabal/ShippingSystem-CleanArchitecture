using BL.DTOs;
using Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Contracts
{
    public interface IShippingType : IBaseService<TbShippingType, ShippingTypeDTO>
    {

        ///base Solid Principles if you need to add more methods for shippingtype table
        ///can you create here rather than implement all method shared for all Business layer 
        ///here can write more method 
        
        //TbShippingType? GetById(Guid id);
        //List<TbShippingType> GetAll();
        //bool Add(TbShippingType entity,Guid userId);
        //bool Update(TbShippingType entity, Guid userId);
        //public bool ChangeStatus(Guid id, Guid userId, int status = 1);
    }
 
}
