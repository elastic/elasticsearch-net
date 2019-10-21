using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.XPack.Docs.En.Security.CcsClientsIntegrations
{
	public class CrossClusterPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line49()
		{
			// tag::b13d05599a1c186137f81cad94f3fcc1[]
			var response0 = new SearchResponse<object>();
			// end::b13d05599a1c186137f81cad94f3fcc1[]

			response0.MatchesExample(@"PUT _cluster/settings
			{
			  ""persistent"": {
			    ""cluster"": {
			      ""remote"": {
			        ""one"": {
			          ""seeds"": [ ""10.0.1.1:9300"" ]
			        },
			        ""two"": {
			          ""seeds"": [ ""10.0.2.1:9300"" ]
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line82()
		{
			// tag::b06c2363c9071d9d5027a8562dd1b7ab[]
			var response0 = new SearchResponse<object>();
			// end::b06c2363c9071d9d5027a8562dd1b7ab[]

			response0.MatchesExample(@"PUT _cluster/settings
			{
			  ""persistent"": {
			    ""cluster.remote.two.seeds"": [ ""10.0.2.1:9300"" ]
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line97()
		{
			// tag::f479f0f364da5ddd096eac93bb4dd207[]
			var response0 = new SearchResponse<object>();
			// end::f479f0f364da5ddd096eac93bb4dd207[]

			response0.MatchesExample(@"POST /_security/role/cluster_two_logs
			{
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line107()
		{
			// tag::743c2583a226963fec7bd6a29e40205f[]
			var response0 = new SearchResponse<object>();
			// end::743c2583a226963fec7bd6a29e40205f[]

			response0.MatchesExample(@"POST /_security/role/cluster_two_logs
			{
			  ""cluster"": [],
			  ""indices"": [
			    {
			      ""names"": [
			        ""logs-*""
			      ],
			      ""privileges"": [
			        ""read"",
			        ""read_cross_cluster""
			      ]
			    }
			  ]
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line128()
		{
			// tag::b6f04e699bbf90f233bb0253f6844958[]
			var response0 = new SearchResponse<object>();
			// end::b6f04e699bbf90f233bb0253f6844958[]

			response0.MatchesExample(@"POST /_security/user/alice
			{
			  ""password"" : ""somepassword"",
			  ""roles"" : [ ""cluster_two_logs"" ],
			  ""full_name"" : ""Alice"",
			  ""email"" : ""alice@example.com"",
			  ""enabled"": true
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line143()
		{
			// tag::efe3cb41fcafd981b8fb51fc7337d1d0[]
			var response0 = new SearchResponse<object>();
			// end::efe3cb41fcafd981b8fb51fc7337d1d0[]

			response0.MatchesExample(@"GET two:logs-2017.04/_search
			{
			  ""query"": {
			    ""match_all"": {}
			  }
			}");
		}
	}
}