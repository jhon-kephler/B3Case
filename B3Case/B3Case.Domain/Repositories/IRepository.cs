using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B3Case.Domain.Repositories
{
    public interface IRepository<T>
    {
        void Add(T entity);
        void Update(int id, T entity);
        void Delete(int id);
        T GetById(int id);
        Task<T> AddAsync(T entity);
        List<T> GetAll();
    }
}
