// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	/// <summary>
	/// A response to an EQL search request.
	/// </summary>
	/// <typeparam name="TDocument">The document type.</typeparam>
	public interface IEqlSearchResponse<out TDocument> : IResponse where TDocument : class
	{
		/// <summary>
		/// Gets the collection of hits that matched the query.
		/// </summary>
		/// <value>
		/// The hits.
		/// </value>
		IReadOnlyCollection<IHit<TDocument>> Hits { get; }

		/// <summary>
		/// Gets the meta data about the hits that match the search query criteria.
		/// </summary>
		IHitsMetadata<TDocument> HitsMetadata { get; }
	}

	public class EqlSearchResponse<TDocument> : ResponseBase, IEqlSearchResponse<TDocument> where TDocument : class
	{
		private IReadOnlyCollection<IHit<TDocument>> _hits;

		/// <inheritdoc />
		[IgnoreDataMember]
		public IReadOnlyCollection<IHit<TDocument>> Hits =>
			_hits ??= HitsMetadata?.Hits ?? EmptyReadOnly<IHit<TDocument>>.Collection;

		/// <inheritdoc />
		[DataMember(Name = "hits")]
		public IHitsMetadata<TDocument> HitsMetadata { get; internal set; }
	}
}
