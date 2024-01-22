using Microsoft.EntityFrameworkCore;
using Role_Base_Authentication_JWT.Models;

namespace Role_Base_Authentication_JWT.Context
{
    public class JwtContext :DbContext
    {
        public JwtContext(DbContextOptions<JwtContext> options):base(options) 
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole>  UserRoles { get; set; }
        public DbSet<Employee>  Employees { get; set; }

    }
}
