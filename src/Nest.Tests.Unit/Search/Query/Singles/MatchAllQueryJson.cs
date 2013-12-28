using NUnit.Framework;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Unit.Search.Query.Singles
{
	[TestFixture]
	public class MatchAllQueryJson
	{
		[Test]
		public void TestMatchAllQuery()
		{
			var s = new SearchDescriptor<ElasticSearchProject>()
				.From(0)
				.Size(10)
				.Query(q => q.MatchAll());
			var json = TestElasticClient.Serialize(s);
			var expected = "{ from: 0, size: 10, query : { match_all: {}}}";
			Assert.True(json.JsonEquals(expected), json);
		}
		[Test]
		public void TestMatchAllShortcut()
		{
			var s = new SearchDescriptor<ElasticSearchProject>()
				.From(0)
				.Size(10)
				.MatchAll();
			var json = TestElasticClient.Serialize(s);
			var expected = "{ from: 0, size: 10, query : { match_all: {}}}";
			Assert.True(json.JsonEquals(expected), json);
		}

		[Test]
		public void TestMatchAllWithBoostQuery()
		{
			var s = new SearchDescriptor<ElasticSearchProject>()
				.From(0)
				.Size(10)
				.Query(q => q
					.MatchAll(Boost: 1.2)
				);
			var json = TestElasticClient.Serialize(s);
			var expected = "{ from: 0, size: 10, query : { match_all: { boost: 1.2 }}}";
			Assert.True(json.JsonEquals(expected), json);
		}

		[Test]
		public void TestMatchAllWithNormFieldQuery()
		{
			var s = new SearchDescriptor<ElasticSearchProject>()
				.From(0)
				.Size(10)
				.Query(q => q
					.MatchAll(NormField: "name")
				);
			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, query : { match_all: { norm_field: ""name"" }}}";
			Assert.True(json.JsonEquals(expected), json);
		}
	}
}
