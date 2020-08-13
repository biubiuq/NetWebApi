using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DapperManager;
using NetCoreApi.Data;
using NetCoreApi.Model;

namespace NetCoreApi.Controllers
{
  [Route("api/[controller]/[action]")]
  [ApiController]
  public class UserInfoesController : ControllerBase
  {
    private readonly NetCoreApiContext _context;

    public UserInfoesController(NetCoreApiContext context)
    {
      _context = context;
    }
    [HttpPost]
    public async Task<ActionResult<ResponseA>> GetUserInfo([FromBody] UserInfo info)
    {
      var aa = await _context.UserInfo.FirstOrDefaultAsync(x => x.ID == info.ID && x.Name == info.Name);
      if (aa == null)
      {
        return new ResponseA() { Status = false, Data = aa, Msg="未找到该数据" };
      }
      return new ResponseA() { Status = true, Data = aa };
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

    // POST: api/UserInfoes
    // To protect from overposting attacks, enable the specific properties you want to bind to, for
    // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
    [HttpPost]
    public async Task<ActionResult<UserInfo>> PostUserInfo(UserInfo userInfo)
    {
      _context.UserInfo.Add(userInfo);
      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateException)
      {
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
