// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

namespace Nest
{
	public interface ISlowLogIndexing
	{
		LogLevel? LogLevel { get; set; }
		int? Source { get; set; }
		Time ThresholdDebug { get; set; }
		Time ThresholdInfo { get; set; }
		Time ThresholdTrace { get; set; }
		Time ThresholdWarn { get; set; }
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

		/// <inheritdoc />
		public SlowLogIndexingDescriptor LogLevel(LogLevel? level) => Assign(level, (a, v) => a.LogLevel = v);

		/// <inheritdoc />
		public SlowLogIndexingDescriptor Source(int? source) => Assign(source, (a, v) => a.Source = v);

		/// <inheritdoc />
		public SlowLogIndexingDescriptor ThresholdDebug(Time time) => Assign(time, (a, v) => a.ThresholdDebug = v);

		/// <inheritdoc />
		public SlowLogIndexingDescriptor ThresholdInfo(Time time) => Assign(time, (a, v) => a.ThresholdInfo = v);

		/// <inheritdoc />
		public SlowLogIndexingDescriptor ThresholdTrace(Time time) => Assign(time, (a, v) => a.ThresholdTrace = v);

		/// <inheritdoc />
		public SlowLogIndexingDescriptor ThresholdWarn(Time time) => Assign(time, (a, v) => a.ThresholdWarn = v);
	}
}
