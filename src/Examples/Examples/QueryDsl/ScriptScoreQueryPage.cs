using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.QueryDsl
{
	public class ScriptScoreQueryPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line18()
		{
			// tag::eb35bef392e0957d609f1a26481e048d[]
			var response0 = new SearchResponse<object>();
			// end::eb35bef392e0957d609f1a26481e048d[]

			response0.MatchesExample(@"GET /_search
			{
			    ""query"" : {
			        ""script_score"" : {
			            ""query"" : {
			                ""match"": { ""message"": ""elasticsearch"" }
			            },
			            ""script"" : {
			                ""source"" : ""doc['likes'].value / 10 ""
			            }
			        }
			     }
			}");
		}
	}
}