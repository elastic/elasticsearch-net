using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Scripting
{
	public class EnginePage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line28()
		{
			// tag::d9de409a4a197ce7cbe3714e07155d34[]
			var response0 = new SearchResponse<object>();
			// end::d9de409a4a197ce7cbe3714e07155d34[]

			response0.MatchesExample(@"POST /_search
			{
			  ""query"": {
			    ""function_score"": {
			      ""query"": {
			        ""match"": {
			          ""body"": ""foo""
			        }
			      },
			      ""functions"": [
			        {
			          ""script_score"": {
			            ""script"": {
			                ""source"": ""pure_df"",
			                ""lang"" : ""expert_scripts"",
			                ""params"": {
			                    ""field"": ""body"",
			                    ""term"": ""foo""
			                }
			            }
			          }
			        }
			      ]
			    }
			  }
			}");
		}
	}
}