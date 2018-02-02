using System.IO;
using JetBrains.Profiler.Windows.Api;
using JetBrains.Profiler.Windows.SelfApi;
using JetBrains.Profiler.Windows.SelfApi.Config;

namespace Tests.Framework.Profiling.Timeline
{
	internal class TimelineProfile : Profile
	{
		public TimelineProfile(string sdkPath, string resultsDirectory) : base(resultsDirectory)
		{
			var saveSnapshotProfilingConfig = new SaveSnapshotProfilingConfig
			{
				ProfilingControlKind = ProfilingControlKind.Api,
				TempDir = Path.GetTempPath(),
				SaveDir = resultsDirectory,
				RedistDir = sdkPath,
				ProfilingType = ProfilingType.Timeline,
				ListFile = ListFile,
				SnapshotFormat = SnapshotFormat.Uncompressed
			};

			SelfAttach.Attach(saveSnapshotProfilingConfig);
			WaitForProfilerToAttachToProcess();

			if (TimelineProfiler.IsActive)
				TimelineProfiler.Begin();
		}

		public override bool IsActive => TimelineProfiler.IsActive;

		public override void Dispose()
		{
			if (TimelineProfiler.IsActive)
				TimelineProfiler.EndSave();

			if (TimelineProfiler.CanDetach)
				TimelineProfiler.Detach();

			base.Dispose();


		}
	}
}
