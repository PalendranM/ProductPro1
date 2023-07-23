using ProductPro.Models.Dto;

namespace ProductPro.Data
{
    public static class ProductDb
    {
        public static List<ProductDto> ProductList = new List<ProductDto>()
            {
                new ProductDto { Id = 1, Name = "Mobile" },
                new ProductDto { Id = 2,Name = "Computer" },

            };



    }
}