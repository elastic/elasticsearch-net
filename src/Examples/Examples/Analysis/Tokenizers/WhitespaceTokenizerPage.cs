using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Analysis.Tokenizers
{
	public class WhitespaceTokenizerPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line11()
		{
			// tag::7b9dfe5857bde1bd8483ea3241656714[]
			var response0 = new SearchResponse<object>();
			// end::7b9dfe5857bde1bd8483ea3241656714[]

			response0.MatchesExample(@"POST _analyze
			{
			  ""tokenizer"": ""whitespace"",
			  ""text"": ""The 2 QUICK Brown-Foxes jumped over the lazy dog's bone.""
			}");
		}
	}
}