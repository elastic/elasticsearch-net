using Elastic.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Analysis.Tokenizers
{
	public class KeywordTokenizerPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenizers/keyword-tokenizer.asciidoc:12")]
		public void Line12()
		{
			// tag::09a44b619a99f6bf3f01bd5e258fd22d[]
			var response0 = new SearchResponse<object>();
			// end::09a44b619a99f6bf3f01bd5e258fd22d[]

			response0.MatchesExample(@"POST _analyze
			{
			  ""tokenizer"": ""keyword"",
			  ""text"": ""New York""
			}");
		}
	}
}