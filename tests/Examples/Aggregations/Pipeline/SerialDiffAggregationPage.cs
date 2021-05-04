// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Aggregations.Pipeline
{
	public class SerialDiffAggregationPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("aggregations/pipeline/serial-diff-aggregation.asciidoc:64")]
		public void Line64()
		{
			// tag::b4da132cb934c33d61e2b60988c6d4a3[]
			var response0 = new SearchResponse<object>();
			// end::b4da132cb934c33d61e2b60988c6d4a3[]

			response0.MatchesExample(@"POST /_search
			{
			   ""size"": 0,
			   ""aggs"": {
			      ""my_date_histo"": {                  \<1>
			         ""date_histogram"": {
			            ""field"": ""timestamp"",
			            ""calendar_interval"": ""day""
			         },
			         ""aggs"": {
			            ""the_sum"": {
			               ""sum"": {
			                  ""field"": ""lemmings""     \<2>
			               }
			            },
			            ""thirtieth_difference"": {
			               ""serial_diff"": {                \<3>
			                  ""buckets_path"": ""the_sum"",
			                  ""lag"" : 30
			               }
			            }
			         }
			      }
			   }
			}");
		}
	}
}
