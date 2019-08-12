using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Analysis.Analyzers
{
	public class PatternAnalyzerPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line26()
		{
			// tag::467833bd44b35a89a7fe0d7df5f253f1[]
			var response0 = new SearchResponse<object>();
			// end::467833bd44b35a89a7fe0d7df5f253f1[]

			response0.MatchesExample(@"POST _analyze
			{
			  ""analyzer"": ""pattern"",
			  ""text"": ""The 2 QUICK Brown-Foxes jumped over the lazy dog's bone.""
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line179()
		{
			// tag::314851d590d195015a76866b92cf6b32[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::314851d590d195015a76866b92cf6b32[]

			response0.MatchesExample(@"PUT my_index
			{
			  ""settings"": {
			    ""analysis"": {
			      ""analyzer"": {
			        ""my_email_analyzer"": {
			          ""type"":      ""pattern"",
			          ""pattern"":   ""\\W|_"", \<1>
			          ""lowercase"": true
			        }
			      }
			    }
			  }
			}");

			response1.MatchesExample(@"POST my_index/_analyze
			{
			  ""analyzer"": ""my_email_analyzer"",
			  ""text"": ""John_Smith@foo-bar.com""
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line268()
		{
			// tag::9e2f7b134ac7c5e7c0119866b7a96700[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::9e2f7b134ac7c5e7c0119866b7a96700[]

			response0.MatchesExample(@"PUT my_index
			{
			  ""settings"": {
			    ""analysis"": {
			      ""analyzer"": {
			        ""camel"": {
			          ""type"": ""pattern"",
			          ""pattern"": ""([^\\p{L}\\d]+)|(?<=\\D)(?=\\d)|(?<=\\d)(?=\\D)|(?<=[\\p{L}&&[^\\p{Lu}]])(?=\\p{Lu})|(?<=\\p{Lu})(?=\\p{Lu}[\\p{L}&&[^\\p{Lu}]])""
			        }
			      }
			    }
			  }
			}");

			response1.MatchesExample(@"GET my_index/_analyze
			{
			  ""analyzer"": ""camel"",
			  ""text"": ""MooseX::FTPClass2_beta""
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line388()
		{
			// tag::f453e14bcf30853e57618bf12f83e148[]
			var response0 = new SearchResponse<object>();
			// end::f453e14bcf30853e57618bf12f83e148[]

			response0.MatchesExample(@"PUT /pattern_example
			{
			  ""settings"": {
			    ""analysis"": {
			      ""tokenizer"": {
			        ""split_on_non_word"": {
			          ""type"":       ""pattern"",
			          ""pattern"":    ""\\W+"" \<1>
			        }
			      },
			      ""analyzer"": {
			        ""rebuilt_pattern"": {
			          ""tokenizer"": ""split_on_non_word"",
			          ""filter"": [
			            ""lowercase""       \<2>
			          ]
			        }
			      }
			    }
			  }
			}");
		}
	}
}