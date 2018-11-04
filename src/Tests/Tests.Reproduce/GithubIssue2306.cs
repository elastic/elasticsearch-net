using Elastic.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;

namespace Tests.Reproduce
{
	public class GithubIssue2306 : IClusterFixture<ReadOnlyCluster>
	{
		private readonly ReadOnlyCluster _cluster;

		public GithubIssue2306(ReadOnlyCluster cluster) => _cluster = cluster;

		[I]
		public void DeleteNonExistentDocumentReturnsNotFound()
		{
			var client = _cluster.Client;
			var response = client.Delete<Project>("non-existent-id");

			response.ShouldBeValid();
#pragma warning disable 618
			response.Found.Should().BeFalse();
#pragma warning restore 618
			response.Result.Should().Be(Result.NotFound);
			response.Index.Should().Be("project");
			response.Type.Should().Be("project");
			response.Id.Should().Be("non-existent-id");
		}
	}
}
