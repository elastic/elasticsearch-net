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
