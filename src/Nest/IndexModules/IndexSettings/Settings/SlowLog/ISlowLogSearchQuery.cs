using System;

namespace Nest
{
	public interface ISlowLogSearchQuery
	{
		//index.search.slowlog.threshold.query.warn: 10s
		TimeUnitExpression TresholdWarn { get; set; }
		//index.search.slowlog.threshold.query.info: 5s
		TimeUnitExpression TresholdInfo { get; set; }
		//index.search.slowlog.threshold.query.debug: 2s
		TimeUnitExpression TresholdDebug { get; set; }
		//index.search.slowlog.threshold.query.trace: 500ms
		TimeUnitExpression TresholdTrace { get; set; }
	}

	public class SlowLogSearchQuery : ISlowLogSearchQuery
	{
		public TimeUnitExpression TresholdDebug { get; set; }
		public TimeUnitExpression TresholdInfo { get; set; }
		public TimeUnitExpression TresholdTrace { get; set; }
		public TimeUnitExpression TresholdWarn { get; set; }
	}

	public class SlowLogSearchQueryDescriptor : ISlowLogSearchQuery
	{
		SlowLogSearchQueryDescriptor Assign(Action<ISlowLogSearchQuery> assigner) => Fluent.Assign(this, assigner);

		TimeUnitExpression ISlowLogSearchQuery.TresholdDebug { get; set; }
		TimeUnitExpression ISlowLogSearchQuery.TresholdInfo { get; set; }
		TimeUnitExpression ISlowLogSearchQuery.TresholdTrace { get; set; }
		TimeUnitExpression ISlowLogSearchQuery.TresholdWarn { get; set; }

		/// <inheritdoc/>
		public SlowLogSearchQueryDescriptor TresholdDebug(TimeUnitExpression time) => Assign(a => a.TresholdDebug = time);

		/// <inheritdoc/>
		public SlowLogSearchQueryDescriptor TresholdInfo(TimeUnitExpression time) => Assign(a => a.TresholdInfo = time);

		/// <inheritdoc/>
		public SlowLogSearchQueryDescriptor TresholdTrace(TimeUnitExpression time) => Assign(a => a.TresholdTrace = time);

		/// <inheritdoc/>
		public SlowLogSearchQueryDescriptor TresholdWarn(TimeUnitExpression time) => Assign(a => a.TresholdWarn = time);


	}
}