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
    public class PastEventsController : ControllerBase
    {
        private readonly PyramidTimesWebContext _context;

        public PastEventsController(PyramidTimesWebContext context)
        {
            _context = context;
        }

        // GET: api/PastEvents
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PastEvent>>> GetPastEvents()
        {
            return await _context.PastEvents.ToListAsync();
        }

        // GET: api/PastEvents/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PastEvent>> GetPastEvent(int id)
        {
            var pastEvent = await _context.PastEvents.FindAsync(id);

            if (pastEvent == null)
            {
                return NotFound();
            }

            return pastEvent;
        }

        // PUT: api/PastEvents/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPastEvent(int id, PastEvent pastEvent)
        {
            if (id != pastEvent.Id)
            {
                return BadRequest();
            }

            _context.Entry(pastEvent).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PastEventExists(id))
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

        // POST: api/PastEvents
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<PastEvent>> PostPastEvent(PastEvent pastEvent)
        {
            _context.PastEvents.Add(pastEvent);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPastEvent", new { id = pastEvent.Id }, pastEvent);
        }

        // DELETE: api/PastEvents/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PastEvent>> DeletePastEvent(int id)
        {
            var pastEvent = await _context.PastEvents.FindAsync(id);
            if (pastEvent == null)
            {
                return NotFound();
            }

            _context.PastEvents.Remove(pastEvent);
            await _context.SaveChangesAsync();

            return pastEvent;
        }

        private bool PastEventExists(int id)
        {
            return _context.PastEvents.Any(e => e.Id == id);
        }
    }
}
