using WEB_API.Model;

namespace WEB_API.Dto
{
    public class ShipmentDto
    {
        public int? Id { get; set; }
        public string Customer_Name { get; set; }
        public string Customer_Phone { get; set; }
        public string Customer_Address { get; set; }
        public decimal Shipping_Fee { get; set; }   
        public string Note { get; set; }
        public DateTime Estimated_Delivery_Date { get; set; }
        public DateTime Actual_Delivery_Date { get; set; }
        public DateTime Estimated_Arrival_Date { get; set; }
        public DateTime Actual_Arrival_Date { get; set; }

        // FK
        public int Order_Id { get; set; }
        public int Shipment_Status_Id { get; set; }
    }
}
