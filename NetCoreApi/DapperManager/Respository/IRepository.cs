using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq.Expressions;
using System.Text;
using Dapper.Contrib;
using Dapper.Contrib.Extensions;


namespace DapperManager
{
    public interface IRepository<T> where T : class, new()
  {
   public IDbConnection GetDbConnection();
    /// <summary>
    /// 得到一个实体类
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
    public T GetT(string t);
    /// <summary>
    /// 得到所有记录
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
    public IEnumerable<T> GetListT();
    /// <summary>
    /// 新增记录
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
    public bool Add(T t);
    /// <summary>
    /// 更新
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
    public bool Update(T t);
    public bool Delete(T t);
  }

  public class CommonRepository<T>: IRepository<T>
    where T : class, new()
  {
    /// 数据库连接名
    public string ConnectionString = @"server =DESKTOP-B64LLUV\RJZGBPC; uid = sa; pwd = 123456; database = JGEQ";

    public bool Add(T t)
    {
      using (IDbConnection db = GetDbConnection())
      {
        if (db.Insert(t) == 1)
        {
          return true;
        }
        return false;
      }
    }

    public bool Delete(T t)
    {
      using (IDbConnection db = GetDbConnection())
      {
        return db.Delete(t);
      }
    }

    public IDbConnection GetDbConnection()
    {
      return  new SqlConnection(ConnectionString);
    }

    public IEnumerable<T> GetListT()
    {
      using (IDbConnection db = GetDbConnection())
      {
        return db.GetAll<T>();
      }
    }
   
    public T GetT(string t)
    {
      using (IDbConnection db = GetDbConnection())
      {
        return db.Get<T>(t);
      }
    }

    public bool Update(T t)
    {
      using (IDbConnection db = GetDbConnection())
      {
        return db.Update(t);
      }
    }
  }
}
