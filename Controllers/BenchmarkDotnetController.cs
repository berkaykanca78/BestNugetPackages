using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Loggers;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Validators;
using BestNugetPackages.Utilities.BenchmarkDotnet;
using Microsoft.AspNetCore.Mvc;

namespace BaseProject.Controllers;

[ApiController]
[Route("[controller]")]
public class BenchmarkDotnetController : ControllerBase
{
    [HttpGet(Name = nameof(BenchmarkDotnetTest))]
    public string BenchmarkDotnetTest()
    {
        var config = new ManualConfig()
        .WithOptions(ConfigOptions.DisableOptimizationsValidator)
        .AddValidator(JitOptimizationsValidator.DontFailOnError)
        .AddLogger(ConsoleLogger.Default)
        .AddColumnProvider(DefaultColumnProviders.Instance);

        var result = BenchmarkRunner.Run<CollectionRun>(config);

        return "Benchmark Testi Sonuçları\n" + result.LogFilePath;
    }
}
