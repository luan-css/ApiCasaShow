using System.Linq;
using eventos.Data;
using Microsoft.AspNetCore.Mvc;

namespace eventos.Controllers
{
    [Route("api/v1/user")]
    [ApiController]
    public class ApiUserController : ControllerBase
    {
        private readonly ApplicationDbContext database;

        public ApiUserController(ApplicationDbContext database)
        {
            this.database = database;
        }

        [HttpGet]
        public IActionResult getUser(){
            var user = database.Users.Select(p => p.UserName).ToList();
            return Ok (user);
        }
        [HttpGet("id")]
        public IActionResult getUser(string id){
            var user = database.Users.First(p => p.Id == id).UserName;
            return Ok (user);
        }
        [HttpGet("nome/{name}")]
        public IActionResult getName(string name){
            var user = database.Users.First(p => p.UserName == name);
            return Ok(user);
        }
    }
}