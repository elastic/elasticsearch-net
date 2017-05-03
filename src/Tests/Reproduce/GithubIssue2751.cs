using System;
using System.Diagnostics;
using FluentAssertions;
using Tests.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Nest;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Tests.Framework.MockData;
using Xunit;

namespace Tests.Reproduce
{
	public class GithubIssue2751: IClusterFixture<ReadOnlyCluster>
	{
		private readonly ReadOnlyCluster _cluster;
		public GithubIssue2751(ReadOnlyCluster cluster) { _cluster = cluster; }

		[I]
		public async Task CancellationOfRequestReturnsResponse()
		{
			var client = TestClient.GetClient(modifySettings: c => c.RequestTimeout(TimeSpan.FromSeconds(2)));

			var response = client.Search<Project>();
			response.IsValid.Should().BeFalse();
			response.ApiCall.AuditTrail.Should().Contain(a=>a.Event == AuditEvent.MaxTimeoutReached);

			response = await client.SearchAsync<Project>();
			response.IsValid.Should().BeFalse();
			response.ApiCall.AuditTrail.Should().Contain(a=>a.Event == AuditEvent.MaxTimeoutReached);

			var ctx = new CancellationTokenSource();
			var task = client.SearchAsync<Project>(s=>s, ctx.Token);
			await Task.Delay(100);
			ctx.Cancel();
			response = await task;

			response.IsValid.Should().BeFalse();
			response.ApiCall.AuditTrail.Should().NotContain(a=>a.Event == AuditEvent.MaxTimeoutReached);

		}

		[I]
		public async Task UserCancellationDoesNotCauseFailOverToNextNode()
		{
			var client = TestClient.GetClient(
				createPool: u => new StaticConnectionPool(new [] { TestClient.CreateUri(9200), TestClient.CreateUri(9201), TestClient.CreateUri(9202)}, randomize: false),
				modifySettings: c => c.RequestTimeout(TimeSpan.FromSeconds(2)));

			var ctx = new CancellationTokenSource();
			var task = client.SearchAsync<Project>(s=>s, ctx.Token);
			await Task.Delay(100);
			ctx.Cancel();
			var response = await task;

			response.IsValid.Should().BeFalse();
			response.ApiCall.AuditTrail.Should().Contain(a=> a.Event == AuditEvent.CancellationRequested);
			response.ApiCall.AuditTrail.Should().NotContain(a=> a.Event == AuditEvent.MaxTimeoutReached);
			response.ApiCall.AuditTrail.Should().NotContain(a=> a.Event == AuditEvent.PingFailure);
			response.ApiCall.AuditTrail.Should().ContainSingle(a=> a.Event == AuditEvent.BadRequest || a.Event == AuditEvent.BadResponse);
			var trailsWithNodes = response.ApiCall.AuditTrail.Where(a=>a.Node != null);
			trailsWithNodes.Should().NotBeEmpty().And.NotContain(a=> a.Node.Uri.Port != 9200);

			//throw new Exception(response.DebugInformation);

		}
	}
}
