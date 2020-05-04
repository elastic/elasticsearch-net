// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

namespace Nest
{
	public interface IMergeSchedulerSettings
	{
		/// <summary>
		/// If this is true (the default), then the merge scheduler will rate-limit IO (writes) for merges to an
		/// adaptive value depending on how many merges are requested over time. An application with a low indexing rate
		/// that unluckily suddenly requires a large merge will see that merge aggressively throttled,
		/// while an application doing heavy indexing will see the throttle move higher to allow merges to keep
		/// up with ongoing indexing.
		/// </summary>
		bool? AutoThrottle { get; set; }

		/// <summary>
		/// The maximum number of threads that may be merging at once. Defaults to
		/// <pre>Math.max(1, Math.min(4, Runtime.getRuntime().availableProcessors() / 2))</pre>
		/// which works well for a good solid-state-disk (SSD). If your index is on spinning platter drives instead,
		/// decrease this to 1.
		/// </summary>
		int? MaxThreadCount { get; set; }
	}

	public class MergeSchedulerSettings : IMergeSchedulerSettings
	{
		/// <inheritdoc />
		public bool? AutoThrottle { get; set; }

		/// <inheritdoc />
		public int? MaxThreadCount { get; set; }
	}

	public class MergeSchedulerSettingsDescriptor
		: DescriptorBase<MergeSchedulerSettingsDescriptor, IMergeSchedulerSettings>, IMergeSchedulerSettings
	{
		bool? IMergeSchedulerSettings.AutoThrottle { get; set; }
		int? IMergeSchedulerSettings.MaxThreadCount { get; set; }

		/// <inheritdoc />
		public MergeSchedulerSettingsDescriptor AutoThrottle(bool? throttle = true) =>
			Assign(throttle, (a, v) => a.AutoThrottle = v);

		/// <inheritdoc />
		public MergeSchedulerSettingsDescriptor MaxThreadCount(int? maxThreadCount) =>
			Assign(maxThreadCount, (a, v) => a.MaxThreadCount = v);
	}
}
