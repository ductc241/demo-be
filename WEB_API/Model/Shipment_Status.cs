using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WEB_API.Model
{
    [Table("shipment_status", Schema = "dbo")]
    public class Shipment_Status
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = String.Empty;

        [Required]
        public bool Status { get; set; }

        public ICollection<Shipment> Shipments { get; set; }
    }
}
