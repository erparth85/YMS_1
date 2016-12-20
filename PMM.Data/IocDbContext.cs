using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using PMM.Core;

namespace PMM.Data
{
    public class IocDbContext : DbContext, IDbContext
    {
        public IocDbContext()
            : base("name=DbConnectionString")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            
            var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
            .Where(type => !String.IsNullOrEmpty(type.Namespace))
            .Where(type => type.BaseType != null && type.BaseType.IsGenericType && type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>));
            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.Configurations.Add(configurationInstance);
            }
         base.OnModelCreating(modelBuilder);
        }

        public new IDbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity
        {
            return base.Set<TEntity>();
        }

        public DataTable ExecuteStoreProcedureList(string procName, params object[] parameters)
        {
            DataTable dt = new DataTable();
            DbDataAdapter adapter = new SqlDataAdapter();
            ////var connection = context.Connection;
            var connection = this.Database.Connection;
            ////Don't close the connection after command execution


            ////open the connection for use
            if (connection.State == ConnectionState.Closed)
                connection.Open();
            //create a command object
            using (var cmd = connection.CreateCommand())
            {
                //command to execute
                cmd.CommandText = procName;
                cmd.CommandType = CommandType.StoredProcedure;

                // move parameters to command object
                if (parameters != null)
                    foreach (var p in parameters)
                        cmd.Parameters.Add(p);

                adapter.SelectCommand = cmd;
                DataSet ds = new DataSet();
                adapter.Fill(ds);

                if (ds.Tables != null && ds.Tables.Count > 0)
                    dt = ds.Tables[0];

            }
            return dt;
        }

        public DataSet ExecuteStoreProcedure(string procName, params object[] parameters)
        {
            DataSet ds = new DataSet();
            DbDataAdapter adapter = new SqlDataAdapter();
            ////var connection = context.Connection;
            var connection = this.Database.Connection;
            ////Don't close the connection after command execution


            ////open the connection for use
            if (connection.State == ConnectionState.Closed)
                connection.Open();
            //create a command object
            using (var cmd = connection.CreateCommand())
            {
                //command to execute
                cmd.CommandText = procName;
                cmd.CommandType = CommandType.StoredProcedure;

                // move parameters to command object
                if (parameters != null)
                    foreach (var p in parameters)
                        cmd.Parameters.Add(p);

                adapter.SelectCommand = cmd;
               
                adapter.Fill(ds);

            }
            return ds;
        }
    }
}
