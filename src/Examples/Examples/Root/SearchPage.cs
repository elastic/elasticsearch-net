using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Root
{
	public class SearchPage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line18()
		{
			// tag::321afb79fc4ee54676a89e0cd24946c1[]
			var response0 = new SearchResponse<object>();
			// end::321afb79fc4ee54676a89e0cd24946c1[]

			response0.MatchesExample(@"POST /twitter/_doc?routing=kimchy
			{
			    ""user"" : ""kimchy"",
			    ""postDate"" : ""2009-11-15T14:12:12"",
			    ""message"" : ""trying out Elasticsearch""
			}");
		}

		[U]
		[SkipExample]
		public void Line33()
		{
			// tag::8acc1d67b152e7027e0f0e1a8b4b2431[]
			var response0 = new SearchResponse<object>();
			// end::8acc1d67b152e7027e0f0e1a8b4b2431[]

			response0.MatchesExample(@"POST /twitter/_search?routing=kimchy
			{
			    ""query"": {
			        ""bool"" : {
			            ""must"" : {
			                ""query_string"" : {
			                    ""query"" : ""some query string here""
			                }
			            },
			            ""filter"" : {
			                ""term"" : { ""user"" : ""kimchy"" }
			            }
			        }
			    }
			}");
		}

		[U]
		[SkipExample]
		public void Line74()
		{
			// tag::014b788c879e4aaa1020672e45e25473[]
			var response0 = new SearchResponse<object>();
			// end::014b788c879e4aaa1020672e45e25473[]

			response0.MatchesExample(@"PUT /_cluster/settings
			{
			    ""transient"": {
			        ""cluster.routing.use_adaptive_replica_selection"": false
			    }
			}");
		}

		[U]
		[SkipExample]
		public void Line99()
		{
			// tag::189a921df2f5b1fe580937210ce9c1c2[]
			var response0 = new SearchResponse<object>();
			// end::189a921df2f5b1fe580937210ce9c1c2[]

			response0.MatchesExample(@"POST /_search
			{
			    ""query"" : {
			        ""match_all"" : {}
			    },
			    ""stats"" : [""group1"", ""group2""]
			}");
		}
	}
}