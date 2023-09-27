using WEB_API.Model;

namespace WEB_API.Interfaces
{
    public interface IShipmentRepository
    {
        public List<Shipment> GetShipments();
    }
}
