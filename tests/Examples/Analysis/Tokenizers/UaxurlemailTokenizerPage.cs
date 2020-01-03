using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Analysis.Tokenizers
{
	public class UaxurlemailTokenizerPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line11()
		{
			// tag::d12df43ffcdcd937bae9b26fb475e239[]
			var response0 = new SearchResponse<object>();
			// end::d12df43ffcdcd937bae9b26fb475e239[]

			response0.MatchesExample(@"POST _analyze
			{
			  ""tokenizer"": ""uax_url_email"",
			  ""text"": ""Email me at john.smith@global-international.com""
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line92()
		{
			// tag::1125986e8e55028ff4c10b5e6c7bbebb[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::1125986e8e55028ff4c10b5e6c7bbebb[]

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
			          ""type"": ""uax_url_email"",
			          ""max_token_length"": 5
			        }
			      }
			    }
			  }
			}");

			response1.MatchesExample(@"POST my_index/_analyze
			{
			  ""analyzer"": ""my_analyzer"",
			  ""text"": ""john.smith@global-international.com""
			}");
		}
	}
}