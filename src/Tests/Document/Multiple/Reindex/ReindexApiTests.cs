using System;
using System.Threading;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using Xunit;
using static Nest.Static;

namespace Tests.Document.Multiple.Reindex
{
	[CollectionDefinition(IntegrationContext.Reindex)]
	public class ReindexCluster : ClusterBase, ICollectionFixture<ReindexCluster>, IClassFixture<EndpointUsage> { } 

	[Collection(IntegrationContext.Reindex)]
	public class ReindexApiTests : SerializationTestBase
	{
		private readonly IObservable<IReindexResponse<ILazyDocument>> _reindexResult;
		private readonly IElasticClient _client;

		private static string NewIndexName { get; } = $"dev-copy-{Guid.NewGuid().ToString("N").Substring(8)}";

		public ReindexApiTests(ReindexCluster cluster, EndpointUsage usage)
		{
			this._client = cluster.Client();
			var indexResult = this._client.IndexMany(Developer.Developers);
			this._client.Refresh(Index<Developer>());
			this._reindexResult = this._client.Reindex<ILazyDocument>(Index<Developer>(), NewIndexName, r=>r);
		}

		[I] public void ReturnsExpectedResponse()
		{
			var handle = new ManualResetEvent(false);
			var observer = new ReindexObserver<ILazyDocument>(
					onError: (e) => { throw e; },
					completed: () =>
					{
						var refresh = this._client.Refresh(NewIndexName);
						var originalIndexCount = this._client.Count<Developer>();
						var newIndexCount = this._client.Count<Developer>(c => c.Index(NewIndexName));

						originalIndexCount.Count.Should().BeGreaterThan(0).And.Be(newIndexCount.Count);

						handle.Set();
					}
				);

			this._reindexResult.Subscribe(observer);
			handle.WaitOne(TimeSpan.FromMinutes(3));
			//await this._reindexResult;
		}
	}
}
