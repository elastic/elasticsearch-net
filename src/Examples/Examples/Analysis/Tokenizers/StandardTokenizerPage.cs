using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Analysis.Tokenizers
{
	public class StandardTokenizerPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line13()
		{
			// tag::88a08d0b15ef41324f5c23db533d47d1[]
			var response0 = new SearchResponse<object>();
			// end::88a08d0b15ef41324f5c23db533d47d1[]

			response0.MatchesExample(@"POST _analyze
			{
			  ""tokenizer"": ""standard"",
			  ""text"": ""The 2 QUICK Brown-Foxes jumped over the lazy dog's bone.""
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line138()
		{
			// tag::7375d4fe72c848ee3b0a799fda8bb0f0[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::7375d4fe72c848ee3b0a799fda8bb0f0[]

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
			          ""type"": ""standard"",
			          ""max_token_length"": 5
			        }
			      }
			    }
			  }
			}");

			response1.MatchesExample(@"POST my_index/_analyze
			{
			  ""analyzer"": ""my_analyzer"",
			  ""text"": ""The 2 QUICK Brown-Foxes jumped over the lazy dog's bone.""
			}");
		}
	}
}