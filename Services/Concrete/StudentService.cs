using BaseWebApp.Models;
using BaseWebApp.Services.Abstract;
using BaseWebApp.Utilities.EFCore;
using Microsoft.EntityFrameworkCore;

namespace BaseWebApp.Services.Concrete
{
    public class StudentService : IStudentService
    {
        private readonly MyDbContext _context;

        public StudentService(MyDbContext context)
        {
            _context = context;
        }
        public List<Student> GetStudents()
        {
            return _context.Students.ToList();
        }

        public Task<List<Student>> GetStudentsAsync()
        {
            return _context.Students.ToListAsync();
        }

        public Student GetStudentById(int id)
        {
            return _context.Students.FirstOrDefault(c => c.Id == id);
        }
    }
}
