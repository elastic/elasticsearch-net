using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Analysis.Analyzers
{
	public class StandardAnalyzerPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line14()
		{
			// tag::6884454f57c3a41059037ea762f48d77[]
			var response0 = new SearchResponse<object>();
			// end::6884454f57c3a41059037ea762f48d77[]

			response0.MatchesExample(@"POST _analyze
			{
			  ""analyzer"": ""standard"",
			  ""text"": ""The 2 QUICK Brown-Foxes jumped over the lazy dog's bone.""
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line151()
		{
			// tag::5af5d2999833b6b1fdcd84404751a7e3[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::5af5d2999833b6b1fdcd84404751a7e3[]

			response0.MatchesExample(@"PUT my_index
			{
			  ""settings"": {
			    ""analysis"": {
			      ""analyzer"": {
			        ""my_english_analyzer"": {
			          ""type"": ""standard"",
			          ""max_token_length"": 5,
			          ""stopwords"": ""_english_""
			        }
			      }
			    }
			  }
			}");

			response1.MatchesExample(@"POST my_index/_analyze
			{
			  ""analyzer"": ""my_english_analyzer"",
			  ""text"": ""The 2 QUICK Brown-Foxes jumped over the lazy dog's bone.""
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line281()
		{
			// tag::ccf84c1e5e5602a9e841cb8f7e3bb29f[]
			var response0 = new SearchResponse<object>();
			// end::ccf84c1e5e5602a9e841cb8f7e3bb29f[]

			response0.MatchesExample(@"PUT /standard_example
			{
			  ""settings"": {
			    ""analysis"": {
			      ""analyzer"": {
			        ""rebuilt_standard"": {
			          ""tokenizer"": ""standard"",
			          ""filter"": [
			            ""lowercase""       \<1>
			          ]
			        }
			      }
			    }
			  }
			}");
		}
	}
}