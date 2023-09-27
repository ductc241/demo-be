using System.ComponentModel.DataAnnotations;
using WEB_API.Model;

namespace WEB_API.Dto
{
    public class Order_Product
    {
        public int Product_Id { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
        public int Amount { get; set; }
    }

    public class Order_Create
    {
        public int? Customer_Id { get; set; }

        public string Customer_Name { get; set; }

        public string Customer_Phone { get; set; }

        public string Shipping_Address { get; set; }

        public int Total_Amount { get; set; }

        public DateTime Order_Date { get; set; }

        public string Payment_Method { get; set; }

        public string Status { get; set; }

        public IList<Order_Product> Products { get; set; }
    }
}
