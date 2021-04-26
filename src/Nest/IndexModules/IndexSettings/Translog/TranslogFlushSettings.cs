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
	public interface ITranslogFlushSettings
	{
		/// <summary>
		/// How often to check if a flush is needed, randomized between
		/// the interval value and 2x the interval value. Defaults to 5s.
		/// </summary>
		Time Interval { get; set; }

		/// <summary>
		/// How long to wait before triggering a flush regardless of translog size. Defaults to 30m.
		/// </summary>
		Time ThresholdPeriod { get; set; }

		/// <summary>
		/// Once the translog hits this size, a flush will happen. Defaults to 512mb.
		/// </summary>
		string ThresholdSize { get; set; }
	}

	public class TranslogFlushSettings : ITranslogFlushSettings
	{
		/// <inheritdoc />
		public Time Interval { get; set; }

		/// <inheritdoc />
		public Time ThresholdPeriod { get; set; }

		/// <inheritdoc />
		public string ThresholdSize { get; set; }
	}

	public class TranslogFlushSettingsDescriptor : DescriptorBase<TranslogFlushSettingsDescriptor, ITranslogFlushSettings>, ITranslogFlushSettings
	{
		Time ITranslogFlushSettings.Interval { get; set; }
		Time ITranslogFlushSettings.ThresholdPeriod { get; set; }
		string ITranslogFlushSettings.ThresholdSize { get; set; }

		/// <inheritdoc />
		public TranslogFlushSettingsDescriptor ThresholdSize(string size) => Assign(size, (a, v) => a.ThresholdSize = v);

		/// <inheritdoc />
		public TranslogFlushSettingsDescriptor ThresholdPeriod(Time time) =>
			Assign(time, (a, v) => a.ThresholdPeriod = v);

		/// <inheritdoc />
		public TranslogFlushSettingsDescriptor Interval(Time time) => Assign(time, (a, v) => a.Interval = v);
	}
}
