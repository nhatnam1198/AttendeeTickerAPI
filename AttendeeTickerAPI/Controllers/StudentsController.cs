using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AttendeeTickerAPI.Models;
using AttendeeTickerAPI.BUS;
using AttendeeTickerAPI.DAL;
using System.Net;

namespace AttendeeTickerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly AttendeeTickerDbContext _context;

        public StudentsController(AttendeeTickerDbContext context)
        {
            _context = context;
        }

        // GET: api/Students
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudent()
        {
            StudentBUS studentBUS = new StudentBUS(_context);
            var students = await studentBUS.GetStudent();
            return students;
        }

        // GET: api/Students/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(string id)
        {
            StudentBUS studentBUS = new StudentBUS(_context);
            var result = await studentBUS.GetStudentById(id);
            if(result.ErrorCode == HttpStatusCode.NotFound.ToString())
            {
                return NotFound();
            }
            return result.item;
        }
        
        [HttpGet("Event/{eventID}")]
        public async Task<ActionResult<List<Student>>> GetAttendedStudentByEventId(int eventID)
        {
            var obj = from s in _context.AttendanceDetails
                      join q in _context.Attendance on s.AttendanceID equals q.AttendanceID
                      join g in _context.Student on q.StudentID equals g.StudentID
                      where s.EventID == eventID
                      select new
                      {
                          s.IsAttended,
                          g.StudentID,
                          g.StudentLastName,
                          g.StudentFirstName
                      };
            List<Student> studentList = new List<Student>();
            foreach(var item in obj)
            {
                if(item.IsAttended == true)
                {
                    studentList.Add(new Student()
                    {
                        StudentID = item.StudentID,
                        StudentFirstName = item.StudentFirstName,
                        StudentLastName = item.StudentLastName,
                        IsDihoc = true 
                    });
                }
                else 
                {
                    studentList.Add(new Student()
                    {
                        StudentID = item.StudentID,
                        StudentFirstName = item.StudentFirstName,
                        StudentLastName = item.StudentLastName,
                        IsDihoc = false
                    });
                }
            }
            return Ok(studentList);
        }

        // PUT: api/Students/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudent(string id, Student student)
        {
            if (id != student.StudentID)
            {
                return BadRequest();
            }

            _context.Entry(student).State = EntityState.Modified;
            StudentBUS studentBUS = new StudentBUS(_context);
            var result = await studentBUS.PutStudent(id, student);
            if(result.ErrorCode == HttpStatusCode.NotFound.ToString())
            {
                return NotFound();
            }
            return NoContent();
        }

        // POST: api/Students
        [HttpPost]
        public async Task<ActionResult<Student>> PostStudent(Student student)
        {
            StudentBUS studentBUS = new StudentBUS(_context);
            var result = await studentBUS.CreateStudent(student);
            if(result.ErrorCode == HttpStatusCode.Conflict.ToString())
            {
                return NotFound();
            }
            return CreatedAtAction("GetStudent", new { id = student.StudentID }, student);
        }

        // DELETE: api/Students/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Student>> DeleteStudent(string id)
        {
            StudentBUS studentBUS = new StudentBUS(_context);
            var result = await studentBUS.DeleteStudent(id);
            if(result.ErrorCode == HttpStatusCode.NotFound.ToString())
            {
                return NotFound();
            }
            return result.item;
        }

        private bool StudentExists(string id)
        {
            return _context.Student.Any(e => e.StudentID == id);
        }
    }
}
