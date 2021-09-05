using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Abstractions.Data;
using Data.Repository;
using Microsoft.EntityFrameworkCore.Metadata;


namespace Data.UnitOfWork
{
    public abstract class UnitOfWork : DbContext, IUnitOfWork
    {

        private Dictionary<Type, object> repositories;


        public UnitOfWork([NotNull] DbContextOptions dbContextOptions)
            : base(dbContextOptions)
        {
            ConfigureContext();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }


        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            if (repositories == null)
            {
                repositories = new Dictionary<Type, object>();
            }

            var type = typeof(TEntity);
            if (!repositories.ContainsKey(type))
            {

                    repositories[type] = new ConnectedRepository<TEntity>(this);
                
            }

            return (IRepository<TEntity>)repositories[type];
        }

        public int SaveChanges()
        {
            var timestamp = DateTime.UtcNow;
            BeforeSave(timestamp);
  
            return base.SaveChanges();
            
        }

        public async Task<int> SaveChangesAsync()
        {
            var timestamp = DateTime.UtcNow;
            BeforeSave(timestamp);

            return await base.SaveChangesAsync();
            
        }

        private void ConfigureContext()
        {

        }

        private void BeforeSave(DateTime timestamp)
        {
            ChangeTracker.DetectChanges();

        }


    }
}
