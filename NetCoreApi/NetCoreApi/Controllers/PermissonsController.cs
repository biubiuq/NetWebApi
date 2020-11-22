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
    public class PermissonsController : ControllerBase
    {
        private readonly NetCoreApiContext _context;

        public PermissonsController(NetCoreApiContext context)
        {
            _context = context;
        }

        // GET: api/Permissons
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Permisson>>> GetPermissons()
        {
            return await _context.Permisson.ToListAsync();
        }

        // GET: api/Permissons/5
        [HttpGet]
        public async Task<ActionResult<Permisson>> GetPermissonId(string id)
        {
            var permisson = await _context.Permisson.Where(a=>a.Permisson_Id==id).SingleOrDefaultAsync();

            if (permisson == null)
            {
                return NotFound();
            }

            return permisson;
        }

        // PUT: api/Permissons/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPermisson(string id, Permisson permisson)
        {
            if (id != permisson.KeyId)
            {
                return BadRequest();
            }

            _context.Entry(permisson).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PermissonExists(id))
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

        // POST: api/Permissons
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Permisson>> PostPermisson(Permisson permisson)
        {
            _context.Permisson.Add(permisson);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PermissonExists(permisson.KeyId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetPermisson", new { id = permisson.KeyId }, permisson);
        }

        // DELETE: api/Permissons/5
        [HttpDelete]
        public async Task<ActionResult<Permisson>> DeletePermisson(string id)
        {
            var permisson = await _context.Permisson.FindAsync(id);
            if (permisson == null)
            {
                return NotFound();
            }

            _context.Permisson.Remove(permisson);
            await _context.SaveChangesAsync();

            return permisson;
        }

        private bool PermissonExists(string id)
        {
            return _context.Permisson.Any(e => e.KeyId == id);
        }
    }
}
