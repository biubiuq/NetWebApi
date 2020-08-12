using System;
using System.Collections.Generic;
using System.Text;
using Dapper.Contrib.Extensions;

namespace DapperManager
{
    [Table("UserInfo")]
    public class UserInfo
  {
    [UniqueCheckIdValidate(ErrorMessage = "Id不可重复")]
    public string Id { get; set; }

        public string Name { get; set; }

    }
}
