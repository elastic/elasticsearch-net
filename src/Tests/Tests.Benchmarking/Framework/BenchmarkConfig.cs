using System;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Jobs;

namespace Tests.Benchmarking.Framework
{
	public class BenchmarkConfigAttribute : Attribute, IConfigSource
	{
		public int RunCount { get; }

		public BenchmarkConfigAttribute(int runCount = 1)
		{
			RunCount = 1;
			var jobs = new[]
			{
				Job.Dry.With(Runtime.Core).With(Jit.RyuJit).WithIterationCount(runCount),
				Job.Dry.With(Runtime.Clr).With(Jit.RyuJit).WithIterationCount(runCount),
				Job.Dry.With(Runtime.Clr).With(Jit.LegacyJit).WithIterationCount(runCount)
			};
			Config = DefaultConfig.Instance
				.With(MemoryDiagnoser.Default)
				.With(MarkdownExporter.GitHub)
				.With(jobs);
		}

		public IConfig Config { get; }
	}
}
