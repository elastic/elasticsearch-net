using Elastic.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Mapping.Types
{
	public class DateNanosPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("mapping/types/date_nanos.asciidoc:32")]
		public void Line32()
		{
			// tag::46dd5948cfc34adf1dfe024fc960bb01[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();

			var response3 = new SearchResponse<object>();

			var response4 = new SearchResponse<object>();

			var response5 = new SearchResponse<object>();

			var response6 = new SearchResponse<object>();
			// end::46dd5948cfc34adf1dfe024fc960bb01[]

			response0.MatchesExample(@"PUT my_index
			{
			  ""mappings"": {
			    ""properties"": {
			      ""date"": {
			        ""type"": ""date_nanos"" <1>
			      }
			    }
			  }
			}");

			response1.MatchesExample(@"PUT my_index/_doc/1
			{ ""date"": ""2015-01-01"" } <2>");

			response2.MatchesExample(@"PUT my_index/_doc/2
			{ ""date"": ""2015-01-01T12:10:30.123456789Z"" } <3>");

			response3.MatchesExample(@"PUT my_index/_doc/3
			{ ""date"": 1420070400 } <4>");

			response4.MatchesExample(@"GET my_index/_search
			{
			  ""sort"": { ""date"": ""asc""} <5>
			}");

			response5.MatchesExample(@"GET my_index/_search
			{
			  ""script_fields"" : {
			    ""my_field"" : {
			      ""script"" : {
			        ""lang"" : ""painless"",
			        ""source"" : ""doc['date'].value.nano"" <6>
			      }
			    }
			  }
			}");

			response6.MatchesExample(@"GET my_index/_search
			{
			  ""docvalue_fields"" : [
			    {
			      ""field"" : ""my_ip_field"",
			      ""format"": ""strict_date_time"" <7>
			    }
			  ]
			}");
		}
	}
}