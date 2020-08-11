using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DapperServer
{
  public class UserInfo
  {
    public IDbConnection db = DapperHelper.OpenCurrentDbConnection();
    public void GetUser()
    {

     
    }
  }
}
