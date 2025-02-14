using Microsoft.EntityFrameworkCore;
using WebApi8_AutorLivro.Models;

namespace WebApi8_AutorLivro.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {            
        }

        public DbSet<AutorModel> Autores { get; set; }
        public DbSet<LivroModel> Livros { get; set; }    

    }
}
