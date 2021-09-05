

using Data.UnitOfWork;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataContexts
{
    public class DirectoryDbContext: DbContext
    {
        public DirectoryDbContext()
        {
        }

        public DirectoryDbContext(DbContextOptions<DirectoryDbContext> options)
            : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<User> User { get; set; }
        public DbSet<ContactInfo> ContactInfo { get; set; }
        public DbSet<ContactType> ContactType { get; set; }


    }
}
