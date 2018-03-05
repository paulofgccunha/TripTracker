using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TripTracker.BackService.Models;
using TripTracker.BackService.Data;
using Microsoft.EntityFrameworkCore;

namespace TripTracker.BackService.Controllers
{
    [Route("api/[controller]")]
    public class TripsController : Controller
    {
        // private Repository _repository;
        private TripContext _context;

        // first approach: in memory storage of data
        // public TripsController(Repository repository)
        // {
        //     _repository = repository;
        // }

        public TripsController(TripContext context)
        {
            _context = context;
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        // GET api/Trips
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var trips = await _context.Trips.ToListAsync();
            return Ok(trips);
            // return  _repository.Get();
        }

        // GET api/Trips/5
        [HttpGet("{id}")]
        public Trip Get(int id)
        {
            return _context.Trips.Find(id);
            // return _repository.Get(id);
        }

        // POST api/Trips
        [HttpPost]
        public IActionResult Post([FromBody]Trip value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _context.Trips.Add(value);
            _context.SaveChanges();

            return Ok();

            // _repository.Add(value);
        }

        // PUT api/Trips/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody]Trip value)
        {
            if (!_context.Trips.Any(t => t.Id == id))
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _context.Trips.Update(value);
            await _context.SaveChangesAsync();

            return Ok();

            // _repository.Update(value);
        }

        // DELETE api/Trips/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var myTrip = _context.Trips.Find(id);

            if (myTrip == null)
            {
                return NotFound();
            }

            _context.Trips.Remove(myTrip);
            _context.SaveChanges();

            return NoContent();

            // _repository.Remove(id);
        }
    }
}
