// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System;
using System.Runtime.Serialization;

namespace Nest
{
	/// <summary>
	/// Delete documents that match a given query
	/// </summary>
	public partial interface IDeleteByQueryRequest
	{
		/// <summary>
		/// The query to use to select documents for deletion
		/// </summary>
		[DataMember(Name ="query")]
		QueryContainer Query { get; set; }

		/// <summary>
		/// Parallelize the deleting process. This parallelization can improve efficiency and
		/// provide a convenient way to break the request down into smaller parts.
		/// </summary>
		[DataMember(Name ="slice")]
		ISlicedScroll Slice { get; set; }

		/// <summary>
		/// Limit the number of processed documents
		/// </summary>
		[DataMember(Name ="max_docs")]
		long? MaximumDocuments { get; set; }
	}

	/// <inheritdoc />
	// ReSharper disable once UnusedTypeParameter
	public partial interface IDeleteByQueryRequest<TDocument>  where TDocument : class { }

	/// <inheritdoc cref="IDeleteByQueryRequest" />
	public partial class DeleteByQueryRequest
	{
		/// <inheritdoc />
		public QueryContainer Query { get; set; }

		/// <inheritdoc />
		public ISlicedScroll Slice { get; set; }

		/// <inheritdoc />
		public long? MaximumDocuments { get; set; }
	}

	/// <inheritdoc cref="IDeleteByQueryRequest" />
	// ReSharper disable once UnusedTypeParameter
	public partial class DeleteByQueryRequest<TDocument> where TDocument : class
	{

	}

	/// <inheritdoc cref="IDeleteByQueryRequest" />
	public partial class DeleteByQueryDescriptor<TDocument> : IDeleteByQueryRequest<TDocument>
		where TDocument : class
	{
		QueryContainer IDeleteByQueryRequest.Query { get; set; }
		ISlicedScroll IDeleteByQueryRequest.Slice { get; set; }
		long? IDeleteByQueryRequest.MaximumDocuments { get; set; }

		/// <summary>
		/// A match_all query to select all documents. Convenient shorthand for specifying
		/// a match_all query using <see cref="Query" />
		/// </summary>
		public DeleteByQueryDescriptor<TDocument> MatchAll() => Assign(new QueryContainerDescriptor<TDocument>().MatchAll(), (a, v) => a.Query = v);

		/// <inheritdoc cref="IDeleteByQueryRequest.Query"/>
		public DeleteByQueryDescriptor<TDocument> Query(Func<QueryContainerDescriptor<TDocument>, QueryContainer> querySelector) =>
			Assign(querySelector, (a, v) => a.Query = v?.Invoke(new QueryContainerDescriptor<TDocument>()));

		/// <inheritdoc cref="IDeleteByQueryRequest.Slice"/>
		public DeleteByQueryDescriptor<TDocument> Slice(Func<SlicedScrollDescriptor<TDocument>, ISlicedScroll> selector) =>
			Assign(selector, (a, v) => a.Slice = v?.Invoke(new SlicedScrollDescriptor<TDocument>()));

		/// <inheritdoc cref="IDeleteByQueryRequest.MaximumDocuments"/>
		public DeleteByQueryDescriptor<TDocument> MaximumDocuments(long? maximumDocuments) =>
			Assign(maximumDocuments, (a, v) => a.MaximumDocuments = v);
	}
}
