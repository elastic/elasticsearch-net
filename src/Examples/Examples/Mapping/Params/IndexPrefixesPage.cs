using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Mapping.Params
{
	public class IndexPrefixesPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line21()
		{
			// tag::ff5d15a265855b1c11cb20ceef6a1b58[]
			var response0 = new SearchResponse<object>();
			// end::ff5d15a265855b1c11cb20ceef6a1b58[]

			response0.MatchesExample(@"PUT my_index
			{
			  ""mappings"": {
			    ""properties"": {
			      ""body_text"": {
			        ""type"": ""text"",
			        ""index_prefixes"": { }    \<1>
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line42()
		{
			// tag::b19ec4a20c19082e5c40e3b1f28bfbcb[]
			var response0 = new SearchResponse<object>();
			// end::b19ec4a20c19082e5c40e3b1f28bfbcb[]

			response0.MatchesExample(@"PUT my_index
			{
			  ""mappings"": {
			    ""properties"": {
			      ""full_name"": {
			        ""type"": ""text"",
			        ""index_prefixes"": {
			          ""min_chars"" : 1,
			          ""max_chars"" : 10
			        }
			      }
			    }
			  }
			}");
		}
	}
}