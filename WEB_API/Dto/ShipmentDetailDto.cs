using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WEB_API.Dto
{
    public class ShipmentDetailDto
    {
        public string Shipping_Method { get; set; }
        public string Driver_Name { get; set; }
        public string Driver_Phone { get; set; }
        public int Quantity { get; set; }
        public decimal Weight { get; set; }
        public string Packaging_Type { get; set; }
        public string Barcode { get; set; }
    }
}
