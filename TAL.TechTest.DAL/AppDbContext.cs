using Microsoft.EntityFrameworkCore;
using TAL.TechTest.DAL.Model;

namespace TAL.TechTest.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer(
                    @"data source=localhost;initial catalog=TALContext;trusted_connection=true;trustServerCertificate=True",
                    providerOptions => { providerOptions.EnableRetryOnFailure(); });
        }

        public DbSet<Appointment> Appointments => Set<Appointment>();
        public DbSet<Blockout> Blockouts => Set<Blockout>();
    }
}
