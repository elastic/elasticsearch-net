using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Nest.Resolvers;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Unit.Search.InitializerSyntax
{
	[TestFixture]
	public class InitializerExample : BaseJsonTests
	{
		[Test]
		public void FullExample_InitializerSyntax_Search()
		{
			QueryContainer query = new TermQuery()
			{
				Field = Property.Path<ElasticsearchProject>(p=>p.Name),
				Value = "value"
			} && new PrefixQuery()
			{
				Field = "prefix_field", 
				Value = "prefi", 
				Rewrite = RewriteMultiTerm.ConstantScoreBoolean
			};

			var result = _client.Search<ElasticsearchProject>(new SearchRequest
			{
				From = 0,
				Size = 20,
				MinScore = 2.1,
				Rescore = new Rescore
				{
					WindowSize = 10,
					Query = new RescoreQuery
					{
						Query = new TermQuery() {}.ToContainer(),
						QueryWeight = 1.2,
						RescoreQueryWeight = 2.1
					}
				},
				Fields = new[]
				{
					"field",
					Property.Path<ElasticsearchProject>(p=>p.Name)
				},
				Query = query,
				Filter = new FilterContainer(new BoolFilter
				{
					Cache = true,
					Must = new FilterContainer[]
					{
						new TermFilter { Field = "value", Value = "asdasd"}
					}
				}),
				TrackScores = true,
				Explain = true,
				IndicesBoost = new Dictionary<IndexNameMarker, double>
				{
					{ Infer.Index<ElasticsearchProject>(), 2.3 }
				},
				ScriptFields = new FluentDictionary<string, IScriptFilter>()
					.Add("script_field_name", new ScriptFilter
					{
						Script = "doc['loc'].value * multiplier",
						Params = new Dictionary<string, object>
						{
							{"multiplier", 4}
						}
					}),
				Sort = new Dictionary<PropertyPathMarker, ISort>()
				{
					{ "field", new Sort { Order = SortOrder.Ascending, Missing = "_first"}}
				},
				Source = new SourceFilter
				{
					Include = new PropertyPathMarker[]
					{
						"na*"
					}
				},
				Suggest = new Dictionary<string, ISuggestBucket>
				{
					{
						"suggestion", new SuggestBucket
						{
							Text = "suggest me",
							Completion = new CompletionSuggester
							{
								Analyzer = "standard",
								Field = Property.Path<ElasticsearchProject>(p=>p.Content),
								Size = 4,
								ShardSize = 10,
								Fuzzy = new FuzzySuggester
								{
									Fuzziness = Fuzziness.Ratio(0.3),
									PrefixLength = 4
								}

							}
						}
					}
				},
				Facets = new Dictionary<PropertyPathMarker, IFacetContainer>()
				{
					{ "name", new FacetContainer
					{
						Terms = new TermFacetRequest
						{
							Field = "field",
							Size = 10,
							FacetFilter = new TermFilter()
							{
								Field = "other_field",
								Value = "term"
							}.ToContainer()
						}
					}
					}
				},
				Aggregations = new Dictionary<string, IAggregationContainer>
				{
					{ "my_agg", new AggregationContainer
					{
						Terms = new TermsAggregator
						{
							Field = Property.Path<ElasticsearchProject>(p=>p.Name),
							Size = 10,
							ExecutionHint = TermsAggregationExecutionHint.Ordinals,
						},
						Aggregations = new Dictionary<string, IAggregationContainer>
						{
							{ "max_count", new AggregationContainer()
							{
								Max = new MaxAggregator()
								{
									Field = "loc"
								}
							}
							}
						}
					}}
				}
				
			});
			
			var json = result.ConnectionStatus.Request.Utf8String();
			Assert.Pass(json);
		}
	}
}
