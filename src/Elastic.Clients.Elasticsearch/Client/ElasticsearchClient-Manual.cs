// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Threading.Tasks;
using System.Threading;

namespace Elastic.Clients.Elasticsearch
{
	public partial class ElasticsearchClient
	{
		public IndexResponse Index<TDocument>(TDocument document, Action<IndexRequestDescriptor<TDocument>> configureRequest)
		{
			var descriptor = new IndexRequestDescriptor<TDocument>(documentWithId: document);
			configureRequest?.Invoke(descriptor);
			return DoRequest<IndexRequestDescriptor<TDocument>, IndexResponse>(descriptor);
		}

		public Task<IndexResponse> IndexAsync<TDocument>(TDocument document, Action<IndexRequestDescriptor<TDocument>> configureRequest, CancellationToken cancellationToken = default)
		{
			var descriptor = new IndexRequestDescriptor<TDocument>(documentWithId: document);
			configureRequest?.Invoke(descriptor);
			return DoRequestAsync<IndexRequestDescriptor<TDocument>, IndexResponse>(descriptor);
		}

		public CreateResponse Create<TDocument>(TDocument document, Action<CreateRequestDescriptor<TDocument>> configureRequest)
		{
			var descriptor = new CreateRequestDescriptor<TDocument>(document);
			configureRequest?.Invoke(descriptor);
			return DoRequest<CreateRequestDescriptor<TDocument>, CreateResponse>(descriptor);
		}

		public Task<CreateResponse> CreateAsync<TDocument>(TDocument document, Action<CreateRequestDescriptor<TDocument>> configureRequest, CancellationToken cancellationToken = default)
		{
			var descriptor = new CreateRequestDescriptor<TDocument>(document);
			configureRequest?.Invoke(descriptor);
			return DoRequestAsync<CreateRequestDescriptor<TDocument>, CreateResponse>(descriptor);
		}

		public DeleteResponse Delete<TDocument>(Id id, Action<DeleteRequestDescriptor<TDocument>> configureRequest)
		{
			var descriptor = new DeleteRequestDescriptor<TDocument>(id);
			configureRequest?.Invoke(descriptor);
			return DoRequest<DeleteRequestDescriptor<TDocument>, DeleteResponse>(descriptor);
		}

		public Task<DeleteResponse> DeleteAsync<TDocument>(Id id, Action<DeleteRequestDescriptor<TDocument>> configureRequest, CancellationToken cancellationToken = default)
		{
			var descriptor = new DeleteRequestDescriptor<TDocument>(id);
			configureRequest?.Invoke(descriptor);
			return DoRequestAsync<DeleteRequestDescriptor<TDocument>, DeleteResponse>(descriptor);
		}

		public Task<UpdateResponse<TDocument>> UpdateAsync<TDocument, TPartialDocument>(IndexName index, Id id, Action<UpdateRequestDescriptor<TDocument, TPartialDocument>> configureRequest = null, CancellationToken cancellationToken = default)
		{
			var descriptor = new UpdateRequestDescriptor<TDocument, TPartialDocument>(index, id);
			configureRequest?.Invoke(descriptor);
			return DoRequestAsync<UpdateRequestDescriptor<TDocument, TPartialDocument>, UpdateResponse<TDocument>>(descriptor);
		}

		public UpdateResponse<TDocument> Update<TDocument, TPartialDocument>(IndexName index, Id id, Action<UpdateRequestDescriptor<TDocument, TPartialDocument>> configureRequest = null)
		{
			var descriptor = new UpdateRequestDescriptor<TDocument, TPartialDocument>(index, id);
			configureRequest?.Invoke(descriptor);
			return DoRequest<UpdateRequestDescriptor<TDocument, TPartialDocument>, UpdateResponse<TDocument>>(descriptor);
		}

		public SourceResponse<TDocument> Source<TDocument>(DocumentPath<TDocument> id, Action<SourceRequestDescriptor<TDocument>> configureRequest = null)
		{
			var descriptor = new SourceRequestDescriptor<TDocument>(document: id.Document, index: id?.Self?.Index ?? typeof(TDocument), id: id?.Self?.Id ?? Id.From(id.Document));
			configureRequest?.Invoke(descriptor);
			return DoRequest<SourceRequestDescriptor<TDocument>, SourceResponse<TDocument>>(descriptor);
		}

		public CountResponse Count<TDocument>(Action<CountRequestDescriptor<TDocument>> configureRequest = null)
		{
			var descriptor = new CountRequestDescriptor<TDocument>();
			configureRequest?.Invoke(descriptor);
			descriptor.BeforeRequest();
			return DoRequest<CountRequestDescriptor<TDocument>, CountResponse>(descriptor);
		}

		public Task<CountResponse> CountAsync<TDocument>(Action<CountRequestDescriptor<TDocument>> configureRequest = null, CancellationToken cancellationToken = default)
		{
			var descriptor = new CountRequestDescriptor<TDocument>();
			configureRequest?.Invoke(descriptor);
			descriptor.BeforeRequest();
			return DoRequestAsync<CountRequestDescriptor<TDocument>, CountResponse>(descriptor);
		}
	}
}
