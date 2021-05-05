// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

namespace Nest
{
	[MapsApi("watcher.query_watches.json")]
	[ReadAs(typeof(QueryWatchesRequest))]
	public partial interface IQueryWatchesRequest
	{
		/// <summary>
		///  Sort values that can be used to start returning results "after" any document in the result list.
		/// </summary>
		//[DataMember(Name = "search_after")]
		//IList<object> SearchAfter { get; set; }
	}

	/// <inheritdoc cref="IQueryWatchesRequest" />
	public partial class QueryWatchesRequest
	{
		/// <inheritdoc />
		//public IList<object> SearchAfter { get; set; }
	}

	/// <inheritdoc cref="IQueryWatchesRequest" />
	public partial class QueryWatchesDescriptor { }
}
