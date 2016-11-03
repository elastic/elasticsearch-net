using FluentAssertions;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
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
			var response = client.Delete<Project>("non-existent-id");

			response.ShouldBeValid();
			response.Found.Should().BeFalse();
			response.Index.Should().Be("project");
			response.Type.Should().Be("project");
			response.Id.Should().Be("non-existent-id");
		}
	}
}
