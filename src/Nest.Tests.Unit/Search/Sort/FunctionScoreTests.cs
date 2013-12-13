using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Unit.Search.Sort
{
    [TestFixture]
    internal class FunctionScoreTests : BaseJsonTests
    {
        [Test]
        public void TestRandomSortWithoutSeed()
        {
            var s = new SearchDescriptor<ElasticSearchProject>()
                .Query(q => q.FunctionScore(
                        fs => fs.RandomScore()
                    )
                )
                .Take(2);
            var json = TestElasticClient.Serialize(s);
            var expected = @"
                {
                    size: 2,
                      query: {
                        function_score: {
                          random_score: {}
                        }
                      }
                    }";
            Assert.True(json.JsonEquals(expected), json);
        }

        [Test]
        public void TestRandomSortWithSeed()
        {
            var seed = 222222;
            var s = new SearchDescriptor<ElasticSearchProject>()
                .Query(q => q.FunctionScore(
                        fs => fs.RandomScore(seed)
                    )
                )
                .Take(2);
            var json = TestElasticClient.Serialize(s);
            var expected = @"
                {
                    size: 2,
                      query: {
                        function_score: {
                          random_score: {
                            seed: "+seed+@"
                          }
                        }
                      }
                    }";
            Assert.True(json.JsonEquals(expected), json);
        }

        [Test]
        public void TestScriptScore()
        {
            var s = new SearchDescriptor<ElasticSearchProject>()
                .Query(q => q.FunctionScore(
                        fs => fs.ScriptScore(
                            "_score / pow(param1, param2)",
                            new { param1 = 1.75 },
                            new { param2 = 4 }
                        )
                    )
                );
            var json = TestElasticClient.Serialize(s);
            var expected = @"{
                              query: {
                                function_score: {
                                  script_score: {
                                    script: ""_score / pow(param1, param2)"",
                                    params: [
                                      {
                                        param1: 1.75
                                      },
                                      {
                                        param2: 4
                                      }
                                    ]
                                  }
                                }
                              }
                            }";

            Assert.True(json.JsonEquals(expected), json);
        }
    }
}