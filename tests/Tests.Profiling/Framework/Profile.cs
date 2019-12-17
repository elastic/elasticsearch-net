using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using JetBrains.Profiler.Windows.SelfApi;

namespace Tests.Profiling.Framework
{
	internal abstract class Profile : IDisposable
	{
		protected Profile(string resultsDirectory)
		{
			if (!Directory.Exists(resultsDirectory))
				Directory.CreateDirectory(resultsDirectory);

			this.ListFile = Path.Combine(resultsDirectory, "snapshot_list.xml");
			using (File.Create(this.ListFile)) { }

			if (this.ProfileProcesses.Any())
			{
				foreach (var profileProcess in this.ProfileProcesses)
				{
					profileProcess.Kill();
				}

				Thread.Sleep(1000);
			}
		}

		protected string ListFile { get; }

		public abstract bool IsActive { get; }

		private IEnumerable<Process> ProfileProcesses =>
			Process.GetProcessesByName("ExternalLauncherProfiler.x64").Concat(
			Process.GetProcessesByName("ExternalLauncherProfiler.x86")).ToArray();

		private static TimeSpan WaitTime => TimeSpan.FromSeconds(60);

		public virtual void Dispose()
		{
			// ensure running profiler process has chance to finish before starting next one
			while (SelfAttach.State == SelfApiState.Active || this.IsActive || this.ProfileProcesses.Any())
			{
				Thread.Sleep(250);
			}
		}

		protected void WaitForProfilerToAttachToProcess()
		{
			var waitTime = TimeSpan.Zero;

			// give the profiler a chance to attach
			while (SelfAttach.State != SelfApiState.Active)
			{
				var timeout = TimeSpan.FromMilliseconds(250);
				Thread.Sleep(timeout);

				if (waitTime <= WaitTime)
				{
					waitTime = waitTime.Add(timeout);
				}
				else
				{
					throw new Exception($"Could not attach profiler to process after {WaitTime}");
				}
			}
		}
	}
}
