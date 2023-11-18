using AutoMapper;
using BaseWebApp.Models;
using BaseWebApp.Services.Abstract;
using BaseWebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BaseProject.Controllers;

[ApiController]
[Route("[controller]")]
public class AutoMapperController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IStudentService _studentService;
    public AutoMapperController(IMapper mapper, IStudentService studentService)
    {
        _mapper = mapper;
        _studentService = studentService;
    }

    [HttpGet(Name = nameof(AutoMapperTest))]
    public List<StudentDto> AutoMapperTest()
    {
        List<Student> students = _studentService.GetStudents();

        return _mapper.Map<List<StudentDto>>(students);
    }
}
