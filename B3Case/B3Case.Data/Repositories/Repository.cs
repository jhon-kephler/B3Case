using B3Case.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace B3Case.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DbContext _dbContext;
        protected readonly DbSet<T> _dbSet;

        public Repository(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<T>();
        }

        public void Add(T entity)
        {
            try
            {
                _dbSet.Attach(entity);
                _dbContext.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine(ex.InnerException.Message);
            }
        }

        public async Task<T> AddAsync(T entity)
        {
            try
            {
                await _dbSet.AddAsync(entity);
                await _dbContext.SaveChangesAsync();

                return entity;
            }
            catch (DbUpdateException ex)
            {
                throw;
            }
        }

        public void Update(int id, T entity)
        {
            var existingEntity = _dbSet.Find(id);
            if (existingEntity != null)
            {
                var properties = typeof(T).GetProperties();
                foreach (var property in properties)
                {
                    var newValue = property.GetValue(entity);
                    var oldValue = property.GetValue(existingEntity);
                    var propertyName = property.Name;

                    if (newValue != null && !newValue.Equals(oldValue))
                    {
                        if (!IsZero(newValue))
                        {
                            property.SetValue(existingEntity, newValue);
                            _dbContext.Entry(existingEntity).Property(propertyName).IsModified = true;
                        }
                    }
                }
                _dbContext.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            var entity = _dbSet.Find(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                _dbContext.SaveChanges();
            }
            else
            {
                throw new ArgumentException("A entidade com o ID fornecido não foi encontrado.");
            }
        }

        public T GetById(int id) => _dbSet.Find(id);

        public List<T> GetAll() => _dbSet.ToList();

        protected bool IsZero(object value)
        {
            if (value is int intValue)
            {
                return intValue == 0;
            }
            else if (value is double doubleValue)
            {
                return doubleValue == 0.0;
            }

            return false;
        }
    }
}
