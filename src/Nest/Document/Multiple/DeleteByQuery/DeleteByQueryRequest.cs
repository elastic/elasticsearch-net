using System;
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

		/// <summary>
		/// A match_all query to select all documents. Convenient shorthand for specifying
		/// a match_all query using <see cref="Query" />
		/// </summary>
		public DeleteByQueryDescriptor<TDocument> MatchAll() => Assign(new QueryContainerDescriptor<TDocument>().MatchAll(), (a, v) => a.Query = v);

		/// <summary>
		/// The query to use to select documents for deletion
		/// </summary>
		public DeleteByQueryDescriptor<TDocument> Query(Func<QueryContainerDescriptor<TDocument>, QueryContainer> querySelector) =>
			Assign(querySelector, (a, v) => a.Query = v?.Invoke(new QueryContainerDescriptor<TDocument>()));

		/// <summary>
		/// Parallelize the deleting process. This parallelization can improve efficiency and
		/// provide a convenient way to break the request down into smaller parts.
		/// </summary>
		public DeleteByQueryDescriptor<TDocument> Slice(Func<SlicedScrollDescriptor<TDocument>, ISlicedScroll> selector) =>
			Assign(selector, (a, v) => a.Slice = v?.Invoke(new SlicedScrollDescriptor<TDocument>()));
	}
}
