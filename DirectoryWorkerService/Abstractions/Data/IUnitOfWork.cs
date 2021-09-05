using System;
using System.Threading.Tasks;

namespace Abstractions.Data
{
    /// <summary>
    /// Veri tabanı işlemlerinin tek seferde yapılabilmesini olanak sağlar.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Belirli bir tipteki entity için repository üretir.<seealso cref="IRepository{TEntity}"/>
        /// </summary>
        /// <typeparam name="TEntity">Repositorysi istenilen entity</typeparam>
        /// <returns>Belirtilen tipteki entity için repository</returns>
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
        /// <summary>
        /// Değişikliklerin kaydedilmesi için kullanılır.
        /// </summary>
        /// <param name="auditBehaviour"><seealso cref="Titra.Abstractions.Data.AuditLog.AuditBehaviour"/>Değişikliklerin kaydı sırasında audit loglarının oluşup oluşmamasını belirtmek
        /// için kullanılır.</param>
        /// <returns>Etkilenen kayıt sayısı</returns>
        int SaveChanges();
        /// <summary>
        /// Değişikliklerin kaydedilmesi için kullanılır.
        /// </summary>
        /// <param name="auditBehaviour"><seealso cref="Titra.Abstractions.Data.AuditLog.AuditBehaviour"/>Değişikliklerin kaydı sırasında audit loglarının oluşup oluşmamasını belirtmek
        /// için kullanılır.</param>
        /// <returns>Etkilenen kayıt sayısı</returns>
        Task<int> SaveChangesAsync();

    }
}
