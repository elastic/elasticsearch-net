using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Analysis
{
	public class TestingPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line9()
		{
			// tag::f0d3b58abf6f2b499a38237a0e6d3498[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::f0d3b58abf6f2b499a38237a0e6d3498[]

			response0.MatchesExample(@"POST _analyze
			{
			  ""analyzer"": ""whitespace"",
			  ""text"":     ""The quick brown fox.""
			}");

			response1.MatchesExample(@"POST _analyze
			{
			  ""tokenizer"": ""standard"",
			  ""filter"":  [ ""lowercase"", ""asciifolding"" ],
			  ""text"":      ""Is this déja vu?""
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line42()
		{
			// tag::acebf0b821acfbd6089f71e0359a56d3[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();
			// end::acebf0b821acfbd6089f71e0359a56d3[]

			response0.MatchesExample(@"PUT my_index
			{
			  ""settings"": {
			    ""analysis"": {
			      ""analyzer"": {
			        ""std_folded"": { \<1>
			          ""type"": ""custom"",
			          ""tokenizer"": ""standard"",
			          ""filter"": [
			            ""lowercase"",
			            ""asciifolding""
			          ]
			        }
			      }
			    }
			  },
			  ""mappings"": {
			    ""properties"": {
			      ""my_text"": {
			        ""type"": ""text"",
			        ""analyzer"": ""std_folded"" \<2>
			      }
			    }
			  }
			}");

			response1.MatchesExample(@"GET my_index/_analyze \<3>
			{
			  ""analyzer"": ""std_folded"", \<4>
			  ""text"":     ""Is this déjà vu?""
			}");

			response2.MatchesExample(@"GET my_index/_analyze \<3>
			{
			  ""field"": ""my_text"", \<5>
			  ""text"":  ""Is this déjà vu?""
			}");
		}
	}
}