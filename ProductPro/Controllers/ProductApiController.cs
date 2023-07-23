using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductPro.Data;
using ProductPro.Models;
using ProductPro.Models.Dto;

namespace ProductPro.Controllers
{
    [Route("api/Product")]
    [ApiController]
    public class ProductApiController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<ProductDto> GetAllProducts()
        {

            return ProductDb.ProductList;
        }

        [HttpGet]
        public ProductDto GetProduct(int id)
        {

            return ProductDb.ProductList.FirstOrDefault(p => p.Id == id);

        }



    }
}