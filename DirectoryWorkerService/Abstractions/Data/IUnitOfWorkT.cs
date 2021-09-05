namespace Abstractions.Data
{
    public interface IUnitOfWork<TContext>: IUnitOfWork where TContext : IUnitOfWork
    {
    }
}
