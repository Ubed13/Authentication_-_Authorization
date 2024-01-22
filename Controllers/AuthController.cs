using Microsoft.AspNetCore.Mvc;
using Role_Base_Authentication_JWT.Interface;
using Role_Base_Authentication_JWT.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Role_Base_Authentication_JWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _auth;
        public AuthController(IAuthService authService)
        {
            _auth = authService;
        }
        [HttpPost("Login")]
        public string Login([FromBody]LoginRequest obj)
        {
            var token = _auth.Login(obj);
            return token;
        }

        [HttpPost("assignRole")]
        public bool AssignRoleToUser([FromBody] AddUserRole userRole)
        {
            var addedUserRole = _auth.AssignRoleToUser(userRole);
            return addedUserRole;
        }

        [HttpPost("addUser")]
        public User AddUser([FromBody] User user)
        {
            var adduser = _auth.AddUser(user);
            return adduser;
        }

        // PUT api/<AuthController>/5
        [HttpPost("addRole")]
        public Role AddRole( [FromBody] Role role)
        {
            var addrole = _auth.AddRole(role);
            return addrole;
        }

        
    }
}
