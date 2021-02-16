using Microsoft.EntityFrameworkCore;

namespace JewelAPI.Models
{
    public class JeweleryDBContext : DbContext
    {
        public JeweleryDBContext(DbContextOptions<JeweleryDBContext> options) : base(options)
        {
            LoadUsers();
        }

        public DbSet<User> Users { get; set; }

        public void LoadUsers()
        {
            User user = new User()
            {
                Id = 1,
                UserName = "PrivelegedUser",
                Password = "test1234",
                IsPrivilegedUser = true
            };
            Users.Add(user);
            user = new User()
            {
                Id = 2,
                UserName = "RegularUser",
                Password = "test1234",
                IsPrivilegedUser = false
            };
            Users.Add(user);
        }

    }
}
