using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Dapper.Contrib.Extensions;

namespace DapperManager
{
    [Table("UserInfo")]
    public class UserInfo
  {
      
    
    
        public string ID { get; set; }
      
       public string Name { get; set; }
       public string Password { get; set; }
       public string Address { get; set; }
       public string Token { get; set; }
        /// <summary>
       /// 状态0表示 启用，1表示禁用
      /// </summary>
      public string Status { get; set; }
      public DateTime Create_Date { get; set; }
      public string Create_User { get; set; }

  }
}
