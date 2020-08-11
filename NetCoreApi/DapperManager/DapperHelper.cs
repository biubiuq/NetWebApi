using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace DapperManager
{
  public class DapperHelper
  {
    /// 数据库连接名
    private static string ConnectionString = @"server=.;uid=sa;pwd=123456;database=JGEQ";

        /// 获取连接名        
        private static string Connection
    {
      get { return ConnectionString; }
      //set { _connection = value; }
    }

    /// 返回连接实例        
    private static IDbConnection dbConnection = null;

    /// 静态变量保存类的实例        
    private static DapperHelper uniqueInstance;

    /// 定义一个标识确保线程同步        
    private static readonly object locker = new object();
    /// <summary>
    /// 私有构造方法，使外界不能创建该类的实例，以便实现单例模式
    /// </summary>
    private DapperHelper()
    {
            // 这里为了方便演示直接写的字符串，实例项目中可以将连接字符串放在配置文件中，再进行读取。
            ConnectionString = @"server=.;uid=sa;pwd=123456;database=JGEQ";
    }

    /// <summary>
    /// 获取实例，这里为单例模式，保证只存在一个实例
    /// </summary>
    /// <returns></returns>
    public static DapperHelper GetInstance()
    {
      // 双重锁定实现单例模式，在外层加个判空条件主要是为了减少加锁、释放锁的不必要的损耗
      if (uniqueInstance == null)
      {
        lock (locker)
        {
          if (uniqueInstance == null)
          {
            uniqueInstance = new DapperHelper();
          }
        }
      }
      return uniqueInstance;
    }


    /// <summary>
    /// 创建数据库连接对象并打开链接
    /// </summary>
    /// <returns></returns>
    public static IDbConnection OpenCurrentDbConnection()
    {
      if (dbConnection == null)
      {
        dbConnection = new SqlConnection(Connection);
      }
      //判断连接状态
      if (dbConnection.State == ConnectionState.Closed)
      {
        dbConnection.Open();
      }
      return dbConnection;
    }
        public async Task<bool> InsertAsync<T>(T t) where T : class
        {
            using (IDbConnection connection = new SqlConnection(ConnectionString))
            {
                return await connection.InsertAsync(t) > 0;
            }
        }

        public async Task<bool> InsertAsync<T>(List<T> list) where T : class
        {
            using (IDbConnection connection = new SqlConnection(ConnectionString))
            {
                return await connection.InsertAsync(list) > 0;
            }
        }

        public async Task<bool> DeleteAsync<T>(T t) where T : class
        {
            using (IDbConnection connection = new SqlConnection(ConnectionString))
            {
                return await connection.DeleteAsync(t);
            }
        }

        public async Task<bool> UpdateAsync<T>(T t) where T : class
        {
            using (IDbConnection connection = new SqlConnection(ConnectionString))
            {
                return await connection.UpdateAsync(t);
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>() where T : class
        {
            using (IDbConnection connection = new SqlConnection(ConnectionString))
            {
                return await connection.GetAllAsync<T>();
            }
        }

        public async Task<T> GetByIDAsync<T>(int id) where T : class
        {
            using (IDbConnection connection = new SqlConnection(ConnectionString))
            {
                return await connection.GetAsync<T>(id);
            }
        }

        //public async Task<int> ExecuteAsync(string path)
        //{
        //    using (IDbConnection connection = new SqlConnection(ConnectionString))
        //    {
        //        using (StreamReader streamReader = new StreamReader(path, System.Text.Encoding.UTF8))
        //        {
        //            var script = await streamReader.ReadToEndAsync();
        //            return await connection.ExecuteAsync(script);
        //        }
        //    }
        //}

        //public async Task<int> ExecuteAsync(string sql, object param = null)
        //{
        //    using (IDbConnection connection = new SqlConnection(ConnectionString))
        //    {
        //        return await connection.ExecuteAsync(sql, param);
        //    }
        //}

        //public async Task<bool> ExecuteAsyncTransaction(List<string> list)
        //{
        //    using (IDbConnection connection = new SqlConnection(ConnectionString))
        //    {
        //        connection.Open();

        //        IDbTransaction transaction = connection.BeginTransaction();

        //        try
        //        {
        //            foreach (var sql in list)
        //            {
        //                await connection.ExecuteAsync(sql, null, transaction);
        //            }

        //            transaction.Commit();

        //            return true;
        //        }
        //        catch (Exception e)
        //        {
        //            transaction.Rollback();

        //            return false;
        //        }
        //    }
        //}

        //public async Task<bool> ExecuteAsyncTransaction(List<KeyValuePair<string, object>> list)
        //{
        //    using (IDbConnection connection = new SqlConnection(ConnectionString))
        //    {
        //        connection.Open();

        //        IDbTransaction transaction = connection.BeginTransaction();

        //        try
        //        {
        //            foreach (var item in list)
        //            {
        //                await connection.ExecuteAsync(item.Key, item.Value, transaction);
        //            }

        //            transaction.Commit();

        //            return true;
        //        }
        //        catch (Exception e)
        //        {
        //            transaction.Rollback();

        //            return false;
        //        }
        //    }
        //}

        //public async Task<IEnumerable<dynamic>> QueryAsync(string sql, object param = null)
        //{
        //    using (IDbConnection connection = new SqlConnection(ConnectionString))
        //    {
        //        return await connection.QueryAsync(sql, param);
        //    }
        //}

        //public async Task<dynamic> QueryFirstOrDefaultAsync(string sql, object param = null)
        //{
        //    using (IDbConnection connection = new SqlConnection(ConnectionString))
        //    {
        //        return await connection.QueryFirstOrDefaultAsync(sql, param);
        //    }
        //}
    }
}
