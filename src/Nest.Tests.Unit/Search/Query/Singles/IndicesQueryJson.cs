using NUnit.Framework;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Unit.Search.Query.Singles
{
	[TestFixture]
	public class IndicesQueryJson
	{
		[Test]
		public void IndicesRawQuery()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.Query(q => q
					.Indices(fz => fz
						.Indices(new[] { "elasticsearchprojects", "people", "randomindex" })
						.Query("{ match_all : {} }")
						.NoMatchQuery(qq => qq.MatchAll())
					)
				);
			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, query : 
			{  
				indices: {
					query: { match_all : {} },
					no_match_query: {
						match_all: {}
					},
					indices: [
						""elasticsearchprojects"",
						""people"",
						""randomindex""
					]
				}
			}}";
			Assert.True(json.JsonEquals(expected), json);
		}
		[Test]
		public void IndicesOtherTypeQuery()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.Query(q => q
					.Indices(fz => fz
						.Indices(new[] { "elasticsearchprojects", "people", "randomindex" })
						.Query(qq => qq.Term(f => f.Name, "elasticsearch.pm"))
						.Query<Person>(qq => qq.Term(f => f.FirstName, "joe"))
						.NoMatchQuery(qq => qq.MatchAll())
					)
				);
			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, query : 
			{  
				indices: {
					query: { term : { firstName : {  value : ""joe"" }  } },
					no_match_query: {
						match_all: {}
					},
					indices: [
						""elasticsearchprojects"",
						""people"",
						""randomindex""
					]
				}
			}}";
			Assert.True(json.JsonEquals(expected), json);
		}
		[Test]
		public void IndicesQuery()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.Query(q => q
					.Indices(fz => fz
						.Indices(new[] { "elasticsearchprojects", "people", "randomindex" })
						.Query(qq => qq.Term(f => f.Name, "elasticsearch.pm"))
						.NoMatchQuery(qq => qq.MatchAll())
					)
				);
			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, query : 
			{  
				indices: {
					query: { term : { name : {  value : ""elasticsearch.pm"" }  } },
					no_match_query: {
						match_all: {}
					},
					indices: [
						""elasticsearchprojects"",
						""people"",
						""randomindex""
					]
				}
			}}";
			Assert.True(json.JsonEquals(expected), json);
		}
	}
}
