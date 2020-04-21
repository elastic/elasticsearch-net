using Elastic.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Analysis.Tokenizers
{
	public class SimplepatternsplitTokenizerPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenizers/simplepatternsplit-tokenizer.asciidoc:37")]
		public void Line37()
		{
			// tag::5c28bb67716ed2bbe03c1d5d3733cb42[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::5c28bb67716ed2bbe03c1d5d3733cb42[]

			response0.MatchesExample(@"PUT my_index
			{
			  ""settings"": {
			    ""analysis"": {
			      ""analyzer"": {
			        ""my_analyzer"": {
			          ""tokenizer"": ""my_tokenizer""
			        }
			      },
			      ""tokenizer"": {
			        ""my_tokenizer"": {
			          ""type"": ""simple_pattern_split"",
			          ""pattern"": ""_""
			        }
			      }
			    }
			  }
			}");

			response1.MatchesExample(@"POST my_index/_analyze
			{
			  ""analyzer"": ""my_analyzer"",
			  ""text"": ""an_underscored_phrase""
			}");
		}
	}
}