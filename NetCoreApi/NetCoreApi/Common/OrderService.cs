using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreApi
{
  /// <summary>
  /// Test Singleton,Transient,Scoped 基本用法
  /// </summary>
  public class OrderService : IOrderService
  {
    private string guid;

    public OrderService()
    {
      guid = $"时间:{DateTime.Now}, guid={ Guid.NewGuid()}";
    }

    public override string ToString()
    {
      return guid;
    }
  }

  public interface IOrderService
  {
  }
  public class OrderServiceb : IOrderServiceb
  {
    private string guid;

    public OrderServiceb()
    {
      guid = $"时间:{DateTime.Now}, guid={ Guid.NewGuid()}";
    }

    public override string ToString()
    {
      return guid;
    }
  }

  public interface IOrderServiceb
  {
  }
  
  public class OrderServicebc : IOrderServicebc
  {
    private string guid;

    public OrderServicebc()
    {
      guid = $"时间:{DateTime.Now}, guid={ Guid.NewGuid()}";
    }

    public override string ToString()
    {
      return guid;
    }
  }

  public interface IOrderServicebc
  {
  }
}
