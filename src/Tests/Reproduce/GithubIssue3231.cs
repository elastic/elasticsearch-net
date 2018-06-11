using Elasticsearch.Net;
using FluentAssertions;
using System;
using Elastic.Xunit.XunitPlumbing;
using Tests.Framework;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Xunit;

namespace Tests.Reproduce
{
	public class GithubIssue3231 : IClusterFixture<ReadOnlyCluster>
	{
		private readonly ReadOnlyCluster _cluster;

		public GithubIssue3231(ReadOnlyCluster cluster)
		{
			_cluster = cluster;
		}

		[I]
		public void CatIndicesUsesThrowExceptionsFromConnectionSettingsWhenRequestSettingsAreNotSetTest()
		{
			var client = TestClient.GetClient(connectionSettings => connectionSettings.ThrowExceptions());
			Action catIndicesRequest = () => client.LowLevel.CatIndices<StringResponse>("non-existing-index");
			catIndicesRequest.ShouldThrow<ElasticsearchClientException>();
		}
	}
}
