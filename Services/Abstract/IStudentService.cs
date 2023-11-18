using BaseWebApp.Models;

namespace BaseWebApp.Services.Abstract
{
    public interface IStudentService
    {
        List<Student> GetStudents();
        Task<List<Student>> GetStudentsAsync();
        Student GetStudentById(int id);
    }
}
