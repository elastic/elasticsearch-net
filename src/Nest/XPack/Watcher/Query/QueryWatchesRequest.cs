// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	[MapsApi("watcher.query_watches.json")]
	[ReadAs(typeof(QueryWatchesRequest))]
	public partial interface IQueryWatchesRequest
	{
		/// <summary>
		/// The offset from the first result to fetch. Needs to be non-negative.
		/// </summary>
		[DataMember(Name = "from")]
		int? From { get; set; }

		/// <summary>
		/// Optional, query filter watches to be returned.
		/// </summary>
		[DataMember(Name = "query")]
		QueryContainer Query { get; set; }

		/// <summary>
		///  Sort values that can be used to start returning results "after" any document in the result list.
		/// </summary>
		[DataMember(Name = "search_after")]
		IList<object> SearchAfter { get; set; }

		/// <summary>
		/// The number of hits to return. Defaults to 10.
		/// </summary>
		[DataMember(Name = "size")]
		int? Size { get; set; }

		/// <summary>
		/// Specifies how to sort the search hits.
		/// </summary>
		[DataMember(Name = "sort")]
		IList<ISort> Sort { get; set; }
	}

	/// <inheritdoc cref="IQueryWatchesRequest" />
	public partial class QueryWatchesRequest
	{
		/// <inheritdoc />
		public int? From { get; set; }

		/// <inheritdoc />
		public QueryContainer Query { get; set; }

		/// <inheritdoc />
		public IList<object> SearchAfter { get; set; }

		/// <inheritdoc />
		public int? Size { get; set; }

		/// <inheritdoc />
		public IList<ISort> Sort { get; set; }
	}

	/// <inheritdoc cref="IQueryWatchesRequest" />
	public partial class QueryWatchesDescriptor
	{
		int? IQueryWatchesRequest.From { get; set; }
		QueryContainer IQueryWatchesRequest.Query { get; set; }
		IList<object> IQueryWatchesRequest.SearchAfter { get; set; }
		int? IQueryWatchesRequest.Size { get; set; }
		IList<ISort> IQueryWatchesRequest.Sort { get; set; }

		/// <inheritdoc cref="IQueryWatchesRequest.From" />
		public QueryWatchesDescriptor From(int? from) => Assign(from, (a, v) => a.From = v);

		/// <inheritdoc cref="IQueryWatchesRequest.Query" />
		public QueryWatchesDescriptor Query(Func<QueryContainerDescriptor<Watch>, QueryContainer> query) =>
			Assign(query, (a, v) => a.Query = v?.Invoke(new QueryContainerDescriptor<Watch>()));

		/// <inheritdoc cref="IQueryWatchesRequest.SearchAfter" />
		public QueryWatchesDescriptor SearchAfter(IEnumerable<object> searchAfter) =>
			Assign(searchAfter, (a, v) => a.SearchAfter = v?.ToListOrNullIfEmpty());

		/// <inheritdoc cref="IQueryWatchesRequest.SearchAfter" />
		public QueryWatchesDescriptor SearchAfter(IList<object> searchAfter) => Assign(searchAfter, (a, v) => a.SearchAfter = v);

		/// <inheritdoc cref="IQueryWatchesRequest.SearchAfter" />
		public QueryWatchesDescriptor SearchAfter(params object[] searchAfter) => Assign(searchAfter, (a, v) => a.SearchAfter = v);

		/// <inheritdoc cref="IQueryWatchesRequest.Size" />
		public QueryWatchesDescriptor Size(int? size) => Assign(size, (a, v) => a.Size = v);

		/// <inheritdoc cref="IQueryWatchesRequest.Sort" />
		public QueryWatchesDescriptor Sort(Func<SortDescriptor<Watch>, IPromise<IList<ISort>>> selector) =>
			Assign(selector, (a, v) => a.Sort = v?.Invoke(new SortDescriptor<Watch>())?.Value);
	}
}
