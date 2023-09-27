using AutoMapper;
using WEB_API.Dto;
using WEB_API.Model;

namespace WEB_API
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile() {
            CreateMap<Shipment, ShipmentDto>();
        }
    }
}
