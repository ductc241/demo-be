using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WEB_API.Model
{
    [Table("shipment_detail", Schema = "dbo")]
    public class Shipment_Detail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(50)]
        public string? Shipping_Method { get; set; }

        [MaxLength(50)]
        public string? Driver_Name { get; set; }

        [MaxLength(10)]
        public string? Driver_Phone { get; set; }

        public int? Quantity { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal? Weight { get; set; }

        [MaxLength(50)]
        public string? Packaging_Type { get; set; }
        
        [MaxLength(50)]
        public string? Barcode { get; set; }

        // FK -> shipment
        public int Shipment_Id { get; set; }
        [ForeignKey("Shipment_Id")]
        public Shipment shipment { get; set; }

        // FK -> shipping_carrier
        public int Shipping_Carrier_Id { get; set; }
        [ForeignKey("Shipping_Carrier_Id")]
        public Shipping_Carrier Shipping_Carrier { get; set; }
    }
}
