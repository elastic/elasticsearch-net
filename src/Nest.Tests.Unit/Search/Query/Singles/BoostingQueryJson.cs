using NUnit.Framework;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Unit.Search.Query.Singles
{
	[TestFixture]
	public class BoostingQueryJson
	{
		[Test]
		public void BoostingQuery()
		{
			var s = new SearchDescriptor<ElasticSearchProject>()
				.From(0)
				.Size(10)
				.Query(qd=>qd
					.Boosting(b=>b
						.Positive(q => q.MatchAll())
						.Negative(q => q.Term(p => p.Name, "elasticsearch.pm"))
						.NegativeBoost(0.4)
					)
				);
				
			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				query : {
						""boosting"": {
							""positive"":	{
									""match_all"": {}
							},
							""negative"": {
								""term"": {
									""name"": { value: ""elasticsearch.pm"" }
								}
							},
							negative_boost: 0.4
						}
					}
			}";
			Assert.True(json.JsonEquals(expected), json);		
		}
	}
}
