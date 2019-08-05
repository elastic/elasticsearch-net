using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Aggregations.Bucket
{
	public class FilterAggregationPage : ExampleBase
	{
		[U]
		[SkipExample]
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