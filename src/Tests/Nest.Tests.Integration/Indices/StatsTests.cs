using System.Collections.Generic;
using Elasticsearch.Net;
using NUnit.Framework;

namespace Nest.Tests.Integration.Indices
{
	[TestFixture]
	public class StatsTest : IntegrationTests
	{
		[Test]
		public void SimpleStats()
		{
			var r = this.Client.IndicesStats();
			Assert.True(r.IsValid);
			Assert.NotNull(r.Stats);
			Assert.NotNull(r.Stats.Primaries);
			Assert.NotNull(r.Stats.Primaries.Documents);
			Assert.NotNull(r.Stats.Primaries.Get);
			Assert.NotNull(r.Stats.Primaries.Indexing);
			Assert.NotNull(r.Stats.Primaries.Search);
			Assert.NotNull(r.Stats.Primaries.Store);
			Assert.NotNull(r.Stats.Total);
			Assert.NotNull(r.Indices);
			Assert.True(r.Indices.Count > 0);
			var deletedOnPrimaries = r.Stats.Primaries.Documents.Deleted;

		}
		[Test]
		public void SimpleIndexStats()
		{
			var r = this.Client.IndicesStats(i=>i.Index(ElasticsearchConfiguration.DefaultIndex));
			Assert.True(r.IsValid);
			Assert.NotNull(r.Stats);
			Assert.NotNull(r.Stats.Primaries);
			Assert.NotNull(r.Stats.Primaries.Documents);
			Assert.NotNull(r.Stats.Primaries.Get);
			Assert.NotNull(r.Stats.Primaries.Indexing);
			Assert.NotNull(r.Stats.Primaries.Search);
			Assert.NotNull(r.Stats.Primaries.Store);
			Assert.NotNull(r.Stats.Total);
		}
		[Test]
		public void ComplexStats()
		{
			var r = this.Client.IndicesStats(i => i
				.Types("elasticsearchprojects")
				.Metrics(IndicesStatsMetric.Completion, IndicesStatsMetric.Indexing)
			);
			Assert.True(r.IsValid);
			Assert.NotNull(r.Stats);
			Assert.NotNull(r.Stats.Primaries);
			Assert.Null(r.Stats.Primaries.Documents);
			Assert.Null(r.Stats.Primaries.Get);
			Assert.NotNull(r.Stats.Primaries.Indexing);
			//possible ES bug https://github.com/elasticsearch/elasticsearch/issues/1516
			//Assert.NotNull(r.Stats.Primaries.Search);
			Assert.Null(r.Stats.Primaries.Store);
			Assert.Null(r.Stats.Primaries.Flush);
			Assert.Null(r.Stats.Primaries.Refresh);
			Assert.Null(r.Stats.Primaries.Merges);
			Assert.NotNull(r.Stats.Total);
			Assert.NotNull(r.Indices);
			Assert.True(r.Indices.Count > 0);

		}
	}
}