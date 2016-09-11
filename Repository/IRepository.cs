using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingleResposibility.Repository
{
    interface IRepository<T> where T : class
    {
        bool Save(T newObject);

        bool Delete(T newObject);

        IList<T> Read();

        T Read(int id);

        bool Update(T updatedObject);
    }
}
