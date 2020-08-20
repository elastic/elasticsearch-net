// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Linq;
using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using Elasticsearch.Net.VirtualizedCluster;
using Elasticsearch.Net.VirtualizedCluster.Audit;
using Elasticsearch.Net.VirtualizedCluster.Rules;
using FluentAssertions;
using Xunit;

namespace Tests.ClientConcepts
{
	public class VirtualClusterTests
	{
		[U] public async Task ThrowsExceptionWithNoRules()
		{
			var audit = new Auditor(() => VirtualClusterWith
				.Nodes(1)
				.StaticConnectionPool()
				.Settings(s => s.DisablePing().EnableDebugMode())
			);
			var e = await Assert.ThrowsAsync<UnexpectedElasticsearchClientException>(
				async () => await audit.TraceCalls(new ClientCall { }));

			e.Message.Should().Contain("No ClientCalls() defined for the current VirtualCluster, so we do not know how to respond");
		}

		[U] public async Task ThrowsExceptionAfterDepleedingRules()
		{
			var audit = new Auditor(() => VirtualClusterWith
				.Nodes(1)
				.ClientCalls(r => r.Succeeds(TimesHelper.Once).ReturnResponse(new { x = 1 }))
				.StaticConnectionPool()
				.Settings(s => s.DisablePing().EnableDebugMode())
			);
			audit = await audit.TraceCalls(
				new ClientCall {

					{ AuditEvent.HealthyResponse, 9200, response =>
					{
						response.ApiCall.Success.Should().BeTrue();
						response.ApiCall.HttpStatusCode.Should().Be(200);
						response.ApiCall.DebugInformation.Should().Contain("x\":1");
					} },
				}
			);
			var e = await Assert.ThrowsAsync<UnexpectedElasticsearchClientException>(
				async () => await audit.TraceCalls(new ClientCall { }));

			e.Message.Should().Contain("No global or port specific rule (9200) matches any longer after 2 calls in to the cluster");
		}

		[U] public async Task AGlobalRuleStaysValidForever()
		{
			var audit = new Auditor(() => VirtualClusterWith
				.Nodes(1)
				.ClientCalls(c=>c.SucceedAlways())
				.StaticConnectionPool()
				.Settings(s => s.DisablePing())
			);
			audit = await audit.TraceCalls(
				Enumerable.Range(0, 1000)
					.Select(i => new ClientCall { { AuditEvent.HealthyResponse, 9200}, })
					.ToArray()
			);

		}

		[U] public async Task RulesAreIgnoredAfterBeingExecuted()
		{
			var audit = new Auditor(() => VirtualClusterWith
				.Nodes(1)
				.ClientCalls(r => r.Succeeds(TimesHelper.Once).ReturnResponse(new { x = 1 }))
				.ClientCalls(r => r.Fails(TimesHelper.Once, 500).ReturnResponse(new { x = 2 }))
				.ClientCalls(r => r.Fails(TimesHelper.Twice, 400).ReturnResponse(new { x = 3 }))
				.ClientCalls(r => r.Succeeds(TimesHelper.Once).ReturnResponse(new { x = 4 }))
				.StaticConnectionPool()
				.Settings(s => s.DisablePing().EnableDebugMode())
			);
			audit = await audit.TraceCalls(
				new ClientCall {

					{ AuditEvent.HealthyResponse, 9200, response =>
					{
						response.ApiCall.Success.Should().BeTrue();
						response.ApiCall.HttpStatusCode.Should().Be(200);
						response.ApiCall.DebugInformation.Should().Contain("x\":1");
					} },
				},
				new ClientCall {

					{ AuditEvent.BadResponse, 9200, response =>
					{
						response.ApiCall.Success.Should().BeFalse();
						response.ApiCall.HttpStatusCode.Should().Be(500);
						response.ApiCall.DebugInformation.Should().Contain("x\":2");
					} },
				},
				new ClientCall {

					{ AuditEvent.BadResponse, 9200, response =>
					{
						response.ApiCall.HttpStatusCode.Should().Be(400);
						response.ApiCall.DebugInformation.Should().Contain("x\":3");
					} },
				},
				new ClientCall {

					{ AuditEvent.BadResponse, 9200, response =>
					{
						response.ApiCall.HttpStatusCode.Should().Be(400);
						response.ApiCall.DebugInformation.Should().Contain("x\":3");
					} },
				},
				new ClientCall {

					{ AuditEvent.HealthyResponse, 9200, response =>
					{
						response.ApiCall.HttpStatusCode.Should().Be(200);
						response.ApiCall.DebugInformation.Should().Contain("x\":4");
					} },
				}
			);
		}
	}
}
