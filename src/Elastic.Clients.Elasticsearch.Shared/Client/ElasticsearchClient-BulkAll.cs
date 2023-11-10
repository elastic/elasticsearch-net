// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

#if !ELASTICSEARCH_SERVERLESS

using System;
using System.Collections.Generic;
using System.Threading;

namespace Elastic.Clients.Elasticsearch;

public partial class ElasticsearchClient
{
	public virtual BulkAllObservable<T> BulkAll<T>(IEnumerable<T> documents, Action<BulkAllRequestDescriptor<T>> configure, CancellationToken cancellationToken = default)
	{
		var descriptor = new BulkAllRequestDescriptor<T>(documents);
		configure?.Invoke(descriptor);
		return BulkAll<T>(descriptor, cancellationToken);
	}

	public virtual BulkAllObservable<T> BulkAll<T>(IBulkAllRequest<T> request, CancellationToken cancellationToken = default) =>
		new(this, request, cancellationToken);
}

#endif
