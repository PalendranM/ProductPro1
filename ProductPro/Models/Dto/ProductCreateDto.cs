using System.ComponentModel.DataAnnotations;

namespace ProductPro.Models.Dto
{
    public class ProductCreateDto
    {
     
        [Required]
        [MaxLength(13)]
        public string Name { get; set; }
    }
}
