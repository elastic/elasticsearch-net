using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Unit.QueryTests
{
	[TestFixture]
	public class BoolQueryJson
	{
		[Test]
		public void BoolQuery()
		{
			var s = new SearchDescriptor<ElasticSearchProject>()
				.From(0)
				.Size(10)
				.Query(qd=>qd
					.Bool(b=>b
						.Must(q => q.MatchAll())
						.MustNot(q => q.Term(p => p.Name, "elasticsearch.pm"))
						.Should(q => q.Term(p => p.Name, "elasticflume"))
					)
				);
			
			var json = ElasticClient.Serialize(s);
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
							]
						}
					}
			}";
			Assert.True(json.JsonEquals(expected), json);		
		}

		[Test]
		public void BoolQueryOverload()
		{
			var q1 = Query<ElasticSearchProject>.Term(p => p.Name, "elasticsearch.pm");
			var q2 = Query<ElasticSearchProject>.Term(p => p.Name, "elasticflume");

			var s = new SearchDescriptor<ElasticSearchProject>()
				.From(0)
				.Size(10)
				.Query((q1 & q2) | (q1 & q2));

			var json = ElasticClient.Serialize(s);
			
			//Assert.True(json.JsonEquals(expected), json);
		}

		[Test]
		public void BoolQueryOverloadInLambda()
		{
			var s = new SearchDescriptor<ElasticSearchProject>()
				.From(0)
				.Size(10)
				.Query(q => 
					(q.Term(p => p.Name, "elasticsearch.pm") & q.Term(p => p.Name, "elasticflume"))
					| (q.Term(p => p.Name, "elasticsearch.pm") & q.Term(p => p.Name, "elasticflume"))
				);

			var json = ElasticClient.Serialize(s);
			var expected = "{}";
			//Assert.True(json.JsonEquals(expected), json);
		}

		[Test]
		public void BoolQueryMetadata()
		{
			var s = new SearchDescriptor<ElasticSearchProject>()
				.From(0)
				.Size(10)
				.Query(qd => qd
					.Bool(b => b
						.Must(q => q.MatchAll())
						.MustNot(q => q.Term(p => p.Name, "elasticsearch.pm"))
						.Should(q => q.Term(p => p.Name, "elasticflume"))
						.MinimumNumberShouldMatch(1)
						.Boost(2.0)
					)
				);

			var json = ElasticClient.Serialize(s);
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
							minimum_number_should_match: 1,
							boost: 2.0
						}
					}
			}";
			Assert.True(json.JsonEquals(expected), json);
		}
	}
}
