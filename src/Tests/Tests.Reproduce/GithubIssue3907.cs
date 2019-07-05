using System;
using System.Net;
using Elastic.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;

namespace Tests.Reproduce
{
	public class GithubIssue3907 : IClusterFixture<ReadOnlyCluster>
	{
		private readonly IntrusiveOperationCluster _cluster;

		// use intrusive operation because we're changing the underlying http handler
		public GithubIssue3907(IntrusiveOperationCluster cluster) => _cluster = cluster;

		[I]
		public void NotUsingSocketsHttpHandlerDoesNotCauseException()
		{
			AppContext.SetSwitch("System.Net.Http.UseSocketsHttpHandler", false);

			var response = _cluster.Client.Indices.Exists("non_existent_index");
			response.ApiCall.HttpStatusCode.Should().Be(404);
			response.OriginalException.Should().BeNull();

			AppContext.SetSwitch("System.Net.Http.UseSocketsHttpHandler", true);
		}
	}
}
