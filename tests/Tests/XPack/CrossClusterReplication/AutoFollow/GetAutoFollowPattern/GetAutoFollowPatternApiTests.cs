// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elastic.Transport;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Core.ManagedElasticsearch.NodeSeeders;
using Tests.Core.Xunit;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.XPack.CrossClusterReplication.AutoFollow.GetAutoFollowPattern
{
	[SkipVersion("<6.5.0", "Only available in Elasticsearch 6.5.0+")]
	[BlockedByIssue("CCR fails license check ElasticsearchException[could not determine the license type "
		+ "for cluster [remote-cluster]]; nested: ConnectTransportException[[][127.0.0.1:9300] general node "
		+ "connection failure]; nested: TransportException[handshake failed because connection reset];")]
	public class GetAutoFollowPatternApiTests
		: ApiIntegrationTestBase<XPackCluster, GetAutoFollowPatternResponse, IGetAutoFollowPatternRequest, GetAutoFollowPatternDescriptor,
			GetAutoFollowPatternRequest>
	{
		public GetAutoFollowPatternApiTests(XPackCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;

		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => "/_ccr/auto_follow";
		private static string FollowerPrefix { get; } = "follower-";
		private static string Prefix { get; } = $"autof-{Guid.NewGuid().ToString("N").Substring(0, 4)}";
		private static string LeaderPrefix { get; } = $"leader-{Prefix}";


		private static string AutoPattern(string v) => $"auto-pattern-{v}";

		//TODO 7.0 i think this should take Names..
		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.CrossClusterReplication.GetAutoFollowPattern(null, f),
			(client, f) => client.CrossClusterReplication.GetAutoFollowPatternAsync(null, f),
			(client, r) => client.CrossClusterReplication.GetAutoFollowPattern(r),
			(client, r) => client.CrossClusterReplication.GetAutoFollowPatternAsync(r)
		);

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			var createAutoFollowPatternResponse = client.CrossClusterReplication.CreateAutoFollowPattern(AutoPattern("getauto-1"), c => c
				.RemoteCluster(DefaultSeeder.RemoteClusterName)
				.LeaderIndexPatterns($"{LeaderPrefix}*")
				.FollowIndexPattern($"{FollowerPrefix}{{{{leader_index}}}}")
				.MaxWriteBufferSize("1mb")
			);

			createAutoFollowPatternResponse.ShouldBeValid();

			createAutoFollowPatternResponse = client.CrossClusterReplication.CreateAutoFollowPattern(AutoPattern("getauto-2"), c => c
				.RemoteCluster(DefaultSeeder.RemoteClusterName)
				.LeaderIndexPatterns($"{LeaderPrefix}*")
				.FollowIndexPattern($"{FollowerPrefix}{{{{leader_index}}}}")
				.MaxWriteBufferSize("1mb")
			);

			createAutoFollowPatternResponse.ShouldBeValid();
		}

		// delete auto follow patterns afterwards as Elasticsearch attempts to connect to remote cluster,
		// which will hang integration tests
		protected override void IntegrationTeardown(IElasticClient client, CallUniqueValues values)
		{
			var deleteAutoFollowPattern = client.CrossClusterReplication.DeleteAutoFollowPattern(AutoPattern("getauto-1"));
			deleteAutoFollowPattern.ShouldBeValid();
			deleteAutoFollowPattern = client.CrossClusterReplication.DeleteAutoFollowPattern(AutoPattern("getauto-2"));
			deleteAutoFollowPattern.ShouldBeValid();
		}

		protected override void ExpectResponse(GetAutoFollowPatternResponse response)
		{
			response.Patterns.Should().NotBeNull().And.ContainKeys(AutoPattern("getauto-1"), AutoPattern("getauto-2"));
			foreach (var autoPattern in response.Patterns.Values)
			{
				autoPattern.Should().NotBeNull();
				autoPattern.MaxWriteBufferSize.Should().Be("1mb");
				autoPattern.RemoteCluster.Should().Be(DefaultSeeder.RemoteClusterName);
				autoPattern.FollowIndexPattern.Should().Contain("follower");
				autoPattern.LeaderIndexPatterns.Should().NotBeEmpty().And.HaveCount(1);
			}
		}
	}
}
