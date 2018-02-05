using System.IO;
using System.Threading;
#if FEATURE_PROFILING
using JetBrains.Profiler.Windows.Api;
using JetBrains.Profiler.Windows.SelfApi;
using JetBrains.Profiler.Windows.SelfApi.Config;

namespace Tests.Framework.Profiling.Memory
{
	internal class MemoryProfile : Profile
	{
		public MemoryProfile(string sdkPath, string resultsDirectory) : base(resultsDirectory)
		{
			var saveSnapshotProfilingConfig = new SaveSnapshotProfilingConfig
			{
				ProfilingControlKind = ProfilingControlKind.Api,
				TempDir = Path.GetTempPath(),
				SaveDir = resultsDirectory,
				RedistDir = sdkPath,
				ProfilingType = ProfilingType.Memory,
				ListFile = ListFile
			};

			while (SelfAttach.State != SelfApiState.None)
			{
				Thread.Sleep(250);
			}

			SelfAttach.Attach(saveSnapshotProfilingConfig);
			WaitForProfilerToAttachToProcess();

			if (MemoryProfiler.IsActive && MemoryProfiler.CanControlAllocations)
			{
				MemoryProfiler.EnableAllocations();
			}
		}

		public override bool IsActive => MemoryProfiler.IsActive;

		public override void Dispose()
		{
			if (MemoryProfiler.IsActive)
			{
				MemoryProfiler.Dump();
			}

			if (MemoryProfiler.CanDetach)
				MemoryProfiler.Detach();

			base.Dispose();
		}
	}
}
#endif
