// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Elastic.Clients.Elasticsearch.Core.Bulk;

namespace Elastic.Clients.Elasticsearch;

/// <summary>
/// Provides GetMany extensions that make it easier to get many documents given a list of ids
/// </summary>
public static class IndexManyExtensions
{
#if !ELASTICSEARCH_SERVERLESS

	/// <summary>
	/// Shortcut into the Bulk call that indexes the specified objects
	/// <para> </para>
	/// https://www.elastic.co/guide/en/elasticsearch/reference/current/docs-bulk.html
	/// </summary>
	/// <param name="client"></param>
	/// <typeparam name="T">The type used to infer the default index and typename</typeparam>
	/// <param name="objects">List of objects to index, Id will be inferred (Id property or IdProperty attribute on type)</param>
	/// <param name="type">Override the inferred typename for T</param>
	[Obsolete("Synchronous methods are deprecated and could be removed in the future.")]
	public static BulkResponse IndexMany<T>(this ElasticsearchClient client, IEnumerable<T> @objects)
		where T : class
	{
		var bulkRequest = CreateIndexBulkRequest(objects, null);
		return client.Bulk(bulkRequest);
	}

	/// <summary>
	/// Shortcut into the Bulk call that indexes the specified objects
	/// <para> </para>
	/// https://www.elastic.co/guide/en/elasticsearch/reference/current/docs-bulk.html
	/// </summary>
	/// <param name="client"></param>
	/// <typeparam name="T">The type used to infer the default index and typename</typeparam>
	/// <param name="objects">List of objects to index, Id will be inferred (Id property or IdProperty attribute on type)</param>
	/// <param name="index">Override the inferred indexname for T</param>
	/// <param name="type">Override the inferred typename for T</param>
	[Obsolete("Synchronous methods are deprecated and could be removed in the future.")]
	public static BulkResponse IndexMany<T>(this ElasticsearchClient client, IEnumerable<T> @objects, IndexName index)
		where T : class
	{
		var bulkRequest = CreateIndexBulkRequest(objects, index);
		return client.Bulk(bulkRequest);
	}

#endif

	/// <summary>
	/// Shortcut into the Bulk call that indexes the specified objects
	/// <para> </para>
	/// https://www.elastic.co/guide/en/elasticsearch/reference/current/docs-bulk.html
	/// </summary>
	/// <param name="client"></param>
	/// <typeparam name="T">The type used to infer the default index and typename</typeparam>
	/// <param name="objects">List of objects to index, Id will be inferred (Id property or IdProperty attribute on type)</param>
	/// <param name="type">Override the inferred typename for T</param>
	public static Task<BulkResponse> IndexManyAsync<T>(this ElasticsearchClient client, IEnumerable<T> objects,
		CancellationToken cancellationToken = default
	)
		where T : class
	{
		var bulkRequest = CreateIndexBulkRequest(objects, null);
		return client.BulkAsync(bulkRequest, cancellationToken);
	}

	/// <summary>
	/// Shortcut into the Bulk call that indexes the specified objects
	/// <para> </para>
	/// https://www.elastic.co/guide/en/elasticsearch/reference/current/docs-bulk.html
	/// </summary>
	/// <param name="client"></param>
	/// <typeparam name="T">The type used to infer the default index and typename</typeparam>
	/// <param name="objects">List of objects to index, Id will be inferred (Id property or IdProperty attribute on type)</param>
	/// <param name="index">Override the inferred indexname for T</param>
	/// <param name="type">Override the inferred typename for T</param>
	public static Task<BulkResponse> IndexManyAsync<T>(this ElasticsearchClient client, IEnumerable<T> objects, IndexName index,
		CancellationToken cancellationToken = default
	)
		where T : class
	{
		var bulkRequest = CreateIndexBulkRequest(objects, index);
		return client.BulkAsync(bulkRequest, cancellationToken);
	}

	private static BulkRequest CreateIndexBulkRequest<T>(IEnumerable<T> objects, IndexName index) where T : class
	{
		@objects.ThrowIfEmpty(nameof(objects));

		var bulkRequest = index is null
			? new BulkRequest()
			: new BulkRequest(index);

		var indexOps = @objects
			.Select(o => new BulkIndexOperation<T>(o))
			.Cast<IBulkOperation>()
			.ToList();

		bulkRequest.Operations = new BulkOperationsCollection(indexOps);
		return bulkRequest;
	}
}
