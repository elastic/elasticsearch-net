using System;

namespace Nest
{
	public interface ISlowLogIndexing
	{
		TimeUnitExpression ThresholdWarn { get; set; }
		TimeUnitExpression ThresholdInfo { get; set; }
		TimeUnitExpression ThresholdDebug { get; set; }
		TimeUnitExpression ThresholdTrace { get; set; }
		LogLevel? LogLevel { get; set; }
		int? Source { get; set; }
	}
	public class SlowLogIndexing : ISlowLogIndexing
	{
		public LogLevel? LogLevel { get; set; }

		public int? Source { get; set; }

		public TimeUnitExpression ThresholdDebug { get; set; }

		public TimeUnitExpression ThresholdInfo { get; set; }

		public TimeUnitExpression ThresholdTrace { get; set; }

		public TimeUnitExpression ThresholdWarn { get; set; }
	}

	public class SlowLogIndexingDescriptor : DescriptorBase<SlowLogIndexingDescriptor, ISlowLogIndexing>, ISlowLogIndexing
	{
		LogLevel? ISlowLogIndexing.LogLevel { get; set; }
		int? ISlowLogIndexing.Source { get; set; }
		TimeUnitExpression ISlowLogIndexing.ThresholdDebug { get; set; }
		TimeUnitExpression ISlowLogIndexing.ThresholdInfo { get; set; }
		TimeUnitExpression ISlowLogIndexing.ThresholdTrace { get; set; }
		TimeUnitExpression ISlowLogIndexing.ThresholdWarn { get; set; }

		/// <inheritdoc/>
		public SlowLogIndexingDescriptor LogLevel(LogLevel? level) => Assign(a => a.LogLevel = level);

		/// <inheritdoc/>
		public SlowLogIndexingDescriptor Source(int? source) => Assign(a => a.Source = source);

		/// <inheritdoc/>
		public SlowLogIndexingDescriptor ThresholdDebug(TimeUnitExpression time) => Assign(a => a.ThresholdDebug = time);

		/// <inheritdoc/>
		public SlowLogIndexingDescriptor ThresholdInfo(TimeUnitExpression time) => Assign(a => a.ThresholdInfo = time);

		/// <inheritdoc/>
		public SlowLogIndexingDescriptor ThresholdTrace(TimeUnitExpression time) => Assign(a => a.ThresholdTrace = time);

		/// <inheritdoc/>
		public SlowLogIndexingDescriptor ThresholdWarn(TimeUnitExpression time) => Assign(a => a.ThresholdWarn = time);


	}
}