using Microsoft.IdentityModel.Tokens;
using Role_Base_Authentication_JWT.Context;
using Role_Base_Authentication_JWT.Interface;
using Role_Base_Authentication_JWT.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Role_Base_Authentication_JWT.Service
{
    public class AuthService : IAuthService
    {
        private readonly JwtContext _Context;
        private readonly IConfiguration _configuration;
        public AuthService(JwtContext jwtContext,IConfiguration configuration)
        {
            _Context = jwtContext;
            _configuration = configuration;
        }

        public Role AddRole(Role role)
        {
            var addrole = _Context.Roles.Add(role);
            _Context.SaveChanges();
            return addrole.Entity;
        }

        public User AddUser(User user)
        {
            var adduser = _Context.Users.Add(user);
            _Context.SaveChanges();
            return adduser.Entity;
        }

        public bool AssignRoleToUser(AddUserRole obj)
        {
            try
            {


                var addRoles = new List<UserRole>();
                var user = _Context.Users.SingleOrDefault(s => s.Id == obj.UserId);
                if (user == null)
                {
                    throw new Exception("User is not valid");
                }
                foreach (var role in obj.RoleIds)
                {
                    var userRole = new UserRole();
                    userRole.RoleId = role;
                    userRole.UserId = user.Id;
                    addRoles.Add(userRole);
                }
                _Context.UserRoles.AddRange(addRoles);
                _Context.SaveChanges();
                return true;
                
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public string Login(LoginRequest loginRequest)
        {
            if (loginRequest.Username != null && loginRequest.Password != null)
            {
                var user = _Context.Users.SingleOrDefault(s => s.UserName == loginRequest.Username && s.Password == loginRequest.Password);
                if(user!= null)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(JwtRegisteredClaimNames.Sub,_configuration["Jwt:Subject"]),
                        new Claim("Id",user.Id.ToString()),
                        new Claim("UserName",user.Name),

                    };
                    var userRoles = _Context.UserRoles.Where(u => u.UserId==user.Id).ToList();
                    var roleIds = userRoles.Select(s => s.RoleId).ToList();
                    var roles = _Context.Roles.Where(r => roleIds.Contains(r.Id)).ToList();
                    foreach(var role in roles)
                    {
                        claims.Add(new Claim("Role", role.Name));
                    }
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        _configuration["Jwt:Issuer"],
                        _configuration["Jwt:Audience"],
                        claims,
                       expires: DateTime.UtcNow.AddMinutes(10),
                        signingCredentials:signIn);

                    var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
                    return jwtToken;
                   
                }
                else
                {
                    throw new Exception("User is not valid");
                }
            }
            else
            {
                throw new Exception("Credentials are not valid");
            }
        }
    }
}
