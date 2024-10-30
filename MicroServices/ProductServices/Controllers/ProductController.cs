using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using ProductServices.Services.ProductServices;
using System.Text.Json;
using System.Text;
using ProductServices.Models.Dtos;
using Microsoft.AspNetCore.Http.HttpResults;
using ProductServices.Models.Entities;
using App.Metrics;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProductServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductServices productServices;
        private readonly IDistributedCache distributedCache;
        private readonly IMetrics metrics; 

        public ProductController(IProductServices productServices, IDistributedCache distributedCache, IMetrics metrics)
        {
            this.productServices = productServices;
            this.distributedCache = distributedCache;
            this.metrics = metrics; 
        }
        // GET: api/<ProductController>
        //[Authorize(policy: "GetProductPolicy")]
        [HttpGet]
        [ProducesResponseType(typeof(GetAllProductsDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            string cacheKey = $"AllProduct";
            var cachedProduct = await distributedCache.GetStringAsync(cacheKey);

            //if(cachedProduct == null)
            //{
            //    var products = await productServices.GetAllProducts();
            //    var serilizedData = JsonSerializer.Serialize(products);

            //    var options = new DistributedCacheEntryOptions
            //    {
            //        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10),  
            //        SlidingExpiration = TimeSpan.FromMinutes(2)  
            //    };

            //    distributedCache.SetString(cacheKey, serilizedData,options);
            //    return Ok(products);
            //}
            //var data = distributedCache.GetStringAsync(cacheKey).Result;

            metrics.Measure.Counter.Increment(new App.Metrics.Counter.CounterOptions
            {
                Name = "Get_List_Product"
            });

            var data = await productServices.GetAllProducts();
            if (data == null)
            {
                return NotFound();
            }

            return Ok(data);
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProductDto), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get(Guid id)
        {
            var data = await productServices.GetProduct(id);
            if (data.Success)
                return Ok(data);
            return BadRequest(data);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ResultDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResultDto), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RempveProduct(Guid id)
        {
            var data = await productServices.RemoveProduct(id);
            if (data.Success)
                return Ok(data);
            return NotFound(data);
        }

        //[Authorize(Roles = "Admin")]
        // POST api/<ProductController>
        [HttpPost]
        [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProductDto), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post( [FromBody] ProductDto value)
        {
            var data = await productServices.AddNewProduct(value);
            if (data.Success)
                return Ok(data);
            return BadRequest(data);
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProductDto), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put([FromBody] UpdateProductDto product)
        {
            var data = await productServices.Updateproduct(product);
            if (data.Success)
                return Ok(data);
            return BadRequest(data);
        }


        [HttpGet("GetBrands")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProductDto), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetBrands()
        {
            var data = await productServices.GetBrands();
            if (data.Success)
                return Ok(data);
            return BadRequest(data);
        }

        [HttpGet("GetMostSellerProducts")]
        [ProducesResponseType(typeof(List<ProductDto>), 200)]
        public IActionResult GetMostSellerProducts()
        {


            var chachedData = distributedCache.GetAsync("MostSellerProducts").Result;
            //distributedCache.Remove("MostSellerProducts");
            if (chachedData == null)
            {

                var chachedProducts = distributedCache.GetAsync("Products").Result;

                if (chachedProducts != null)
                {
                    var products = JsonSerializer.Deserialize<List<ProductDto>>(chachedData);
                    var result = products.OrderBy(p => p.SellNumber).Take(10).ToList();
                    string JsonData = JsonSerializer.Serialize(result);
                    byte[] encodedJson = Encoding.UTF8.GetBytes(JsonData);
                    var options = new DistributedCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(3600));
                    distributedCache.SetAsync("MostSellerProducts", encodedJson, options);

                    return Ok(result);
                }

                else
                {
                    var mostSellerProducts = productServices.GetMostSellerProducts().Result;
                    string JsonData = JsonSerializer.Serialize(mostSellerProducts);
                    byte[] encodedJson = Encoding.UTF8.GetBytes(JsonData);
                    var options = new DistributedCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(3600));
                    distributedCache.SetAsync("MostSellerProducts", encodedJson, options);
                    return Ok(mostSellerProducts);
                }

            }
            else
            {
                var chachedProducts = distributedCache.GetAsync("MostSellerProducts").Result;

                var MostSellerproducts = JsonSerializer.Deserialize<List<ProductDto>>(chachedProducts);
                return Ok(MostSellerproducts);
            }
        }
    }
}
