using Application.Interfaces;
using Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly ICartService _cartService;

        public ClientController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpPost("[action]")]
        [Authorize("Client")]
        public async Task<ActionResult> AddProductToCart([FromBody] int productId)
        {
            try
            {
                var clientId = int.Parse(User.Claims.First(c => c.Type == "sub").Value);
                await _cartService.AddProductToCart(clientId, productId);
                return Ok("Product added to cart successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("[action]")]
        [Authorize("Client")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetCartProducts()
        {
            try
            {
                var clientId = int.Parse(User.Claims.First(c => c.Type == "sub").Value);
                var products = await _cartService.GetCartProducts(clientId);
                return Ok(products);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
