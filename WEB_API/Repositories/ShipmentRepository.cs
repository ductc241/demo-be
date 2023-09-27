using Microsoft.EntityFrameworkCore;
using WEB_API.Interfaces;
using WEB_API.Model;

namespace WEB_API.Repositories
{
    public class ShipmentRepository: IShipmentRepository
    {
        private readonly DataContext _context;

        public ShipmentRepository(DataContext context) {
            _context = context;
        }

        public List<Shipment> GetShipments()
        {

            var shipmentList = _context.Shipments.Include(s => s.Shipment_Status).ToList();

            return shipmentList;
        }
    }
}
