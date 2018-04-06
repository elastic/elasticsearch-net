using System;
using System.Diagnostics;
using FluentAssertions;
using Tests.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Elastic.Xunit.Sdk;
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

		/*
		* These tests are ignored because they rely on a fiddler autoresponder rule that artificially delays
		* the response from /project/doc/_search to 5 seconds. See this issue's accompanying PR.
		*/

		//[I]
		public async Task TimeoutOfRequestReturnsResponse()
		{
			var client = TestClient.GetClient(modifySettings: c => c.RequestTimeout(TimeSpan.FromSeconds(2)));

			var stopwatch = Stopwatch.StartNew();

			var response = client.Search<Project>();
			stopwatch.Stop();
			response.ShouldNotBeValid();
			//fiddler delays for 5 seconds, but we expect our request timeout to kick in plenty sooner
			stopwatch.Elapsed.Should().BeLessThan(TimeSpan.FromSeconds(3));
			response.ApiCall.AuditTrail.Should().Contain(a=>a.Event == AuditEvent.MaxTimeoutReached);
			response.ApiCall.AuditTrail.Should().NotContain(a=> a.Event == AuditEvent.CancellationRequested);


			stopwatch.Restart();
			response = await client.SearchAsync<Project>();
			stopwatch.Stop();
			response.ShouldNotBeValid();
			//fiddler delays for 5 seconds, but we expect our request timeout to kick in plenty sooner
			stopwatch.Elapsed.Should().BeLessThan(TimeSpan.FromSeconds(3));
			response.ApiCall.AuditTrail.Should().Contain(a=>a.Event == AuditEvent.MaxTimeoutReached);
			response.ApiCall.AuditTrail.Should().NotContain(a=> a.Event == AuditEvent.CancellationRequested);

			//cancelling an async request should behave differently
			stopwatch.Restart();
			var ctx = new CancellationTokenSource();
			var task = client.SearchAsync<Project>(s=>s, ctx.Token);
			await Task.Delay(100);
			ctx.Cancel();
			response = await task;
			stopwatch.Stop();

			response.ShouldNotBeValid();
			//fiddler delays for 5 seconds, but we cancel after 100ms so we expect less then a second to have passed
			stopwatch.Elapsed.Should().BeLessThan(TimeSpan.FromSeconds(1));
			//we cancelled this request it should not contain MaxTimeoutReached
			response.ApiCall.AuditTrail.Should().NotContain(a=>a.Event == AuditEvent.MaxTimeoutReached);
			//it should contain CancellationRequested
			response.ApiCall.AuditTrail.Should().Contain(a=> a.Event == AuditEvent.CancellationRequested);
		}

		//[I]
		public async Task UserCancellationDoesNotCauseFailOverToNextNode()
		{
			//this tests assumes only a single node is running under 9200 and fiddler is set up to artificially delay results
			var client = TestClient.GetClient(
				createPool: u => new StaticConnectionPool(new [] { TestClient.CreateUri(9200), TestClient.CreateUri(9201), TestClient.CreateUri(9202)}, randomize: false),
				modifySettings: c => c.RequestTimeout(TimeSpan.FromSeconds(2)));

			var ctx = new CancellationTokenSource();
			var task = client.SearchAsync<Project>(s=>s, ctx.Token);
			await Task.Delay(100);
			ctx.Cancel();
			var response = await task;

			response.ShouldNotBeValid();
			response.ApiCall.AuditTrail.Should().Contain(a=> a.Event == AuditEvent.CancellationRequested);
			response.ApiCall.AuditTrail.Should().NotContain(a=> a.Event == AuditEvent.MaxTimeoutReached);


			//Assert no failover happened, PingFailure would occur on 9201 normally.
			response.ApiCall.AuditTrail.Should().NotContain(a=> a.Event == AuditEvent.PingFailure);
			//Assert no failover happened, for explicitness sake we only expect to see a single BadRequest or BadResponse
			//(depending on how fast Task.Delay kicks in)
			response.ApiCall.AuditTrail.Should().ContainSingle(a=> a.Event == AuditEvent.BadRequest || a.Event == AuditEvent.BadResponse);
			//nothing in our trail should have gone to anything other then node 9200
			var trailsWithNodes = response.ApiCall.AuditTrail.Where(a=>a.Node != null);
			trailsWithNodes.Should().NotBeEmpty().And.NotContain(a=> a.Node.Uri.Port != 9200);

			//throw new Exception(response.DebugInformation);

		}
	}
}
