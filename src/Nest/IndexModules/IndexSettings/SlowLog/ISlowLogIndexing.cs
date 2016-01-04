namespace Nest
{
	public interface ISlowLogIndexing
	{
		Time ThresholdWarn { get; set; }
		Time ThresholdInfo { get; set; }
		Time ThresholdDebug { get; set; }
		Time ThresholdTrace { get; set; }
		LogLevel? LogLevel { get; set; }
		int? Source { get; set; }
	}
	public class SlowLogIndexing : ISlowLogIndexing
	{
		public LogLevel? LogLevel { get; set; }

		public int? Source { get; set; }

		public Time ThresholdDebug { get; set; }

		public Time ThresholdInfo { get; set; }

		public Time ThresholdTrace { get; set; }

		public Time ThresholdWarn { get; set; }
	}

	public class SlowLogIndexingDescriptor : DescriptorBase<SlowLogIndexingDescriptor, ISlowLogIndexing>, ISlowLogIndexing
	{
		LogLevel? ISlowLogIndexing.LogLevel { get; set; }
		int? ISlowLogIndexing.Source { get; set; }
		Time ISlowLogIndexing.ThresholdDebug { get; set; }
		Time ISlowLogIndexing.ThresholdInfo { get; set; }
		Time ISlowLogIndexing.ThresholdTrace { get; set; }
		Time ISlowLogIndexing.ThresholdWarn { get; set; }

		/// <inheritdoc/>
		public SlowLogIndexingDescriptor LogLevel(LogLevel? level) => Assign(a => a.LogLevel = level);

		/// <inheritdoc/>
		public SlowLogIndexingDescriptor Source(int? source) => Assign(a => a.Source = source);

		/// <inheritdoc/>
		public SlowLogIndexingDescriptor ThresholdDebug(Time time) => Assign(a => a.ThresholdDebug = time);

		/// <inheritdoc/>
		public SlowLogIndexingDescriptor ThresholdInfo(Time time) => Assign(a => a.ThresholdInfo = time);

		/// <inheritdoc/>
		public SlowLogIndexingDescriptor ThresholdTrace(Time time) => Assign(a => a.ThresholdTrace = time);

		/// <inheritdoc/>
		public SlowLogIndexingDescriptor ThresholdWarn(Time time) => Assign(a => a.ThresholdWarn = time);


	}
}