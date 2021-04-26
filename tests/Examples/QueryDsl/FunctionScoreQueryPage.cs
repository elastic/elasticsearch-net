/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;
using FluentAssertions;
using Newtonsoft.Json.Linq;

namespace Examples.QueryDsl
{
	public class FunctionScoreQueryPage : ExampleBase
	{
		[U]
		[Description("query-dsl/function-score-query.asciidoc:19")]
		public void Line19()
		{
			// tag::a42f33e15b0995bb4b6058659bfdea85[]
			var searchResponse = client.Search<object>(s => s
				.AllIndices()
				.Query(q => q
					.FunctionScore(fs => fs
						.Query(qq => qq
							.MatchAll()
						)
						.Functions(fun => fun
							.RandomScore()
						)
						.Boost(5)
						.BoostMode(FunctionBoostMode.Multiply)
					)
				)
			);
			// end::a42f33e15b0995bb4b6058659bfdea85[]

			searchResponse.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""function_score"": {
			            ""query"": { ""match_all"": {} },
			            ""boost"": ""5"",
			            ""random_score"": {}, \<1>
			            ""boost_mode"":""multiply""
			        }
			    }
			}", e => e.ApplyBodyChanges(json =>
			{
				var randomScore = json["query"]["function_score"]["random_score"];
				((JObject)json["query"]["function_score"]).Remove("random_score");
				json["query"]["function_score"]["functions"] = new JArray(new JObject { ["random_score"] = randomScore });
				json["query"]["function_score"]["boost"] = 5.0;
			}));
		}

		[U]
		[Description("query-dsl/function-score-query.asciidoc:41")]
		public void Line41()
		{
			// tag::b4a0d0ed512dffc10ee53bca2feca49b[]
			var searchResponse = client.Search<object>(s => s
				.AllIndices()
				.Query(q => q
					.FunctionScore(fs => fs
						.Query(qq => qq
							.MatchAll()
						)
						.Boost(5)
						.Functions(fun => fun
							.RandomScore(rs => rs
								.Filter(f => f
									.Match(m => m
										.Field("test")
										.Query("bar")
									)
								)
								.Weight(23)
							)
							.Weight(rs => rs
								.Filter(f => f
									.Match(m => m
										.Field("test")
										.Query("cat")
									)
								)
								.Weight(42)
							)
						)
						.MaxBoost(42)
						.BoostMode(FunctionBoostMode.Multiply)
						.ScoreMode(FunctionScoreMode.Max)
						.MinScore(42)
					)
				)
			);
			// end::b4a0d0ed512dffc10ee53bca2feca49b[]

			searchResponse.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""function_score"": {
			          ""query"": { ""match_all"": {} },
			          ""boost"": ""5"", \<1>
			          ""functions"": [
			              {
			                  ""filter"": { ""match"": { ""test"": ""bar"" } },
			                  ""random_score"": {}, \<2>
			                  ""weight"": 23
			              },
			              {
			                  ""filter"": { ""match"": { ""test"": ""cat"" } },
			                  ""weight"": 42
			              }
			          ],
			          ""max_boost"": 42,
			          ""score_mode"": ""max"",
			          ""boost_mode"": ""multiply"",
			          ""min_score"" : 42
			        }
			    }
			}", e => e.ApplyBodyChanges(json =>
			{
				json["query"]["function_score"]["boost"] = 5.0;
				json["query"]["function_score"]["max_boost"] = 42.0;
				json["query"]["function_score"]["min_score"] = 42.0;
				json["query"]["function_score"]["functions"][0]["weight"] = 23.0;
				json["query"]["function_score"]["functions"][0]["filter"]["match"]["test"].ToLongFormQuery();
				json["query"]["function_score"]["functions"][1]["filter"]["match"]["test"].ToLongFormQuery();
				json["query"]["function_score"]["functions"][1]["weight"] = 42.0;
			}));
		}

		[U]
		[Description("query-dsl/function-score-query.asciidoc:137")]
		public void Line137()
		{
			// tag::ec473de07fe89bcbac1f8e278617fe46[]
			var searchResponse = client.Search<object>(s => s
				.AllIndices()
				.Query(q => q
					.FunctionScore(fs => fs
						.Query(qq => qq
							.Match(m => m
								.Field("message")
								.Query("elasticsearch")
							)
						)
						.Functions(fun => fun
							.ScriptScore(ss => ss
								.Script(sc => sc
									.Source("Math.log(2 + doc['likes'].value)")
								)
							)
						)
					)
				)
			);
			// end::ec473de07fe89bcbac1f8e278617fe46[]

			searchResponse.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""function_score"": {
			            ""query"": {
			                ""match"": { ""message"": ""elasticsearch"" }
			            },
			            ""script_score"" : {
			                ""script"" : {
			                  ""source"": ""Math.log(2 + doc['likes'].value)""
			                }
			            }
			        }
			    }
			}", e => e.ApplyBodyChanges(json =>
			{
				var scriptScore = json["query"]["function_score"]["script_score"];
				((JObject)json["query"]["function_score"]).Remove("script_score");
				json["query"]["function_score"]["functions"] = new JArray(new JObject { ["script_score"] = scriptScore });
				json["query"]["function_score"]["query"]["match"]["message"].ToLongFormQuery();
			}));
		}

		[U]
		[Description("query-dsl/function-score-query.asciidoc:175")]
		public void Line175()
		{
			// tag::b68c85fe1b0d2f264dc0d1cbf530f319[]
			var searchResponse = client.Search<object>(s => s
				.AllIndices()
				.Query(q => q
					.FunctionScore(fs => fs
						.Query(qq => qq
							.Match(m => m
								.Field("message")
								.Query("elasticsearch")
							)
						)
						.Functions(fun => fun
							.ScriptScore(ss => ss
								.Script(sc => sc
									.Source("params.a / Math.pow(params.b, doc['likes'].value)")
									.Params(p => p
										.Add("a", 5)
										.Add("b", 1.2)
									)
								)
							)
						)
					)
				)
			);
			// end::b68c85fe1b0d2f264dc0d1cbf530f319[]

			searchResponse.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""function_score"": {
			            ""query"": {
			                ""match"": { ""message"": ""elasticsearch"" }
			            },
			            ""script_score"" : {
			                ""script"" : {
			                    ""params"": {
			                        ""a"": 5,
			                        ""b"": 1.2
			                    },
			                    ""source"": ""params.a / Math.pow(params.b, doc['likes'].value)""
			                }
			            }
			        }
			    }
			}", e => e.ApplyBodyChanges(json =>
			{
				var scriptScore = json["query"]["function_score"]["script_score"];
				((JObject)json["query"]["function_score"]).Remove("script_score");
				json["query"]["function_score"]["functions"] = new JArray(new JObject { ["script_score"] = scriptScore });
				json["query"]["function_score"]["query"]["match"]["message"].ToLongFormQuery();
			}));
		}

		[U]
		[Description("query-dsl/function-score-query.asciidoc:241")]
		public void Line241()
		{
			// tag::645c4c6e209719d3a4d25b1a629cb23b[]
			var searchResponse = client.Search<object>(s => s
				.AllIndices()
				.Query(q => q
					.FunctionScore(fs => fs
						.Functions(fun => fun
							.RandomScore(rs => rs
								.Seed(10)
								.Field("_seq_no")
							)
						)
					)
				)
			);
			// end::645c4c6e209719d3a4d25b1a629cb23b[]

			searchResponse.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""function_score"": {
			            ""random_score"": {
			                ""seed"": 10,
			                ""field"": ""_seq_no""
			            }
			        }
			    }
			}", e => e.ApplyBodyChanges(json =>
			{
				var randomScore = json["query"]["function_score"]["random_score"];
				((JObject)json["query"]["function_score"]).Remove("random_score");
				json["query"]["function_score"]["functions"] = new JArray(new JObject { ["random_score"] = randomScore });
			}));
		}

		[U]
		[Description("query-dsl/function-score-query.asciidoc:269")]
		public void Line269()
		{
			// tag::8eaf4d5dd4ab1335deefa7749fdbbcc3[]
			var searchResponse = client.Search<object>(s => s
				.AllIndices()
				.Query(q => q
					.FunctionScore(fs => fs
						.Functions(fun => fun
							.FieldValueFactor(fvf => fvf
								.Field("likes")
								.Factor(1.2)
								.Modifier(FieldValueFactorModifier.SquareRoot)
								.Missing(1)
							)
						)
					)
				)
			);
			// end::8eaf4d5dd4ab1335deefa7749fdbbcc3[]

			searchResponse.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""function_score"": {
			            ""field_value_factor"": {
			                ""field"": ""likes"",
			                ""factor"": 1.2,
			                ""modifier"": ""sqrt"",
			                ""missing"": 1
			            }
			        }
			    }
			}", e => e.ApplyBodyChanges(json =>
			{
				json["query"]["function_score"]["field_value_factor"]["missing"] = 1.0;
				var fvf = json["query"]["function_score"]["field_value_factor"];
				((JObject)json["query"]["function_score"]).Remove("field_value_factor");
				json["query"]["function_score"]["functions"] = new JArray(new JObject { ["field_value_factor"] = fvf });
			}));
		}

		[U]
		[Description("query-dsl/function-score-query.asciidoc:380")]
		public void Line380()
		{
			// tag::ec27afee074001b0e4e393611010842b[]
			var searchResponse = client.Search<object>(s => s
				.AllIndices()
				.Query(q => q
					.FunctionScore(fs => fs
						.Functions(fun => fun
							.GaussDate(g => g
								.Field("date")
								.Origin("2013-09-17")
								.Scale("10d")
								.Offset("5d")
								.Decay(0.5)
							)
						)
					)
				)
			);
			// end::ec27afee074001b0e4e393611010842b[]

			searchResponse.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""function_score"": {
			            ""gauss"": {
			                ""date"": {
			                      ""origin"": ""2013-09-17"", \<1>
			                      ""scale"": ""10d"",
			                      ""offset"": ""5d"", \<2>
			                      ""decay"" : 0.5 \<2>
			                }
			            }
			        }
			    }
			}", e => e.ApplyBodyChanges(json =>
			{
				var gauss = json["query"]["function_score"]["gauss"];
				((JObject)json["query"]["function_score"]).Remove("gauss");
				json["query"]["function_score"]["functions"] = new JArray(new JObject { ["gauss"] = gauss });
			}));
		}

		[U]
		[Description("query-dsl/function-score-query.asciidoc:578")]
		public void Line578()
		{
			// tag::df17f920b0deab3529b98df88b781f55[]
			var searchResponse = client.Search<object>(s => s
				.AllIndices()
				.Query(q => q
					.FunctionScore(fs => fs
						.Functions(fun => fun
							.Gauss(g => g
								.Field("price")
								.Origin(0)
								.Scale(20)
							)
							.GaussGeoLocation(g => g
								.Field("location")
								.Origin(new GeoLocation(11, 12))
								.Scale("2km")
							)
						)
						.Query(qq => qq
							.Match(mm => mm
								.Field("properties")
								.Query("balcony")
							)
						)
						.ScoreMode(FunctionScoreMode.Multiply)
					)
				)
			);
			// end::df17f920b0deab3529b98df88b781f55[]

			searchResponse.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""function_score"": {
			          ""functions"": [
			            {
			              ""gauss"": {
			                ""price"": {
			                  ""origin"": ""0"",
			                  ""scale"": ""20""
			                }
			              }
			            },
			            {
			              ""gauss"": {
			                ""location"": {
			                  ""origin"": ""11, 12"",
			                  ""scale"": ""2km""
			                }
			              }
			            }
			          ],
			          ""query"": {
			            ""match"": {
			              ""properties"": ""balcony""
			            }
			          },
			          ""score_mode"": ""multiply""
			        }
			    }
			}", e => e.ApplyBodyChanges(json =>
			{
				json["query"]["function_score"]["query"]["match"]["properties"].ToLongFormQuery();
				json["query"]["function_score"]["functions"][0]["gauss"]["price"]["origin"] = 0.0;
				json["query"]["function_score"]["functions"][0]["gauss"]["price"]["scale"] = 20.0;
				json["query"]["function_score"]["functions"][1]["gauss"]["location"]["origin"] = new JObject { ["lat"] = 11.0, ["lon"] = 12.0 };
			}));
		}
	}
}
