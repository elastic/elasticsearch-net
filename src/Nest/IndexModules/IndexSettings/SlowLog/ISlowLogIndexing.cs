/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

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
