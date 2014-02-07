using NUnit.Framework;
using Nest.DSL.Query;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Unit.Search.Query.Singles
{
	[TestFixture]
	public class FunctionScoreQueryTests
	{
		[Test]
		public void FunctionScoreQuery()
		{
			var s = new SearchDescriptor<ElasticsearchProject>().From(0).Size(10)
				.Query(q => q
					.FunctionScore(fs => fs
						.Query(qq => qq.MatchAll())
						.Functions(
							f => f.Gauss(x => x.StartedOn, d => d.Scale("42w")),
							f => f.Linear(x => x.FloatValue, d => d.Scale("0.3")),
							f => f.Exp(x => x.DoubleValue, d => d.Scale("0.5")),
							f => f.BoostFactor(2)
						)
						.ScoreMode(FunctionScoreMode.sum)
					)
				).Fields(x => x.Content);

			var json = TestElasticClient.Serialize(s);
			var expected = @"{ 
                from: 0, size: 10, 
				query : {
                    function_score : { 
                        functions: [
                            {gauss:  { startedOn  : { scale: '42w'}}},
                            {linear: { floatValue : { scale: '0.3'}}},
                            {exp:    { doubleValue: { scale: '0.5'}}}, 
                            {boost_factor: 2.0 }
                        ],				
						query : { match_all : {} },
                        score_mode: 'sum'
					}
				},
                fields: [""content""]
			}";
			Assert.True(json.JsonEquals(expected), json);
		}
	}
}