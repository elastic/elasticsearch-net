// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

namespace Nest
{
	public interface ISlowLogSearchFetch
	{
		Time ThresholdDebug { get; set; }
		Time ThresholdInfo { get; set; }
		Time ThresholdTrace { get; set; }
		Time ThresholdWarn { get; set; }
	}

	public class SlowLogSearchFetch : ISlowLogSearchFetch
	{
		public Time ThresholdDebug { get; set; }
		public Time ThresholdInfo { get; set; }
		public Time ThresholdTrace { get; set; }
		public Time ThresholdWarn { get; set; }
	}

	public class SlowLogSearchFetchDescriptor : DescriptorBase<SlowLogSearchFetchDescriptor, ISlowLogSearchFetch>, ISlowLogSearchFetch
	{
		Time ISlowLogSearchFetch.ThresholdDebug { get; set; }
		Time ISlowLogSearchFetch.ThresholdInfo { get; set; }
		Time ISlowLogSearchFetch.ThresholdTrace { get; set; }
		Time ISlowLogSearchFetch.ThresholdWarn { get; set; }

		/// <inheritdoc />
		public SlowLogSearchFetchDescriptor ThresholdDebug(Time time) => Assign(time, (a, v) => a.ThresholdDebug = v);

		/// <inheritdoc />
		public SlowLogSearchFetchDescriptor ThresholdInfo(Time time) => Assign(time, (a, v) => a.ThresholdInfo = v);

		/// <inheritdoc />
		public SlowLogSearchFetchDescriptor ThresholdTrace(Time time) => Assign(time, (a, v) => a.ThresholdTrace = v);

		/// <inheritdoc />
		public SlowLogSearchFetchDescriptor ThresholdWarn(Time time) => Assign(time, (a, v) => a.ThresholdWarn = v);
	}
}
