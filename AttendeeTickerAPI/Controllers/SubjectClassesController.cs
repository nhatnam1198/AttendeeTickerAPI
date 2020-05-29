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
    public class SubjectClassesController : ControllerBase
    {
        private readonly AttendeeTickerDbContext _context;

        public SubjectClassesController(AttendeeTickerDbContext context)
        {
            _context = context;
        }

        // GET: api/SubjectClasses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubjectClass>>> GetSubjectClass()
        {
            var result = await _context.SubjectClass.ToListAsync();
            return result;
        }

        // GET: api/SubjectClasses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SubjectClass>> GetSubjectClass(int id)
        {
            var subjectClass = await _context.SubjectClass.FindAsync(id);

            if (subjectClass == null)
            {
                return NotFound();
            }

            return subjectClass;
        }

        // PUT: api/SubjectClasses/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSubjectClass(int id, SubjectClass subjectClass)
        {
            if (id != subjectClass.SubjectClassID)
            {
                return BadRequest();
            }

            _context.Entry(subjectClass).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubjectClassExists(id))
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

        // POST: api/SubjectClasses
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<SubjectClass>> PostSubjectClass(SubjectClass subjectClass)
        {
            _context.SubjectClass.Add(subjectClass);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSubjectClass", new { id = subjectClass.SubjectClassID }, subjectClass);
        }

        // DELETE: api/SubjectClasses/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<SubjectClass>> DeleteSubjectClass(int id)
        {
            var subjectClass = await _context.SubjectClass.FindAsync(id);
            if (subjectClass == null)
            {
                return NotFound();
            }

            _context.SubjectClass.Remove(subjectClass);
            await _context.SaveChangesAsync();

            return subjectClass;
        }

        private bool SubjectClassExists(int id)
        {
            return _context.SubjectClass.Any(e => e.SubjectClassID == id);
        }
    }
}
