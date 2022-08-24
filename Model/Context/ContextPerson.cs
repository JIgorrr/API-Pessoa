using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace APIPerson.Model.Context
{
    public class ContextPerson : DbContext
    {
        public DbSet<Person> Persons { get; set; }

        public ContextPerson(DbContextOptions<ContextPerson> options) : base(options) {}

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            IConfiguration configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", false, true).Build();

            options.UseSqlServer(configuration.GetConnectionString("ServerConnection"));
        }
    }
}
