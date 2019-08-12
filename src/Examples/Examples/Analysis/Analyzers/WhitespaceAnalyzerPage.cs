using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Analysis.Analyzers
{
	public class WhitespaceAnalyzerPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line11()
		{
			// tag::262a778d754add491fbc9c721ac25bf0[]
			var response0 = new SearchResponse<object>();
			// end::262a778d754add491fbc9c721ac25bf0[]

			response0.MatchesExample(@"POST _analyze
			{
			  ""analyzer"": ""whitespace"",
			  ""text"": ""The 2 QUICK Brown-Foxes jumped over the lazy dog's bone.""
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line130()
		{
			// tag::31aed390c30bd4f42a5c56253695e53f[]
			var response0 = new SearchResponse<object>();
			// end::31aed390c30bd4f42a5c56253695e53f[]

			response0.MatchesExample(@"PUT /whitespace_example
			{
			  ""settings"": {
			    ""analysis"": {
			      ""analyzer"": {
			        ""rebuilt_whitespace"": {
			          ""tokenizer"": ""whitespace"",
			          ""filter"": [         \<1>
			          ]
			        }
			      }
			    }
			  }
			}");
		}
	}
}