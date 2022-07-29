using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace ListDePessoas.Model.Context
{
    public class ContextPessoa : DbContext
    {
        public DbSet<Pessoa> Perssoas { get; set; }

        public ContextPessoa(DbContextOptions<ContextPessoa> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            IConfiguration configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", false, true).Build();

            options.UseSqlServer(configuration.GetConnectionString("ServerConnection"));
        }
    }
}
