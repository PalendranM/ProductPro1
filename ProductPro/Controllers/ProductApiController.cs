using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductPro.logging;
using productPro.Repository;
using ProductPro.Models.Dto;
using ProductPro.Models;
using System.Net;
using Microsoft.AspNetCore.Authorization;

namespace ProductPro.Controllers
{
    [Route("api/V{version:apiVersion}/Product")]
    [ApiController]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    public class ProductApiController : ControllerBase
    {
        // private readonly ILogging Logger;
        // private readonly ProductDbContext db;
        private readonly IMapper mapper;
        private readonly IProductRepository repo;
        private APIRespons _respons;


        //public ProductApiController(ILogger<ProductApiController> _logger) 
        //{
        //    Logger = _logger;
        //}
        public ProductApiController(IProductRepository _repo, IMapper _mapper)
        {
            //  Logger = _logging;
            //  db = _Db;
            repo = _repo;
            mapper = _mapper;
            _respons = new();
        }
        [HttpGet]
        [MapToApiVersion("1.0")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<IEnumerable<APIRespons>>> GetAllProducts()
        {
            try
            {
                IEnumerable<Product> productList = await repo.GetAllAsync();
                //Logger.Log("GetAllProducts","");
                _respons.Resutl = mapper.Map<List<ProductDto>>(productList);
                _respons.StatusCode = HttpStatusCode.OK;
                return Ok(_respons);
            }
            catch(Exception ex)
            {
                _respons.IsSuccess = false;
               _respons.ErrorMessages = new List<String>(){ ex.ToString() };
                return BadRequest( _respons);

            }
            
        }

        [HttpGet]
        [MapToApiVersion("2.0")]
        public IEnumerable<String> GetAll()
        {
            return new string[] {"Sananthan","Bala","Vakshala"};
        }


        [HttpGet("{id:int}", Name = "GetProduct")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(200)]
        public async Task< ActionResult<APIRespons>> GetProduct(int id)
        {
            if (id == 0)
            {
                //   Logger.Log("GetProduct" + id, "error");
                return BadRequest();
            }
            var Product = await repo.GetAsync(p => p.Id == id);
            if (Product == null)
            {
                return NotFound();
            }
            _respons.Resutl = mapper.Map<ProductDto>(Product);
            _respons.StatusCode = HttpStatusCode.OK;
            return Ok(_respons);


        }

        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<APIRespons>> CreateProduct([FromBody] ProductCreateDto productDto)
        {

            if (productDto == null)
            {
                return BadRequest();
            }

            if ((await repo.GetAsync(p => p.Name == productDto.Name)) != null)
            {
                ModelState.AddModelError("CustomError", "Product already exist");
                return BadRequest(ModelState);
            }
            //if (productDto.Id > 0) 
            //{
            //    return StatusCode(StatusCodes.Status500InternalServerError);

            //}

            //int maxId = db.Products.OrderByDescending(p => p.Id).FirstOrDefault().Id;
            //productDto.Id = maxId+1;
            //Product model = new Product()
            //{
            //    Name = productDto.Name
            //};
            Product model = mapper.Map<Product>(productDto);
            await repo.CreateAsync(model);

            //var productdto = new ProductDto 
            //{
            //    Id=model.Id, 
            //    Name=model.Name 
            //};
            var productdto = mapper.Map<ProductDto>(model);
            _respons.Resutl = productdto;
            _respons.StatusCode = HttpStatusCode.Created;
            return CreatedAtRoute("GetProduct", new { id = productdto.Id }, _respons);

        }
        [HttpDelete("{id:int}", Name = "DeleteProduct")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var deleteproduct = await repo.GetAsync(p => p.Id == id);
            if (deleteproduct == null)
            {
                return NotFound();
            }

            await repo.RemoveAsync(deleteproduct);

            _respons.StatusCode = HttpStatusCode.NoContent;
            _respons.IsSuccess = true;
            return Ok(_respons);


        }
        [HttpPut("{id:int}", Name = "UpdateProduct")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductUpdateDto productDto)
        {
            if (id == 0 || productDto == null)
            {
                return BadRequest();
            }

            var existingProduct = await repo.GetAsync(p => p.Id == id);
            if (existingProduct == null)
            {
                return NotFound();
            }

            if ((await repo.GetAsync(p => p.Id != id && p.Name == productDto.Name)) != null)

            {
                ModelState.AddModelError("CustomError", "Product name already exists for another product.");
                return BadRequest(ModelState);
            }
            //Product modal = new Product()
            //{
            //    Id = existingProduct.Id,
            //    Name = productDto.Name,
            //    Detail = existingProduct.Detail,
            //    Qty = existingProduct.Qty
            //};
            //Product modal = mapper.Map<Product>(existingProduct);
            existingProduct.Name = productDto.Name;
            // existingProduct.Name = updatedProductDto.Name;
           await repo.UpdateAsync(existingProduct);

            // You can update other properties as needed.
            _respons.StatusCode = HttpStatusCode.NoContent;
            _respons.IsSuccess=true;
            return Ok(_respons);

        }



    }
}