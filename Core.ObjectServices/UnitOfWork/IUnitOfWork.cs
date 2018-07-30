namespace Core.ObjectServices.UnitOfWork
{
    using Core.ObjectServices.Repositories;

    public interface IUnitOfWork
    {
        IRepository<T> GetRepository<T>() where T: class;
        bool Save();
        void Dispose();
    }
}
