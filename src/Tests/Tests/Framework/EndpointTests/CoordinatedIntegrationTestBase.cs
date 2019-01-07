using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Elastic.Managed.Ephemeral;
using Elastic.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Core.ManagedElasticsearch.NodeSeeders;
using Tests.Domain;
using Tests.Framework.Integration;
using Xunit;

namespace Tests.Framework
{
	public abstract class CoordinatedIntegrationTestBase<TCluster>
		: IClusterFixture<TCluster>, IClassFixture<EndpointUsage>
		where TCluster : IEphemeralCluster<EphemeralClusterConfiguration>, INestTestCluster, new()
	{
		private readonly TCluster _cluster;
		private readonly CoordinatedUsage _coordinatedUsage;
		private readonly EndpointUsage _usage;

		protected CoordinatedIntegrationTestBase(CoordinatedUsage coordinatedUsage) => _coordinatedUsage = coordinatedUsage;

		protected async Task Assert<TResponse>(string name, Action<TResponse> assert)
			where TResponse : class, IResponse
		{
			var lazyResponses = await ExecuteOnceInOrderUntil(name);
			if (lazyResponses == null) throw new Exception($"{name} is defined but it yields no LazyResponses object");

			await AssertOnAllResponses(lazyResponses, assert);
		}

		protected async Task AssertRunsToCompletion(string name)
		{
			var lazyResponses = await ExecuteOnceInOrderUntil(name);
			if (lazyResponses == null) throw new Exception($"{name} is defined but it yields no LazyResponses object");
		}

		protected async Task AssertOnAllResponses<TResponse>(LazyResponses responses, Action<TResponse> assert)
			where TResponse : class, IResponse
		{
			foreach (var (_, value) in await responses)
			{
				if (!(value is TResponse response))
					throw new Exception($"{value.GetType()} is not expected response type {typeof(TResponse)}");

				assert(response);
			}
		}

		private async Task<LazyResponses> ExecuteOnceInOrderUntil(string name)
		{
			if (!_coordinatedUsage.Contains(name)) throw new Exception($"{name} is not a keyed after create response");

			foreach (var lazyResponses in _coordinatedUsage)
			{
				await lazyResponses;
				if (lazyResponses.Name == name) return lazyResponses;
			}
			return null;
		}
	}

	public class CoordinatedUsage : KeyedCollection<string, LazyResponses>
	{
		public static readonly IResponse VoidResponse = new PingResponse();
		private readonly INestTestCluster _cluster;
		private readonly EndpointUsage _usage;

		public CoordinatedUsage(INestTestCluster cluster, EndpointUsage usage, string prefix = null)
		{
			_cluster = cluster;
			_usage = usage;
			Prefix = prefix;
		}

		protected IElasticClient Client => _cluster.Client;
		private string Prefix { get; }
		private static string RandomFluent { get; } = $"f-{RandomString()}";
		private static string RandomFluentAsync { get; } = $"fa-{RandomString()}";
		private static string RandomInitializer { get; } = $"o-{RandomString()}";
		private static string RandomInitializerAsync { get; } = $"oa-{RandomString()}";

		protected override string GetKeyForItem(LazyResponses item) => item.Name;

		public void Add(string name, Func<CoordinatedUsage, Func<string, LazyResponses>> create)
		{
			var responses = create(this)(name);
			Add(responses);
		}

		protected static string RandomString() => Guid.NewGuid().ToString("N").Substring(0, 8);

		public Func<string, LazyResponses> Calls<TDescriptor, TInitializer, TInterface, TResponse>(
			Func<string, TInitializer> initializerBody,
			Func<string, TDescriptor, TInterface> fluentBody,
			Func<string, IElasticClient, Func<TDescriptor, TInterface>, TResponse> fluent,
			Func<string, IElasticClient, Func<TDescriptor, TInterface>, Task<TResponse>> fluentAsync,
			Func<string, IElasticClient, TInitializer, TResponse> request,
			Func<string, IElasticClient, TInitializer, Task<TResponse>> requestAsync
		)
			where TResponse : class, IResponse
			where TDescriptor : class, TInterface
			where TInitializer : class, TInterface
			where TInterface : class
		{
			var client = Client;
			return k => _usage.CallOnce(
				() => new LazyResponses(k,
					async () => await CallAllClientMethodsOverloads(initializerBody, fluentBody, fluent, fluentAsync, request, requestAsync, client))
				, k);
		}

		public Func<string, LazyResponses> Call(Func<string, IElasticClient, Task> call) =>
			Call(async (s, c) =>
			{
				await call(s, c);

				return VoidResponse;
			});

		public Func<string, LazyResponses> Call(Func<string, IElasticClient, Task<IResponse>> call)
		{
			var client = Client;
			return k => _usage.CallOnce(
				() => new LazyResponses(k, async () =>
				{
					var dict = new Dictionary<ClientMethod, IResponse>();
					var values = new[]
					{
						(ClientMethod.Fluent, Sanitize(RandomFluent)),
						(ClientMethod.Initializer, Sanitize(RandomInitializer)),
						(ClientMethod.FluentAsync, Sanitize(RandomFluentAsync)),
						(ClientMethod.InitializerAsync, Sanitize(RandomInitializerAsync))
					};
					foreach (var (m, v) in values)
					{
						var response = await call(v, client);
						dict.Add(m, response);
					}

					return dict;
				})
				, k);
		}

		private string Sanitize(string value) => string.IsNullOrEmpty(Prefix) ? value : $"{Prefix}-{value}";

		private async Task<Dictionary<ClientMethod, IResponse>> CallAllClientMethodsOverloads<TDescriptor, TInitializer, TInterface, TResponse>(
			Func<string, TInitializer> initializerBody,
			Func<string, TDescriptor, TInterface> fluentBody,
			Func<string, IElasticClient, Func<TDescriptor, TInterface>, TResponse> fluent,
			Func<string, IElasticClient, Func<TDescriptor, TInterface>, Task<TResponse>> fluentAsync,
			Func<string, IElasticClient, TInitializer, TResponse> request,
			Func<string, IElasticClient, TInitializer, Task<TResponse>> requestAsync,
			IElasticClient client
		)
			where TResponse : class, IResponse
			where TDescriptor : class, TInterface
			where TInitializer : class, TInterface
			where TInterface : class
		{
			var dict = new Dictionary<ClientMethod, IResponse>();

			var sf = Sanitize(RandomFluent);
			dict.Add(ClientMethod.Fluent, fluent(sf, client, f => fluentBody(sf, f)));

			var sfa = Sanitize(RandomFluentAsync);
			dict.Add(ClientMethod.FluentAsync, await fluentAsync(sfa, client, f => fluentBody(sfa, f)));

			var si = Sanitize(RandomInitializer);
			dict.Add(ClientMethod.Initializer, request(si, client, initializerBody(si)));

			var sia = Sanitize(RandomInitializerAsync);
			dict.Add(ClientMethod.InitializerAsync, await requestAsync(sia, client, initializerBody(sia)));
			return dict;
		}
	}

	public class ACoordinatedTestBase : CoordinatedIntegrationTestBase<WritableCluster>
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

		private static readonly Project[] Data = Project.Generator.GenerateLazy(1000).ToArray();

		public ACoordinatedTestBase(WritableCluster cluster, EndpointUsage usage) : base(new CoordinatedUsage(cluster, usage, Prefix)
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
