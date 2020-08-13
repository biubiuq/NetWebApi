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

    public CommonRepository<UserInfo> _manger = new CommonRepository<UserInfo>();

    // GET: api/<UserInfoController>
    [HttpGet]
    public IEnumerable<UserInfo> Get()
    {
        return  _manger.GetListT();
    }

    // GET api/<UserInfoController>/5
    [HttpGet("{id}")]
    public UserInfo Get(string id)
    {
      return _manger.GetT(id);
    }
        /// <summary>
        /// 不能用string 作参数否则会报错异常
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
    // POST api/<UserInfoController>
    [HttpPost]
    public bool Post([FromBody] UserInfo value)
    {
      return _manger.Add(value);
     
    }

    // PUT api/<UserInfoController>/5
    [HttpPut("{id}")]
    public void Put(string id, [FromBody] UserInfo value)
    {
         value.ID = id;
         _manger.Update(value);
    }

    // DELETE api/<UserInfoController>/5
    [HttpDelete("{id}")]
    public void Delete(string id)
    {
      _manger.Delete(new UserInfo() { ID = id });
    }
    
  }
}
