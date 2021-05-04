// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elastic.Transport;
using Elasticsearch.Net;
using Elasticsearch.Net.Specification.CatApi;
using FluentAssertions;
using Tests.Core.ManagedElasticsearch.Clusters;

namespace Tests.Reproduce
{
	public class GithubIssue4243 : IClusterFixture<ReadOnlyCluster>
	{
		private readonly ReadOnlyCluster _cluster;

		public GithubIssue4243(ReadOnlyCluster cluster) => _cluster = cluster;

		[I]
		public async Task UsingFormatJsonIsSuccessfulResponse()
		{
			var connectionConfiguration = new ConnectionConfiguration(_cluster.Client.ConnectionSettings.ConnectionPool);
			var lowLevelClient = new ElasticLowLevelClient(connectionConfiguration);
			var response = await lowLevelClient.Cat.MasterAsync<StringResponse>(new CatMasterRequestParameters { Format = "JSON" });

			response.Success.Should().BeTrue();
			response.ApiCall.HttpStatusCode.Should().Be(200);
			response.OriginalException.Should().BeNull();
		}
	}
}
