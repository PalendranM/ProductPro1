using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProductPro.Models.Dto
{
    public class ProductType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Detail { get; set; }
        public int Qty { get; set; }   
        //public int ProductId { get; set; }
       // public virtual ICollection<Product> Products { get; set; }=new List<Product>();
    }
}
