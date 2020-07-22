using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using System.ComponentModel;
using Nest;

namespace Examples.Aggregations.Bucket
{
	public class VariablewidthhistogramAggregationPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("aggregations/bucket/variablewidthhistogram-aggregation.asciidoc:17")]
		public void Line17()
		{
			// tag::2203f0435921c054416c67a617cd53e7[]
			var response0 = new SearchResponse<object>();
			// end::2203f0435921c054416c67a617cd53e7[]

			response0.MatchesExample(@"POST /sales/_search?size=0
			{
			    ""aggs"" : {
			        ""prices"" : {
			            ""variable_width_histogram"" : {
			                ""field"" : ""price"",
			                ""buckets"" : 2
			            }
			        }
			    }
			}");
		}
	}
}