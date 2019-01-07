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
	public class CrossClusterReplicationFollowTests : CoordinatedIntegrationTestBase<WritableCluster>
	{
		private const string CloseIndexStep = nameof(CloseIndexStep);
		private const string CountAfterStep = nameof(CountAfterStep);
		private const string CountBeforeStep = nameof(CountBeforeStep);
		private const string CreateIndexStep = nameof(CreateIndexStep);
		private const string DeleteOriginalIndicesStep = nameof(DeleteOriginalIndicesStep);
		private const string FollowIndexStep = nameof(FollowIndexStep);
		private const string FollowStatsAgainStep = nameof(FollowStatsAgainStep);
		private const string FollowStatsStep = nameof(FollowStatsStep);
		private const string IndexDataStep = nameof(IndexDataStep);
		private const string PauseFollowStep = nameof(PauseFollowStep);
		private const string PauseForCloseStep = nameof(PauseForCloseStep);
		private const string ResumeFollowStep = nameof(ResumeFollowStep);
		private const string UnfollowAgainStep = nameof(UnfollowAgainStep);
		private const string UnfollowStep = nameof(UnfollowStep);
		private const string GlobalStatsStep = nameof(GlobalStatsStep);

		private static readonly Project[] Data = Project.Generator.GenerateLazy(1000).ToArray();

		public CrossClusterReplicationFollowTests(WritableCluster cluster, EndpointUsage usage) : base(new CoordinatedUsage(cluster, usage, Prefix)
		{
			{
				CreateIndexStep, u => u.Calls<CreateIndexDescriptor, CreateIndexRequest, ICreateIndexRequest, ICreateIndexResponse>(
					v => new CreateIndexRequest(v)
					{
						Settings = new IndexSettings()
						{
							NumberOfShards = 1,
							NumberOfReplicas = 0,
							SoftDeletes = new SoftDeleteSettings
							{
								Enabled = true,
								Retention = new SoftDeleteRetentionSettings { Operations = 1024 }
							}
						}
					},
					(v, d) => d.Settings(s => s
						.NumberOfShards(1)
						.NumberOfReplicas(0)
						.SoftDeletes(sd => sd
							.Enabled()
							.Retention(r => r.Operations(1024))
						)
					),
					(v, c, f) => c.CreateIndex(v, f),
					(v, c, f) => c.CreateIndexAsync(v, f),
					(v, c, r) => c.CreateIndex(r),
					(v, c, r) => c.CreateIndexAsync(r)
				)
			},
			{
				FollowIndexStep, u =>
					u.Calls<CreateFollowIndexDescriptor, CreateFollowIndexRequest, ICreateFollowIndexRequest, ICreateFollowIndexResponse>(
						v => new CreateFollowIndexRequest(CopyIndex(v))
						{
							RemoteCluster = DefaultSeeder.RemoteClusterName,
							LeaderIndex = v
						},
						(v, d) => d
							.RemoteCluster(DefaultSeeder.RemoteClusterName)
							.LeaderIndex(v),
						(v, c, f) => c.CreateFollowIndex(CopyIndex(v), f),
						(v, c, f) => c.CreateFollowIndexAsync(CopyIndex(v), f),
						(v, c, r) => c.CreateFollowIndex(r),
						(v, c, r) => c.CreateFollowIndexAsync(r)
					)
			},
			{
				IndexDataStep, u => u.Call(async (v, c) =>
				{
					var seenPages = 0;
					var tokenSource = new CancellationTokenSource();
					var bulkAllObservable = c.BulkAll(Data, b => b.Index(v).Size(1000).RefreshOnCompleted(), tokenSource.Token);
					bulkAllObservable.Wait(TimeSpan.FromSeconds(20), x => Interlocked.Increment(ref seenPages));
				})
			},
			{
				CountBeforeStep, u => u.Call(async (v, c) => c.Count<Project>(d => d.Index(v)))
			},
			{
				PauseFollowStep, u =>
					u.Calls<PauseFollowIndexDescriptor, PauseFollowIndexRequest, IPauseFollowIndexRequest, IPauseFollowIndexResponse>(
						v => new PauseFollowIndexRequest(CopyIndex(v)),
						(v, d) => d,
						(v, c, f) => c.PauseFollowIndex(CopyIndex(v), f),
						(v, c, f) => c.PauseFollowIndexAsync(CopyIndex(v), f),
						(v, c, r) => c.PauseFollowIndex(r),
						(v, c, r) => c.PauseFollowIndexAsync(r)
					)
			},
			{
				ResumeFollowStep, u =>
					u.Calls<ResumeFollowIndexDescriptor, ResumeFollowIndexRequest, IResumeFollowIndexRequest, IResumeFollowIndexResponse>(
						v => new ResumeFollowIndexRequest(CopyIndex(v)),
						(v, d) => d,
						(v, c, f) => c.ResumeFollowIndex(CopyIndex(v), f),
						(v, c, f) => c.ResumeFollowIndexAsync(CopyIndex(v), f),
						(v, c, r) => c.ResumeFollowIndex(r),
						(v, c, r) => c.ResumeFollowIndexAsync(r)
					)
			},
			{
				CountAfterStep, u => u.Call(async (v, c) => c.Count<Project>(d => d.Index(CopyIndex(v))))
			},
			{
				FollowStatsStep, u =>
					u.Calls<FollowIndexStatsDescriptor, FollowIndexStatsRequest, IFollowIndexStatsRequest, IFollowIndexStatsResponse>(
						v => new FollowIndexStatsRequest(CopyIndex(v)),
						(v, d) => d,
						(v, c, f) => c.FollowIndexStats(CopyIndex(v), f),
						(v, c, f) => c.FollowIndexStatsAsync(CopyIndex(v), f),
						(v, c, r) => c.FollowIndexStats(r),
						(v, c, r) => c.FollowIndexStatsAsync(r)
					)
			},
			{
				DeleteOriginalIndicesStep, u => u.Calls<DeleteIndexDescriptor, DeleteIndexRequest, IDeleteIndexRequest, IDeleteIndexResponse>
				(
					v => new DeleteIndexRequest(v),
					(v, d) => d,
					(v, c, f) => c.DeleteIndex(v, f),
					(v, c, f) => c.DeleteIndexAsync(v, f),
					(v, c, r) => c.DeleteIndex(r),
					(v, c, r) => c.DeleteIndexAsync(r)
				)
			},
			{
				//This time we get exceptions on the stats
				FollowStatsAgainStep, u =>
					u.Calls<FollowIndexStatsDescriptor, FollowIndexStatsRequest, IFollowIndexStatsRequest, IFollowIndexStatsResponse>(
						v => new FollowIndexStatsRequest(CopyIndex(v)),
						(v, d) => d,
						(v, c, f) => c.FollowIndexStats(CopyIndex(v), f),
						(v, c, f) => c.FollowIndexStatsAsync(CopyIndex(v), f),
						(v, c, r) => c.FollowIndexStats(r),
						(v, c, r) => c.FollowIndexStatsAsync(r)
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
			},
			{
				UnfollowStep, u => u.Calls<UnfollowIndexDescriptor, UnfollowIndexRequest, IUnfollowIndexRequest, IUnfollowIndexResponse>
				(
					v => new UnfollowIndexRequest(CopyIndex(v)),
					(v, d) => d,
					(v, c, f) => c.UnfollowIndex(CopyIndex(v), f),
					(v, c, f) => c.UnfollowIndexAsync(CopyIndex(v), f),
					(v, c, r) => c.UnfollowIndex(r),
					(v, c, r) => c.UnfollowIndexAsync(r)
				)
			},
			{
				PauseForCloseStep, u => u.Call(async (v, c) => c.PauseFollowIndex(CopyIndex(v)))
			},
			{
				CloseIndexStep, u => u.Call(async (v, c) => c.CloseIndex(CopyIndex(v)))
			},
			{
				UnfollowAgainStep, u => u.Calls<UnfollowIndexDescriptor, UnfollowIndexRequest, IUnfollowIndexRequest, IUnfollowIndexResponse>
				(
					v => new UnfollowIndexRequest(CopyIndex(v)),
					(v, d) => d,
					(v, c, f) => c.UnfollowIndex(CopyIndex(v), f),
					(v, c, f) => c.UnfollowIndexAsync(CopyIndex(v), f),
					(v, c, r) => c.UnfollowIndex(r),
					(v, c, r) => c.UnfollowIndexAsync(r)
				)
			},
		}) { }

		protected static string Prefix { get; } = $"f{Guid.NewGuid().ToString("N").Substring(0, 4)}";

		private static string CopyIndex(string v) => $"{v}-copy";

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
			r.Indices.Count.Should().BeGreaterOrEqualTo(4);
			var currentIndices = r.Indices.Where(i => i.Index.StartsWith(Prefix)).ToArray();
			currentIndices.Should().HaveCount(4);
			foreach (var i in currentIndices)
			{
				i.Index.Should().NotBeNullOrWhiteSpace("index name");
				i.Shards.Should().NotBeEmpty();
				foreach (var s in i.Shards)
				{
					s.RemoteCluster.Should().Be(DefaultSeeder.RemoteClusterName);
					s.LeaderIndex.Should().NotBeNullOrWhiteSpace("leader_index");
					s.FollowerIndex.Should().NotBeNullOrWhiteSpace("follower_index");
					s.LeaderGlobalCheckpoint.Should().BeGreaterOrEqualTo(-1, nameof(FollowIndexShardStats.LeaderGlobalCheckpoint));
					s.LeaderMaxSequenceNumber.Should().BeGreaterOrEqualTo(-1, nameof(FollowIndexShardStats.LeaderMaxSequenceNumber));
					s.LastRequestedSequenceNumber.Should().BeGreaterOrEqualTo(-1, nameof(FollowIndexShardStats.LastRequestedSequenceNumber));
					s.ReadExceptions.Should().NotBeNull().And.BeEmpty();
				}
			}
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
			foreach (var i in currentIndices)
			{
				i.Index.Should().NotBeNullOrWhiteSpace("index name");
				i.Shards.Should().NotBeEmpty();
				foreach (var s in i.Shards)
				{
					s.RemoteCluster.Should().Be(DefaultSeeder.RemoteClusterName);
					s.LeaderIndex.Should().NotBeNullOrWhiteSpace("leader_index");
					s.FollowerIndex.Should().NotBeNullOrWhiteSpace("follower_index");
					s.LeaderGlobalCheckpoint.Should().BeGreaterOrEqualTo(-1, nameof(FollowIndexShardStats.LeaderGlobalCheckpoint));
					s.LeaderMaxSequenceNumber.Should().BeGreaterOrEqualTo(-1, nameof(FollowIndexShardStats.LeaderMaxSequenceNumber));
					s.LastRequestedSequenceNumber.Should().BeGreaterOrEqualTo(-1, nameof(FollowIndexShardStats.LastRequestedSequenceNumber));
					s.ReadExceptions.Should().NotBeNull().And.BeEmpty();
				}
			}
		});

		[I] public async Task LeadersAreDeleted() =>
			await Assert<DeleteIndexResponse>(DeleteOriginalIndicesStep, r => r.Acknowledged.Should().BeTrue());

		[I] public async Task FollowStatsResponseAgain() => await Assert<FollowIndexStatsResponse>(FollowStatsAgainStep, r =>
		{
			r.IsValid.Should().BeTrue();
			r.Indices.Should().NotBeEmpty();
			var currentIndices = r.Indices.Where(i => i.Index.StartsWith(Prefix)).ToArray();
			currentIndices.Should().HaveCount(4);
			foreach (var i in currentIndices)
			{
				i.Index.Should().NotBeNullOrWhiteSpace("index name");
				i.Shards.Should().NotBeEmpty($"shards is empty on {i.Index}");
				foreach (var s in i.Shards)
				{
					var because = $"index: {i.Index} shard: {s.ShardId}";
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
						//eventhough read exceptions is set fatal exception can still be null (race condition?).
						s.FatalException.Should().NotBeNull($"{s.FollowerIndex}", because);
						s.FatalException.Type.Should().NotBeNullOrWhiteSpace().And.EndWith("_exception", because);
					}
				}
			}
		});

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
			await Assert<UnfollowIndexResponse>(UnfollowAgainStep, r => r.Acknowledged.Should().BeTrue());
	}
}
