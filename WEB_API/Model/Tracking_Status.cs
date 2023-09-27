using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WEB_API.Model
{
    [Table("tracking_status", Schema = "dbo")]
    public class Tracking_Status
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(50)]
        public string Name { get; set; } = String.Empty;

        public bool Status { get; set; }

        public ICollection<Tracking> Trackings { get; set; }
    }
}
