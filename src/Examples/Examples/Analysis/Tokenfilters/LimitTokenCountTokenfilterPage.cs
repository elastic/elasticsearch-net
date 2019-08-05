using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Analysis.Tokenfilters
{
	public class LimitTokenCountTokenfilterPage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line20()
		{
			// tag::fdd46bdb2b0b0b5f8e7e502291496db8[]
			var response0 = new SearchResponse<object>();
			// end::fdd46bdb2b0b0b5f8e7e502291496db8[]

			response0.MatchesExample(@"PUT /limit_example
			{
			  ""settings"": {
			    ""analysis"": {
			      ""analyzer"": {
			        ""limit_example"": {
			          ""type"": ""custom"",
			          ""tokenizer"": ""standard"",
			          ""filter"": [""lowercase"", ""five_token_limit""]
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