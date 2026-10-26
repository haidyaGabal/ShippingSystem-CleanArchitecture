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
            CreateMap<TbCarrier,CarrierDTO>().ReverseMap();
            CreateMap<TbCity, CityDTO>().ReverseMap();
            CreateMap<TbRefreshToken, RefreshTokenDTO>().ReverseMap();
            CreateMap<TbCountry, CountryDTO>().ReverseMap();
            CreateMap<TbPaymentMethod, PaymentMethodDTO>().ReverseMap();
            CreateMap<TbSetting, SettingDTO>().ReverseMap();
            CreateMap<TbShipingType, ShipingTypeDTO>().ReverseMap();
            CreateMap<TbShipingPackage, ShipingPackageDTO>().ReverseMap();
            CreateMap<TbShipment, ShipmentDTO>().ReverseMap();
            CreateMap<TbShipmentStatus, TbShipmentStatus>().ReverseMap();
            CreateMap<TbSubscriptionPackage, SubscriptionPackageDTO>().ReverseMap();
            CreateMap<TbUserReceiver, UserReceiverDTO>()
                .ForMember(d => d.CountryId, o => o.MapFrom(s => s.City != null ? s.City.CountryId : Guid.Empty))
                .ReverseMap()
                .ForMember(e => e.City, o => o.Ignore());

            CreateMap<TbUserSender, UserSenderDTO>()
                .ForMember(d => d.CountryId, o => o.MapFrom(s => s.City != null ? s.City.CountryId : Guid.Empty))
                .ReverseMap()
                .ForMember(e => e.City, o => o.Ignore());
            CreateMap<TbUserSubscription, UserSubscriptionDTO>().ReverseMap();

        }
    }
}
