using BaseWebApp.Models;
using BaseWebApp.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace BaseProject.Controllers;

[ApiController]
[Route("[controller]")]
public class FluentValidation : ControllerBase
{
    private readonly IProductService _productService;

    public FluentValidation(IProductService productService)
    {
        _productService = productService;
    }

    [HttpPost(Name = nameof(FluentValidationTest))]
    public List<string> FluentValidationTest(Product product)
    {
        List<string> messages = new();
        if (ModelState.IsValid)
        {
            _productService.Add(product);
            messages.Add("Herhangi bir hata ile karşılaşılmamıştır.");
        }
        else
        {
            foreach (var modelItem in ModelState.Values)
            {
                if (modelItem.Errors.Count > 0)
                {
                    messages.Add("Hata Mesajı: " + modelItem.Errors.FirstOrDefault().ErrorMessage);
                }
            }
        }
        return messages;
    }
}
