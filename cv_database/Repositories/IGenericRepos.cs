using System.Linq.Expressions;

namespace cv_database.Repositories
{
    public interface IGenericRepos
    {
        Task<List<T>> GetAll<T>() where T : class;

        Task<T> GetOne<T>(int id) where T : class;

        Task<T> PostAll<T>(T tObj) where T : class;

        Task UpdateAll<T>(T tObj) where T : class;

        Task DeleteAll<T>(T tObj) where T : class;
        Task<List<T>> GetUserData<T>(Expression<Func<T,bool>>ForUser) where T : class;

        Task <T> GetByName<T>(Expression<Func<T, bool>> ForUser) where T : class;




    }
}
