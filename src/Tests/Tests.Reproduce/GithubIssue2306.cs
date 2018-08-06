using Elastic.Xunit.Sdk;
using Elastic.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Xunit;

namespace Tests.Reproduce
{
	public class GithubIssue2306 : IClusterFixture<ReadOnlyCluster>
	{
		private readonly ReadOnlyCluster _cluster;

		public GithubIssue2306(ReadOnlyCluster cluster)
		{
			_cluster = cluster;
		}

		[I]
		public void DeleteNonExistentDocumentReturnsNotFound()
		{
			var client = _cluster.Client;
			var response = client.Delete<Project>("non-existent-id", d => d.Routing("routing"));

			response.ShouldNotBeValid();
			response.Result.Should().Be(Result.NotFound);
			response.Index.Should().Be("project");
			response.Type.Should().Be("doc");
			response.Id.Should().Be("non-existent-id");
		}
	}
}
