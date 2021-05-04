// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

namespace Nest
{
	public interface ISlowLogSearchQuery
	{
		//index.search.slowlog.threshold.query.debug: 2s
		Time ThresholdDebug { get; set; }

		//index.search.slowlog.threshold.query.info: 5s
		Time ThresholdInfo { get; set; }

		//index.search.slowlog.threshold.query.trace: 500ms
		Time ThresholdTrace { get; set; }

		//index.search.slowlog.threshold.query.warn: 10s
		Time ThresholdWarn { get; set; }
	}

	public class SlowLogSearchQuery : ISlowLogSearchQuery
	{
		public Time ThresholdDebug { get; set; }
		public Time ThresholdInfo { get; set; }
		public Time ThresholdTrace { get; set; }
		public Time ThresholdWarn { get; set; }
	}

	public class SlowLogSearchQueryDescriptor : DescriptorBase<SlowLogSearchQueryDescriptor, ISlowLogSearchQuery>, ISlowLogSearchQuery
	{
		Time ISlowLogSearchQuery.ThresholdDebug { get; set; }
		Time ISlowLogSearchQuery.ThresholdInfo { get; set; }
		Time ISlowLogSearchQuery.ThresholdTrace { get; set; }
		Time ISlowLogSearchQuery.ThresholdWarn { get; set; }

		/// <inheritdoc />
		public SlowLogSearchQueryDescriptor ThresholdDebug(Time time) => Assign(time, (a, v) => a.ThresholdDebug = v);

		/// <inheritdoc />
		public SlowLogSearchQueryDescriptor ThresholdInfo(Time time) => Assign(time, (a, v) => a.ThresholdInfo = v);

		/// <inheritdoc />
		public SlowLogSearchQueryDescriptor ThresholdTrace(Time time) => Assign(time, (a, v) => a.ThresholdTrace = v);

		/// <inheritdoc />
		public SlowLogSearchQueryDescriptor ThresholdWarn(Time time) => Assign(time, (a, v) => a.ThresholdWarn = v);
	}
}
