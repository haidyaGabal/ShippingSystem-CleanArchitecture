using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BL.DTOs;
using Domains;

namespace BL.Mapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<TbCarrier,CityDTO>().ReverseMap();
            CreateMap<TbCity, CityDTO>().ReverseMap();
            CreateMap<TbRefreshToken, RefershTokenDTO>().ReverseMap();
            CreateMap<TbCountry, CountryDTO>().ReverseMap();
            CreateMap<TbPaymentMethod, PaymentMethodDTO>().ReverseMap();
            CreateMap<TbSetting, SettingDTO>().ReverseMap();
            CreateMap<TbShippingType, ShippingTypeDTO>().ReverseMap();
            CreateMap<TbShippment, ShippmentDTO>().ReverseMap();
            CreateMap<TbShippmentStatus, TbShippmentStatus>().ReverseMap();
            CreateMap<TbSubscriptionPackage, SubscriptionPackageDTO>().ReverseMap();
            CreateMap<TbUserReceiver, UserReceiverDTO>().ReverseMap();
            CreateMap<TbUserSebder, UserSebderDTO>().ReverseMap();
            CreateMap<TbUserSubscription, UserSubscriptionDTO>().ReverseMap();

        }
    }
}
