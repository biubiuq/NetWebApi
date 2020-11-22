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
    public class RolePermissonController : ControllerBase
    {
        private readonly NetCoreApiContext _context;

        public RolePermissonController(NetCoreApiContext context)
        {
            _context = context;
        }

        // GET: api/RolePermisson
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Role_Permisson>>> GetRole_Permisson()
        {
            return await _context.Role_Permisson.ToListAsync();
        }

        // GET: api/RolePermisson/5
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Permisson>>> GetRolePermissonById(string id)
        {
            List<Role_Permisson> role_Permisson = await _context.Role_Permisson.Where(a=>a.Role_Id==id).ToListAsync<Role_Permisson>();
            List<string> strlist = new List<string>();
            foreach (Role_Permisson item in role_Permisson)
            {
                strlist.Add(item.Permisson_Id);
            }
            if (role_Permisson == null)
            {
                return NotFound();
            }
   
            var permisson = await _context.Permisson.Where(a =>strlist.Contains(a.Permisson_Id)).Include(a => a.Permissons).ToListAsync();
            foreach (Permisson item in permisson)
            {
             
                foreach (Permisson child in item.Permissons)
                {
                    child.Permissons = await _context.Permisson.Where(a => a.Parent_Id == child.Permisson_Id).ToListAsync();
                 

                }
            }

            return permisson;
        }

        // PUT: api/RolePermisson/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRole_Permisson(string id, Role_Permisson role_Permisson)
        {
            if (id != role_Permisson.Guid)
            {
                return BadRequest();
            }

            _context.Entry(role_Permisson).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Role_PermissonExists(id))
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

        // POST: api/RolePermisson
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Role_Permisson>> PostRole_Permisson(Role_Permisson role_Permisson)
        {
            _context.Role_Permisson.Add(role_Permisson);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (Role_PermissonExists(role_Permisson.Guid))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetRole_Permisson", new { id = role_Permisson.Guid }, role_Permisson);
        }

        // DELETE: api/RolePermisson/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Role_Permisson>> DeleteRole_Permisson(string id)
        {
            var role_Permisson = await _context.Role_Permisson.FindAsync(id);
            if (role_Permisson == null)
            {
                return NotFound();
            }

            _context.Role_Permisson.Remove(role_Permisson);
            await _context.SaveChangesAsync();

            return role_Permisson;
        }

        private bool Role_PermissonExists(string id)
        {
            return _context.Role_Permisson.Any(e => e.Guid == id);
        }
    }
}
