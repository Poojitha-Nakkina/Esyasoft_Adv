using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using ProductCatalogApi.Models;
using ProductCatalogAPI.Data;

namespace ProductCatalogApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        [HttpGet]
        [Route("GetAllProducts")]

        public ActionResult<IEnumerable<ProductDTO>> GetAllProducts()
        {
            var products = ProductRepository.Products.Select(p => new Models.ProductDTO
            {
                ProductID = p.ProductID,
                Name = p.Name,
                Category = p.Category,
                Price = p.Price,
                StockQuantity = p.StockQuantity

            }).ToList();
            return Ok(products);
        }


        [HttpGet("id/{id}", Name = "GetStudentById")]

        public ActionResult<ProductDTO> GetStudentById(int id)
        {
            if(id <= 0)
            {
                return BadRequest();
            }

            var product = ProductRepository.Products.Where(p => p.ProductID == id).FirstOrDefault();
            if (product == null)
            {
                return NotFound("No Products found");
            }

            return Ok(product);
        }

        [HttpPost("CreateNewProduct")]
        public ActionResult <ProductDTO> CreateNewProduct([FromBody] ProductDTO model)
        {
            if(model == null)
            {
                return BadRequest();
            }
            var newid = ProductRepository.Products.LastOrDefault().ProductID + 1;
            var newProduct = new ProductDTO
            {
                ProductID = newid,
                Name = model.Name,
                Category = model.Category,
                Price = model.Price,
                StockQuantity = model.StockQuantity
            };
            ProductRepository.Products.Add(newProduct);
            return Ok(model);

        }

        [HttpPut("Update")]

        public ActionResult UpdateProduct(int id, [FromBody] ProductDTO model)
        {
            if(model == null)
            {
                return BadRequest();
            }
            var newProduct = ProductRepository.Products.Where(p => p.ProductID == id).FirstOrDefault();
            if (newProduct == null)
            {
                return NotFound("No product found");
            }
            newProduct.Name = model.Name;
            newProduct.Category = model.Category;
            newProduct.Price = model.Price;
            newProduct.StockQuantity = model.StockQuantity;
            return NoContent();
        }

        [HttpPatch("UpdatePartial")]
        public ActionResult<ProductDTO> updatepartialproduct(int id, [FromBody] JsonPatchDocument<ProductDTO> Model)
        {
            if (id == 0 || Model == null)
            {
                return BadRequest();
            }
            var product = ProductRepository.Products.Where(p => p.ProductID == id).FirstOrDefault();
            if (product == null)
            {
                return NotFound($"No products were found with the given id {id}");
            }
            var newProduct = new ProductDTO
            {
                ProductID = product.ProductID,
                Name = product.Name,
                Category = product.Category,
                Price = product.Price,
                StockQuantity = product.StockQuantity
            };
            Model.ApplyTo(newProduct, ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            product.Name = newProduct.Name;
            product.Price = newProduct.Price;
            product.Category = newProduct.Category;
            product.StockQuantity = newProduct.StockQuantity;
            return Ok(product);
        }



    }
}