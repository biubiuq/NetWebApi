using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DapperManager;
using Microsoft.AspNetCore.Mvc;
using NetCoreApi.Entity;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NetCoreApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class UserInfoController : ControllerBase
  {
        private UserInfoServer db = new UserInfoServer();
      public  SqlRepository<UserInfo> aa = new DapperManager.SqlRepository<UserInfo>();

       // GET: api/<UserInfoController>
       [HttpGet]
    public IEnumerable<string> Get()
    {
            aa.Insert(new UserInfo() {Name="2222",Id="2222" });
        db.AddUser();
      return new string[] { "value1", "value2" };
    }

    // GET api/<UserInfoController>/5
    [HttpGet("{id}")]
    public string Get(int id)
    {
      return "valueAAA";
    }
        /// <summary>
        /// 不能用string 作参数否则会报错异常
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
    // POST api/<UserInfoController>
    [HttpPost]
    public UserInfo Post([FromBody] UserInfo value)
    {

            return value;
    }

    // PUT api/<UserInfoController>/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/<UserInfoController>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
  }
}
