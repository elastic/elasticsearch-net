using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Mapping.Types
{
	public class DateNanosPage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line32()
		{
			// tag::14dc06a4c28ffdc1f9dde97dc6838c1e[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();

			var response3 = new SearchResponse<object>();

			var response4 = new SearchResponse<object>();

			var response5 = new SearchResponse<object>();

			var response6 = new SearchResponse<object>();
			// end::14dc06a4c28ffdc1f9dde97dc6838c1e[]

			response0.MatchesExample(@"PUT my_index?include_type_name=true
			{
			  ""mappings"": {
			    ""_doc"": {
			      ""properties"": {
			        ""date"": {
			          ""type"": ""date_nanos"" \<1>
			        }
			      }
			    }
			  }
			}");

			response1.MatchesExample(@"PUT my_index/_doc/1
			{ ""date"": ""2015-01-01"" } \<2>");

			response2.MatchesExample(@"PUT my_index/_doc/2
			{ ""date"": ""2015-01-01T12:10:30.123456789Z"" } \<3>");

			response3.MatchesExample(@"PUT my_index/_doc/3
			{ ""date"": 1420070400 } \<4>");

			response4.MatchesExample(@"GET my_index/_search
			{
			  ""sort"": { ""date"": ""asc""} \<5>
			}");

			response5.MatchesExample(@"GET my_index/_search
			{
			  ""script_fields"" : {
			    ""my_field"" : {
			      ""script"" : {
			        ""lang"" : ""painless"",
			        ""source"" : ""doc['date'].date.nanos"" \<6>
			      }
			    }
			  }
			}");

			response6.MatchesExample(@"GET my_index/_search
			{
			  ""docvalue_fields"" : [
			    {
			      ""field"" : ""my_ip_field"",
			      ""format"": ""strict_date_time"" \<7>
			    }
			  ]
			}");
		}
	}
}