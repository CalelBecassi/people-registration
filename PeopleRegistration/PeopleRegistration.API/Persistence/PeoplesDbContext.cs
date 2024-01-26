using Microsoft.EntityFrameworkCore;
using PeopleRegistration.API.Entities;

namespace PeopleRegistration.API.Persistence
{
    public class PeoplesDbContext : DbContext
    {
        public PeoplesDbContext(DbContextOptions<PeoplesDbContext> options) : base(options)
        {
            
        }
        public DbSet<People> Peoples { get; set; }
        public DbSet<PeoplePhone> PeoplePhones { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<People>(e =>
            {
                e.HasKey(p => p.Id);

                e.Property(p => p.Name).IsRequired();

                e.Property(p => p.Cpf)
                    .HasMaxLength(11)
                    .IsRequired()
                    .HasColumnType("varchar(11)");

                e.Property(p => p.Nascimento)
                    .HasColumnName("Data_Nascimento");

                e.HasMany(p => p.Phones)
                    .WithOne()
                    .HasForeignKey(ph => ph.PeopleId);
            });

            builder.Entity<PeoplePhone>(e =>
            {
                e.HasKey(p => p.Id);
            });
        }
    }
}
