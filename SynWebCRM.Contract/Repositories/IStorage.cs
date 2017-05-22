namespace SynWebCRM.Contract.Repositories
{
    public interface IStorage
    {
        T GetRepository<T>() where T : IRepository;
        void Save();
    }
}
