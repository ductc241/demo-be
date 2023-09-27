using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WEB_API.Model
{
    [Table("tracking", Schema = "dbo")]
    public class Tracking
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string? From_Location { get; set; }

        public string? To_Location { get; set;}

        public string? Note { get; set; }

        // FK -> shipment
        public int Shipment_Id { get; set; }
        [ForeignKey("Shipment_Id")]
        public Shipment Shipment { get; set; }


        // FK -> tracking_status
        public int Tracking_Status_Id { get; set; }
        [ForeignKey("Tracking_Status_Id")]
        public Tracking_Status Tracking_Status { get; set; }
    }
}
