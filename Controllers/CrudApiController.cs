using Microsoft.AspNetCore.Mvc;
using VkCrud2.Models;
using VkCrud2.Data;
using VkCrud2.Services;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace VkCrud2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("text/json")]
    public class CrudApiController : ControllerBase
    {
        

        private readonly ILogger<CrudApiController> _logger;

        public CrudApiController(ILogger<CrudApiController> logger)
        {
            _logger = logger;
        }

        [HttpGet("user",Name = "GetUser")]
        public async Task<IActionResult> GetUser(string userJson)
        {   
            Models.Response response =  await UserManager.GetUser(userJson);
            if (response.Status != 200)
            {
                return StatusCode(response.Status);
            }
            else
                return Ok(response.Content);
        }

        [HttpGet("users", Name = "GetUsers")]
        
        public async Task<IActionResult> GetUsers(int page = 0, int count = 30)
        {
            Models.Response response = await UserManager.GetUsers(page, count);
            if (response.Status != 200)
            {
                return StatusCode(response.Status);
            }
            else
                return Ok(response.Content);
        }

        [HttpPost("add_user", Name = "AddUser")]
        public async Task<IActionResult> AddUser(string userJson)
        {
            Models.Response response = await UserManager.AddUser(userJson);
            if (response.Status != 200)
            {
                return StatusCode(response.Status);
            }
            else
                return Ok(response.Content);
        }

        [HttpDelete("delete_user", Name = "DeleteUser")]
        public async Task<IActionResult> Delete(string userJson)
        {
            Models.Response response = await UserManager.DeleteUser(userJson);
            if (response.Status != 200)
            {
                return StatusCode(response.Status);
            }
            else
                return Ok(response.Content);
        }

    }
}