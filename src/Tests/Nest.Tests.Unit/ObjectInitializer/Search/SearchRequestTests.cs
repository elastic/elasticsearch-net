using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Elasticsearch.Net;
using FluentAssertions;
using Nest.Resolvers;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Unit.ObjectInitializer.Search
{
	[TestFixture]
	public class SearchRequestTests : BaseJsonTests
	{
		private readonly IElasticsearchResponse _status;

		public SearchRequestTests()
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

			var request = new SearchRequest<ElasticsearchProject>
			{
				From = 0,
				Size = 20,
				Explain = true,
				TrackScores = true,
				MinScore = 2.1,
				IndicesBoost = new Dictionary<IndexNameMarker, double>
				{
					{ Infer.Index<ElasticsearchProject>(), 2.3 }
				},
				Sort = new List<KeyValuePair<PropertyPathMarker, ISort>>()
				{
					new KeyValuePair<PropertyPathMarker, ISort>("field", new Sort { Order = SortOrder.Ascending, Missing = "_first"})
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
				Rescore = new Rescore
				{
					WindowSize = 10,
					Query = new RescoreQuery
					{
						Query = new TermQuery() { }.ToContainer(),
						QueryWeight = 1.2,
						RescoreQueryWeight = 2.1
					}
				},
				Fields = new[]
				{
					"field",
					Property.Path<ElasticsearchProject>(p=>p.Name)
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
				Source = new SourceFilter
				{
					Include = new PropertyPathMarker[]
					{
						"na*"
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
							ExecutionHint = TermsAggregationExecutionHint.GlobalOrdinals,
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
				},
				Query = query,
				Filter = new FilterContainer(new BoolFilter
				{
					Cache = true,
					Must = new FilterContainer[]
					{
						new TermFilter { Field = "value", Value = "asdasd"}
					}
				})
			};
			var response = this._client.Search<ElasticsearchProject>(request);
			this._status = response.ConnectionStatus;
		}

		[Test]
		public void Url()
		{
			this._status.RequestUrl.Should().EndWith("/nest_test_data/elasticsearchprojects/_search");
			this._status.RequestMethod.Should().Be("POST");
		}
		
		[Test]
		public void SearchBody()
		{
			this.JsonEquals(this._status.Request, MethodBase.GetCurrentMethod());
		}
	}
}
