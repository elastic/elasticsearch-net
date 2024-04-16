// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

// TODO: Move away from shared project

#if !ELASTICSEARCH_SERVERLESS

using System.Threading.Tasks;
using System.Threading;
using System;

namespace Elastic.Clients.Elasticsearch.IndexManagement;

public partial class IndicesNamespacedClient
{
	public virtual GetAliasResponse GetAlias(Indices indicies, Action<GetAliasRequestDescriptor> configureRequest)
	{
		var descriptor = new GetAliasRequestDescriptor(indicies, null);
		configureRequest?.Invoke(descriptor);
		descriptor.BeforeRequest();
		return DoRequest<GetAliasRequestDescriptor, GetAliasResponse, GetAliasRequestParameters>(descriptor);
	}

	public virtual Task<GetAliasResponse> GetAliasAsync(Indices indicies, Action<GetAliasRequestDescriptor> configureRequest, CancellationToken cancellationToken = default)
	{
		var descriptor = new GetAliasRequestDescriptor(indicies, null);
		configureRequest?.Invoke(descriptor);
		descriptor.BeforeRequest();
		return DoRequestAsync<GetAliasRequestDescriptor, GetAliasResponse, GetAliasRequestParameters>(descriptor, cancellationToken);
	}
}

#endif
