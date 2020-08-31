using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DapperManager;
using NetCoreApi.Data;

namespace NetCoreApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RoleUserController : ControllerBase
    {
        private readonly NetCoreApiContext _context;

        public RoleUserController(NetCoreApiContext context)
        {
            _context = context;
        }

        // GET: api/RoleUser
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Role_User>>> GetRole_User()
        {
            return await _context.Role_User.ToListAsync();
        }

        // GET: api/RoleUser/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Role_User>> GetRole_User(string id)
        {
            var role_User = await _context.Role_User.FindAsync(id);

            if (role_User == null)
            {
                return NotFound();
            }

            return role_User;
        }

        // PUT: api/RoleUser/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRole_User(string id, Role_User role_User)
        {
            if (id != role_User.Role_Id)
            {
                return BadRequest();
            }

            _context.Entry(role_User).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Role_UserExists(id))
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

        // POST: api/RoleUser
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Role_User>> PostRole_User(Role_User role_User)
        {
            _context.Role_User.Add(role_User);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (Role_UserExists(role_User.Role_Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetRole_User", new { id = role_User.Role_Id }, role_User);
        }

        // DELETE: api/RoleUser/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Role_User>> DeleteRole_User(string id)
        {
            var role_User = await _context.Role_User.FindAsync(id);
            if (role_User == null)
            {
                return NotFound();
            }

            _context.Role_User.Remove(role_User);
            await _context.SaveChangesAsync();

            return role_User;
        }

        private bool Role_UserExists(string id)
        {
            return _context.Role_User.Any(e => e.Role_Id == id);
        }
    }
}
