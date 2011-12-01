using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ElasticSearch.Client;
using Nest.TestData;
using Nest.TestData.Domain;
using NUnit.Framework;
using ElasticSearch.Client.Mapping;

namespace ElasticSearch.Tests.Search
{
	[TestFixture]
	public class StatsTest : BaseElasticSearchTests
	{
		[Test]
		public void SimpleStats()
		{
			//analyze text using default index settings
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
		}
		[Test]
		public void SimpleIndexStats()
		{
			//analyze text using default index settings
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
		
	}
}