using System;

namespace Nest
{
	public interface ISlowLogIndexing
	{
		TimeUnitExpression TresholdWarn { get; set; }
		TimeUnitExpression TresholdInfo { get; set; }
		TimeUnitExpression TresholdDebug { get; set; }
		TimeUnitExpression TresholdTrace { get; set; }
		SlowLogLevel? LogLevel { get; set; }
		int? Source { get; set; }
	}
	public class SlowLogIndexing : ISlowLogIndexing
	{
		public SlowLogLevel? LogLevel { get; set; }

		public int? Source { get; set; }

		public TimeUnitExpression TresholdDebug { get; set; }

		public TimeUnitExpression TresholdInfo { get; set; }

		public TimeUnitExpression TresholdTrace { get; set; }

		public TimeUnitExpression TresholdWarn { get; set; }
	}

	public class SlowLogIndexingDescriptor : ISlowLogIndexing
	{
		SlowLogIndexingDescriptor Assign(Action<ISlowLogIndexing> assigner) => Fluent.Assign(this, assigner);

		SlowLogLevel? ISlowLogIndexing.LogLevel { get; set; }
		int? ISlowLogIndexing.Source { get; set; }
		TimeUnitExpression ISlowLogIndexing.TresholdDebug { get; set; }
		TimeUnitExpression ISlowLogIndexing.TresholdInfo { get; set; }
		TimeUnitExpression ISlowLogIndexing.TresholdTrace { get; set; }
		TimeUnitExpression ISlowLogIndexing.TresholdWarn { get; set; }

		/// </inheritdoc>
		public SlowLogIndexingDescriptor LogLevel(SlowLogLevel? level) => Assign(a => a.LogLevel = level);

		/// </inheritdoc>
		public SlowLogIndexingDescriptor Source(int? source) => Assign(a => a.Source = source);

		/// </inheritdoc>
		public SlowLogIndexingDescriptor TresholdDebug(TimeUnitExpression time) => Assign(a => a.TresholdDebug = time);

		/// </inheritdoc>
		public SlowLogIndexingDescriptor TresholdInfo(TimeUnitExpression time) => Assign(a => a.TresholdInfo = time);

		/// </inheritdoc>
		public SlowLogIndexingDescriptor TresholdTrace(TimeUnitExpression time) => Assign(a => a.TresholdTrace = time);

		/// </inheritdoc>
		public SlowLogIndexingDescriptor TresholdWarn(TimeUnitExpression time) => Assign(a => a.TresholdWarn = time);


	}
}