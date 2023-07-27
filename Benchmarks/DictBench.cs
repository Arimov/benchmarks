using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System;

namespace Benchmarks;

public class DictBench
{
    private readonly List<int> _source = new List<int>();
    private readonly List<int> _used = new List<int>();

    public DictBench()
    {
        var random = new Random();
        for(var i = 0; i < 100000; i++)
        {
            if (!_source.Contains(i))
                _source.Add(random.Next(0, 100000));
        }
        
        for(var i = 0; i < 50000; i++)
        {
            if (!_used.Contains(i))
                _source.Add(random.Next(0, 100000));
        }
    }
        
    [Benchmark]
    public void Except()
    {
        var dict = _used.ToDictionary(x => x, x => 0);
        foreach(var i in _source.Except(_used))
        {
            dict.Add(i, 0);
        }
    }
    
    [Benchmark]
    public void TryAdd()
    {
        var dict = _used.ToDictionary(x => x, x => 0);
        foreach(var i in _source)
        {
            dict.TryAdd(i, 0);
        }
    }
}