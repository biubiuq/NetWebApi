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
    public class RolesController : ControllerBase
    {
        private readonly NetCoreApiContext _context;
        RolePermissonController RolePermisson;

        public RolesController(NetCoreApiContext context)
        {
            _context = context;
            RolePermisson = new RolePermissonController(_context);
        }

        // GET: api/Roles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<dynamic>>> GetRole()
        {
            return await _context.Role.ToListAsync();
        }
        public async Task< IList<Role>> GetAllRolePermisson()
        {
         
            ////先得到所有角色
            IList<Role> lrole =  _context.Role.ToList();
            
            foreach (Role item in lrole)
            {
                item.Permissons =  RolePermisson.GetRolePermissonById(item.Role_Id).Result.Value as ICollection<Permisson>;
            
            }
            return  lrole;
        }
        // GET: api/Roles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Role>> GetRole(string id)
        {
            var role = await _context.Role.FindAsync(id);

            if (role == null)
            {
                return NotFound();
            }

            return role;
        }

        // PUT: api/Roles/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRole(string id, Role role)
        {
            if (id != role.Role_Id)
            {
                return BadRequest();
            }

            _context.Entry(role).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoleExists(id))
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

        // POST: api/Roles
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Role>> PostRole(Role role)
        {
            _context.Role.Add(role);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (RoleExists(role.Role_Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetRole", new { id = role.Role_Id }, role);
        }

        // DELETE: api/Roles/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Role>> DeleteRole(string id)
        {
            var role = await _context.Role.FindAsync(id);
            if (role == null)
            {
                return NotFound();
            }

            _context.Role.Remove(role);
            await _context.SaveChangesAsync();

            return role;
        }

        private bool RoleExists(string id)
        {
            return _context.Role.Any(e => e.Role_Id == id);
        }
    }
}
