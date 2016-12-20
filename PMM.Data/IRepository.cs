using System.Data;
using System.Linq;
using PMM.Core;

namespace PMM.Data
{
    public interface IRepository<T> where T : BaseEntity
    {
        T GetById(object id);
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
        IQueryable<T> Table { get; }
        DataTable ExecuteStoreProcedureList(string procName, params object[] parameters);
        DataSet ExecuteStoreProcedure(string procName, params object[] parameters);
    }
}
