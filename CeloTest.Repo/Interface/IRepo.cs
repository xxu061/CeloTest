using CeloTest.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CeloTest.Repo
{
    public interface IRepo<T>
    {
        Task<IEnumerable<User>> Get(Func<T, bool> filter, int? take = null, int? skip = null);
        Task<IList<T>> GetAll();
        Task Update(T t);
        Task Delete(T t);
    }
}
