using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WEB_API.Model
{
    [Table("shipments", Schema = "dbo")]
    public class Shipment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(50)]
        public string Customer_Name { get; set; } = String.Empty;

        [MaxLength(50)]
        public string Customer_Phone { get; set; } = String.Empty;

        [MaxLength(255)]
        public string Customer_Address { get; set; } = String.Empty;

        [Column(TypeName = "decimal(18,4)")]
        public decimal Shipping_Fee { get; set; }

        public string? Note { get; set; }

        public DateTime? Estimated_Delivery_Date { get; set; }
        public DateTime? Actual_Delivery_Date { get; set; }
        public DateTime? Estimated_Arrival_Date { get; set; }
        public DateTime? Actual_Arrival_Date { get; set; }


        // FK -> order
        public int Order_Id { get; set; }
        [ForeignKey("Order_Id")]
        public Order Order { get; set; }

        // FK -> shipment_status
        public int Shipment_Status_Id { get; set; }
        [ForeignKey("Shipment_Status_Id")]
        public Shipment_Status Shipment_Status { get; set; }

        // FK -> shipment_detail
        public Shipment_Detail Shipment_Detail { get; set; }

        // FK -> tracking
        public ICollection<Tracking> Trackings { get; set; }
    }
}
