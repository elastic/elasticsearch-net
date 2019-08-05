using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Analysis.Tokenizers
{
	public class ChargroupTokenizerPage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line26()
		{
			// tag::f8cafb1a08bc9b2dd5239f99d4e93f4c[]
			var response0 = new SearchResponse<object>();
			// end::f8cafb1a08bc9b2dd5239f99d4e93f4c[]

			response0.MatchesExample(@"POST _analyze
			{
			  ""tokenizer"": {
			    ""type"": ""char_group"",
			    ""tokenize_on_chars"": [
			      ""whitespace"",
			      ""-"",
			      ""\n""
			    ]
			  },
			  ""text"": ""The QUICK brown-fox""
			}");
		}
	}
}