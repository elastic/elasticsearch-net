using NUnit.Framework;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Unit.Search.Query.Singles
{
	[TestFixture]
	public class WildcardQueryJson
	{
		[Test]
		public void TestWildcardQuery()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.Query(q => q
					.Wildcard(f => f.Name, "elasticsearch.*")
				);
			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, query : 
			{ wildcard: { name : { value : ""elasticsearch.*"" } }}}";
			Assert.True(json.JsonEquals(expected));
		}
		[Test]
		public void TestWildcardWithBoostQuery()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.Query(q => q
					.Wildcard(f => f.Name, "elasticsearch.*", Boost: 1.2)
				);
			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, query : 
			{ wildcard: { name : { value : ""elasticsearch.*"", boost: 1.2 } }}}";
			Assert.True(json.JsonEquals(expected));
		}
		[Test]
		public void TestWildcardWithBoostRewriteQuery()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.Query(q => q
					.Wildcard(f => f.Name, "elasticsearch.*", Boost: 1.2, Rewrite: RewriteMultiTerm.scoring_boolean)
				);
			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, query : 
			{ wildcard: { name : { value : ""elasticsearch.*"", boost: 1.2, rewrite: ""scoring_boolean"" } }}}";
			Assert.True(json.JsonEquals(expected), json);
		}
	}
}
