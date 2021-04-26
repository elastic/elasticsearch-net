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

using System.Runtime.Serialization;

namespace Nest
{
	[MapsApi("ml.stop_datafeed.json")]
	public partial interface IStopDatafeedRequest
	{
		/// <summary>
		/// If true, the datafeed is stopped forcefully.
		/// </summary>
		[DataMember(Name ="force")]
		bool? Force { get; set; }

		/// <summary>
		/// Controls the amount of time to wait until a datafeed stops.
		/// </summary>
		[DataMember(Name ="timeout")]
		Time Timeout { get; set; }
	}

	/// <inheritdoc />
	public partial class StopDatafeedRequest
	{
		/// <inheritdoc />
		public bool? Force { get; set; }

		/// <inheritdoc />
		public Time Timeout { get; set; }
	}

	/// <inheritdoc />
	public partial class StopDatafeedDescriptor
	{
		bool? IStopDatafeedRequest.Force { get; set; }
		Time IStopDatafeedRequest.Timeout { get; set; }

		/// <inheritdoc />
		public StopDatafeedDescriptor Timeout(Time timeout) => Assign(timeout, (a, v) => a.Timeout = v);

		/// <inheritdoc />
		public StopDatafeedDescriptor Force(bool? force = true) => Assign(force, (a, v) => a.Force = v);
	}
}
