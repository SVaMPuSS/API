using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.DataBase
{
    public class ApiContext:DbContext
    {

        public ApiContext(DbContextOptions<ApiContext> context):base(context)
        {

        }
       public DbSet<User> Users { get; set; } 
    }
}
