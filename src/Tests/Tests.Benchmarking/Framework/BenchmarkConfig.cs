using System;
using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Loggers;
using BenchmarkDotNet.Reports;
using Newtonsoft.Json;

namespace Tests.Benchmarking.Framework
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
				summary.HostEnvironmentInfo.CpuInfo.Value.ProcessorName,
				summary.HostEnvironmentInfo.CpuInfo.Value.PhysicalCoreCount,
				summary.HostEnvironmentInfo.RuntimeVersion,
				summary.HostEnvironmentInfo.Architecture,
				summary.HostEnvironmentInfo.HasAttachedDebugger,
				summary.HostEnvironmentInfo.HasRyuJit,
				summary.HostEnvironmentInfo.Configuration,
				summary.HostEnvironmentInfo.JitModules,
				DotNetCliVersion = summary.HostEnvironmentInfo.DotNetSdkVersion.Value,
				summary.HostEnvironmentInfo.ChronometerFrequency,
				HardwareTimerKind = summary.HostEnvironmentInfo.HardwareTimerKind.ToString()
			};

			var benchmarks = summary.Reports.Select(r =>
			{
				var data = new Dictionary<string, object>
				{
                    { "DisplayInfo", r.BenchmarkCase.DisplayInfo },
					{ "Namespace", r.BenchmarkCase.Descriptor.Type.Namespace },
					{ "Type", r.BenchmarkCase.Descriptor.Type.Name },
					{ "Method", r.BenchmarkCase.Descriptor.WorkloadMethod.Name },
					{ "MethodTitle", r.BenchmarkCase.Descriptor.WorkloadMethod.Name },
					{ "Parameters", r.BenchmarkCase.Parameters.PrintInfo },
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
				Job.Dry.With(Runtime.Core).With(Jit.RyuJit).WithIterationCount(runCount),
				Job.Dry.With(Runtime.Clr).With(Jit.RyuJit).WithIterationCount(runCount),
				Job.Dry.With(Runtime.Clr).With(Jit.LegacyJit).WithIterationCount(runCount)
			};
			this.Config = DefaultConfig.Instance
				.With(jobs)
				.With(new CustomJsonExporter())
				.With(MemoryDiagnoser.Default);
		}
	}
}
