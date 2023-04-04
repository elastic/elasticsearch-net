// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Threading.Tasks;
using System.Threading;

namespace Elastic.Clients.Elasticsearch;

public partial class ElasticsearchClient
{
	public virtual CreateResponse Create<TDocument>(TDocument document, IndexName index, Id id)
	{
		var descriptor = new CreateRequestDescriptor<TDocument>(document, index, id);
		descriptor.BeforeRequest();
		return DoRequest<CreateRequestDescriptor<TDocument>, CreateResponse, CreateRequestParameters>(descriptor);
	}

	public virtual Task<CreateResponse> CreateAsync<TDocument>(TDocument document, IndexName index, Id id, CancellationToken cancellationToken = default)
	{
		var descriptor = new CreateRequestDescriptor<TDocument>(document, index, id);
		descriptor.BeforeRequest();
		return DoRequestAsync<CreateRequestDescriptor<TDocument>, CreateResponse, CreateRequestParameters>(descriptor);
	}

	public virtual IndexResponse Index<TDocument>(TDocument document)
	{
		var descriptor = new IndexRequestDescriptor<TDocument>(document);
		descriptor.BeforeRequest();
		return DoRequest<IndexRequestDescriptor<TDocument>, IndexResponse, IndexRequestParameters>(descriptor);
	}

	public virtual IndexResponse Index<TDocument>(TDocument document, IndexName index)
	{
		var descriptor = new IndexRequestDescriptor<TDocument>(document, index);
		descriptor.BeforeRequest();
		return DoRequest<IndexRequestDescriptor<TDocument>, IndexResponse, IndexRequestParameters>(descriptor);
	}

	public virtual Task<IndexResponse> IndexAsync<TDocument>(TDocument document,CancellationToken cancellationToken = default)
	{
		var descriptor = new IndexRequestDescriptor<TDocument>(document);
		descriptor.BeforeRequest();
		return DoRequestAsync<IndexRequestDescriptor<TDocument>, IndexResponse, IndexRequestParameters>(descriptor);
	}

	public virtual Task<IndexResponse> IndexAsync<TDocument>(TDocument document, IndexName index, CancellationToken cancellationToken = default)
	{
		var descriptor = new IndexRequestDescriptor<TDocument>(document, index);
		descriptor.BeforeRequest();
		return DoRequestAsync<IndexRequestDescriptor<TDocument>, IndexResponse, IndexRequestParameters>(descriptor);
	}

	public virtual Task<UpdateResponse<TDocument>> UpdateAsync<TDocument, TPartialDocument>(IndexName index, Id id, CancellationToken cancellationToken = default)
	{
		var descriptor = new UpdateRequestDescriptor<TDocument, TPartialDocument>(index, id);
		return DoRequestAsync<UpdateRequestDescriptor<TDocument, TPartialDocument>, UpdateResponse<TDocument>, UpdateRequestParameters>(descriptor, cancellationToken);
	}

	public virtual Task<UpdateResponse<TDocument>> UpdateAsync<TDocument, TPartialDocument>(IndexName index, Id id, Action<UpdateRequestDescriptor<TDocument, TPartialDocument>> configureRequest, CancellationToken cancellationToken = default)
	{
		var descriptor = new UpdateRequestDescriptor<TDocument, TPartialDocument>(index, id);
		configureRequest?.Invoke(descriptor);
		return DoRequestAsync<UpdateRequestDescriptor<TDocument, TPartialDocument>, UpdateResponse<TDocument>, UpdateRequestParameters>(descriptor, cancellationToken);
	}

	public virtual UpdateResponse<TDocument> Update<TDocument, TPartialDocument>(TDocument document, TPartialDocument partialDocument, IndexName index, Id id)
	{
		var descriptor = new UpdateRequestDescriptor<TDocument, TPartialDocument>(document, index, id);
		descriptor.BeforeRequest();
		return DoRequest<UpdateRequestDescriptor<TDocument, TPartialDocument>, UpdateResponse<TDocument>, UpdateRequestParameters>(descriptor);
	}

	public virtual Task<UpdateResponse<TDocument>> UpdateAsync<TDocument, TPartialDocument>(TDocument document, TPartialDocument partialDocument, IndexName index, Id id, CancellationToken cancellationToken = default)
	{
		var descriptor = new UpdateRequestDescriptor<TDocument, TPartialDocument>(document, index, id);
		descriptor.BeforeRequest();
		return DoRequestAsync<UpdateRequestDescriptor<TDocument, TPartialDocument>, UpdateResponse<TDocument>, UpdateRequestParameters>(descriptor);
	}


	// TODO: Test and introduce in a future release
	//public virtual Task<UpdateResponse<TDocument>> UpdateAsync<TDocument>(IndexName index, Id id, Action<UpdateRequestDescriptor<TDocument, TDocument>> configureRequest, CancellationToken cancellationToken = default)
	//{
	//	var descriptor = new UpdateRequestDescriptor<TDocument, TDocument>(index, id);
	//	configureRequest?.Invoke(descriptor);
	//	return DoRequestAsync<UpdateRequestDescriptor<TDocument, TDocument>, UpdateResponse<TDocument>, UpdateRequestParameters>(descriptor, cancellationToken);
	//}

	// TODO: Test and introduce in a future release
	//public virtual Task<UpdateResponse<TPartialDocument>> UpdateAsync<TPartialDocument>(IndexName index, Id id, TPartialDocument doc, CancellationToken cancellationToken = default)
	//{
	//	var descriptor = new UpdateRequestDescriptor<TPartialDocument, TPartialDocument>(index, id);
	//	descriptor.Doc(doc);
	//	return DoRequestAsync<UpdateRequestDescriptor<TPartialDocument, TPartialDocument>, UpdateResponse<TPartialDocument>, UpdateRequestParameters>(descriptor, cancellationToken);
	//}

	// TODO - Add methods to infer index and/or ID + use expressions as we know the document type.

	public virtual UpdateResponse<TDocument> Update<TDocument, TPartialDocument>(IndexName index, Id id)
	{
		var descriptor = new UpdateRequestDescriptor<TDocument, TPartialDocument>(index, id);
		return DoRequest<UpdateRequestDescriptor<TDocument, TPartialDocument>, UpdateResponse<TDocument>, UpdateRequestParameters>(descriptor);
	}

	public virtual UpdateResponse<TDocument> Update<TDocument, TPartialDocument>(IndexName index, Id id, Action<UpdateRequestDescriptor<TDocument, TPartialDocument>> configureRequest)
	{
		var descriptor = new UpdateRequestDescriptor<TDocument, TPartialDocument>(index, id);
		configureRequest?.Invoke(descriptor);
		return DoRequest<UpdateRequestDescriptor<TDocument, TPartialDocument>, UpdateResponse<TDocument>, UpdateRequestParameters>(descriptor);
	}
}
