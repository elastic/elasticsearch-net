using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Modules.Indices
{
	public class RequestCachePage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line47()
		{
			// tag::00629ea43db6ee1704183170df085495[]
			var response0 = new SearchResponse<object>();
			// end::00629ea43db6ee1704183170df085495[]

			response0.MatchesExample(@"POST /kimchy,elasticsearch/_cache/clear?request=true");
		}

		[U(Skip = "Example not implemented")]
		public void Line59()
		{
			// tag::adebfecf7485326e9f7fae9de9169abc[]
			var response0 = new SearchResponse<object>();
			// end::adebfecf7485326e9f7fae9de9169abc[]

			response0.MatchesExample(@"PUT /my_index
			{
			  ""settings"": {
			    ""index.requests.cache.enable"": false
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line72()
		{
			// tag::f22e069bc0c6f9dae57e084c662a86fd[]
			var response0 = new SearchResponse<object>();
			// end::f22e069bc0c6f9dae57e084c662a86fd[]

			response0.MatchesExample(@"PUT /my_index/_settings
			{ ""index.requests.cache.enable"": true }");
		}

		[U(Skip = "Example not implemented")]
		public void Line86()
		{
			// tag::13e9c7cdd43161f1336c94fd70a0db0c[]
			var response0 = new SearchResponse<object>();
			// end::13e9c7cdd43161f1336c94fd70a0db0c[]

			response0.MatchesExample(@"GET /my_index/_search?request_cache=true
			{
			  ""size"": 0,
			  ""aggs"": {
			    ""popular_colors"": {
			      ""terms"": {
			        ""field"": ""colors""
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line139()
		{
			// tag::36da9668fef56910370f16bfb772cc40[]
			var response0 = new SearchResponse<object>();
			// end::36da9668fef56910370f16bfb772cc40[]

			response0.MatchesExample(@"GET /_stats/request_cache?human");
		}

		[U(Skip = "Example not implemented")]
		public void Line146()
		{
			// tag::90631797c7fbda43902abf2cc0ea8304[]
			var response0 = new SearchResponse<object>();
			// end::90631797c7fbda43902abf2cc0ea8304[]

			response0.MatchesExample(@"GET /_nodes/stats/indices/request_cache?human");
		}
	}
}