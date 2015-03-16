using NUnit.Framework;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Unit.Search.Query.Singles
{
	[TestFixture]
	public class BoolQueryJson
	{

		[Test]
		public void EmptyBoolQuery()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.Query(qd => null);

			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10 }";
			Assert.True(json.JsonEquals(expected), json);
		}



		[Test]
		public void BoolQuery()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.Query(qd=>qd
					.Bool(b=>b
						.Name("named_query")
						.Must(q => q.MatchAll())
						.MustNot(q => q.Term(p => p.Name, "elasticsearch.pm"))
						.Should(q => q.Term(p => p.Name, "elasticflume"))
					)
				);
			
			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				query : {
						""bool"": {
							_name: ""named_query"",
							""must"": [
								{
									""match_all"": {}
								}
							],
							""must_not"": [
								{
									""term"": {
										""name"": { value: ""elasticsearch.pm"" }
									}
								}
							],
							""should"": [
								{
									""term"": {
										""name"": { value: ""elasticflume"" }
									}
								}
							]
						}
					}
			}";
			Assert.True(json.JsonEquals(expected), json);		
		}

		[Test]
		[Ignore]
		public void BoolQueryOverload()
		{
			var q1 = Query<ElasticsearchProject>.Term(p => p.Name, "elasticsearch.pm");
			var q2 = Query<ElasticsearchProject>.Term(p => p.Name, "elasticflume");

			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.Query((q1 & q2) | (q1 & q2));

			var json = TestElasticClient.Serialize(s);

			var expected = "{}";
			Assert.True(json.JsonEquals(expected), json);
		}

		[Test]
		[Ignore]
		public void BoolQueryOverloadAvoidUnneededNesting()
		{
			var q1 = Query<ElasticsearchProject>.Term(p => p.Name, "elasticsearch.pm");
			var q2 = Query<ElasticsearchProject>.Term(p => p.Name, "elasticflume");
			var q3 = Query<ElasticsearchProject>.Term(p => p.Name, "elastica");

			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.Query(q1 && q2 && (q3 || q1));

			var json = TestElasticClient.Serialize(s);

			var expected = "{}";
			Assert.True(json.JsonEquals(expected), json);
		}

		[Test]
		[Ignore]
		public void BoolQueryOverloadInLambda()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.Query(q => 
					(q.Term(p => p.Name, "elasticsearch.pm") & q.Term(p => p.Name, "elasticflume"))
					| (q.Term(p => p.Name, "elasticsearch.pm") & q.Term(p => p.Name, "elasticflume"))
				);

			var json = TestElasticClient.Serialize(s);
			var expected = "{}";
			Assert.True(json.JsonEquals(expected), json);
		}

		[Test]
		public void BoolQueryMetadata()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.Query(qd => qd
					.Bool(b => b
						.Must(q => q.MatchAll())
						.MustNot(q => q.Term(p => p.Name, "elasticsearch.pm"))
						.Should(q => q.Term(p => p.Name, "elasticflume"))
						.MinimumShouldMatch(1)
						.Boost(2.0)
					)
				);

			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				query : {
						""bool"": {
						
							""must"": [
								{
									""match_all"": {}
								}
							],
							""must_not"": [
								{
									""term"": {
										""name"": { value: ""elasticsearch.pm"" }
									}
								}
							],
							""should"": [
								{
									""term"": {
										""name"": { value: ""elasticflume"" }
									}
								}
							],
							minimum_should_match: ""1"",
							boost: 2.0
						}
					}
			}";
			Assert.True(json.JsonEquals(expected), json);
		}
	}
}
