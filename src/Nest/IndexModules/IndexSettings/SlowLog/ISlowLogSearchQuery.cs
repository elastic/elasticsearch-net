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
