using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BagAPI.Data;
using BagAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BagAPI.Controllers
{
    [Route("api/[controller]")]
    public class ToyController : Controller
    {
        private BagAPIContext _context;
        public ToyController(BagAPIContext ctx)
        {
            _context= ctx;
        }
        // GET api/values
        [HttpGet]
        public IActionResult Get()
        {
            IQueryable<object> toys = from toy in _context.Toy select toy;

            if (toys == null)
            {
                return NotFound();
            }

            return Ok(toys);

        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public IActionResult Post([FromBody] Toy toy)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Toy.Add(toy);
            
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ToyExists(toy.ToyId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("GetToy", new { id = toy.ToyId }, toy);
        }

    private bool ToyExists(int toyId)
    {
      return _context.Toy.Count(e => e.ToyId == toyId) > 0;
    }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
