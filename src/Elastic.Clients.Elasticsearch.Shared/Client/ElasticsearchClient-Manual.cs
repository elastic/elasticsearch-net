// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Threading.Tasks;
using System.Threading;

#if ELASTICSEARCH_SERVERLESS
namespace Elastic.Clients.Elasticsearch.Serverless;
#else
namespace Elastic.Clients.Elasticsearch;
#endif

public partial class ElasticsearchClient
{
	/// <summary>
	/// <para>Creates a new document in the index.</para>
	/// <para>Returns a 409 response when a document with a same ID already exists in the index.</para>
	/// <para><see href="https://www.elastic.co/guide/en/elasticsearch/reference/master/docs-index_.html">Learn more about this API in the Elasticsearch documentation.</see></para>
	/// </summary>
	public virtual CreateResponse Create<TDocument>(TDocument document, IndexName index, Id id)
	{
		var descriptor = new CreateRequestDescriptor<TDocument>(document, index, id);
		descriptor.BeforeRequest();
		return DoRequest<CreateRequestDescriptor<TDocument>, CreateResponse, CreateRequestParameters>(descriptor);
	}

	/// <summary>
	/// <para>Creates a new document in the index.</para>
	/// <para>Returns a 409 response when a document with a same ID already exists in the index.</para>
	/// <para><see href="https://www.elastic.co/guide/en/elasticsearch/reference/master/docs-index_.html">Learn more about this API in the Elasticsearch documentation.</see></para>
	/// </summary>
	public virtual Task<CreateResponse> CreateAsync<TDocument>(TDocument document, IndexName index, Id id, CancellationToken cancellationToken = default)
	{
		var descriptor = new CreateRequestDescriptor<TDocument>(document, index, id);
		descriptor.BeforeRequest();
		return DoRequestAsync<CreateRequestDescriptor<TDocument>, CreateResponse, CreateRequestParameters>(descriptor);
	}

	/// <summary>
	/// <para>Creates or updates a document in an index.</para>
	/// <para><see href="https://www.elastic.co/guide/en/elasticsearch/reference/master/docs-index_.html">Learn more about this API in the Elasticsearch documentation.</see></para>
	/// </summary>
	public virtual IndexResponse Index<TDocument>(TDocument document)
	{
		var descriptor = new IndexRequestDescriptor<TDocument>(document);
		descriptor.BeforeRequest();
		return DoRequest<IndexRequestDescriptor<TDocument>, IndexResponse, IndexRequestParameters>(descriptor);
	}

	/// <summary>
	/// <para>Creates or updates a document in an index.</para>
	/// <para><see href="https://www.elastic.co/guide/en/elasticsearch/reference/master/docs-index_.html">Learn more about this API in the Elasticsearch documentation.</see></para>
	/// </summary>
	public virtual IndexResponse Index<TDocument>(TDocument document, IndexName index)
	{
		var descriptor = new IndexRequestDescriptor<TDocument>(document, index);
		descriptor.BeforeRequest();
		return DoRequest<IndexRequestDescriptor<TDocument>, IndexResponse, IndexRequestParameters>(descriptor);
	}

	/// <summary>
	/// <para>Creates or updates a document in an index.</para>
	/// <para><see href="https://www.elastic.co/guide/en/elasticsearch/reference/master/docs-index_.html">Learn more about this API in the Elasticsearch documentation.</see></para>
	/// </summary>
	public virtual Task<IndexResponse> IndexAsync<TDocument>(TDocument document,CancellationToken cancellationToken = default)
	{
		var descriptor = new IndexRequestDescriptor<TDocument>(document);
		descriptor.BeforeRequest();
		return DoRequestAsync<IndexRequestDescriptor<TDocument>, IndexResponse, IndexRequestParameters>(descriptor);
	}

	/// <summary>
	/// <para>Creates or updates a document in an index.</para>
	/// <para><see href="https://www.elastic.co/guide/en/elasticsearch/reference/master/docs-index_.html">Learn more about this API in the Elasticsearch documentation.</see></para>
	/// </summary>
	public virtual Task<IndexResponse> IndexAsync<TDocument>(TDocument document, IndexName index, CancellationToken cancellationToken = default)
	{
		var descriptor = new IndexRequestDescriptor<TDocument>(document, index);
		descriptor.BeforeRequest();
		return DoRequestAsync<IndexRequestDescriptor<TDocument>, IndexResponse, IndexRequestParameters>(descriptor);
	}

	/// <summary>
	/// <para>Updates a document with a script or partial document.</para>
	/// <para><see href="https://www.elastic.co/guide/en/elasticsearch/reference/master/docs-update.html">Learn more about this API in the Elasticsearch documentation.</see></para>
	/// </summary>
	public virtual Task<UpdateResponse<TDocument>> UpdateAsync<TDocument, TPartialDocument>(IndexName index, Id id, CancellationToken cancellationToken = default)
	{
		var descriptor = new UpdateRequestDescriptor<TDocument, TPartialDocument>(index, id);
		return DoRequestAsync<UpdateRequestDescriptor<TDocument, TPartialDocument>, UpdateResponse<TDocument>, UpdateRequestParameters>(descriptor, cancellationToken);
	}

	/// <summary>
	/// <para>Updates a document with a script or partial document.</para>
	/// <para><see href="https://www.elastic.co/guide/en/elasticsearch/reference/master/docs-update.html">Learn more about this API in the Elasticsearch documentation.</see></para>
	/// </summary>
	public virtual Task<UpdateResponse<TDocument>> UpdateAsync<TDocument, TPartialDocument>(IndexName index, Id id, Action<UpdateRequestDescriptor<TDocument, TPartialDocument>> configureRequest, CancellationToken cancellationToken = default)
	{
		var descriptor = new UpdateRequestDescriptor<TDocument, TPartialDocument>(index, id);
		configureRequest?.Invoke(descriptor);
		return DoRequestAsync<UpdateRequestDescriptor<TDocument, TPartialDocument>, UpdateResponse<TDocument>, UpdateRequestParameters>(descriptor, cancellationToken);
	}

	/// <summary>
	/// <para>Updates a document with a script or partial document.</para>
	/// <para><see href="https://www.elastic.co/guide/en/elasticsearch/reference/master/docs-update.html">Learn more about this API in the Elasticsearch documentation.</see></para>
	/// </summary>
	public virtual UpdateResponse<TDocument> Update<TDocument, TPartialDocument>(TDocument document, TPartialDocument partialDocument, IndexName index, Id id)
	{
		var descriptor = new UpdateRequestDescriptor<TDocument, TPartialDocument>(document, index, id);
		descriptor.BeforeRequest();
		return DoRequest<UpdateRequestDescriptor<TDocument, TPartialDocument>, UpdateResponse<TDocument>, UpdateRequestParameters>(descriptor);
	}

	/// <summary>
	/// <para>Updates a document with a script or partial document.</para>
	/// <para><see href="https://www.elastic.co/guide/en/elasticsearch/reference/master/docs-update.html">Learn more about this API in the Elasticsearch documentation.</see></para>
	/// </summary>
	public virtual Task<UpdateResponse<TDocument>> UpdateAsync<TDocument, TPartialDocument>(TDocument document, TPartialDocument partialDocument, IndexName index, Id id, CancellationToken cancellationToken = default)
	{
		var descriptor = new UpdateRequestDescriptor<TDocument, TPartialDocument>(document, index, id);
		descriptor.BeforeRequest();
		return DoRequestAsync<UpdateRequestDescriptor<TDocument, TPartialDocument>, UpdateResponse<TDocument>, UpdateRequestParameters>(descriptor);
	}

	/// <summary>
	/// <para>Updates a document with a script or partial document.</para>
	/// <para><see href="https://www.elastic.co/guide/en/elasticsearch/reference/master/docs-update.html">Learn more about this API in the Elasticsearch documentation.</see></para>
	/// </summary>
	public virtual UpdateResponse<TDocument> Update<TDocument, TPartialDocument>(IndexName index, Id id)
	{
		var descriptor = new UpdateRequestDescriptor<TDocument, TPartialDocument>(index, id);
		return DoRequest<UpdateRequestDescriptor<TDocument, TPartialDocument>, UpdateResponse<TDocument>, UpdateRequestParameters>(descriptor);
	}

	/// <summary>
	/// <para>Updates a document with a script or partial document.</para>
	/// <para><see href="https://www.elastic.co/guide/en/elasticsearch/reference/master/docs-update.html">Learn more about this API in the Elasticsearch documentation.</see></para>
	/// </summary>
	public virtual UpdateResponse<TDocument> Update<TDocument, TPartialDocument>(IndexName index, Id id, Action<UpdateRequestDescriptor<TDocument, TPartialDocument>> configureRequest)
	{
		var descriptor = new UpdateRequestDescriptor<TDocument, TPartialDocument>(index, id);
		configureRequest?.Invoke(descriptor);
		return DoRequest<UpdateRequestDescriptor<TDocument, TPartialDocument>, UpdateResponse<TDocument>, UpdateRequestParameters>(descriptor);
	}
}
