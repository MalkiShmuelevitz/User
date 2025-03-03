using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Services;
using Entities;
using AutoMapper;
using DTO;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Store.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        IUserService _iuserservice;
        IMapper _imapper;

        public UsersController(IUserService iuserservice, IMapper _imapper,ILogger<UsersController> logger)
        {
            _logger = logger;
            this._imapper = _imapper;
            _iuserservice = iuserservice;
        }


        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetUserDTO>> GetById(int id)
        {
            User user = await _iuserservice.GetById(id);
            GetUserDTO userDTO = _imapper.Map<User, GetUserDTO>(user);
            if (userDTO != null)
                return Ok(userDTO);
            return NoContent();
        }

        // POST api/<UsersController>0w
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<GetUserDTO>> PostLogin([FromQuery] string username, string password)
        {
            //check it:    [FromQuery] string username,string password
            //where we will put the ask of the null?

            User user = await _iuserservice.PostLoginS(username, password);
            GetUserDTO userDTO = _imapper.Map<User, GetUserDTO>(user);
            if (userDTO != null)
            {
                _logger.LogCritical($"Login with username - {username} and password - {password}");
                return Ok(userDTO);
            }
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<User>> PostNewUser([FromBody] UserDTO user)
        {
            User user1 = _imapper.Map<UserDTO, User>(user);
            User newUser =  await _iuserservice.Post(user1);
            if(newUser == null)
                return NotFound();
            UserDTO newUser1 = _imapper.Map<User,UserDTO>(newUser) ;
            if (newUser1 != null)
                return Ok(newUser1);
            //return CreatedAtAction(nameof(GetById), new { UserName = newUser1.UserName }, newUser1);
            return NoContent();

        }

        [HttpPost]
        [Route("password")]
        public int PostOnChange([FromBody] string password)
        {
            int result = _iuserservice.CheckPassword(password);
            return result;
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] UserDTO user)
        {
            User user1 = _imapper.Map<UserDTO, User>(user);
            await _iuserservice.Put(id, user1);
        }

        

     
    }
}
