using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UdemyIdentityServer.API1.Models;

namespace UdemyIdentityServer.API1.Controllers
{
    [Route("api/[controller]/[action]")]  //metod ismi(action) üzerinden endpointlere ulaşılacak
    [ApiController]
    public class ProductsController : ControllerBase
    {
        // api/products/getproducts
        [Authorize(Policy ="ReadProduct")]
        [HttpGet]
        public IActionResult GetProducts()
        {
            var productList = new List<Product>()
            {
                new Product{ID = 1, Name = "Kalem", Price = 10, Stock = 450},
                new Product{ID = 2, Name = "Silgi", Price = 5, Stock = 560},
                new Product{ID = 3, Name = "Defter", Price = 25, Stock = 600},
                new Product{ID = 4, Name = "Kitap", Price = 40, Stock = 546},
                new Product{ID = 5, Name = "Dosya", Price = 50, Stock = 243}
            };
            return Ok(productList);
        }

        [Authorize(Policy = "UpdateOrCreate")]
        //[HttpPut]
        public IActionResult UpdateProduct(int id)
        {
            return Ok($"id'si {id} olan ürün güncellenmiştir");
        }

        [Authorize(Policy ="UpdateOrCreate")]
        //[HttpPost]
        public IActionResult CreateProduct(Product product)
        {
            return Ok(product);
        }
    }
}
