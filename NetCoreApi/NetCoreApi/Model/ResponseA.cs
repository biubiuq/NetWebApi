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
    public dynamic Data { get; set; }

    public bool Status { get; set; }
  }
}
