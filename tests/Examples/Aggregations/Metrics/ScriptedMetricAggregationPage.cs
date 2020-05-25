// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Aggregations.Metrics
{
	public class ScriptedMetricAggregationPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("aggregations/metrics/scripted-metric-aggregation.asciidoc:9")]
		public void Line9()
		{
			// tag::20600097aa51aa3386536bdc681e92b6[]
			var response0 = new SearchResponse<object>();
			// end::20600097aa51aa3386536bdc681e92b6[]

			response0.MatchesExample(@"POST ledger/_search?size=0
			{
			    ""query"" : {
			        ""match_all"" : {}
			    },
			    ""aggs"": {
			        ""profit"": {
			            ""scripted_metric"": {
			                ""init_script"" : ""state.transactions = []"", \<1>
			                ""map_script"" : ""state.transactions.add(doc.type.value == 'sale' ? doc.amount.value : -1 * doc.amount.value)"",
			                ""combine_script"" : ""double profit = 0; for (t in state.transactions) { profit += t } return profit"",
			                ""reduce_script"" : ""double profit = 0; for (a in states) { profit += a } return profit""
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("aggregations/metrics/scripted-metric-aggregation.asciidoc:53")]
		public void Line53()
		{
			// tag::129ce418d8dd1f71087678725a0df19f[]
			var response0 = new SearchResponse<object>();
			// end::129ce418d8dd1f71087678725a0df19f[]

			response0.MatchesExample(@"POST ledger/_search?size=0
			{
			    ""aggs"": {
			        ""profit"": {
			            ""scripted_metric"": {
			                ""init_script"" : {
			                    ""id"": ""my_init_script""
			                },
			                ""map_script"" : {
			                    ""id"": ""my_map_script""
			                },
			                ""combine_script"" : {
			                    ""id"": ""my_combine_script""
			                },
			                ""params"": {
			                    ""field"": ""amount"" \<1>
			                },
			                ""reduce_script"" : {
			                    ""id"": ""my_reduce_script""
			                }
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("aggregations/metrics/scripted-metric-aggregation.asciidoc:150")]
		public void Line150()
		{
			// tag::75e360d03fb416f0a65ca37c662c2e9c[]
			var response0 = new SearchResponse<object>();
			// end::75e360d03fb416f0a65ca37c662c2e9c[]

			response0.MatchesExample(@"PUT /transactions/_bulk?refresh
			{""index"":{""_id"":1}}
			{""type"": ""sale"",""amount"": 80}
			{""index"":{""_id"":2}}
			{""type"": ""cost"",""amount"": 10}
			{""index"":{""_id"":3}}
			{""type"": ""cost"",""amount"": 30}
			{""index"":{""_id"":4}}
			{""type"": ""sale"",""amount"": 130}");
		}
	}
}
