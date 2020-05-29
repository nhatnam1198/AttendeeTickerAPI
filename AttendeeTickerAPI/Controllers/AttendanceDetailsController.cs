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
        [HttpPut("Event/{id}")]
        public async Task<IActionResult> PutAttendanceDetails(int id, AttendanceDetailsDTO attendanceDetails)
        {
            if (id != attendanceDetails.EventID)
            {
                return BadRequest();
            }

            var attandance = from s in _context.Attendance
                             where s.SubjectClassID == attendanceDetails.SubjectClassID
                             select new
                             {
                                 AttendanceID = s.AttendanceID,
                                 StudentID = s.StudentID
                             };
            foreach(var s in attandance)
            {
                var attendedStudent = _context.AttendanceDetails.FirstOrDefault(x => x.AttendanceID == s.AttendanceID && x.EventID == id );
                //var  attendedStudent = new AttendanceDetails()
                //{

                //    AttendanceID = s.AttendanceID,
                //    EventID = attendanceDetails.EventID,
                //    IsAttended = false
                //};
                attendedStudent.IsAttended = false;
                foreach (var p in attendanceDetails.StudentList)
                {
                    if (s.StudentID == p.StudentID)
                    {
                        attendedStudent.IsAttended = true;
                    }
                }
            }
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
            List<AttendanceDetails> attendedStudentList = new List<AttendanceDetails>();
            
            var attendance = from s in _context.Attendance
                             where s.SubjectClassID == attendanceDetailsDTO.SubjectClassID
                             select new
                             {
                                 AttendanceID = s.AttendanceID,
                                 StudentID = s.StudentID,
                             };
            var eventID = attendanceDetailsDTO.EventID;
            // update event status
            (from x in _context.Event where x.EventID == eventID select x).ToList().ForEach(x => x.Status = 1);
            foreach (var s in attendance)
            {
                var attendedStudent = new AttendanceDetails()
                {
                    AttendanceID = s.AttendanceID,
                    EventID = eventID,
                    IsAttended = false
                };
                foreach (var a in attendanceDetailsDTO.StudentList)
                {
                    
                    if (s.StudentID == a.StudentID)
                    {
                        attendedStudent.IsAttended = true;
                    }
                    
                }
                _context.AttendanceDetails.Add(attendedStudent);
            }
            
            try
            {
                await _context.SaveChangesAsync();
            }catch(Exception ex)
            {

            }
            
            return CreatedAtAction("CreateAttendanceDetails", new { id = attendanceDetailsDTO.EventID }, attendanceDetailsDTO.EventID);
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
