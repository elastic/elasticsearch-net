namespace Nest
{
	public interface ISlowLogSearchFetch
	{
		Time ThresholdWarn { get; set; }
		Time ThresholdInfo { get; set; }
		Time ThresholdDebug { get; set; }
		Time ThresholdTrace { get; set; }
	}

	public class SlowLogSearchFetch : ISlowLogSearchFetch
	{
		public Time ThresholdDebug { get; set; }
		public Time ThresholdInfo { get; set; }
		public Time ThresholdTrace { get; set; }
		public Time ThresholdWarn { get; set; }
	}

	public class SlowLogSearchFetchDescriptor: DescriptorBase<SlowLogSearchFetchDescriptor, ISlowLogSearchFetch>, ISlowLogSearchFetch
	{
		Time ISlowLogSearchFetch.ThresholdDebug { get; set; }
		Time ISlowLogSearchFetch.ThresholdInfo { get; set; }
		Time ISlowLogSearchFetch.ThresholdTrace { get; set; }
		Time ISlowLogSearchFetch.ThresholdWarn { get; set; }

		/// <inheritdoc/>
		public SlowLogSearchFetchDescriptor ThresholdDebug(Time time) => Assign(a => a.ThresholdDebug = time);

		/// <inheritdoc/>
		public SlowLogSearchFetchDescriptor ThresholdInfo(Time time) => Assign(a => a.ThresholdInfo = time);

		/// <inheritdoc/>
		public SlowLogSearchFetchDescriptor ThresholdTrace(Time time) => Assign(a => a.ThresholdTrace = time);

		/// <inheritdoc/>
		public SlowLogSearchFetchDescriptor ThresholdWarn(Time time) => Assign(a => a.ThresholdWarn = time);


	}
}