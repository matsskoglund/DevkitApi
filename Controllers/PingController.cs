using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace DevkitApi.Controllers
{
    [Route("api/[controller]")]
    public class PingController : Controller
    {
       

        /// <summary>
        /// Returns a Pong 
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///  GET /api/Ping        
        /// </remarks>
        [HttpGet("")]
        public string Get()
        {
            return "Pong";
        }

        /// <summary>
        /// Returns a Pong 
        /// </summary>
        /// <param name="value"></param>
        /// <returns>Pong</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///  POST /api/Ping        
        /// </remarks>
        [HttpPost]        
        [ProducesResponseType(typeof(String), 200)]
        public IActionResult Post([FromBody] string value)
        {
            return Ok("Pong");
        }

        /// <summary>
        /// Returns a Pong 
        /// </summary>
        /// <param name="value"></param>
        /// <returns>Pong</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///  PUT /api/Ping        
        /// </remarks>
        [HttpPut("")]
        [ProducesResponseType(typeof(String), 200)]
        public IActionResult Put([FromBody] string value)
        {
            return Ok("Pong");
        }

        /// <summary>
        /// Returns a Pong 
        /// </summary>
        /// <param name="value"></param>
        /// <returns>Pong</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///  DELETE /api/Ping        
        /// </remarks>
        [HttpDelete("")]
        [ProducesResponseType(typeof(String), 200)]
        public IActionResult Delete(string value)
        {
            return Ok("Pong");
        }
    }
}
