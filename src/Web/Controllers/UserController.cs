using Application.Interfaces;
using Application.Models;
using Application.Models.Requests;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize("Admin")] //politca 
    public class UserController : ControllerBase
    {
        private readonly IUserService _sysAdminService;
        public UserController(IUserService sysAdminService) 
        { 
            _sysAdminService = sysAdminService;
        }
       
        [HttpPost("[action]")]
        public ActionResult CreateUser([FromBody] UserRequest user)
        {
            try 
            {
                 _sysAdminService.CreateUser(user);
               return NoContent();
            }catch (Exception ex) 
            {
               return NotFound(ex.Message);
            }
        
        }

        [HttpPut("{id}")]
        public ActionResult UpadateUser([FromRoute] int id, [FromBody] UserRequest user)
        {
            try
            {
                  _sysAdminService.UpdateUser(id , user);
                return NoContent();
            }catch (Exception ex) 
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteUser([FromRoute] int id)
        {
            try
            {
                 _sysAdminService.DeleteUser(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<UserDto> GetUserById(int id)
        {

            var user =  _sysAdminService.GetUserById(id);
            return Ok(user);
        }

        [HttpGet("[action]")]
        public ActionResult<IEnumerable<UserDto>> GetUsersAll() 
        {
            var userAll =  _sysAdminService.GetAllUsers();
            return Ok(userAll);
        }
    }
}
