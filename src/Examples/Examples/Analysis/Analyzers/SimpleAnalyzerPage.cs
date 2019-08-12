using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Analysis.Analyzers
{
	public class SimpleAnalyzerPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line11()
		{
			// tag::1ea24f67fbbb6293d53caf2fe0c4b984[]
			var response0 = new SearchResponse<object>();
			// end::1ea24f67fbbb6293d53caf2fe0c4b984[]

			response0.MatchesExample(@"POST _analyze
			{
			  ""analyzer"": ""simple"",
			  ""text"": ""The 2 QUICK Brown-Foxes jumped over the lazy dog's bone.""
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line137()
		{
			// tag::432ab6ff7cfe06988dda436907218cc5[]
			var response0 = new SearchResponse<object>();
			// end::432ab6ff7cfe06988dda436907218cc5[]

			response0.MatchesExample(@"PUT /simple_example
			{
			  ""settings"": {
			    ""analysis"": {
			      ""analyzer"": {
			        ""rebuilt_simple"": {
			          ""tokenizer"": ""lowercase"",
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