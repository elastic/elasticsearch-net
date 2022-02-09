// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Threading;
using Elastic.Clients.Elasticsearch.Helpers;

namespace Elastic.Clients.Elasticsearch;

public partial interface IElasticsearchClient
{
	//HelpersNamespace Helpers { get; }

	BulkAllObservable<T> BulkAll<T>(IEnumerable<T> documents, Action<BulkAllRequestDescriptor<T>> configure, CancellationToken cancellationToken = default);

    BulkAllObservable<T> BulkAll<T>(IBulkAllRequest<T> request, CancellationToken cancellationToken = default);
}

public partial class ElasticsearchClient
{
	//public HelpersNamespace Helpers { get; private set; }

	public BulkAllObservable<T> BulkAll<T>(IEnumerable<T> documents, Action<BulkAllRequestDescriptor<T>> configure, CancellationToken cancellationToken = default)
    {
        var descriptor = new BulkAllRequestDescriptor<T>(documents);
        configure?.Invoke(descriptor);
        return BulkAll<T>(descriptor, cancellationToken);
    }

    public BulkAllObservable<T> BulkAll<T>(IBulkAllRequest<T> request, CancellationToken cancellationToken = default) =>
        new(this, request, cancellationToken);
}

//public class HelpersNamespace : NamespacedClientProxy
//{
//	private readonly ElasticsearchClient _client;

//	internal HelpersNamespace(ElasticsearchClient client) : base(client) => _client = client;

//	public BulkAllObservable<T> BulkAllObservable<T>(IEnumerable<T> documents, Action<BulkAllRequestDescriptor<T>> configure, CancellationToken cancellationToken = default)
//	{
//		var descriptor = new BulkAllRequestDescriptor<T>(documents);
//		configure?.Invoke(descriptor);
//		return BulkAllObservable<T>(descriptor, cancellationToken);
//	}

//	public BulkAllObservable<T> BulkAllObservable<T>(IBulkAllRequest<T> request, CancellationToken cancellationToken = default) =>
//		new(_client, request, cancellationToken);
//}
