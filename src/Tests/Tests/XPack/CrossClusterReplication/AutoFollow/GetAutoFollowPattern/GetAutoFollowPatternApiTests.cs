using System;
using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Core.ManagedElasticsearch.NodeSeeders;
using Tests.Framework;
using Tests.Framework.Integration;

namespace Tests.XPack.CrossClusterReplication.AutoFollow.GetAutoFollowPattern
{
	[SkipVersion("<6.5.0", "Only available in Elasticsearch 6.5.0+")]
	public class GetAutoFollowPatternApiTests
		: ApiIntegrationTestBase<WritableCluster, IGetAutoFollowPatternResponse, IGetAutoFollowPatternRequest, GetAutoFollowPatternDescriptor,
			GetAutoFollowPatternRequest>
	{
		public GetAutoFollowPatternApiTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;

		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => "/_ccr/auto_follow";
		private static string FollowerPrefix { get; } = "follower-";
		private static string LeaderPrefix { get; } = $"leader-{Prefix}";

		private static string Prefix { get; } = $"autof-{Guid.NewGuid().ToString("N").Substring(0, 4)}";

		private static string AutoPattern(string v) => $"auto-pattern-{v}";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.GetAutoFollowPattern(f),
			(client, f) => client.GetAutoFollowPatternAsync(f),
			(client, r) => client.GetAutoFollowPattern(r),
			(client, r) => client.GetAutoFollowPatternAsync(r)
		);

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			var createAutoFollowPatternResponse = client.CreateAutoFollowPattern(AutoPattern("getauto-1"), c => c
				.RemoteCluster(DefaultSeeder.RemoteClusterName)
				.LeaderIndexPatterns($"{LeaderPrefix}*")
				.FollowIndexPattern($"{FollowerPrefix}{{{{leader_index}}}}")
				.MaxWriteBufferSize("1mb")
			);

			createAutoFollowPatternResponse.ShouldBeValid();

			createAutoFollowPatternResponse = client.CreateAutoFollowPattern(AutoPattern("getauto-2"), c => c
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
			var deleteAutoFollowPattern = client.DeleteAutoFollowPattern(AutoPattern("getauto-1"));
			deleteAutoFollowPattern.ShouldBeValid();
			deleteAutoFollowPattern = client.DeleteAutoFollowPattern(AutoPattern("getauto-2"));
			deleteAutoFollowPattern.ShouldBeValid();
		}

		protected override void ExpectResponse(IGetAutoFollowPatternResponse response)
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
