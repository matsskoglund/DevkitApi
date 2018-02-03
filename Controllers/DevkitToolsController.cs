using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DevkitApi.Model;
using DevkitApi.Services;
using Microsoft.AspNetCore.Cors;

namespace DevkitApi.Controllers
{
    [Produces("application/json")]
    [Route("api/DevkitTools")]
    [EnableCors("AllowAll")]
    public class DevkitToolsController : Controller
    {
        private readonly DevkitContext _context;

        public DevkitToolsController(DevkitContext context)
        {
            _context = context;
        }

        // GET: api/DevkitTools
        [HttpGet]
        public IEnumerable<DevkitTools> GetDevkitTools()
        {
            return _context.DevkitTools;
        }

        // GET: api/DevkitTools/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDevkitTools([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var devkitTools = await _context.DevkitTools.SingleOrDefaultAsync(m => m.DevkitToolsID == id);

            if (devkitTools == null)
            {
                return NotFound();
            }

            return Ok(devkitTools);
        }

        // PUT: api/DevkitTools/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDevkitTools([FromRoute] int id, [FromBody] DevkitTools devkitTools)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != devkitTools.DevkitToolsID)
            {
                return BadRequest();
            }

            _context.Entry(devkitTools).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DevkitToolsExists(id))
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

        // POST: api/DevkitTools
        [HttpPost]
        public async Task<IActionResult> PostDevkitTools([FromBody] DevkitTools devkitTools)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.DevkitTools.Add(devkitTools);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDevkitTools", new { id = devkitTools.DevkitToolsID }, devkitTools);
        }

        // DELETE: api/DevkitTools/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDevkitTools([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var devkitTools = await _context.DevkitTools.SingleOrDefaultAsync(m => m.DevkitToolsID == id);
            if (devkitTools == null)
            {
                return NotFound();
            }

            _context.DevkitTools.Remove(devkitTools);
            await _context.SaveChangesAsync();

            return Ok(devkitTools);
        }

        private bool DevkitToolsExists(int id)
        {
            return _context.DevkitTools.Any(e => e.DevkitToolsID == id);
        }
    }
}