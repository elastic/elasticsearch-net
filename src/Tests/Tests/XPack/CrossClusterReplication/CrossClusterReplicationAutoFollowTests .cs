using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Core.ManagedElasticsearch.NodeSeeders;
using Tests.Domain;
using Tests.Framework;
using Tests.Framework.EndpointTests.TestState;
using Tests.Framework.Integration;

namespace Tests.XPack.CrossClusterReplication
{
	[SkipVersion("<6.5.0", "")]
	public class CrossClusterReplicationAutoFollowTests : CoordinatedIntegrationTestBase<WritableCluster>
	{
		private const string CreateAutoFollowStep = nameof(CreateAutoFollowStep);
		private const string GetAutoFollowStep = nameof(GetAutoFollowStep);
		private const string DeleteAutoFollowStep = nameof(DeleteAutoFollowStep);
		private const string GlobalStatsStep = nameof(GlobalStatsStep);

		private static readonly Project[] Data = Project.Generator.GenerateLazy(1000).ToArray();

		public CrossClusterReplicationAutoFollowTests(WritableCluster cluster, EndpointUsage usage) : base(new CoordinatedUsage(cluster, usage, Prefix)
		{
			{
				CreateAutoFollowStep, u =>
					u.Calls<CreateAutoFollowPatternDescriptor, CreateAutoFollowPatternRequest, ICreateAutoFollowPatternRequest, ICreateAutoFollowPatternResponse>(
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
						(v, c, f) => c.CreateAutoFollowPattern(AutoPattern(v), f),
						(v, c, f) => c.CreateAutoFollowPatternAsync(AutoPattern(v), f),
						(v, c, r) => c.CreateAutoFollowPattern(r),
						(v, c, r) => c.CreateAutoFollowPatternAsync(r)
					)
			},
			{
				GetAutoFollowStep, u =>
					u.Calls<GetAutoFollowPatternDescriptor, GetAutoFollowPatternRequest, IGetAutoFollowPatternRequest, IGetAutoFollowPatternResponse>(
						v => new GetAutoFollowPatternRequest(AutoPattern(v)),
						(v, d) => d.Name(AutoPattern(v)),
						(v, c, f) => c.GetAutoFollowPattern(f),
						(v, c, f) => c.GetAutoFollowPatternAsync(f),
						(v, c, r) => c.GetAutoFollowPattern(r),
						(v, c, r) => c.GetAutoFollowPatternAsync(r)
					)
			},
			{
				DeleteAutoFollowStep, u =>
					u.Calls<DeleteAutoFollowPatternDescriptor, DeleteAutoFollowPatternRequest, IDeleteAutoFollowPatternRequest, IDeleteAutoFollowPatternResponse>(
						v => new DeleteAutoFollowPatternRequest(AutoPattern(v)),
						(v, d) => d,
						(v, c, f) => c.DeleteAutoFollowPattern(AutoPattern(v), f),
						(v, c, f) => c.DeleteAutoFollowPatternAsync(AutoPattern(v), f),
						(v, c, r) => c.DeleteAutoFollowPattern(r),
						(v, c, r) => c.DeleteAutoFollowPatternAsync(r)
					)
			},
			{
				GlobalStatsStep, u => u.Calls<CcrStatsDescriptor, CcrStatsRequest, ICcrStatsRequest, ICcrStatsResponse>(
						v => new CcrStatsRequest(),
						(v, d) => d,
						(v, c, f) => c.CcrStats(f),
						(v, c, f) => c.CcrStatsAsync(f),
						(v, c, r) => c.CcrStats(r),
						(v, c, r) => c.CcrStatsAsync(r)
					)
			}
		}) { }

		protected static string Prefix { get; } = $"autof-{Guid.NewGuid().ToString("N").Substring(0, 4)}";
		protected static string LeaderPrefix { get; } = $"leader-{Prefix}";
		protected static string FollowerPrefix { get; } = "follower-";

		private static string LeaderIndex(string v) => $"{LeaderPrefix}{v}";
		private static string FollowIndex(string v) => $"{FollowerPrefix}{LeaderPrefix}{v}";
		private static string AutoPattern(string v) => $"auto-pattern-{v}";

		[I] public async Task CreateIsAcked() => await Assert<CreateAutoFollowPatternResponse>(CreateAutoFollowStep, r => r.Acknowledged.Should().BeTrue());

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
