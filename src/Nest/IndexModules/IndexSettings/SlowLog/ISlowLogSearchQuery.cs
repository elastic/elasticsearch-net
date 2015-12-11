using System;

namespace Nest
{
	public interface ISlowLogSearchQuery
	{
		//index.search.slowlog.threshold.query.warn: 10s
		TimeUnit ThresholdWarn { get; set; }
		//index.search.slowlog.threshold.query.info: 5s
		TimeUnit ThresholdInfo { get; set; }
		//index.search.slowlog.threshold.query.debug: 2s
		TimeUnit ThresholdDebug { get; set; }
		//index.search.slowlog.threshold.query.trace: 500ms
		TimeUnit ThresholdTrace { get; set; }
	}

	public class SlowLogSearchQuery : ISlowLogSearchQuery
	{
		public TimeUnit ThresholdDebug { get; set; }
		public TimeUnit ThresholdInfo { get; set; }
		public TimeUnit ThresholdTrace { get; set; }
		public TimeUnit ThresholdWarn { get; set; }
	}

	public class SlowLogSearchQueryDescriptor: DescriptorBase<SlowLogSearchQueryDescriptor, ISlowLogSearchQuery>, ISlowLogSearchQuery
	{
		TimeUnit ISlowLogSearchQuery.ThresholdDebug { get; set; }
		TimeUnit ISlowLogSearchQuery.ThresholdInfo { get; set; }
		TimeUnit ISlowLogSearchQuery.ThresholdTrace { get; set; }
		TimeUnit ISlowLogSearchQuery.ThresholdWarn { get; set; }

		/// <inheritdoc/>
		public SlowLogSearchQueryDescriptor ThresholdDebug(TimeUnit time) => Assign(a => a.ThresholdDebug = time);

		/// <inheritdoc/>
		public SlowLogSearchQueryDescriptor ThresholdInfo(TimeUnit time) => Assign(a => a.ThresholdInfo = time);

		/// <inheritdoc/>
		public SlowLogSearchQueryDescriptor ThresholdTrace(TimeUnit time) => Assign(a => a.ThresholdTrace = time);

		/// <inheritdoc/>
		public SlowLogSearchQueryDescriptor ThresholdWarn(TimeUnit time) => Assign(a => a.ThresholdWarn = time);


	}
}