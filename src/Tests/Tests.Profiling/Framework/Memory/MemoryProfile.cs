using System.IO;
using System.Threading;
using JetBrains.Profiler.Windows.Api;
using JetBrains.Profiler.Windows.SelfApi;
using JetBrains.Profiler.Windows.SelfApi.Config;

namespace Tests.Profiling.Framework.Memory
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
				ListFile = this.ListFile
			};

			while (SelfAttach.State != SelfApiState.None)
			{
				Thread.Sleep(250);
			}

			SelfAttach.Attach(saveSnapshotProfilingConfig);
			this.WaitForProfilerToAttachToProcess();

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
