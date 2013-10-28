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
            var s = new SearchDescriptor<ElasticSearchProject>().From(0).Size(10)
                .Query(q => q
                    .FunctionScore(fs => fs
                        .Query(qq => qq.MatchAll())
                        .Functions(
                            f => f.Gauss(x=>x.StartedOn, "42w"), 
                            f => f.BoostFactor(2)
                        )
                    )
                ).Fields(x=>x.Content);

            var json = TestElasticClient.Serialize(s);
            var expected = @"{ from: 0, size: 10, 
				query : {
						function_score : { 
							query : { match_all : {} }
						}
					},
                fields: [""content""]
			}";
            Assert.True(json.JsonEquals(expected), json);
        }
    }
}