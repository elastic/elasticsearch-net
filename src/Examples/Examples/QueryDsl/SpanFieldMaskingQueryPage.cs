using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.QueryDsl
{
	public class SpanFieldMaskingQueryPage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line16()
		{
			// tag::b59861ad84352fee3e78bc869ccbe8b0[]
			var response0 = new SearchResponse<object>();
			// end::b59861ad84352fee3e78bc869ccbe8b0[]

			response0.MatchesExample(@"GET /_search
			{
			  ""query"": {
			    ""span_near"": {
			      ""clauses"": [
			        {
			          ""span_term"": {
			            ""text"": ""quick brown""
			          }
			        },
			        {
			          ""field_masking_span"": {
			            ""query"": {
			              ""span_term"": {
			                ""text.stems"": ""fox""
			              }
			            },
			            ""field"": ""text""
			          }
			        }
			      ],
			      ""slop"": 5,
			      ""in_order"": false
			    }
			  }
			}");
		}
	}
}