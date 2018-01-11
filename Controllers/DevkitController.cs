using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DevkitApi.Model;
using DevkitApi.Services;
using Microsoft.Extensions.Logging;

namespace DevkitApi.Controllers
{

    
    [Produces("application/json")]
    [Route("api/Devkits")]
    public class DevkitController : Controller
    {
        private readonly DevkitContext _context;
        private IDevkitService _devkitService;
        private readonly ILogger _logger;

        public DevkitController(DevkitContext context, IDevkitService devkitService, ILogger<DevkitController> logger)
        {
            _context = context;
            _devkitService = devkitService;
            _logger = logger;
        }

        // GET: api/Devkits
        [HttpGet]
        public IEnumerable<Devkit> GetDevkits()
        {
            return _context.Devkits;
        }

        // GET: api/Devkits/5
        [HttpGet("{id}")]
        public  async Task<IActionResult>  GetDevkit([FromRoute] int id)
        {
           
             if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return  Ok(_devkitService.FindById(id));
        }
        
        
        

        // PUT: api/Devkits/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDevkit([FromRoute] int id, [FromBody] Devkit devkit)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != devkit.DevkitID)
            {
                return BadRequest();
            }

            _context.Entry(devkit).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DevkitExists(id))
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

        // POST: api/Devkits
        [HttpPost]
        public async Task<IActionResult> PostDevkit([FromBody] Devkit devkit)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Devkits.Add(devkit);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDevkit", new { id = devkit.DevkitID }, devkit);
        }

        // DELETE: api/Devkits/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDevkit([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var devkit = await _context.Devkits.SingleOrDefaultAsync(m => m.DevkitID == id);
            if (devkit == null)
            {
                return NotFound();
            }

            _context.Devkits.Remove(devkit);
            await _context.SaveChangesAsync();

            return Ok(devkit);
        }

        private bool DevkitExists(int id)
        {
            return _context.Devkits.Any(e => e.DevkitID == id);
        }
    }
}