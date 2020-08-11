using DapperExtensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq.Expressions;
using System.Text;

namespace DapperManager
{
    class ConnectionFactory
    {
        public static IDbConnection CreateConnection<T>() where T : IDbConnection, new()
        {
            IDbConnection connection = new T();
            connection.ConnectionString = @"server =.; uid = sa; pwd = 123456; database = JGEQ";
            connection.Open();
            return connection;
        }

        public static IDbConnection CreateSqlConnection()
        {
            return CreateConnection<SqlConnection>();
        }
    }
    interface IRepository<T>
    {
        IEnumerable<T> GetList();

        T Get(object id);

        bool Update(T t);

        T Insert(T apply);

        bool Delete(T t);

        IEnumerable<T> Find(Expression<Func<T, object>> expression, Operator op, object param);
    }
  public  class SqlRepository<T> : IRepository<T> where T : class,new()
    {
        public virtual IEnumerable<T> GetList()
        {
            using (var conn = ConnectionFactory.CreateSqlConnection())
            {
                return conn.GetList<T>();
            }

        }

        public virtual T Get(object id)
        {
            using (var conn = ConnectionFactory.CreateSqlConnection())
            {
                return conn.Get<T>(id);
            }
        }

        public virtual bool Update(T t)
        {
            using (var conn = ConnectionFactory.CreateSqlConnection())
            {
                return conn.Update(t);
            }
        }

        public virtual T Insert(T apply)
        {
            using (var conn = ConnectionFactory.CreateSqlConnection())
            {
                conn.Insert(apply);
                return apply;
            }
        }

        public virtual bool Delete(T t)
        {
            using (var conn = ConnectionFactory.CreateSqlConnection())
            {
                return conn.Delete(t);
            }
        }

        public virtual IEnumerable<T> Find(Expression<Func<T, object>> expression, Operator op, object param)
        {
            using (var conn = ConnectionFactory.CreateSqlConnection())
            {
                return conn.GetList<T>(Predicates.Field<T>(expression, op, param));
            }
        }

    }
}
