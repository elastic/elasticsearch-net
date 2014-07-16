using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;


namespace Nest.Tests.Integration.Index
{
	[TestFixture]
	public class ReindexTests : IntegrationTests
	{
		[Test]
		public void ReindexMinimal()
		{
			var toIndex = ElasticsearchConfiguration.NewUniqueIndexName();
			var observable = this.Client.Reindex<object>(r => r
				.FromIndex(ElasticsearchConfiguration.DefaultIndex)
				.ToIndex(toIndex)
			);
			var observer = new ReindexObserver<object>(
				onError: (e) => Assert.Fail(e.Message),
				completed: () =>
				{
					var refresh = this.Client.Refresh(r=>r.Indices(toIndex, ElasticsearchConfiguration.DefaultIndex));
					var originalIndexCount = this.Client.Count(c=>c
						.Index(ElasticsearchConfiguration.DefaultIndex)
						.Query(q => q.MatchAll())
					);
					var newIndexCount = this.Client.Count(c => c
						.Index(toIndex)
						.Query(q => q.MatchAll())
					);
					Assert.Greater(newIndexCount.Count, 0);
					Assert.AreEqual(originalIndexCount.Count, newIndexCount.Count);
				}
			);

			observable.Subscribe(observer);
		}

		[Test]
		public void Reindex()
		{
			var toIndex = ElasticsearchConfiguration.NewUniqueIndexName();
			var observable = this.Client.Reindex<object>(r => r
				.FromIndex(ElasticsearchConfiguration.DefaultIndex)
				.ToIndex(toIndex)
				.Query(q=>q.MatchAll())
				.Scroll("10s")
				.CreateIndex(c=>c
					.NumberOfReplicas(0)
					.NumberOfShards(1)
				)
			);
			var observer = new ReindexObserver<object>(
				onNext: (r) =>
				{
					var scrollResults = r.SearchResponse;
					var bulkResults = r.BulkResponse;

					Assert.NotNull(scrollResults);
					Assert.NotNull(bulkResults);

					Assert.True(scrollResults.IsValid);
					Assert.True(bulkResults.IsValid);
				},
				onError: (e) => Assert.Fail(e.Message),
				completed: () =>
				{
					var refresh = this.Client.Refresh(r=>r.Indices(toIndex, ElasticsearchConfiguration.DefaultIndex));

					var originalIndexCount = this.Client.Count(c=>c
						.Index(ElasticsearchConfiguration.DefaultIndex)
						.Query(q=>q.MatchAll())
					);
					var newIndexCount = this.Client.Count(i=>i.Index(toIndex).Query(q=>q.MatchAll()));
					Assert.Greater(newIndexCount.Count, 0);
					Assert.AreEqual(originalIndexCount.Count, newIndexCount.Count);
				}
			);

			observable.Subscribe(observer);
		}

	}
}
