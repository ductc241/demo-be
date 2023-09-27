using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WEB_API.Model
{
    [Table("orders", Schema = "dbo")]
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int Customer_Id {  get; set; }

        [MaxLength(50)]
        public string Customer_Name { get; set; } = String.Empty;

        [MaxLength(50)]
        public string Customer_Phone { get; set; } = String.Empty;

        [MaxLength(255)]
        public string Shipping_Address { get; set; } = String.Empty;

        public int Total_Amount { get; set; }

        public DateTime Order_Date { get; set;}

        [MaxLength(255)]
        public string Payment_Method { get; set; } = String.Empty;

        //public string Status { get; set; } = String.Empty;

        // FK -> Shipment
        public Shipment Shipment { get; set; }

        public ICollection<Order_Item> Order_Item { get; set; }
    }
}
