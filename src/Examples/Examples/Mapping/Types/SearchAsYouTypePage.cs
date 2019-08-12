using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Mapping.Types
{
	public class SearchAsYouTypePage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line18()
		{
			// tag::6f31f9cfe0dd741ccad4af62ba8f815e[]
			var response0 = new SearchResponse<object>();
			// end::6f31f9cfe0dd741ccad4af62ba8f815e[]

			response0.MatchesExample(@"PUT my_index
			{
			  ""mappings"": {
			    ""properties"": {
			      ""my_field"": {
			        ""type"": ""search_as_you_type""
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line72()
		{
			// tag::867e5fad9c57055712fe2b69fa69a97c[]
			var response0 = new SearchResponse<object>();
			// end::867e5fad9c57055712fe2b69fa69a97c[]

			response0.MatchesExample(@"PUT my_index/_doc/1?refresh
			{
			  ""my_field"": ""quick brown fox jump lazy dog""
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line89()
		{
			// tag::9bd25962f177e86dbc5a8030a420cc31[]
			var response0 = new SearchResponse<object>();
			// end::9bd25962f177e86dbc5a8030a420cc31[]

			response0.MatchesExample(@"GET my_index/_search
			{
			  ""query"": {
			    ""multi_match"": {
			      ""query"": ""brown f"",
			      ""type"": ""bool_prefix"",
			      ""fields"": [
			        ""my_field"",
			        ""my_field._2gram"",
			        ""my_field._3gram""
			      ]
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line151()
		{
			// tag::0ced86822f8c0a479af5e1fe28dfc2ec[]
			var response0 = new SearchResponse<object>();
			// end::0ced86822f8c0a479af5e1fe28dfc2ec[]

			response0.MatchesExample(@"GET my_index/_search
			{
			  ""query"": {
			    ""match_phrase_prefix"": {
			      ""my_field"": ""brown f""
			    }
			  }
			}");
		}
	}
}