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

		[U(Skip = "Example not implemented")]
		public void Line338()
		{
			// tag::e5240a59149072e8bc7532603fa813bd[]
			var response0 = new SearchResponse<object>();
			// end::e5240a59149072e8bc7532603fa813bd[]

			response0.MatchesExample(@"GET /twitter/_explain/0
			{
			    ""query"" : {
			        ""script_score"" : {
			            ""query"" : {
			                ""match"": { ""message"": ""elasticsearch"" }
			            },
			            ""script"" : {
			                ""source"" : """"""
			                  long likes = doc['likes'].value;
			                  double normalizedLikes = likes / 10;
			                  if (explanation != null) {
			                    explanation.set('normalized likes = likes / 10 = ' + likes + ' / 10 = ' + normalizedLikes);
			                  }
			                  return normalizedLikes;
			                """"""
			            }
			        }
			     }
			}");
		}
	}
}