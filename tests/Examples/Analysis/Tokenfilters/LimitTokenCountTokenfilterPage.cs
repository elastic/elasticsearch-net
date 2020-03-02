using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Analysis.Tokenfilters
{
	public class LimitTokenCountTokenfilterPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line43()
		{
			// tag::5a3855f1b3e37d89ab7cbcc4f7ae1dd3[]
			var response0 = new SearchResponse<object>();
			// end::5a3855f1b3e37d89ab7cbcc4f7ae1dd3[]

			response0.MatchesExample(@"GET _analyze
			{
			  ""tokenizer"": ""standard"",
			    ""filter"": [
			    {
			      ""type"": ""limit"",
			      ""max_token_count"": 2
			    }
			  ],
			  ""text"": ""quick fox jumps over lazy dog""
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line96()
		{
			// tag::b96f465abb658fe32889c3d183f159a3[]
			var response0 = new SearchResponse<object>();
			// end::b96f465abb658fe32889c3d183f159a3[]

			response0.MatchesExample(@"PUT limit_example
			{
			  ""settings"": {
			    ""analysis"": {
			      ""analyzer"": {
			        ""standard_one_token_limit"": {
			          ""tokenizer"": ""standard"",
			          ""filter"": [ ""limit"" ]
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line123()
		{
			// tag::63521e0089c631d6668c44a0a9d7fdcc[]
			var response0 = new SearchResponse<object>();
			// end::63521e0089c631d6668c44a0a9d7fdcc[]

			response0.MatchesExample(@"PUT custom_limit_example
			{
			  ""settings"": {
			    ""analysis"": {
			      ""analyzer"": {
			        ""whitespace_five_token_limit"": {
			          ""tokenizer"": ""whitespace"",
			          ""filter"": [ ""five_token_limit"" ]
			        }
			      },
			      ""filter"": {
			        ""five_token_limit"": {
			          ""type"": ""limit"",
			          ""max_token_count"": 5
			        }
			      }
			    }
			  }
			}");
		}
	}
}