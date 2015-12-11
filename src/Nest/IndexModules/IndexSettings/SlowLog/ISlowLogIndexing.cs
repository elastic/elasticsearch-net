using System;

namespace Nest
{
	public interface ISlowLogIndexing
	{
		TimeUnit ThresholdWarn { get; set; }
		TimeUnit ThresholdInfo { get; set; }
		TimeUnit ThresholdDebug { get; set; }
		TimeUnit ThresholdTrace { get; set; }
		LogLevel? LogLevel { get; set; }
		int? Source { get; set; }
	}
	public class SlowLogIndexing : ISlowLogIndexing
	{
		public LogLevel? LogLevel { get; set; }

		public int? Source { get; set; }

		public TimeUnit ThresholdDebug { get; set; }

		public TimeUnit ThresholdInfo { get; set; }

		public TimeUnit ThresholdTrace { get; set; }

		public TimeUnit ThresholdWarn { get; set; }
	}

	public class SlowLogIndexingDescriptor : DescriptorBase<SlowLogIndexingDescriptor, ISlowLogIndexing>, ISlowLogIndexing
	{
		LogLevel? ISlowLogIndexing.LogLevel { get; set; }
		int? ISlowLogIndexing.Source { get; set; }
		TimeUnit ISlowLogIndexing.ThresholdDebug { get; set; }
		TimeUnit ISlowLogIndexing.ThresholdInfo { get; set; }
		TimeUnit ISlowLogIndexing.ThresholdTrace { get; set; }
		TimeUnit ISlowLogIndexing.ThresholdWarn { get; set; }

		/// <inheritdoc/>
		public SlowLogIndexingDescriptor LogLevel(LogLevel? level) => Assign(a => a.LogLevel = level);

		/// <inheritdoc/>
		public SlowLogIndexingDescriptor Source(int? source) => Assign(a => a.Source = source);

		/// <inheritdoc/>
		public SlowLogIndexingDescriptor ThresholdDebug(TimeUnit time) => Assign(a => a.ThresholdDebug = time);

		/// <inheritdoc/>
		public SlowLogIndexingDescriptor ThresholdInfo(TimeUnit time) => Assign(a => a.ThresholdInfo = time);

		/// <inheritdoc/>
		public SlowLogIndexingDescriptor ThresholdTrace(TimeUnit time) => Assign(a => a.ThresholdTrace = time);

		/// <inheritdoc/>
		public SlowLogIndexingDescriptor ThresholdWarn(TimeUnit time) => Assign(a => a.ThresholdWarn = time);


	}
}