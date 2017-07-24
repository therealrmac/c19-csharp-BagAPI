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
    public class ChildController : Controller
    {
        private BagAPIContext _context;
        public ChildController(BagAPIContext ctx)
        {
            _context = ctx;
        }

        // GET api/values
        [HttpGet]
        public IActionResult Get()
        {
            IQueryable<object> children = from child in _context.Child select child;

            if (children == null)
            {
                return NotFound();
            }

            return Ok(children);

        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody] Child child)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Child.Add(child);
            
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ChildExists(child.ChildId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("GetToy", new { id = child.ChildId }, child);
        }

    private bool ChildExists(int toyId)
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