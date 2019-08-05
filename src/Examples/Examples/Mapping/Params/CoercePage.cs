using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Mapping.Params
{
	public class CoercePage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line19()
		{
			// tag::5c734d4a7252cc155f8dc90c4785f491[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();
			// end::5c734d4a7252cc155f8dc90c4785f491[]

			response0.MatchesExample(@"PUT my_index
			{
			  ""mappings"": {
			    ""properties"": {
			      ""number_one"": {
			        ""type"": ""integer""
			      },
			      ""number_two"": {
			        ""type"": ""integer"",
			        ""coerce"": false
			      }
			    }
			  }
			}");

			response1.MatchesExample(@"PUT my_index/_doc/1
			{
			  ""number_one"": ""10"" \<1>
			}");

			response2.MatchesExample(@"PUT my_index/_doc/2
			{
			  ""number_two"": ""10"" \<2>
			}");
		}

		[U]
		[SkipExample]
		public void Line60()
		{
			// tag::dad2db81c728827a782a3fefd3399849[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();
			// end::dad2db81c728827a782a3fefd3399849[]

			response0.MatchesExample(@"PUT my_index
			{
			  ""settings"": {
			    ""index.mapping.coerce"": false
			  },
			  ""mappings"": {
			    ""properties"": {
			      ""number_one"": {
			        ""type"": ""integer"",
			        ""coerce"": true
			      },
			      ""number_two"": {
			        ""type"": ""integer""
			      }
			    }
			  }
			}");

			response1.MatchesExample(@"PUT my_index/_doc/1
			{ ""number_one"": ""10"" } \<1>");

			response2.MatchesExample(@"PUT my_index/_doc/2
			{ ""number_two"": ""10"" } \<2>");
		}
	}
}