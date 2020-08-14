using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreApi.Model
{
  public class ResponseA
  {
    /// <summary>
    /// 返回的消息
    /// </summary>
    public string Msg { get; set; }
    /// <summary>
    /// 数据源
    /// </summary>
    public dynamic Entity { get; set; }

    public bool Status { get; set; }
  }
  public class ResponseB: ResponseA
  {
    /// <summary>
    /// 总行数
    /// </summary>
    public int total { get; set; }
  }
}
