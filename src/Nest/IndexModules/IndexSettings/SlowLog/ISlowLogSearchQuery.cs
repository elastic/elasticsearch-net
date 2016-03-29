namespace Nest
{
	public interface ISlowLogSearchQuery
	{
		//index.search.slowlog.threshold.query.warn: 10s
		Time ThresholdWarn { get; set; }
		//index.search.slowlog.threshold.query.info: 5s
		Time ThresholdInfo { get; set; }
		//index.search.slowlog.threshold.query.debug: 2s
		Time ThresholdDebug { get; set; }
		//index.search.slowlog.threshold.query.trace: 500ms
		Time ThresholdTrace { get; set; }
	}

	public class SlowLogSearchQuery : ISlowLogSearchQuery
	{
		public Time ThresholdDebug { get; set; }
		public Time ThresholdInfo { get; set; }
		public Time ThresholdTrace { get; set; }
		public Time ThresholdWarn { get; set; }
	}

	public class SlowLogSearchQueryDescriptor: DescriptorBase<SlowLogSearchQueryDescriptor, ISlowLogSearchQuery>, ISlowLogSearchQuery
	{
		Time ISlowLogSearchQuery.ThresholdDebug { get; set; }
		Time ISlowLogSearchQuery.ThresholdInfo { get; set; }
		Time ISlowLogSearchQuery.ThresholdTrace { get; set; }
		Time ISlowLogSearchQuery.ThresholdWarn { get; set; }

		/// <inheritdoc/>
		public SlowLogSearchQueryDescriptor ThresholdDebug(Time time) => Assign(a => a.ThresholdDebug = time);

		/// <inheritdoc/>
		public SlowLogSearchQueryDescriptor ThresholdInfo(Time time) => Assign(a => a.ThresholdInfo = time);

		/// <inheritdoc/>
		public SlowLogSearchQueryDescriptor ThresholdTrace(Time time) => Assign(a => a.ThresholdTrace = time);

		/// <inheritdoc/>
		public SlowLogSearchQueryDescriptor ThresholdWarn(Time time) => Assign(a => a.ThresholdWarn = time);


	}
}