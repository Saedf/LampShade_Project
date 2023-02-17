using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using _01_Framework.Domain;

namespace _01_Framework.Infrastructure
{
    public class RepositoryBase<TKey,T>:IRepository<TKey,T> where T:class
    {
        private readonly DbContext 
        public void Create(T entity)
        {
            throw new NotImplementedException();
        }

        public T GetBy(TKey id)
        {
            throw new NotImplementedException();
        }

        public List<T> GetAll()
        {
            throw new NotImplementedException();
        }

        public bool Exists(Expression<Func<T, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}
