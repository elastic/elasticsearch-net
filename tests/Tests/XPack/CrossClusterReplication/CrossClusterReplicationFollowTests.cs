// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;
using Tests.Core.Client;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Core.ManagedElasticsearch.NodeSeeders;
using Tests.Core.Xunit;
using Tests.Domain;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.XPack.CrossClusterReplication
{
	[SkipVersion("<6.5.0", "")]
	[BlockedByIssue("CCR change in structure, will be fixed on 6.x and forward ported")]
	public class CrossClusterReplicationFollowTests : CoordinatedIntegrationTestBase<XPackCluster>
	{
		private readonly XPackCluster _cluster;
		private const string CloseIndexStep = nameof(CloseIndexStep);
		private const string CountAfterStep = nameof(CountAfterStep);
		private const string CountBeforeStep = nameof(CountBeforeStep);
		private const string CreateIndexStep = nameof(CreateIndexStep);
		private const string DeleteOriginalIndicesStep = nameof(DeleteOriginalIndicesStep);
		private const string FollowIndexStep = nameof(FollowIndexStep);
		private const string FollowStatsAgainStep = nameof(FollowStatsAgainStep);
		private const string FollowStatsStep = nameof(FollowStatsStep);
		private const string FollowInfoStep = nameof(FollowInfoStep);
		private const string IndexDataStep = nameof(IndexDataStep);
		private const string PauseFollowStep = nameof(PauseFollowStep);
		private const string PauseForCloseStep = nameof(PauseForCloseStep);
		private const string ResumeFollowStep = nameof(ResumeFollowStep);
		private const string UnfollowAgainStep = nameof(UnfollowAgainStep);
		private const string UnfollowStep = nameof(UnfollowStep);
		private const string GlobalStatsStep = nameof(GlobalStatsStep);

		// ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
		// wrong would result in different projects being sent
		private static readonly Project[] Data = Project.Generator.GenerateLazy(1000).ToArray();

		public CrossClusterReplicationFollowTests(XPackCluster cluster, EndpointUsage usage) : base(new CoordinatedUsage(cluster, usage, Prefix)
		{
			{
				CreateIndexStep, u => u.Calls<CreateIndexDescriptor, CreateIndexRequest, ICreateIndexRequest, CreateIndexResponse>(
					v => new CreateIndexRequest(v)
					{
						Settings = new IndexSettings()
						{
							NumberOfShards = 1,
							NumberOfReplicas = 0,
							SoftDeletes = new SoftDeleteSettings
							{
								Retention = new SoftDeleteRetentionSettings { Operations = 1024 }
							}
						}
					},
					(v, d) => d.Settings(s => s
						.NumberOfShards(1)
						.NumberOfReplicas(0)
						.SoftDeletes(sd => sd
							.Retention(r => r.Operations(1024))
						)
					),
					(v, c, f) => c.Indices.Create(v, f),
					(v, c, f) => c.Indices.CreateAsync(v, f),
					(v, c, r) => c.Indices.Create(r),
					(v, c, r) => c.Indices.CreateAsync(r)
				)
			},
			{
				FollowIndexStep, u =>
					u.Calls<CreateFollowIndexDescriptor, CreateFollowIndexRequest, ICreateFollowIndexRequest, CreateFollowIndexResponse>(
						v =>
						{
							if (cluster.ClusterConfiguration.Version < "6.7.0")
								return new CreateFollowIndexRequest(CopyIndex(v))
								{
									RemoteCluster = DefaultSeeder.RemoteClusterName,
									LeaderIndex = v
								};

							return new CreateFollowIndexRequest(CopyIndex(v))
							{
								RemoteCluster = DefaultSeeder.RemoteClusterName,
								LeaderIndex = v,
								WaitForActiveShards = "1"
							};
						},
						(v, d) =>
						{
							if (cluster.ClusterConfiguration.Version < "6.7.0")
								return d
									.RemoteCluster(DefaultSeeder.RemoteClusterName)
									.LeaderIndex(v);

							return d
								.RemoteCluster(DefaultSeeder.RemoteClusterName)
								.LeaderIndex(v)
								.WaitForActiveShards("1");
						},
						(v, c, f) => c.CrossClusterReplication.CreateFollowIndex(CopyIndex(v), f),
						(v, c, f) => c.CrossClusterReplication.CreateFollowIndexAsync(CopyIndex(v), f),
						(v, c, r) => c.CrossClusterReplication.CreateFollowIndex(r),
						(v, c, r) => c.CrossClusterReplication.CreateFollowIndexAsync(r)
					)
			},
			{
				IndexDataStep, u => u.Call((v, c) =>
				{
					var seenPages = 0;
					var tokenSource = new CancellationTokenSource();
					var bulkAllObservable = c.BulkAll(Data, b => b.Index(v).Size(1000).RefreshOnCompleted(), tokenSource.Token);
					bulkAllObservable.Wait(TimeSpan.FromSeconds(20), x => Interlocked.Increment(ref seenPages));
					return Task.CompletedTask;
				})
			},
			{
				CountBeforeStep, u => u.Call(async (v, c) => await c.CountAsync<Project>(d => d.Index(v)))
			},
			{
				PauseFollowStep, u =>
					u.Calls<PauseFollowIndexDescriptor, PauseFollowIndexRequest, IPauseFollowIndexRequest, PauseFollowIndexResponse>(
						v => new PauseFollowIndexRequest(CopyIndex(v)),
						(v, d) => d,
						(v, c, f) => c.CrossClusterReplication.PauseFollowIndex(CopyIndex(v), f),
						(v, c, f) => c.CrossClusterReplication.PauseFollowIndexAsync(CopyIndex(v), f),
						(v, c, r) => c.CrossClusterReplication.PauseFollowIndex(r),
						(v, c, r) => c.CrossClusterReplication.PauseFollowIndexAsync(r)
					)
			},
			{
				ResumeFollowStep, u =>
					u.Calls<ResumeFollowIndexDescriptor, ResumeFollowIndexRequest, IResumeFollowIndexRequest, ResumeFollowIndexResponse>(
						v => new ResumeFollowIndexRequest(CopyIndex(v)),
						(v, d) => d,
						(v, c, f) => c.CrossClusterReplication.ResumeFollowIndex(CopyIndex(v), f),
						(v, c, f) => c.CrossClusterReplication.ResumeFollowIndexAsync(CopyIndex(v), f),
						(v, c, r) => c.CrossClusterReplication.ResumeFollowIndex(r),
						(v, c, r) => c.CrossClusterReplication.ResumeFollowIndexAsync(r)
					)
			},
			{
				CountAfterStep, u => u.Call(async (v, c) => await c.CountAsync<Project>(d => d.Index(CopyIndex(v))))
			},
			{
				FollowStatsStep, u =>
					u.Calls<FollowIndexStatsDescriptor, FollowIndexStatsRequest, IFollowIndexStatsRequest, FollowIndexStatsResponse>(
						v => new FollowIndexStatsRequest(CopyIndex(v)),
						(v, d) => d,
						(v, c, f) => c.CrossClusterReplication.FollowIndexStats(CopyIndex(v), f),
						(v, c, f) => c.CrossClusterReplication.FollowIndexStatsAsync(CopyIndex(v), f),
						(v, c, r) => c.CrossClusterReplication.FollowIndexStats(r),
						(v, c, r) => c.CrossClusterReplication.FollowIndexStatsAsync(r)
					)
			},
			{
				FollowInfoStep, u =>
					u.Calls<FollowInfoDescriptor, FollowInfoRequest, IFollowInfoRequest, FollowInfoResponse>(
						v => new FollowInfoRequest(CopyIndex(v)),
						(v, d) => d,
						(v, c, f) => c.CrossClusterReplication.FollowInfo(CopyIndex(v), f),
						(v, c, f) => c.CrossClusterReplication.FollowInfoAsync(CopyIndex(v), f),
						(v, c, r) => c.CrossClusterReplication.FollowInfo(r),
						(v, c, r) => c.CrossClusterReplication.FollowInfoAsync(r)
					)
			},
			{
				DeleteOriginalIndicesStep, u => u.Calls<DeleteIndexDescriptor, DeleteIndexRequest, IDeleteIndexRequest, DeleteIndexResponse>
				(
					v => new DeleteIndexRequest(v),
					(v, d) => d,
					(v, c, f) => c.Indices.Delete(v, f),
					(v, c, f) => c.Indices.DeleteAsync(v, f),
					(v, c, r) => c.Indices.Delete(r),
					(v, c, r) => c.Indices.DeleteAsync(r)
				)
			},
			{
				//This time we get exceptions on the stats
				FollowStatsAgainStep, u =>
					u.Calls<FollowIndexStatsDescriptor, FollowIndexStatsRequest, IFollowIndexStatsRequest, FollowIndexStatsResponse>(
						v => new FollowIndexStatsRequest(CopyIndex(v)),
						(v, d) => d,
						(v, c, f) => c.CrossClusterReplication.FollowIndexStats(CopyIndex(v), f),
						(v, c, f) => c.CrossClusterReplication.FollowIndexStatsAsync(CopyIndex(v), f),
						(v, c, r) => c.CrossClusterReplication.FollowIndexStats(r),
						(v, c, r) => c.CrossClusterReplication.FollowIndexStatsAsync(r)
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
			},
			{
				UnfollowStep, u => u.Calls<UnfollowIndexDescriptor, UnfollowIndexRequest, IUnfollowIndexRequest, UnfollowIndexResponse>
				(
					v => new UnfollowIndexRequest(CopyIndex(v)),
					(v, d) => d,
					(v, c, f) => c.CrossClusterReplication.UnfollowIndex(CopyIndex(v), f),
					(v, c, f) => c.CrossClusterReplication.UnfollowIndexAsync(CopyIndex(v), f),
					(v, c, r) => c.CrossClusterReplication.UnfollowIndex(r),
					(v, c, r) => c.CrossClusterReplication.UnfollowIndexAsync(r)
				)
			},
			{
				PauseForCloseStep, u => u.Call(async (v, c) => await c.CrossClusterReplication.PauseFollowIndexAsync(CopyIndex(v)))
			},
			{
				CloseIndexStep, u => u.Call(async (v, c) => await c.Indices.CloseAsync(CopyIndex(v)))
			},
			{
				UnfollowAgainStep, u => u.Calls<UnfollowIndexDescriptor, UnfollowIndexRequest, IUnfollowIndexRequest, UnfollowIndexResponse>
				(
					v => new UnfollowIndexRequest(CopyIndex(v)),
					(v, d) => d,
					(v, c, f) => c.CrossClusterReplication.UnfollowIndex(CopyIndex(v), f),
					(v, c, f) => c.CrossClusterReplication.UnfollowIndexAsync(CopyIndex(v), f),
					(v, c, r) => c.CrossClusterReplication.UnfollowIndex(r),
					(v, c, r) => c.CrossClusterReplication.UnfollowIndexAsync(r)
				)
			},
		}) => _cluster = cluster;

		protected static string Prefix { get; } = $"f{Guid.NewGuid().ToString("N").Substring(0, 4)}";

		private static string CopyIndex(string v) => $"{v}-copy";

		// see https://github.com/elastic/elasticsearch/pull/36647. difference in behaviour between <=6.5.3 and 6.5.4+
		private static int ExpectedFollowerIndices => TestClient.Configuration.InRange("<=6.5.3") ? 4 : 1;

		[I] public async Task CreateReadOnlyIndexIsOk() => await Assert<CreateIndexResponse>(CreateIndexStep, r => r.Acknowledged.Should().BeTrue());

		[I] public async Task IndexingDataIsOk() => await AssertRunsToCompletion(IndexDataStep);

		[I] public async Task FollowResponseIsValid() => await Assert<CreateFollowIndexResponse>(FollowIndexStep, r =>
		{
			r.IsValid.Should().BeTrue();
			r.FollowIndexCreated.Should().BeTrue();
			r.FollowIndexShardsAcked.Should().BeTrue();
			r.IndexFollowingStarted.Should().BeTrue();
		});

		[I] public async Task PauseIsAcked() => await Assert<PauseFollowIndexResponse>(PauseFollowStep, r => r.Acknowledged.Should().BeTrue());

		[I] public async Task ResumeIsAcked() => await Assert<ResumeFollowIndexResponse>(ResumeFollowStep, r => r.Acknowledged.Should().BeTrue());

		[I] public async Task CountBeforeHasDocs() => await Assert<CountResponse>(CountBeforeStep, r => r.Count.Should().Be(1000));

		[I] public async Task FollowStatsResponse() => await Assert<FollowIndexStatsResponse>(FollowStatsStep, r =>
		{
			r.IsValid.Should().BeTrue();
			r.Indices.Should().NotBeEmpty();
			r.Indices.Count.Should().BeGreaterOrEqualTo(ExpectedFollowerIndices);
			var currentIndices = r.Indices.Where(i => i.Index.StartsWith(Prefix)).ToArray();
			currentIndices.Should().HaveCount(ExpectedFollowerIndices);
			foreach (var i in currentIndices)
			{
				i.Index.Should().NotBeNullOrWhiteSpace("index name");
				i.Shards.Should().NotBeEmpty();
				foreach (var s in i.Shards)
				{
					var because = $"index: {i.Index} shard: {s.ShardId}";
					s.RemoteCluster.Should().Be(DefaultSeeder.RemoteClusterName);
					s.LeaderIndex.Should().NotBeNullOrWhiteSpace("leader_index");
					s.FollowerIndex.Should().NotBeNullOrWhiteSpace("follower_index");
					s.LeaderGlobalCheckpoint.Should().BeGreaterOrEqualTo(-1, nameof(FollowIndexShardStats.LeaderGlobalCheckpoint));
					s.LeaderMaxSequenceNumber.Should().BeGreaterOrEqualTo(-1, nameof(FollowIndexShardStats.LeaderMaxSequenceNumber));
					s.LastRequestedSequenceNumber.Should().BeGreaterOrEqualTo(-1, nameof(FollowIndexShardStats.LastRequestedSequenceNumber));
					s.ReadExceptions.Should().NotBeNull(because).And.BeEmpty(because);
				}
			}
		});

		[I] public async Task FollowInfoResponse() => await Assert<FollowInfoResponse>(FollowInfoStep, r =>
		{
			r.IsValid.Should().BeTrue();
			r.FollowerIndices.Should().NotBeNull();
			r.FollowerIndices.Should().NotBeEmpty();
			var first = r.FollowerIndices.First();
			first.FollowerIndex.Should().NotBeNullOrWhiteSpace();
			first.RemoteCluster.Should().NotBeNullOrWhiteSpace();
			first.LeaderIndex.Should().NotBeNullOrWhiteSpace();
			first.Parameters.Should().NotBeNull();
		});

		[I] public async Task GlobalStatsResponse() => await Assert<CcrStatsResponse>(GlobalStatsStep, r =>
		{
			r.IsValid.Should().BeTrue();
			r.FollowStats.Should().NotBeNull();
			var indices = r.FollowStats.Indices;
			indices.Should().NotBeEmpty();
			indices.Count.Should().BeGreaterOrEqualTo(4);
			var currentIndices = indices.Where(i => i.Index.StartsWith(Prefix)).ToArray();
			currentIndices.Should().HaveCount(4);
			AssertErrorsOnShardStats(currentIndices);
		});

		[I] public async Task LeadersAreDeleted() =>
			await Assert<DeleteIndexResponse>(DeleteOriginalIndicesStep, r => r.Acknowledged.Should().BeTrue());

		[I] public async Task FollowStatsResponseAgain() => await Assert<FollowIndexStatsResponse>(FollowStatsAgainStep, r =>
		{
			r.IsValid.Should().BeTrue();
			r.Indices.Should().NotBeEmpty();
			var currentIndices = r.Indices.Where(i => i.Index.StartsWith(Prefix)).ToArray();
			currentIndices.Should().HaveCount(ExpectedFollowerIndices);
			AssertErrorsOnShardStats(currentIndices);
		});

		private static void AssertErrorsOnShardStats(FollowIndexStats[] currentIndices)
		{
			foreach (var i in currentIndices)
			{
				i.Index.Should().NotBeNullOrWhiteSpace("index name");
				i.Shards.Should().NotBeEmpty($"shards is empty on {i.Index}");
				foreach (var s in i.Shards)
				{
					var because = $"index: {i.Index} shard: {s.ShardId}";

					//We can call follow stats to quickly and read exceptions is not propagated yet
					if (s.ReadExceptions.Count == 0) continue;

					s.ReadExceptions.Should().NotBeEmpty(because);
					foreach (var e in s.ReadExceptions)
					{
						e.Retries.Should().BeGreaterThan(0, because);
						e.FromSquenceNumber.Should().BeGreaterThan(0, because);
						e.Exception.Should().NotBeNull(because);
						e.Exception.Type.Should().NotBeNullOrWhiteSpace().And.EndWith("_exception", because);
					}
					if (s.FatalException != null)
					{
						//even though read exceptions is set fatal exception can still be null (race condition?).
						s.FatalException.Should().NotBeNull($"{s.FollowerIndex}", because);
						s.FatalException.Type.Should().NotBeNullOrWhiteSpace().And.EndWith("_exception", because);
					}
				}
			}
		}

		[I] public async Task UnfollowReturns() => await AssertRunsToCompletion(UnfollowAgainStep);

		[I] public async Task UnfollowWithoutCloseIsAcked() => await Assert<UnfollowIndexResponse>(UnfollowStep, r =>
		{
			r.IsValid.Should().BeFalse();
			r.ServerError.Should().NotBeNull();
		});

		[I] public async Task PauseBeforeCloseIsAcked() =>
			await Assert<PauseFollowIndexResponse>(PauseForCloseStep, r => r.Acknowledged.Should().BeTrue());

		[I] public async Task CloseIsAcked() => await Assert<CloseIndexResponse>(CloseIndexStep, r => r.Acknowledged.Should().BeTrue());

		[I] public async Task UnfollowAfterCloseIsAcked() =>
			await Assert<UnfollowIndexResponse>(UnfollowAgainStep, r =>
			{
				// Unfollowing an index after closing on 6.7.0 throws an exception.
				if (_cluster.ClusterConfiguration.Version < "6.7.0")
				{
					r.Acknowledged.Should().BeTrue();
					return;
				}
				r.IsValid.Should().BeFalse();
				r.ServerError.Should().NotBeNull();
			});
	}
}
