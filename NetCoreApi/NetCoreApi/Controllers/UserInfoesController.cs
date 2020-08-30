using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DapperManager;
using NetCoreApi.Data;
using log4net.Core;
using Microsoft.Extensions.Logging;
using NetCoreApi.Model;
using NetCoreApi.Common;

namespace NetCoreApi.Controllers
{
  [Route("api/[controller]/[action]")]
  [ApiController]
 
  public class UserInfoesController : ControllerBase
  {
    private readonly NetCoreApiContext _context;
        private readonly DbHelper helper = new DbHelper();

    public UserInfoesController(NetCoreApiContext context, ILogger<UserInfoesController> logger )
    {
      _context = context;
    }
    /// <summary>
    /// 登录
    /// </summary>
    /// <param name="info"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<ResponseA>> GetUserInfo([FromBody] UserInfo info)
    {
      var aa = await _context.UserInfo.FirstOrDefaultAsync(x => x.Name == info.Name && x.Password == info.Password);
      if (aa == null)
      {
        return new ResponseA() { Status = false, Entity = aa, Msg="未找到该数据" };
      }
      return new ResponseA() { Status = true, Entity = aa };
    }
    /// <summary>
    /// 分页
    /// </summary>
    /// <param name="info"></param>
    /// <returns></returns>
    [HttpGet]
    public  ActionResult<ResponseA> GetUsers(int pageNum, int pageSize,

         [ModelBinder(BinderType = typeof(EntityModelBinder2<UserInfo>))]
      UserInfo userInfo)
    {
      var aa = _context.UserInfo.Skip(pageSize * (pageNum - 1)).Take(pageSize).OrderByDescending(a => a.Create_Date).AsQueryable();
      if (aa == null)
      {
        return new ResponseA() { Status = false, Entity = aa, Msg = "未找到该数据" };
      }
      return new ResponseB() { Status = true, Entity = aa, total = _context.UserInfo.Count()};
    }
    // GET: api/UserInfoes/5
    [HttpGet("{id}")]
    public async Task<ActionResult<UserInfo>> GetUserInfoById(string id)
    {
      var userInfo = await _context.UserInfo.FindAsync(id);

      if (userInfo == null)
      {
        return NotFound();
      }

      return userInfo;
    }

    // PUT: api/UserInfoes/5
    // To protect from overposting attacks, enable the specific properties you want to bind to, for
    // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
    [HttpPut("{id}")]
    public async Task<IActionResult> PutUserInfo(string id, UserInfo userInfo)
    {
      if (id != userInfo.ID)
      {
        return BadRequest();
      }

      _context.Entry(userInfo).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!UserInfoExists(id))
        {
          return NotFound();
        }
        else
        {
          throw;
        }
      }

      return NoContent();
    }
    [HttpGet]
    public void GetSSS(
          [ModelBinder(BinderType = typeof(ArrayModelBinder))]
      List<String> ids)
    {
               ids.Add("aaa");
    }
    // POST: api/UserInfoes
    // To protect from overposting attacks, enable the specific properties you want to bind to, for
    // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
    [HttpPost]
    public async Task<ActionResult<UserInfo>> PostUserInfo(
        [ModelBinder(BinderType = typeof(EntityModelBinder2<UserInfo>))]
      UserInfo userInfo)
    {
      var aa= _context.UserInfo.Where(helper.PageSearch(userInfo));
      userInfo.ID = Guid.NewGuid().ToString();
      _context.UserInfo.Add(userInfo);
      try
      {
        _context.Entry(userInfo).State = EntityState.Added;
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateException e)
      {
        var a = e;
        if (UserInfoExists(userInfo.ID))
        {
          return Conflict();
        }
        else
        {
          throw;
        }
      }

      return CreatedAtAction("GetUserInfo", new { id = userInfo.ID }, userInfo);
    }

    // DELETE: api/UserInfoes/5
    [HttpDelete("{id}")]
    public async Task<ActionResult<UserInfo>> DeleteUserInfo(string id)
    {
      var userInfo = await _context.UserInfo.FindAsync(id);
      if (userInfo == null)
      {
        return NotFound();
      }

      _context.UserInfo.Remove(userInfo);
      await _context.SaveChangesAsync();

      return userInfo;
    }

    private bool UserInfoExists(string id)
    {
      return _context.UserInfo.Any(e => e.ID == id);
    }
  }
}
