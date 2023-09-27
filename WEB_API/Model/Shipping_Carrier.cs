using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WEB_API.Model
{
    [Table("shipping_carrier", Schema = "dbo")]
    public class Shipping_Carrier
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(50)]
        public string? Contact_Person { get; set; }

        [MaxLength(13)]
        public string? Phone_Number { get; set; }

        [MaxLength(50)]
        public string? Email { get; set; }

        [MaxLength(255)]
        public string? Note { get; set; }

        public bool Status { get; set; }

        public ICollection<Shipment_Detail> Shipment_Detail { get; set; }
    }
}
