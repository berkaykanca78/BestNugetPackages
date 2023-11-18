using BaseProject.Enums;
using Microsoft.AspNetCore.Mvc;

namespace BaseProject.Controllers;

[ApiController]
[Route("[controller]")]
public class SmartEnumController : ControllerBase
{
    [HttpGet(Name = nameof(SmartEnumTest))]
    public List<string> SmartEnumTest()
    {
        return Pizza.List.Select(x => x.Name + " ~ " + Enum.GetName(typeof(PizzaType), x.Type)).ToList();
    }
}
