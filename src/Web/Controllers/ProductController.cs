using Application.Interfaces;
using Application.Models;
using Application.Models.Requests;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost("[action]")]
        [Authorize("Admin&Seller")]
        public async Task<ActionResult<Product>> CreateProduct([FromBody] ProductRequest productRequest)
        {
            int sellerId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "");
            try
            {
                
                var product = await _productService.CreateProduct(productRequest, sellerId);
                return Ok(product);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al crear el producto: {ex.Message}");
            }
        }


        [HttpPut("[action]/{id}")]
        [Authorize("Admin&Seller")]
        public async Task<ActionResult> UpdateProduct(int id, [FromBody] ProductRequest productRequest)
        {
            int sellerId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "");


            try
            {
                var product = await _productService.GetProductEntityById(id);
                if (product.SellerId != sellerId)
                {
                    return Forbid("You do not have permission to update this product");
                }

                await _productService.UpdateProduct(id, productRequest);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("[action]/{id}")]
        [Authorize("Admin&Seller")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            int sellerId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "");

            try
            {
                var product = await _productService.GetProductEntityById(id);
                if (product.SellerId != sellerId)
                {
                    return StatusCode(403, new { message = "You do not have permission to delete this product" });
                }

                await _productService.DeleteProduct(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }



        [HttpGet("[action]")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllProducts()
        {
            var products = await _productService.GetAllProducts();
            return Ok(products);
        }

        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<ProductDto>> GetProductById(int id)
        {
            var product = await _productService.GetProductById(id);
            return Ok(product);
        }

        // Falta el buscador por nombre...
    }
}
