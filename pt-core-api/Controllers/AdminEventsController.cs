using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pt_core_api.Models;

namespace pt_core_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminEventsController : ControllerBase
    {
        private readonly PyramidTimesWebContext _context;

        public AdminEventsController(PyramidTimesWebContext context)
        {
            _context = context;
        }

        // GET: api/AdminEvents
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AdminEvent>>> GetAdminEvents()
        {
            return await _context.AdminEvents.ToListAsync();
        }

        // GET: api/AdminEvents/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AdminEvent>> GetAdminEvent(int id)
        {
            var adminEvent = await _context.AdminEvents.FindAsync(id);

            if (adminEvent == null)
            {
                return NotFound();
            }

            return adminEvent;
        }

        // PUT: api/AdminEvents/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAdminEvent(int id, AdminEvent adminEvent)
        {
            if (id != adminEvent.Id)
            {
                return BadRequest();
            }

            _context.Entry(adminEvent).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdminEventExists(id))
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

        // POST: api/AdminEvents
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<AdminEvent>> PostAdminEvent(AdminEvent adminEvent)
        {
            _context.AdminEvents.Add(adminEvent);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAdminEvent", new { id = adminEvent.Id }, adminEvent);
        }

        // DELETE: api/AdminEvents/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<AdminEvent>> DeleteAdminEvent(int id)
        {
            var adminEvent = await _context.AdminEvents.FindAsync(id);
            if (adminEvent == null)
            {
                return NotFound();
            }

            _context.AdminEvents.Remove(adminEvent);
            await _context.SaveChangesAsync();

            return adminEvent;
        }

        private bool AdminEventExists(int id)
        {
            return _context.AdminEvents.Any(e => e.Id == id);
        }
    }
}
