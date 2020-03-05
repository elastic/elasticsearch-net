using Elastic.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Aggregations.Bucket
{
	public class MissingAggregationPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("aggregations/bucket/missing-aggregation.asciidoc:9")]
		public void Line9()
		{
			// tag::09dd80a4b937315d4a1aa629b22f9332[]
			var response0 = new SearchResponse<object>();
			// end::09dd80a4b937315d4a1aa629b22f9332[]

			response0.MatchesExample(@"POST /sales/_search?size=0
			{
			    ""aggs"" : {
			        ""products_without_a_price"" : {
			            ""missing"" : { ""field"" : ""price"" }
			        }
			    }
			}");
		}
	}
}