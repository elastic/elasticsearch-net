using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Mapping.Types
{
	public class DatePage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line35()
		{
			// tag::645136747d37368a14ab34de8bd046c6[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();

			var response3 = new SearchResponse<object>();

			var response4 = new SearchResponse<object>();
			// end::645136747d37368a14ab34de8bd046c6[]

			response0.MatchesExample(@"PUT my_index
			{
			  ""mappings"": {
			    ""properties"": {
			      ""date"": {
			        ""type"": ""date"" \<1>
			      }
			    }
			  }
			}");

			response1.MatchesExample(@"PUT my_index/_doc/1
			{ ""date"": ""2015-01-01"" } \<2>");

			response2.MatchesExample(@"PUT my_index/_doc/2
			{ ""date"": ""2015-01-01T12:10:30Z"" } \<3>");

			response3.MatchesExample(@"PUT my_index/_doc/3
			{ ""date"": 1420070400001 } \<4>");

			response4.MatchesExample(@"GET my_index/_search
			{
			  ""sort"": { ""date"": ""asc""} \<5>
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line77()
		{
			// tag::e2a042c629429855c3bcaefffb26b7fa[]
			var response0 = new SearchResponse<object>();
			// end::e2a042c629429855c3bcaefffb26b7fa[]

			response0.MatchesExample(@"PUT my_index
			{
			  ""mappings"": {
			    ""properties"": {
			      ""date"": {
			        ""type"":   ""date"",
			        ""format"": ""yyyy-MM-dd HH:mm:ss||yyyy-MM-dd||epoch_millis""
			      }
			    }
			  }
			}");
		}
	}
}