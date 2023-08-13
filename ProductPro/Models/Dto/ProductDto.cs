using System.ComponentModel.DataAnnotations;

namespace ProductPro.Models.Dto
{
    public class ProductDto
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(13)]
        public string Name { get; set; }
    }
}