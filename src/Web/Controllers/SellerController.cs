using Application.Interfaces;
using Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize("Seller")]
    public class SellerController : ControllerBase
    {
        private readonly ISellerService _sellerService;

        public SellerController(ISellerService sellerService)
        {
            _sellerService = sellerService;
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetSellerProducts()
        {
            int sellerId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "");
            try
            {
                var products = await _sellerService.GetSellerProducts(sellerId);
                return Ok(products);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

        }

        [HttpGet("[action]")]
        public async Task<ActionResult> GetSellerSales()
        {
            int sellerId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "");
            try
            {
                var sales = await _sellerService.GetSellerSales(sellerId);
                return Ok(sales);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}