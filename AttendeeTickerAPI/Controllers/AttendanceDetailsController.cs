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
    public class AttendanceDetailsController : ControllerBase
    {
        private readonly AttendeeTickerDbContext _context;

        public AttendanceDetailsController(AttendeeTickerDbContext context)
        {
            _context = context;
        }

        // GET: api/AttendanceDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AttendanceDetails>>> GetAttendanceDetails()
        {
            return await _context.AttendanceDetails.ToListAsync();
        }

        // GET: api/AttendanceDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AttendanceDetails>> GetAttendanceDetails(int id)
        {
            var attendanceDetails = await _context.AttendanceDetails.FindAsync(id);

            if (attendanceDetails == null)
            {
                return NotFound();
            }

            return attendanceDetails;
        }

        // PUT: api/AttendanceDetails/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAttendanceDetails(int id, AttendanceDetails attendanceDetails)
        {
            if (id != attendanceDetails.AttendanceID)
            {
                return BadRequest();
            }

            _context.Entry(attendanceDetails).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AttendanceDetailsExists(id))
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

        // POST: api/AttendanceDetails
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<AttendanceDetails>> PostAttendanceDetails(AttendanceDetails attendanceDetails)
        {
            _context.AttendanceDetails.Add(attendanceDetails);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAttendanceDetails", new { id = attendanceDetails.AttendanceID }, attendanceDetails);
        }
        [HttpPost("List")]
        public async Task<ActionResult<AttendanceDetails>> PostAttendanceDetailList(AttendanceDetailsDTO attendanceDetailsDTO)
        {
            var attandanceDetails = from s in _context.Attendance
                                    where s.SubjectClassID == attendanceDetailsDTO.SubjectClassID
                                    select new AttendanceDetails()
                                    {
                                        AttendanceID = s.AttendanceID,
                                        IsAttended = true
                                    };
            foreach (var attendedStudent in attandanceDetails)
            {
                _context.AttendanceDetails.Add(attendedStudent);
            }
            await _context.SaveChangesAsync();
            return Ok();
        }
        
    // DELETE: api/AttendanceDetails/5
    [HttpDelete("{id}")]
        public async Task<ActionResult<AttendanceDetails>> DeleteAttendanceDetails(int id)
        {
            var attendanceDetails = await _context.AttendanceDetails.FindAsync(id);
            if (attendanceDetails == null)
            {
                return NotFound();
            }

            _context.AttendanceDetails.Remove(attendanceDetails);
            await _context.SaveChangesAsync();

            return attendanceDetails;
        }

        private bool AttendanceDetailsExists(int id)
        {
            return _context.AttendanceDetails.Any(e => e.AttendanceID == id);
        }
    }
}
