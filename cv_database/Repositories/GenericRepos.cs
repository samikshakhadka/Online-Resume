using cv_database.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;



namespace cv_database.Repositories
{
    public class GenericRepos : IGenericRepos
    {
        private readonly cv_databaseContext _context;
        public GenericRepos(cv_databaseContext context)

        {
            _context = context;
        }
        public async Task<List<T>> GetAll<T>() where T : class
        {
            if (_context.Information == null)
            {
                throw new Exception("message");
            }

            return await this._context.Set<T>() .ToListAsync();
            

        }

        public async Task<T> GetOne<T>(int id) where T : class
        {

            if (_context.Information == null)
            {
                throw new Exception("message");
            }


            return await this._context.Set<T>().FindAsync(id);


        }

        public async Task<T> PostAll<T>(T tObj) where T : class
        {
            _context.Set<T>().Add(tObj);
            await _context.SaveChangesAsync();
            return tObj;

        }

        public async Task UpdateAll<T>(T tObj) where T : class
        {
            if (_context.Set<T>() == null)
            {
                throw new Exception("Bad Request");
            }
            _context.Set<T>().Update(tObj);
            await _context.SaveChangesAsync();
            
           
        }

        public async Task DeleteAll<T>(T tObj) where T : class
        {
            if (_context.Set<T>() == null)
            {
                throw new Exception("Bad Request");
            }
            _context.Set<T>().Remove(tObj);
            await _context.SaveChangesAsync();
            
        }

        public async Task<List<T>> GetUserData<T>(Expression<Func<T, bool>> ForUser) where T : class
        {
            return await _context.Set<T>().Where(ForUser).ToListAsync();
        }

        public async Task<T> GetByName<T>(Expression<Func<T, bool>> ForUser) where T : class
        {
            if (_context.Set<T>() == null)
            {
                throw new Exception("Not Found");
            }
            return await this._context.Set<T>().Where(ForUser).FirstOrDefaultAsync();
        }




    }
}
