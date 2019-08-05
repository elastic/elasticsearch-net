using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Analysis.Analyzers
{
	public class ConfiguringPage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line10()
		{
			// tag::98fa08f638178692476abcae1ac8ce5a[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();

			var response3 = new SearchResponse<object>();
			// end::98fa08f638178692476abcae1ac8ce5a[]

			response0.MatchesExample(@"PUT my_index
			{
			  ""settings"": {
			    ""analysis"": {
			      ""analyzer"": {
			        ""std_english"": { \<1>
			          ""type"":      ""standard"",
			          ""stopwords"": ""_english_""
			        }
			      }
			    }
			  },
			  ""mappings"": {
			    ""properties"": {
			      ""my_text"": {
			        ""type"":     ""text"",
			        ""analyzer"": ""standard"", \<2>
			        ""fields"": {
			          ""english"": {
			            ""type"":     ""text"",
			            ""analyzer"": ""std_english"" \<3>
			          }
			        }
			      }
			    }
			  }
			}");

			response1.MatchesExample(@"POST my_index/_analyze
			{
			  ""field"": ""my_text"", \<2>
			  ""text"": ""The old brown cow""
			}");

			response2.MatchesExample(@"POST my_index/_analyze
			{
			  ""field"": ""my_text.english"", \<3>
			  ""text"": ""The old brown cow""
			}");

			response3.MatchesExample(@"");
		}
	}
}