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
    [Route("api/Tools")]
    [EnableCors("AllowAll")]
    public class ToolController : Controller
    {
        private readonly DevkitContext _context;

        public ToolController(DevkitContext context)
        {
            _context = context;
        }

        // GET: api/Tools
        [HttpGet]
        public IEnumerable<Tool> GetTools()
        {
            return _context.Tools.OrderBy(t => t.Name);
        }

        // GET: api/Tools/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTool([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tool = await _context.Tools.SingleOrDefaultAsync(m => m.ToolID == id);

            if (tool == null)
            {
                return NotFound();
            }

            return Ok(tool);
        }

        // PUT: api/Tools/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTool([FromRoute] int id, [FromBody] Tool tool)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tool.ToolID)
            {
                return BadRequest();
            }

            _context.Entry(tool).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ToolExists(id))
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

        // POST: api/Tools
        [HttpPost]
        [EnableCors("AllowAll")]
        public async Task<IActionResult> PostTool([FromBody] Tool tool)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Tools.Add(tool);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTool", new { id = tool.ToolID }, tool);
        }

        // DELETE: api/Tools/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTool([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tool = await _context.Tools.SingleOrDefaultAsync(m => m.ToolID == id);
            if (tool == null)
            {
                return NotFound();
            }

            _context.Tools.Remove(tool);
            await _context.SaveChangesAsync();

            return Ok(tool);
        }

        private bool ToolExists(int id)
        {
            return _context.Tools.Any(e => e.ToolID == id);
        }
    }
}