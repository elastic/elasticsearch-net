// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Runtime.Serialization;

namespace Nest
{
	/// <summary>
	/// A response to an SQL get status request.
	/// </summary>
	public class SqlSearchStatusResponse : ResponseBase
	{
		/// <summary>
		/// For a completed search shows the http status code of the completed search.
		/// </summary>
		[DataMember(Name = "completion_status")]
		public int? CompletionStatus { get; internal set; }

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
