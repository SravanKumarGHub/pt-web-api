using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pt_core_api.Models;
using System.Web;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System.Net.Http.Headers;

namespace pt_core_api.Controllers
{
    [Route("api/[controller]")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SpeakerRegistrationsController : ControllerBase
    {
        private readonly PyramidTimesWebContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;
        public SpeakerRegistrationsController(PyramidTimesWebContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
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
        //[ActionName("speakerRegistration")]
        public async Task<ActionResult<SpeakerRegistration>> PostSpeakerRegistration([FromForm] SpeakerRegistration speakerRegistration)
        {
            try
            {
                var file = Request.Form.Files[0];
                string folderName = "Img";
                string webRootPath = _hostingEnvironment.ContentRootPath;
                string newPath = Path.Combine(webRootPath, folderName);
                if (!Directory.Exists(newPath))
                {
                    Directory.CreateDirectory(newPath);
                }
                if (file.Length > 0)
                {
                    string fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    string fullPath = Path.Combine(newPath, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                }
                //return Json("Upload Successful.");
            }
            catch (System.Exception ex)
            {
                //return Json("Upload Failed: " + ex.Message);
            }
            if(speakerRegistration.ProfilePicture.IndexOf("base64") > 0 && speakerRegistration.ProfilePicture.Split("base64,").Length > 1)
            {
                speakerRegistration.ProfilePicture = speakerRegistration.ProfilePicture.Split("base64,")[1].ToString();
            }
            _context.SpeakerRegistrations.Add(speakerRegistration);
            await _context.SaveChangesAsync();



            return CreatedAtAction("GetSpeakerRegistration", new { id = speakerRegistration.Id }, speakerRegistration);
        }

        // POST: api/SpeakerRegistrations
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        //[HttpPost]
        //[ActionName("speakerRegistrationInclProfile")]
        //public async Task<ActionResult<SpeakerRegistration>> SpeakerRegistrationInclProfilePic(SpeakerRegistration regsirtation, IFormFile ProfilePicture)
        //{
        //    string webRootPath = _hostingEnvironment.WebRootPath;
        //    string contentRootPath = _hostingEnvironment.ContentRootPath;
        //    if (ModelState.IsValid)
        //    {
        //        SpeakerRegistration data = new SpeakerRegistration();

        //        string fileContent = string.Empty;
        //        string fileContentType = string.Empty;
        //        var allowedExtensions = new[] {
        //             ".Jpg", ".png", ".jpg", "jpeg"
        //         };
        //        data.Name = regsirtation.Name;
        //        data.ProfilePicture = ProfilePicture.ToString(); //getting complete url  

        //        var fileName = Path.GetFileName(ProfilePicture.FileName); //getting only file name(ex-ganesh.jpg)  
        //        var ext = Path.GetExtension(ProfilePicture.FileName); //getting the extension(ex-.jpg)
        //        byte[] uploadedFile = new byte[ProfilePicture.Length];
        //        ProfilePicture.OpenReadStream();

        //        // Initialization.  
        //        fileContent = Convert.ToBase64String(uploadedFile);
        //        fileContentType = ProfilePicture.ContentType;



        //        string name = Path.GetFileNameWithoutExtension(fileName); //getting file name without extension  
        //        string myfile = name + "_" + DateTime.Now + ext; //appending the name with id  
        //                                                         // store the file inside ~/project folder(Img)  
        //        var path = Path.Combine(webRootPath + "\n" + contentRootPath , myfile);
        //        data.ProfilePicture = fileContent;
        //        data.Name = regsirtation.Name;
        //        data.Age = regsirtation.Age;
        //        data.Email = regsirtation.Email;
        //        data.Gender = regsirtation.Gender;
        //        data.Topic = regsirtation.Topic;
        //        data.Country = regsirtation.Country;
        //        data.Theme = regsirtation.Theme;
        //        data.OneLineProfile = regsirtation.OneLineProfile;
        //        data.Phone = regsirtation.Phone;
        //        data.Description = regsirtation.Description;
        //        data.IsApporved = false;
        //        _context.SpeakerRegistrations.Add(data);
        //        await _context.SaveChangesAsync();


        //    }

        //    return CreatedAtAction("GetSpeakerRegistration", new { id = regsirtation.Id }, regsirtation);
        //}

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
