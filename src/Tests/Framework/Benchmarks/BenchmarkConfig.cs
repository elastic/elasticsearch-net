using System;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Exporters.Json;
using BenchmarkDotNet.Jobs;

namespace Tests.Framework.Benchmarks
{
	public class BenchmarkConfigAttribute : Attribute, IConfigSource
	{
		public IConfig Config { get; }

		public BenchmarkConfigAttribute(int runCount = 1)
		{
			var jobs = new[] {
				Job.Dry.With(Runtime.Core).With(Jit.RyuJit).WithTargetCount(runCount),
				Job.Dry.With(Runtime.Clr).With(Jit.RyuJit).WithTargetCount(runCount),
				Job.Dry.With(Runtime.Clr).With(Jit.LegacyJit).WithTargetCount(runCount)
			};
			Config = ManualConfig.CreateEmpty().With(jobs).With(JsonExporter.Brief);
		}
	}
}
