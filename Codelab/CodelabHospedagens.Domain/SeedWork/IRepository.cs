namespace CodelabHospedagens.Domain.SeedWork
{
    public interface IRepository <T> where T : IAggregateRoot
    {
        Task<T> ObterPorIdAsync(Guid id);
        Task<IEnumerable<T>> ObterTodosAsync();
        Task InserirAsync(T entity);
        void Remover(Guid id);
    }
}
