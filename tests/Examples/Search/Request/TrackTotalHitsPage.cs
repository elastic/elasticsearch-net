using Elastic.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Search.Request
{
	public class TrackTotalHitsPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("search/request/track-total-hits.asciidoc:23")]
		public void Line23()
		{
			// tag::32789ba30a73d8813b61c39619ad7d71[]
			var response0 = new SearchResponse<object>();
			// end::32789ba30a73d8813b61c39619ad7d71[]

			response0.MatchesExample(@"GET twitter/_search
			{
			    ""track_total_hits"": true,
			     ""query"": {
			        ""match"" : {
			            ""message"" : ""Elasticsearch""
			        }
			     }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("search/request/track-total-hits.asciidoc:68")]
		public void Line68()
		{
			// tag::e45cb729ed4a694b2d6cabaa55c9b5be[]
			var response0 = new SearchResponse<object>();
			// end::e45cb729ed4a694b2d6cabaa55c9b5be[]

			response0.MatchesExample(@"GET twitter/_search
			{
			    ""track_total_hits"": 100,
			     ""query"": {
			        ""match"" : {
			            ""message"" : ""Elasticsearch""
			        }
			     }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("search/request/track-total-hits.asciidoc:142")]
		public void Line142()
		{
			// tag::d9e08bca979c7ba3a9581f69470bf914[]
			var response0 = new SearchResponse<object>();
			// end::d9e08bca979c7ba3a9581f69470bf914[]

			response0.MatchesExample(@"GET twitter/_search
			{
			    ""track_total_hits"": false,
			     ""query"": {
			        ""match"" : {
			            ""message"" : ""Elasticsearch""
			        }
			     }
			}");
		}
	}
}