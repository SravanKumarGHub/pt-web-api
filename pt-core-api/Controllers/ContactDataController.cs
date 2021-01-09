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
    public class ContactDataController : ControllerBase
    {
        private readonly PyramidTimesWebContext _context;

        public ContactDataController(PyramidTimesWebContext context)
        {
            _context = context;
        }

        // GET: api/ContactData
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContactDatum>>> GetContactData()
        {
            return await _context.ContactData.ToListAsync();
        }

        // GET: api/ContactData/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ContactDatum>> GetContactDatum(int id)
        {
            var contactDatum = await _context.ContactData.FindAsync(id);

            if (contactDatum == null)
            {
                return NotFound();
            }

            return contactDatum;
        }

        // PUT: api/ContactData/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContactDatum(int id, ContactDatum contactDatum)
        {
            if (id != contactDatum.Id)
            {
                return BadRequest();
            }

            _context.Entry(contactDatum).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactDatumExists(id))
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

        // POST: api/ContactData
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<ContactDatum>> PostContactDatum(ContactDatum contactDatum)
        {
            _context.ContactData.Add(contactDatum);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetContactDatum", new { id = contactDatum.Id }, contactDatum);
        }

        // DELETE: api/ContactData/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ContactDatum>> DeleteContactDatum(int id)
        {
            var contactDatum = await _context.ContactData.FindAsync(id);
            if (contactDatum == null)
            {
                return NotFound();
            }

            _context.ContactData.Remove(contactDatum);
            await _context.SaveChangesAsync();

            return contactDatum;
        }

        private bool ContactDatumExists(int id)
        {
            return _context.ContactData.Any(e => e.Id == id);
        }
    }
}
