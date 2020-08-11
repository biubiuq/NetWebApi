using System;
using System.Collections.Generic;
using System.Text;
using Dapper.Contrib.Extensions;

namespace DapperServer.Entity
{
  public class UserInfo
  {
    [Key]
    public int Id { get; set; }

  }
}
