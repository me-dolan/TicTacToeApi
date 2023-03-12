using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TicTacToeApi.Data.Model;

namespace TicTacToeApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        private readonly IConfiguration _config;
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration config) 
            : base(options)
        {
            _config = config;
        }

        public DbSet<Game> Games { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_config.GetConnectionString("DefaultConnection"));
        }
    }
}
