using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WEB_API.Model
{
    [Table("order_item", Schema = "dbo")]
    public class Order_Item
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int Quantity { get; set; }

        public int Price { get; set; }

        public int Amount { get; set; }


        // FK -> Product
        public int Product_Id { get; set; }
        [ForeignKey("Product_Id")]
        public Product Product { get; set; }

        // FK -> Order
        public int Order_Id { get; set; }
        [ForeignKey("Order_Id")]
        public Order Order { get; set; }
    }
}
