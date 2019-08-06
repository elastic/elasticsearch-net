using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Analysis.Tokenizers
{
	public class LowercaseTokenizerPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line18()
		{
			// tag::a99bc141066ef673e35f306157750ec9[]
			var response0 = new SearchResponse<object>();
			// end::a99bc141066ef673e35f306157750ec9[]

			response0.MatchesExample(@"POST _analyze
			{
			  ""tokenizer"": ""lowercase"",
			  ""text"": ""The 2 QUICK Brown-Foxes jumped over the lazy dog's bone.""
			}");
		}
	}
}