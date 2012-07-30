using System.Collections.Generic;
using NUnit.Framework;

namespace Nest.Tests.Integration.Indices
{
	[TestFixture]
	public class StatsTest : BaseElasticSearchTests
	{
		[Test]
		public void SimpleStats()
		{
			var r = this.ConnectedClient.Stats();
			Assert.True(r.OK);
			Assert.True(r.IsValid);
			Assert.NotNull(r.Stats);
			Assert.NotNull(r.Stats.Primaries);
			Assert.NotNull(r.Stats.Primaries.Documents);
			Assert.NotNull(r.Stats.Primaries.Get);
			Assert.NotNull(r.Stats.Primaries.Indexing);
			Assert.NotNull(r.Stats.Primaries.Search);
			Assert.NotNull(r.Stats.Primaries.Store);
			Assert.NotNull(r.Stats.Total);
			Assert.NotNull(r.Stats.Indices);
			Assert.True(r.Stats.Indices.Count > 0);
			var deletedOnPrimaries = r.Stats.Primaries.Documents.Deleted;

		}
		[Test]
		public void SimpleIndexStats()
		{
			var r = this.ConnectedClient.Stats(this.Settings.DefaultIndex);
			Assert.True(r.OK);
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
			var r = this.ConnectedClient.Stats(new StatsParams()
			{
				InfoOn = StatsInfo.All,
				Refresh = true,
				Types = new List<string>{ "elasticsearchprojects" }

			});
			Assert.True(r.OK);
			Assert.True(r.IsValid);
			Assert.NotNull(r.Stats);
			Assert.NotNull(r.Stats.Primaries);
			Assert.NotNull(r.Stats.Primaries.Documents);
			Assert.NotNull(r.Stats.Primaries.Get);
			Assert.NotNull(r.Stats.Primaries.Indexing);
			//possible ES bug https://github.com/elasticsearch/elasticsearch/issues/1516
			//Assert.NotNull(r.Stats.Primaries.Search);
			Assert.NotNull(r.Stats.Primaries.Store);
			Assert.NotNull(r.Stats.Primaries.Flush);
			Assert.NotNull(r.Stats.Primaries.Refresh);
			Assert.NotNull(r.Stats.Primaries.Merges);
			Assert.NotNull(r.Stats.Total);
			Assert.NotNull(r.Stats.Indices);
			Assert.True(r.Stats.Indices.Count > 0);
			Assert.NotNull(r.Stats.Primaries.Indexing.Types);
			Assert.True(r.Stats.Primaries.Indexing.Types.Count > 0);
			var deletedOnPrimaries = r.Stats.Primaries.Documents.Deleted;
			var x = r.Stats.Primaries.Indexing.Types["elasticsearchprojects"].Current;

		}
	}
}