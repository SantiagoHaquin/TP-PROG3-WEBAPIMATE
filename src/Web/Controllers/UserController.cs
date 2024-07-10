using Application.Interfaces;
using Application.Models;
using Application.Models.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize("Admin")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _sysAdminService;

        public UserController(IUserService sysAdminService)
        {
            _sysAdminService = sysAdminService;
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> CreateUser([FromBody] UserRequest user)
        {
            try
            {
                await _sysAdminService.CreateUser(user);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateUser([FromRoute] int id, [FromBody] UserRequest user)
        {
            try
            {
                await _sysAdminService.UpdateUser(id, user);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser([FromRoute] int id)
        {
            try
            {
                await _sysAdminService.DeleteUser(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUserById(int id)
        {
            try
            {
                var user = await _sysAdminService.GetUserById(id);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsersAll()
        {
            var userAll = await _sysAdminService.GetAllUsers();
            return Ok(userAll);
        }
    }
}
