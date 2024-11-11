using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Services;
using Entities;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Store.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        //UserService userservice = new();
        IUserService _iUserService;

        public UsersController(IUserService iUserService)
        {
            _iUserService = iUserService;
        }

        // GET: api/<UsersController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "how", "are you" };
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UsersController>0w
        [HttpPost]
        [Route("login")]
        public ActionResult<User> PostLogin([FromQuery] string username,string password)
        {
            //where we will put the ask of the null?
            User user = _iUserService.PostLoginS(username, password);
            if (user != null)
                return Ok(user);
            return NoContent();
        }

        [HttpPost]
        public ActionResult<User> PostNewUser([FromBody] User user)
        {
            int result = _iUserService.CheckPassword(user.Password);
            if (result <= 3)
                return NotFound(result);
            User newUser = _iUserService.Post(user);
            if (newUser != null)
                return Ok(newUser);
            return NoContent();
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] User userFromClient)
        {
            _iUserService.Put(id, userFromClient);
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {

        }
        [HttpPost]
        [Route("password")]
        public int PostCheckScore([FromBody] string password)
        {
            return _iUserService.CheckPassword(password);
        }
    }
}
