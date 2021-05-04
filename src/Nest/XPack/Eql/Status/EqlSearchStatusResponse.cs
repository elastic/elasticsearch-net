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
	/// <summary>
	/// A response to an EQL get status request.
	/// </summary>
	public class EqlSearchStatusResponse : ResponseBase
	{
		/// <summary>
		/// For a completed search shows the http status code of the completed search.
		/// </summary>
		[DataMember(Name = "completion_status")]
		public int CompletionStatus { get; internal set; }

		/// <summary>
		/// For a running search shows a timestamp when the eql search started, in milliseconds since the Unix epoch.
		/// </summary>
		[DataMember(Name = "expiration_time_in_millis")]
		public long ExpirationTimeInMillis { get; internal set; }

		/// <summary>
		/// Identifier for the search.
		/// </summary>
		[DataMember(Name = "id")]
		public string Id { get; internal set; }

		/// <summary>
		/// If true, the response does not contain complete search results.
		/// </summary>
		[DataMember(Name = "is_partial")]
		public bool IsPartial { get; internal set; }

		/// <summary>
		/// If true, the search request is still executing. If false, the search is completed.
		/// </summary>
		[DataMember(Name = "is_running")]
		public bool IsRunning { get; internal set; }

		/// <summary>
		/// For a running search shows a timestamp when the eql search started, in milliseconds since the Unix epoch.
		/// </summary>
		[DataMember(Name = "start_time_in_millis")]
		public long StartTimeInMillis { get; internal set; }
	}
}
