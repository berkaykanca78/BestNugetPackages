using BaseWebApp.Models;
using BaseWebApp.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace BaseProject.Controllers;

[ApiController]
[Route("[controller]")]
public class EFCoreController : ControllerBase
{
    private readonly IStudentService _studentService;

    public EFCoreController(IStudentService studentService)
    {
        _studentService = studentService;
    }

    [HttpGet(Name = nameof(EFCoreTest))]
    public async Task<List<Student>> EFCoreTest()
    {
        return await _studentService.GetStudentsAsync();
    }
}
