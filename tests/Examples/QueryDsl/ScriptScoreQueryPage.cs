// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.QueryDsl
{
	public class ScriptScoreQueryPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("query-dsl/script-score-query.asciidoc:18")]
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
		[Description("query-dsl/script-score-query.asciidoc:345")]
		public void Line345()
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
