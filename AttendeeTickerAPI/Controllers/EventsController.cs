using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AttendeeTickerAPI.DAL;
using AttendeeTickerAPI.Models;

namespace AttendeeTickerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly AttendeeTickerDbContext _context;

        public EventsController(AttendeeTickerDbContext context)
        {
            _context = context;
        }

        // GET: api/Events
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Event>>> GetEvent()
        {
            return await _context.Event.ToListAsync();
        }

        // GET: api/Events/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Event>> GetEvent(int id)
        {
            var @event = await _context.Event.FindAsync(id);

            if (@event == null)
            {
                return NotFound();
            }

            return @event;
        }

        // PUT: api/Events/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEvent(int id, Event @event)
        {
            if (id != @event.EventID)
            {
                return BadRequest();
            }

            _context.Entry(@event).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventExists(id))
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
        [HttpGet("Details")]
        public ActionResult<IList<object>> GetEventDetails()
        {
            //var @event = await _context.Event.FindAsync(id);



            var obj = from s in _context.SubjectClass
                      join q in _context.Event on s.SubjectClassID equals q.SubjectClassID
                      join g in _context.Shift on q.ShiftID equals g.ShiftID                  
                      select new
                      {
                          s.SubjectClassName,
                          q.DateTime,
                          g.ShiftName,
                          q.EventID

                      };
            List<object> result = new List<object>();
            foreach (var item in obj)
            {
                result.Add(item);
            }
           
       return result;
        }
        [HttpGet("Students/{id}")]
        public ActionResult<IList<object>> GetStudents(int id)
        {
            //var @event = await _context.Event.FindAsync(id);



            var obj = from s in _context.Event
                      join q in _context.SubjectClass on s.SubjectClassID equals q.SubjectClassID
                      join g in _context.Attendance on q.SubjectClassID equals g.SubjectClassID
                      join m in _context.Student on g.StudentID equals m.StudentID
                      where s.EventID == id
                      select new
                      {                   
                          m.StudentID,
                          HoTen=m.StudentFirstName+m.StudentLastName,
                          m.ClassName

                      };
            List<object> result = new List<object>();
            foreach (var item in obj)
            {
                result.Add(item);
            }

            return result;
        }

        // POST: api/Events
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Event>> PostEvent(Event @event)
        {
            _context.Event.Add(@event);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEvent", new { id = @event.EventID }, @event);
        }

        // DELETE: api/Events/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Event>> DeleteEvent(int id)
        {
            var @event = await _context.Event.FindAsync(id);
            if (@event == null)
            {
                return NotFound();
            }

            _context.Event.Remove(@event);
            await _context.SaveChangesAsync();

            return @event;
        }

        private bool EventExists(int id)
        {
            return _context.Event.Any(e => e.EventID == id);
        }
    }
}
