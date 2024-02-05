using Filmotech.Entities;
using Microsoft.EntityFrameworkCore;
namespace Filmotech
{
    public class Database : DbContext
    {
        public DbSet<Film> Films { get; set; } = null!;

        public Database(DbContextOptions options) : base(options)
        {

        }
    }
}