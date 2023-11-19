using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace BaseProject.Controllers;

[ApiController]
[Route("[controller]")]
public class SerilogController : ControllerBase
{
    [HttpGet(Name = nameof(SerilogTest))]
    public async Task<string> SerilogTest()
    {
        Log.Information("Serilog Test Metpdu Çalıştırılmıştır.");
        Log.CloseAndFlush();
        return "logs/app.log klasörünün içerisine ilgili logunuz eklenmiştir.";
    }
}
