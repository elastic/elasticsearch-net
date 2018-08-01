using System;
using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Exporters.Json;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Loggers;
using BenchmarkDotNet.Reports;
using Newtonsoft.Json;

namespace Tests.Framework.Benchmarks
{
	public class CustomJsonExporter : BenchmarkDotNet.Exporters.ExporterBase
	{
		protected override string FileExtension => "json";

		protected override string FileNameSuffix => "-custom";

		public override void ExportToLog(Summary summary, ILogger logger)
		{
			var environmentInfo = new
			{
				HostEnvironmentInfo.BenchmarkDotNetCaption,
				summary.HostEnvironmentInfo.BenchmarkDotNetVersion,
				OsVersion = summary.HostEnvironmentInfo.OsVersion.Value,
				ProcessorName = summary.HostEnvironmentInfo.ProcessorName.Value,
				summary.HostEnvironmentInfo.ProcessorCount,
				summary.HostEnvironmentInfo.RuntimeVersion,
				summary.HostEnvironmentInfo.Architecture,
				summary.HostEnvironmentInfo.HasAttachedDebugger,
				summary.HostEnvironmentInfo.HasRyuJit,
				summary.HostEnvironmentInfo.Configuration,
				summary.HostEnvironmentInfo.JitModules,
				DotNetCliVersion = summary.HostEnvironmentInfo.DotNetCliVersion.Value,
				summary.HostEnvironmentInfo.ChronometerFrequency,
				HardwareTimerKind = summary.HostEnvironmentInfo.HardwareTimerKind.ToString()
			};

			var benchmarks = summary.Reports.Select(r =>
			{
				var data = new Dictionary<string, object>
				{
                    { "DisplayInfo", r.Benchmark.DisplayInfo },
					{ "Namespace", r.Benchmark.Target.Type.Namespace },
					{ "Type", r.Benchmark.Target.Type.Name },
					{ "Method", r.Benchmark.Target.Method.Name },
					{ "MethodTitle", r.Benchmark.Target.MethodDisplayInfo },
					{ "Parameters", r.Benchmark.Parameters.PrintInfo },
                    { "Statistics", r.ResultStatistics },
					{ "Memory", r.GcStats }
				};

				return data;
			});

			logger.WriteLine(JsonConvert.SerializeObject(new Dictionary<string, object>
			{
				{ "Title", summary.Title },
				{ "TotalTime", summary.TotalTime },
				{ "HostEnvironmentInfo", environmentInfo },
				{ "Benchmarks", benchmarks }
			}));
		}
	}
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
			Config = DefaultConfig.Instance
				.With(jobs)
				.With(new CustomJsonExporter())
				.With(MemoryDiagnoser.Default);
		}
	}
}
