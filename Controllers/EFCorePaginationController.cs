using BaseWebApp.Models;
using BaseWebApp.Utilities.EFCore;
using EntityFrameworkCorePagination.Nuget.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace BaseProject.Controllers;

[ApiController]
[Route("[controller]")]
public class EFCorePaginationController : ControllerBase
{
    private readonly MyDbContext _context;

    public EFCorePaginationController(MyDbContext context)
    {
        _context = context;
    }

    [HttpGet(Name = nameof(EFCorePaginationTest))]
    public async Task<PaginationResult<Student>> EFCorePaginationTest()
    {
        return await _context.Students.ToPagedListAsync(1, 5);
    }
}
