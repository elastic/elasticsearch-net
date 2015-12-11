using System;

namespace Nest
{
	public interface ISlowLogSearchFetch
	{
		TimeUnit ThresholdWarn { get; set; }
		TimeUnit ThresholdInfo { get; set; }
		TimeUnit ThresholdDebug { get; set; }
		TimeUnit ThresholdTrace { get; set; }
	}

	public class SlowLogSearchFetch : ISlowLogSearchFetch
	{
		public TimeUnit ThresholdDebug { get; set; }
		public TimeUnit ThresholdInfo { get; set; }
		public TimeUnit ThresholdTrace { get; set; }
		public TimeUnit ThresholdWarn { get; set; }
	}

	public class SlowLogSearchFetchDescriptor: DescriptorBase<SlowLogSearchFetchDescriptor, ISlowLogSearchFetch>, ISlowLogSearchFetch
	{
		TimeUnit ISlowLogSearchFetch.ThresholdDebug { get; set; }
		TimeUnit ISlowLogSearchFetch.ThresholdInfo { get; set; }
		TimeUnit ISlowLogSearchFetch.ThresholdTrace { get; set; }
		TimeUnit ISlowLogSearchFetch.ThresholdWarn { get; set; }

		/// <inheritdoc/>
		public SlowLogSearchFetchDescriptor ThresholdDebug(TimeUnit time) => Assign(a => a.ThresholdDebug = time);

		/// <inheritdoc/>
		public SlowLogSearchFetchDescriptor ThresholdInfo(TimeUnit time) => Assign(a => a.ThresholdInfo = time);

		/// <inheritdoc/>
		public SlowLogSearchFetchDescriptor ThresholdTrace(TimeUnit time) => Assign(a => a.ThresholdTrace = time);

		/// <inheritdoc/>
		public SlowLogSearchFetchDescriptor ThresholdWarn(TimeUnit time) => Assign(a => a.ThresholdWarn = time);


	}
}