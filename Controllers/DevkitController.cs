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
    //[Route("v1/[controller]")]
    //[Route("api/Devkits")]        
    [Route("api/[controller]")]
    public class DevkitsController : Controller
    {
        private readonly DevkitContext _context;
        private IDevkitService _devkitService;
        private readonly ILogger _logger;

        public DevkitsController(DevkitContext context, IDevkitService devkitService, ILogger<DevkitsController> logger)
        {
            _context = context;
            _devkitService = devkitService;
            _logger = logger;
        }

        /// <summary>
        /// Returns all Devkits 
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///  GET /api/Devkits        
        /// </remarks>
        /// <returns>Returns all Devkits</returns>
        /// <response code="200">Returns all Devkits</response>
        [HttpGet("")]
        [ProducesResponseType(typeof(Devkit), 200)]
        public IEnumerable<Devkit> GetDevkits()
        {
            return _devkitService.GetDevkits();
           
        }

        /// <summary>
        /// Returns a Devkit with the specificed id 
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///  GET /api/Devkits/5        
        /// </remarks>
        /// <returns>Returns a Devkit with the specificed id if it exists. If it does not exist it returns empty. If id is not an int it returns Bad request.</returns>
        /// <response code="200">Returns a Devkit with the specificed id</response>
        /// <response code="400">Returns bad request if the id is not an int.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(String), 200)]
        [ProducesResponseType(typeof(String), 400)]
        public  IActionResult  GetDevkit([FromRoute] int id)
        {
           
             if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return  Ok(_devkitService.FindById(id));
        }



        /// <summary>
        /// Updates the Devkit with the specificed id.
        /// Returns no content
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///  PUT /api/Devkits/1
        /// </remarks>
        /// <returns>Returns empty if the Devkit with the specificed id exist and is updated. If it does not exist it returns Bad request response. 
        /// If id is not an int or if the id for the Devkit is different from the id it returns Bad request.</returns>
        /// <response code="200">Returns a the tools for the Devkit with the specificed id</response>
        /// <response code="400">Returns bad request if the id is not an int or if the id does not match the Devkit id in the body.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(String), 200)]
        [ProducesResponseType(typeof(String), 400)]
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


        /// <summary>
        /// Create a new Devkit
        /// Returns the Devkit 
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///  POST /api/Devkits/
        /// </remarks>
        /// <returns>Returns the Devkit. If the Devkit in the request is malfomed it returns Bad request response. </returns>        
        /// <response code="200">The Devkit was created and the Devkit returned</response>
        /// <response code="400">Returns bad request if the Devkit is malfomed.</response>

        [HttpPost]
        public async Task<IActionResult> PostDevkit([FromBody] Devkit devkit)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Devkits.Add(devkit);
            await _context.SaveChangesAsync();

            return CreatedAtAction("PostDevkit", new { id = devkit.DevkitID }, devkit);
        }

      // POST: api/Devkits/details
       
        [HttpPost("tools")]
        //[Route("tools")]
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
       // [Route("tools/{id}")]
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

        /// <summary>
        /// Returns the tools for a Devkit with the specificed id 
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///  GET /api/Devkits/tools/5        
        /// </remarks>
        /// <returns>Returns the tools for a Devkit with the specificed id if it exist. If it does not exist it returns empty. If id is not an int it returns Bad request.</returns>
        /// <response code="200">Returns a the tools for the Devkit with the specificed id</response>
        /// <response code="400">Returns bad request if the id is not an int.</response>        
        [HttpGet("tools/{id}")]
        [ProducesResponseType(typeof(String), 200)]
        [ProducesResponseType(typeof(String), 400)]
        public IActionResult GetToolsForKit([FromRoute] int id)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(_devkitService.GetToolsForDevkit(id));
        }

        // PUT: api/Devkits/5
        [HttpPut("tools/{id}")]
        // [Route("tools/{id}")]
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


    }
}