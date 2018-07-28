using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndroidManager.DAL.Interfaces {

    public interface IRepository<T> : IDisposable {
        void Create(T item);
        void Update(T item);
        void Remove(T item);
        T Get(int? id);
        IEnumerable<T> GetAll();
        IEnumerable<T> Find(Func<T, bool> predicate);
    }
}
