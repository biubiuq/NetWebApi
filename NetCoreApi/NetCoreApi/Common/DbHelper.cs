using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace NetCoreApi.Common
{
  public class DbHelper
  {
    /// <summary>
    /// ef 分页 1.0 版本  lb
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="t"></param>
    /// <returns></returns>
    public Expression<Func<T, bool>> PageSearch<T>(T t)
    {
      ///p=>
      ParameterExpression parameter = Expression.Parameter(typeof(T), "p");
      Expression condition = null;
      foreach (PropertyInfo item in typeof(T).GetProperties())
      {
        ///如果为空则不加入搜索，关于时间类型暂时不处理
        if (item.PropertyType == typeof(string))
        {
          var data = item.GetValue(t) ;
          if (string.IsNullOrEmpty(data.ToString())) continue;
          ConstantExpression constant = Expression.Constant(data);
          Expression propertyExpression = Expression.Property(parameter, typeof(T).GetProperty(item.Name));
          BinaryExpression binary = Expression.Equal(constant, propertyExpression);
          if (condition == null)
          {
            condition = binary;
          }
          else
          {
            condition = Expression.And(condition, binary);
          }
      
        }
     
      }
      Expression<Func<T, bool>> expression = Expression.Lambda<Func<T, bool>>(condition, parameter);
      return expression;
    }
  }
}
