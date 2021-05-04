// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;

namespace Tests.Reproduce
{
	public class GithubIssue2173 : IClusterFixture<WritableCluster>
	{
		private readonly WritableCluster _cluster;

		public GithubIssue2173(WritableCluster cluster) => _cluster = cluster;

		[I] public void UpdateByQueryWithInvalidScript()
		{
			var client = _cluster.Client;
			var response = client.UpdateByQuery<Project>(u => u
				.Script(ss => ss.Source("invalid groovy").Lang("groovy"))
			);
			response.ShouldNotBeValid();
		}
	}
}
