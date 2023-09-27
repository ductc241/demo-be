using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WEB_API.Model
{
    [Table("products", Schema = "dbo")]
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(255)]
        public string Name { get; set; } = String.Empty;

        public int Price { get; set; }

        public int Quantity { get; set; }

        public ICollection<Order_Item> Order_Item { get; set; }
    }
}
