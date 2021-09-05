using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using Abstractions.Data;
using Data.UnitOfWork;

namespace Data.Configuration
{
    public static class DataServiceCollectionExtensions
    {


        /// <summary>
        /// UnitOfWork servisi ve bileşenlerini eklemek için kullanılır.
        /// </summary>
        /// <typeparam name="TContext"></typeparam>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddUnitOfWork<TContext>(this IServiceCollection services, Action<DbContextOptionsBuilder> optionsAction = null) where TContext : UnitOfWork.UnitOfWork
        {
            services.AddInternalUnitOfWorkServices();
            if (optionsAction != null)
            {
                services.AddDbContextOptions<TContext>((p, b) => optionsAction(b));
            }
            services.AddScoped<IUnitOfWork, TContext>();
            return services;
        }

        /// <summary>
        /// UnitOfWork servisi ve bileşenlerini eklemek için kullanılır.
        /// </summary>
        /// <typeparam name="TContext"></typeparam>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddAdditionalUnitOfWork<TContext>(this IServiceCollection services, Action<DbContextOptionsBuilder> optionsAction = null) where TContext : UnitOfWork.UnitOfWork
        {
            if (optionsAction != null)
            {
                services.AddDbContextOptions<TContext>((p, b) => optionsAction(b));
            }
            services.AddScoped<TContext, TContext>();
            services.AddScoped<IUnitOfWork<TContext>, UnitOfWork<TContext>>();
            return services;
        }

        private static IServiceCollection AddInternalUnitOfWorkServices(this IServiceCollection services)
        {
            return services;
        }

        private static void AddDbContextOptions<TContext>(this IServiceCollection services, Action<IServiceProvider, DbContextOptionsBuilder> optionsAction) where TContext : UnitOfWork.UnitOfWork
        {
            services.TryAdd(
                new ServiceDescriptor(
                    typeof(DbContextOptions<TContext>),
                    p => CreateDbContextOptions<TContext>(p, optionsAction),
                    ServiceLifetime.Scoped));

            services.Add(
                new ServiceDescriptor(
                    typeof(DbContextOptions),
                    p => p.GetRequiredService<DbContextOptions<TContext>>(),
                    ServiceLifetime.Scoped));
        }

        private static void AddAdditionalDbContextOptions<TContext>(this IServiceCollection services, Action<IServiceProvider, DbContextOptionsBuilder> optionsAction) where TContext : UnitOfWork.UnitOfWork
        {
            services.Add(
                new ServiceDescriptor(
                    typeof(DbContextOptions),
                    p => p.GetRequiredService<DbContextOptions<TContext>>(),
                    ServiceLifetime.Scoped));
        }

        private static DbContextOptions<TContext> CreateDbContextOptions<TContext>(
            IServiceProvider applicationServiceProvider,
            Action<IServiceProvider, DbContextOptionsBuilder> optionsAction)
            where TContext : UnitOfWork.UnitOfWork
        {
            var builder = new DbContextOptionsBuilder<TContext>(
                new DbContextOptions<TContext>(new Dictionary<Type, IDbContextOptionsExtension>()));

            builder.UseApplicationServiceProvider(applicationServiceProvider);

            optionsAction?.Invoke(applicationServiceProvider, builder);

            return builder.Options;
        }
    }
}
