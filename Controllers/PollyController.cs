using Microsoft.AspNetCore.Mvc;
using Polly;

namespace BaseProject.Controllers;

[ApiController]
[Route("[controller]")]
public class PollyController : ControllerBase
{
    private readonly HttpClient _httpClient;
    public PollyController(HttpClient client)
    {
        _httpClient = client;
        _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        _httpClient.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/");
    }

    [HttpGet("Retry01")]
    public async Task<ActionResult<string>> Retry01()
    {
        var response = Policy.HandleResult<HttpResponseMessage>(x => x.StatusCode != System.Net.HttpStatusCode.OK)
        .RetryAsync(2, (response, retryCount) => Console.WriteLine($"Deneme Adedi : {retryCount}"))
        .ExecuteAsync(async () =>
        {
            return await _httpClient.GetAsync("*");
        });


        var data = await response.Result.Content.ReadAsStringAsync();
        return Ok(data);
    }

    [HttpGet("WaitAndRetry01")]
    public async Task<ActionResult<string>> WaitAndRetry01()
    {
        var response = Policy.HandleResult<HttpResponseMessage>(x => x.StatusCode != System.Net.HttpStatusCode.OK)
            .WaitAndRetryAsync(2, retryAttempt => TimeSpan.FromSeconds(3), (response, timeSpan, retryCount, context) =>
            {
                Console.WriteLine($"Deneme Adedi : {retryCount}, Beklenen Zaman : {timeSpan} ");

            }).ExecuteAsync(async () =>
            {
                return await _httpClient.GetAsync("*");

            });

        var data = await response.Result.Content.ReadAsStringAsync();
        return Ok(data);
    }
}
