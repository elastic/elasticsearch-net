using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using Elasticsearch.Net.Specification.CatApi;
using FluentAssertions;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;

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
