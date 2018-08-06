using System.IO;
using System.Threading;
using JetBrains.Profiler.Windows.Api;
using JetBrains.Profiler.Windows.SelfApi;
using JetBrains.Profiler.Windows.SelfApi.Config;

namespace Tests.Profiling.Framework.Performance
{
	internal class PerformanceProfile : Profile
	{
		public PerformanceProfile(string sdkPath, string resultsDirectory) : base(resultsDirectory)
		{
			var saveSnapshotProfilingConfig = new SaveSnapshotProfilingConfig
			{
				ProfilingControlKind = ProfilingControlKind.Api,
				TempDir = Path.GetTempPath(),
				SaveDir = resultsDirectory,
				RedistDir = sdkPath,
				ProfilingType = ProfilingType.Performance,
				ListFile = this.ListFile,
				SnapshotFormat = SnapshotFormat.Uncompressed
			};

			while (SelfAttach.State != SelfApiState.None)
			{
				Thread.Sleep(250);
			}

			SelfAttach.Attach(saveSnapshotProfilingConfig);
			this.WaitForProfilerToAttachToProcess();

			if (PerformanceProfiler.IsActive)
			{
				PerformanceProfiler.Begin();
				PerformanceProfiler.Start();
			}
		}

		public override bool IsActive => PerformanceProfiler.IsActive;

		public override void Dispose()
		{
			if (PerformanceProfiler.IsActive)
			{
				PerformanceProfiler.Stop();
				PerformanceProfiler.EndSave();
			}

			if (PerformanceProfiler.CanDetach)
				PerformanceProfiler.Detach();

			base.Dispose();
		}
	}
}
