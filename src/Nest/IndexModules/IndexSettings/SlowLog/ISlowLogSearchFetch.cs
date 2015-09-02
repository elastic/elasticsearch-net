using System;

namespace Nest
{
	public interface ISlowLogSearchFetch
	{
		TimeUnitExpression ThresholdWarn { get; set; }
		TimeUnitExpression ThresholdInfo { get; set; }
		TimeUnitExpression ThresholdDebug { get; set; }
		TimeUnitExpression ThresholdTrace { get; set; }
	}

	public class SlowLogSearchFetch : ISlowLogSearchFetch
	{
		public TimeUnitExpression ThresholdDebug { get; set; }
		public TimeUnitExpression ThresholdInfo { get; set; }
		public TimeUnitExpression ThresholdTrace { get; set; }
		public TimeUnitExpression ThresholdWarn { get; set; }
	}

	public class SlowLogSearchFetchDescriptor: DescriptorBase<SlowLogSearchFetchDescriptor, ISlowLogSearchFetch>, ISlowLogSearchFetch
	{
		TimeUnitExpression ISlowLogSearchFetch.ThresholdDebug { get; set; }
		TimeUnitExpression ISlowLogSearchFetch.ThresholdInfo { get; set; }
		TimeUnitExpression ISlowLogSearchFetch.ThresholdTrace { get; set; }
		TimeUnitExpression ISlowLogSearchFetch.ThresholdWarn { get; set; }

		/// <inheritdoc/>
		public SlowLogSearchFetchDescriptor ThresholdDebug(TimeUnitExpression time) => Assign(a => a.ThresholdDebug = time);

		/// <inheritdoc/>
		public SlowLogSearchFetchDescriptor ThresholdInfo(TimeUnitExpression time) => Assign(a => a.ThresholdInfo = time);

		/// <inheritdoc/>
		public SlowLogSearchFetchDescriptor ThresholdTrace(TimeUnitExpression time) => Assign(a => a.ThresholdTrace = time);

		/// <inheritdoc/>
		public SlowLogSearchFetchDescriptor ThresholdWarn(TimeUnitExpression time) => Assign(a => a.ThresholdWarn = time);


	}
}