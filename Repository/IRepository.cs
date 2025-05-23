using SocialMediaApp.Models;

namespace SocialMediaApp.Repository
{
    public interface IRepository<T>
    {
        List<T> GetAll(string includes = null);
        T GetById(long id);
        void Add(T entity);
        void Update(T entity);
        void Delete(long Id);
        void Save();
    }
}
