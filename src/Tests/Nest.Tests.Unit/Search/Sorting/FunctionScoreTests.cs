using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Unit.Search.Sorting
{
	[TestFixture]
	internal class FunctionScoreTests : BaseJsonTests
	{
		[Test]
		public void TestRandomSortWithoutSeed()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
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
			var s = new SearchDescriptor<ElasticsearchProject>()
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
                            seed: " + seed + @"
                          }
                        }
                      }
                    }";
			Assert.True(json.JsonEquals(expected), json);
		}

		[Test]
		public void TestScriptScore()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.Query(q => q.FunctionScore(
						fs => fs.ScriptScore(ss => ss.Script("_score / pow(param1, param2)").Params(p => p.Add("param1", 1.75).Add("param2", 4)).Lang("mvel")
						)
					)
				);
			var json = TestElasticClient.Serialize(s);
			var expected = @"{
                          query: {
                            function_score: {
                              script_score: {
                                script: ""_score / pow(param1, param2)"",
                                params: {
                                  param1: 1.75,
                                  param2: 4
                                },
                                lang: ""mvel""
                              }
                            }
                          }
                        }";

			Assert.True(json.JsonEquals(expected), json);
		}

		[Test]
		public void TestScriptScoreWithFilter()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.Query(q => q
					.FunctionScore(fs => fs
						.Functions(func => func
							.ScriptScore(ss => ss
								.Script("_score / pow(param1, param2)")
								.Params(p => p
									.Add("param1", 1.75)
									.Add("param2", 4)
								)
								.Lang("mvel")
							).Filter(filter => filter
								.Term("term1", "termValue")
							)
						)
					)
				);
			var json = TestElasticClient.Serialize(s);
			var expected = @"{
                          query: {
                            function_score: {
                              functions : [
                                {
                                    script_score: {
                                        script: ""_score / pow(param1, param2)"",
                                        params: {
                                            param1: 1.75,
                                            param2: 4
                                        },
                                        lang: ""mvel""
                                    },
                                    filter:{
                                        term : {
                                            ""term1"":""termValue""
                                        }
                                    }
                                }
                              ]
                            }
                          }
                        }";

			Assert.True(json.JsonEquals(expected), json);
		}

		[Test]
		public void TestBoostFactorWithFilter()
		{
			var s = new SearchDescriptor<ElasticsearchProject>().Query(
				q => q.FunctionScore(
					fs => fs.Functions(
						f => f.BoostFactor(2)
							.Filter(
								filter => filter.Term("term1", "termValue")
							)
					)
				)
			);
			var json = TestElasticClient.Serialize(s);
			var expected = @"{
                          query: {
                            function_score: {
                              functions : [
                                {
                                    boost_factor: 2.0,
                                    filter:{
                                        term : {
                                            ""term1"":""termValue""
                                        }
                                    }
                                }
                              ]
                            }
                          }
                        }";

			Assert.True(json.JsonEquals(expected), json);
		}

	}
}