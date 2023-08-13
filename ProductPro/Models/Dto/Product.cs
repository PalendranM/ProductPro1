using ProductPro.Models.Dto;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductPro.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Detail { get; set; } = null;
        public Nullable<int> Qty { get; set; } = 0;

        //public virtual ProductType ProductType { get; set; }=null;
    }
}