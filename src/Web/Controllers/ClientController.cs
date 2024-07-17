using Application.Interfaces;
using Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize("Client")]
    public class ClientController : ControllerBase
    {
        private readonly ICartService _cartService;

        public ClientController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpPost("[action]/{productId}")]
        
        public async Task<ActionResult> AddProductToCart([FromRoute] int productId)
        {
            int clientId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "");
            try
            {
                
                await _cartService.AddProductToCart(clientId, productId);
                return Ok("Product added to cart successfully.");
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("[action]")]
        
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetCartProducts()
        {
            int clientId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "");
            try
            {
                
                var products = await _cartService.GetCartProducts(clientId);
                return Ok(products);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("[action] / {productId}")]
        public async Task<ActionResult> RemoveProductFromCart( int productId)
        {
            int clientId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "");
            try
            {
                await _cartService.RemoveProductFromCart(clientId, productId);
                return NoContent(); 
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> PurchaseCart()
        {
            int clientId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "");
            try
            {
                await _cartService.PurchaseCart(clientId);
                return Ok("Purchase successful.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
