using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Search.Request
{
	public class DocvalueFieldsPage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line8()
		{
			// tag::097a6bc1d76c3fc92fb299001d27896e[]
			var response0 = new SearchResponse<object>();
			// end::097a6bc1d76c3fc92fb299001d27896e[]

			response0.MatchesExample(@"GET /_search
			{
			    ""query"" : {
			        ""match_all"": {}
			    },
			    ""docvalue_fields"" : [
			        ""my_ip_field"", \<1>
			        {
			            ""field"": ""my_keyword_field"" \<2>
			        },
			        {
			            ""field"": ""my_date_field"",
			            ""format"": ""epoch_millis"" \<3>
			        }
			    ]
			}");
		}

		[U]
		[SkipExample]
		public void Line36()
		{
			// tag::1518ad2c540fd55f9df84bbe75c81606[]
			var response0 = new SearchResponse<object>();
			// end::1518ad2c540fd55f9df84bbe75c81606[]

			response0.MatchesExample(@"GET /_search
			{
			    ""query"" : {
			        ""match_all"": {}
			    },
			    ""docvalue_fields"" : [
			        {
			            ""field"": ""*_date_field"", \<1>
			            ""format"": ""epoch_millis"" \<2>
			        }
			    ]
			}");
		}
	}
}