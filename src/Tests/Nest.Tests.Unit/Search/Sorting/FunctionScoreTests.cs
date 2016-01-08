using System.Collections.Generic;
using Nest.Resolvers;
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
						fs => fs.Functions(ff => ff
							.RandomScore()
							.Filter(f => f
								.Term("term1", "termValue")
							)
							.Weight(1)
						)
					)
				)
				.Take(2);
			var json = TestElasticClient.Serialize(s);
			var expected = @"
                {
                    size: 2,
                      query: {
                        function_score: {
						  functions: [
                            { 
						      random_score: {},
							  filter: {
								term : {
								  ""term1"" : ""termValue""
								}
							  },
							  weight: 1.0
                            } 
						  ]
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
						fs => fs.Functions(ff => ff
							.RandomScore(seed)
							.Filter(f => f
								.Term("term1", "termValue")
							)
							.Weight(1)
						)
					)
				)
				.Take(2);
			var json = TestElasticClient.Serialize(s);
			var expected = @"
                {
                    size: 2,
                      query: {
                        function_score: {
						  functions: [
							{ 
							  random_score: {
							    seed: " + seed + @"
							  },
							  filter: {
								term : {
								  ""term1"" : ""termValue""
								}
							  },
							  weight: 1.0 
                            }
						  ]
                        }
                      }
                    }";
			Assert.True(json.JsonEquals(expected), json);
		}

		[Test]
		public void TestScriptScore()
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
							).Weight(5)
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
                                    },
									weight: 5.0
                                }
                              ]
                            }
                          }
                        }";

			Assert.True(json.JsonEquals(expected), json);
		}

		[Test]
		public void TestBoostFactor()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.Query(q => q
					.FunctionScore(fs => fs
						.Functions(
							f => f
								.BoostFactor(2)
								.Filter(
									filter => filter.Term("term1", "termValue")
								)
								.Weight(0.5)
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
                                    },
                                    weight: 0.5
                                }
                              ]
                            }
                          }
                        }";

			Assert.True(json.JsonEquals(expected), json);
		}

		[Test]
		public void TestBoostFactor_WrongOverload()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.Query(q => q
					.FunctionScore(fs => fs
						.Functions(
							f => f
								.BoostFactor(2)
								.Filter(
									filter => filter.Term("term1", "termValue")
								)
								.Weight(2)
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
                                    },
                                    weight: 2.0
                                }
                              ]
                            }
                          }
                        }";

			Assert.True(json.JsonEquals(expected), json);
		}

		[Test]
		public void TestBoostFactor_ObjectInitializer()
		{
			IFunctionScoreFunction boost = new BoostFactorFunction<ElasticsearchProject>(2);
			boost.WeightAsDouble = 0.5;
			boost.Filter = new TermFilter {Field = Property.Path<ElasticsearchProject>(p => p.Name), Value = "termValue"};
			QueryContainer q = new FunctionScoreQuery()
			{
				Functions = new[] {boost}

			};
			var json = TestElasticClient.Serialize(q);
			var expected = @"{
                            function_score: {
                              functions : [
                                {
                                    boost_factor: 2.0,
                                    filter:{
                                        term : {
                                            ""name"":""termValue""
                                        }
                                    },
                                    weight: 0.5
                                }
                              ]
                            }
                        }";

			Assert.True(json.JsonEquals(expected), json);
		}

		[Test]
		public void TestBoostFactor_ObjectInitializer_WrongProperty()
		{
			IFunctionScoreFunction boost = new BoostFactorFunction<ElasticsearchProject>(2);
			boost.Weight = 1;
			boost.Filter = new TermFilter {Field = Property.Path<ElasticsearchProject>(p => p.Name), Value = "termValue"};
			QueryContainer q = new FunctionScoreQuery()
			{
				Functions = new[] {boost}
			};
			var json = TestElasticClient.Serialize(q);
			var expected = @"{
                            function_score: {
                              functions : [
                                {
                                    boost_factor: 2.0,
                                    filter:{
                                        term : {
                                            ""name"":""termValue""
                                        }
                                    },
                                    weight: 1.0
                                }
                              ]
                            }
                        }";

			Assert.True(json.JsonEquals(expected), json);
		}
		[Test]
		public void TestDecayFunction()
		{
			var s = new SearchDescriptor<ElasticsearchProject>().Query(
				q => q.FunctionScore(
					fs => fs.Functions(
						f => f.Gauss("floatValue", g => g.Origin("5").Scale("0.1"))
							  .Filter(gf => gf.Term("term1", "termValue"))
							  .Weight(5)
						)
					)
				);
			var json = TestElasticClient.Serialize(s);
			var expected = @"{
							  ""query"": {
								""function_score"": {
								  ""functions"": [
									{
									  ""gauss"": {
										""floatValue"": {
										  ""origin"": ""5"",
										  ""scale"": ""0.1""
										}
									  },
									  ""filter"": {
										""term"": {
										  ""term1"": ""termValue""
										}
									  },
									 ""weight"": 5.0
									}
								  ]
								}
							  }
							}";
			Assert.IsTrue(json.JsonEquals(expected), json);
		}

		[Test]
		public void TestMinScore()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.Query(q => q
					.FunctionScore(fs => fs
						.MinScore(1.1f)
						.Functions(
							f => f
								.BoostFactor(2)
								.Filter(
									filter => filter.Term("term1", "termValue")
								)
								.Weight(0.5)
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
                                    },
                                    weight: 0.5
                                }
                              ],
                              min_score: 1.1
                            }
                          }
                        }";

			Assert.True(json.JsonEquals(expected), json);
		}
	}
}