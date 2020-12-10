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
    public class SpeakerRegistrationsController : ControllerBase
    {
        private readonly PyramidTimesWebContext _context;

        public SpeakerRegistrationsController(PyramidTimesWebContext context)
        {
            _context = context;
        }

        // GET: api/SpeakerRegistrations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SpeakerRegistration>>> GetSpeakerRegistrations()
        {
            return await _context.SpeakerRegistrations.ToListAsync();
        }

        // GET: api/SpeakerRegistrations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SpeakerRegistration>> GetSpeakerRegistration(int id)
        {
            var speakerRegistration = await _context.SpeakerRegistrations.FindAsync(id);

            if (speakerRegistration == null)
            {
                return NotFound();
            }

            return speakerRegistration;
        }

        // PUT: api/SpeakerRegistrations/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSpeakerRegistration(int id, SpeakerRegistration speakerRegistration)
        {
            if (id != speakerRegistration.Id)
            {
                return BadRequest();
            }

            _context.Entry(speakerRegistration).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SpeakerRegistrationExists(id))
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

        // POST: api/SpeakerRegistrations
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<SpeakerRegistration>> PostSpeakerRegistration(SpeakerRegistration speakerRegistration)
        {
            _context.SpeakerRegistrations.Add(speakerRegistration);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSpeakerRegistration", new { id = speakerRegistration.Id }, speakerRegistration);
        }

        // DELETE: api/SpeakerRegistrations/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<SpeakerRegistration>> DeleteSpeakerRegistration(int id)
        {
            var speakerRegistration = await _context.SpeakerRegistrations.FindAsync(id);
            if (speakerRegistration == null)
            {
                return NotFound();
            }

            _context.SpeakerRegistrations.Remove(speakerRegistration);
            await _context.SaveChangesAsync();

            return speakerRegistration;
        }

        private bool SpeakerRegistrationExists(int id)
        {
            return _context.SpeakerRegistrations.Any(e => e.Id == id);
        }
    }
}
