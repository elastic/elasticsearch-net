// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elasticsearch.Net;
using Nest;

namespace Tests.Core.Extensions
{
	public static class ClientExtensions
	{
		public static ClusterHealthResponse WaitForSecurityIndices(this IElasticClient client) =>
			client.Cluster.Health(new ClusterHealthRequest(".security-*") { WaitForStatus = WaitForStatus.Green });
	}
}
