using CadastroPessoas.Models;
using Microsoft.EntityFrameworkCore;

namespace CadastroPessoas.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Pessoa> Pessoas { get; set; }
    }
}
