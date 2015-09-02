using System;

namespace Nest
{
	public interface ISlowLogSearchQuery
	{
		//index.search.slowlog.threshold.query.warn: 10s
		TimeUnitExpression ThresholdWarn { get; set; }
		//index.search.slowlog.threshold.query.info: 5s
		TimeUnitExpression ThresholdInfo { get; set; }
		//index.search.slowlog.threshold.query.debug: 2s
		TimeUnitExpression ThresholdDebug { get; set; }
		//index.search.slowlog.threshold.query.trace: 500ms
		TimeUnitExpression ThresholdTrace { get; set; }
	}

	public class SlowLogSearchQuery : ISlowLogSearchQuery
	{
		public TimeUnitExpression ThresholdDebug { get; set; }
		public TimeUnitExpression ThresholdInfo { get; set; }
		public TimeUnitExpression ThresholdTrace { get; set; }
		public TimeUnitExpression ThresholdWarn { get; set; }
	}

	public class SlowLogSearchQueryDescriptor: DescriptorBase<SlowLogSearchQueryDescriptor, ISlowLogSearchQuery>, ISlowLogSearchQuery
	{
		TimeUnitExpression ISlowLogSearchQuery.ThresholdDebug { get; set; }
		TimeUnitExpression ISlowLogSearchQuery.ThresholdInfo { get; set; }
		TimeUnitExpression ISlowLogSearchQuery.ThresholdTrace { get; set; }
		TimeUnitExpression ISlowLogSearchQuery.ThresholdWarn { get; set; }

		/// <inheritdoc/>
		public SlowLogSearchQueryDescriptor ThresholdDebug(TimeUnitExpression time) => Assign(a => a.ThresholdDebug = time);

		/// <inheritdoc/>
		public SlowLogSearchQueryDescriptor ThresholdInfo(TimeUnitExpression time) => Assign(a => a.ThresholdInfo = time);

		/// <inheritdoc/>
		public SlowLogSearchQueryDescriptor ThresholdTrace(TimeUnitExpression time) => Assign(a => a.ThresholdTrace = time);

		/// <inheritdoc/>
		public SlowLogSearchQueryDescriptor ThresholdWarn(TimeUnitExpression time) => Assign(a => a.ThresholdWarn = time);


	}
}