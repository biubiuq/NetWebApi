using System;
using System.Collections.Generic;
using System.Text;
using Dapper.Contrib.Extensions;

namespace DapperManager
{
    [Table("UserInfo")]
    public class UserInfo
  {
     [Key]
    public String Id { get; set; }
        public string Name { get; set; }

    }
}
