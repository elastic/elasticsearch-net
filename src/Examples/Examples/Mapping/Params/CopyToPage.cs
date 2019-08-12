using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Mapping.Params
{
	public class CopyToPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line10()
		{
			// tag::363d200a378f8c3acc6d8a77df42eba7[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();

			var response3 = new SearchResponse<object>();
			// end::363d200a378f8c3acc6d8a77df42eba7[]

			response0.MatchesExample(@"PUT my_index
			{
			  ""mappings"": {
			    ""properties"": {
			      ""first_name"": {
			        ""type"": ""text"",
			        ""copy_to"": ""full_name"" \<1>
			      },
			      ""last_name"": {
			        ""type"": ""text"",
			        ""copy_to"": ""full_name"" \<1>
			      },
			      ""full_name"": {
			        ""type"": ""text""
			      }
			    }
			  }
			}");

			response1.MatchesExample(@"PUT my_index/_doc/1
			{
			  ""first_name"": ""John"",
			  ""last_name"": ""Smith""
			}");

			response2.MatchesExample(@"GET my_index/_search
			{
			  ""query"": {
			    ""match"": {
			      ""full_name"": { \<2>
			        ""query"": ""John Smith"",
			        ""operator"": ""and""
			      }
			    }
			  }
			}");

			response3.MatchesExample(@"");
		}
	}
}