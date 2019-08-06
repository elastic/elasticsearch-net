using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Analysis.Tokenfilters
{
	public class KeywordMarkerTokenfilterPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line25()
		{
			// tag::863c221b28ae5e58d39bd8f138291949[]
			var response0 = new SearchResponse<object>();
			// end::863c221b28ae5e58d39bd8f138291949[]

			response0.MatchesExample(@"PUT /keyword_marker_example
			{
			  ""settings"": {
			    ""analysis"": {
			      ""analyzer"": {
			        ""protect_cats"": {
			          ""type"": ""custom"",
			          ""tokenizer"": ""standard"",
			          ""filter"": [""lowercase"", ""protect_cats"", ""porter_stem""]
			        },
			        ""normal"": {
			          ""type"": ""custom"",
			          ""tokenizer"": ""standard"",
			          ""filter"": [""lowercase"", ""porter_stem""]
			        }
			      },
			      ""filter"": {
			        ""protect_cats"": {
			          ""type"": ""keyword_marker"",
			          ""keywords"": [""cats""]
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line57()
		{
			// tag::abcbf3c246c0d88831b875a601686e35[]
			var response0 = new SearchResponse<object>();
			// end::abcbf3c246c0d88831b875a601686e35[]

			response0.MatchesExample(@"POST /keyword_marker_example/_analyze
			{
			  ""analyzer"" : ""protect_cats"",
			  ""text"" : ""I like cats""
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line102()
		{
			// tag::4ab8f55a8a45d53fb1676112379c212e[]
			var response0 = new SearchResponse<object>();
			// end::4ab8f55a8a45d53fb1676112379c212e[]

			response0.MatchesExample(@"POST /keyword_marker_example/_analyze
			{
			  ""analyzer"" : ""normal"",
			  ""text"" : ""I like cats""
			}");
		}
	}
}