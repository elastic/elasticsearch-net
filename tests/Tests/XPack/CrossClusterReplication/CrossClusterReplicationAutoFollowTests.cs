// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Linq;
using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Core.ManagedElasticsearch.NodeSeeders;
using Tests.Core.Xunit;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.XPack.CrossClusterReplication
{
	[SkipVersion("<6.5.0", "")]
	[BlockedByIssue("CCR change in structure, will be fixed on 6.x and forward ported")]
	public class CrossClusterReplicationAutoFollowTests : CoordinatedIntegrationTestBase<XPackCluster>
	{
		private const string CreateAutoFollowStep = nameof(CreateAutoFollowStep);
		private const string GetAutoFollowStep = nameof(GetAutoFollowStep);
		private const string PauseAutoFollowStep = nameof(PauseAutoFollowStep);
		private const string ResumeAutoFollowStep = nameof(ResumeAutoFollowStep);
		private const string DeleteAutoFollowStep = nameof(DeleteAutoFollowStep);
		private const string GlobalStatsStep = nameof(GlobalStatsStep);

		public CrossClusterReplicationAutoFollowTests(XPackCluster cluster, EndpointUsage usage) : base(new CoordinatedUsage(cluster, usage, Prefix)
		{
			{
				CreateAutoFollowStep, u =>
					u.Calls<CreateAutoFollowPatternDescriptor, CreateAutoFollowPatternRequest, ICreateAutoFollowPatternRequest, CreateAutoFollowPatternResponse>(
						v => new CreateAutoFollowPatternRequest(AutoPattern(v))
						{
							RemoteCluster = DefaultSeeder.RemoteClusterName,
							LeaderIndexPatterns = new [] { $"{LeaderPrefix}*"},
							FollowIndexPattern = $"{FollowerPrefix}{{{{leader_index}}}}",
							MaxWriteBufferSize = "1mb"
						},
						(v, d) => d
							.RemoteCluster(DefaultSeeder.RemoteClusterName)
							.LeaderIndexPatterns($"{LeaderPrefix}*")
							.FollowIndexPattern($"{FollowerPrefix}{{{{leader_index}}}}")
							.MaxWriteBufferSize("1mb")
						,
						(v, c, f) => c.CrossClusterReplication.CreateAutoFollowPattern(AutoPattern(v), f),
						(v, c, f) => c.CrossClusterReplication.CreateAutoFollowPatternAsync(AutoPattern(v), f),
						(v, c, r) => c.CrossClusterReplication.CreateAutoFollowPattern(r),
						(v, c, r) => c.CrossClusterReplication.CreateAutoFollowPatternAsync(r)
					)
			},
			{
				GetAutoFollowStep, u =>
					u.Calls<GetAutoFollowPatternDescriptor, GetAutoFollowPatternRequest, IGetAutoFollowPatternRequest, GetAutoFollowPatternResponse>(
						v => new GetAutoFollowPatternRequest(AutoPattern(v)),
						(v, d) => d,
						(v, c, f) => c.CrossClusterReplication.GetAutoFollowPattern(AutoPattern(v), f),
						(v, c, f) => c.CrossClusterReplication.GetAutoFollowPatternAsync(AutoPattern(v), f),
						(v, c, r) => c.CrossClusterReplication.GetAutoFollowPattern(r),
						(v, c, r) => c.CrossClusterReplication.GetAutoFollowPatternAsync(r)
					)
			},
			{
				PauseAutoFollowStep, u =>
					u.Calls<PauseAutoFollowPatternDescriptor, PauseAutoFollowPatternRequest, IPauseAutoFollowPatternRequest, PauseAutoFollowPatternResponse>(
						v => new PauseAutoFollowPatternRequest(AutoPattern(v)),
						(v, d) => d,
						(v, c, f) => c.CrossClusterReplication.PauseAutoFollowPattern(AutoPattern(v), f),
						(v, c, f) => c.CrossClusterReplication.PauseAutoFollowPatternAsync(AutoPattern(v), f),
						(v, c, r) => c.CrossClusterReplication.PauseAutoFollowPattern(r),
						(v, c, r) => c.CrossClusterReplication.PauseAutoFollowPatternAsync(r)
					)
			},
			{
				ResumeAutoFollowStep, u =>
					u.Calls<ResumeAutoFollowPatternDescriptor, ResumeAutoFollowPatternRequest, IResumeAutoFollowPatternRequest, ResumeAutoFollowPatternResponse>(
						v => new ResumeAutoFollowPatternRequest(AutoPattern(v)),
						(v, d) => d,
						(v, c, f) => c.CrossClusterReplication.ResumeAutoFollowPattern(AutoPattern(v), f),
						(v, c, f) => c.CrossClusterReplication.ResumeAutoFollowPatternAsync(AutoPattern(v), f),
						(v, c, r) => c.CrossClusterReplication.ResumeAutoFollowPattern(r),
						(v, c, r) => c.CrossClusterReplication.ResumeAutoFollowPatternAsync(r)
					)
			},
			{
				DeleteAutoFollowStep, u =>
					u.Calls<DeleteAutoFollowPatternDescriptor, DeleteAutoFollowPatternRequest, IDeleteAutoFollowPatternRequest, DeleteAutoFollowPatternResponse>(
						v => new DeleteAutoFollowPatternRequest(AutoPattern(v)),
						(v, d) => d,
						(v, c, f) => c.CrossClusterReplication.DeleteAutoFollowPattern(AutoPattern(v), f),
						(v, c, f) => c.CrossClusterReplication.DeleteAutoFollowPatternAsync(AutoPattern(v), f),
						(v, c, r) => c.CrossClusterReplication.DeleteAutoFollowPattern(r),
						(v, c, r) => c.CrossClusterReplication.DeleteAutoFollowPatternAsync(r)
					)
			},
			{
				GlobalStatsStep, u => u.Calls<CcrStatsDescriptor, CcrStatsRequest, ICcrStatsRequest, CcrStatsResponse>(
						v => new CcrStatsRequest(),
						(v, d) => d,
						(v, c, f) => c.CrossClusterReplication.Stats(f),
						(v, c, f) => c.CrossClusterReplication.StatsAsync(f),
						(v, c, r) => c.CrossClusterReplication.Stats(r),
						(v, c, r) => c.CrossClusterReplication.StatsAsync(r)
					)
			}
		}) { }

		protected static string Prefix { get; } = $"autof-{Guid.NewGuid().ToString("N").Substring(0, 4)}";
		protected static string LeaderPrefix { get; } = $"leader-{Prefix}";
		protected static string FollowerPrefix { get; } = "follower-";

		private static string AutoPattern(string v) => $"auto-pattern-{v}";

		[I] public async Task CreateIsAcked() => await Assert<CreateAutoFollowPatternResponse>(CreateAutoFollowStep, r => r.Acknowledged.Should().BeTrue());

		[I] public async Task PauseIsAcked() => await Assert<PauseAutoFollowPatternResponse>(PauseAutoFollowStep, r => r.Acknowledged.Should().BeTrue());

		[I] public async Task ResumeIsAcked() => await Assert<ResumeAutoFollowPatternResponse>(ResumeAutoFollowStep, r => r.Acknowledged.Should().BeTrue());

		[I] public async Task DeleteIsAcked() => await Assert<DeleteAutoFollowPatternResponse>(DeleteAutoFollowStep, r => r.Acknowledged.Should().BeTrue());

		[I] public async Task GetReturnsAndIsMapped() => await Assert<GetAutoFollowPatternResponse>(GetAutoFollowStep, r =>
		{
			r.Patterns.Should().NotBeNull().And.HaveCount(1);

			var autoPattern = r.Patterns.First().Value;
			autoPattern.Should().NotBeNull();
			autoPattern.MaxWriteBufferSize.Should().Be("1mb");
			autoPattern.RemoteCluster.Should().Be(DefaultSeeder.RemoteClusterName);
			autoPattern.FollowIndexPattern.Should().Contain("follower");
			autoPattern.LeaderIndexPatterns.Should().NotBeEmpty().And.HaveCount(1);
		});

		[I] public async Task GlobalStatsResponse() => await Assert<CcrStatsResponse>(GlobalStatsStep, r =>
		{
			r.IsValid.Should().BeTrue();
			r.AutoFollowStats.Should().NotBeNull();
			r.AutoFollowStats.RecentAutoFollowErrors.Should().NotBeNull().And.BeEmpty();
		});

	}
}
