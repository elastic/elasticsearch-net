using System;

namespace Nest
{
	public interface ISlowLogSearchFetch
	{
		TimeUnitExpression TresholdWarn { get; set; }
		TimeUnitExpression TresholdInfo { get; set; }
		TimeUnitExpression TresholdDebug { get; set; }
		TimeUnitExpression TresholdTrace { get; set; }
	}

	public class SlowLogSearchFetch : ISlowLogSearchFetch
	{
		public TimeUnitExpression TresholdDebug { get; set; }
		public TimeUnitExpression TresholdInfo { get; set; }
		public TimeUnitExpression TresholdTrace { get; set; }
		public TimeUnitExpression TresholdWarn { get; set; }
	}

	public class SlowLogSearchFetchDescriptor : ISlowLogSearchFetch
	{
		SlowLogSearchFetchDescriptor Assign(Action<ISlowLogSearchFetch> assigner) => Fluent.Assign(this, assigner);

		TimeUnitExpression ISlowLogSearchFetch.TresholdDebug { get; set; }
		TimeUnitExpression ISlowLogSearchFetch.TresholdInfo { get; set; }
		TimeUnitExpression ISlowLogSearchFetch.TresholdTrace { get; set; }
		TimeUnitExpression ISlowLogSearchFetch.TresholdWarn { get; set; }

		/// </inheritdoc>
		public SlowLogSearchFetchDescriptor TresholdDebug(TimeUnitExpression time) => Assign(a => a.TresholdDebug = time);

		/// </inheritdoc>
		public SlowLogSearchFetchDescriptor TresholdInfo(TimeUnitExpression time) => Assign(a => a.TresholdInfo = time);

		/// </inheritdoc>
		public SlowLogSearchFetchDescriptor TresholdTrace(TimeUnitExpression time) => Assign(a => a.TresholdTrace = time);

		/// </inheritdoc>
		public SlowLogSearchFetchDescriptor TresholdWarn(TimeUnitExpression time) => Assign(a => a.TresholdWarn = time);


	}
}