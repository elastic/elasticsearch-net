// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Threading.Tasks;
using System.Threading;

namespace Elastic.Clients.Elasticsearch;

public partial class ElasticsearchClient
{
	[Obsolete("Synchronous methods are deprecated and could be removed in the future.")]
	public virtual UpdateResponse<TDocument> Update<TDocument, TPartialDocument>(TDocument document, TPartialDocument partialDocument, IndexName index, Id id)
	{
		var descriptor = new UpdateRequestDescriptor<TDocument, TPartialDocument>(index, id);
		descriptor.Upsert(document);
		descriptor.Doc(partialDocument);
		descriptor.BeforeRequest();
		return DoRequest<UpdateRequestDescriptor<TDocument, TPartialDocument>, UpdateResponse<TDocument>, UpdateRequestParameters>(descriptor);
	}

	public virtual Task<UpdateResponse<TDocument>> UpdateAsync<TDocument, TPartialDocument>(TDocument document, TPartialDocument partialDocument, IndexName index, Id id, CancellationToken cancellationToken = default)
	{
		var descriptor = new UpdateRequestDescriptor<TDocument, TPartialDocument>(index, id);
		descriptor.Upsert(document);
		descriptor.Doc(partialDocument);
		descriptor.BeforeRequest();
		return DoRequestAsync<UpdateRequestDescriptor<TDocument, TPartialDocument>, UpdateResponse<TDocument>, UpdateRequestParameters>(descriptor, cancellationToken);
	}

	public virtual Task<UpdateResponse<TDocument>> UpdateAsync<TDocument, TPartialDocument>(TDocument document, TPartialDocument partialDocument, Action<UpdateRequestDescriptor<TDocument, TPartialDocument>> configureRequest, CancellationToken cancellationToken = default)
	{
		var descriptor = new UpdateRequestDescriptor<TDocument, TPartialDocument>(typeof(TDocument), Id.From(document));
		descriptor.Upsert(document);
		descriptor.Doc(partialDocument);
		configureRequest?.Invoke(descriptor);
		descriptor.BeforeRequest();
		return DoRequestAsync<UpdateRequestDescriptor<TDocument, TPartialDocument>, UpdateResponse<TDocument>, UpdateRequestParameters>(descriptor, cancellationToken);
	}
}
