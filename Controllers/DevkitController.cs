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
using Microsoft.AspNetCore.Cors;

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


        // GET: api/Devkits/5
        [HttpGet("tools/{id}")]
        [Route("tools/{id}")]
        public async Task<IActionResult> GetToolsForKit([FromRoute] int id)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(_devkitService.GetToolsForDevkit(id));
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

        // PUT: api/Devkits/5
        [HttpPut("tools/{id}")]
        [Route("tools/{id}")]
        public async Task<IActionResult> PutDevkitTools([FromRoute] int id, [FromBody]  DevkitTools devkittool)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Entry(devkittool).State = EntityState.Modified;

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

      // POST: api/Devkits/details
       
        [HttpPost("tools")]
        [Route("tools")]
        public async Task<IActionResult> PostDevkitTool([FromBody] DevkitTools devkittool)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            
            _context.DevkitTools.Add(devkittool);
            await _context.SaveChangesAsync();

            //return CreatedAtAction("PostDevkitTool", new { id = devkittool.DevkitToolsID }, devkittool);
            return Ok();
        }
        
        // DELETE: api/Devkits/5
        // Delete the Devkit
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

        // DELETE: api/tools/5
        // Delete the tools from the devkit
        [HttpDelete("tools/{id}")]
        [Route("tools/{id}")]
        public async Task<IActionResult> DeleteDevkitTools([FromRoute] int id)
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


            IEnumerable<DevkitTools> allTools = _devkitService.GetDevkitToolsForDevkit(id);
            _context.DevkitTools.RemoveRange(allTools);
            //_context.Devkits.Remove(devkit);
            await _context.SaveChangesAsync();
            allTools = _context.DevkitTools;
            return Ok(devkit);
        }


        private bool DevkitExists(int id)
        {
            return _context.Devkits.Any(e => e.DevkitID == id);
        }
    }
}