using System.Data;
using System.Data.Entity;
using PMM.Core;
namespace PMM.Data
{
   public interface IDbContext
    {
        IDbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity;
        int SaveChanges();

        DataTable ExecuteStoreProcedureList(string procName, params object[] parameters);
        DataSet ExecuteStoreProcedure(string procName, params object[] parameters);
    }
}
