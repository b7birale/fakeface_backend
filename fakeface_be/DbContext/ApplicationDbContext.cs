using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FakeFace_BE.DbContext
{
    public class ApplicationDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; } // táblák
    }

    public class User
    {
        [Key]
        public int User_Id { get; set; }
    } // Ezt te külön osztályba rakd légyszi : )
}
