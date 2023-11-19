using BenchmarkDotNet.Attributes;

namespace BestNugetPackages.Utilities.BenchmarkDotnet;

[MemoryDiagnoser]
public class CollectionRun
{
    [Benchmark]
    public void List()
    {
        var list = new List<string>();
        for (int i = 0; i < 10; i++)
        {
            list.Add("Mr.KOÇ Benchmark");
        }
    }

    [Benchmark]
    public void ListWithCapacity()
    {
        var list = new List<string>(10);
        for (int i = 0; i < 10; i++)
        {
            list.Add("Mr.KOÇ Benchmark");
        }
    }
}
