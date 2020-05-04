// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Aggregations.Bucket
{
	public class FilterAggregationPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("aggregations/bucket/filter-aggregation.asciidoc:9")]
		public void Line9()
		{
			// tag::b93ed4ef309819734f0eeea82e8b0f1f[]
			var response0 = new SearchResponse<object>();
			// end::b93ed4ef309819734f0eeea82e8b0f1f[]

			response0.MatchesExample(@"POST /sales/_search?size=0
			{
			    ""aggs"" : {
			        ""t_shirts"" : {
			            ""filter"" : { ""term"": { ""type"": ""t-shirt"" } },
			            ""aggs"" : {
			                ""avg_price"" : { ""avg"" : { ""field"" : ""price"" } }
			            }
			        }
			    }
			}");
		}
	}
}