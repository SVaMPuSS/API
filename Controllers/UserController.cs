using API.DataBase;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {

        ApiContext context;
        public UserController(ApiContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Registration new user
        /// </summary>
        /// <param name="id">Here is the description for ID.</param>
        /// <param name="Login">User login</param>
        /// <param name="Password">User password</param>
        /// <response code="200">Success create user</response>
        /// <response code="400">Bad parameter value</response>
        [HttpPost("/Registration")]
        public ActionResult reg([FromBody][Required] User user)
        {
            context.Users.Add(user);
            context.SaveChanges();
            return Ok("Success create user");
        }

        /// <summary>
        /// Authorization user
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <response code="200">Success login</response>
        /// <response code="400">Not found user</response>
        /// <returns >token</returns>
        [HttpPost("/Authorization")]
        [ProducesResponseType(typeof(int), 200)]
        public ActionResult auth([FromBody][Required] User user)
        {
            var obj = context.Users.FirstOrDefault(q => q.password == user.password && q.login == user.login);
            if (obj == null)
                return BadRequest("Not found user");
            obj.token = new Random().Next(100000, 999999);
            context.Users.Update(obj);
            context.SaveChanges();
            return Ok(obj.token);
        }

        /// <summary>
        /// Delete user by id
        /// </summary>
        /// <param name="id">User id</param>
        /// <response code="200">Success delete user</response>
        /// <response code="400">User not found</response>
        [HttpDelete("/Delete/{id}")]
        public ActionResult delete(int id)
        {
            var obj = context.Users.FirstOrDefault(q => q.id == id);
            if (obj == null)
                return NotFound("User not found");
            context.Users.Remove(obj);
            context.SaveChanges();
            return Ok("Success delete user");
        }

        /// <summary>
        /// Get user by id
        /// </summary>
        /// <param name="id">User id</param>
        /// <response code="200">Success found user</response>
        /// <response code="400">Not found user</response>
        [HttpGet("/Get/{id}")]
        public ActionResult getUser(int id)
        {
            var obj = context.Users.FirstOrDefault(q=>q.id == id);
            if (obj == null)
                return BadRequest("User not found");
            return Ok(obj);
        }
        /// <summary>
        /// Patch user
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <response code="200">Success patch user</response>
        /// <response code="400">User not found</response>
        [HttpPatch("/Patch/{id}")]
        public ActionResult update([Required] string login,[Required] string password, [Required]int id)
        {
            var obj = context.Users.FirstOrDefault(q => q.id == id);
            if(obj == null)
                return BadRequest("User not found");
            obj.login = login;
            obj.password = password;
            context.Update(obj);
            context.SaveChanges();
            return Ok("Success update");
        }
    }
}
