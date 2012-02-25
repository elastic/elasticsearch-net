using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Nest.DSL;
using Nest.TestData.Domain;

namespace Nest.Tests.Dsl.Json.Filter
{
	[TestFixture]
	public class QueryStringQueryJson
	{
		[Test]
		public void QueryStringQuery()
		{
			var s = new SearchDescriptor<ElasticSearchProject>()
				.From(0)
				.Size(10)
				.Query(q=>q
					.QueryString(qs=>qs
						.OnFieldsWithBoost(d=>d
							.Add(f=>f.Name, 2.0)
							.Add(f=>f.Country, 5.0)
						)
						.Query("this AND that OR thus")
					)
			);
				
			var json = ElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				query : {
					query_string: {
						query : ""this AND that OR thus"",
						fields : [""name^2"", ""country^5""]
					}
				}
			}";
			Assert.True(json.JsonEquals(expected), json);		
		}
		[Test]
		public void QueryStringQueryWithAllOptions()
		{
			var s = new SearchDescriptor<ElasticSearchProject>()
				.From(0)
				.Size(10)
				.Query(q => q
					.QueryString(qs => qs
						.OnField(f=>f.Name)
						.Query("this that thus")
						.Operator(Operator.and)
						.Analyzer("my_analyzer")
						.AllowLeadingWildcard(true)
						.LowercaseExpendedTerms(true)
						.EnablePositionIncrements(true)
						.FuzzyPrefixLength(2)
						.FuzzyMinimumSimilarity(0.5)
						.PhraseSlop(1.0)
						.Boost(1.0)
						.AnalyzeWildcard(true)
						.AutoGeneratePhraseQueries(true)
						.MinimumShouldMatchPercentage(20)
						.UseDisMax(true)
						.TieBreaker(0.7)
					)
			);

			var json = ElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				query : {
					query_string: {
						query : ""this that thus"",
						default_field : ""name"",
						default_operator : ""and"",
						analyzer : ""my_analyzer"",
						allow_leading_wildcard: true,
						lowercase_expanded_terms: true,
						enable_position_increments: true,
						fuzzy_prefix_length : 2,
						fuzzy_min_sim: 0.5,
						phrase_slop: 1.0,
						boost: 1.0,
						analyze_wildcard: true,
						auto_generate_phrase_queries : true,
						minimum_should_match: ""20%"",
						use_dis_max: true,
						tie_breaker: 0.7
					}
				}
			}";
			Assert.True(json.JsonEquals(expected), json);
		}
	}
}
