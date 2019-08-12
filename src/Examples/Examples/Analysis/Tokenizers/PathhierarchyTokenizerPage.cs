using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Analysis.Tokenizers
{
	public class PathhierarchyTokenizerPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line12()
		{
			// tag::dc4dcfeae8a5f248639335c2c9809549[]
			var response0 = new SearchResponse<object>();
			// end::dc4dcfeae8a5f248639335c2c9809549[]

			response0.MatchesExample(@"POST _analyze
			{
			  ""tokenizer"": ""path_hierarchy"",
			  ""text"": ""/one/two/three""
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line95()
		{
			// tag::fcc35d56dff0291bcf3663830ce99254[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::fcc35d56dff0291bcf3663830ce99254[]

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
			          ""type"": ""path_hierarchy"",
			          ""delimiter"": ""-"",
			          ""replacement"": ""/"",
			          ""skip"": 2
			        }
			      }
			    }
			  }
			}");

			response1.MatchesExample(@"POST my_index/_analyze
			{
			  ""analyzer"": ""my_analyzer"",
			  ""text"": ""one-two-three-four-five""
			}");
		}
	}
}