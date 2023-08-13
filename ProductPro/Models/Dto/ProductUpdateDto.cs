using System.ComponentModel.DataAnnotations;

namespace ProductPro.Models.Dto
{
    public class ProductUpdateDto
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(13)]
        public string Name { get; set; }
    }
}
 