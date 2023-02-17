using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace _01_Framework.Domain
{
    public interface IRepository<in TKey,T> where T:class
    {
        void Create(T entity);
        T GetBy(TKey id);
        List<T> GetAll();
        bool Exists(Expression<Func<T, bool>> expression);
        void SaveChanges();
    }
}
