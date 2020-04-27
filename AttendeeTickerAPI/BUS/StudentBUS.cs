using AttendeeTickerAPI.Common;
using AttendeeTickerAPI.DAL;
using AttendeeTickerAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace AttendeeTickerAPI.BUS
{
    public class StudentBUS
    {
        private readonly AttendeeTickerDbContext _context;

        public StudentBUS(AttendeeTickerDbContext context)
        {
            _context = context;
        }
        public async Task<List<Student>> GetStudent()
        {
            return await _context.Student.ToListAsync();
        }
        public async Task<ReturnResult<Student>> GetStudentById(string id)
        {
            ReturnResult<Student> result = new ReturnResult<Student>();
            var student = await _context.Student.FindAsync(id);
            result.item = student;
            if (student == null)
            {
                result.ErrorCode = HttpStatusCode.NotFound.ToString();
                result.ErrorMessage = "Not found";
            }
            return result;
        }
        public async Task<ReturnResult<Student>> PutStudent(string id, Student student)
        {
            _context.Entry(student).State = EntityState.Modified;
            ReturnResult<Student> result = new ReturnResult<Student>();
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(id))
                {
                    result.ErrorCode = HttpStatusCode.NotFound.ToString();
                    result.ErrorMessage = "Stundent not exists";
                }
                else
                {
                    throw;
                }
            }

            result.ErrorCode = HttpStatusCode.NotFound.ToString();
            result.ErrorMessage = "No Content";
            return result;
        }

        public async Task<ReturnResult<Student>> CreateStudent(Student student)
        {
            _context.Student.Add(student);
            ReturnResult<Student> result = new ReturnResult<Student>();
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (StudentExists(student.StudentID))
                {
                    result.ErrorCode = HttpStatusCode.Conflict.ToString();
                    result.ErrorMessage = "Already have this student";
                }
                else
                {
                    throw;
                }
            }

            return result;
        }

        // DELETE: api/Students/5
        public async Task<ReturnResult<Student>> DeleteStudent(string id)
        {
            var student = await _context.Student.FindAsync(id);
            ReturnResult<Student> result = new ReturnResult<Student>();

            if (student == null)
            {
                result.ErrorCode = HttpStatusCode.NotFound.ToString();
                result.ErrorMessage = "Not found";
            }
            result.item = student;
            _context.Student.Remove(student);
            await _context.SaveChangesAsync();

            return result;
        }

        private bool StudentExists(string id)
        {
            return _context.Student.Any(e => e.StudentID == id);
        }
    }
}
