using BaseProject.Models;
using Bogus;
using Microsoft.AspNetCore.Mvc;

namespace BaseProject.Controllers;

[ApiController]
[Route("[controller]")]
public class BogusController : ControllerBase
{
    [HttpGet(Name = nameof(BogusTest))]
    public List<Customer> BogusTest()
    {
        var customerAddressTr = new Faker<CustomerAddress>("tr")
                .RuleFor(c => c.Id, (f, c) => f.IndexFaker + 1)
                .RuleFor(c => c.Address, (f, c) => f.Address.StreetAddress(true));

        var customersTr = new Faker<Customer>("tr")
                            .RuleFor(c => c.Id, (f, c) => f.IndexFaker + 1)
                            .RuleFor(c => c.Name, (f, c) => f.Name.FullName())
                            .RuleFor(c => c.Email, (f, c) => f.Internet.Email())
                            .RuleFor(c => c.CustomerAddresses, f => customerAddressTr.Generate(f.Random.Int(min: 1, max: 5)));

        return customersTr.Generate(3);
    }
}
