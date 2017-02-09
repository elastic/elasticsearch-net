using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Jobs;

namespace Tests.Framework.Benchmarks
{
	public class BenchmarkConfig : ManualConfig
	{
		public BenchmarkConfig()
		{
			Add(AsciiDocExporter.Default);
		}
	}

	public class FastRunConfig : ManualConfig
	{
		public FastRunConfig()
		{
			Add(Job.Dry.With(Runtime.Core).With(Jit.RyuJit));
			Add(Job.Dry.With(Runtime.Clr).With(Jit.RyuJit));
			Add(Job.Dry.With(Runtime.Clr).With(Jit.LegacyJit));
			Add(AsciiDocExporter.Default);
		}
	}
}
