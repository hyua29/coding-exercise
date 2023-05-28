namespace DotnetBenchmarkSpike;

using System.Security.Cryptography;
using BenchmarkDotNet.Attributes;

[SimpleJob]
[RPlotExporter]
public class Md5VsSha256
{
    private byte[] data;
    private readonly MD5 md5 = MD5.Create();

    [Params(1000, 10000)] public int N;
    private readonly SHA256 sha256 = SHA256.Create();

    [GlobalSetup]
    public void Setup()
    {
        data = new byte[N];
        new Random(42).NextBytes(data);
    }

    [Benchmark]
    public byte[] Sha256()
    {
        return sha256.ComputeHash(data);
    }

    [Benchmark]
    public byte[] Md5()
    {
        return md5.ComputeHash(data);
    }
}