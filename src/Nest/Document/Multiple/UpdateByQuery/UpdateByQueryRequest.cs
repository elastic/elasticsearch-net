// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Runtime.Serialization;

namespace Nest
{
	[MapsApi("update_by_query.json")]
	public partial interface IUpdateByQueryRequest
	{
		/// <summary>
		/// Parallelize the update process by splitting a query into
		/// multiple slices.
		/// </summary>
		[DataMember(Name ="slice")]
		ISlicedScroll Slice { get; set; }

		/// <summary>
		/// Query to select documents to update
		/// </summary>
		[DataMember(Name ="query")]
		QueryContainer Query { get; set; }

		/// <summary>
		/// A script specifying the update to make
		/// </summary>
		[DataMember(Name ="script")]
		IScript Script { get; set; }

		/// <summary>
		/// Limit the number of processed documents
		/// </summary>
		[DataMember(Name ="max_docs")]
		long? MaximumDocuments { get; set; }
	}

	// ReSharper disable once UnusedMember.Global
	// ReSharper disable once UnusedTypeParameter
	public partial interface IUpdateByQueryRequest<TDocument> where TDocument : class { }

	public partial class UpdateByQueryRequest
	{
		/// <inheritdoc />
		public ISlicedScroll Slice { get; set; }
		/// <inheritdoc />
		public QueryContainer Query { get; set; }
		/// <inheritdoc />
		public IScript Script { get; set; }
		/// <inheritdoc />
		public long? MaximumDocuments { get; set; }
	}

	// ReSharper disable once UnusedTypeParameter
	public partial class UpdateByQueryRequest<TDocument> where TDocument : class
	{
	}

	public partial class UpdateByQueryDescriptor<TDocument>
		where TDocument : class
	{
		QueryContainer IUpdateByQueryRequest.Query { get; set; }
		IScript IUpdateByQueryRequest.Script { get; set; }
		long? IUpdateByQueryRequest.MaximumDocuments { get; set; }
		ISlicedScroll IUpdateByQueryRequest.Slice { get; set; }

		/// <summary>
		/// Query that selects all documents
		/// </summary>
		public UpdateByQueryDescriptor<TDocument> MatchAll() => Assign(new QueryContainerDescriptor<TDocument>().MatchAll(), (a, v) => a.Query = v);

		/// <inheritdoc cref="IUpdateByQueryRequest.Query"/>
		public UpdateByQueryDescriptor<TDocument> Query(Func<QueryContainerDescriptor<TDocument>, QueryContainer> querySelector) =>
			Assign(querySelector, (a, v) => a.Query = v?.Invoke(new QueryContainerDescriptor<TDocument>()));

		/// <inheritdoc cref="IUpdateByQueryRequest.Script"/>
		public UpdateByQueryDescriptor<TDocument> Script(string script) => Assign((InlineScript)script, (a, v) => a.Script = v);

		/// <inheritdoc cref="IUpdateByQueryRequest.Script"/>
		public UpdateByQueryDescriptor<TDocument> Script(Func<ScriptDescriptor, IScript> scriptSelector) =>
			Assign(scriptSelector, (a, v) => a.Script = v?.Invoke(new ScriptDescriptor()));

		/// <inheritdoc cref="IUpdateByQueryRequest.MaximumDocuments"/>
		public UpdateByQueryDescriptor<TDocument> MaximumDocuments(long? maximumDocuments) =>
			Assign(maximumDocuments, (a, v) => a.MaximumDocuments = v);

		/// <inheritdoc cref="IUpdateByQueryRequest.Slice"/>
		public UpdateByQueryDescriptor<TDocument> Slice(Func<SlicedScrollDescriptor<TDocument>, ISlicedScroll> selector) =>
			Assign(selector, (a, v) => a.Slice = v?.Invoke(new SlicedScrollDescriptor<TDocument>()));
	}
}
