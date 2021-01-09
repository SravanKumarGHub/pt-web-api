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
    public class EventRegistrationsController : ControllerBase
    {
        private readonly PyramidTimesWebContext _context;

        public EventRegistrationsController(PyramidTimesWebContext context)
        {
            _context = context;
        }

        // GET: api/EventRegistrations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventRegistration>>> GetEventRegistrations()
        {
            return await _context.EventRegistrations.ToListAsync();
        }

        // GET: api/EventRegistrations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EventRegistration>> GetEventRegistration(int id)
        {
            var eventRegistration = await _context.EventRegistrations.FindAsync(id);

            if (eventRegistration == null)
            {
                return NotFound();
            }

            return eventRegistration;
        }

        // PUT: api/EventRegistrations/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEventRegistration(int id, EventRegistration eventRegistration)
        {
            if (id != eventRegistration.Id)
            {
                return BadRequest();
            }

            _context.Entry(eventRegistration).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventRegistrationExists(id))
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

        // POST: api/EventRegistrations
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<EventRegistration>> PostEventRegistration(EventRegistration eventRegistration)
        {
            _context.EventRegistrations.Add(eventRegistration);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEventRegistration", new { id = eventRegistration.Id }, eventRegistration);
        }

        // DELETE: api/EventRegistrations/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<EventRegistration>> DeleteEventRegistration(int id)
        {
            var eventRegistration = await _context.EventRegistrations.FindAsync(id);
            if (eventRegistration == null)
            {
                return NotFound();
            }

            _context.EventRegistrations.Remove(eventRegistration);
            await _context.SaveChangesAsync();

            return eventRegistration;
        }

        private bool EventRegistrationExists(int id)
        {
            return _context.EventRegistrations.Any(e => e.Id == id);
        }
    }
}
