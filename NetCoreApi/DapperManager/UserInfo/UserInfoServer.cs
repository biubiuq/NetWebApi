using Dapper;
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace DapperManager
{
  public class UserInfoServer
  {
    public IDbConnection db = DapperHelper.OpenCurrentDbConnection();
    public void AddUser()
    {




            db.InsertAsync<UserInfo>(new UserInfo() { Name="bbb",Id="1"});



        }
  }
}
